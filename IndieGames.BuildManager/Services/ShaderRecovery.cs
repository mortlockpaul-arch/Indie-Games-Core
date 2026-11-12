using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

/// <summary>Recovers effect source and compiles it for review; never replaces game assets.</summary>
public static class ShaderRecovery
{
    public static async Task RunAsync(string gameFolder, string decompilerProject, bool recurse,
        bool overwrite, Action<string> log, CancellationToken token)
    {
        string content = Path.Combine(gameFolder, "Content");
        if (!Directory.Exists(content)) { log("SKIP: Content folder missing: " + gameFolder); return; }
        if (!File.Exists(decompilerProject)) throw new FileNotFoundException("XnaEffectDecompiler project missing", decompilerProject);
        string compiler = KnownShaderRepair.FindCompiler();
        if (!File.Exists(compiler)) throw new FileNotFoundException("Windows SDK fxc.exe not found", compiler);
        string outputRoot = Path.Combine(gameFolder, "build", "shader-recovery");
        Directory.CreateDirectory(outputRoot);
        var options = new EnumerationOptions { RecurseSubdirectories = recurse, IgnoreInaccessible = true,
            AttributesToSkip = FileAttributes.ReparsePoint };
        int recovered = 0, compiled = 0, failed = 0, skipped = 0;
        foreach (string asset in Directory.EnumerateFiles(content, "*.xnb", options))
        {
            token.ThrowIfCancellationRequested();
            // Preserve the Content-relative hierarchy to avoid filename collisions.
            string relative = Path.GetRelativePath(content, asset);
            string fx = Path.Combine(outputRoot, "recovered-hlsl", Path.ChangeExtension(relative, ".fx"));
            string fxb = Path.Combine(outputRoot, "compiled-fna", Path.ChangeExtension(relative, ".fxb"));
            if (!overwrite && File.Exists(fx)) { skipped++; continue; }
            Directory.CreateDirectory(Path.GetDirectoryName(fx)!);
            Directory.CreateDirectory(Path.GetDirectoryName(fxb)!);
            string staged = fx + ".tmp-" + Guid.NewGuid().ToString("N") + ".fx";
            try
            {
                // Non-effect XNBs are expected to fail; they are never modified.
                var args = new List<string> { "run", "--project", decompilerProject, "--", asset, staged, "--force" };
                int code = await ProcessRunner.RunAsync("dotnet", args, Path.GetDirectoryName(decompilerProject)!, log, token);
                if (code != 0 || !File.Exists(staged)) { failed++; continue; }
                // Preserve original recovered source if it exists; never overwrite it implicitly.
                File.Move(staged, fx, overwrite: overwrite);
                string stagedMetadata = Path.ChangeExtension(staged, "metadata.json");
                if (File.Exists(stagedMetadata)) File.Move(stagedMetadata, Path.ChangeExtension(fx, "metadata.json"), overwrite: overwrite);
                string stagedOriginal = Path.ChangeExtension(staged, "original.fx");
                if (File.Exists(stagedOriginal)) File.Move(stagedOriginal, Path.ChangeExtension(fx, "original.fx"), overwrite: overwrite);
                recovered++;
                // Compilation output remains in a review directory; not an installed XNB.
                string temporaryFxb = fxb + ".tmp-" + Guid.NewGuid().ToString("N");
                try
                {
                    int compileCode = await ProcessRunner.RunAsync(compiler,
                        ["/nologo", "/T", "fx_2_0", "/O3", "/Fo", temporaryFxb, fx], outputRoot, log, token);
                    if (compileCode == 0 && File.Exists(temporaryFxb))
                    {
                        File.Move(temporaryFxb, fxb, overwrite: overwrite);
                        compiled++;
                        string sha = Convert.ToHexString(SHA256.HashData(await File.ReadAllBytesAsync(asset, token)));
                        string report = fxb + ".json";
                        await File.WriteAllTextAsync(report, JsonSerializer.Serialize(new { sourceXnb = relative, sourceSha256 = sha,
                            recoveredFx = Path.GetRelativePath(outputRoot, fx), compiledFxb = Path.GetRelativePath(outputRoot, fxb),
                            deployed = false }, new JsonSerializerOptions { WriteIndented = true }), token);
                    }
                    else failed++;
                }
                finally { if (File.Exists(temporaryFxb)) File.Delete(temporaryFxb); }
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception e) { failed++; log($"FAILED: {relative}: {e.Message}"); }
            finally
            {
                foreach (string leftover in new[] { staged, Path.ChangeExtension(staged, "metadata.json"), Path.ChangeExtension(staged, "original.fx") })
                    if (File.Exists(leftover)) File.Delete(leftover);
            }
        }
        log($"Shader recovery: {recovered} recovered, {compiled} compiled, {skipped} skipped, {failed} failed/non-effect XNBs. No assets deployed. Review: {outputRoot}");
    }
}
