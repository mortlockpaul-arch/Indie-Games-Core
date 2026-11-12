using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

/// <summary>Stages verified Windows XNBs, then deploys only after explicit confirmation.</summary>
public static class ShaderDeployment
{
    public sealed record Candidate(string Asset, string Source, string Compiled, string Package, string OriginalHash, string PackageHash);
    public sealed record Summary(int Packaged, int Deployed, int Skipped);

    static string Hash(string path) => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path)));
    static string Within(string root, string relative)
    {
        if (Path.IsPathRooted(relative)) throw new InvalidDataException("Absolute paths are forbidden in shader reports.");
        string path = Path.GetFullPath(Path.Combine(root, relative));
        if (!path.StartsWith(Path.TrimEndingDirectorySeparator(Path.GetFullPath(root)) + Path.DirectorySeparatorChar,
            StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Shader report path escapes its directory.");
        return path;
    }

    public static IReadOnlyList<Candidate> Prepare(string gameFolder, Action<string> log, CancellationToken token)
    {
        string root = Path.GetFullPath(gameFolder);
        string content = Path.Combine(root, "Content");
        string recovery = Path.Combine(root, "build", "shader-recovery");
        string compiled = Path.Combine(recovery, "compiled-fna");
        string staged = Path.Combine(recovery, "packaged-xnb");
        var candidates = new List<Candidate>();
        if (!Directory.Exists(compiled)) { log("No compiled effects: " + compiled); return candidates; }
        foreach (string report in Directory.EnumerateFiles(compiled, "*.fxb.json", SearchOption.AllDirectories).OrderBy(x => x))
        {
            token.ThrowIfCancellationRequested();
            try
            {
                using var json = JsonDocument.Parse(File.ReadAllText(report));
                var obj = json.RootElement;
                string relative = obj.GetProperty("sourceXnb").GetString() ?? throw new InvalidDataException("Missing sourceXnb");
                string expected = obj.GetProperty("sourceSha256").GetString() ?? throw new InvalidDataException("Missing source hash");
                string fxRelative = obj.GetProperty("recoveredFx").GetString() ?? throw new InvalidDataException("Missing recovered FX");
                string fxbRelative = obj.GetProperty("compiledFxb").GetString() ?? throw new InvalidDataException("Missing compiled FXB");
                string asset = Within(content, relative);
                string fx = Within(recovery, fxRelative);
                string fxb = Within(recovery, fxbRelative);
                if (!relative.EndsWith(".xnb", StringComparison.OrdinalIgnoreCase) ||
                    !fx.StartsWith(Path.Combine(recovery, "recovered-hlsl") + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) ||
                    !fxb.StartsWith(compiled + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) ||
                    !File.Exists(asset) || !File.Exists(fx) || !File.Exists(fxb)) throw new InvalidDataException("Missing or invalid source/output paths");
                if (!Hash(asset).Equals(expected, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Original XNB changed since recovery");
                byte[] bytes = File.ReadAllBytes(fxb);
                if (bytes.Length < 16 || bytes.Length > 64 * 1024 * 1024) throw new InvalidDataException("Invalid compiled effect size");
                // The review-stage report is produced only after fxc exits successfully.
                // Packaging does not prove rendering correctness.
                byte[] packed = KnownShaderRepair.PackEffect(bytes);
                string package = Within(staged, relative);
                Directory.CreateDirectory(Path.GetDirectoryName(package)!);
                File.WriteAllBytes(package, packed);
                string packageHash = Hash(package);
                candidates.Add(new(asset, fx, fxb, package, expected, packageHash));
                log("PACKAGED: " + relative + " (source fingerprint verified)");
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException or InvalidDataException or KeyNotFoundException)
            { log("SKIPPED: " + Path.GetFileName(report) + ": " + ex.Message); }
        }
        return candidates;
    }

    public static Summary Deploy(string gameFolder, IReadOnlyList<Candidate> candidates, Action<string> log, CancellationToken token)
    {
        string root = Path.GetFullPath(gameFolder);
        string backup = Path.Combine(root, "shader-repair-backups", "recovered-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss") + "-" + Guid.NewGuid().ToString("N"));
        var eligible = new List<Candidate>();
        foreach (var c in candidates)
        {
            token.ThrowIfCancellationRequested();
            if (!File.Exists(c.Asset) || !File.Exists(c.Package) ||
                !Hash(c.Asset).Equals(c.OriginalHash, StringComparison.OrdinalIgnoreCase) ||
                !Hash(c.Package).Equals(c.PackageHash, StringComparison.OrdinalIgnoreCase))
            { log("SKIPPED changed input: " + c.Asset); continue; }
            if (c.Asset.EndsWith(".xnb", StringComparison.OrdinalIgnoreCase) &&
                c.Asset.StartsWith(Path.Combine(root, "Content") + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                eligible.Add(c);
        }
        if (eligible.Count == 0) return new Summary(candidates.Count, 0, candidates.Count);
        // Write all backups before replacing any game file.
        foreach (var c in eligible)
        {
            token.ThrowIfCancellationRequested();
            string destination = Within(backup, Path.GetRelativePath(root, c.Asset));
            Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
            File.Copy(c.Asset, destination, overwrite: false);
        }
        var installed = new List<Candidate>();
        try
        {
            foreach (var c in eligible)
            {
                token.ThrowIfCancellationRequested();
                if (!Hash(c.Asset).Equals(c.OriginalHash, StringComparison.OrdinalIgnoreCase) ||
                    !Hash(c.Package).Equals(c.PackageHash, StringComparison.OrdinalIgnoreCase))
                    throw new IOException("File changed during deployment: " + c.Asset);
                string temp = c.Asset + ".shader-" + Guid.NewGuid().ToString("N") + ".tmp";
                try { File.Copy(c.Package, temp); File.Move(temp, c.Asset, overwrite: true); }
                finally { if (File.Exists(temp)) File.Delete(temp); }
                installed.Add(c);
                log("DEPLOYED: " + Path.GetRelativePath(root, c.Asset));
            }
        }
        catch
        {
            foreach (var c in installed.AsEnumerable().Reverse())
            {
                string original = Within(backup, Path.GetRelativePath(root, c.Asset));
                File.Copy(original, c.Asset, overwrite: true);
            }
            log("Deployment failed; installed assets restored from backups.");
            throw;
        }
        log("Backups: " + backup);
        return new Summary(candidates.Count, installed.Count, candidates.Count - installed.Count);
    }
}
