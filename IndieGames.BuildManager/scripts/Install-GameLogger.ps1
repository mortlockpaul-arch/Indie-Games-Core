param(
    [string]$Root = 'C:\source\Indie-Games-Core',
    [string]$Archive = ''
)

$ErrorActionPreference = 'Stop'
$Root = [IO.Path]::GetFullPath($Root)
if ([string]::IsNullOrWhiteSpace($Archive)) {
    $candidate = Join-Path $Root 'indie-game-archive'
    $Archive = if (Test-Path -LiteralPath $candidate) { $candidate } else { 'D:\indie-game-archive' }
}
$archive = [IO.Path]::GetFullPath($Archive)
$shared = Join-Path $Root 'Shared'
$logger = Join-Path $shared 'GameLogger.cs'
$targets = Join-Path $archive 'Directory.Build.targets'

if (-not (Test-Path -LiteralPath $archive -PathType Container)) {
    throw "Archive directory not found: $archive"
}
New-Item -ItemType Directory -Path $shared -Force | Out-Null

$source = @'
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Loader;
using System.Threading.Tasks;
using System.Text;

internal static class GameLogger
{
    private static readonly object Sync = new object();
    private static string? _path;
    private static bool _captured, _consoleRedirected;
    [ThreadStatic] private static bool _writing;

    [ModuleInitializer]
    internal static void Initialize()
    {
        try
        {
            string gameName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "UnknownGame");
            foreach (char c in Path.GetInvalidFileNameChars()) gameName = gameName.Replace(c, '_');
            string? logDir = Environment.GetEnvironmentVariable("INDIE_GAME_LOG_DIRECTORY");
            if (string.IsNullOrWhiteSpace(logDir)) logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IndieGames.BuildManager", "game-logs");
            Directory.CreateDirectory(logDir);
            _path = Path.Combine(logDir, gameName + ".log");
            _captured = Environment.GetEnvironmentVariable("INDIE_GAME_CAPTURED") == "1";
            if (!_captured)
            {
                Console.SetOut(TextWriter.Synchronized(new ConsoleLogWriter(Console.Out,"stdout")));
                Console.SetError(TextWriter.Synchronized(new ConsoleLogWriter(Console.Error,"stderr")));
                _consoleRedirected = true;
            }
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

    internal static void Write(string message)
    {
        if (_writing) return;
        if (_captured || _consoleRedirected)
        {
            try { Console.Error.WriteLine("[GameLogger] " + message); } catch { }
            return;
        }
        Append(message);
    }

    private static void Append(string message)
    {
        if (_writing) return;
        _writing = true;
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
        finally { _writing = false; }
    }

    private sealed class ConsoleLogWriter : TextWriter
    {
        private readonly TextWriter _original;
        private readonly string _channel;
        private readonly StringBuilder _pending = new StringBuilder();
        internal ConsoleLogWriter(TextWriter original, string channel) { _original=original; _channel=channel; }
        public override Encoding Encoding { get { return _original.Encoding; } }
        public override void Write(char value) { Write(value.ToString()); }
        public override void Write(string? value)
        {
            try { _original.Write(value); } catch (IOException) { } catch (ObjectDisposedException) { }
            Capture(value ?? "");
        }
        public override void WriteLine(string? value)
        {
            try { _original.WriteLine(value); } catch (IOException) { } catch (ObjectDisposedException) { }
            Capture((value ?? "") + Environment.NewLine);
        }
        public override void WriteLine() { WriteLine(""); }
        private void Capture(string text)
        {
            lock (Sync)
            {
                foreach (char value in text)
                {
                    if (value == '\n') { Append("[" + _channel + "] " + _pending.ToString().TrimEnd('\r')); _pending.Clear(); }
                    else { _pending.Append(value); if (_pending.Length >= 32768) { Append("[" + _channel + "] " + _pending); _pending.Clear(); } }
                }
            }
        }
    }
}
'@

Set-Content -LiteralPath $logger -Value $source -Encoding UTF8

# Add an independent imported targets file; don't overwrite the existing targets.
$importFile = Join-Path $archive 'GameLogger.targets'
$xml = @'
<Project>
  <ItemGroup Condition="('$(OutputType)' == 'Exe' Or '$(OutputType)' == 'WinExe') And '$(TargetFrameworkIdentifier)' == '.NETCoreApp' And $([System.Text.RegularExpressions.Regex]::IsMatch('$(TargetFramework)', '^net([6-9]|[1-9][0-9]+)[.]')) And Exists('__LOGGER_PATH__')">
    <Compile Include="__LOGGER_PATH__" Link="Shared\GameLogger.cs" Condition="!Exists('$(MSBuildProjectDirectory)\GameLogger.cs')" />
  </ItemGroup>
</Project>
'@
$xml = $xml.Replace('__LOGGER_PATH__', [System.Security.SecurityElement]::Escape($logger))
Set-Content -LiteralPath $importFile -Value $xml -Encoding UTF8

# Directory.Build.targets may already exist and contain native DLL copy targets.
# Insert an import without replacing anything, idempotently.
if (-not (Test-Path -LiteralPath $targets)) {
    Set-Content -LiteralPath $targets -Value '<Project></Project>' -Encoding UTF8
}
$content = Get-Content -LiteralPath $targets -Raw
if ($content -notmatch 'GameLogger\.targets') {
    $updated = [regex]::Replace($content, '</Project>\s*$', '  <Import Project="$(MSBuildThisFileDirectory)GameLogger.targets" />' + "`r`n" + '</Project>')
    if ($updated -eq $content) { throw "Could not find closing </Project> in $targets" }
    Set-Content -LiteralPath $targets -Value $updated -Encoding UTF8
}

Write-Host "Installed logger: $logger" -ForegroundColor Green
Write-Host "Installed targets: $importFile" -ForegroundColor Green
Write-Host "Updated import in: $targets" -ForegroundColor Green
Write-Host "Rebuild the selected .NET game project to enable console and diagnostic logging." -ForegroundColor Cyan
