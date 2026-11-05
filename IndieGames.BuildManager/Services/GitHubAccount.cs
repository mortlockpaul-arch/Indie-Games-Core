using System.Diagnostics;
using System.IO;

namespace IndieGames.BuildManager.Services;

public static class GitHubAccount
{
    public static string? PublishingToken => Environment.GetEnvironmentVariable("INDIE_GAMES_PUBLISHING_TOKEN")?.Trim();

    public static string? FindCli()
    {
        var folders = new[] { Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "GitHub CLI"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "GitHub CLI") }
            .Concat((Environment.GetEnvironmentVariable("PATH") ?? "").Split(Path.PathSeparator));
        return folders.Where(f => !string.IsNullOrWhiteSpace(f)).Select(f => Path.Combine(f.Trim('"'), "gh.exe")).FirstOrDefault(File.Exists);
    }

    public static async Task InstallAndSignInAsync(CancellationToken token)
    {
        if (FindCli() == null)
        {
            await InteractiveAsync("winget.exe", ["install", "--id", "GitHub.cli", "--exact", "--source", "winget", "--accept-package-agreements", "--accept-source-agreements"], token);
        }
        string cli = FindCli() ?? throw new IOException("GitHub CLI was not found. Install GitHub CLI, then choose Connect GitHub again.");
        await InteractiveAsync(cli, ["auth", "login", "--hostname", "github.com", "--git-protocol", "https", "--web"], token, useSavedAccount: true);
    }

    private static async Task InteractiveAsync(string executable, string[] args, CancellationToken token, bool useSavedAccount = false)
    {
        var info = new ProcessStartInfo(executable) { UseShellExecute = !useSavedAccount };
        if (useSavedAccount) UseSavedAccount(info);
        foreach (string arg in args) info.ArgumentList.Add(arg);
        using var process = Process.Start(info) ?? throw new IOException("Could not open GitHub sign-in.");
        await process.WaitForExitAsync(token);
        if (process.ExitCode != 0) throw new IOException("GitHub setup was not completed. Choose Connect GitHub to try again.");
    }

    private static void UseSavedAccount(ProcessStartInfo info)
    {
        // The Connect GitHub button signs into the CLI credential store.
        info.Environment.Remove("GH_TOKEN");
        info.Environment.Remove("GITHUB_TOKEN");
    }

    public static async Task<string> ReadTokenAsync(CancellationToken token)
    {
        if (PublishingToken is { Length: > 0 } tokenValue) return tokenValue;
        string cli = FindCli() ?? throw new IOException("Choose Connect GitHub before publishing. Browsing and downloading do not require an account.");
        var info = new ProcessStartInfo(cli) { UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true };
        UseSavedAccount(info);
        foreach (string arg in new[] { "auth", "token", "--hostname", "github.com" }) info.ArgumentList.Add(arg);
        using var process = Process.Start(info) ?? throw new IOException("Could not access GitHub sign-in.");
        using var registration = token.Register(() => { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } });
        Task<string> output = process.StandardOutput.ReadToEndAsync(token), errors = process.StandardError.ReadToEndAsync(token);
        await process.WaitForExitAsync(token);
        string value = (await output).Trim(); await errors;
        if (process.ExitCode != 0 || string.IsNullOrEmpty(value)) throw new IOException("GitHub is not connected. Choose Connect GitHub and complete sign-in.");
        // The credential stays in memory; never put it in logs, command arguments or app settings.
        return value;
    }
}
