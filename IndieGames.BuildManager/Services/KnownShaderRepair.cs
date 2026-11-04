using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

/// <summary>Reconstructs only fingerprinted effects; never treats an arbitrary effect filename as a match.</summary>
public static class KnownShaderRepair
{
    const string Owner = "IndieGames.BuildManager.KnownShaders";
    const string TargetName = "BuildKnownShaders";
    static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    static readonly Dictionary<string, string[]> Fingerprints = new(StringComparer.OrdinalIgnoreCase) {
        ["RoundLine"] = ["DDA496638B1C573768477349EDAAF9B1C7E16200E423AB8A54D713EA20C023AE",
            "2101428CE16B5982FFCDFAFE878BDDD16A38D3C668AE6A44F5F943EF91375C5D"],
        ["spotlight"] = ["AA5F17EBCD82154A95AF7D0A4B4617C59C13538F17E88E8CC8AE75CBE359D9F4"],
        ["Blur"] = ["ABCA83F256CD0189A6D3AA3C744E3AB76A6C27B2F35F6F82B81008410B45CC9C"],
        ["ParticleEffect"] = ["FE654166875C3A3303BDEC9345176A9BF5356A3B3729C9DBDFA9E4D9F0B04A3B"],
        ["BloomExtract"] = ["C290DC243B2078D3A030853331F5B405B07E2D363DB694D21D1E344D5BFE30F1"],
        ["BloomCombine"] = ["02F63272849123591A817848B03A2E010FB9FFEDFB6FCC9B2EC2AC74218822F7"],
        ["GaussianBlur"] = ["F96CD21F0FC6F5D41D0655323DAFF082D148122C5D21D467177E3F3BCBE45C06"]
    };
    public sealed record Result(string Asset, string Status, string Detail);
    public sealed record InstalledEffect(string Name, string Source, string Asset, string TemplateHash, string InstalledHash);
    public sealed record Manifest(int Schema, List<InstalledEffect> Effects);
    sealed record Plan(string Name, string Asset, string Source, byte[] Template);

