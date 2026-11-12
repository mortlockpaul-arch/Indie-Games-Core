using System.Buffers.Binary;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using XnbExtractor.Compression;

internal static class ArchiveAudit
{
    record AssemblyInfo(string Path, string Name, Version Version, HashSet<string> Types);
    record ReaderRef(string Type, string Assembly, string Version, string Identity);
    static readonly StringComparer CI = StringComparer.OrdinalIgnoreCase;
    const int MaxUnpacked = 512 * 1024 * 1024;

    public static int Run(string[] args)
    {
        string root = @"D:\indie-game-archive", report = Path.Combine(Environment.CurrentDirectory, "XnaContentReaderAudit-v3.csv");
        bool includeBuilt = true;
        for (int i = 0; i < args.Length; i++)
        {
            string Next() => ++i < args.Length ? args[i] : throw new ArgumentException("Missing argument value");
            switch (args[i].ToLowerInvariant())
            {
                case "--root": root = Next(); break;
                case "--report": report = Next(); break;
                case "--no-built": includeBuilt = false; break;
                default: throw new ArgumentException("Unknown audit option " + args[i]);
            }
        }
        if (!Directory.Exists(root)) { Console.Error.WriteLine("Archive not found: " + root); return 2; }
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(report))!);
        var totals = new Dictionary<string, long>(CI);
        long files = 0, compressed = 0, records = 0;
        using var writer = new StreamWriter(report, false, new UTF8Encoding(true));
        writer.WriteLine("Game,XNB,Reader,ReferencedAssembly,ReferencedVersion,ProviderAssembly,ProviderVersion,ProviderPath,Status,Detail");
        foreach (var game in Directory.EnumerateDirectories(root).OrderBy(x => x, CI))
        {
            var assemblies = new List<AssemblyInfo>();
            foreach (var dll in SafeFiles(game, "*.dll"))
            {
                if (IsExcluded(dll, game, includeBuilt)) continue;
                try { var a = ReadAssembly(dll); if (a != null) assemblies.Add(a); }
                catch { /* unreadable or native DLL; report unresolved readers below */ }
            }
            var byName = assemblies.GroupBy(a => a.Name, CI).ToDictionary(g => g.Key, g => g.ToList(), CI);
            var byType = new Dictionary<string, List<AssemblyInfo>>(StringComparer.Ordinal);
            foreach (var a in assemblies)
                foreach (var t in a.Types)
                {
                    if (!byType.TryGetValue(t, out var list)) byType[t] = list = new();
                    list.Add(a);
                }
            long gameFiles = 0;
            foreach (var xnb in SafeFiles(game, "*.xnb"))
            {
                if (IsExcluded(xnb, game, includeBuilt)) continue;
                files++; gameFiles++;
                try
                {
                    var (readers, wasCompressed) = ReadReaders(xnb);
                    if (wasCompressed) compressed++;
                    foreach (var r in readers)
                    {
                        if (r.Type.StartsWith("Microsoft.", StringComparison.Ordinal) || r.Type.StartsWith("System.", StringComparison.Ordinal)) continue;
                        records++;
                        string status, detail = "", provider = "", providerVersion = "", providerPath = "";
                        var candidates = byName.TryGetValue(r.Assembly, out var named) ? named.Where(a => a.Types.Contains(r.Type)).ToList() : new();
                        var exact = candidates.Where(a => r.Version.Length == 0 || a.Version.ToString() == r.Version).ToList();
                        if (exact.Count > 0)
                        {
                            var a = exact.OrderBy(a => IsBuilt(a.Path) ? 1 : 0).First();
                            status = "VALID_EXTERNAL_READER"; detail = "Reader type exists in assembly with matching name/version; runtime compatibility unverified";
                            provider = a.Name; providerVersion = a.Version.ToString(); providerPath = a.Path;
                        }
                        else if (candidates.Count > 0)
                        {
                            var a = candidates[0]; status = "ASSEMBLY_VERSION_MISMATCH";
                            detail = "Reader type and assembly name found, but embedded XNB version differs";
                            provider = a.Name; providerVersion = a.Version.ToString(); providerPath = a.Path;
                        }
                        else if (byType.TryGetValue(r.Type, out var other) && other.Count > 0)
                        {
                            var a = other.OrderBy(a => IsBuilt(a.Path) ? 0 : 1).First();
                            status = "ASSEMBLY_MISMATCH"; detail = "Reader type found in different assembly; verify before rewriting";
                            provider = a.Name; providerVersion = a.Version.ToString(); providerPath = a.Path;
                        }
                        else
                        {
                            status = "MISSING_READER"; detail = "Reader type not found in indexed managed DLLs; may be in unbuilt source or missing dependency";
                        }
                        Emit(Path.GetFileName(game), xnb, r.Type, r.Assembly, r.Version, provider, providerVersion, providerPath, status, detail);
                    }
                }
                catch (NotSupportedException e) { Emit(Path.GetFileName(game), xnb, "", "", "", "", "", "", "UNSUPPORTED_COMPRESSION", e.Message); }
                catch (Exception e) { Emit(Path.GetFileName(game), xnb, "", "", "", "", "", "", "PARSE_ERROR", e.Message); }
            }
            if (gameFiles > 0) Console.WriteLine($"{Path.GetFileName(game)}: {gameFiles} XNBs");
        }
        Console.WriteLine($"Total: {files} XNBs; {compressed} LZX decompressed; {records} custom-reader records");
        foreach (var kv in totals.OrderBy(x => x.Key)) Console.WriteLine($"  {kv.Key}: {kv.Value}");
        Console.WriteLine("Report: " + report);
        return 0;

        void Emit(params string[] fields)
        {
            writer.WriteLine(string.Join(",", fields.Select(Csv)));
            string status = fields[8];
            totals[status] = totals.GetValueOrDefault(status) + 1;
        }
    }
    static string Csv(string s) => "\"" + s.Replace("\"", "\"\"").Replace("\r", " ").Replace("\n", " ") + "\"";
    static bool IsBuilt(string p) => p.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
    static bool IsExcluded(string path, string game, bool includeBuilt)
    {
        var relative = Path.GetRelativePath(game, path).Replace('\\', '/');
        if (relative.Split('/').Any(s => s.Equals("obj", StringComparison.OrdinalIgnoreCase) || s.Equals("Content-Patched", StringComparison.OrdinalIgnoreCase) || s.Equals("XnaRedirect-Test", StringComparison.OrdinalIgnoreCase))) return true;
        return !includeBuilt && relative.Split('/').Any(s => s.Equals("bin", StringComparison.OrdinalIgnoreCase));
    }
    static IEnumerable<string> SafeFiles(string folder, string pattern)
    {
        try { return Directory.EnumerateFiles(folder, pattern, SearchOption.AllDirectories).ToArray(); }
        catch (Exception e) { Console.Error.WriteLine($"[ENUMERATION WARNING] {folder}: {e.Message}"); return Array.Empty<string>(); }
    }
    static AssemblyInfo? ReadAssembly(string path)
    {
        using var stream = File.OpenRead(path);
        using var pe = new PEReader(stream);
        if (!pe.HasMetadata) return null;
        var md = pe.GetMetadataReader();
        if (!md.IsAssembly) return null;
        var a = md.GetAssemblyDefinition();
        var types = new HashSet<string>(StringComparer.Ordinal);
        foreach (var handle in md.TypeDefinitions)
        {
            var t = md.GetTypeDefinition(handle);
            string name = md.GetString(t.Name), ns = md.GetString(t.Namespace);
            if (name == "<Module>") continue;
            types.Add(ns.Length == 0 ? name : ns + "." + name);
        }
        return new AssemblyInfo(path, md.GetString(a.Name), a.Version, types);
    }
    static (List<ReaderRef> readers, bool compressed) ReadReaders(string path)
    {
        using var fs = File.OpenRead(path);
        using var header = new BinaryReader(fs, Encoding.UTF8, true);
        if (fs.Length < 10 || header.ReadByte() != 'X' || header.ReadByte() != 'N' || header.ReadByte() != 'B') throw new InvalidDataException("Not XNB");
        header.ReadByte(); header.ReadByte();
        byte flags = header.ReadByte();
        int declared = header.ReadInt32();
        if (declared != fs.Length) throw new InvalidDataException("XNB header file size mismatch");
        bool lzx = (flags & 0x80) != 0, lz4 = (flags & 0x40) != 0;
        if (lz4) throw new NotSupportedException("LZ4 compressed XNB is not supported by this decoder");
        Stream payload;
        if (lzx)
        {
            if (fs.Length < 14) throw new InvalidDataException("Truncated LZX header");
            int size = header.ReadInt32();
            if (size < 0 || size > MaxUnpacked) throw new InvalidDataException("Invalid/excessive LZX decompressed size");
            payload = new LzxDecoderStream(fs, size, checked((int)(fs.Length - 14)));
        }
        else payload = fs;
        using (payload == fs ? null : payload)
        using (var reader = new BinaryReader(payload, Encoding.UTF8, true))
        {
            int count = Read7Bit(reader);
            if (count < 0 || count > 4096) throw new InvalidDataException("Invalid reader count");
            var result = new List<ReaderRef>(count);
            for (int i = 0; i < count; i++)
            {
                string identity = ReadString(reader);
                reader.ReadInt32();
                int comma = identity.IndexOf(',');
                if (comma < 0) continue;
                string type = identity[..comma].Trim();
                string remainder = identity[(comma + 1)..];
                string assembly = remainder.Split(',')[0].Trim();
                var match = System.Text.RegularExpressions.Regex.Match(remainder, @"(?:^|,)\s*Version\s*=\s*([0-9.]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result.Add(new ReaderRef(type, assembly, match.Success ? match.Groups[1].Value : "", identity));
            }
            return (result, lzx);
        }
    }
    static int Read7Bit(BinaryReader reader)
    {
        int value = 0;
        for (int i = 0; i < 5; i++)
        {
            byte b = reader.ReadByte();
            if (i == 4 && (b & 0xF0) != 0) throw new InvalidDataException("Invalid 7-bit integer");
            value |= (b & 0x7F) << (i * 7);
            if ((b & 0x80) == 0) return value;
        }
        throw new InvalidDataException("Invalid 7-bit integer");
    }
    static string ReadString(BinaryReader r)
    {
        int length = Read7Bit(r);
        if (length < 0 || length > 1_000_000) throw new InvalidDataException("Invalid reader identity length");
        byte[] bytes = r.ReadBytes(length);
        if (bytes.Length != length) throw new EndOfStreamException();
        return new UTF8Encoding(false, true).GetString(bytes);
    }
}
