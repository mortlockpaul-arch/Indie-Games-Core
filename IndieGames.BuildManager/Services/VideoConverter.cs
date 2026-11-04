using System.IO;

namespace IndieGames.BuildManager.Services;

public static class VideoConverter
{
    public sealed record Result(string Asset, string Status, string Detail);
    public static async Task<IReadOnlyList<Result>> ConvertAsync(string contentDirectory, string ffmpeg,
        Action<string> log, CancellationToken token)
    {
        if (!File.Exists(ffmpeg)) throw new FileNotFoundException("FFmpeg not found.", ffmpeg);
        var results = new List<Result>();
        var formats = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".wmv", ".avi", ".mp4", ".mov", ".m4v" };
        var options = new EnumerationOptions { RecurseSubdirectories = true, AttributesToSkip = FileAttributes.ReparsePoint, IgnoreInaccessible = true };
        foreach (string source in Directory.EnumerateFiles(contentDirectory, "*", options).Where(p => formats.Contains(Path.GetExtension(p))).Order(StringComparer.OrdinalIgnoreCase))
        {
            token.ThrowIfCancellationRequested();
            string target = Path.ChangeExtension(source, ".ogv");
            string asset = Path.GetRelativePath(contentDirectory, source);
            if (File.Exists(target))
            {
                results.Add(new(asset, "Skipped", "Existing OGV preserved."));
                log($"Skipped: {asset} — existing OGV preserved.");
                continue;
            }
            string temporary = target + ".converting-" + Guid.NewGuid().ToString("N") + ".ogv";
            try
            {
                log("Converting video: " + asset);
                int exit = await ProcessRunner.RunAsync(ffmpeg,
                    ["-hide_banner", "-loglevel", "warning", "-nostdin", "-n", "-i", source,
                     "-map", "0:v:0", "-map", "0:a:0?", "-c:v", "libtheora", "-q:v", "7", "-pix_fmt", "yuv420p",
                     "-c:a", "libvorbis", "-q:a", "5", "-f", "ogg", temporary], contentDirectory, log, token);
                token.ThrowIfCancellationRequested();
                if (exit != 0 || !File.Exists(temporary) || new FileInfo(temporary).Length < 4)
                    throw new InvalidDataException($"FFmpeg video conversion failed (exit {exit}).");
                using (var stream = File.OpenRead(temporary))
                {
                    byte[] header = new byte[4]; stream.ReadExactly(header);
                    if (!header.AsSpan().SequenceEqual("OggS"u8)) throw new InvalidDataException("FFmpeg did not produce an Ogg container.");
                }
                File.Move(temporary, target, overwrite: false);
                results.Add(new(asset, "Converted", "Theora/Vorbis OGV created; original retained."));
                log("Converted: " + asset + " → " + Path.GetFileName(target));
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception error) when (error is IOException or InvalidDataException or InvalidOperationException or UnauthorizedAccessException)
            {
                results.Add(new(asset, "Failed", error.Message)); log("Failed: " + asset + " — " + error.Message);
            }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
        }
        return results;
    }
}
