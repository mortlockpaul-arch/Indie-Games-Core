using IndieGames.BuildManager.Models;
using IndieGames.BuildManager.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Text;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Mono.Cecil;
using AssemblyDefinition = Mono.Cecil.AssemblyDefinition;

namespace IndieGames.BuildManager;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<GameProject> _projects = [];
    private CancellationTokenSource? _cts;
    private CancellationTokenSource? _scanCts;
    private TorrentWindow? _torrentWindow;
    private readonly LibraryStorage _storage = LibraryStorage.Load();
    private bool _torrentShutdown;
    private bool _architectureReady, _updatingArchitecture;
    private readonly ConcurrentQueue<string> _pendingLog = new();
    private readonly DispatcherTimer _logTimer;
    private readonly DispatcherTimer _membershipTimer;
    private string? _membershipStamp, _membershipError;
    private bool _checkingMembership, _membershipPending;
    public MainWindow()
    {
        InitializeComponent();
        _logTimer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher) { Interval = TimeSpan.FromMilliseconds(75) };
        _logTimer.Tick += (_, _) => FlushLog();
        _logTimer.Start();
        _membershipTimer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher) { Interval=TimeSpan.FromSeconds(2) };
        _membershipTimer.Tick += async (_,_) => await RefreshSolutionMembershipAsync();
        _membershipTimer.Start();
        Closed += (_, _) => { _logTimer.Stop(); _membershipTimer.Stop(); };
        _architectureReady = true;
        Title=AppVersion.WindowTitle(Title);
        Closing += MainWindow_Closing;
        RootBox.Text = _storage.Archive;
        UpdateSolutionLabel();
        ProjectsGrid.ItemsSource = _projects;
        AudioFfmpegBox.Text = Path.Combine(AppContext.BaseDirectory, "tools", "ffmpeg.exe");
        WaveBankDecoderBox.Text = Path.Combine(AppContext.BaseDirectory, "tools", "vgmstream", "vgmstream-cli.exe");
        Loaded += (_,_) => Scan();
    }

