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
    private readonly ProjectStatusStore _projectStatuses;
    private bool _torrentShutdown;
    private bool _refreshingTorrents;
    private bool _architectureReady, _updatingArchitecture;
    private readonly ConcurrentQueue<string> _pendingLog = new();
    private readonly DispatcherTimer _logTimer;
    private readonly DispatcherTimer _membershipTimer;
    private string? _membershipStamp, _membershipError;
    private bool _checkingMembership, _membershipPending;
    public MainWindow()
    {
        InitializeComponent();
        _projectStatuses = new(Path.Combine(Path.GetDirectoryName(LibraryStorage.SettingsPath)!, "project-status-v1"), Log);
        WindowAppearance.SizeToWorkArea(this);
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
        System.Windows.Data.CollectionViewSource.GetDefaultView(_projects).Filter = item => {
            string query = GameSearchBox.Text.Trim();
            return item is GameProject project && (query.Length == 0 ||
                project.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                project.Path.Contains(query, StringComparison.OrdinalIgnoreCase));
        };
        PreviewKeyDown += (_, e) => {
            if (e.Key == System.Windows.Input.Key.F && System.Windows.Input.Keyboard.Modifiers == System.Windows.Input.ModifierKeys.Control) {
                GameSearchBox.Focus(); GameSearchBox.SelectAll(); e.Handled = true;
            }
        };
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
        FolderScanStatus.Text="Scanning Folders…";
        var progress=new Progress<ProjectScanProgress>(p=> {
            if (_scanCts != scan || scan.IsCancellationRequested) return;
            FolderScanProgress.IsIndeterminate=p.Discovering;
            FolderScanProgress.Maximum=Math.Max(1,p.TotalFolders); FolderScanProgress.Value=p.FoldersScanned;
            FolderScanStatus.Text=p.Discovering ? $"Scanning Folders… {p.FoldersScanned:N0} Scanned · {p.ProjectsFound:N0} Projects" : $"Scanning Folders: {p.FoldersScanned:N0} / {p.TotalFolders:N0} · {p.ProjectsFound:N0} Projects";
            FolderScanStatus.ToolTip=p.Folder;
        });
        try
        {
            string scanCache = Path.Combine(Path.GetDirectoryName(LibraryStorage.SettingsPath)!, "project-scan-v1");
            var projects=await Task.Run(()=>ProjectScanner.Scan(root,scan.Token,progress,scanCache));
            if (_scanCts != scan || scan.IsCancellationRequested) return;
            foreach (var project in projects)
            {
                _projectStatuses.Track(project);
                _projects.Add(project);
            }
            await RefreshSolutionMembershipAsync(true);
            FolderScanProgress.IsIndeterminate=false; FolderScanProgress.Value=FolderScanProgress.Maximum;
            FolderScanStatus.Text=$"Scan Complete · {_projects.Count:N0} Projects"; Log($"Found {_projects.Count} projects.");
        }
        catch (OperationCanceledException) { if (_scanCts == scan) FolderScanStatus.Text="Scan Cancelled"; }
        catch (Exception ex) { if (_scanCts == scan) { FolderScanStatus.Text="Scan Failed: "+ex.Message; Log(FolderScanStatus.Text); } }
        finally { if (_scanCts == scan) { _scanCts=null; FolderScanProgress.IsIndeterminate=false; CancelScanButton.IsEnabled=false; } }
    }
    private void GameSearch_Changed(object sender, TextChangedEventArgs e)
    {
        if (ProjectsGrid?.ItemsSource != null)
            System.Windows.Data.CollectionViewSource.GetDefaultView(_projects).Refresh();
    }
    private void ClearGameSearch_Click(object sender, RoutedEventArgs e) { GameSearchBox.Clear(); GameSearchBox.Focus(); }
    private void GameSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Escape) { GameSearchBox.Clear(); e.Handled = true; }
        else if (e.Key == System.Windows.Input.Key.Enter && ProjectsGrid.Items.Count > 0) {
            ProjectsGrid.SelectedItems.Clear(); ProjectsGrid.SelectedItem = ProjectsGrid.Items[0];
            ProjectsGrid.ScrollIntoView(ProjectsGrid.SelectedItem); ProjectsGrid.Focus(); e.Handled = true;
        }
    }
    private void CancelScan_Click(object sender, RoutedEventArgs e) => _scanCts?.Cancel();
    private void Scan_Click(object sender, RoutedEventArgs e) => Scan();

    private void CloseWindow_Click(object sender, RoutedEventArgs e) => Close();

    private async void FullShutdown_Click(object sender, RoutedEventArgs e)
    {
        if (Application.Current is App app)
            await app.ExitAsync();
    }

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
    private async void MergeCatalogue_Click(object sender, RoutedEventArgs e)
    {
        if (_cts != null) { Log("Wait for the current operation to finish."); return; }
        string script = _storage.ScriptPath("Merge-CatalogueSubmissions.ps1");
        if (!File.Exists(script))
        {
            var chooser = new OpenFileDialog { Title = "Locate Merge-CatalogueSubmissions.ps1", Filter = "PowerShell (*.ps1)|*.ps1" };
            if (chooser.ShowDialog(this) != true) return;
            script = chooser.FileName;
        }
        var dialog = new Window {
            Title = "Merge Catalogue Submissions", Owner = this, Width = 1050, Height = 650,
            MinWidth = 750, MinHeight = 450, WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(17,24,39)), Foreground = Brushes.White
        };
        var layout = new DockPanel { Margin = new Thickness(14) };
        dialog.Content = layout;
        var header = new StackPanel(); DockPanel.SetDock(header, Dock.Top); layout.Children.Add(header);
        header.Children.Add(new TextBlock { Text = "Catalogue pull requests", FontSize = 19, Margin = new Thickness(0,0,0,7) });
        header.Children.Add(new TextBlock { Text = "Preview first. Select READY rows to merge. Duplicate games and failed checks are excluded.",
            Foreground = Brushes.LightGray, Margin = new Thickness(0,0,0,12) });
        var footer = new StackPanel(); DockPanel.SetDock(footer, Dock.Bottom); layout.Children.Add(footer);
        var status = new TextBlock { Text = "Select Refresh to retrieve open pull requests.", Foreground = Brushes.LightGray, Margin = new Thickness(0,8,0,8) };
        footer.Children.Add(status);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        footer.Children.Add(actions);
        var refresh = new Button { Content = "Refresh / Preview", MinWidth = 135, Margin = new Thickness(5) };
        var merge = new Button { Content = "Merge selected READY PRs", MinWidth = 185, Margin = new Thickness(5), IsEnabled = false };
        var close = new Button { Content = "Close", MinWidth = 80, Margin = new Thickness(5) };
        actions.Children.Add(refresh); actions.Children.Add(merge); actions.Children.Add(close);
        var grid = new DataGrid {
            AutoGenerateColumns = false, IsReadOnly = true, SelectionMode = DataGridSelectionMode.Extended,
            SelectionUnit = DataGridSelectionUnit.FullRow, CanUserAddRows = false,
            Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(24,34,52)),
            Foreground = Brushes.White, RowHeaderWidth = 0
        };
        grid.Columns.Add(new DataGridTextColumn { Header = "PR", Binding = new System.Windows.Data.Binding("Number"), Width = 55 });
        grid.Columns.Add(new DataGridTextColumn { Header = "Status", Binding = new System.Windows.Data.Binding("Status"), Width = 90 });
        grid.Columns.Add(new DataGridTextColumn { Header = "Author", Binding = new System.Windows.Data.Binding("Author"), Width = 150 });
        grid.Columns.Add(new DataGridTextColumn { Header = "Game / Submission", Binding = new System.Windows.Data.Binding("Title"), Width = new DataGridLength(1, DataGridLengthUnitType.Star) });
        grid.Columns.Add(new DataGridTextColumn { Header = "Reason", Binding = new System.Windows.Data.Binding("Reason"), Width = 230 });
        layout.Children.Add(grid);
        var entries = new ObservableCollection<CataloguePrRow>();
        grid.ItemsSource = entries;
        grid.SelectionChanged += (_, _) => merge.IsEnabled = grid.SelectedItems.Cast<CataloguePrRow>().Any(p => p.Status == "READY");
        string report = Path.Combine(Path.GetTempPath(), "buildmanager-catalogue-" + Guid.NewGuid().ToString("N") + ".json");
        bool busy = false;
        async Task RefreshAsync()
        {
            if (busy) return;
            busy = true; refresh.IsEnabled = false; merge.IsEnabled = false;
            status.Text = "Checking GitHub permissions, duplicates and validation…";
            try {
                if (File.Exists(report)) File.Delete(report);
                int code = await ProcessRunner.RunAsync("powershell.exe",
                    ["-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-File", script, "-ReportPath", report],
                    Path.GetDirectoryName(script)!, Log, CancellationToken.None);
                if (code != 0 || !File.Exists(report)) throw new InvalidOperationException("Catalogue preview failed; inspect the Build Manager log.");
                string json = await File.ReadAllTextAsync(report);
                var parsed = System.Text.Json.JsonSerializer.Deserialize<List<CataloguePrRow>>(json,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
                entries.Clear(); foreach (var row in parsed.OrderByDescending(p => p.Number)) entries.Add(row);
                status.Text = $"{entries.Count} open PR(s); {entries.Count(p => p.Status == "READY")} ready. Ctrl+click to select multiple rows.";
            } catch (Exception ex) { status.Text = "Preview failed: " + ex.Message; Log(status.Text); }
            finally { busy = false; refresh.IsEnabled = true; }
        }
        refresh.Click += async (_, _) => await RefreshAsync();
        merge.Click += async (_, _) => {
            if (busy) return;
            int[] numbers = grid.SelectedItems.Cast<CataloguePrRow>().Where(p => p.Status == "READY").Select(p => p.Number).Distinct().ToArray();
            if (numbers.Length == 0) return;
            if (MessageBox.Show(dialog, $"Administrator squash-merge {numbers.Length} selected PR(s): {string.Join(", ", numbers.Select(n => "#" + n))}?\n\nThis bypasses approval rules and cannot be undone automatically.",
                "Confirm catalogue merges", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            busy = true; refresh.IsEnabled = false; merge.IsEnabled = false;
            status.Text = "Merging selected pull requests; see Build Manager log…";
            try {
                var args = new List<string> { "-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-File", script, "-Apply", "-Confirm:$false", "-Numbers" };
                args.Add(string.Join(",", numbers));
                int code = await ProcessRunner.RunAsync("powershell.exe", args, Path.GetDirectoryName(script)!, Log, CancellationToken.None);
                status.Text = code == 0 ? "Merge run completed. Refreshing…" : $"Merge run returned exit code {code}; inspect the log.";
            } catch (Exception ex) { Log("Catalogue merge error: " + ex.Message); status.Text = ex.Message; }
            finally { busy = false; refresh.IsEnabled = true; await RefreshAsync(); }
        };
        close.Click += (_, _) => dialog.Close();
        dialog.Closed += (_, _) => { try { if (File.Exists(report)) File.Delete(report); } catch { } };
        dialog.Show();
        await RefreshAsync();
    }

    private sealed class CataloguePrRow
    {
        public int Number { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Status { get; set; } = "";
        public string Reason { get; set; } = "";
    }

    private void TorrentCatalogue_Click(object sender, RoutedEventArgs e)
    {
        OpenTorrentManager();
    }

    private TorrentWindow OpenTorrentManager(bool show = true)
    {
        if (_torrentWindow != null) { if(show) { _torrentWindow.Show(); if(_torrentWindow.WindowState==WindowState.Minimized)_torrentWindow.WindowState=WindowState.Normal; _torrentWindow.Activate(); } return _torrentWindow; }
        string root = Path.GetFullPath(RootBox.Text.Trim());
        string? folder = Selected().FirstOrDefault() is { } project ? Path.GetDirectoryName(Path.GetFullPath(project.Path)) : null;
        string prefix = Path.TrimEndingDirectorySeparator(root) + Path.DirectorySeparatorChar;
        if (folder != null && folder.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            folder = Path.Combine(root, Path.GetRelativePath(root, folder).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0]);
        _storage.Archive = root;
        _torrentWindow = new TorrentWindow(root, folder, _storage.TorrentCache, _storage.Games, Selected().FirstOrDefault() is { } chosen ? _storage.GameFolder(chosen.Path) : null, (Application.Current as App)?.Torrents) { Owner = this };
        _torrentWindow.ProjectInstalled += archive => { RootBox.Text = archive; Scan(); };
        _torrentWindow.StatusChanged += UpdateTorrentStatus;
        _torrentWindow.ProjectSelected += project => SelectTorrentGame(project.FolderName, project.Name);
        _torrentWindow.Closed += (_, _) => { _torrentWindow = null; StopTorrentsButton.IsEnabled=false; };
        StopTorrentsButton.IsEnabled=true;
        if(show)_torrentWindow.Show();
        return _torrentWindow;
    }

    private void UpdateTorrentStatus(TorrentStatusSnapshot snapshot)
    {
        TorrentSummary.Text=snapshot.Summary;
        TorrentActivity.Text=snapshot.Activity;
        TorrentActivity.ToolTip=snapshot.Activity;
        string? selectedHash = (TorrentTransfersGrid.SelectedItem as TorrentStatus)?.InfoHash;
        _refreshingTorrents = true;
        try {
            TorrentTransfersGrid.ItemsSource = snapshot.Transfers;
            if (!string.IsNullOrEmpty(selectedHash))
                TorrentTransfersGrid.SelectedItem = snapshot.Transfers.FirstOrDefault(t => t.InfoHash == selectedHash);
        }
        finally { _refreshingTorrents = false; }
    }
    private void TorrentTransfersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_refreshingTorrents && e.AddedItems.Count > 0 && TorrentTransfersGrid.SelectedItem is TorrentStatus transfer)
            SelectTorrentGame(transfer.FolderName, transfer.Name);
    }

    private void SelectTorrentGame(string folderName, string name)
    {
        // Prefer the package folder (including hashed playable-game folders) over a title match.
        var matches = string.IsNullOrWhiteSpace(folderName) ? Array.Empty<GameProject>() : _projects.Where(p =>
            string.Equals(Path.GetRelativePath(RootBox.Text.Trim(), p.Path).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0], folderName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(Path.GetFileName(_storage.GameFolder(p.Path)), folderName, StringComparison.OrdinalIgnoreCase)).ToArray();
        var project = matches.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase))
            ?? (matches.Length == 1 ? matches[0] : null);
        if (project == null && matches.Length == 0) {
            var named = _projects.Where(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)).ToArray();
            if (named.Length == 1) project = named[0];
        }
        if (project == null) return;
        if (!ProjectsGrid.Items.Contains(project)) GameSearchBox.Clear();
        ProjectsGrid.SelectedItems.Clear();
        ProjectsGrid.SelectedItem = project;
        ProjectsGrid.ScrollIntoView(project);
    }
    private void ViewTorrents_Click(object sender, RoutedEventArgs e) => OutputTabs.SelectedIndex=1;
    private async void StartTorrents_Click(object sender, RoutedEventArgs e)
    {
        StartTorrentsButton.IsEnabled=false;
        OutputTabs.SelectedIndex=1;
        try
        {
            var manager=OpenTorrentManager(show:false);
            if(await manager.StartSavedTorrentsAsync()==0)OpenTorrentManager();
        }
        catch(Exception error) { TorrentActivity.Text=error.GetBaseException().Message;Log(error.GetBaseException().Message); }
        finally { StartTorrentsButton.IsEnabled=true; }
    }
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

    private async void RunGameButton_Click(object sender, RoutedEventArgs e)
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
            string platform = Value(TargetBox);
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
            var launch = GameProcessLauncher.Start(executable,GameProcessLauncher.DefaultLogDirectory,Log);
            project.Status = $"In progress: Game running (PID {launch.ProcessId}); log: {launch.LogPath}";
            int exitCode = await launch.Completion;
            project.Status = exitCode == 0 ? "Game exited normally (exit 0)"
                : $"Game exited with error {exitCode}; log: {launch.LogPath}";
        }
        catch (Exception ex)
        {
            project.Status = $"Game launch failed: {ex.GetBaseException().Message}";
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
            TargetBox.Text = architecture;
            string runtime = RuntimeBox.Text.Trim();
            int separator = runtime.LastIndexOf('-');
            if (architecture != "AnyCPU" && separator > 0 &&
                runtime[(separator + 1)..] is "x86" or "x64" or "arm64" or "arm")
                RuntimeBox.Text = runtime[..(separator + 1)] + architecture;
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
                $"-p:Configuration={Value(ConfigBox)}", $"-p:Platform={Value(TargetBox)}",
                "-getProperty:TargetFramework,TargetFrameworks,Configuration,Platform,PlatformTarget,Platforms,RuntimeIdentifier,RuntimeIdentifiers,OutputPath,BaseIntermediateOutputPath,ProjectAssetsFile" };
            args.AddRange(_storage.BuildArguments(project.Path).Select(EscapeMsbuildPropertyArgument));
            await ProcessRunner.RunAsync("dotnet", args,
                _storage.SdkWorkingDirectory(project.Path), Log, token);
        });
    }
    private async void Detect_Click(object sender, RoutedEventArgs e)
    {
        var selected = Selected();
        if (selected.Count == 0) { Log("Select one or more projects first."); return; }
        string exe = UseVsBox.IsChecked == true ? MsbuildPathBox.Text.Trim() : "dotnet";
        if (UseVsBox.IsChecked == true && !File.Exists(exe)) { Log("Set a valid MSBuild.exe path."); return; }
        string config = Value(ConfigBox), platform = Value(TargetBox), target = Value(TargetBox);
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
                        target, framework, runtime, exe, Log, token, _storage.BuildArguments(project.Path).Select(EscapeMsbuildPropertyArgument).ToList(),
                        exe == "dotnet" ? _storage.SdkWorkingDirectory(project.Path) : null);
                    foreach (var issue in report.Issues)
                        Log($"[{issue.Severity}] {issue.Code}: {issue.Detail}\\n  Suggested action: {issue.Recommendation}");
                    int errorCount = report.Issues.Count(i => i.Severity == "Error");
                    int warningCount = report.Issues.Count(i => i.Severity == "Warning");
                    project.Status = $"Configuration checked ({config} / {platform}); {errorCount} error(s), {warningCount} warning(s)";
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) { project.Status = $"Configuration check failed: {ex.Message}"; Log($"ERROR: {ex.Message}"); }
            }
        });
    }
    // MSBuild treats commas and semicolons inside -p: values as property separators.
    // Escape them for MSBuild's own parser (independent of Windows argument quoting).
    private static string EscapeMsbuildPropertyArgument(string argument)
    {
        if (!(argument.StartsWith("-p:", StringComparison.OrdinalIgnoreCase) ||
              argument.StartsWith("/p:", StringComparison.OrdinalIgnoreCase) ||
              argument.StartsWith("-property:", StringComparison.OrdinalIgnoreCase) ||
              argument.StartsWith("/property:", StringComparison.OrdinalIgnoreCase)))
            return argument;

        int equals = argument.IndexOf('=');
        if (equals < 0) return argument;
        string prefix = argument[..(equals + 1)];
        string value = argument[(equals + 1)..];
        return prefix + value.Replace(",", "%2C").Replace(";", "%3B");
    }

    private bool _loadingBuildPreferences;
    private void BuildProfile_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (_loadingBuildPreferences) return;
        UpdateBuildProfileOptions();
        if (!_architectureReady) return;
        var preferences = new ProjectBuildPreferences(Value(GameFrameworkBox), Value(NetworkBackendBox),
            Value(MonoGamePlatformBox), MonoGameVersionBox.Text.Trim());
        foreach (var project in Selected())
        {
            try { preferences.Save(project.Path); }
            catch (Exception error) { Log($"Could not save build preferences for {project.Name}: {error.Message}"); }
        }
    }
    private void UpdateBuildProfileOptions()
    {
        if(GameFrameworkBox==null || NetworkBackendBox==null || MonoGamePlatformBox==null || InstallOverlayButton==null || ProjectsGrid==null || NoRestoreBox==null)return;
        var selected=Selected();var capabilities=selected.Select(p=>GameBuildProfiles.Inspect(p.Path)).ToArray();
        bool managed=capabilities.Length>0 && capabilities.All(c=>c.ManagedSource && c.Framework is "FNA" or "MonoGame" or "XNA");
        foreach(var item in GameFrameworkBox.Items.Cast<ComboBoxItem>().Skip(1)) {
            item.IsEnabled=managed && (item.Content.ToString()!="XNA → FNA" || capabilities.All(c=>c.Framework is "XNA" or "FNA"));
            item.ToolTip=item.IsEnabled?"Saved per-game build preference; source project references are preserved.":"Requires compatible .NET 9+ Windows source projects. Binary-only and legacy XNA projects cannot use this option.";
        }
        foreach(var item in NetworkBackendBox.Items.Cast<ComboBoxItem>().Where(i=>i.Content.ToString() is "NetStub" or "Steamworks"))item.IsEnabled=managed;
        string mode=Value(GameFrameworkBox);
        MonoGamePlatformBox.IsEnabled=mode=="MonoGame" || mode=="Project default"&&capabilities.Length>0&&capabilities.All(c=>c.Framework=="MonoGame");
        MonoGameVersionBox.IsEnabled=MonoGamePlatformBox.IsEnabled;
        MonoGamePlatformPanel.Visibility=MonoGameVersionPanel.Visibility=MonoGamePlatformBox.IsEnabled?Visibility.Visible:Visibility.Collapsed;
        XnaConversionPanel.Visibility=mode=="XNA → FNA"?Visibility.Visible:Visibility.Collapsed;
        ConvertXna3BuildBox.IsEnabled=mode=="XNA → FNA";ConvertSceneBuildBox.IsEnabled=mode=="XNA → FNA";
        bool overlay=capabilities.Length>0 && capabilities.All(c=>c.Overlay) && mode!="XNA → FNA";
        InstallOverlayButton.IsEnabled=overlay;
        OverlayAvailabilityNote.Text=mode=="XNA → FNA"?"F1 is unavailable during XNA conversion. Convert/rebuild a supported Windows source project, then install F1 for its source framework.":capabilities.Length==1?capabilities[0].Reason:overlay?"F1 available for selected FNA/MonoGame Windows source projects.":"F1 disabled: select supported .NET 9+ Windows FNA/MonoGame source projects. Legacy XNA and binary-only ports are unsupported.";
        NoRestoreBox.IsEnabled=capabilities.Length==0 || capabilities.All(c=>c.Framework is "Unknown" or "XNA");
        BuildProfileNote.Text=capabilities.Length==0?"Select projects to see compatible options.":"Detected: "+string.Join(", ",capabilities.Select(c=>c.Framework).Distinct())+". Framework/network choices are saved per game on this computer. Managed profiles require restore. NetStub is offline compatibility; Steamworks needs a working Steam integration. Source/playable submissions keep their existing format.";
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
                string context = $"{Value(ConfigBox)} / {Value(TargetBox)} / {Value(GameFrameworkBox)} / {Value(NetworkBackendBox)}";
                string operation = char.ToUpperInvariant(action[0]) + action[1..];
                project.Status = $"In progress: {operation} ({context})";
                var warnings = new ConcurrentDictionary<string, byte>();
                var errors = new ConcurrentDictionary<string, byte>();
                void BuildOutput(string line)
                {
                    Log(line);
                    if (System.Text.RegularExpressions.Regex.IsMatch(line, @"\bwarning\s+\w+\s*:", System.Text.RegularExpressions.RegexOptions.IgnoreCase)) warnings.TryAdd(line.Trim(), 0);
                    if (System.Text.RegularExpressions.Regex.IsMatch(line, @"\berror\s+\w+\s*:", System.Text.RegularExpressions.RegexOptions.IgnoreCase)) errors.TryAdd(line.Trim(), 0);
                }
                try
                {
                string core=Path.GetDirectoryName(Path.GetFullPath(_storage.Solution))!;
                var profile=GameBuildProfiles.Prepare(project.Path,Value(GameFrameworkBox),Value(NetworkBackendBox),Value(MonoGamePlatformBox),MonoGameVersionBox.Text.Trim(),core,_storage.BuildCache);
                Log($"Build profile: {profile.Framework} / {profile.Network}. Project and submission metadata are unchanged.");
                if(profile.ConvertXna && action is not ("clean" or "restore")) {
                    if(ConvertXna3BuildBox.IsChecked!=true && ConvertSceneBuildBox.IsChecked!=true)throw new InvalidOperationException("Choose at least one XNA source conversion option.");
                    string repair=Path.Combine(AppContext.BaseDirectory,"scripts","repair_xna3.py");if(!File.Exists(repair))repair=_storage.ScriptPath("repair_xna3.py");
                    if(!File.Exists(repair))throw new FileNotFoundException("The bundled XNA source converter is missing.",repair);
                    Log("Source conversion backup: "+GameBuildProfiles.BackupConversion(project.Path));
                    var conversion=new List<string>{"-3","-X","utf8",repair,Path.GetDirectoryName(project.Path)!};
                    if(ConvertXna3BuildBox.IsChecked==true)conversion.Add("--convert-xna3");if(ConvertSceneBuildBox.IsChecked==true)conversion.Add("--cron-bloom");
                    int converted=await ProcessRunner.RunAsync("py.exe",conversion,Path.GetDirectoryName(project.Path)!,Log,token);
                    if(converted!=0)throw new InvalidOperationException("XNA source conversion failed; build was not started. Restore from the logged backup if needed.");
                }
                var args = new List<string>();
                bool vs = UseVsBox.IsChecked == true;
                string exe = vs ? MsbuildPathBox.Text.Trim() : "dotnet";
                if (vs && !File.Exists(exe)) throw new FileNotFoundException("Specify an existing MSBuild.exe path.", exe);
                if (!vs) args.Add(action == "restore" ? "restore" : "build");
                args.Add(project.Path);
                _storage.Archive = Path.GetFullPath(RootBox.Text.Trim());
                args.AddRange(_storage.BuildArguments(project.Path).Select(EscapeMsbuildPropertyArgument));
                if (vs) args.Add(action switch { "rebuild" => "-t:Rebuild", "clean" => "-t:Clean", "restore" => "-t:Restore", _ => "-t:Build" });
                else if (action == "rebuild") args.Add("-t:Rebuild");
                else if (action == "clean") args.Add("-t:Clean");
                args.Add($"-p:Configuration={Value(ConfigBox)}");
                args.Add($"-p:Platform={Value(TargetBox)}");
                args.Add($"-p:PlatformTarget={Value(TargetBox)}");
                args.AddRange(profile.Arguments);
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
                if (action != "restore" && !vs && NoRestoreBox.IsEnabled && NoRestoreBox.IsChecked == true) args.Add("--no-restore");
                // Apply MSBuild escaping to every property, including the layout paths.
                // This prevents commas in game names from becoming extra properties.
                for (int i = 0; i < args.Count; i++)
                    args[i] = EscapeMsbuildPropertyArgument(args[i]);
                Log($"> {exe} {string.Join(" ", args.Select(a => a.Contains(' ') ? "\"" + a + "\"" : a))}");
                    string workingDirectory = vs ? Path.GetDirectoryName(project.Path)! : _storage.SdkWorkingDirectory(project.Path);
                    int code = await ProcessRunner.RunAsync(exe, args, workingDirectory, BuildOutput, token);
                    project.Status = code == 0
                        ? $"{operation} succeeded ({context}); {warnings.Count} warning(s)" + (action == "clean" ? "; outputs cleaned — rebuild before running" : "")
                        : $"{operation} failed ({context}); exit {code}, {errors.Count} error(s), {warnings.Count} warning(s) — see build log";
                }
                catch (OperationCanceledException) { project.Status = $"{operation} cancelled ({context})"; throw; }
                catch (Exception ex) { project.Status = $"{operation} failed ({context}): {ex.GetBaseException().Message}"; throw; }
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
    private void ConvertXnaBinary_Click(object sender, RoutedEventArgs e) => new XnaConversionWindow(_storage, Selected().FirstOrDefault()?.Path) { Owner=this }.ShowDialog();
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
        var selected = Selected();
        var project = ProjectsGrid.SelectedItem as GameProject;
        if (_architectureReady && selected.Count == 1 && project != null)
        {
            _loadingBuildPreferences = true;
            try
            {
                var preferences = ProjectBuildPreferences.Load(project.Path);
                void Choose(ComboBox box, string value) => box.SelectedItem = box.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content?.ToString() == value) ?? box.Items[0];
                Choose(GameFrameworkBox, preferences.Framework);
                Choose(NetworkBackendBox, preferences.Network);
                Choose(MonoGamePlatformBox, preferences.MonoPlatform);
                MonoGameVersionBox.Text = preferences.MonoVersion;
            }
            catch (Exception error)
            {
                GameFrameworkBox.SelectedIndex = NetworkBackendBox.SelectedIndex = MonoGamePlatformBox.SelectedIndex = 0;
                Log($"Could not load build preferences for {project.Name}: {error.Message}");
            }
            finally { _loadingBuildPreferences = false; }
            _updatingArchitecture = true;
            try
            {
                // Multiple conditional choices cannot populate a single build option.
                string architecture = project.PlatformTarget.Trim();
                if (architecture is "x86" or "x64" or "AnyCPU")
                {
                    TargetBox.Text = architecture;
                }
                if (!project.RuntimeIdentifier.Contains(';') && !project.RuntimeIdentifier.Contains("$("))
                    RuntimeBox.Text = project.RuntimeIdentifier;
                if (!project.TargetFramework.Contains(';') && !project.TargetFramework.Contains("$("))
                    FrameworkBox.Text = project.TargetFramework;
            }
            finally { _updatingArchitecture = false; }
        }
        UpdateBuildArchitectureRows();
        UpdateBuildProfileOptions();
        GameSelectionCount.Text = selected.Count == 0 ? "Select games to build or repair" :
            $"{selected.Count} game{(selected.Count == 1 ? "" : "s")} selected";
        OpenSelectedFolderButton.IsEnabled = project != null;
        BumpVersionButton.IsEnabled = selected.Count == 1;
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

    private void BumpVersion_Click(object sender, RoutedEventArgs e)
    {
        if (_cts != null) { Log("Wait for the current operation to finish before changing the version."); return; }
        var selected = Selected();
        if (selected.Count != 1) return;
        var project = selected[0];
        Version current;
        try { current = ProjectVersionUpdater.Read(project.Path); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message, "Game version", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

        var dialog = new Window
        {
            Title = $"Bump Version — {project.Name}",
            Owner = this,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Width = 420,
            SizeToContent = SizeToContent.Height,
            ResizeMode = ResizeMode.NoResize,
            ShowInTaskbar = false,
            Background = Background,
            Foreground = Foreground
        };
        var content = new StackPanel { Margin = new Thickness(18) };
        content.Children.Add(new TextBlock { Text = $"Current version: {current}" });
        content.Children.Add(new TextBlock { Text = "New executable version", Margin = new Thickness(0, 12, 0, 4) });
        var version = new TextBox
        {
            Text = current.Build < ushort.MaxValue
                ? $"{current.Major}.{current.Minor}.{Math.Max(0, current.Build) + 1}"
                : current.ToString()
        };
        content.Children.Add(version);
        content.Children.Add(new TextBlock { Text = "Rebuild the game to apply the new version.", Margin = new Thickness(0, 8, 0, 0) });
        var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 14, 0, 0) };
        var save = new Button { Content = "Save Version", IsDefault = true, MinWidth = 105, Margin = new Thickness(0, 0, 8, 0) };
        var cancel = new Button { Content = "Cancel", IsCancel = true, MinWidth = 75 };
        buttons.Children.Add(save);
        buttons.Children.Add(cancel);
        content.Children.Add(buttons);
        save.Click += (_, _) =>
        {
            try
            {
                ProjectVersionUpdater.Apply(project.Path, version.Text);
                project.Status = "Version saved; rebuild";
                Log($"Saved executable version {version.Text.Trim()} for {project.Name}. Rebuild to apply it.");
                dialog.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(dialog, ex.Message, "Game version", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        };
        dialog.Content = content;
        dialog.Loaded += (_, _) => { version.Focus(); version.SelectAll(); };
        dialog.ShowDialog();
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

    private async Task<int> RunWarningFixerAsync(string project, bool preview, CancellationToken token)
    {
        Log("Warning Fixer: building to collect current diagnostics…");
        var output = new System.Collections.Concurrent.ConcurrentQueue<string>();
        var arguments = new[] { "build", project, "-c", Value(ConfigBox), "-p:Platform=" + Value(TargetBox), "-v:q", "-p:GenerateFullPaths=true" };
        int exit = await ProcessRunner.RunAsync("dotnet", arguments, Path.GetDirectoryName(project)!, line => output.Enqueue(line), token);
        string log = string.Join(Environment.NewLine, output);
        var plan = SourceWarningFixer.Plan(log, Path.GetDirectoryName(project)!);
        foreach (var file in plan.Files) Log($"{(preview ? "PREVIEW" : "FIX")}: {file.Path} · {file.Count} safe change(s)");
        foreach (var group in plan.Review.GroupBy(line => System.Text.RegularExpressions.Regex.Match(line, @"\b(?:CS\d+|SYSLIB\d+)\b").Value))
            Log($"Review: {group.Key} · {group.Count()} warning(s) require a source-specific decision.");
        string reportDirectory = Path.Combine(_storage.BuildCache, "warning-reports", LibraryStorage.ProjectKey(project));
        Directory.CreateDirectory(reportDirectory);
        string report = Path.Combine(reportDirectory, DateTime.Now.ToString("yyyyMMdd-HHmmss-fffffff") + ".txt");
        File.WriteAllText(report, log + Environment.NewLine + "REQUIRES REVIEW" + Environment.NewLine + string.Join(Environment.NewLine, plan.Review));
        Log("Warning report: " + report);
        if (!preview && plan.Files.Count > 0)
        {
            Log("Warning repair backups: " + SourceWarningFixer.Apply(plan, Path.GetDirectoryName(project)!));
            exit = await ProcessRunner.RunAsync("dotnet", arguments, Path.GetDirectoryName(project)!, Log, token);
        }
        Log($"Warning Fixer: {plan.Files.Sum(file => file.Count)} {(preview ? "planned" : "applied")} change(s); build exit {exit}.");
        if (exit != 0 && preview) Log("Preview completed; the game has build errors. See the report for details.");
        return preview ? 0 : exit;
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
                return new { Name = topFolder, Folder = Path.Combine(archive, topFolder), Project = path };
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

        var dialog = new RepairCenterWindow(games.Select(g => g.Name).ToArray()) { Owner = this };
        WindowAppearance.SizeToWorkArea(dialog);
        if (dialog.ShowDialog() != true) return;
        var selectedGames = games;
        var selectedFixers = dialog.SourceRepairs;
        var selectedSdkFixers = dialog.SdkRepairs;
        bool repairXna3 = selectedFixers.Contains("Repair-Xna3");
        bool repairCronBloom = selectedFixers.Contains("Repair-CronBloom");
        bool repairAmbiguities = selectedFixers.Contains("Repair-Ambiguities");
        bool repairWarnings = selectedFixers.Contains("Repair-Warnings");
        selectedFixers = selectedFixers.Where(fix => fix is not ("Repair-Xna3" or "Repair-CronBloom" or "Repair-Ambiguities" or "Repair-Warnings")).ToArray();
        string xna3Script = Path.Combine(AppContext.BaseDirectory, "scripts", "repair_xna3.py");
        bool sdkDoRestore = dialog.RestorePackages;
        bool sdkDoClean = dialog.CleanIntermediates;
        bool sdkFixRids = dialog.SetRuntimeIdentifiers;
        string sdkScript = _storage.ScriptPath("BuildSdkRepairs.ps1");
        if (selectedFixers.Length > 0 && !File.Exists(script)) {
            MessageBox.Show(this, "Source repair script is missing: " + script, "Repair Center"); return;
        }
        if (selectedSdkFixers.Length > 0 && !File.Exists(sdkScript)) {
            MessageBox.Show(this, "SDK repair script is missing: " + sdkScript, "Repair Center"); return;
        }
        string sdkFramework = string.IsNullOrWhiteSpace(FrameworkBox.Text)
            ? "net9.0-windows" : FrameworkBox.Text.Trim();
        string sdkPlatform = Value(TargetBox);
        if (sdkPlatform is not ("x64" or "x86")) sdkPlatform = "x64";
        bool whatIf = dialog.PreviewOnly;
        OutputTabs.SelectedIndex = 0;
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
                    int exit = 0;
                    if (!whatIf && (selectedFixers.Length > 0 || repairXna3 || repairCronBloom || repairAmbiguities || repairWarnings))
                        Log("Source backup: " + GameBuildProfiles.BackupConversion(game.Project));
                    if (repairXna3 || repairCronBloom) {
                        if (!File.Exists(xna3Script))
                            throw new FileNotFoundException("XNA 3.x repair script is missing.", xna3Script);
                        var arguments = new List<string> { "-3", "-X", "utf8", xna3Script, game.Folder };
                        if (repairXna3) arguments.Add("--convert-xna3");
                        if (repairCronBloom) arguments.Add("--cron-bloom");
                        if (whatIf) arguments.Add("--preview");
                        exit = await ProcessRunner.RunAsync("py.exe", arguments, game.Folder, Log, token);
                    }
                    if (exit == 0 && (selectedFixers.Length > 0 || selectedSdkFixers.Length > 0)) {
                        exit = await ProcessRunner.RunAsync("powershell.exe",
                            ["-NoProfile", "-NonInteractive", "-ExecutionPolicy", "Bypass", "-EncodedCommand", encoded],
                            game.Folder, Log, token);
                    }
                    if (exit == 0 && repairAmbiguities) {
                        string ambiguityScript = Path.Combine(AppContext.BaseDirectory, "scripts", "repair_ambiguities.py");
                        if (!File.Exists(ambiguityScript)) ambiguityScript = _storage.ScriptPath("repair_ambiguities.py");
                        if (!File.Exists(ambiguityScript)) throw new FileNotFoundException("Ambiguous type repair script is missing.", ambiguityScript);
                        var arguments = new List<string> { "-3", "-X", "utf8", ambiguityScript, "--project", game.Project,
                            "--platform", Value(TargetBox), "--configuration", Value(ConfigBox) };
                        if (!whatIf) arguments.Add("--apply");
                        exit = await ProcessRunner.RunAsync("py.exe", arguments, game.Folder, Log, token);
                    }
                    if (exit == 0 && repairWarnings)
                        exit = await RunWarningFixerAsync(game.Project, whatIf, token);
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
                Log(code switch
                {
                    0 => "Audio converted. Rebuild the game to copy updated content to bin.",
                    2 => "Skipped: no Song or SoundEffect XNBs. Use Repairs → Wave banks for XACT wave banks.",
                    _ => "Audio conversion had failures. Review the log and audio-conversion report."
                });
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
