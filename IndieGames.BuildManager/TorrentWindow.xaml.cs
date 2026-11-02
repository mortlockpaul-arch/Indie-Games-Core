using IndieGames.BuildManager.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace IndieGames.BuildManager;

public partial class TorrentWindow : Window
{
    private sealed class Row(TorrentProject project) : INotifyPropertyChanged
    {
        public TorrentProject Project { get; } = project;
        public string Status { get; private set; } = "Ready";
        public string Progress { get; private set; } = "";
        public string Rates { get; private set; } = "";
        public string Peers { get; private set; } = "";
        public string? LastError { get; set; }
        public void Update(ProjectTransfer transfer)
        {
            Status = transfer.Manager.State.ToString();
            Progress = $"{transfer.Manager.Progress:N1}%";
            Rates = $"{transfer.Manager.Monitor.DownloadRate / 1024.0:N0} / {transfer.Manager.Monitor.UploadRate / 1024.0:N0} KB/s";
            Peers = transfer.Manager.OpenConnections.ToString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }

    private readonly ObservableCollection<Row> _rows = [];
    private readonly ProjectTorrentService _service;
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };
    private readonly CancellationTokenSource _lifetime = new();
    private CancellationTokenSource? _operation;
    private Task? _operationTask;
    private List<SharePlan>? _preview;
    private bool _closing, _closed;
    private ProjectShareWindow? _shareWindow;
    private string _activity = "";
    private string? _shareName;
    private readonly List<TorrentProject> _createdShares=[];
    private int ShareKind
    {
        get => GameKindChoice?.IsChecked == true ? 1 : BothKindChoice?.IsChecked == true ? 2 : 0;
        set { SourceKindChoice.IsChecked=value == 0; GameKindChoice.IsChecked=value == 1; BothKindChoice.IsChecked=value == 2; }
    }
    public event Action<TorrentStatusSnapshot>? StatusChanged;
    private readonly TaskCompletionSource _shutdown = new(TaskCreationOptions.RunContinuationsAsynchronously);
    public event Action<string>? ProjectInstalled;

    public TorrentWindow(string archiveRoot, string? selectedFolder = null, string? stateDirectory = null, string? gamesRoot = null, string? gameFolder = null)
    {
        InitializeComponent();
        Title=AppVersion.WindowTitle(Title);
        ArchiveBox.Text = archiveRoot;
        GamesBox.Text = gamesRoot ?? LibraryStorage.Load().Games;
        GameFolderBox.Text = gameFolder ?? "";
        ShareFolderBox.Text = selectedFolder ?? "";
        CatalogueGrid.ItemsSource = _rows;
        _service = new ProjectTorrentService(stateDirectory ?? LibraryStorage.Load().TorrentCache);
        CacheNote.Text = "Packages and resume data: " + _service.StateDirectory;
        _timer.Tick += (_, _) => RefreshTransfers();
        _timer.Start();
    }

