using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
namespace IndieGames.BuildManager.Services;

public sealed class LibraryStorage
{
    public string Archive { get; set; } = @"D:\indie-game-archive";
    public string Games { get; set; } = @"D:\indie-games";
    public string BuildCache { get; set; } = @"D:\indie-build-cache";
    public string TorrentCache { get; set; } = @"D:\indie-torrent-cache";
    public static string SettingsPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IndieGames.BuildManager", "storage.json");
    public static LibraryStorage Load()
    {
        try { return JsonSerializer.Deserialize<LibraryStorage>(File.ReadAllText(SettingsPath)) ?? new(); }
        catch { return new(); }
    }
    public void Validate()
    {
        var values = new[] { Archive, Games, BuildCache, TorrentCache };
        if (values.Any(x => !Path.IsPathFullyQualified(x))) throw new IOException("Use absolute storage folder paths.");
        var roots = values.Select(x => Path.TrimEndingDirectorySeparator(Path.GetFullPath(x))).ToArray();
        foreach (var root in roots)
            if (root == Path.GetPathRoot(root) || !Path.IsPathFullyQualified(root)) throw new IOException("Choose a dedicated folder, not a drive root.");
        for (int i = 0; i < roots.Length; i++) for (int j = 0; j < roots.Length; j++)
            if (i != j && (roots[i].Equals(roots[j], StringComparison.OrdinalIgnoreCase) || roots[i].StartsWith(roots[j] + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)))
                throw new IOException("Source, games, build cache and torrent cache must be separate folders, outside one another.");
    }
    public void Save()
    {
        Validate(); Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
        string temp = SettingsPath + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true })); File.Move(temp, SettingsPath, true);
    }
    public string GameFolder(string project) => Path.Combine(Games, ProjectKey(project));
    public static string ProjectKey(string project) => Path.GetFileNameWithoutExtension(project) + "-" + Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Path.GetFullPath(project).ToUpperInvariant())))[..10];
    public IEnumerable<string> BuildArguments(string project)
    {
        Validate();
        yield return "-p:CustomAfterDirectoryBuildProps=" + Path.Combine(AppContext.BaseDirectory, "scripts", "BuildManager.Layout.props");
        yield return "-p:CustomBeforeMicrosoftCommonTargets=" + Path.Combine(AppContext.BaseDirectory, "scripts", "BuildManager.Layout.targets");
        yield return "-p:BuildManagerCache=" + BuildCache;
        yield return "-p:BuildManagerMainProject=" + Path.GetFullPath(project);
        yield return "-p:BuildManagerGameOutput=" + GameFolder(project);
    }
}
