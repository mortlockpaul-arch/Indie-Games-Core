using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

public sealed record PortableGameManifest
{
    public int Version { get; init; } = 1;
    public string Name { get; init; } = "";
    public string EntryPoint { get; init; } = "";
    public string Kind { get; init; } = "Port";
    public string SourceSha256 { get; init; } = "";
    public Dictionary<string, string> Environment { get; init; } = new();
}
public sealed record PortableGame(string Folder, string Name, string EntryPoint, string Kind, string Status)
{
    public string DisplayKind => System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Kind);
    public string? ExecutableVersion { get; init; }
    public string Executable => string.IsNullOrEmpty(EntryPoint) ? "" : Path.Combine(Folder, EntryPoint);
}

public static class PortableGames
{
    public const string ManifestName = ".buildmanager-game.json";
    static readonly JsonSerializerOptions Json = new() { WriteIndented = true, PropertyNameCaseInsensitive = true };
    static readonly HashSet<string> Helpers = new(StringComparer.OrdinalIgnoreCase)
        { "createdump.exe", "uninstall.exe", "unins000.exe", "setup.exe", "dxsetup.exe", "vcredist_x64.exe", "vcredist_x86.exe", "XnaToFna.exe", "MonoMod.exe" };
    public static bool IsCandidate(string path) => path.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && !Helpers.Contains(Path.GetFileName(path));
    public static bool IsEntry(string path, IEnumerable<string> files)
    {
        if (!IsCandidate(path)) return false;
        bool runtime = files.Contains(Path.ChangeExtension(path, ".runtimeconfig.json"), StringComparer.OrdinalIgnoreCase);
        bool deps = files.Contains(Path.ChangeExtension(path, ".deps.json"), StringComparer.OrdinalIgnoreCase);
        return runtime == deps; // Both sidecars for modern builds, neither for native/standalone ports.
    }
    public static string ResolveRelative(string root, string relative)
    {
        if (string.IsNullOrWhiteSpace(relative) || Path.IsPathRooted(relative)) throw new InvalidDataException("Choose a relative executable inside the game folder.");
        foreach (var part in relative.Replace('\\', '/').Split('/')) ProjectBundle.ValidateSegment(part);
        string folder = Path.TrimEndingDirectorySeparator(Path.GetFullPath(root));
        string full = Path.GetFullPath(Path.Combine(folder, relative.Replace('/', Path.DirectorySeparatorChar)));
        if (!full.StartsWith(folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Executable is outside the game folder.");
        return full;
    }
    public static PortableGameManifest? ReadManifest(string folder)
    {
        string file = Path.Combine(folder, ManifestName);
        if (!File.Exists(file)) return null;
        if (new FileInfo(file).Length > 65536) throw new InvalidDataException("Game launch manifest is too large.");
        var data = JsonSerializer.Deserialize<PortableGameManifest>(File.ReadAllText(file), Json) ?? throw new InvalidDataException("Invalid game launch manifest.");
        if (data.Version != 1 || string.IsNullOrWhiteSpace(data.Name)) throw new InvalidDataException("Invalid game launch manifest version or name.");
        ProjectBundle.ValidateSegment(data.Name);
        if (data.EntryPoint.Length > 0)
        {
            ResolveRelative(folder, data.EntryPoint);
            if (!data.EntryPoint.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Launch entry must be a Windows executable.");
        }
        if (data.Environment == null || data.Environment.Count > 100 || data.Environment.Any(p => p.Key.Length is < 1 or > 100 || p.Key.Contains('=') || p.Key.Contains('\0') || p.Value == null || p.Value.Length > 4096 || p.Value.Contains('\0')))
            throw new InvalidDataException("Invalid game launch environment.");
        return data;
    }
    public static void WriteManifest(string folder, PortableGameManifest manifest)
    {
        string path = Path.Combine(folder, ManifestName), temp = path + "." + Guid.NewGuid().ToString("N");
        try { File.WriteAllText(temp, JsonSerializer.Serialize(manifest, Json)); File.Move(temp, path, true); }
        finally { if (File.Exists(temp)) File.Delete(temp); }
        _ = ReadManifest(folder);
    }
    public static IReadOnlyList<string> Executables(string folder)
    {
        var result = new List<string>();
        void Walk(string directory)
        {
            if ((File.GetAttributes(directory) & FileAttributes.ReparsePoint) != 0) return;
            foreach (var file in Directory.EnumerateFiles(directory, "*.exe"))
                if (IsCandidate(file) && (File.GetAttributes(file) & FileAttributes.ReparsePoint) == 0) result.Add(Path.GetRelativePath(folder, file));
            foreach (var child in Directory.EnumerateDirectories(directory))
                if (!Path.GetFileName(child).StartsWith('.') && Path.GetFileName(child) is not ("obj" or "logs")) Walk(child);
        }
        Walk(folder);
        return result.OrderBy(p => p, StringComparer.OrdinalIgnoreCase).ToArray();
    }
    static string GuessEntry(string folder, IReadOnlyList<string> candidates)
    {
        var modern = candidates.Where(p => File.Exists(Path.Combine(folder, Path.ChangeExtension(p, ".runtimeconfig.json"))) && File.Exists(Path.Combine(folder, Path.ChangeExtension(p, ".deps.json")))).ToArray();
        if (modern.Length == 1) return modern[0];
        return candidates.Count == 1 ? candidates[0] : "";
    }
    public static PortableGame Inspect(string folder)
    {
        var manifest = ReadManifest(folder);
        var candidates = Executables(folder);
        string entry = manifest?.EntryPoint ?? GuessEntry(folder, candidates);
        string status = entry.Length == 0 ? "Choose executable" : File.Exists(ResolveRelative(folder, entry)) ? "Ready to run" : "Executable missing";
        var game = new PortableGame(folder, manifest?.Name ?? Path.GetFileName(folder), entry, manifest?.Kind ?? "Built game", status);
        return status == "Ready to run" ? game with { ExecutableVersion = ReadExecutableVersion(game.Executable) } : game;
    }

    static string? ReadExecutableVersion(string executable)
    {
        try
        {
            var version = FileVersionInfo.GetVersionInfo(executable);
            return string.IsNullOrWhiteSpace(version.FileVersion) ? version.ProductVersion?.Trim() : version.FileVersion.Trim();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.ComponentModel.Win32Exception)
        {
            return null;
        }
    }
    public static IReadOnlyList<PortableGame> Scan(string gamesRoot)
    {
        if (!Directory.Exists(gamesRoot)) return [];
        var result = new List<PortableGame>();
        foreach (var folder in Directory.EnumerateDirectories(gamesRoot))
        {
            if (Path.GetFileName(folder).StartsWith('.') || (File.GetAttributes(folder) & FileAttributes.ReparsePoint) != 0) continue;
            try { if (Executables(folder).Count > 0 || File.Exists(Path.Combine(folder, ManifestName))) result.Add(Inspect(folder)); }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException or ArgumentException) { result.Add(new(folder, Path.GetFileName(folder), "", "Port", ex.Message)); }
        }
        return result.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToArray();
    }
    public static string ResolveExecutable(string folder)
    {
        var game = Inspect(folder);
        if (game.Status != "Ready to run") throw new IOException(game.Status + ". Use Playable Games / Ports → Choose executable.");
        string path = ResolveRelative(folder, game.EntryPoint);
        for (string current = path; ; current = Path.GetDirectoryName(current)!)
        {
            if ((File.GetAttributes(current) & FileAttributes.ReparsePoint) != 0) throw new IOException("The game launch path is a link. Choose the actual game folder.");
            if (current.Equals(Path.GetFullPath(folder).TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase)) break;
        }
        return path;
    }
    public static IReadOnlyDictionary<string, string> LaunchEnvironment(string folder)
        => (ReadManifest(folder)?.Environment ?? new()).ToDictionary(p => p.Key, p => p.Value.Replace("{GameFolder}", Path.GetFullPath(folder)), StringComparer.OrdinalIgnoreCase);
    public static void SetExecutable(string folder, string relative)
    {
        string path = ResolveRelative(folder, relative);
        if (!path.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) || !File.Exists(path)) throw new IOException("Choose an executable in this game folder.");
        var old = ReadManifest(folder) ?? new PortableGameManifest { Name = Path.GetFileName(folder), Kind = "Playable game" };
        WriteManifest(folder, old with { EntryPoint = relative });
    }
    static PortableGameManifest InitialManifest(string folder, string name, string entry)
    {
        var environment = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
        // Preserve literal SDL/FNA launcher settings without executing or interpreting scripts.
        foreach (string script in Directory.EnumerateFiles(folder,"*.ps1"))
        {
            if (new FileInfo(script).Length > 65536) continue;
            foreach (System.Text.RegularExpressions.Match match in System.Text.RegularExpressions.Regex.Matches(File.ReadAllText(script), @"(?m)^\s*\$env:((?:SDL_|FNA_|ONYC_)[A-Z0-9_]+)\s*=\s*'([^'\r\n]*)'\s*$"))
                environment[match.Groups[1].Value] = match.Groups[2].Value;
        }
        if (File.Exists(Path.Combine(folder,"gamecontrollerdb.txt")))
            foreach (string key in new[]{"SDL_GAMECONTROLLERCONFIG_FILE","SDL_HINT_GAMECONTROLLERCONFIG_FILE","SDL_GAMEPADCONFIG_FILE","SDL_HINT_GAMEPADCONFIG_FILE"}) environment[key]="{GameFolder}"+Path.DirectorySeparatorChar+"gamecontrollerdb.txt";
        return new PortableGameManifest { Name=name, EntryPoint=entry, Environment=environment };
    }
    public static async Task<PortableGame> ImportZipAsync(string zipPath, string gamesRoot, string? name, Action<string> log, CancellationToken token)
    {
        string root = StorageRoot(gamesRoot);
        name ??= Path.GetFileNameWithoutExtension(zipPath).Replace(" - Zipped", "").Replace(" Complete", "").Replace("-Windows", "");
        ProjectBundle.ValidateSegment(name);
        string hash;
        await using (var stream = File.OpenRead(zipPath)) hash = Convert.ToHexString(await SHA256.HashDataAsync(stream, token));
        string destination = Path.Combine(root, name);
        if (Directory.Exists(destination))
        {
            var old = ReadManifest(destination);
            if (old?.SourceSha256 == hash) { log("Already imported: " + name); return Inspect(destination); }
            throw new IOException("A game folder already exists: " + destination + ". Existing files are kept; choose another title.");
        }
        using var archive = ZipFile.OpenRead(zipPath);
        if (archive.Entries.Count > 100000) throw new InvalidDataException("Too many archive entries.");
        var entries = new List<(ZipArchiveEntry Entry, string Relative)>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        long total = 0;
        foreach (var entry in archive.Entries)
        {
            token.ThrowIfCancellationRequested();
            string relative = entry.FullName.Replace('\\', '/').TrimEnd('/');
            foreach (string part in relative.Split('/')) ProjectBundle.ValidateSegment(part);
            if (!seen.Add(relative) || ((entry.ExternalAttributes >> 16) & 0xF000) == 0xA000 || (entry.ExternalAttributes & (int)FileAttributes.ReparsePoint) != 0) throw new InvalidDataException("Duplicate or linked archive entry.");
            if (entry.FullName.EndsWith('/') || entry.FullName.EndsWith('\\')) continue;
            total = checked(total + entry.Length);
            if (total > 100L * 1024 * 1024 * 1024) throw new InvalidDataException("Port is too large.");
            entries.Add((entry, relative));
        }
        if (entries.Count == 0) throw new InvalidDataException("Empty game archive.");
        var roots = entries.Select(p => p.Relative.Split('/')[0]).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
        string prefix = roots.Length == 1 && entries.All(p => p.Relative.Contains('/')) ? roots[0] + "/" : "";
        string stage = Path.Combine(root, ".port-import-" + Guid.NewGuid().ToString("N")); Directory.CreateDirectory(stage);
        try
        {
            foreach (var item in entries)
            {
                token.ThrowIfCancellationRequested();
                string relative = item.Relative[prefix.Length..];
                string path = ResolveRelative(stage, relative);
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                await using var input = item.Entry.Open(); await using var output = new FileStream(path, FileMode.CreateNew);
                await input.CopyToAsync(output, token);
                if (output.Length != item.Entry.Length) throw new InvalidDataException("Archive entry size mismatch.");
            }
            var candidates = Executables(stage);
            if (candidates.Count == 0) throw new InvalidDataException("The archive has no game executable.");
            var existing = ReadManifest(stage);
            WriteManifest(stage, (existing ?? InitialManifest(stage, name, GuessEntry(stage, candidates))) with { Name = name, Kind = "Port", SourceSha256 = hash });
            token.ThrowIfCancellationRequested(); Directory.Move(stage, destination);
            log("Imported port: " + destination); return Inspect(destination);
        }
        finally { CleanupStage(stage, root); }
    }
    public static async Task<PortableGame> ImportFolderAsync(string source, string gamesRoot, Action<string> log, CancellationToken token)
    {
        source = Path.TrimEndingDirectorySeparator(Path.GetFullPath(source)); string root = StorageRoot(gamesRoot);
        if (source.Equals(root, StringComparison.OrdinalIgnoreCase) || root.StartsWith(source + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new IOException("Choose a game folder outside the playable-games library.");
        if (Path.GetDirectoryName(source)!.Equals(root, StringComparison.OrdinalIgnoreCase)) return Inspect(source);
        string name = Path.GetFileName(source); ProjectBundle.ValidateSegment(name); string destination = Path.Combine(root, name);
        if (Directory.Exists(destination) || File.Exists(destination)) throw new IOException("This game folder already exists. Existing files are kept.");
        string stage = Path.Combine(root, ".port-import-" + Guid.NewGuid().ToString("N")); Directory.CreateDirectory(stage);
        try
        {
            async Task Copy(string dir)
            {
                token.ThrowIfCancellationRequested();
                if ((File.GetAttributes(dir) & FileAttributes.ReparsePoint) != 0) throw new IOException("Game folder contains a link.");
                foreach (var file in Directory.EnumerateFiles(dir))
                {
                    if ((File.GetAttributes(file) & FileAttributes.ReparsePoint) != 0) throw new IOException("Game folder contains a linked file.");
                    string path = ResolveRelative(stage, Path.GetRelativePath(source, file)); Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    await using var input = File.OpenRead(file); await using var output = File.Create(path); await input.CopyToAsync(output, token);
                }
                foreach (var child in Directory.EnumerateDirectories(dir)) await Copy(child);
            }
            await Copy(source);
            var candidates = Executables(stage); if (candidates.Count == 0) throw new InvalidDataException("No game executables in this folder.");
            var old = ReadManifest(stage);
            WriteManifest(stage, old ?? InitialManifest(stage, name, GuessEntry(stage, candidates)));
            token.ThrowIfCancellationRequested(); Directory.Move(stage, destination); log("Imported port: " + destination); return Inspect(destination);
        }
        finally { CleanupStage(stage, root); }
    }
    static string StorageRoot(string root)
    {
        root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(root));
        if (root.Equals(Path.GetPathRoot(root), StringComparison.OrdinalIgnoreCase)) throw new IOException("Choose a dedicated games folder, not a drive root.");
        Directory.CreateDirectory(root);
        if ((File.GetAttributes(root) & FileAttributes.ReparsePoint) != 0) throw new IOException("Choose the actual games folder, not a link.");
        return root;
    }
    static void CleanupStage(string stage, string root)
    {
        stage = Path.GetFullPath(stage); root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(root));
        if (Path.GetDirectoryName(stage)!.Equals(root, StringComparison.OrdinalIgnoreCase) && Path.GetFileName(stage).StartsWith(".port-import-", StringComparison.Ordinal) && Directory.Exists(stage)) Directory.Delete(stage, true);
    }
}
