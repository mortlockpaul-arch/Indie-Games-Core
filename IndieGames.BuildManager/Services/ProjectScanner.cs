using System.IO;
using System.Xml.Linq;
using IndieGames.BuildManager.Models;
namespace IndieGames.BuildManager.Services;
public static class ProjectScanner
{
    public static List<GameProject> Scan(string root)
    {
        if (!Directory.Exists(root)) return [];
        var result = new List<GameProject>();
        foreach (var file in Directory.EnumerateFiles(root, "*.csproj", SearchOption.AllDirectories))
        {
            if (file.Split(System.IO.Path.DirectorySeparatorChar).Any(p => p is "obj" or "bin" or ".git"))
                continue;
            try
            {
                var doc = XDocument.Load(file);
                string Read(string name) => string.Join("; ", doc.Descendants()
                    .Where(e => e.Name.LocalName == name)
                    .Select(e => e.Value.Trim()).Where(v => v.Length > 0).Distinct());
                result.Add(new GameProject {
                    Name = System.IO.Path.GetFileNameWithoutExtension(file),
                    Path = file, TargetFramework = Read("TargetFrameworks") is { Length: > 0 } multi ? multi : Read("TargetFramework"),
                    PlatformTarget = Read("PlatformTarget"), Platforms = Read("Platforms"),
                    RuntimeIdentifier = Read("RuntimeIdentifiers") is { Length: > 0 } ris ? ris : Read("RuntimeIdentifier"),
                    OutputType = Read("OutputType")
                });
            }
            catch { /* malformed projects can be diagnosed separately */ }
        }
        return result.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
    }
}
