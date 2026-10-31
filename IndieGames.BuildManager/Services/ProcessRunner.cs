using System.Diagnostics;
using System.IO;
namespace IndieGames.BuildManager.Services;
public static class ProcessRunner
{
    public static async Task<int> RunAsync(string exe, IEnumerable<string> args, string workingDir,
        Action<string> output, CancellationToken token)
    {
        using var process = new Process();
        process.StartInfo = new ProcessStartInfo(exe) {
            WorkingDirectory = workingDir,
            RedirectStandardOutput = true, RedirectStandardError = true,
            UseShellExecute = false, CreateNoWindow = true
        };
        foreach (var arg in args) process.StartInfo.ArgumentList.Add(arg);
        process.Start();
        async Task PumpAsync(StreamReader reader) {
            while (await reader.ReadLineAsync() is { } line) output(line);
        }
        using var registration = token.Register(() => {
            try { if (!process.HasExited) process.Kill(entireProcessTree: true); } catch { }
        });
        await Task.WhenAll(PumpAsync(process.StandardOutput), PumpAsync(process.StandardError),
            process.WaitForExitAsync(token));
        return process.ExitCode;
    }
}
