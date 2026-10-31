param(
    [string]$Root = 'C:\source\Indie-Games'
)

$ErrorActionPreference = 'Stop'
$Root = [IO.Path]::GetFullPath($Root)
$archive = Join-Path $Root 'indie-game-archive'
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
'@

Set-Content -LiteralPath $logger -Value $source -Encoding UTF8

# Add an independent imported targets file; don't overwrite the existing targets.
$importFile = Join-Path $archive 'GameLogger.targets'
$xml = @'
<Project>
  <ItemGroup Condition="('$(OutputType)' == 'Exe' Or '$(OutputType)' == 'WinExe') And '$(TargetFramework)' == 'net9.0-windows' And Exists('$(MSBuildThisFileDirectory)..\Shared\GameLogger.cs')">
    <Compile Include="$(MSBuildThisFileDirectory)..\Shared\GameLogger.cs" Link="Shared\GameLogger.cs" Condition="!Exists('$(MSBuildProjectDirectory)\GameLogger.cs')" />
  </ItemGroup>
</Project>
'@
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
Write-Host "Rebuild an SDK-style net9.0 game project to enable logging." -ForegroundColor Cyan
