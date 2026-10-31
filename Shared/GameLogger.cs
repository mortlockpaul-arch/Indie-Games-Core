using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Loader;
using System.Threading.Tasks;

internal static class GameLogger
{
    private static readonly object Sync = new object();
    private static string _path;

    [ModuleInitializer]
    internal static void Initialize()
    {
        try
        {
            string gameName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "UnknownGame");
            foreach (char c in Path.GetInvalidFileNameChars()) gameName = gameName.Replace(c, '_');
            string root = FindRoot();
            string logDir = root == null
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Indie-Games", "logs")
                : Path.Combine(root, "logs");
            Directory.CreateDirectory(logDir);
            _path = Path.Combine(logDir, gameName + ".log");
            Write("===== Game started =====");
            Write("Executable: " + Environment.ProcessPath);
            Write("Runtime: " + Environment.Version);
            Write("Architecture: " + (Environment.Is64BitProcess ? "x64" : "x86"));
            Write("Base directory: " + AppContext.BaseDirectory);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                Write("UNHANDLED: " + args.ExceptionObject);
            TaskScheduler.UnobservedTaskException += (sender, args) =>
                Write("UNOBSERVED TASK: " + args.Exception);
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                Write("ASSEMBLY RESOLVE: " + args.Name + " requested by " + args.RequestingAssembly);
                return null;
            };
            AssemblyLoadContext.Default.Resolving += (context, name) =>
            {
                Write("ASSEMBLY RESOLVING: " + name.FullName);
                return null;
            };
            AppDomain.CurrentDomain.FirstChanceException += (sender, args) =>
            {
                if (args.Exception is FileNotFoundException ||
                    args.Exception is FileLoadException ||
                    args.Exception is BadImageFormatException ||
                    args.Exception is DllNotFoundException)
                    Write("FIRST CHANCE: " + args.Exception);
            };
            foreach (string dll in new[] { "Steamworks.NET.dll", "FNA.Steamworks.dll", "FNA.dll" })
            {
                string path = Path.Combine(AppContext.BaseDirectory, dll);
                Write(File.Exists(path) ? "DLL PRESENT: " + path : "DLL MISSING: " + path);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("GameLogger init failed: " + ex);
        }
    }

    private static string FindRoot()
    {
        DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (Directory.Exists(Path.Combine(dir.FullName, "indie-game-archive")))
                return dir.FullName;
            dir = dir.Parent;
        }
        return null;
    }

    internal static void Write(string message)
    {
        try
        {
            string line = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + message;
            lock (Sync)
            {
                if (_path != null) File.AppendAllText(_path, line + Environment.NewLine);
            }
            System.Diagnostics.Debug.WriteLine(line);
        }
        catch { /* Diagnostics must not crash the game. */ }
    }
}
