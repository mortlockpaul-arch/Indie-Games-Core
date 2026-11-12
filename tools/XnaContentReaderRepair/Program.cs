using System.Buffers.Binary;
using System.Text;
using XnbExtractor.Compression;

// Edits reader metadata only; LZX assets are decompressed and written as uncompressed XNBs.
internal static class Program
{
    private static readonly HashSet<string> Readers = new(StringComparer.Ordinal)
    {
        "Skeleton.AnimReader", "Skeleton.CostumeReader", "Skeleton.MouthReader",
        "Skeleton.OutfitReader", "Skeleton.SkeletonReader",
        "GameplayManager.Readers.MapReader", "Cutscene.BackdropReader",
        "Cutscene.CutsceneReader"
    };

    static int Main(string[] args)
    {
        if (args.Length > 0 && args[0].Equals("--audit", StringComparison.OrdinalIgnoreCase))
            return ArchiveAudit.Run(args.Skip(1).ToArray());
        string? root = null, output = null;
        string from = "Renderer", to = "This Is Hard";
        bool apply = false;
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLowerInvariant())
            {
                case "--root": root = Next(); break;
                case "--output": output = Next(); break;
                case "--from": from = Next(); break;
                case "--to": to = Next(); break;
                case "--apply": apply = true; break;
                case "--whatif": apply = false; break;
                case "--help": Usage(); return 0;
                default: throw new ArgumentException("Unknown option: " + args[i]);
            }
            string Next() => ++i < args.Length ? args[i] : throw new ArgumentException("Missing option value");
        }
        if (root is null || !Directory.Exists(root)) { Usage(); return 2; }
        if (apply && output is null) { Console.Error.WriteLine("--apply requires --output (separate destination folder)"); return 2; }
        if (apply && Path.GetFullPath(output!).TrimEnd(Path.DirectorySeparatorChar)
            .Equals(Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
        { Console.Error.WriteLine("Output must differ from input"); return 2; }
        int scanned = 0, changed = 0, compressed = 0, errors = 0;
        foreach (string path in Directory.EnumerateFiles(root, "*.xnb", SearchOption.AllDirectories).ToArray())
        {
            if (output is not null && Path.GetFullPath(path).StartsWith(Path.GetFullPath(output).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) continue;
            scanned++;
            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                if (bytes.Length < 10 || bytes[0] != 'X' || bytes[1] != 'N' || bytes[2] != 'B')
                    throw new InvalidDataException("Not an XNB header");
                bool lzx = (bytes[5] & 0x80) != 0;
                bool lz4 = (bytes[5] & 0x40) != 0;
                if (lz4) { compressed++; Console.WriteLine("[LZ4/SKIP] " + path); continue; }
                if (lzx) bytes = DecompressLzxXnb(bytes);
                int declared = BinaryPrimitives.ReadInt32LittleEndian(bytes.AsSpan(6, 4));
                if (declared != bytes.Length) throw new InvalidDataException($"Header size {declared} != actual {bytes.Length}");
                using var source = new MemoryStream(bytes, writable: false);
                source.Position = 10;
                using var reader = new BinaryReader(source, Encoding.UTF8, leaveOpen: true);
                int count = Read7Bit(reader);
                if (count < 0 || count > 4096) throw new InvalidDataException("Invalid reader count");
                var names = new List<(string name, int version)>();
                int matches = 0;
                for (int n = 0; n < count; n++)
                {
                    string name = ReadString(reader);
                    int version = reader.ReadInt32();
                    string replacement = ReplaceReaderAssembly(name, from, to);
                    if (replacement != name) { matches++; Console.WriteLine($"  {name} => {replacement}"); }
                    names.Add((replacement, version));
                }
                if (matches == 0) continue;
                changed++;
                Console.WriteLine($"[{(apply ? "PATCH" : "WHATIF")}] {path} ({matches} reader(s)){(lzx ? " [LZX -> uncompressed]" : "")}");
                if (!apply) continue;
                long tailStart = source.Position;
                using var dest = new MemoryStream();
                dest.Write(bytes, 0, 10);
                using (var writer = new BinaryWriter(dest, Encoding.UTF8, leaveOpen: true))
                {
                    Write7Bit(writer, count);
                    foreach (var (name, version) in names) { WriteString(writer, name); writer.Write(version); }
                    writer.Flush();
                }
                dest.Write(bytes, (int)tailStart, bytes.Length - (int)tailStart);
                if (dest.Length > int.MaxValue) throw new InvalidDataException("XNB exceeds supported size");
                long newLength = dest.Length;
                dest.Position = 6;
                using (var writer = new BinaryWriter(dest, Encoding.UTF8, leaveOpen: true)) writer.Write((int)newLength);
                string relative = Path.GetRelativePath(root, path);
                string target = Path.Combine(output!, relative);
                Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                // Never overwrite an existing destination file.
                using var file = new FileStream(target, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                dest.Position = 0;
                dest.CopyTo(file);
            }
            catch (Exception ex) { errors++; Console.Error.WriteLine($"[ERROR] {path}: {ex.Message}"); }
        }
        Console.WriteLine($"Summary: scanned={scanned}, matched={changed}, compressed_skipped={compressed}, errors={errors}, mode={(apply ? "apply" : "whatif")}");
        return errors > 0 ? 1 : compressed > 0 ? 3 : 0;
    }

    static byte[] DecompressLzxXnb(byte[] bytes)
    {
        if (bytes.Length < 14) throw new InvalidDataException("Truncated compressed XNB header");
        int unpackedLength = BinaryPrimitives.ReadInt32LittleEndian(bytes.AsSpan(10, 4));
        if (unpackedLength < 1 || unpackedLength > 512 * 1024 * 1024)
            throw new InvalidDataException("Invalid or excessive decompressed XNB payload size");
        using var compressed = new MemoryStream(bytes, 14, bytes.Length - 14, writable: false);
        using var decoder = new LzxDecoderStream(compressed, unpackedLength, bytes.Length - 14);
        using var result = new MemoryStream(10 + unpackedLength);
        result.Write(bytes, 0, 10);
        decoder.CopyTo(result);
        if (result.Length != 10L + unpackedLength)
            throw new InvalidDataException("Unexpected decompressed payload length");
        byte[] output = result.ToArray();
        output[5] = (byte)(output[5] & ~0xC0);
        BinaryPrimitives.WriteInt32LittleEndian(output.AsSpan(6, 4), output.Length);
        return output;
    }

    static string ReplaceReaderAssembly(string value, string from, string to)
    {
        // Only rewrite an exact verified reader name and its immediate assembly identity.
        int comma = value.IndexOf(',');
        if (comma < 0 || !Readers.Contains(value[..comma].Trim())) return value;
        int next = value.IndexOf(',', comma + 1);
        string assembly = (next < 0 ? value[(comma + 1)..] : value[(comma + 1)..next]).Trim();
        if (!assembly.Equals(from, StringComparison.OrdinalIgnoreCase)) return value;
        // Preserve optional version/culture/token metadata, but note that identity
        // compatibility must be checked at runtime if those values are non-default.
        return value[..(comma + 1)] + " " + to + (next < 0 ? "" : value[next..]);
    }
    static int Read7Bit(BinaryReader r)
    {
        int result = 0;
        for (int i = 0; i < 5; i++)
        {
            byte b = r.ReadByte();
            if (i == 4 && (b & 0xF0) != 0) throw new InvalidDataException("Invalid 7-bit integer");
            result |= (b & 0x7F) << (7 * i);
            if ((b & 0x80) == 0) return result;
        }
        throw new InvalidDataException("Invalid 7-bit integer");
    }
    static void Write7Bit(BinaryWriter w, int value)
    {
        uint v = (uint)value;
        while (v >= 128) { w.Write((byte)(v | 0x80)); v >>= 7; }
        w.Write((byte)v);
    }
    static string ReadString(BinaryReader r)
    {
        int len = Read7Bit(r);
        if (len > 1_000_000 || len > r.BaseStream.Length - r.BaseStream.Position) throw new InvalidDataException("Invalid string length");
        return new UTF8Encoding(false, true).GetString(r.ReadBytes(len));
    }
    static void WriteString(BinaryWriter w, string s)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        Write7Bit(w, bytes.Length);
        w.Write(bytes);
    }
    static void Usage() => Console.WriteLine("Usage: dotnet run --project XnaContentReaderRepair.csproj -- --root <folder> [--from Renderer] [--to 'This Is Hard'] [--whatif | --apply --output <folder>]");
}
