using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

public sealed class ProjectCleanupRow
{
    public string Action { get; init; } = "";
    public string Path { get; init; } = "";
    public string Destination { get; init; } = "";
    public string Reason { get; init; } = "";
    public string Status { get; set; } = "Preview";
    public bool Actionable => Action is "Disable" or "Delete backup";
}
public sealed record ProjectCleanupPlan(string Root, string Fingerprint, IReadOnlyList<ProjectCleanupRow> Rows);

public static class ArchiveProjectCleanup
{
    private static string Full(string path) => System.IO.Path.TrimEndingDirectorySeparator(System.IO.Path.GetFullPath(path));
    private static void Check(string root, string path)
    {
        path = Full(path);
        if (!path.Equals(root, StringComparison.OrdinalIgnoreCase) && !path.StartsWith(root + System.IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
            throw new IOException("Cleanup target is outside the archive.");
        for (string? current = path; current != null; current = System.IO.Path.GetDirectoryName(current))
            if ((File.GetAttributes(current) & FileAttributes.ReparsePoint) != 0) throw new IOException("Cleanup refuses linked paths.");
    }
    public static ProjectCleanupPlan Preview(string archive, CancellationToken token = default, IProgress<string>? progress = null)
    {
        string root = Full(archive);
        if (root == System.IO.Path.GetPathRoot(root)) throw new IOException("Choose a dedicated archive folder, not a drive root.");
        if (!Directory.Exists(root)) throw new DirectoryNotFoundException(root);
        // Check ancestors too: a normal folder beneath a junction is still a linked path.
        Check(root, root);
        return Scan(root, token, progress);
    }
    private static ProjectCleanupPlan Scan(string root, CancellationToken token, IProgress<string>? progress)
    {
        var files = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var rows = new List<ProjectCleanupRow>();
        int scannedFolders=0; long lastProgress=0;
        void Add(string action, string path, string reason, string destination = "") => rows.Add(new() { Action=action, Path=path, Reason=reason, Destination=destination });
        void Walk(string dir)
        {
            token.ThrowIfCancellationRequested();
            scannedFolders++;
            if (Environment.TickCount64-lastProgress>=100) { progress?.Report($"Scanning Folders: {scannedFolders:N0} · {dir}"); lastProgress=Environment.TickCount64; }
            foreach (string entry in Directory.EnumerateFileSystemEntries(dir))
            {
                token.ThrowIfCancellationRequested();
                var attributes = File.GetAttributes(entry);
                if ((attributes & FileAttributes.ReparsePoint) != 0) { Add("Keep", entry, "Linked path excluded"); continue; }
                if ((attributes & FileAttributes.Directory) != 0) Walk(entry);
                else if (System.IO.Path.GetExtension(entry).Equals(".csproj", StringComparison.OrdinalIgnoreCase) || System.IO.Path.GetExtension(entry).Equals(".bak", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = File.OpenRead(entry);
                    files.Add(entry, Convert.ToHexString(SHA256.HashData(stream)));
                }
            }
        }
        Walk(root);
        var projects = files.Keys.Where(p => p.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)).ToArray();
        var references = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        bool uncertain = false;
        foreach (string project in projects)
        {
            string raw = File.ReadAllText(project);
            // Even malformed old projects can refer to a library that must stay active.
            foreach (Match match in Regex.Matches(raw, "<ProjectReference\\b[^>]*\\bInclude\\s*=\\s*[\"']([^\"']+)[\"']", RegexOptions.IgnoreCase))
            {
                string include = System.Net.WebUtility.HtmlDecode(match.Groups[1].Value);
                if (include.Contains("$(") || include.Contains("@(") || include.IndexOfAny(['*','?',';']) >= 0) { uncertain=true; continue; }
                try { references.Add(Full(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(project)!, include))); }
                catch { uncertain=true; }
            }
        }
        foreach (string game in Directory.EnumerateDirectories(root).Order(StringComparer.OrdinalIgnoreCase))
        {
            token.ThrowIfCancellationRequested();
            if ((File.GetAttributes(game) & FileAttributes.ReparsePoint) != 0) continue;
            string canonical = System.IO.Path.Combine(game, System.IO.Path.GetFileName(game) + ".csproj");
            var gameProjects = projects.Where(p => p.StartsWith(game + System.IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)).ToArray();
            if (gameProjects.Length == 0) continue;
            string? problem = null;
            if (!files.ContainsKey(canonical)) problem = "Folder-named main project is missing";
            else
            {
                try
                {
                    var xml = XDocument.Load(canonical);
                    if (!xml.Descendants().Any(e => e.Name.LocalName == "OutputType" && (e.Value.Trim().Equals("Exe", StringComparison.OrdinalIgnoreCase) || e.Value.Trim().Equals("WinExe", StringComparison.OrdinalIgnoreCase))))
                        problem = "Main project is not explicitly executable";
                }
                catch (Exception e) { problem = "Main project XML is invalid: " + e.Message; }
            }
            if (problem != null) Add("Review", canonical, problem);
            else Add("Keep", canonical, "Folder-named executable main project");
            foreach (string project in gameProjects)
            {
                if (project.Equals(canonical, StringComparison.OrdinalIgnoreCase)) continue;
                if (problem != null) { Add("Keep", project, "Main project needs review"); continue; }
                if (uncertain) { Add("Keep", project, "Dynamic project references require manual review"); continue; }
                if (references.Contains(project)) { Add("Keep", project, "Referenced by an active project"); continue; }
                string destination = project + ".disabled";
                bool collision = File.Exists(destination);
                if (collision) destination = project + ".cleanup-" + Guid.NewGuid().ToString("N") + ".disabled";
                Add("Disable", project, collision ? "Unreferenced duplicate; preserve existing disabled copy" : "Unreferenced project; folder-named executable exists", destination);
            }
        }
        foreach (string project in projects.Where(p => System.IO.Path.GetDirectoryName(p)!.Equals(root, StringComparison.OrdinalIgnoreCase)))
            Add("Keep", project, "Archive-level project; outside a game folder");
        foreach (string backup in files.Keys.Where(p => p.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))) Add("Delete backup", backup, "Backup file (*.bak)");
        string fingerprint = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(string.Join("\n", files.Select(f => f.Key + "\0" + f.Value)))));
        return new(root, fingerprint, rows);
    }
    public static void Apply(ProjectCleanupPlan plan, CancellationToken token = default, IProgress<string>? progress = null)
    {
        // Re-scan the entire dependency graph before applying a preview, including new/changed projects.
        var fresh = Preview(plan.Root, token, progress);
        if (fresh.Fingerprint != plan.Fingerprint) throw new IOException("Archive changed since preview. Scan again before applying cleanup.");
        var allowed = fresh.Rows.Where(r => r.Actionable).ToDictionary(r => r.Path, StringComparer.OrdinalIgnoreCase);
        foreach (var row in plan.Rows.Where(r => r.Actionable && r.Status == "Preview"))
        {
            token.ThrowIfCancellationRequested();
            try
            {
                if (!allowed.TryGetValue(row.Path, out var verified) || verified.Action != row.Action) throw new IOException("Cleanup action is no longer valid.");
                Check(plan.Root, row.Path);
                progress?.Report(row.Action + ": " + row.Path);
                if (row.Action == "Disable")
                {
                    if (!row.Destination.StartsWith(row.Path + ".", StringComparison.OrdinalIgnoreCase) || !row.Destination.EndsWith(".disabled", StringComparison.OrdinalIgnoreCase) || System.IO.Path.GetDirectoryName(row.Destination) != System.IO.Path.GetDirectoryName(row.Path)) throw new IOException("Invalid disabled destination.");
                    File.Move(row.Path, row.Destination); // Never overwrite an existing disabled copy.
                }
                else File.Delete(row.Path);
                row.Status = "Applied";
            }
            catch (Exception e) { row.Status = "Failed: " + e.Message; }
        }
    }
    public static void Export(ProjectCleanupPlan plan, string path)
    {
        static string Quote(string value) => "\"" + value.Replace("\"", "\"\"") + "\"";
        File.WriteAllLines(path, new[] { "Action,Path,Destination,Reason,Status" }.Concat(plan.Rows.Select(r => string.Join(",", new[] { r.Action,r.Path,r.Destination,r.Reason,r.Status }.Select(Quote)))), new UTF8Encoding(true));
    }
}