    public static async Task<IReadOnlyList<Result>> RepairAsync(string projectPath, Action<string> log,
        CancellationToken token, string? compilerPath = null)
    {
        projectPath = Path.GetFullPath(projectPath);
        string root = Path.GetDirectoryName(projectPath)!;
        string content = Path.Combine(root, "Content");
        var results = new List<Result>();
        if (!Directory.Exists(content)) return results;
        string manifestPath = Path.Combine(root, "build", "KnownShaders.json");
        string scriptPath = Path.Combine(root, "build", "BuildKnownShaders.ps1");
        var manifest = File.Exists(manifestPath)
            ? JsonSerializer.Deserialize<Manifest>(File.ReadAllText(manifestPath)) ?? throw new InvalidDataException("Empty shader manifest.")
            : new Manifest(1, []);
        if (manifest.Schema != 1) throw new InvalidDataException("Unsupported shader manifest version.");
        foreach (var entry in manifest.Effects) { ProjectPath(root, entry.Source); ProjectPath(root, entry.Asset); }
        var project = XDocument.Load(projectPath, LoadOptions.PreserveWhitespace);
        var ns = project.Root!.Name.Namespace;
        var target = project.Root.Elements(ns + "Target").SingleOrDefault(e => (string?)e.Attribute("Name") == TargetName);
        byte[] script = Resource("BuildKnownShaders.ps1");
        bool hookReady = target?.Attribute("Label")?.Value == Owner && File.Exists(scriptPath) &&
            File.ReadAllBytes(scriptPath).AsSpan().SequenceEqual(script);
        var plans = new List<Plan>();
        var options = new EnumerationOptions { RecurseSubdirectories = true, AttributesToSkip = FileAttributes.ReparsePoint, IgnoreInaccessible = true };
        foreach (string asset in Directory.EnumerateFiles(content, "*.xnb", options).Order(StringComparer.OrdinalIgnoreCase))
        {
            token.ThrowIfCancellationRequested();
            string name = Path.GetFileNameWithoutExtension(asset);
            Fingerprints.TryGetValue(name, out var hashes);
            if (hashes == null && !RecoveredShaderTemplates.All.Values.Any(t => t.AssetName.Equals(name, StringComparison.OrdinalIgnoreCase))) continue;
            string relative = Relative(root, asset);
            string hash = Hash(File.ReadAllBytes(asset));
            var previous = manifest.Effects.SingleOrDefault(e => e.Asset.Equals(relative, StringComparison.OrdinalIgnoreCase));
            string? canonical = hashes?.Contains(hash, StringComparer.OrdinalIgnoreCase) == true
                ? Fingerprints.Keys.Single(k => k.Equals(name, StringComparison.OrdinalIgnoreCase)) : null;
            if (RecoveredShaderTemplates.All.TryGetValue(hash + "|" + name, out var recovered)) canonical = recovered.ResourceName;
            if (canonical == null && previous != null)
            {
                if (Fingerprints.ContainsKey(previous.Name) && previous.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) canonical = previous.Name;
                else if (RecoveredShaderTemplates.All.Values.Any(t => t.AssetName.Equals(name, StringComparison.OrdinalIgnoreCase) && t.ResourceName == previous.Name)) canonical = previous.Name;
            }
            if (canonical == null)
            {
                results.Add(new(relative, "Unsupported", "Unrecognized shader fingerprint; left untouched."));
                continue;
            }
            byte[] template = Resource(canonical + ".fx");
            string source = Path.ChangeExtension(asset, ".fx");
            if (File.Exists(source) && !File.ReadAllBytes(source).AsSpan().SequenceEqual(template) &&
                !(previous != null && Hash(File.ReadAllBytes(source)) == previous.TemplateHash))
            {
                results.Add(new(relative, "Conflict", "Existing custom HLSL source was preserved."));
                continue;
            }
            bool bufferFix = canonical == "RoundLine" && BufferPatch(root) != null;
            if (previous?.InstalledHash == hash && previous.TemplateHash == Hash(template) &&
                File.Exists(source) && hookReady && !bufferFix)
            {
                results.Add(new(relative, "UpToDate", "Windows shader and rebuild hook are current."));
                continue;
            }
            plans.Add(new(canonical, asset, source, template));
        }
        if (plans.Count == 0) { Report(results, log); return results; }
        if (target != null && target.Attribute("Label")?.Value != Owner)
            throw new InvalidDataException("A custom BuildKnownShaders target already exists; it was preserved.");
        compilerPath ??= FindCompiler();
        if (!File.Exists(compilerPath)) throw new FileNotFoundException("Install the Windows SDK (fxc.exe) to repair known shaders.", compilerPath);
        string staging = Path.Combine(root, "obj", "KnownShaderRepair", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(staging);
        var writes = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
        var installed = manifest.Effects.ToList();
        foreach (var plan in plans)
        {
            token.ThrowIfCancellationRequested();
            string hlsl = Path.Combine(staging, plan.Name + ".fx");
            string fxb = Path.Combine(staging, plan.Name + ".fxb");
            File.WriteAllBytes(hlsl, plan.Template);
            int code = await ProcessRunner.RunAsync(compilerPath, ["/nologo", "/T", "fx_2_0", "/O3", "/Fo", fxb, hlsl], staging, log, token);
            if (code != 0 || !File.Exists(fxb)) throw new InvalidDataException($"Shader compilation failed: {plan.Name} (exit {code}). No game files were changed.");
            byte[] xnb = PackEffect(File.ReadAllBytes(fxb));
            writes[plan.Source] = plan.Template;
            writes[plan.Asset] = xnb;
            string relative = Relative(root, plan.Asset);
            installed.RemoveAll(e => e.Asset.Equals(relative, StringComparison.OrdinalIgnoreCase));
            installed.Add(new(plan.Name, Relative(root, plan.Source), relative, Hash(plan.Template), Hash(xnb)));
            if (plan.Name == "RoundLine" && BufferPatch(root) is { } patch) writes[patch.Path] = patch.Bytes;
        }
        if (plans.Any(p => p.Name == "RoundLine"))
        {
            foreach (var old in project.Root.Elements(ns + "Target").Where(e => (string?)e.Attribute("Name") == "BuildRoundLineShader").ToList())
            {
                if (!old.ToString().Contains("BuildRoundLine.ps1", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidDataException("An unfamiliar RoundLine build target was preserved. No game files were changed.");
                old.Remove();
            }
        }
        installed = installed.OrderBy(e => e.Asset, StringComparer.OrdinalIgnoreCase).ToList();
        target?.Remove();
        string scriptCommand = "powershell.exe -NoProfile -NonInteractive -ExecutionPolicy Bypass -File \"$(MSBuildProjectDirectory)\\build\\BuildKnownShaders.ps1\" -ProjectRoot \"$(MSBuildProjectDirectory)\" -IntermediateDirectory \"$(MSBuildProjectDirectory)\\$(IntermediateOutputPath)KnownShaders\"";
        string Macro(string path) => "$(MSBuildProjectDirectory)\\" + path.Replace('/', '\\');
        project.Root.Add(new XElement(ns + "Target", new XAttribute("Name", TargetName), new XAttribute("Label", Owner),
            new XAttribute("BeforeTargets", "BeforeBuild"), new XAttribute("Condition", "'$(DesignTimeBuild)' != 'true'"),
            new XAttribute("Inputs", string.Join(";", installed.Select(e => Macro(e.Source)).Append(Macro("build/KnownShaders.json")).Append(Macro("build/BuildKnownShaders.ps1")))),
            new XAttribute("Outputs", string.Join(";", installed.Select(e => Macro(e.Asset)))),
            new XElement(ns + "Exec", new XAttribute("Command", scriptCommand))));
        foreach (var group in project.Root.Elements(ns + "ItemGroup").Where(e => (string?)e.Attribute("Label") == Owner).ToList()) group.Remove();
        project.Root.Add(new XElement(ns + "ItemGroup", new XAttribute("Label", Owner),
            new XElement(ns + "Compile", new XAttribute("Remove", "shader-repair-backups\\**\\*.cs")),
            new XElement(ns + "EmbeddedResource", new XAttribute("Remove", "shader-repair-backups\\**\\*")),
            new XElement(ns + "None", new XAttribute("Remove", "shader-repair-backups\\**\\*"))));
        using (var stream = new MemoryStream()) { project.Save(stream, SaveOptions.DisableFormatting); writes[projectPath] = stream.ToArray(); }
        writes[manifestPath] = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new Manifest(1, installed), JsonOptions));
        writes[scriptPath] = script;
        token.ThrowIfCancellationRequested();
        Commit(root, writes, log);
        results.AddRange(plans.Select(p => new Result(Relative(root, p.Asset), "Repaired", "Installed Windows shader, source, and incremental rebuild hook.")));
        Report(results, log);
        return results;
    }

    static (string Path, byte[] Bytes)? BufferPatch(string root)
    {
        string path = Path.Combine(root, "Maximinus", "RoundLineManager.cs");
        if (!File.Exists(path)) return null;
        var encoding = Encoding.Latin1;
        byte[] bytes = File.ReadAllBytes(path);
        string original = encoding.GetString(bytes);
        if (!original.Contains("class RoundLineManager") || !original.Contains("instanceData"))
            throw new InvalidDataException("RoundLine renderer source is unfamiliar; no source changes were made.");
        string updated = original.Replace("numVertices * bytesPerVertex, BufferUsage.None", "numVertices, BufferUsage.None", StringComparison.Ordinal)
            .Replace("numIndices * 2, BufferUsage.None", "numIndices, BufferUsage.None", StringComparison.Ordinal);
        return original == updated ? null : (path, encoding.GetBytes(updated));
    }
    static byte[] Resource(string name)
    {
        var assembly = typeof(KnownShaderRepair).Assembly;
        string resource = assembly.GetManifestResourceNames().Single(n => n.EndsWith(".KnownShaders." + name, StringComparison.Ordinal));
        using var stream = assembly.GetManifestResourceStream(resource)!;
        using var output = new MemoryStream(); stream.CopyTo(output); return output.ToArray();
    }
    static string Hash(byte[] bytes) => Convert.ToHexString(SHA256.HashData(bytes));
    static string Relative(string root, string path) => Path.GetRelativePath(root, path).Replace('\\', '/');
    static string ProjectPath(string root, string relative)
    {
        if (Path.IsPathRooted(relative)) throw new InvalidDataException("Manifest paths must be relative.");
        string path = Path.GetFullPath(Path.Combine(root, relative));
        if (!path.StartsWith(Path.TrimEndingDirectorySeparator(root) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("Shader manifest path escapes the game project.");
        return path;
    }
    public static string FindCompiler()
    {
        string sdk = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Windows Kits", "10", "bin");
        return Directory.Exists(sdk) ? Directory.EnumerateDirectories(sdk)
            .Select(p => new { Path = p, Version = Version.TryParse(Path.GetFileName(p), out var version) ? version : new Version() })
            .OrderByDescending(p => p.Version).Select(p => Path.Combine(p.Path, "x64", "fxc.exe")).FirstOrDefault(File.Exists) ?? "" : "";
    }
    public static byte[] PackEffect(byte[] effect)
    {
        using var payload = new MemoryStream();
        using (var writer = new BinaryWriter(payload, Encoding.UTF8, true))
        {
            writer.Write((byte)1); writer.Write("Microsoft.Xna.Framework.Content.EffectReader, FNA"); writer.Write(0);
            writer.Write((byte)0); writer.Write((byte)1); writer.Write(effect.Length); writer.Write(effect);
        }
        using var output = new MemoryStream();
        using (var writer = new BinaryWriter(output, Encoding.UTF8, true))
        {
            writer.Write("XNBw"u8); writer.Write((byte)5); writer.Write((byte)0); writer.Write(checked((int)payload.Length + 10)); writer.Write(payload.ToArray());
        }
        return output.ToArray();
    }
    static void Commit(string root, Dictionary<string, byte[]> writes, Action<string> log)
    {
        var changed = writes.Where(p => !File.Exists(p.Key) || !File.ReadAllBytes(p.Key).AsSpan().SequenceEqual(p.Value)).ToList();
        string backup = Path.Combine(root, "shader-repair-backups", DateTime.UtcNow.ToString("yyyyMMdd-HHmmss") + "-" + Guid.NewGuid().ToString("N"));
        var before = changed.ToDictionary(p => p.Key, p => File.Exists(p.Key) ? File.ReadAllBytes(p.Key) : null);
        foreach (var item in before.Where(p => p.Value != null))
        {
            string path = Path.Combine(backup, Relative(root, item.Key));
            Directory.CreateDirectory(Path.GetDirectoryName(path)!); File.WriteAllBytes(path, item.Value!);
        }
        Directory.CreateDirectory(backup);
        File.WriteAllText(Path.Combine(backup, "changes.json"), JsonSerializer.Serialize(before.Select(p => new { Path = Relative(root, p.Key), Created = p.Value == null }), JsonOptions));
        var written = new List<string>();
        try
        {
            foreach (var item in changed) { Directory.CreateDirectory(Path.GetDirectoryName(item.Key)!); AtomicWrite(item.Key, item.Value); written.Add(item.Key); }
        }
        catch
        {
            foreach (string path in written.AsEnumerable().Reverse()) { if (before[path] is { } bytes) AtomicWrite(path, bytes); else File.Delete(path); }
            throw;
        }
        log("Shader/source backups: " + backup);
    }
    static void AtomicWrite(string path, byte[] bytes)
    {
        string temporary = path + ".knownshader-" + Guid.NewGuid().ToString("N") + ".tmp";
        try { File.WriteAllBytes(temporary, bytes); File.Move(temporary, path, overwrite: true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
    static void Report(IEnumerable<Result> results, Action<string> log)
    {
        foreach (var result in results) log($"{result.Status}: {result.Asset} — {result.Detail}");
    }
}
