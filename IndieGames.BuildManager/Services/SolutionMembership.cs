using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

public static class SolutionMembership
{
    public static HashSet<string> Read(string solution)
    {
        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(solution)) return result;
        string folder = Path.GetDirectoryName(Path.GetFullPath(solution))!;
        IEnumerable<string> references;
        if (Path.GetExtension(solution).Equals(".slnx",StringComparison.OrdinalIgnoreCase))
            references = XDocument.Load(solution).Descendants().Where(e => e.Name.LocalName == "Project")
                .Select(e => (string?)e.Attribute("Path")).OfType<string>();
        else
            references = Regex.Matches(File.ReadAllText(solution),"(?m)^Project\\(\"[^\"]+\"\\)\\s*=\\s*\"[^\"]*\",\\s*\"(?<path>[^\"]+)\"")
                .Select(m => m.Groups["path"].Value);
        foreach (string reference in references.Where(p => p.EndsWith(".csproj",StringComparison.OrdinalIgnoreCase)))
        {
            string path = reference.Replace('\\',Path.DirectorySeparatorChar).Replace('/',Path.DirectorySeparatorChar);
            result.Add(Path.GetFullPath(Path.IsPathRooted(path) ? path : Path.Combine(folder,path)));
        }
        return result;
    }
}
