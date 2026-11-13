using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
namespace IndieGames.BuildManager.Services;

public sealed class LibraryStorage
{
    public string Archive { get; set; } = @"D:\indie-game-archive";
    public string Games { get; set; } = @"D:\indie-games";
    public string DecompPorts { get; set; } = @"D:\indie-decomp-ports";
    public string BuildCache { get; set; } = @"D:\indie-build-cache";
    public string TorrentCache { get; set; } = @"D:\indie-torrent-cache";
    public string Solution { get; set; } = @"C:\source\Indie-Games-Core\Indie-Games.slnx";
    public string Scripts { get; set; } = @"C:\PycharmProjects\xenia-game-manager\src\scripts";
    public string ZxBoxRoot { get; set; } = @"C:\source\ZXSpectrum";
    public string LegacyFnaRuntime { get; set; } = "";
    public static string SettingsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IndieGames.BuildManager", "storage.json");
    public static LibraryStorage Load()
    {
        try { return JsonSerializer.Deserialize<LibraryStorage>(File.ReadAllText(SettingsPath)) ?? new(); }
        catch { return new(); }
    }
    public void Validate()
    {
        var values = new[] { Archive, Games, DecompPorts, BuildCache, TorrentCache };
        if (values.Any(x => !Path.IsPathFullyQualified(x))) throw new IOException("Use absolute storage folder paths.");
        var roots = values.Select(x => Path.TrimEndingDirectorySeparator(Path.GetFullPath(x))).ToArray();
        foreach (var root in roots)
            if (root == Path.GetPathRoot(root) || !Path.IsPathFullyQualified(root)) throw new IOException("Choose a dedicated folder, not a drive root.");
        for (int i = 0; i < roots.Length; i++) for (int j = 0; j < roots.Length; j++)
            if (i != j && (roots[i].Equals(roots[j], StringComparison.OrdinalIgnoreCase) || roots[i].StartsWith(roots[j] + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)))
                throw new IOException("Source, games, decomp ports, build cache and torrent cache must be separate folders, outside one another.");
    }
    public void Save()
    {
        Validate(); Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
        string temp = SettingsPath + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true })); File.Move(temp, SettingsPath, true);
    }
    public string GameFolder(string project) => Path.Combine(Games, ProjectKey(project));
    // One validated package for the XNA Python launcher, converter and manifest.
    // Never combine files from different package versions.
    public static string? FindRepairRoot()
    {
        var candidates = new List<string>();
        for (DirectoryInfo? dir = new DirectoryInfo(AppContext.BaseDirectory); dir != null; dir = dir.Parent)
        {
            candidates.Add(Path.Combine(dir.FullName, "tools", "XnaRepair"));
            candidates.Add(Path.Combine(dir.FullName, "IndieGames.BuildManager", "tools", "XnaRepair"));
            candidates.Add(Path.Combine(dir.FullName, "scripts"));
        }
        candidates.Add(@"C:\source\Indie-Games-Core\IndieGames.BuildManager\tools\XnaRepair");
        candidates.Add(@"C:\source\Indie-Games-Core\tools\XnaRepair");
        foreach (string candidate in candidates.Distinct(StringComparer.OrdinalIgnoreCase))
            if (File.Exists(Path.Combine(candidate, "repair_manifest.json")) &&
                File.Exists(Path.Combine(candidate, "run_repair.py")) &&
                File.Exists(Path.Combine(candidate, "repair_xna3.py")))
                return candidate;
        return null;
    }

    public static string RequireRepairRoot()
    {
        return FindRepairRoot() ?? throw new FileNotFoundException(
            "XNA repair package incomplete. Keep repair_manifest.json, run_repair.py and repair_xna3.py " +
            "together under IndieGames.BuildManager\\tools\\XnaRepair (or the published scripts folder).");
    }

    public string ScriptPath(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || Path.GetFileName(name) != name)
            throw new ArgumentException("Use a script filename.", nameof(name));

        // These files must come from the same validated repair package.
        if (name is "repair_manifest.json" or "run_repair.py" or "repair_xna3.py")
            return Path.Combine(RequireRepairRoot(), name);

        // Other scripts can remain in their existing configured/bundled locations.
        string? repairRoot = FindRepairRoot();
        if (repairRoot is not null)
        {
            string candidate = Path.Combine(repairRoot, name);
            if (File.Exists(candidate)) return candidate;
        }
        string bundled = Path.Combine(AppContext.BaseDirectory, "scripts", name);
        if (File.Exists(bundled)) return bundled;
        if (!string.IsNullOrWhiteSpace(Scripts))
        {
            string legacy = Path.Combine(Scripts, name);
            if (File.Exists(legacy)) return legacy;
        }
        throw new FileNotFoundException($"Script '{name}' not found in the repair package, bundled scripts or configured scripts directory.", name);
    }
    public static string ProjectKey(string project) => Path.GetFileNameWithoutExtension(project) + "-" + Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Path.GetFullPath(project).ToUpperInvariant())))[..10];
    public string SdkWorkingDirectory(string project)
    {
        string projectDirectory = Path.GetDirectoryName(Path.GetFullPath(project))!;
        for (var folder = new DirectoryInfo(projectDirectory); folder is not null; folder = folder.Parent)
            if (File.Exists(Path.Combine(folder.FullName, "global.json"))) return projectDirectory;

        string? solutionDirectory = Path.GetDirectoryName(Solution);
        return !string.IsNullOrWhiteSpace(solutionDirectory) &&
            File.Exists(Path.Combine(solutionDirectory, "global.json"))
            ? solutionDirectory : projectDirectory;
    }

    public IEnumerable<string> BuildArguments(string project)
    {
        Validate();
        string BundledLayout(string name) { string path=Path.Combine(AppContext.BaseDirectory,"scripts",name);return File.Exists(path)?path:ScriptPath(name); }
        yield return "-p:CustomAfterDirectoryBuildProps=" + BundledLayout("BuildManager.Layout.props");
        yield return "-p:CustomBeforeMicrosoftCommonTargets=" + BundledLayout("BuildManager.Layout.targets");
        yield return "-p:BuildManagerCache=" + BuildCache;
        yield return "-p:BuildManagerMainProject=" + Path.GetFullPath(project);
        yield return "-p:BuildManagerGameOutput=" + GameFolder(project);
    }
}
