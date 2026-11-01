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
    private const string VersionMarker = "// INDIE_BUILD_MANAGER_FNA_VERSIONS";
    private const string VersionLines = """
        // INDIE_BUILD_MANAGER_FNA_VERSIONS
        try
        {
            var fnaVersionAssembly = typeof(Microsoft.Xna.Framework.Game).Assembly;
            System.Console.WriteLine("Framework: " + fnaVersionAssembly.FullName);
            foreach (string[] dependency in new string[][] {
                new string[] { "FNA3D", "Microsoft.Xna.Framework.Graphics.FNA3D", "FNA3D_LinkedVersion" },
                new string[] { "FAudio", "FAudio", "FAudioLinkedVersion" },
                new string[] { "SDL", System.Environment.GetEnvironmentVariable("FNA_PLATFORM_BACKEND") == "SDL2" ? "SDL2.SDL" : "SDL3.SDL", "SDL_GetVersion" }
            })
            {
                try
                {
                    var binding = fnaVersionAssembly.GetType(dependency[1], false);
                    if (binding == null && dependency[0] == "SDL") binding = fnaVersionAssembly.GetType("SDL2.SDL", false);
                    if (binding == null) throw new System.TypeLoadException("Missing binding: " + dependency[1]);
                    var query = binding.GetMethod(dependency[2], System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                    if (query == null) throw new System.MissingMethodException(dependency[1], dependency[2]);
                    var parameters = query.GetParameters();
                    if (parameters.Length == 1 && parameters[0].ParameterType.IsByRef)
                    {
                        object[] arguments = new object[] { null };
                        query.Invoke(null, arguments);
                        var version = arguments[0];
                        var versionType = version.GetType();
                        System.Console.WriteLine(dependency[0] + ": " + versionType.GetField("major").GetValue(version) + "." + versionType.GetField("minor").GetValue(version) + "." + versionType.GetField("patch").GetValue(version));
                    }
                    else
                    {
                        uint version = System.Convert.ToUInt32(query.Invoke(null, null));
                        uint scale = dependency[0] == "SDL" ? 1000u : 100u;
                        System.Console.WriteLine(dependency[0] + ": " + (version / (scale * scale)) + "." + (version / scale % scale) + "." + (version % scale));
                    }
                }
                catch (System.Exception error)
                {
                    System.Console.WriteLine(dependency[0] + " version unavailable: " + error.GetBaseException().Message);
                }
            }
        }
        catch (System.Exception error)
        {
            System.Console.WriteLine("Framework version unavailable: " + error.GetBaseException().Message);
        }
        """;

    public static string InjectVersions(string projectPath) => Inject(projectPath, true);

    public static string Inject(string projectPath) => Inject(projectPath, false);

    private static string Inject(string projectPath, bool versions)
    {
        string marker = versions ? VersionMarker : Marker;
        string lines = versions ? VersionLines : Lines;
        string directory = Path.GetDirectoryName(projectPath)!;
        var matches = new List<(string file, Match match)>();
        var regex = new Regex(@"\bstatic\s+(?:void|int)\s+Main\s*\([^)]*\)\s*\{", RegexOptions.CultureInvariant);
        foreach (string file in Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories))
        {
            if (file.Split(Path.DirectorySeparatorChar).Any(p => p.Equals("obj", StringComparison.OrdinalIgnoreCase) || p.Equals("bin", StringComparison.OrdinalIgnoreCase))) continue;
            string content = File.ReadAllText(file);
            if (content.Contains(marker, StringComparison.Ordinal)) return $"Already injected: {file}";
            foreach (Match m in regex.Matches(content)) matches.Add((file, m));
        }
        if (matches.Count != 1) return $"Skipped: found {matches.Count} conventional Main methods. Manual review needed.";
        var (path, match) = matches[0];
        string backup = path + (versions ? ".fna-versions.bak" : ".startup-diagnostics.bak");
        if (File.Exists(backup)) return $"Skipped: backup already exists: {backup}";
        string source = File.ReadAllText(path);
        string newline = source.Contains("\r\n") ? "\r\n" : "\n";
        string insert = newline + "        " + lines.Replace("\r\n", "\n").Replace("\n", newline + "        ") + newline;
        File.Copy(path, backup);
        File.WriteAllText(path, source.Insert(match.Index + match.Length, insert), new UTF8Encoding(false));
        return $"Injected into {path}; backup: {backup}";
    }
}
