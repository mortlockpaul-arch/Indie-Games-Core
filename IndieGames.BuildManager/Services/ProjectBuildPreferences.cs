using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

public sealed record ProjectBuildPreferences(string Framework, string Network, string MonoPlatform = "Project default", string MonoVersion = "3.8.1.303")
{
    static string FilePath(string project) => Path.Combine(Path.GetDirectoryName(LibraryStorage.SettingsPath)!,
        "project-build-preferences", Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Path.GetFullPath(project).ToUpperInvariant()))) + ".json");

    public static ProjectBuildPreferences Load(string project)
    {
        string file = FilePath(project);
        if (File.Exists(file))
            return JsonSerializer.Deserialize<ProjectBuildPreferences>(File.ReadAllText(file))
                ?? throw new InvalidDataException("Empty saved build preferences.");
        var capability = GameBuildProfiles.Inspect(project);
        if (!capability.ManagedSource || capability.Framework is not ("FNA" or "MonoGame"))
            return new("Project default", "Project default");
        var references = XDocument.Load(project).Descendants()
            .Where(e => e.Name.LocalName is "ProjectReference" or "Reference" or "PackageReference")
            .Select(e => (string?)e.Attribute("Include") ?? "").ToArray();
        string network = references.Any(r => r.Contains("Steamworks", StringComparison.OrdinalIgnoreCase)) ? "Steamworks" :
            capability.Framework == "FNA" || references.Any(r => r.Contains("NetStub", StringComparison.OrdinalIgnoreCase) ||
                r.Contains("Monogame.Contents", StringComparison.OrdinalIgnoreCase)) ? "NetStub" : "None";
        return new(capability.Framework, network);
    }

    public void Save(string project)
    {
        string file = FilePath(project);
        Directory.CreateDirectory(Path.GetDirectoryName(file)!);
        string temporary = file + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporary, JsonSerializer.Serialize(this));
            File.Move(temporary, file, true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}
