using System.IO;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

public sealed record ProjectArchitectureSettings(string PlatformTarget, string Platforms, string RuntimeIdentifier);

public static class ProjectArchitectureUpdater
{
    public const string Label = "IndieGames.BuildManager.Architecture";

    public static ProjectArchitectureSettings Apply(string projectPath, string architecture)
    {
        architecture = architecture.Trim().ToLowerInvariant() switch
        {
            "x86" => "x86", "x64" => "x64", "anycpu" => "AnyCPU",
            _ => throw new ArgumentException("Choose x86, x64 or AnyCPU.", nameof(architecture))
        };
        var document = XDocument.Load(projectPath, LoadOptions.PreserveWhitespace);
        var root = document.Root ?? throw new InvalidDataException("Project has no root element.");
        XNamespace ns = root.Name.Namespace;
        string Read(string name) => root.Elements(ns + "PropertyGroup").Elements(ns + name)
            .LastOrDefault()?.Value.Trim() ?? "";
        var platforms = Read("Platforms").Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        if (!platforms.Contains(architecture, StringComparer.OrdinalIgnoreCase)) platforms.Add(architecture);
        string runtime = Read("RuntimeIdentifier");
        if (architecture != "AnyCPU" && runtime.Length > 0)
        {
            int separator = runtime.LastIndexOf('-');
            if (separator > 0 && runtime[(separator + 1)..] is "x86" or "x64" or "arm64" or "arm")
                runtime = runtime[..(separator + 1)] + architecture;
        }
        var group = root.Elements(ns + "PropertyGroup").FirstOrDefault(g => (string?)g.Attribute("Label") == Label);
        // Put saved choices after project-local conditions so rescans and ordinary
        // builds agree. Explicit MSBuild command-line overrides still take precedence.
        group?.Remove();
        group ??= new XElement(ns + "PropertyGroup", new XAttribute("Label", Label));
        void Set(string name, string value) => group.SetElementValue(ns + name, value);
        Set("Platform", architecture);
        Set("PlatformTarget", architecture);
        Set("Platforms", string.Join(';', platforms));
        Set("Prefer32Bit", "false");
        if (runtime.Length > 0) Set("RuntimeIdentifier", runtime);
        root.Add(new XText(Environment.NewLine + "  "), group, new XText(Environment.NewLine));
        string backup = projectPath + ".before-architecture.bak";
        if (!File.Exists(backup)) File.Copy(projectPath, backup);
        string temporary = projectPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            document.Save(temporary, SaveOptions.DisableFormatting);
            File.Move(temporary, projectPath, overwrite: true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
        return new(architecture, string.Join(';', platforms), runtime);
    }
}
