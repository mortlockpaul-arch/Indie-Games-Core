using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using IndieGames.BuildManager.Models;

namespace IndieGames.BuildManager.Services;

public sealed record ConfigurationIssue(string Severity, string Code, string Detail, string Recommendation);
public sealed record ConfigurationReport(string Project, IReadOnlyList<ConfigurationIssue> Issues);

public static class ConfigurationDiagnostics
{
    public static async Task<ConfigurationReport> InspectAsync(
        GameProject project, string configuration, string platform, string target,
        string frameworkOverride, string runtimeOverride, string msbuildExecutable,
        Action<string> log, CancellationToken token, IEnumerable<string>? buildArguments = null, string? workingDirectory = null)
    {
        var issues = new List<ConfigurationIssue>();
        void Add(string severity, string code, string detail, string fix) =>
            issues.Add(new(severity, code, detail, fix));

        var args = new List<string>();
        if (msbuildExecutable.Equals("dotnet", StringComparison.OrdinalIgnoreCase))
            args.Add("msbuild");
        args.Add(project.Path);
        if (buildArguments != null) args.AddRange(buildArguments);
        args.Add($"-p:Configuration={configuration}");
        args.Add($"-p:Platform={platform}");
        if (!string.IsNullOrWhiteSpace(target)) args.Add($"-p:PlatformTarget={target}");
        if (!string.IsNullOrWhiteSpace(frameworkOverride))
            args.Add($"-p:TargetFramework={frameworkOverride}");
        if (!string.IsNullOrWhiteSpace(runtimeOverride))
            args.Add($"-p:RuntimeIdentifier={runtimeOverride}");
        args.Add("-getProperty:Configuration,Platform,PlatformTarget,Platforms,Configurations,TargetFramework,TargetFrameworks,RuntimeIdentifier,RuntimeIdentifiers,OutputPath,TargetPath,BaseIntermediateOutputPath,ProjectAssetsFile,IsCrossTargetingBuild");

        var start = new ProcessStartInfo(msbuildExecutable) {
            WorkingDirectory = workingDirectory ?? Path.GetDirectoryName(project.Path)!,
            RedirectStandardOutput = true, RedirectStandardError = true,
            UseShellExecute = false, CreateNoWindow = true
        };
        foreach (var arg in args) start.ArgumentList.Add(arg);
        using var process = new Process { StartInfo = start };
        process.Start();
        using var registration = token.Register(() => {
            try { if (!process.HasExited) process.Kill(entireProcessTree: true); } catch { }
        });
        var stdout = process.StandardOutput.ReadToEndAsync(token);
        var stderr = process.StandardError.ReadToEndAsync(token);
        await process.WaitForExitAsync(token);
        string output = await stdout, errors = await stderr;
        if (process.ExitCode != 0) {
            Add("Error", "MSBUILD", $"MSBuild evaluation failed: {errors.Trim()} {output.Trim()}",
                "Check project XML, imported props/targets and SDK availability.");
            return new(project.Name, issues);
        }

        Dictionary<string, string> props;
        try {
            using var json = JsonDocument.Parse(output);
            props = json.RootElement.GetProperty("Properties").EnumerateObject()
                .ToDictionary(x => x.Name, x => x.Value.ToString(), StringComparer.OrdinalIgnoreCase);
        }
        catch (Exception ex) {
            Add("Error", "EVALUATION", $"Could not parse MSBuild property output: {ex.Message}",
                "Check that MSBuild supports -getProperty and produces JSON output.");
            log(output);
            return new(project.Name, issues);
        }
        string P(string key) => props.GetValueOrDefault(key) ?? "";
        bool Contains(string values, string value) => values.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Contains(value, StringComparer.OrdinalIgnoreCase);
        var platforms = P("Platforms");
        if (!string.IsNullOrWhiteSpace(platforms) && !Contains(platforms, platform))
            Add("Warning", "PLATFORM", $"Requested Platform '{platform}' is not in evaluated Platforms '{platforms}'.",
                "Select a supported platform or inspect solution/project configuration mappings.");
        var configurations = P("Configurations");
        if (!string.IsNullOrWhiteSpace(configurations) && !Contains(configurations, configuration))
            Add("Warning", "CONFIGURATION", $"Configuration '{configuration}' is not in '{configurations}'.",
                "Select a declared configuration.");
        var evaluatedTarget = P("PlatformTarget");
        if (!string.IsNullOrWhiteSpace(evaluatedTarget) && !string.IsNullOrWhiteSpace(target) &&
            !evaluatedTarget.Equals(target, StringComparison.OrdinalIgnoreCase))
            Add("Warning", "TARGET", $"Evaluated PlatformTarget '{evaluatedTarget}' differs from requested '{target}'.",
                "Review conditional PropertyGroups and the requested compiler architecture.");
        var tfm = P("TargetFramework");
        var tfms = P("TargetFrameworks");
        if (string.IsNullOrWhiteSpace(tfm) && !string.IsNullOrWhiteSpace(tfms))
            Add("Info", "MULTITARGET", $"Project targets multiple frameworks: {tfms}.",
                "Select a specific framework when checking assets or building.");
        if (!string.IsNullOrWhiteSpace(frameworkOverride) && !string.IsNullOrWhiteSpace(tfms) &&
            !Contains(tfms, frameworkOverride))
            Add("Warning", "FRAMEWORK", $"Framework '{frameworkOverride}' is not declared in TargetFrameworks '{tfms}'.",
                "Use a supported framework or update the project intentionally.");

        string assetsPath = P("ProjectAssetsFile");
        if (string.IsNullOrWhiteSpace(assetsPath))
            assetsPath = Path.Combine(Path.GetDirectoryName(project.Path)!, "obj", "project.assets.json");
        if (!Path.IsPathRooted(assetsPath))
            assetsPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(project.Path)!, assetsPath));
        if (!File.Exists(assetsPath)) {
            Add("Warning", "ASSETS_MISSING", $"NuGet assets file not found: {assetsPath}",
                "Run restore for the selected framework and runtime before building.");
        }
        else {
            try {
                using var json = JsonDocument.Parse(await File.ReadAllTextAsync(assetsPath, token));
                if (json.RootElement.TryGetProperty("targets", out var targets)) {
                    var keys = targets.EnumerateObject().Select(p => p.Name).ToArray();
                    string rid = P("RuntimeIdentifier");
                    if (!string.IsNullOrWhiteSpace(tfm)) {
                        bool matching = keys.Any(k =>
                            k.Equals(tfm, StringComparison.OrdinalIgnoreCase) ||
                            k.StartsWith(tfm + "/", StringComparison.OrdinalIgnoreCase));
                        if (!matching)
                            Add("Error", "NETSDK1005", $"Assets targets [{string.Join(", ", keys)}] do not include '{tfm}'.",
                                "Restore this project using the evaluated target framework.");
                        else if (!string.IsNullOrWhiteSpace(rid) &&
                                 !keys.Any(k => k.Equals(tfm + "/" + rid, StringComparison.OrdinalIgnoreCase)))
                            Add("Error", "NETSDK1047", $"Assets targets do not include '{tfm}/{rid}'.",
                                "Restore with the matching RuntimeIdentifier, or remove an unintended RID override.");
                    }
                }
            }
            catch (Exception ex) {
                Add("Warning", "ASSETS_PARSE", $"Cannot read {assetsPath}: {ex.Message}",
                    "Clean and restore the project's NuGet assets.");
            }
        }
        string targetPath = P("TargetPath");
        if (!string.IsNullOrWhiteSpace(targetPath) && !Path.IsPathRooted(targetPath))
            targetPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(project.Path)!, targetPath));
        issues.AddRange(ArchitectureDiagnostics.Inspect(targetPath, evaluatedTarget, P("RuntimeIdentifier"), token));
        if (issues.Count == 0)
            Add("Info", "NO_ISSUES", "No issues found in the inspected properties and assets.",
                "This is not a guarantee that compilation or solution configuration mappings are valid.");
        return new(project.Name, issues);
    }
}
