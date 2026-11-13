using System;
using System.IO;
using System.Linq;
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
            "x86" => "x86",
            "x64" => "x64",
            "anycpu" => "AnyCPU",
            _ => throw new ArgumentException("Choose x86, x64 or AnyCPU.", nameof(architecture))
        };

        var document = XDocument.Load(projectPath, LoadOptions.PreserveWhitespace);
        var root = document.Root ?? throw new InvalidDataException("Project has no root element.");
        XNamespace ns = root.Name.Namespace;

        // Do not remove or rewrite the project's own conditional PropertyGroups.
        // Only replace groups previously created by this updater.
        foreach (var old in root.Elements(ns + "PropertyGroup")
                     .Where(g => (string?)g.Attribute("Label") == Label).ToList())
            old.Remove();

        string Read(string name) => root.Elements(ns + "PropertyGroup")
            .Where(g => g.Attribute("Condition") == null)
            .Elements(ns + name).LastOrDefault()?.Value.Trim() ?? "";

        var platforms = Read("Platforms")
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        foreach (string value in new[] { "x86", "x64", "AnyCPU" })
            if (!platforms.Contains(value, StringComparer.OrdinalIgnoreCase)) platforms.Add(value);

        string existingRid = Read("RuntimeIdentifier");
        string Rid(string arch)
        {
            if (arch == "AnyCPU") return "";
            // Keep an existing Windows RID prefix, e.g. win10-x64 -> win10-x86.
            int dash = existingRid.LastIndexOf('-');
            string prefix = dash > 0 && existingRid.StartsWith("win", StringComparison.OrdinalIgnoreCase)
                ? existingRid[..(dash + 1)] : "win-";
            return prefix + arch;
        }

        // Never set $(Platform) here: it belongs to Visual Studio's solution mapping
        // or to Build Manager's explicit -p:Platform=... command-line argument.
        // Conditional groups ensure PlatformTarget and RID cannot disagree.
        foreach (string value in new[] { "x86", "x64", "AnyCPU" })
        {
            var group = new XElement(ns + "PropertyGroup",
                new XAttribute("Label", Label),
                new XAttribute("Condition", $"'$(Platform)' == '{value}'"),
                new XElement(ns + "PlatformTarget", value),
                new XElement(ns + "Prefer32Bit", "false"),
                new XElement(ns + "RuntimeIdentifier", Rid(value)));
            root.Add(group);
        }

        // Default when a project is built without an explicit Platform. Do not
        // force the Platform itself, and do not override explicit MSBuild -p values.
        root.Add(new XElement(ns + "PropertyGroup",
            new XAttribute("Label", Label),
            new XAttribute("Condition", "'$(Platform)' == ''"),
            new XElement(ns + "PlatformTarget", architecture),
            new XElement(ns + "Prefer32Bit", "false"),
            new XElement(ns + "RuntimeIdentifier", Rid(architecture))));

        // A fresh backup every time, rather than a single stale backup.
        string backup = projectPath + ".before-architecture-" +
            DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fffffff") + ".bak";
        File.Copy(projectPath, backup);
        string temporary = projectPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            document.Save(temporary, SaveOptions.DisableFormatting);
            File.Move(temporary, projectPath, overwrite: true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }

        return new ProjectArchitectureSettings(architecture, string.Join(';', platforms), Rid(architecture));
    }
}
