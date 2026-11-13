using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IndieGames.BuildManager.Services;

public static class VideoConverterImproved
{
    public sealed record Result(string Status, string File, string Message);
    // Change this signature whenever conversion settings change.
    private const string ConversionVersion = "theora-q10-fps-v2";
    private const double FpsTolerance = 0.5;


    public static async Task<List<Result>> ConvertAsync(string content, string ffmpeg, Action<string> log, CancellationToken token)
    {
        var results = new List<Result>();
        if (!Directory.Exists(content)) return results;
        var files = Directory.EnumerateFiles(content, "*", SearchOption.AllDirectories)
            .Where(f => f.EndsWith(".wmv", StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f, StringComparer.OrdinalIgnoreCase).ToArray();
        // Only identical source files are deduplicated. Every destination still gets its own OGV.
        foreach (var file in files)
        {
            token.ThrowIfCancellationRequested();
            string relative = Path.GetRelativePath(content, file);
            string output = Path.ChangeExtension(file, ".ogv");
            // A sidecar records the exact source and OGV hashes for this quality profile.
            // Older outputs (including ones produced by our earlier converters) are upgraded once.
            string stamp = output + ".buildmanager-video.txt";
            string sourceHash;
            using (var stream = File.OpenRead(file))
                sourceHash = Convert.ToHexString(await SHA256.HashDataAsync(stream, token));
            bool exists = File.Exists(output);
            double? sourceFps = await ReadFps(ffmpeg, file, token);
            double? existingFps = exists ? await ReadFps(ffmpeg, output, token) : null;
            bool unusual = sourceFps >= 120;
            double? targetFps = unusual ? 30.0 : sourceFps;
            string expectedStampPrefix = ConversionVersion + "|" + sourceHash + "|";
            if (exists && File.Exists(stamp))
            {
                string recorded = await File.ReadAllTextAsync(stamp, token);
                if (recorded.StartsWith(expectedStampPrefix, StringComparison.Ordinal) &&
                    recorded.Length > expectedStampPrefix.Length &&
                    targetFps.HasValue && existingFps.HasValue &&
                    Math.Abs(existingFps.Value - targetFps.Value) < FpsTolerance)
                {
                    using var existingStream = File.OpenRead(output);
                    string actualHash = Convert.ToHexString(await SHA256.HashDataAsync(existingStream, token));
                    if (string.Equals(recorded.Trim(), expectedStampPrefix + actualHash, StringComparison.Ordinal))
                    {
                        log($"Skipped: {relative} — already converted at maximum quality ({existingFps:0.###} fps).");
                        results.Add(new("Skipped", relative, "Already converted with current quality profile"));
                        continue;
                    }
                }
            }
            if (exists)
                log($"Upgrading existing OGV: {relative} (current fps: {existingFps?.ToString("0.###") ?? "unknown"}; original will be backed up).");
            string temp = Path.Combine(Path.GetDirectoryName(output)!, "." + Path.GetFileNameWithoutExtension(output) + "." + Guid.NewGuid().ToString("N") + ".ogv");
            try
            {
                log($"Converting video: {relative}");
                // Preserve native source fps except for known Xbox WMV 240fps timing anomalies.
                // A future XNB metadata reader can replace this heuristic.
                if (unusual) log($"High source frame rate detected ({sourceFps:0.###}); using 30 fps output.");
                var common = new List<string> { "-hide_banner", "-nostdin", "-y", "-fflags", "+genpts", "-i", file,
                    "-map", "0:v:0", "-map", "0:a:0?", "-sn", "-dn", "-c:v", "libtheora", "-q:v", "10", "-pix_fmt", "yuv420p" };
                if (unusual) common.AddRange(new[] { "-vf", "fps=30", "-fps_mode", "cfr" });
                else common.AddRange(new[] { "-vsync", "vfr" });
                common.AddRange(new[] { "-c:a", "libvorbis", "-q:a", "6", "-max_muxing_queue_size", "4096", "-f", "ogg", temp });
                var first = await RunWithCode(ffmpeg, common, token);
                if (first.Code != 0 || !Valid(temp))
                {
                    log($"Retrying {relative} with video only (audio/interleaving fallback).");
                    if (File.Exists(temp)) File.Delete(temp);
                    var retry = new List<string> { "-hide_banner", "-nostdin", "-y", "-fflags", "+genpts", "-i", file,
                        "-map", "0:v:0", "-an", "-sn", "-dn", "-c:v", "libtheora", "-q:v", "10", "-pix_fmt", "yuv420p" };
                    if (unusual) retry.AddRange(new[] { "-vf", "fps=30", "-fps_mode", "cfr" });
                    else retry.AddRange(new[] { "-vsync", "vfr" });
                    retry.AddRange(new[] { "-f", "ogg", temp });
                    var second = await RunWithCode(ffmpeg, retry, token);
                    if (second.Code != 0 || !Valid(temp)) throw new InvalidOperationException("FFmpeg failed: " + Tail(second.Output));
                    log($"Warning: {relative} converted without audio; check whether soundtrack is needed.");
                }
                // Confirm the container is readable before committing the destination.
                var validation = await RunWithCode(ffmpeg, new[] { "-v", "error", "-i", temp, "-map", "0:v:0", "-frames:v", "1", "-f", "null", "-" }, token);
                if (validation.Code != 0) throw new InvalidOperationException("Output validation failed: " + Tail(validation.Output));
                double? actualFps = await ReadFps(ffmpeg, temp, token);
                if (actualFps is null) throw new InvalidOperationException("Could not verify encoded OGV frame rate.");
                if (targetFps.HasValue && Math.Abs(actualFps.Value - targetFps.Value) >= FpsTolerance)
                    throw new InvalidOperationException($"Encoded OGV is {actualFps:0.###} fps, expected {targetFps:0.###} fps. Original preserved.");
                var originalDimensions = await ReadDimensions(ffmpeg, file, token);
                var outputDimensions = await ReadDimensions(ffmpeg, temp, token);
                if (originalDimensions is null || outputDimensions is null || originalDimensions != outputDimensions)
                    throw new InvalidOperationException($"Video resolution mismatch or unknown: source={originalDimensions}, output={outputDimensions}. Original preserved.");
                log($"Verified output: {actualFps:0.###} fps.");
                if (File.Exists(output))
                {
                    // Do not replace an existing file unless a unique backup was safely created.
                    string backup = output + ".before-fps-repair-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + ".bak";
                    File.Move(output, backup);
                    try { File.Move(temp, output); }
                    catch { File.Move(backup, output); throw; }
                    log("Original OGV backed up: " + backup);
                }
                else File.Move(temp, output);
                using (var stream = File.OpenRead(output))
                {
                    string outputHash = Convert.ToHexString(await SHA256.HashDataAsync(stream, token));
                    await File.WriteAllTextAsync(stamp, expectedStampPrefix + outputHash, token);
                }
                log($"Converted at maximum quality: {relative}");
                results.Add(new("Converted", relative, "OGV created"));
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                log($"Failed: {relative} — {ex.Message}");
                results.Add(new("Failed", relative, ex.Message));
            }
            finally { if (File.Exists(temp)) File.Delete(temp); }
        }
        return results;
    }

    // Synchronize the converted archive videos into an already installed game.
    // Never create an installation or overwrite files that are not sourced from WMV.
    public static async Task<(int Updated, int Current, int Failed)> DeployAsync(
        string content, string installedGameFolder, Action<string> log, CancellationToken token)
    {
        int updated = 0, current = 0, failed = 0;
        if (!Directory.Exists(installedGameFolder))
        {
            log("Deployment skipped: game is not installed at " + installedGameFolder);
            return (0, 0, 0);
        }
        foreach (string wmv in Directory.EnumerateFiles(content, "*", SearchOption.AllDirectories)
                     .Where(f => f.EndsWith(".wmv", StringComparison.OrdinalIgnoreCase)))
        {
            token.ThrowIfCancellationRequested();
            string source = Path.ChangeExtension(wmv, ".ogv");
            if (!File.Exists(source)) continue;
            string relative = Path.GetRelativePath(content, source);
            string destination = Path.GetFullPath(Path.Combine(installedGameFolder, "Content", relative));
            string allowedRoot = Path.TrimEndingDirectorySeparator(Path.GetFullPath(
                Path.Combine(installedGameFolder, "Content"))) + Path.DirectorySeparatorChar;
            if (!destination.StartsWith(allowedRoot, StringComparison.OrdinalIgnoreCase))
            {
                log("Deployment rejected unsafe relative path: " + relative);
                failed++;
                continue;
            }
            // Only update already deployed content; don't invent game layouts.
            if (!File.Exists(destination))
            {
                log("Deployment skipped (no installed counterpart): " + relative);
                continue;
            }
            try
            {
                string sourceHash;
                await using (var stream = File.OpenRead(source))
                    sourceHash = Convert.ToHexString(await SHA256.HashDataAsync(stream, token));
                string destinationHash;
                await using (var stream = File.OpenRead(destination))
                    destinationHash = Convert.ToHexString(await SHA256.HashDataAsync(stream, token));
                if (sourceHash == destinationHash) { current++; continue; }

                string backup = destination + ".before-video-deploy-" +
                    DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff") + "-" +
                    Guid.NewGuid().ToString("N")[..8] + ".bak";
                string temp = destination + "." + Guid.NewGuid().ToString("N") + ".tmp";
                try
                {
                    File.Copy(source, temp);
                    await using (var stream = File.OpenRead(temp))
                    {
                        string copiedHash = Convert.ToHexString(await SHA256.HashDataAsync(stream, token));
                        if (copiedHash != sourceHash) throw new IOException("Temporary deployment hash mismatch");
                    }
                    // Atomic replacement on the same volume, preserving the previous video.
                    File.Replace(temp, destination, backup);
                    updated++;
                    log("Deployed updated video: " + relative + " (backup: " + backup + ")");
                }
                finally { if (File.Exists(temp)) File.Delete(temp); }
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                failed++;
                log("Video deployment failed: " + relative + " — " + ex.Message);
            }
        }
        return (updated, current, failed);
    }

    private static async Task<double?> ReadFps(string ffmpeg, string file, CancellationToken token)
    {
        var result = await RunWithCode(ffmpeg, new[] { "-hide_banner", "-i", file }, token);
        var videoLine = result.Output.Split('\n').FirstOrDefault(l => l.Contains("Video:", StringComparison.OrdinalIgnoreCase)) ?? "";
        var match = Regex.Match(videoLine, @"(?<!\d)(\d+(?:\.\d+)?)\s+fps\b");
        if (!match.Success) match = Regex.Match(videoLine, @"(?<!\d)(\d+(?:\.\d+)?)\s+tbr\b");
        return match.Success && double.TryParse(match.Groups[1].Value, System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture, out double fps) ? fps : null;
    }

    private static async Task<string?> ReadDimensions(string ffmpeg, string file, CancellationToken token)
    {
        var result = await RunWithCode(ffmpeg, new[] { "-hide_banner", "-i", file }, token);
        var videoLine = result.Output.Split('\n').FirstOrDefault(l => l.Contains("Video:", StringComparison.OrdinalIgnoreCase)) ?? "";
        var match = Regex.Match(videoLine, @"(?<!\d)(\d{2,5})x(\d{2,5})(?!\d)");
        return match.Success ? match.Groups[1].Value + "x" + match.Groups[2].Value : null;
    }

    private static bool Valid(string file) => File.Exists(file) && new FileInfo(file).Length > 1024;
    private static string Tail(string s) => s.Length > 800 ? s[^800..] : s;
    private static async Task<string> Run(string exe, IEnumerable<string> args, CancellationToken token)
        => (await RunWithCode(exe, args, token)).Output;
    private static async Task<(int Code, string Output)> RunWithCode(string exe, IEnumerable<string> args, CancellationToken token)
    {
        using var process = new Process { StartInfo = new ProcessStartInfo(exe) { UseShellExecute = false, RedirectStandardError = true, RedirectStandardOutput = true, CreateNoWindow = true } };
        foreach (var arg in args) process.StartInfo.ArgumentList.Add(arg);
        process.Start();
        using var registration = token.Register(() => { try { if (!process.HasExited) process.Kill(entireProcessTree: true); } catch { } });
        var stderr = process.StandardError.ReadToEndAsync();
        var stdout = process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync(token);
        return (process.ExitCode, (await stderr) + (await stdout));
    }
}