    private void Log(string text) { StatusBox.AppendText(text + Environment.NewLine); StatusBox.ScrollToEnd(); _shareWindow?.Log(text); _activity=text; PublishStatus(); }
    private void PublishStatus() => StatusChanged?.Invoke(new(_service.Transfers.Select(TorrentStatus.Capture).ToArray(),_activity));
    private async Task RunAsync(Func<CancellationToken, Task> action)
    {
        if (_closing) return;
        if (_operation != null) { Log("Wait for the current operation or cancel it."); return; }
        _operation = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token);
        _shareWindow?.SetBusy(true);
        try { _operationTask = action(_operation.Token); await _operationTask; }
        catch (OperationCanceledException) { Log("Operation cancelled. Partial downloads are kept for resume."); }
        catch (Exception error) { Log(error.GetBaseException().Message); if (_shareWindow != null && _preview == null) ShareSummary.Text="Choose A Folder And Refresh The Preview"; }
        finally { _operation.Dispose(); _operation = null; _operationTask = null; _shareWindow?.SetBusy(false); RefreshTransfers(); }
    }

    private void ConfigureNetwork()
    {
        if (_service.NetworkStarted) return;
        if (!int.TryParse(PortBox.Text, out int port) || port is < 1024 or > 65535) throw new InvalidOperationException("Choose a TCP/UDP port from 1024 to 65535.");
        _service.ListenPort = port; _service.AllowPortForwarding = PortForwardingBox.IsChecked == true;
    }

    private void RefreshTransfers()
    {
        foreach (var row in _rows)
        {
            var transfer = _service.Transfers.FirstOrDefault(t => t.Project.InfoHash.Equals(row.Project.InfoHash, StringComparison.OrdinalIgnoreCase));
            if (transfer != null)
            {
                row.Update(transfer);
                string? error = transfer.Manager.Error?.Exception.Message;
                if (error != null && row.LastError != error) { row.LastError = error; Log($"{row.Project.Name}: {error}"); }
            }
        }
        if (_service.NetworkStarted)
        {
            PortBox.IsEnabled = PortForwardingBox.IsEnabled = false;
            NetworkNote.Text = "Network settings apply until Stop Torrents. Internet seeding may require the selected port to be reachable.";
        }
        PublishStatus();
    }

    private IReadOnlyList<Row> SelectedRows() => CatalogueGrid.SelectedItems.Cast<Row>().ToArray() is { Length: > 0 } rows ? rows : throw new InvalidOperationException("Select one or more packages first.");
    private Row SelectedRow() => CatalogueGrid.SelectedItem as Row ?? throw new InvalidOperationException("Select a project in the catalogue first.");
    private ProjectTransfer TransferFor(Row row)
    {
        return _service.Transfers.FirstOrDefault(t => t.Project.InfoHash.Equals(row.Project.InfoHash, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException("Start downloading or seeding this project first.");
    }

    private async Task LoadCatalogueAsync(string location, CancellationToken token)
    {
        var catalogue = await TorrentCatalogue.LoadAsync(location, token);
        _rows.Clear(); foreach (var entry in catalogue.Projects) _rows.Add(new Row(entry));
        foreach (var transfer in _service.Transfers)
            if (!_rows.Any(r => r.Project.InfoHash.Equals(transfer.Project.InfoHash, StringComparison.OrdinalIgnoreCase))) _rows.Add(new Row(transfer.Project));
        CatalogueLocationBox.Text = location;
        Log($"Loaded {catalogue.Projects.Count} project(s). Select a project to download.");
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (File.Exists(_service.LocalCataloguePath)) await RunAsync(token => LoadCatalogueAsync(_service.LocalCataloguePath, token));
        else Log("Open a shared catalogue file/URL, or create a project share and export its catalogue.");
    }
    private async void LoadCatalogue_Click(object sender, RoutedEventArgs e) => await RunAsync(token => LoadCatalogueAsync(CatalogueLocationBox.Text.Trim(), token));
    private async void OpenCatalogue_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog { Filter = "Project catalogues (*.json)|*.json", Title = "Open shared project catalogue" };
        if (dialog.ShowDialog(this) == true) await RunAsync(token => LoadCatalogueAsync(dialog.FileName, token));
    }
    private async void MyCatalogue_Click(object sender, RoutedEventArgs e)
        => await RunAsync(token => LoadCatalogueAsync(_service.LocalCataloguePath, token));
    private async void Remove_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async token =>
        {
            var rows = SelectedRows();
            if (MessageBox.Show(this, $"Remove {rows.Count} selected package(s) from this list and My catalogue?\n\nTheir transfers will stop. Cached packages, source files and installed games will be kept. External catalogue files and URLs remain unchanged.",
                "Remove selected packages", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            foreach (var row in rows)
            {
                await _service.RemoveAsync(row.Project, token);
                _rows.Remove(row);
                Log($"Removed: {row.Project.Name} ({row.Project.PackageKind}). Files kept.");
            }
        });

    private async void ExportCatalogue_Click(object sender, RoutedEventArgs e)
    {
        if (_rows.Count == 0) { Log("Load or create a catalogue first."); return; }
        var dialog = new SaveFileDialog { Filter = "Project catalogue (*.json)|*.json", FileName = "indie-projects.json" };
        if (dialog.ShowDialog(this) == true)
            await RunAsync(async token => { await new TorrentCatalogue { Projects = _rows.Select(r => r.Project).ToList() }.SaveAsync(dialog.FileName, token); Log("Catalogue exported. Share this JSON file or host it at an HTTP/HTTPS URL. Keep a seeder running for its projects."); });
    }
    private void BrowseArchive_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog { Title = "Choose archive download destination" };
        if (dialog.ShowDialog(this) == true) ArchiveBox.Text = dialog.FolderName;
    }
    public void OpenShare(int kind, ShareSelection selection)
    {
        if (_operation != null) { Log("Wait for the current operation or cancel it."); return; }
        ShareKind=kind;
        ShareFolderBox.Text=selection.Source; GameFolderBox.Text=selection.PlayableGame; _shareName=selection.Name;
        ShowShare_Click(this,new RoutedEventArgs());
    }
    private void ShowShare_Click(object sender, RoutedEventArgs e)
    {
        if (_shareWindow != null) { _shareWindow.Activate(); return; }
        if (_operation != null) { Log("Wait for the current operation or cancel it."); return; }
        // Reuse the editor and its preview state; it no longer occupies catalogue layout space.
        object form=ShareHost.Content;
        ShareHost.Content=null;
        _createdShares.Clear();
        var window=new ProjectShareWindow(this,form,()=>_operation != null,()=>_operation?.Cancel(),()=>ExportCreatedShares());
        _shareWindow=window;
        window.Loaded += (_,_) => PreviewShare_Click(window,new RoutedEventArgs());
        try { window.ShowDialog(); }
        finally { window.DetachForm(); ShareHost.Content=form; _shareWindow=null; }
    }
    private async void ExportCreatedShares()
    {
        if (_createdShares.Count == 0) return;
        var dialog=new SaveFileDialog { Filter="Share Catalogue (*.json)|*.json", FileName="indie-share.json" };
        if (dialog.ShowDialog(_shareWindow ?? (Window)this) == true)
            await RunAsync(async token=> { await new TorrentCatalogue { Projects=_createdShares.ToList() }.SaveAsync(dialog.FileName,token); Log("Share catalogue saved: "+dialog.FileName); });
    }
    private void BrowseShare_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog { Title = "Choose the game's source and Content folder" };
        if (dialog.ShowDialog(_shareWindow ?? (Window)this) == true) { ShareFolderBox.Text = dialog.FolderName; _shareName=null; }
    }
    private void ShareKind_Changed(object sender, RoutedEventArgs e)
    {
        _preview=null; if (CreateShareButton != null) CreateShareButton.IsEnabled=false;
        if (SourceShareRow != null) SourceShareRow.Visibility=ShareKind == 1 ? Visibility.Collapsed : Visibility.Visible;
        if (GameShareRow != null) GameShareRow.Visibility=ShareKind == 0 ? Visibility.Collapsed : Visibility.Visible;
        if (_shareWindow != null && _operation == null) PreviewShare_Click(this,new RoutedEventArgs());
    }



    private void BrowseGame_Click(object sender, RoutedEventArgs e) { var d=new OpenFolderDialog { Title="Choose playable Windows game output" }; if(d.ShowDialog(_shareWindow ?? (Window)this)==true) GameFolderBox.Text=d.FolderName; }
    private void ShareFolder_Changed(object sender, TextChangedEventArgs e)
    {
        _preview = null;
        if (CreateShareButton != null) CreateShareButton.IsEnabled = false;
    }
    private async void PreviewShare_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async token =>
        {
            _preview=null; CreateShareButton.IsEnabled=false;
            ShareFilesBox.Clear(); ShareSummary.Text="Preparing File Preview…";
            int kind = ShareKind;
            string sourceFolder = ShareFolderBox.Text.Trim(), gameFolder = GameFolderBox.Text.Trim();
            var plans = await Task.Run(() => {
                var list=new List<SharePlan>();
                if(kind is 0 or 2) list.Add(ProjectBundle.Preview(sourceFolder, token));
                if(kind is 1 or 2) { var game=ProjectBundle.Preview(gameFolder, token, "Game"); list.Add(kind==2 ? game with { Name=list[0].Name } : game); }
                return list;
            }, token);
            if(kind != ShareKind || sourceFolder != ShareFolderBox.Text.Trim() || gameFolder != GameFolderBox.Text.Trim()) return;
            if (_shareName != null) plans=plans.Select(p=>p with { Name=_shareName }).ToList();
            _preview = plans;
            ShareFilesBox.Text = string.Join(Environment.NewLine, plans.SelectMany(plan => plan.Files.Select(f => plan.PackageKind + ": " + f.RelativePath)));
            ShareSummary.Text = $"{plans.Count} package(s), {plans.Sum(p=>p.Files.Count):N0} files, {plans.Sum(p=>p.TotalBytes)/1048576.0:N1} MB";
            CreateShareButton.IsEnabled = true;
            Log("File preview ready. Creating a share makes an immutable snapshot and starts uploading it to peers.");
        });
    private async void CreateShare_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async token =>
        {
            var plans = _preview ?? throw new InvalidOperationException("Preview the project files first.");
            ConfigureNetwork();
            var trackers = TrackersBox.Text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var plan in plans)
            {
            Log($"Packaging {plan.PackageKind}: {plan.Name}…");
            var entry = await _service.CreateShareAsync(plan, trackers, token);
            _createdShares.Add(entry); _shareWindow?.EnableExport();
            _rows.Add(new Row(entry)); CatalogueGrid.SelectedItem = _rows.Last();
            await _service.StartAsync(entry, seed: true, token);
            }
            Log("Share created and seeding started. Export the catalogue for other users. A snapshot is shared; later source edits require a new share.");
            _preview=null; CreateShareButton.IsEnabled=false; ShareSummary.Text="Share Created — Seeding";
        });
    private async void Download_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async token => { ConfigureNetwork(); foreach(var row in SelectedRows()) { await _service.StartAsync(row.Project, seed: false, token); Log($"Download started: {row.Project.Name} ({row.Project.PackageKind}). Use Verify and install when complete."); } });
    private async void Seed_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async token => { ConfigureNetwork(); foreach(var row in SelectedRows()) { await _service.StartAsync(row.Project, seed: true, token); Log($"Seeding: {row.Project.Name}"); } });
    private void OpenGames_Click(object sender, RoutedEventArgs e)
    {
        try { string root=Path.GetFullPath(GamesBox.Text.Trim()); Directory.CreateDirectory(root); Process.Start(new ProcessStartInfo(root) { UseShellExecute=true }); }
        catch(Exception error) { Log(error.Message); }
    }
    private void RunInstalled_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var entry=SelectedRow().Project;
            if(entry.PackageKind!="Game") throw new InvalidOperationException("Select a Game package to run.");
            string folder=Path.Combine(Path.GetFullPath(GamesBox.Text.Trim()),entry.FolderName);
            var plan=ProjectBundle.Preview(folder,packageKind:"Game");
            var executables=plan.Files.Where(f=>f.RelativePath.EndsWith(".exe",StringComparison.OrdinalIgnoreCase) && File.Exists(Path.Combine(folder,Path.ChangeExtension(f.RelativePath,".runtimeconfig.json"))) && File.Exists(Path.Combine(folder,Path.ChangeExtension(f.RelativePath,".deps.json")))).ToArray();
            if(executables.Length!=1) throw new InvalidOperationException("Open the games folder to choose an executable; this package has more than one entry point.");
            string exe=Path.Combine(folder,executables[0].RelativePath);
            Process.Start(new ProcessStartInfo(exe) { UseShellExecute=true, WorkingDirectory=Path.GetDirectoryName(exe)! });
        }
        catch(Exception error) { Log(error.Message); }
    }
    private async void Pause_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async _ => { foreach(var row in SelectedRows()) await TransferFor(row).Manager.PauseAsync(); Log("Transfer paused. Download / resume restarts it."); });
    private async void Stop_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async _ => { foreach(var row in SelectedRows()) await TransferFor(row).Manager.StopAsync(); Log("Transfer stopped. Cached files remain available for resume."); });
    private async void Install_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async token =>
        {
            foreach(var row in SelectedRows())
            {
                var transfer = _service.Transfers.FirstOrDefault(t=>t.Project.InfoHash==row.Project.InfoHash) ?? throw new InvalidOperationException("Download the selected packages first.");
                bool game=row.Project.PackageKind=="Game";
                string root=Path.GetFullPath((game ? GamesBox : ArchiveBox).Text.Trim());
                string installed=await _service.InstallAsync(transfer,root,token);
                Log($"Verified and installed {row.Project.PackageKind}: {installed}");
                if(!game) ProjectInstalled?.Invoke(root);
            }
        });
    private void Cancel_Click(object sender, RoutedEventArgs e) => _operation?.Cancel();

    private void Window_Closing(object? sender, CancelEventArgs e)
    {
        if (_closed) return;
        e.Cancel = true;
        Hide();
        Log("Torrent Manager hidden. Transfers continue; use Stop Torrents in the main window to stop them.");
    }

    public async Task ShutdownAsync()
    {
        if (_closed) return;
        if (_closing) { await _shutdown.Task; return; }
        _closing = true; _timer.Stop(); _lifetime.Cancel();
        try
        {
            if (_operationTask != null) { try { await _operationTask; } catch { } }
            await _service.DisposeAsync();
        }
        catch (Exception error) { Log("Transfer shutdown: " + error.Message); }
        finally { _lifetime.Dispose(); _closed = true; StatusChanged?.Invoke(new([],"Torrents Stopped")); Close(); _shutdown.TrySetResult(); }
    }
}
