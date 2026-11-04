
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

public static class GameLogger
{
    private static readonly object LockObject = new object();

    private static bool _initialized;
    private static string? _logFile;

    public static string? LogFile => _logFile;

    public static void Initialize(string? gameName = null, string? solutionRoot = null)
    {
        if (_initialized)
            return;

        _initialized = true;

        string? name = gameName;

        if (string.IsNullOrWhiteSpace(name))
        {
            name = Assembly.GetEntryAssembly()?.GetName().Name;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            name = Path.GetFileNameWithoutExtension(
                Environment.ProcessPath ?? "UnknownGame"
            );
        }

        name = SanitizeFileName(name);

        string? root = solutionRoot ?? FindSolutionRoot();

        string logDirectory = root != null
            ? Path.Combine(root, "logs")
            : Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData
                ),
                "Indie-Games",
                "Logs"
            );

        Directory.CreateDirectory(logDirectory);

        _logFile = Path.Combine(logDirectory, $"{name}.log");

        Write("========================================");
        Write($"Game: {name}");
        Write($"Started: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Write($"Runtime: {Environment.Version}");
        Write($"64-bit process: {Environment.Is64BitProcess}");
        Write($"Process: {Environment.ProcessPath}");
        Write($"Base directory: {AppContext.BaseDirectory}");
        Write($"Working directory: {Environment.CurrentDirectory}");
        Write($"Solution root: {root ?? "(not found)"}");

        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            Write("UNHANDLED EXCEPTION:");
            Write(args.ExceptionObject?.ToString() ?? "Unknown exception");
        };

        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            Write("UNOBSERVED TASK EXCEPTION:");
            Write(args.Exception.ToString());
        };

        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        {
            Write($"ASSEMBLY RESOLVE: {args.Name}");
            Write($"Requested by: {args.RequestingAssembly?.FullName}");
            return null;
        };

        AssemblyLoadContext.Default.Resolving += (context, assemblyName) =>
        {
            Write($"ASSEMBLY RESOLVING: {assemblyName.FullName}");
            return null;
        };

        AppDomain.CurrentDomain.FirstChanceException += (sender, args) =>
        {
            if (args.Exception is FileNotFoundException ||
                args.Exception is FileLoadException ||
                args.Exception is BadImageFormatException ||
                args.Exception is DllNotFoundException)
            {
                Write($"FIRST CHANCE EXCEPTION: {args.Exception}");
            }
        };

        WriteAssembly("Steamworks.NET.dll");
        WriteAssembly("FNA.Steamworks.dll");
        WriteAssembly("FNA.dll");

        Write("Logger initialized");
    }

    private static string? FindSolutionRoot()
    {
        DirectoryInfo? directory = new DirectoryInfo(
            AppContext.BaseDirectory
        );

        while (directory != null)
        {
            bool hasArchive = Directory.Exists(
                Path.Combine(directory.FullName, "indie-game-archive")
            );

            bool hasFNA = Directory.Exists(
                Path.Combine(directory.FullName, "FNA")
            );

            if (hasArchive && hasFNA)
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        return null;
    }

    private static string SanitizeFileName(string name)
    {
        foreach (char character in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(character, '_');
        }

        return name;
    }

    private static void WriteAssembly(string name)
    {
        string path = Path.Combine(
            AppContext.BaseDirectory,
            name
        );

        if (!File.Exists(path))
        {
            Write($"MISSING DLL: {path}");
            return;
        }

        try
        {
            AssemblyName assembly = AssemblyName.GetAssemblyName(path);

            Write($"DLL: {name}");
            Write($"  Version: {assembly.Version}");
            Write($"  Path: {path}");
        }
        catch (Exception ex)
        {
            Write($"DLL INSPECTION FAILED: {name}: {ex}");
        }
    }

    public static void Write(string message)
    {
        try
        {
            string line =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}";

            lock (LockObject)
            {
                if (!string.IsNullOrEmpty(_logFile))
                {
                    File.AppendAllText(
                        _logFile,
                        line + Environment.NewLine
                    );
                }
            }

            System.Diagnostics.Debug.WriteLine(line);
        }
        catch
        {
            // Never allow logging to crash the game.
        }
    }

    public static void Exception(Exception exception)
    {
        Write("EXCEPTION:");
        Write(exception.ToString());
    }
}
