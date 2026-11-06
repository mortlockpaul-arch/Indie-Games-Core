using System.IO;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IndieGames.BuildManager.Services;

public static class ProjectVersionUpdater
{
    private const string Label = "IndieGames.BuildManager.Version";
    private const string FileVersion = "AssemblyFileVersion";
    private const string ProductVersion = "AssemblyInformationalVersion";

    public static Version Read(string projectPath)
    {
        var project = XDocument.Load(projectPath);
        if (UsesManualAttributes(project))
        {
            var source = ParseAssemblyInfo(AssemblyInfoPath(projectPath));
            return Parse(AttributeValue(source, FileVersion))
                ?? Parse(AttributeValue(source, "AssemblyVersion"))
                ?? new Version(1, 0, 0);
        }

        return Parse(Property(project, "FileVersion"))
            ?? Parse(Property(project, "Version"))
            ?? Parse(Property(project, "VersionPrefix"))
            ?? new Version(1, 0, 0);
    }

    public static void Apply(string projectPath, string versionText)
    {
        var version = Parse(versionText);
        if (version is null || version.Build < 0)
            throw new ArgumentException("Enter a numeric version such as 1.0.1 or 1.0.1.0.");

        string fileVersion = new Version(version.Major, version.Minor, version.Build, Math.Max(0, version.Revision)).ToString();
        string productVersion = version.ToString();
        var project = XDocument.Load(projectPath, LoadOptions.PreserveWhitespace);
        if (UsesManualAttributes(project))
        {
            string path = AssemblyInfoPath(projectPath);
            var source = ParseAssemblyInfo(path);
            source = SetAttribute(source, FileVersion, fileVersion);
            source = SetAttribute(source, ProductVersion, productVersion);
            Save(path, temporary => File.WriteAllText(temporary, source.ToFullString()));
            return;
        }

        if (IsFalse(Property(project, "GenerateAssemblyFileVersionAttribute")) ||
            IsFalse(Property(project, "GenerateAssemblyInformationalVersionAttribute")))
            throw new InvalidOperationException("This project disables individual version attributes. Update its version metadata directly.");

        var root = project.Root ?? throw new InvalidDataException("Project has no root element.");
        XNamespace ns = root.Name.Namespace;
        var group = root.Elements(ns + "PropertyGroup").SingleOrDefault(g => (string?)g.Attribute("Label") == Label);
        if (group is null)
        {
            group = new XElement(ns + "PropertyGroup", new XAttribute("Label", Label));
            root.Add(new XText(Environment.NewLine + "  "), group, new XText(Environment.NewLine));
        }
        group.SetElementValue(ns + "FileVersion", fileVersion);
        group.SetElementValue(ns + "InformationalVersion", productVersion);
        Save(projectPath, temporary => project.Save(temporary, SaveOptions.DisableFormatting));
    }

    private static Version? Parse(string? text) =>
        Version.TryParse(text?.Trim(), out var version) &&
        version.Major <= ushort.MaxValue && version.Minor <= ushort.MaxValue &&
        version.Build <= ushort.MaxValue && version.Revision <= ushort.MaxValue ? version : null;

    private static string? Property(XDocument project, string name) =>
        project.Root?.Elements().Where(e => e.Name.LocalName == "PropertyGroup")
            .SelectMany(e => e.Elements()).LastOrDefault(e => e.Name.LocalName == name)?.Value.Trim();

    private static bool IsFalse(string? value) => string.Equals(value, "false", StringComparison.OrdinalIgnoreCase);

    private static bool UsesManualAttributes(XDocument project) =>
        IsFalse(Property(project, "GenerateAssemblyInfo")) ||
        (project.Root?.Attribute("Sdk") is null && project.Root?.Elements().Any(e => e.Name.LocalName == "Sdk") != true);

    private static string AssemblyInfoPath(string projectPath)
    {
        string folder = Path.GetDirectoryName(Path.GetFullPath(projectPath))!;
        string[] candidates = [Path.Combine(folder, "Properties", "AssemblyInfo.cs"), Path.Combine(folder, "AssemblyInfo.cs")];
        var existing = candidates.Where(File.Exists).ToArray();
        return existing.Length == 1 ? existing[0] :
            throw new InvalidOperationException("Expected one AssemblyInfo.cs in the project folder or its Properties folder.");
    }

    private static CompilationUnitSyntax ParseAssemblyInfo(string path) =>
        SyntaxFactory.ParseCompilationUnit(File.ReadAllText(path));

    private static AttributeSyntax? FindAttribute(CompilationUnitSyntax source, string name) =>
        source.AttributeLists.Where(list => list.Target?.Identifier.ValueText == "assembly")
            .SelectMany(list => list.Attributes).SingleOrDefault(attribute =>
            {
                string identifier = attribute.Name.ToString().Split('.').Last();
                return identifier == name || identifier == name + "Attribute";
            });

    private static string? AttributeValue(CompilationUnitSyntax source, string name) =>
        (FindAttribute(source, name)?.ArgumentList?.Arguments.FirstOrDefault()?.Expression as LiteralExpressionSyntax)?.Token.ValueText;

    private static CompilationUnitSyntax SetAttribute(CompilationUnitSyntax source, string name, string value)
    {
        var attribute = FindAttribute(source, name);
        var literal = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(value));
        if (attribute is not null)
        {
            var argument = attribute.ArgumentList?.Arguments.SingleOrDefault()
                ?? throw new InvalidDataException($"Cannot update the existing {name} attribute.");
            return source.ReplaceNode(argument.Expression, literal.WithTriviaFrom(argument.Expression));
        }

        string text = source.ToFullString();
        string newline = text.Contains("\r\n", StringComparison.Ordinal) ? "\r\n" : "\n";
        string separator = text.EndsWith('\n') ? "" : newline;
        return SyntaxFactory.ParseCompilationUnit(text + separator +
            $"[assembly: System.Reflection.{name}({literal})]" + newline);
    }

    private static void Save(string path, Action<string> write)
    {
        string backup = path + ".before-version.bak";
        if (!File.Exists(backup)) File.Copy(path, backup);
        string temporary = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            write(temporary);
            File.Move(temporary, path, overwrite: true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}
