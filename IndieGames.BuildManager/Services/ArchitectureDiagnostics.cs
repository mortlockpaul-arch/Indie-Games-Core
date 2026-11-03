using System.IO;
using System.Reflection.PortableExecutable;

namespace IndieGames.BuildManager.Services;

public static class ArchitectureDiagnostics
{
    public static string? Normalize(string value) => value.Trim().ToLowerInvariant() switch
    {
        "x86" or "win-x86" => "x86",
        "x64" or "amd64" or "win-x64" => "x64",
        "arm64" or "win-arm64" => "arm64",
        "arm" or "win-arm" => "arm",
        _ => null
    };

    public static IReadOnlyList<ConfigurationIssue> Inspect(
        string targetPath, string platformTarget, string runtimeIdentifier,
        CancellationToken token = default)
    {
        var issues = new List<ConfigurationIssue>();
        string? target = Normalize(platformTarget), runtime = Normalize(runtimeIdentifier);
        if (target != null && runtime != null && target != runtime)
            issues.Add(new("Error", "ARCH_SETTINGS",
                $"PlatformTarget '{platformTarget}' conflicts with RuntimeIdentifier '{runtimeIdentifier}'.",
                "Use the same architecture for the compiler target and runtime identifier."));
        string? expected = runtime ?? target;
        string? folder = Path.GetDirectoryName(targetPath);
        if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder) || !File.Exists(targetPath))
        {
            issues.Add(new("Warning", "ARCH_OUTPUT_MISSING",
                $"No compiled game found at '{targetPath}'; binary architecture has not been checked.",
                "Build the selected configuration, then check architecture again."));
            return issues;
        }

        var binaries = new List<(string Path, string Architecture)>();
        foreach (string path in Directory.EnumerateFiles(folder).Where(path =>
                     Path.GetExtension(path).Equals(".dll", StringComparison.OrdinalIgnoreCase) ||
                     Path.GetExtension(path).Equals(".exe", StringComparison.OrdinalIgnoreCase)))
        {
            token.ThrowIfCancellationRequested();
            try
            {
                using var stream = File.OpenRead(path);
                using var pe = new PEReader(stream);
                if (pe.PEHeaders.PEHeader == null) continue;
                var clr = pe.PEHeaders.CorHeader;
                // I386 + ILOnly without Requires32Bit is portable managed IL,
                // not an x86-only dependency. Prefer32Bit does not constrain DLLs.
                string architecture = clr != null &&
                    pe.PEHeaders.CoffHeader.Machine == Machine.I386 &&
                    clr.Flags.HasFlag(CorFlags.ILOnly) &&
                    !clr.Flags.HasFlag(CorFlags.Requires32Bit)
                    ? "AnyCPU"
                    : pe.PEHeaders.CoffHeader.Machine switch
                    {
                        Machine.I386 => "x86",
                        Machine.Amd64 => "x64",
                        Machine.Arm64 => "arm64",
                        Machine.ArmThumb2 or Machine.Arm => "arm",
                        _ => pe.PEHeaders.CoffHeader.Machine.ToString()
                    };
                binaries.Add((path, architecture));
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or BadImageFormatException)
            {
                issues.Add(new("Warning", "ARCH_UNREADABLE",
                    $"Cannot inspect '{Path.GetFileName(path)}': {ex.Message}",
                    "Check that the file is a valid, readable Windows executable or DLL."));
            }
        }

        string appHost = Path.ChangeExtension(targetPath, ".exe");
        string? launchArchitecture = binaries.FirstOrDefault(b =>
            b.Path.Equals(appHost, StringComparison.OrdinalIgnoreCase)).Architecture;
        launchArchitecture = launchArchitecture == "AnyCPU" ? null : launchArchitecture;
        launchArchitecture ??= binaries.FirstOrDefault(b =>
            b.Path.Equals(targetPath, StringComparison.OrdinalIgnoreCase) && b.Architecture != "AnyCPU").Architecture;
        string? processArchitecture = launchArchitecture ?? expected;
        foreach (var binary in binaries)
        {
            if (binary.Architecture == "AnyCPU") continue;
            if (processArchitecture != null && binary.Architecture != processArchitecture)
                issues.Add(new("Error", "ARCH_MISMATCH",
                    $"'{Path.GetFileName(binary.Path)}' is {binary.Architecture}, but the game runs as {processArchitecture}. Output: {folder}",
                    $"Rebuild or replace this dependency for {processArchitecture}, then rebuild and repackage the game."));
        }
        if (expected != null && launchArchitecture != null && expected != launchArchitecture)
            issues.Add(new("Error", "ARCH_STALE_OUTPUT",
                $"The compiled game runs as {launchArchitecture}, but the selected build settings require {expected}. Output: {folder}",
                "Rebuild the selected configuration and architecture before running or sharing this output."));
        if (processArchitecture == null && binaries.Any(b => b.Architecture != "AnyCPU"))
            issues.Add(new("Warning", "ARCH_UNSPECIFIED",
                "The output contains architecture-specific dependencies, but the game's process architecture is unspecified.",
                "Select a matching PlatformTarget or RuntimeIdentifier, rebuild, and check again."));
        if (!issues.Any(i => i.Code.StartsWith("ARCH_", StringComparison.Ordinal) && i.Severity != "Info"))
            issues.Add(new("Info", "ARCH_OK",
                $"Checked {binaries.Count} executable/DLL files in '{folder}': compatible with {processArchitecture ?? "AnyCPU"}.",
                "Repeat this check after changing dependencies or packaging a new build."));
        return issues;
    }
}
