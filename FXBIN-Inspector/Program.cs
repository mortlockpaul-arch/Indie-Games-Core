using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

// Read-only, heuristic inspection. This does not decompile or convert Xbox 360 shaders.
if (args.Length == 0 || args.Contains("--help") || args.Contains("-h"))
{
    Console.WriteLine("FXBIN Inspector (.NET 9)\nUsage: FxbinInspector <file-or-folder> [--recursive] [--json <report.json>] [--min-string <length>]\nScans .fxbin files without modifying them.");
    return;
}
string input = args[0];
bool recursive = args.Contains("--recursive");
string? jsonPath = null;
int minString = 4;
for (int i = 1; i < args.Length; i++)
{
    if (args[i] == "--json" && i + 1 < args.Length) jsonPath = args[++i];
    else if (args[i] == "--min-string" && i + 1 < args.Length && int.TryParse(args[++i], out int n)) minString = Math.Clamp(n, 3, 64);
}
if (!File.Exists(input) && !Directory.Exists(input))
{
    Console.Error.WriteLine($"Input not found: {input}");
    Environment.ExitCode = 1;
    return;
}
string[] files = File.Exists(input)
    ? [Path.GetFullPath(input)]
    : Directory.GetFiles(input, "*.fxbin", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
var reports = new List<EffectReport>();
foreach (var file in files.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
{
    try
    {
        byte[] bytes = File.ReadAllBytes(file);
        var strings = ScanStrings(bytes, minString);
        var profiles = strings.Where(s => Regex.IsMatch(s.Value, @"\b(?:vs|ps|gs|hs|ds|cs)_[1-6]_[0-9]\b", RegexOptions.IgnoreCase)).ToList();
        var candidates = strings.Where(s => s.Value.StartsWith("g_", StringComparison.Ordinal) ||
                            Regex.IsMatch(s.Value, @"(?i)(texture|sampler|water|timer|blur|reflection|postprocess|bloom|matrix|world|view|projection)"))
                            .ToList();
        string magic = bytes.Length >= 4 ? Encoding.ASCII.GetString(bytes, 0, 4) : "";
        string format = magic switch
        {
            "DXBC" => "DXBC container signature",
            "DXIL" => "DXIL signature",
            "MGFX" => "MGFX signature",
            _ when bytes.Length >= 4 && bytes[0] == 0xBC && bytes[1] == 0xF0 && bytes[2] == 0x0B && bytes[3] == 0xCF
                => "Legacy Direct3D effect signature (0xCF0BF0BC; exact variant not decoded)",
            _ => "Unknown signature"
        };
        var report = new EffectReport(Path.GetFullPath(file), bytes.LongLength,
            Convert.ToHexString(bytes.AsSpan(0, Math.Min(16, bytes.Length))), format,
            Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant(),
            profiles, candidates, strings);
        reports.Add(report);
        Console.WriteLine($"\n{Path.GetFileName(file)}  [{bytes.Length:N0} bytes]");
        Console.WriteLine($"  Format: {format}");
        Console.WriteLine($"  Header: {report.HeaderHex}");
        Console.WriteLine($"  Shader profiles (string evidence): {(profiles.Count == 0 ? "none found" : string.Join(", ", profiles.Select(x => x.Value).Distinct()))}");
        Console.WriteLine($"  Candidate parameter/technique strings: {candidates.Count}");
        foreach (var s in candidates) Console.WriteLine($"    0x{s.Offset:X4}  {s.Value}");
        Console.WriteLine($"  All printable strings: {strings.Count} (see JSON for offsets)");
    }
    catch (Exception e)
    {
        Console.Error.WriteLine($"ERROR {file}: {e.Message}");
        Environment.ExitCode = 2;
    }
}
if (jsonPath != null)
{
    string full = Path.GetFullPath(jsonPath);
    Directory.CreateDirectory(Path.GetDirectoryName(full)!);
    File.WriteAllText(full, JsonSerializer.Serialize(reports, new JsonSerializerOptions { WriteIndented = true }));
    Console.WriteLine($"\nJSON report: {full}");
}
Console.WriteLine($"\nInspected {reports.Count}/{files.Length} file(s).");

static List<OffsetString> ScanStrings(byte[] bytes, int minimum)
{
    var found = new List<OffsetString>();
    for (int i = 0; i < bytes.Length;)
    {
        if (bytes[i] < 32 || bytes[i] > 126) { i++; continue; }
        int start = i;
        while (i < bytes.Length && bytes[i] >= 32 && bytes[i] <= 126) i++;
        if (i - start >= minimum)
            found.Add(new OffsetString(start, Encoding.ASCII.GetString(bytes, start, i - start)));
    }
    return found;
}
record OffsetString(int Offset, string Value);
record EffectReport(string File, long Length, string HeaderHex, string FormatHint, string Sha256,
    List<OffsetString> ShaderProfileStrings, List<OffsetString> CandidateNames, List<OffsetString> PrintableStrings);
