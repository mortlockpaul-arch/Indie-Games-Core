using System;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

/// <summary>Protects playable outputs when a user switches between x86 and x64.</summary>
internal static class GameOutputArchitecture
{
    private const string MarkerName = ".buildmanager-architecture";

    public static void Prepare(string output, string project, string target, Action<string> log)
    {
        string arch = Normalize(target);
        if (!Directory.Exists(output)) return;
        string marker = Path.Combine(output, MarkerName);
        string? prior = File.Exists(marker) ? File.ReadAllText(marker).Trim() : null;
        string? mismatch = FindMismatch(output, project, arch);
        if (prior != null && prior != arch)
            mismatch ??= $"previous deployment was {prior}, requested {arch}";
        if (mismatch == null)
        {
            log($"Playable output architecture: no incompatible main binaries detected ({arch}).");
            return;
        }
        string backup = output + ".architecture-backup-" + DateTime.Now.ToString("yyyyMMdd-HHmmss-fff");
        if (Directory.Exists(backup)) throw new IOException("Backup already exists: " + backup);
        log("Architecture change detected: " + mismatch);
        // Directory.Move is atomic on the same volume. Never delete the old output.
        Directory.Move(output, backup);
        log("Previous playable output preserved: " + backup);
    }

    public static void Verify(string output, string project, string target, Action<string> log)
    {
        string arch = Normalize(target);
        if (!Directory.Exists(output)) throw new DirectoryNotFoundException(output);
        string stem = AssemblyStem(project);
        string dll = Path.Combine(output, stem + ".dll");
        string exe = Path.Combine(output, stem + ".exe");
        if (!File.Exists(dll)) throw new FileNotFoundException("Main game DLL not deployed", dll);
        if (!File.Exists(exe)) throw new FileNotFoundException("Main game executable not deployed", exe);
        string? mismatch = FindMismatch(output, project, arch);
        if (mismatch != null) throw new InvalidOperationException(mismatch);
        // Write only after successful verification, so interrupted builds are not marked valid.
        File.WriteAllText(Path.Combine(output, MarkerName), arch);
        log($"Playable output verified: {arch} executable and compatible main assembly: {output}");
    }

    private static string Normalize(string target) => target.Trim().ToLowerInvariant() switch
    {
        "x86" => "x86", "x64" => "x64",
        _ => throw new NotSupportedException("Architecture verification requires x86 or x64: " + target)
    };

    private static string AssemblyStem(string project)
    {
        try
        {
            var doc = XDocument.Load(project);
            string? name = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "AssemblyName")?.Value;
            if (!string.IsNullOrWhiteSpace(name) && !name.Contains("$(", StringComparison.Ordinal))
                return name.Trim();
        }
        catch (System.Xml.XmlException) { }
        return Path.GetFileNameWithoutExtension(project);
    }

    private static string? FindMismatch(string output, string project, string expected)
    {
        string stem = AssemblyStem(project);
        foreach (string ext in new[] { ".exe", ".dll" })
        {
            string file = Path.Combine(output, stem + ext);
            if (!File.Exists(file)) continue;
            try
            {
                using var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var pe = new PEReader(stream);
                var headers = pe.PEHeaders;
                var machine = headers.CoffHeader.Machine;
                bool managed = headers.CorHeader != null;
                if (managed)
                {
                    bool requires32 = (headers.CorHeader!.Flags & CorFlags.Requires32Bit) != 0;
                    if (expected == "x64" && requires32)
                        return $"{Path.GetFileName(file)} is managed x86 (32BITREQUIRED)";
                    if (expected == "x86" && machine == System.Reflection.PortableExecutable.Machine.Amd64)
                        return $"{Path.GetFileName(file)} is a managed x64 PE";
                }
                else if ((expected == "x64" && machine == System.Reflection.PortableExecutable.Machine.I386) ||
                         (expected == "x86" && machine == System.Reflection.PortableExecutable.Machine.Amd64))
                    return $"{Path.GetFileName(file)} is native {machine}, expected {expected}";
            }
            catch (BadImageFormatException) { return $"{Path.GetFileName(file)} is not a valid PE image"; }
        }
        return null;
    }
}