public static void ScanXnaReferences(string folder)
{
    foreach (string file in Directory.GetFiles(folder, "*.dll"))
    {
        try
        {
            using var assembly = AssemblyDefinition.ReadAssembly(file);

            var references = assembly.MainModule.AssemblyReferences
                .Where(r => r.Name.StartsWith(
                    "Microsoft.Xna.Framework",
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (references.Count == 0)
                continue;

            Console.WriteLine($"\n{Path.GetFileName(file)}");

            foreach (var reference in references)
            {
                Console.WriteLine(
                    $"  {reference.Name}, Version={reference.Version}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Failed to inspect {Path.GetFileName(file)}: {ex.Message}");
        }
    }
}
    private void Log(string line) => _pendingLog.Enqueue(line);
    private void FlushLog()
    {
        var text = new StringBuilder();
        for (int count = 0; count < 200 && text.Length < 32768 && _pendingLog.TryDequeue(out string? line); count++)
            text.AppendLine(line);
        if (text.Length == 0) return;
        LogBox.AppendText(text.ToString());
        LogBox.ScrollToEnd();
    }
    private async void Scan()
    {
        _scanCts?.Cancel();
        using var scan=new CancellationTokenSource(); _scanCts=scan;
        string root=RootBox.Text.Trim();
        _projects.Clear();
        FolderScanProgress.IsIndeterminate=true; FolderScanProgress.Value=0; CancelScanButton.IsEnabled=true;
        FolderScanStatus.Text="Finding Folders…";
        var progress=new Progress<ProjectScanProgress>(p=> {
            if (_scanCts != scan || scan.IsCancellationRequested) return;
            FolderScanProgress.IsIndeterminate=p.Discovering;
            FolderScanProgress.Maximum=Math.Max(1,p.TotalFolders); FolderScanProgress.Value=p.FoldersScanned;
            FolderScanStatus.Text=p.Discovering ? $"Finding Folders… {p.TotalFolders:N0} Found" : $"Scanning Folders: {p.FoldersScanned:N0} / {p.TotalFolders:N0} · {p.ProjectsFound:N0} Projects";
            FolderScanStatus.ToolTip=p.Folder;
        });
        try
        {
            var projects=await Task.Run(()=>ProjectScanner.Scan(root,scan.Token,progress));
            if (_scanCts != scan || scan.IsCancellationRequested) return;
            foreach (var project in projects) _projects.Add(project);
            await RefreshSolutionMembershipAsync(true);
            FolderScanProgress.IsIndeterminate=false; FolderScanProgress.Value=FolderScanProgress.Maximum;
            FolderScanStatus.Text=$"Scan Complete · {_projects.Count:N0} Projects"; Log($"Found {_projects.Count} projects.");
        }
        catch (OperationCanceledException) { if (_scanCts == scan) FolderScanStatus.Text="Scan Cancelled"; }
        catch (Exception ex) { if (_scanCts == scan) { FolderScanStatus.Text="Scan Failed: "+ex.Message; Log(FolderScanStatus.Text); } }
        finally { if (_scanCts == scan) { _scanCts=null; FolderScanProgress.IsIndeterminate=false; CancelScanButton.IsEnabled=false; } }
    }
    private void CancelScan_Click(object sender, RoutedEventArgs e) => _scanCts?.Cancel();
    private void Scan_Click(object sender, RoutedEventArgs e) => Scan();

    private async void InstallGameLoggerButton_Click(object sender, RoutedEventArgs e)
    {
        string script=_storage.ScriptPath("Install-GameLogger.ps1");
        if(!File.Exists(script)) { Log("Script not found: "+script); return; }
        string core=Path.GetDirectoryName(Path.GetFullPath(_storage.Solution))!;
        string archive=RootBox.Text.Trim();
        await ExecuteAsync(async token => {
            int code=await ProcessRunner.RunAsync("powershell.exe",
                ["-NoProfile","-NonInteractive","-ExecutionPolicy","Bypass","-File",script,"-Root",core,"-Archive",archive],
                core,Log,token);
            Log(code==0 ? "Game logger installed. Rebuild games to include direct-launch console capture." : $"Game logger installation failed ({code}).");
        });
    }

    private void ProjectCleanup_Click(object sender, RoutedEventArgs e)
    {
        if (_cts != null || _torrentWindow != null) { Log("Finish builds and close torrent sharing before cleaning archive projects."); return; }
        var window = new ProjectCleanupWindow(RootBox.Text.Trim()) { Owner = this };
        window.ShowDialog();
        if (window.Changed) Scan();
    }

    private void Storage_Click(object sender, RoutedEventArgs e)
    {
        if (_cts != null || _torrentWindow != null) { Log("Finish builds and close torrent sharing before changing storage folders."); return; }
        _storage.Archive = RootBox.Text.Trim();
        var window = new StorageWindow(_storage) { Owner = this };
        if (window.ShowDialog() == true) { RootBox.Text = _storage.Archive; UpdateSolutionLabel(); Scan(); }
    }

    private PortableGamesWindow? _playableGames;
    private void PlayableGames_Click(object sender, RoutedEventArgs e)
    {
        if (_playableGames != null) { _playableGames.Activate(); return; }
        _playableGames = new PortableGamesWindow(_storage, selection => OpenTorrentManager().OpenShare(1, selection), Log) { Owner=this };
        _playableGames.Closed += (_,_) => _playableGames=null;
        _playableGames.Show();
    }
    private void TorrentCatalogue_Click(object sender, RoutedEventArgs e)
    {
        OpenTorrentManager();
    }

    private TorrentWindow OpenTorrentManager()
    {
        if (_torrentWindow != null) { _torrentWindow.Show(); _torrentWindow.WindowState=WindowState.Normal; _torrentWindow.Activate(); return _torrentWindow; }
        string root = Path.GetFullPath(RootBox.Text.Trim());
        string? folder = Selected().FirstOrDefault() is { } project ? Path.GetDirectoryName(Path.GetFullPath(project.Path)) : null;
        string prefix = Path.TrimEndingDirectorySeparator(root) + Path.DirectorySeparatorChar;
        if (folder != null && folder.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            folder = Path.Combine(root, Path.GetRelativePath(root, folder).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0]);
        _storage.Archive = root;
        _torrentWindow = new TorrentWindow(root, folder, _storage.TorrentCache, _storage.Games, Selected().FirstOrDefault() is { } chosen ? _storage.GameFolder(chosen.Path) : null, (Application.Current as App)?.Torrents) { Owner = this };
        _torrentWindow.ProjectInstalled += archive => { RootBox.Text = archive; Scan(); };
        _torrentWindow.StatusChanged += UpdateTorrentStatus;
        _torrentWindow.Closed += (_, _) => { _torrentWindow = null; StopTorrentsButton.IsEnabled=false; };
        StopTorrentsButton.IsEnabled=true;
        _torrentWindow.Show();
        return _torrentWindow;
    }

    private void UpdateTorrentStatus(TorrentStatusSnapshot snapshot)
    {
        TorrentSummary.Text=snapshot.Summary;
        TorrentActivity.Text=snapshot.Activity;
        TorrentActivity.ToolTip=snapshot.Activity;
        TorrentTransfersGrid.ItemsSource=snapshot.Transfers;
    }
    private void ViewTorrents_Click(object sender, RoutedEventArgs e) => OutputTabs.SelectedIndex=1;
    private async void StopTorrents_Click(object sender, RoutedEventArgs e)
    {
        if(Application.Current is App {BackgroundSeeding:true} app)
        {
            StopTorrentsButton.IsEnabled=false;
            await app.StopBackgroundSeedingAsync();
        }
        if (_torrentWindow == null) return;
        StopTorrentsButton.IsEnabled=false;
        await _torrentWindow.ShutdownAsync();
    }
    private void ShareSource_Click(object sender, RoutedEventArgs e) => ShareSelected(0);
    private void ShareGame_Click(object sender, RoutedEventArgs e) => ShareSelected(1);
    private void ShareBoth_Click(object sender, RoutedEventArgs e) => ShareSelected(2);
    private void ShareSelected(int kind)
    {
        try
        {
            var project=Selected().FirstOrDefault() ?? throw new InvalidOperationException("Select a game project to share first.");
            if (_cts != null) throw new InvalidOperationException("Wait for the current build to finish before sharing a snapshot.");
            var selection=ShareSelection.ForProject(RootBox.Text.Trim(),project.Path,_storage);
            OutputTabs.SelectedIndex=1;
            OpenTorrentManager().OpenShare(kind,selection);
        }
        catch (Exception error) { MessageBox.Show(this,error.Message,"Share Project",MessageBoxButton.OK,MessageBoxImage.Information); }
    }

    private async void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if(Application.Current is App {BackgroundSeeding:true}) {e.Cancel=true;Hide();return;}
        _scanCts?.Cancel();
        if (_torrentWindow == null) return;
        e.Cancel = true;
        if (_torrentShutdown) return;
        _torrentShutdown = true;
        await _torrentWindow.ShutdownAsync();
        Close();
    }

    private void RunGameButton_Click(object sender, RoutedEventArgs e)
    {
        var project = Selected().FirstOrDefault();
        if (project is null)
        {
            MessageBox.Show("Please select a game first.", "Run Game",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        string? gameDirectory = Path.GetDirectoryName(project.Path);
        if (string.IsNullOrWhiteSpace(gameDirectory) || !Directory.Exists(gameDirectory))
        {
            MessageBox.Show($"Project directory not found: {gameDirectory}", "Run Game",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        string binDirectory = _storage.GameFolder(project.Path);
        if (!Directory.Exists(binDirectory)) binDirectory = Path.Combine(gameDirectory, "bin");
        if (!Directory.Exists(binDirectory))
        {
            MessageBox.Show($"No build output found in:\n{binDirectory}\nBuild the game first.",
                "Run Game", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            string configuration = Value(ConfigBox);
            string platform = Value(PlatformBox);
            string runtime = RuntimeBox.Text.Trim();
            string framework = FrameworkBox.Text.Trim();
            string projectName = Path.GetFileNameWithoutExtension(project.Path);
            string? assemblyName = null;

            // AssemblyName can differ from the project filename (e.g. spaces in XBLIG names).
            // Reading the simple property from the project avoids launching an unrelated tool.
            var doc = System.Xml.Linq.XDocument.Load(project.Path);
            assemblyName = doc.Descendants()
                .FirstOrDefault(e => e.Name.LocalName == "AssemblyName" &&
                                     !e.Value.Contains("$(", StringComparison.Ordinal))?.Value.Trim();
            if (string.IsNullOrWhiteSpace(assemblyName)) assemblyName = projectName;

            var executables = Directory.EnumerateFiles(binDirectory, "*.exe", SearchOption.AllDirectories)
                .Where(path => !path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    .Any(part => part.Equals("ref", StringComparison.OrdinalIgnoreCase) ||
                                 part.Equals("obj", StringComparison.OrdinalIgnoreCase)))
                .Select(path => new
                {
                    Path = path,
                    Name = Path.GetFileNameWithoutExtension(path),
                    Parts = Path.GetRelativePath(binDirectory, path)
                        .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
                    Modified = File.GetLastWriteTimeUtc(path)
                })
                .Select(item => new
                {
                    item.Path,
                    item.Modified,
                    Score =
                        (item.Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) ? 1000 :
                         item.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase) ? 900 : 0) +
                        (item.Parts.Any(part => part.Equals(configuration, StringComparison.OrdinalIgnoreCase)) ? 100 : 0) +
                        (item.Parts.Any(part => part.Equals(platform, StringComparison.OrdinalIgnoreCase)) ? 50 : 0) +
                        (string.IsNullOrWhiteSpace(runtime) || item.Parts.Any(part => part.Equals(runtime, StringComparison.OrdinalIgnoreCase)) ? 25 : 0) +
                        (string.IsNullOrWhiteSpace(framework) || item.Parts.Any(part => part.Equals(framework, StringComparison.OrdinalIgnoreCase)) ? 25 : 0) -
                        (item.Parts.Any(part => part.Equals("x86", StringComparison.OrdinalIgnoreCase) ||
                                                part.Equals("win-x86", StringComparison.OrdinalIgnoreCase)) &&
                         platform.Equals("x64", StringComparison.OrdinalIgnoreCase) ? 200 : 0) -
                        (item.Parts.Any(part => part.Equals("x64", StringComparison.OrdinalIgnoreCase) ||
                                                part.Equals("win-x64", StringComparison.OrdinalIgnoreCase)) &&
                         platform.Equals("x86", StringComparison.OrdinalIgnoreCase) ? 200 : 0)
                })
                .Where(item => item.Score >= 900)
                .OrderByDescending(item => item.Score)
                .ThenByDescending(item => item.Modified)
                .ToList();

            if (executables.Count == 0)
            {
                Log($"No executable matching {assemblyName}.exe found under {binDirectory}");
                MessageBox.Show($"No executable for '{project.Name}' found in:\n{binDirectory}\n\nBuild the game first, or check its AssemblyName and output directory.",
                    "Run Game", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string executable = executables[0].Path;
            Log($"=== Running {project.Name} ===");
            Log($"Executable: {executable}");
            OutputTabs.SelectedIndex=0;
            GameProcessLauncher.Start(executable,GameProcessLauncher.DefaultLogDirectory,Log);
        }
        catch (Exception ex)
        {
            Log($"Game launch failed: {ex}");
            MessageBox.Show(ex.ToString(), "Game Launch Failed",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Browse_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog { Title = "Choose indie-game-archive", InitialDirectory = Directory.Exists(RootBox.Text) ? RootBox.Text : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) };
        if (dialog.ShowDialog() == true) { RootBox.Text = dialog.FolderName; Scan(); }
    }
    private static string Value(ComboBox box) => box.SelectedItem is ComboBoxItem item ? item.Content.ToString() ?? "" : box.Text.Trim();
    private List<GameProject> Selected() => ProjectsGrid.SelectedItems.Cast<GameProject>().ToList();
    private void BuildArchitecture_Changed(object sender, RoutedEventArgs e)
    {
        if (!_architectureReady || _updatingArchitecture || sender is not ComboBox box) return;
        string choice = e is SelectionChangedEventArgs && box.SelectedItem is ComboBoxItem item ? item.Content?.ToString() ?? "" : box.Text;
        string architecture = choice.Trim().ToLowerInvariant() switch
        {
            "x86" => "x86", "x64" => "x64", "anycpu" => "AnyCPU", _ => ""
        };
        if (architecture.Length == 0) return;
        _updatingArchitecture = true;
        try
        {
            PlatformBox.Text = architecture;
            TargetBox.Text = architecture;
            UpdateBuildArchitectureRows();
        }
        finally { _updatingArchitecture = false; }
    }

    private void UpdateBuildArchitectureRows()
    {
        if (!_architectureReady || TargetBox == null) return;
        var selected = Selected().ToHashSet();
        string architecture = Value(TargetBox);
        foreach (var project in _projects)
            project.BuildArchitecture = selected.Contains(project) ? architecture : "";
    }

    private void SaveArchitecture_Click(object sender, RoutedEventArgs e)
    {
        if (_cts != null) { Log("Wait for the current operation to finish before saving project settings."); return; }
        var selected = Selected();
        if (selected.Count == 0) { Log("Select game rows before saving architecture."); return; }
        string architecture = Value(TargetBox);
        foreach (var project in selected)
        {
            try
            {
                var settings = ProjectArchitectureUpdater.Apply(project.Path, architecture);
                project.PlatformTarget = settings.PlatformTarget;
                project.Platforms = settings.Platforms;
                project.RuntimeIdentifier = settings.RuntimeIdentifier;
                project.Status = "Architecture saved; rebuild";
                Log($"Saved {settings.PlatformTarget} architecture: {project.Path} (original project backed up).");
            }
            catch (Exception ex) { Log($"Could not save architecture for {project.Name}: {ex.Message}"); }
        }
        ProjectsGrid_SelectionChanged(ProjectsGrid, new SelectionChangedEventArgs(DataGrid.SelectionChangedEvent, Array.Empty<object>(), Array.Empty<object>()));
    }
    private async Task ExecuteAsync(Func<CancellationToken, Task> job)
    {
        if (_cts != null) { Log("Another operation is already running."); return; }
        _cts = new CancellationTokenSource();
        try { await job(_cts.Token); }
        catch (OperationCanceledException) { Log("Cancelled."); }
        catch (Exception ex) { Log($"ERROR: {ex.Message}"); }
        finally { _cts.Dispose(); _cts = null; }
    }
    private void Cancel_Click(object sender, RoutedEventArgs e) => _cts?.Cancel();
    private async void Inspect_Click(object sender, RoutedEventArgs e)
    {
        var project = Selected().FirstOrDefault();
        if (project is null) { Log("Select a project first."); return; }
        await ExecuteAsync(async token =>
        {
            Log($"Inspecting {project.Name}");
            var args = new List<string> { "msbuild", project.Path,
                $"-p:Configuration={Value(ConfigBox)}", $"-p:Platform={Value(PlatformBox)}",
                "-getProperty:TargetFramework,TargetFrameworks,Configuration,Platform,PlatformTarget,Platforms,RuntimeIdentifier,RuntimeIdentifiers,OutputPath,BaseIntermediateOutputPath,ProjectAssetsFile" };
            args.AddRange(_storage.BuildArguments(project.Path));
            await ProcessRunner.RunAsync("dotnet", args,
                Path.GetDirectoryName(project.Path)!, Log, token);
        });
    }
    private async void Detect_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects first."); return; }
        string exe = UseVsBox.IsChecked == true ? MsbuildPathBox.Text.Trim() : "dotnet";
        if (UseVsBox.IsChecked == true && !File.Exists(exe)) { Log("Set a valid MSBuild.exe path."); return; }
        string config = Value(ConfigBox), platform = Value(PlatformBox), target = Value(TargetBox);
        string framework = FrameworkBox.Text.Trim(), runtime = RuntimeBox.Text.Trim();
        await ExecuteAsync(async token =>
        {
            foreach (var project in selected)
            {
                token.ThrowIfCancellationRequested();
                Log($"=== Checking {project.Name} [{config}|{platform}] ===");
                try
                {
                    var report = await ConfigurationDiagnostics.InspectAsync(project, config, platform,
                        target, framework, runtime, exe, Log, token, _storage.BuildArguments(project.Path));
                    foreach (var issue in report.Issues)
                        Log($"[{issue.Severity}] {issue.Code}: {issue.Detail}\\n  Suggested action: {issue.Recommendation}");
                    project.Status = report.Issues.Any(i => i.Severity == "Error") ? "Issues found" :
                        report.Issues.Any(i => i.Severity == "Warning") ? "Warnings" : "Checked";
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) { project.Status = "Check failed"; Log($"ERROR: {ex.Message}"); }
            }
        });
    }
    private async void Build_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects."); return; }
        var action = Value(ActionBox).ToLowerInvariant();
        await ExecuteAsync(async token =>
        {
            foreach (var project in selected)
            {
                token.ThrowIfCancellationRequested();
                project.Status = "Running";
                var args = new List<string>();
                bool vs = UseVsBox.IsChecked == true;
                string exe = vs ? MsbuildPathBox.Text.Trim() : "dotnet";
                if (vs && !File.Exists(exe)) throw new FileNotFoundException("Specify an existing MSBuild.exe path.", exe);
                if (!vs) args.Add(action == "restore" ? "restore" : "build");
                args.Add(project.Path);
                _storage.Archive = Path.GetFullPath(RootBox.Text.Trim());
                args.AddRange(_storage.BuildArguments(project.Path));
                if (vs) args.Add(action switch { "rebuild" => "-t:Rebuild", "clean" => "-t:Clean", "restore" => "-t:Restore", _ => "-t:Build" });
                else if (action == "rebuild") args.Add("-t:Rebuild");
                else if (action == "clean") args.Add("-t:Clean");
                args.Add($"-p:Configuration={Value(ConfigBox)}");
                args.Add($"-p:Platform={Value(PlatformBox)}");
                args.Add($"-p:PlatformTarget={Value(TargetBox)}");
                string networkBackend = Value(NetworkBackendBox);

                args.Add($"-p:NetworkBackend={networkBackend}");
                if (!string.IsNullOrWhiteSpace(FrameworkBox.Text)) args.Add($"-p:TargetFramework={FrameworkBox.Text.Trim()}");
                if (!string.IsNullOrWhiteSpace(RuntimeBox.Text)) args.Add($"-p:RuntimeIdentifier={RuntimeBox.Text.Trim()}");
                if (!string.IsNullOrWhiteSpace(DefineConstantsBox.Text))
                {
                    // MSBuild -p treats semicolons as property separators.
                    // Escape them as %3B to preserve the full DefineConstants value.
                    string symbols = DefineConstantsBox.Text.Trim().Replace(";", "%3B");
                    args.Add($"-p:DefineConstants={symbols}");
                    Log("Note: DefineConstants overrides the project's evaluated symbols for this build.");
                }
                if (action != "restore" && !vs && NoRestoreBox.IsChecked == true) args.Add("--no-restore");
                Log($"> {exe} {string.Join(" ", args)}");
                try
                {
                    int code = await ProcessRunner.RunAsync(exe, args, Path.GetDirectoryName(project.Path)!, Log, token);
                    project.Status = code == 0 ? "Succeeded" : $"Failed ({code})";
                }
                catch { project.Status = "Failed"; throw; }
            }
        });
    }
    private void InjectStartup_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects."); return; }
        if (MessageBox.Show($"Inject startup diagnostics into {selected.Count} selected project(s)? Source backups will be created.",
            "Confirm source changes", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
        foreach (var project in selected)
        {
            try { Log($"{project.Name}: {StartupDiagnosticsInjector.Inject(project.Path)}"); }
            catch (Exception ex) { Log($"{project.Name}: {ex.Message}"); }
        }
    }

    private void InjectFnaVersions_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects."); return; }
        foreach (var project in selected)
        {
            try { Log($"{project.Name}: {StartupDiagnosticsInjector.InjectVersions(project.Path)}"); }
            catch (Exception ex) { Log($"{project.Name}: {ex.Message}"); }
        }
    }

    private async void RepairWindowTitle_Click(object sender, RoutedEventArgs e)
    {
        var selected=Selected();
        if(selected.Count==0) { Log("Select game rows first."); return; }
        await ExecuteAsync(async token => {
            foreach(var project in selected)
            {
                token.ThrowIfCancellationRequested();
                Log(project.Name+": "+await Task.Run(()=>GameTitleRepair.Apply(project.Path),token));
            }
        });
    }

    private void XnaRepairHelp_Click(object sender, RoutedEventArgs e) => SharingHelpWindow.ShowXnaHelp(this);
    private async void InstallOverlay_Click(object sender, RoutedEventArgs e)
    {
        var selected=Selected();if(selected.Count==0){Log("Select one or more game rows first.");return;}
        string core=Path.GetDirectoryName(Path.GetFullPath(_storage.Solution))!;
        await ExecuteAsync(async token=>{
            foreach(var game in selected){token.ThrowIfCancellationRequested();try{var result=await Task.Run(()=>GameOverlayInstaller.Install(game.Path,core,token),token);Log($"{game.Name}: {result.Status} — {result.Message}");if(result.Backup!=null)Log("Overlay backup: "+result.Backup);}catch(OperationCanceledException){throw;}catch(Exception ex){Log($"{game.Name}: overlay installation failed — {ex.GetBaseException().Message}");}}
        });
    }
    private void ConvertXnaBinary_Click(object sender, RoutedEventArgs e) => new XnaConversionWindow(_storage) { Owner=this }.ShowDialog();
    private void OpenGameLogs_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string folder=GameProcessLauncher.DefaultLogDirectory; Directory.CreateDirectory(folder);
            Process.Start(new ProcessStartInfo(folder) { UseShellExecute=true });
        }
        catch(Exception ex) { Log("Could not open game logs: "+ex.Message); }
    }

    private void ProjectsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (GameSelectionCount == null) return;
        UpdateBuildArchitectureRows();
        var selected = Selected();
        var project = ProjectsGrid.SelectedItem as GameProject;
        GameSelectionCount.Text = selected.Count == 0 ? "Select games to build or repair" :
            $"{selected.Count} game{(selected.Count == 1 ? "" : "s")} selected";
        OpenSelectedFolderButton.IsEnabled = project != null;
        AddToSolutionButton.IsEnabled = selected.Count > 0;
        RemoveFromSolutionButton.IsEnabled = selected.Count > 0;
        SelectedGameName.Text = project?.Name ?? "No game selected";
        SelectedGameFolderBox.Text = project == null ? "Select a row to see its folder" :
            Path.GetDirectoryName(Path.GetFullPath(project.Path))!;
        SelectedGameFolderBox.ToolTip = SelectedGameFolderBox.Text;
        SelectedGameDetails.Text = project == null ? "" :
            $"{Path.GetFileName(project.Path)} · {Display(project.TargetFramework)} · {Display(project.PlatformTarget)}" +
            (string.IsNullOrWhiteSpace(project.RuntimeIdentifier) ? "" : $" · {project.RuntimeIdentifier}") +
            (selected.Count > 1 ? $" · +{selected.Count - 1} other selected" : "");
        static string Display(string value) => string.IsNullOrWhiteSpace(value) ? "Project default" : value;
    }

    private void OpenSelectedFolder_Click(object sender, RoutedEventArgs e)
    {
        if (ProjectsGrid.SelectedItem is not GameProject project) return;
        string folder = Path.GetDirectoryName(Path.GetFullPath(project.Path))!;
        if (!Directory.Exists(folder)) { Log($"Folder not found: {folder}"); return; }
        try { Process.Start(new ProcessStartInfo(folder) { UseShellExecute = true }); }
        catch (Exception ex) { Log($"Could not open folder: {ex.Message}"); }
    }

    private void UpdateSolutionLabel()
    {
        SelectedSolutionName.Text = string.IsNullOrWhiteSpace(_storage.Solution) ? "Set solution in settings" : Path.GetFileName(_storage.Solution) + " · green rows are in the solution";
        SelectedSolutionName.ToolTip = _storage.Solution;
        EffectsScriptBox.Text = _storage.ScriptPath("Convert-XnbEffects.ps1");
        ScriptBox.Text = _storage.ScriptPath("fix_sdk1005.ps1");
    }

    private async void AddToSolution_Click(object sender, RoutedEventArgs e) => await UpdateSolutionAsync(true);
    private async void RemoveFromSolution_Click(object sender, RoutedEventArgs e) => await UpdateSolutionAsync(false);

    private async Task UpdateSolutionAsync(bool add)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select project rows first."); return; }
        string solution = _storage.Solution;
        if (string.IsNullOrWhiteSpace(solution) || !File.Exists(solution)) {
            Log($"Configured solution not found: {solution}. Update the solution path in Settings.");
            return;
        }
        await ExecuteAsync(async token => {
            try { await SolutionProjects.UpdateAsync(solution, selected.Select(p => p.Path), add, Log, token); await RefreshSolutionMembershipAsync(true); }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex) { Log($"Solution update failed: {ex.Message}"); }
        });
    }

    private async Task RefreshSolutionMembershipAsync(bool force=false)
    {
        if (_checkingMembership) { _membershipPending |= force; return; }
        _checkingMembership=true;
        string solution=_storage.Solution, previous=_membershipStamp ?? "";
        try
        {
            var snapshot=await Task.Run(() => {
                var info=new FileInfo(solution);
                string stamp=solution + "|" + (info.Exists ? info.LastWriteTimeUtc.Ticks + ":" + info.Length : "missing");
                return !force && stamp==previous ? null : new { Stamp=stamp, Members=SolutionMembership.Read(solution) };
            });
            if (snapshot == null || solution != _storage.Solution || Dispatcher.HasShutdownStarted) return;
            foreach (var project in _projects) project.IsInSolution=snapshot.Members.Contains(Path.GetFullPath(project.Path));
            _membershipStamp=snapshot.Stamp; _membershipError=null;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or System.Xml.XmlException)
        {
            if (_membershipError!=ex.Message) { Log("Could not refresh solution membership: " + ex.Message); _membershipError=ex.Message; }
        }
        finally
        {
            _checkingMembership=false;
            if (_membershipPending) { _membershipPending=false; _=RefreshSolutionMembershipAsync(true); }
        }
    }

    private async void BatchXnaFixes_Click(object sender, RoutedEventArgs e)
    {
        string archive = Path.GetFullPath(RootBox.Text.Trim());
        if (!Directory.Exists(archive)) { Log($"Archive not found: {archive}"); return; }
        // Use the selected rows in the main grid; group multiple projects
        // belonging to the same game so the repair is applied once per game folder.
        string archivePrefix = Path.TrimEndingDirectorySeparator(archive) + Path.DirectorySeparatorChar;
        var games = Selected()
            .Select(p => Path.GetFullPath(p.Path))
            .Where(path => path.StartsWith(archivePrefix, StringComparison.OrdinalIgnoreCase))
            .Select(path => {
                string relative = Path.GetRelativePath(archive, path);
                string topFolder = relative.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0];
                return new { Name = topFolder, Folder = Path.Combine(archive, topFolder) };
            })
            .GroupBy(g => g.Folder, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .OrderBy(g => g.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
        if (games.Count == 0) {
            MessageBox.Show(this, "Select one or more game rows first.", "Batch repairs");
            return;
        }
        string script = _storage.ScriptPath("XnaRepairFunctions.ps1");

        var dialog = new Window {
            Title = "Source and SDK repairs", Width = 820, Height = 600,
            MinWidth = 650, MinHeight = 450, Owner = this,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(17,24,39)),
            Foreground = Brushes.White
        };
        var layout = new DockPanel { Margin = new Thickness(14) };
        dialog.Resources[typeof(Button)] = FindResource(typeof(Button));
        dialog.Content = layout;
        var top = new StackPanel(); DockPanel.SetDock(top, Dock.Top); layout.Children.Add(top);
        top.Children.Add(new TextBlock { Text = "Repair selected games", FontSize = 17, Margin = new Thickness(0,0,0,10) });
        top.Children.Add(new TextBlock {
            Text = $"{games.Count} distinct game folder(s) selected. Use Ctrl or Shift in the main grid to change the selection.",
            Margin = new Thickness(0,0,0,10), Foreground = Brushes.LightGray
        });
        var fixers = new[] {
            ("Storage selector", "Repair-XnaStorageSelector"), ("Open storage", "Repair-XnaOpenContainer"),
            ("Save games", "Repair-XnaStorageSave"), ("Load games", "Repair-XnaStorageLoad"),
            ("File access", "Repair-XnaFileStream"), ("Colors", "Repair-XnaColors"),
            ("Gamer avatars", "Repair-XnaSignedInGamerAvatar"), ("Sprite drawing", "Repair-XnaSpriteBatch")
        };
        top.Children.Add(new TextBlock { Text = "Source repairs", Margin = new Thickness(0,8,0,4) });
        var fixerPanel = new WrapPanel(); top.Children.Add(fixerPanel);
        var fixerChecks = fixers.Select(name => new CheckBox {
            Content = name.Item1, Tag = name.Item2, IsChecked = name.Item2 == "Repair-XnaColors",
            Margin = new Thickness(0,0,16,7), Foreground = Brushes.White
        }).ToList();
        foreach (var check in fixerChecks) fixerPanel.Children.Add(check);
        var sdkFixers = new[] {
            ("Missing framework assets (1005)", "Repair-NetSdk1005"),
            ("Conflicting build folders", "Repair-SharedProjectAssets"),
            ("Missing runtime assets (1047)", "Repair-SDK1047"),
            ("Architecture settings (1032)", "Repair-SDK1032"),
            ("Disable auxiliary DLL projects", "Rename-DllProjects"),
            ("Restore disabled DLL projects", "Rename-DllProjects-Restore"),
            ("Format project XML (with backups)", "Format-XbligProjects")
        };
        top.Children.Add(new TextBlock { Text = "SDK / project fixes:", Margin = new Thickness(0,8,0,4) });
        var sdkPanel = new WrapPanel(); top.Children.Add(sdkPanel);
        var sdkChecks = sdkFixers.Select(f => new CheckBox {
            Content = f.Item1, Tag = f.Item2, IsChecked = false,
            Margin = new Thickness(0,0,16,7), Foreground = Brushes.White
        }).ToList();
        foreach (var check in sdkChecks) sdkPanel.Children.Add(check);
        var sdkRestore = new CheckBox { Content = "Restore packages (shared assets / SDK1032)",
            IsChecked = true, Foreground = Brushes.White, Margin = new Thickness(0,3,12,3) };
        var sdkClean = new CheckBox { Content = "Clean intermediates (shared assets / SDK1047)",
            IsChecked = false, Foreground = Brushes.White, Margin = new Thickness(0,3,12,3) };
        var sdkRids = new CheckBox { Content = "Set RuntimeIdentifiers on shared projects",
            IsChecked = true, Foreground = Brushes.White, Margin = new Thickness(0,3,12,3) };
        var sdkOptions = new StackPanel();
        sdkOptions.Children.Add(sdkRestore); sdkOptions.Children.Add(sdkClean); sdkOptions.Children.Add(sdkRids);
        top.Children.Add(new Expander { Header = "Advanced SDK options", Content = sdkOptions,
            Foreground = Brushes.White, Margin = new Thickness(0,6,0,6) });
        var preview = new CheckBox { Content = "Preview only (-WhatIf)", IsChecked = true,
            Foreground = Brushes.White, Margin = new Thickness(0,6,0,8) };
        top.Children.Add(preview);
        var bottom = new StackPanel(); DockPanel.SetDock(bottom, Dock.Bottom); layout.Children.Add(bottom);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        bottom.Children.Add(actions);
        var cancel = new Button { Content = "Cancel", Margin = new Thickness(5) };
        var run = new Button { Content = "Run selected repairs", Margin = new Thickness(5) };
        actions.Children.Add(cancel); actions.Children.Add(run);
        var summary = new TextBlock {
            Text = "Selected games: " + string.Join(", ", games.Take(8).Select(g => g.Name)) +
                   (games.Count > 8 ? $" (+{games.Count - 8} more)" : ""),
            TextWrapping = TextWrapping.Wrap, Foreground = Brushes.LightGray,
            Margin = new Thickness(0,8,0,8)
        };
        layout.Children.Add(summary);
        cancel.Click += (_, _) => { dialog.DialogResult = false; };
        run.Click += (_, _) => {
            if (games.Count == 0 || !(fixerChecks.Any(c => c.IsChecked == true) || sdkChecks.Any(c => c.IsChecked == true))) {
                MessageBox.Show(dialog, "Select at least one game and one repair."); return;
            }
            if (preview.IsChecked != true && MessageBox.Show(dialog,
                $"Modify source files for {games.Count} games? Backups will be created.",
                "Confirm source modifications", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            dialog.DialogResult = true;
        };
        if (dialog.ShowDialog() != true) return;
        var selectedGames = games;
        var selectedFixers = fixerChecks.Where(c => c.IsChecked == true).Select(c => (string)c.Tag).ToArray();
        if (selectedFixers.Length > 0 && !File.Exists(script)) {
            var chooser = new OpenFileDialog { Title = "Choose XnaRepairFunctions.ps1",
                Filter = "PowerShell (*.ps1)|*.ps1" };
            if (chooser.ShowDialog(this) != true) return;
            script = chooser.FileName;
        }
        var selectedSdkFixers = sdkChecks.Where(c => c.IsChecked == true)
            .Select(c => (string)c.Tag).ToArray();
        bool sdkDoRestore = sdkRestore.IsChecked == true;
        bool sdkDoClean = sdkClean.IsChecked == true;
        bool sdkFixRids = sdkRids.IsChecked == true;
        string sdkScript = _storage.ScriptPath("BuildSdkRepairs.ps1");
        if (selectedSdkFixers.Length > 0 && !File.Exists(sdkScript)) {
            var chooser = new OpenFileDialog { Title = "Choose BuildSdkRepairs.ps1",
                Filter = "PowerShell (*.ps1)|*.ps1" };
            if (chooser.ShowDialog(this) != true) return;
            sdkScript = chooser.FileName;
        }
        string sdkFramework = string.IsNullOrWhiteSpace(FrameworkBox.Text)
            ? "net9.0-windows" : FrameworkBox.Text.Trim();
        string sdkPlatform = Value(PlatformBox);
        if (sdkPlatform is not ("x64" or "x86")) sdkPlatform = "x64";
        bool whatIf = preview.IsChecked == true;
        await ExecuteAsync(async token => {
            int succeeded = 0, failed = 0;
            foreach (var game in selectedGames) {
                token.ThrowIfCancellationRequested();
                Log($"=== {(whatIf ? "PREVIEW" : "REPAIR")}: {game.Name} ===");
                // Run a wrapper using -Command so the function-only .ps1 can be dot-sourced.
                // EncodedCommand avoids escaping game names with quotes/apostrophes.
                string quote(string value) => "'" + value.Replace("'", "''") + "'";
                string commands = "$ErrorActionPreference='Stop'; " +
                    "if (" + (selectedFixers.Length > 0 ? "$true" : "$false") + ") { . " + quote(script) + "; " +
                    "foreach($fix in @(" + string.Join(",", selectedFixers.Select(quote)) + ")) { " +
                    "if(-not (Get-Command $fix -CommandType Function -ErrorAction SilentlyContinue)) { throw \"Missing fixer: $fix\" }; " +
                    "& $fix -Root " + quote(game.Folder) + (whatIf ? " -WhatIf" : "") +
                    " -ErrorAction Stop; if(-not $?) { throw \"Fixer failed: $fix\" } } }; ";
                if (selectedSdkFixers.Length > 0) {
                    if (whatIf) {
                        commands += "Write-Host 'PREVIEW: SDK fixes are listed but NOT executed (they do not support -WhatIf).'; " +
                            "Write-Host " + quote("SDK: " + string.Join(", ", selectedSdkFixers)) + "; ";
                    } else {
                        commands += ". " + quote(sdkScript) + "; " +
                            "$game=" + quote(game.Folder) + "; " +
                            "$framework=" + quote(sdkFramework) + "; " +
                            "$platform=" + quote(sdkPlatform) + "; ";
                        foreach (string fix in selectedSdkFixers) {
                            commands += fix switch {
                                "Repair-NetSdk1005" =>
                                    "Repair-NetSdk1005 -Root " + quote(archive) +
                                    " -ProjectName " + quote(game.Name) + " -Framework $framework; ",
                                "Repair-SharedProjectAssets" =>
                                    "Repair-SharedProjectAssets -Game $game" +
                                    (sdkDoRestore ? " -Restore" : "") +
                                    (sdkDoClean ? " -Clean" : "") +
                                    (sdkFixRids ? " -FixRuntimeIdentifiers" : "") + "; ",
                                "Repair-SDK1047" =>
                                    "Repair-SDK1047 -Game $game -Framework $framework -Platform $platform" +
                                    (sdkDoClean ? " -Clean" : "") + "; ",
                                "Repair-SDK1032" =>
                                    "Repair-SDK1032 -Game $game -Framework $framework -Platform $platform" +
                                    (sdkDoRestore ? " -Restore" : "") + "; ",
                                "Rename-DllProjects" => "Rename-DllProjects -Game $game; ",
                                "Rename-DllProjects-Restore" => "Rename-DllProjects -Game $game -Restore; ",
                                "Format-XbligProjects" => "Format-XbligProjects -Game $game; ",
                                _ => ""
                            };
                            commands += "if(-not $?) { throw 'SDK fixer failed' }; ";
                        }
                    }
                }
                string encoded = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(commands));
                try {
                    int exit = await ProcessRunner.RunAsync("powershell.exe",
                        ["-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-EncodedCommand", encoded],
                        game.Folder, Log, token);
                    if (exit == 0) succeeded++; else { failed++; Log($"FAILED: {game.Name} (exit {exit})"); }
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) { failed++; Log($"FAILED: {game.Name}: {ex.Message}"); }
            }
            Log($"Batch complete. Successful: {succeeded}; failed: {failed}; preview: {whatIf}.");
        });
    }

    private async void ConvertWaveBanks_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more game rows first."); return; }
        string decoder = WaveBankDecoderBox.Text.Trim();
        if (!File.Exists(decoder)) { Log($"Wave bank decoder not found: {decoder}"); return; }
        string archive = Path.GetFullPath(RootBox.Text.Trim());
        string prefix = Path.TrimEndingDirectorySeparator(archive) + Path.DirectorySeparatorChar;
        var folders = selected.Select(p => Path.GetFullPath(p.Path))
            .Select(path => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? Path.Combine(archive, Path.GetRelativePath(archive, path)
                    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0])
                : Path.GetDirectoryName(path)!)
            .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        await ExecuteAsync(async token =>
        {
            foreach (string folder in folders)
            {
                token.ThrowIfCancellationRequested();
                string content = Path.Combine(folder, "Content");
                if (!Directory.Exists(content)) { Log($"SKIP: No Content folder: {folder}"); continue; }
                Log($"=== Converting wave banks: {Path.GetFileName(folder)} ===");
                var results = await WaveBankConverter.ConvertAsync(content, decoder, Log, token);
                Log($"Wave banks: {results.Count(r => r.Status == "Converted")} converted, " +
                    $"{results.Count(r => r.Status == "Skipped")} already PCM, {results.Count(r => r.Status == "Failed")} failed. " +
                    "Rebuild the game to copy updated wave banks to bin.");
            }
        });
    }

    private async void ConvertXnbFonts_Click(object sender, RoutedEventArgs e)
        => await ConvertGraphicsContentAsync(textures: false);

    private async void ShaderDemos_Click(object sender, RoutedEventArgs e)
    {
        try { await ShaderDemoLauncher.OpenAsync(Log); }
        catch (Exception error) { Log("Shader demo browser: " + error.Message); }
    }

    private async void ConvertVideos_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more game rows first."); return; }
        string ffmpeg = AudioFfmpegBox.Text.Trim();
        if (!File.Exists(ffmpeg)) { Log("FFmpeg not found: " + ffmpeg); return; }
        string archive = Path.GetFullPath(RootBox.Text.Trim());
        string prefix = Path.TrimEndingDirectorySeparator(archive) + Path.DirectorySeparatorChar;
        var folders = selected.Select(p => Path.GetFullPath(p.Path))
            .Select(path => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? Path.Combine(archive, Path.GetRelativePath(archive, path).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0])
                : Path.GetDirectoryName(path)!).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        await ExecuteAsync(async token =>
        {
            foreach (string folder in folders)
            {
                string content = Path.Combine(folder, "Content");
                if (!Directory.Exists(content)) { Log("SKIP: No Content folder: " + folder); continue; }
                Log("=== Converting videos: " + Path.GetFileName(folder) + " ===");
                var results = await VideoConverter.ConvertAsync(content, ffmpeg, Log, token);
                Log($"Videos: {results.Count(r => r.Status == "Converted")} converted, {results.Count(r => r.Status == "Skipped")} existing OGVs preserved, {results.Count(r => r.Status == "Failed")} failed. Rebuild to copy content to bin; restart the game to reload it.");
            }
        });
    }

    private async void ConvertXnbTextures_Click(object sender, RoutedEventArgs e)
        => await ConvertGraphicsContentAsync(textures: true);

    private async Task ConvertGraphicsContentAsync(bool textures)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more game rows first."); return; }
        string extractor = EffectsExtractorBox.Text.Trim();
        if (!File.Exists(extractor)) { Log($"XNB extractor not found: {extractor}"); return; }
        string archive = Path.GetFullPath(RootBox.Text.Trim());
        string prefix = Path.TrimEndingDirectorySeparator(archive) + Path.DirectorySeparatorChar;
        var folders = selected.Select(p => Path.GetFullPath(p.Path))
            .Select(path => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? Path.Combine(archive, Path.GetRelativePath(archive, path)
                    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0])
                : Path.GetDirectoryName(path)!)
            .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        await ExecuteAsync(async token =>
        {
            foreach (string folder in folders)
            {
                token.ThrowIfCancellationRequested();
                string content = Path.Combine(folder, "Content");
                if (!Directory.Exists(content)) { Log($"SKIP: No Content folder: {folder}"); continue; }
                string kind = textures ? "Textures" : "Fonts";
                Log($"=== Converting {kind}: {Path.GetFileName(folder)} ===");
                var results = await Task.Run(() => textures
                    ? SpriteFontConverter.ConvertTextures(content, extractor, Log, token)
                    : SpriteFontConverter.ConvertFonts(content, extractor, Log, token), token);
                Log($"{kind}: {results.Count(r => r.Status == "Converted")} converted, " +
                    $"{results.Count(r => r.Status == "Skipped")} up to date, {results.Count(r => r.Status == "Failed")} failed. " +
                    "Rebuild the game to copy updated content to bin.");
            }
        });
    }

    private async void ConvertXnbAudio_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more game rows first."); return; }
        string script = _storage.ScriptPath("Convert-XnbAudio.ps1");
        string extractor = EffectsExtractorBox.Text.Trim();
        string ffmpeg = AudioFfmpegBox.Text.Trim();
        foreach (string path in new[] { script, extractor, ffmpeg })
            if (!File.Exists(path)) { Log($"Audio conversion dependency not found: {path}"); return; }
        string archive = Path.GetFullPath(RootBox.Text.Trim());
        string prefix = Path.TrimEndingDirectorySeparator(archive) + Path.DirectorySeparatorChar;
        var folders = selected.Select(p => Path.GetFullPath(p.Path))
            .Select(path => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? Path.Combine(archive, Path.GetRelativePath(archive, path)
                    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0])
                : Path.GetDirectoryName(path)!)
            .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        await ExecuteAsync(async token =>
        {
            foreach (string folder in folders)
            {
                token.ThrowIfCancellationRequested();
                string content = Path.Combine(folder, "Content");
                if (!Directory.Exists(content)) { Log($"SKIP: No Content folder: {folder}"); continue; }
                Log($"=== Converting Songs and Sounds: {Path.GetFileName(folder)} ===");
                int code = await ProcessRunner.RunAsync("powershell.exe",
                    ["-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-File", script,
                     "-ContentDirectory", content, "-ExtractorExe", extractor, "-FfmpegExe", ffmpeg, "-Install"],
                    folder, Log, token);
                Log(code == 0 ? "Audio converted. Rebuild the game to copy updated content to bin." :
                    "Audio conversion had failures. Review the log and audio-conversion report.");
            }
        });
    }

    private async void RepairKnownShaders_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more game rows first."); return; }
        string archive = Path.GetFullPath(RootBox.Text.Trim());
        string prefix = Path.TrimEndingDirectorySeparator(archive) + Path.DirectorySeparatorChar;
        var games = selected.GroupBy(p => {
            string path = Path.GetFullPath(p.Path);
            return path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? Path.Combine(archive, Path.GetRelativePath(archive, path).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0])
                : Path.GetDirectoryName(path)!;
        }, StringComparer.OrdinalIgnoreCase).ToList();
        await ExecuteAsync(async token =>
        {
            foreach (var game in games)
            {
                token.ThrowIfCancellationRequested();
                string project = Path.Combine(game.Key, Path.GetFileName(game.Key) + ".csproj");
                if (!File.Exists(project)) project = game.First().Path;
                Log($"=== Known shader repairs: {Path.GetFileName(game.Key)} ===");
                try
                {
                    var results = await Task.Run(() => KnownShaderRepair.RepairAsync(project, Log, token), token);
                    Log($"Known shaders: {results.Count(r => r.Status == "Repaired")} repaired, " +
                        $"{results.Count(r => r.Status == "UpToDate")} up to date, " +
                        $"{results.Count(r => r.Status == "Unsupported")} unrecognized, " +
                        $"{results.Count(r => r.Status == "Conflict")} custom-source conflicts. Rebuild the game to deploy updated shaders.");
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) { Log($"Shader repair failed: {ex.Message}"); }
            }
        });
    }


    private async void ConvertXnbEffects_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more game rows first."); return; }

        string script = EffectsScriptBox.Text.Trim();
        string extractor = EffectsExtractorBox.Text.Trim();
        if (!File.Exists(script)) { Log($"Effect conversion script not found: {script}"); return; }
        if (!File.Exists(extractor)) { Log($"XNB extractor not found: {extractor}"); return; }

        // Group projects belonging to the same archive game to avoid duplicate conversion.
        string archive = Path.GetFullPath(RootBox.Text.Trim());
        string prefix = Path.TrimEndingDirectorySeparator(archive) + Path.DirectorySeparatorChar;
        var folders = selected.Select(p => Path.GetFullPath(p.Path))
            .Select(path => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                ? Path.Combine(archive, Path.GetRelativePath(archive, path)
                    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0])
                : Path.GetDirectoryName(path)!)
            .Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        bool recurse = EffectsRecurseBox.IsChecked == true;
        bool overwrite = EffectsOverwriteBox.IsChecked == true;
        if (overwrite && MessageBox.Show(this,
            $"Overwrite existing extracted effects in {folders.Count} selected game(s)?",
            "Confirm overwrite", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
            return;

        await ExecuteAsync(async token =>
        {
            int success = 0, failed = 0, skipped = 0;
            foreach (string folder in folders)
            {
                token.ThrowIfCancellationRequested();
                string shaders = Path.Combine(folder, "Content", "Shaders");
                string content = Directory.Exists(shaders) ? shaders : Path.Combine(folder, "Content");
                if (!Directory.Exists(content))
                {
                    Log($"SKIP: No Content folder: {folder}"); skipped++; continue;
                }
                Log($"=== Converting XNB effects: {Path.GetFileName(folder)} ===");
                Log($"Input: {content}");
                // Explicit PowerShell array avoids -File argument-binding ambiguity.
                static string Q(string value) => "'" + value.Replace("'", "''") + "'";
                string command = "$ErrorActionPreference='Stop'; & " + Q(script) +
                    " -InputPath " + Q(content) +
                    " -ExtractorExe " + Q(extractor) +
                    " -ExtractorArguments @('--input','{input}','--output','{output}','--loader','--parser','--extract')" +
                    (recurse ? " -Recurse" : "") + (overwrite ? " -Overwrite" : "") +
                    "; if (-not $?) { exit 1 }";
                string encoded = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(command));
                var args = new List<string> { "-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-EncodedCommand", encoded };
                try
                {
                    int code = await ProcessRunner.RunAsync("powershell.exe", args, folder, Log, token);
                    if (code == 0) { success++; Log("Conversion finished."); }
                    else { failed++; Log($"Conversion failed (exit {code}): {folder}"); }
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) { failed++; Log($"Conversion failed: {ex.Message}"); }
            }
            Log($"XNB effects complete: {success} succeeded, {failed} failed, {skipped} skipped.");
        });
    }

    private async void Fixer_Click(object sender, RoutedEventArgs e)
    {
        var project = Selected().FirstOrDefault();
        if (project is null) { Log("Select a project first."); return; }
        if (!File.Exists(ScriptBox.Text)) { Log("PowerShell script not found."); return; }
        var confirm = MessageBox.Show($"Run this script?\\n{ScriptBox.Text}\\n\\nProject: {project.Name}\\n\\nReview the script parameters first. The initial integration passes no project parameters.", "Confirm fixer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (confirm != MessageBoxResult.Yes) return;
        await ExecuteAsync(async token =>
        {
            var code = await ProcessRunner.RunAsync("powershell.exe",
                ["-NoProfile", "-ExecutionPolicy", "Bypass", "-File", ScriptBox.Text],
                Path.GetDirectoryName(project.Path)!, Log, token);
            Log($"Fixer exit code: {code}");
        });
    }
}
