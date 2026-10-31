using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace IndieGames.BuildManager.Services;
public static class StartupDiagnosticsInjector
{
    private const string Marker = "// INDIE_BUILD_MANAGER_STARTUP_DIAGNOSTICS";
    private const string Lines = """
        // INDIE_BUILD_MANAGER_STARTUP_DIAGNOSTICS
        System.Console.WriteLine("=== Game Startup Diagnostics ===");
        System.Console.WriteLine($"Runtime: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}");
        System.Console.WriteLine($"Architecture: {System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture}");
        System.Console.WriteLine($"OS Architecture: {System.Runtime.InteropServices.RuntimeInformation.OSArchitecture}");
        System.Console.WriteLine($"Platform: {System.Runtime.InteropServices.RuntimeInformation.OSDescription}");
        System.Console.WriteLine($"64-bit Process: {System.Environment.Is64BitProcess}");
        System.Console.WriteLine($"Executable: {System.Environment.ProcessPath}");
        System.Console.WriteLine($"Working Directory: {System.Environment.CurrentDirectory}");
        System.Console.WriteLine("================================");
        """;
    public static string Inject(string projectPath)
    {
        string directory = Path.GetDirectoryName(projectPath)!;
        var matches = new List<(string file, Match match)>();
        var regex = new Regex(@"\bstatic\s+(?:void|int)\s+Main\s*\([^)]*\)\s*\{", RegexOptions.CultureInvariant);
        foreach (string file in Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories))
        {
            if (file.Split(Path.DirectorySeparatorChar).Any(p => p.Equals("obj", StringComparison.OrdinalIgnoreCase) || p.Equals("bin", StringComparison.OrdinalIgnoreCase))) continue;
            string content = File.ReadAllText(file);
            if (content.Contains(Marker, StringComparison.Ordinal)) return $"Already injected: {file}";
            foreach (Match m in regex.Matches(content)) matches.Add((file, m));
        }
        if (matches.Count != 1) return $"Skipped: found {matches.Count} conventional Main methods. Manual review needed.";
        var (path, match) = matches[0];
        string backup = path + ".startup-diagnostics.bak";
        if (File.Exists(backup)) return $"Skipped: backup already exists: {backup}";
        string source = File.ReadAllText(path);
        string newline = source.Contains("\r\n") ? "\r\n" : "\n";
        string insert = newline + "        " + Lines.Replace("\n", newline + "        ") + newline;
        File.Copy(path, backup);
        File.WriteAllText(path, source.Insert(match.Index + match.Length, insert), new UTF8Encoding(false));
        return $"Injected into {path}; backup: {backup}";
    }
}
