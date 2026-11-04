using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;

namespace IndieGames.BuildManager.Services;

public static class ShaderDemoLauncher
{
    static readonly SemaphoreSlim Gate = new(1, 1);
    static Process? _active;
    [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr handle);
    [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr handle, int command);
    public static async Task OpenAsync(Action<string> log)
    {
        await Gate.WaitAsync();
        try
        {
            if (_active is { HasExited: false })
            {
                ShowWindow(_active.MainWindowHandle, 9); SetForegroundWindow(_active.MainWindowHandle); return;
            }
            string root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IndieGames.BuildManager", "ShaderDemos");
            string folder = Path.Combine(root, Guid.NewGuid().ToString("N"));
            try { await Task.Run(() =>
            {
                using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("IndieGames.BuildManager.ShaderDemoBrowser.zip")
                    ?? throw new FileNotFoundException("The shader demo package is missing. Build and package Tools/ShaderDemoBrowser first.");
                using var archive = new ZipArchive(resource);
                string prefix = Path.GetFullPath(folder) + Path.DirectorySeparatorChar;
                foreach (var entry in archive.Entries)
                {
                    string target = Path.GetFullPath(Path.Combine(folder, entry.FullName));
                    if (!target.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Invalid demo package path.");
                    if (entry.FullName.EndsWith('/')) { Directory.CreateDirectory(target); continue; }
                    Directory.CreateDirectory(Path.GetDirectoryName(target)!); entry.ExtractToFile(target);
                }
            }); }
            catch { Cleanup(folder, root); throw; }
            try
            {
                var process = Process.Start(new ProcessStartInfo(Path.Combine(folder, "ShaderDemoBrowser.exe")) { WorkingDirectory = folder, UseShellExecute = false, CreateNoWindow = true })
                    ?? throw new InvalidOperationException("The demo browser did not start.");
                _active = process;
                _ = CleanupAsync(process, folder, root);
                log("Shader Demo Browser opened. Select an effect; Tab changes technique, Space pauses animation.");
            }
            catch { Cleanup(folder, root); throw; }
        }
        finally { Gate.Release(); }
    }
    static async Task CleanupAsync(Process process, string folder, string root)
    {
        try { await process.WaitForExitAsync(); }
        catch (InvalidOperationException) { }
        finally { Cleanup(folder, root); if (ReferenceEquals(_active, process)) _active = null; process.Dispose(); }
    }
    static void Cleanup(string folder, string root)
    {
        string full = Path.GetFullPath(folder);
        string prefix = Path.TrimEndingDirectorySeparator(Path.GetFullPath(root)) + Path.DirectorySeparatorChar;
        if (!full.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) return;
        try { if (Directory.Exists(full)) Directory.Delete(full, recursive: true); }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }
}
