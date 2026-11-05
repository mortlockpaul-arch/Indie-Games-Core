using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using IndieGames.BuildManager.Models;
namespace IndieGames.BuildManager.Services;

public sealed record ProjectScanProgress(int FoldersScanned, int TotalFolders, int ProjectsFound, string Folder, bool Discovering);
public static class ProjectScanner
{
    public static List<GameProject> Scan(string root, CancellationToken token=default, IProgress<ProjectScanProgress>? progress=null, string? cacheDirectory=null)
    {
        if (!Directory.Exists(root)) return [];
        var rootDirectory = new DirectoryInfo(root);
        string? cachePath = cacheDirectory is null ? null : Path.Combine(cacheDirectory,
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rootDirectory.FullName.ToUpperInvariant()))) + ".json");
        var cached = ReadCache(cachePath);
        var current = new Dictionary<string, CachedProject>(StringComparer.OrdinalIgnoreCase);
        var pending = new Stack<DirectoryInfo>();
        var result = new List<GameProject>();
        pending.Push(rootDirectory);
        int foldersScanned = 0;
        long lastReport = 0;
        while (pending.TryPop(out var folder))
        {
            token.ThrowIfCancellationRequested();
            if ((folder.Attributes & FileAttributes.ReparsePoint) != 0) continue;
            foreach (var entry in folder.EnumerateFileSystemInfos())
            {
                token.ThrowIfCancellationRequested();
                if (entry is DirectoryInfo child)
                {
                    if (child.Name is not ("obj" or "bin" or ".git" or ".vs")) pending.Push(child);
                    continue;
                }
                if (entry is not FileInfo project || !project.Extension.Equals(".csproj", StringComparison.OrdinalIgnoreCase)) continue;
                string file = project.FullName;
                try
                {
                    var modified = project.LastWriteTimeUtc;
                    long length = project.Length;
                    if (!cached.TryGetValue(file, out var metadata) || metadata.Modified != modified || metadata.Length != length)
                        metadata = ReadProject(file, modified, length);
                    current[file] = metadata;
                    result.Add(new GameProject
                    {
                        Name = Path.GetFileNameWithoutExtension(file),
                        Path = file,
                        IconPath = FindIcon(file, metadata.Icons),
                        TargetFramework = metadata.TargetFramework,
                        PlatformTarget = metadata.PlatformTarget,
                        Platforms = metadata.Platforms,
                        RuntimeIdentifier = metadata.RuntimeIdentifier,
                        OutputType = metadata.OutputType
                    });
                }
                catch (System.Xml.XmlException) { }
            }
            foldersScanned++;
            long now = Environment.TickCount64;
            if (now - lastReport >= 100)
            {
                progress?.Report(new(foldersScanned, foldersScanned + pending.Count, result.Count, folder.FullName, true));
                lastReport = now;
            }
        }
        token.ThrowIfCancellationRequested();
        if (cachePath is not null && (current.Count != cached.Count ||
            current.Any(pair => !cached.TryGetValue(pair.Key, out var previous) || !ReferenceEquals(pair.Value, previous))))
            WriteCache(cachePath, current);
        progress?.Report(new(foldersScanned, foldersScanned, result.Count, rootDirectory.FullName, false));
        return result.OrderBy(p=>p.Name,StringComparer.OrdinalIgnoreCase).ToList();
    }

    private sealed record CachedProject(DateTime Modified, long Length, string TargetFramework,
        string PlatformTarget, string Platforms, string RuntimeIdentifier, string OutputType, string[] Icons);

    private static Dictionary<string, CachedProject> ReadCache(string? path)
    {
        if (path is not null && File.Exists(path))
        {
            try
            {
                var entries = JsonSerializer.Deserialize<Dictionary<string, CachedProject>>(File.ReadAllBytes(path));
                if (entries is not null) return new(entries, StringComparer.OrdinalIgnoreCase);
            }
            catch (Exception error) when (error is IOException or UnauthorizedAccessException or JsonException) { }
        }
        return new(StringComparer.OrdinalIgnoreCase);
    }

    private static void WriteCache(string path, Dictionary<string, CachedProject> entries)
    {
        string temporary = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllBytes(temporary, JsonSerializer.SerializeToUtf8Bytes(entries));
            File.Move(temporary, path, true);
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException) { }
        finally
        {
            try { File.Delete(temporary); }
            catch (Exception error) when (error is IOException or UnauthorizedAccessException) { }
        }
    }

    private static CachedProject ReadProject(string file, DateTime modified, long length)
    {
        var doc = XDocument.Load(file);
        var saved = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "PropertyGroup" &&
            (string?)e.Attribute("Label") == ProjectArchitectureUpdater.Label);
        string Read(string name) => saved?.Elements().FirstOrDefault(e => e.Name.LocalName == name)?.Value.Trim()
            ?? string.Join("; ", doc.Descendants().Where(e => e.Name.LocalName == name)
                .Select(e => e.Value.Trim()).Where(v => v.Length > 0).Distinct());
        var icons = doc.Descendants().Where(e => e.Name.LocalName == "ApplicationIcon").Select(e => e.Value.Trim()).ToArray();
        return new(modified, length,
            Read("TargetFrameworks") is { Length: > 0 } multi ? multi : Read("TargetFramework"),
            Read("PlatformTarget"), Read("Platforms"),
            Read("RuntimeIdentifiers") is { Length: > 0 } ris ? ris : Read("RuntimeIdentifier"),
            Read("OutputType"), icons);
    }

    private static string FindIcon(string project, string[] icons)
    {
        string folder = Path.GetDirectoryName(project)!;
        var candidates = icons.Select(value => value.Replace("$(MSBuildProjectDirectory)", folder))
            .Where(value => value.Length > 0 && !value.Contains("$(", StringComparison.Ordinal))
            .Concat(new[] { "DashboardIcon.ico", "app.ico", "Game.ico", "icon.ico" });
        foreach (string candidate in candidates)
        {
            try
            {
                string path = Path.GetFullPath(Path.IsPathRooted(candidate) ? candidate : Path.Combine(folder, candidate));
                if (File.Exists(path)) return path;
            }
            catch (Exception ex) when (ex is ArgumentException or NotSupportedException or PathTooLongException) { }
        }
        return "";
    }
}
