using IndieGames.BuildManager.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;

namespace IndieGames.BuildManager;

public partial class TorrentWindow : Window
{
    private sealed class Row(TorrentProject project, ImageSource? icon) : INotifyPropertyChanged
    {
        public TorrentProject Project { get; } = project;
        public ImageSource? Icon { get; private set; } = icon;
        public void SetIcon(ImageSource? value) { Icon=value; PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Icon))); }
        public string KindLabel => Project.PackageKind == "Game" ? "Playable game" : "Source files";
        public bool IsInSolution { get; private set; }
        public void SetMembership(bool value) { if(IsInSolution==value)return; IsInSolution=value; PropertyChanged?.Invoke(this,new(nameof(IsInSolution))); }
        public string Status { get; private set; } = "Not started";
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
    private sealed record SourceOption(string Folder) { public string Name=>Path.GetFileName(Path.TrimEndingDirectorySeparator(Folder)); public override string ToString()=>Name; }
    private string _selectedSourceFolder="";
    private ShareSelection? _selectedShare;
    private bool _loadingSourceChoices, _previewPending, _isPreviewOperation;
    private int _previewRevision;
    private readonly DispatcherTimer _previewTimer=new() { Interval=TimeSpan.FromMilliseconds(250) };
    private string CurrentSource=>ShareFolderBox.SelectedValue as string ?? _selectedSourceFolder;
    private readonly List<TorrentProject> _createdShares=[];
    private string? _previewIcon;
    private string? _membershipStamp;
    private bool _checkingMembership;
    private int _rowRevision;
    private int ShareKind
    {
        get => GameKindChoice?.IsChecked == true ? 1 : BothKindChoice?.IsChecked == true ? 2 : 0;
        set { SourceKindChoice.IsChecked=value == 0; GameKindChoice.IsChecked=value == 1; BothKindChoice.IsChecked=value == 2; }
    }
    public event Action<TorrentStatusSnapshot>? StatusChanged;
    private readonly TaskCompletionSource _shutdown = new(TaskCreationOptions.RunContinuationsAsynchronously);
    public event Action<string>? ProjectInstalled;

    public TorrentWindow(string archiveRoot, string? selectedFolder = null, string? stateDirectory = null, string? gamesRoot = null, string? gameFolder = null, ProjectTorrentService? service = null)
    {
        InitializeComponent();
        Title=AppVersion.WindowTitle(Title);
        ArchiveBox.Text = archiveRoot;
        GamesBox.Text = gamesRoot ?? LibraryStorage.Load().Games;
        GameFolderBox.Text = gameFolder ?? "";
        _selectedSourceFolder = selectedFolder ?? "";
        CatalogueGrid.ItemsSource = _rows;
        _service = service ?? new ProjectTorrentService(stateDirectory ?? LibraryStorage.Load().TorrentCache);
        CacheNote.Text = "Packages and resume data: " + _service.StateDirectory;
        _timer.Tick += (_, _) => RefreshTransfers();
        _timer.Start();
        _previewTimer.Tick += async (_,_) => {
            _previewTimer.Stop();
            if (_shareWindow == null || _loadingSourceChoices) return;
            if (_operation != null) { _previewPending=true; return; }
            _previewPending=false; _isPreviewOperation=true;
            try { await PreviewShareAsync(); }
            finally { _isPreviewOperation=false; if (_previewPending && _shareWindow != null) _previewTimer.Start(); }
        };
    }

    private void Log(string text) { if(!Dispatcher.CheckAccess()) { Dispatcher.InvokeAsync(()=>Log(text)); return; } StatusBox.AppendText(text + Environment.NewLine); StatusBox.ScrollToEnd(); _shareWindow?.Log(text); _activity=text; PublishStatus(); }
    private void PublishStatus() => StatusChanged?.Invoke(new(_service.Transfers.Select(TorrentStatus.Capture).ToArray(),_activity));
    private async Task RunAsync(Func<CancellationToken, Task> action)
    {
        if (_closing) return;
        if (_operation != null) { Log("Wait for the current operation or cancel it."); return; }
        _operation = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token);
        _shareWindow?.SetBusy(true);
        try { _operationTask = action(_operation.Token); await _operationTask; }
        catch (OperationCanceledException) { if (!_isPreviewOperation) Log("Operation cancelled. Partial downloads are kept for resume."); }
        catch (Exception error) { Log(error.GetBaseException().Message); if (_shareWindow != null && _preview == null) ShareSummary.Text=error.GetBaseException().Message; }
        finally { _operation.Dispose(); _operation = null; _operationTask = null; _shareWindow?.SetBusy(false); RefreshTransfers(); if (_previewPending && _shareWindow != null) _previewTimer.Start(); }
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
        UpdatePackageHelp();
        _=RefreshCatalogueMembershipAsync();
        PublishStatus();
    }

    private IReadOnlyList<Row> SelectedRows() => CatalogueGrid.SelectedItems.Cast<Row>().ToArray() is { Length: > 0 } rows ? rows : throw new InvalidOperationException("Select one or more packages first.");
    private Row SelectedRow() => CatalogueGrid.SelectedItem as Row ?? throw new InvalidOperationException("Select a project in the catalogue first.");
    private Row CreateRow(TorrentProject project) { _rowRevision++; return new(project, CatalogueArtwork.ForProject(project,ArchiveBox.Text,GamesBox.Text)); }
    private async Task RefreshCatalogueMembershipAsync()
    {
        if(_checkingMembership)return;
        _checkingMembership=true;
        string solution=LibraryStorage.Load().Solution,archive=ArchiveBox.Text,previous=_membershipStamp ?? "";
        int revision=_rowRevision;
        try
        {
            var snapshot=await Task.Run(()=>{
                var info=new FileInfo(solution);
                string stamp=solution+"|"+archive+"|"+revision+"|"+(info.Exists ? info.LastWriteTimeUtc.Ticks+":"+info.Length : "missing");
                return stamp==previous ? null : new {Stamp=stamp,Members=SolutionMembership.Read(solution)};
            });
            if(snapshot==null || revision!=_rowRevision)return;
            foreach(var row in _rows)
            {
                bool included=false;
                foreach(string name in new[]{row.Project.FolderName,row.Project.Name})
                {
                    try
                    {
                        ProjectBundle.ValidateSegment(name);
                        string folder=Path.GetFullPath(Path.Combine(archive,name))+Path.DirectorySeparatorChar;
                        included |= snapshot.Members.Any(p=>p.StartsWith(folder,StringComparison.OrdinalIgnoreCase));
                    }
                    catch(Exception ex) when(ex is IOException or ArgumentException) { }
                }
                row.SetMembership(included);
            }
            _membershipStamp=snapshot.Stamp;
        }
        catch(Exception ex) when(ex is IOException or UnauthorizedAccessException or ArgumentException or System.Xml.XmlException) { }
        finally { _checkingMembership=false; }
    }
    private void SharingHelp_Click(object sender, RoutedEventArgs e) => SharingHelpWindow.ShowFor(_shareWindow ?? (Window)this);
    private void RefreshShare_Click(object sender, RoutedEventArgs e) => SchedulePreview();
    private void CatalogueSelection_Changed(object sender, SelectionChangedEventArgs e) => UpdatePackageHelp();
    private void UpdatePackageHelp()
    {
        if (PackageHelpText == null) return;
        string text = "Select a package to download, share or install.";
        if (CatalogueGrid.SelectedItem is Row row)
        {
            var transfer = _service.Transfers.FirstOrDefault(t => t.Project.InfoHash == row.Project.InfoHash);
            bool installed = Directory.Exists(Path.Combine(row.Project.PackageKind == "Game" ? GamesBox.Text : ArchiveBox.Text,row.Project.FolderName));
            text = installed ? row.Project.PackageKind == "Game" ? "Installed game: choose Run installed game to play." : "Source is available in the source archive. Open the main window to build it." :
                transfer?.Manager.Complete == true ? "Download complete. Choose Verify and install; playable games can then be launched with Run installed game." :
                "Choose Download / Resume, wait for 100%, then Verify and install. Source files require building before they can run.";
            text = $"{row.KindLabel} · Created {row.Project.CreatedUtc.ToLocalTime():dd MMM yyyy HH:mm:ss} · Snapshot {row.Project.SnapshotDisplay} · {row.Project.FileCount:N0} files\n" + text;
            if (_rows.Count(r => r.Project.Name.Equals(row.Project.Name, StringComparison.OrdinalIgnoreCase) && r.Project.PackageKind == row.Project.PackageKind) > 1)
                text += "\nMore than one snapshot exists for this package type. Select two and choose Compare snapshots to see file differences.";
        }
        if (PackageHelpText.Text != text) PackageHelpText.Text = text;
    }
    private ProjectTransfer TransferFor(Row row)
    {
        return _service.Transfers.FirstOrDefault(t => t.Project.InfoHash.Equals(row.Project.InfoHash, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException("Start downloading or seeding this project first.");
    }

    private async Task LoadCatalogueAsync(string location, CancellationToken token)
    {
        var catalogue = await TorrentCatalogue.LoadAsync(location, token);
        _rows.Clear(); foreach (var entry in catalogue.Projects) _rows.Add(CreateRow(entry));
        foreach (var transfer in _service.Transfers)
            if (!_rows.Any(r => r.Project.InfoHash.Equals(transfer.Project.InfoHash, StringComparison.OrdinalIgnoreCase))) _rows.Add(CreateRow(transfer.Project));
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
    private void BrowseGames_Click(object sender, RoutedEventArgs e)
    {
        var dialog=new OpenFolderDialog { Title="Choose playable games folder", InitialDirectory=GamesBox.Text.Trim() };
        if(dialog.ShowDialog(this)==true) GamesBox.Text=dialog.FolderName;
    }
    public void OpenShare(int kind, ShareSelection selection)
    {
        if (_operation != null) { Log("Wait for the current operation or cancel it."); return; }
        ShareKind=kind;
        _selectedSourceFolder=selection.Source; _selectedShare=selection; GameFolderBox.Text=selection.PlayableGame;
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
        var window=new ProjectShareWindow(this,form,()=>_operation != null,()=>_operation?.Cancel(),()=>ExportCreatedShares(),ShareActionsPanel);
        _shareWindow=window;
        window.Loaded += async (_,_) => {
            await RunAsync(async token=> {
                _loadingSourceChoices=true; ShareFolderBox.IsEnabled=false;
                try {
                    string archive=ArchiveBox.Text.Trim(), selected=_selectedSourceFolder, games=GamesBox.Text.Trim();
                    var folders=await Task.Run(()=>ShareSelection.SourceFolders(archive).Concat(PortableGames.Scan(games).Select(g=>g.Folder)).ToArray(),token);
                    var options=folders.Concat(string.IsNullOrWhiteSpace(selected) ? [] : new[]{selected}).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(Path.GetFileName,StringComparer.OrdinalIgnoreCase).Select(f=>new SourceOption(f)).ToArray();
                    ShareFolderBox.ItemsSource=options;
                    ShareFolderBox.SelectedValue=options.FirstOrDefault(o=>o.Folder.Equals(selected,StringComparison.OrdinalIgnoreCase))?.Folder ?? options.FirstOrDefault()?.Folder;
                } finally { _loadingSourceChoices=false; ShareFolderBox.IsEnabled=true; }
            });
            UpdateShareKindForFolder();
            SchedulePreview();
        };
        try { window.ShowDialog(); }
        finally { _previewTimer.Stop(); _previewPending=false; window.DetachForm(); ShareHost.Content=form; _shareWindow=null; }
    }
    private async void ExportCreatedShares()
    {
        if (_createdShares.Count == 0) return;
        var dialog=new SaveFileDialog { Filter="Share Catalogue (*.json)|*.json", FileName="indie-share.json" };
        if (dialog.ShowDialog(_shareWindow ?? (Window)this) == true)
            await RunAsync(async token=> { await new TorrentCatalogue { Projects=_createdShares.ToList() }.SaveAsync(dialog.FileName,token); Log("Share catalogue saved: "+dialog.FileName); });
    }
    private void ShareKind_Changed(object sender, RoutedEventArgs e)
    {
        _preview=null; if (CreateShareButton != null) CreateShareButton.IsEnabled=false;
        if (GameShareRow != null) GameShareRow.Visibility=ShareKind == 0 ? Visibility.Collapsed : Visibility.Visible;
        SchedulePreview();
    }



    private void ShareFolder_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (_loadingSourceChoices) return;
        _selectedSourceFolder=CurrentSource;
        UpdateShareKindForFolder();
        SchedulePreview();
    }
    private void UpdateShareKindForFolder()
    {
        bool source = Directory.Exists(CurrentSource) && Directory.EnumerateFiles(CurrentSource, "*.csproj").Any();
        SourceKindChoice.IsEnabled = BothKindChoice.IsEnabled = source;
        if (!source) ShareKind = 1;
    }
    private void SchedulePreview()
    {
        _preview=null; _previewIcon=null; _previewRevision++;
        if (CreateShareButton != null) CreateShareButton.IsEnabled=false;
        if (_shareWindow == null || _loadingSourceChoices) return;
        ShareFilesBox.Clear(); ShareSummary.Text="Updating File Preview…";
        GameFolderBox.Text="Finding playable files…";
        if (_isPreviewOperation) _operation?.Cancel();
        _previewPending=true; _previewTimer.Stop(); _previewTimer.Start();
    }
    private Task PreviewShareAsync()
        => RunAsync(async token =>
        {
            _preview=null; CreateShareButton.IsEnabled=false;
            ShareFilesBox.Clear(); ShareSummary.Text="Preparing File Preview…";
            int kind = ShareKind;
            int revision=_previewRevision;
            string sourceFolder = CurrentSource;
            if (string.IsNullOrWhiteSpace(sourceFolder)) throw new IOException("No source projects were found in this archive.");
            string archive=ArchiveBox.Text.Trim();
            var storage=new LibraryStorage { Games=GamesBox.Text.Trim() };
            var initial=_selectedShare;
            var selection=await Task.Run(()=>initial != null && initial.Source.Equals(sourceFolder,StringComparison.OrdinalIgnoreCase)
                    ? initial : Directory.EnumerateFiles(sourceFolder,"*.csproj").Any() ? ShareSelection.ForSource(archive,sourceFolder,storage) : ShareSelection.ForPlayable(sourceFolder),token);
            if (revision != _previewRevision) return;
            GameFolderBox.Text=selection.PlayableGame;
            var plans = await Task.Run(() => {
                var list=new List<SharePlan>();
                if(kind is 0 or 2) list.Add(ProjectBundle.Preview(sourceFolder, token));
                if(kind is 1 or 2) {
                    if (!Directory.Exists(selection.PlayableGame)) throw new IOException("No playable files found. Build this source project, or import a standalone game through Playable Games / Ports.");
                    list.Add(ProjectBundle.Preview(selection.PlayableGame, token, "Game"));
                }
                return list.Select(p=>p with { Name=selection.Name }).ToList();
            }, token);
            if(revision != _previewRevision || kind != ShareKind || sourceFolder != CurrentSource) return;
            _preview = plans;
            _previewIcon = CatalogueArtwork.Encode(sourceFolder);
            ShareFilesBox.Text = string.Join(Environment.NewLine, plans.SelectMany(plan => plan.Files.Select(f => plan.PackageKind + ": " + f.RelativePath)));
            ShareSummary.Text = $"{plans.Count} package(s), {plans.Sum(p=>p.Files.Count):N0} files, {plans.Sum(p=>p.TotalBytes)/1048576.0:N1} MB";
            CreateShareButton.IsEnabled = true;
            Log("File preview ready. Creating a share makes an immutable snapshot and starts uploading it to peers.");
        });
    private async void CreateShare_Click(object sender, RoutedEventArgs e)
    {
        if (_operation != null || _preview == null) return;
        var plans = _preview.ToArray();
        string icon = _previewIcon ?? "";
        string name = plans[0].Name;
        foreach (char invalid in Path.GetInvalidFileNameChars()) name = name.Replace(invalid,'_');
        var dialog = new SaveFileDialog {
            Title = "Save the catalogue file to send to recipients", Filter = "Share catalogue (*.json)|*.json",
            FileName = name + "-share.json", DefaultExt = ".json", AddExtension = true
        };
        if (dialog.ShowDialog(_shareWindow ?? (Window)this) != true) return;
        await RunAsync(async token =>
        {
            ConfigureNetwork();
            var trackers = TrackersBox.Text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var exported = new List<TorrentProject>();
            foreach (var plan in plans)
            {
            Log($"Packaging {plan.PackageKind}: {plan.Name}…");
            var entry = await Task.Run(() => _service.CreateShareAsync(plan, trackers, token, icon), token);
            if (!_createdShares.Any(p => p.InfoHash == entry.InfoHash)) _createdShares.Add(entry); _shareWindow?.EnableExport();
            var existingRow = _rows.FirstOrDefault(r => r.Project.InfoHash.Equals(entry.InfoHash, StringComparison.OrdinalIgnoreCase));
            var displayRow = CreateRow(entry);
            if (existingRow != null) { _rows[_rows.IndexOf(existingRow)] = displayRow; Log("Unchanged content: reusing snapshot " + entry.SnapshotDisplay); }
            else _rows.Add(displayRow);
            CatalogueGrid.SelectedItem = displayRow;
            exported.Add(entry);
            await new TorrentCatalogue { Projects = exported.ToList() }.SaveAsync(dialog.FileName,token);
            await _service.StartAsync(entry, seed: true, token);
            }
            Log("Share ready. Catalogue saved: " + dialog.FileName + ". Send this JSON file to recipients and keep Build Manager open while they download.");
            _shareWindow?.ShowSaved(dialog.FileName);
            _preview=null; CreateShareButton.IsEnabled=false; ShareSummary.Text="Share created — catalogue saved — seeding";
        });
    }
    private async void Download_Click(object sender, RoutedEventArgs e)
        => await RunAsync(async token => { ConfigureNetwork(); foreach(var row in SelectedRows()) { await _service.StartAsync(row.Project, seed: false, token); Log($"Download started: {row.Project.Name} ({row.Project.PackageKind}). Use Verify and install when complete."); } });

    private async void CompareSnapshots_Click(object sender, RoutedEventArgs e)
    {
        var selected = CatalogueGrid.SelectedItems.Cast<Row>().ToArray();
        if (selected.Length != 2 || selected[0].Project.PackageKind != selected[1].Project.PackageKind ||
            !selected[0].Project.Name.Equals(selected[1].Project.Name, StringComparison.OrdinalIgnoreCase))
        { Log("Select exactly two snapshots of the same game and package type (Source files or Playable game)."); return; }
        await RunAsync(async token =>
        {
            var first = selected[0].Project; var second = selected[1].Project;
            Log($"Comparing {first.SnapshotDisplay} ({first.CreatedDisplay}) with {second.SnapshotDisplay} ({second.CreatedDisplay})…");
            var before = await Task.Run(() => _service.InspectContentsAsync(first, token), token);
            var after = await Task.Run(() => _service.InspectContentsAsync(second, token), token);
            var added = after.Files.Keys.Except(before.Files.Keys, StringComparer.Ordinal).Order().ToArray();
            var removed = before.Files.Keys.Except(after.Files.Keys, StringComparer.Ordinal).Order().ToArray();
            var changed = before.Files.Keys.Intersect(after.Files.Keys, StringComparer.Ordinal).Where(name => before.Files[name] != after.Files[name]).Order().ToArray();
            string summary = added.Length + removed.Length + changed.Length == 0
                ? "Identical file contents. These are duplicate snapshots; ZIP metadata and snapshot IDs can differ. Keep either snapshot, or remove an unwanted row with Remove selected."
                : $"Different revisions: {added.Length} added, {removed.Length} removed, {changed.Length} changed files.";
            Log(summary); PackageHelpText.Text = summary;
            foreach (var line in added.Select(n => "Added: " + n).Concat(removed.Select(n => "Removed: " + n)).Concat(changed.Select(n => "Changed: " + n)).Take(100)) Log(line);
        });
    }
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
            string exe;
            try { exe=PortableGames.ResolveExecutable(folder); }
            catch(IOException)
            {
                var picker = new OpenFileDialog { Title="Choose this game's executable", Filter="Windows executable (*.exe)|*.exe", InitialDirectory=folder };
                if(picker.ShowDialog(this)!=true)return;
                PortableGames.SetExecutable(folder,Path.GetRelativePath(folder,picker.FileName));
                exe=PortableGames.ResolveExecutable(folder);
            }
            GameProcessLauncher.Start(exe,GameProcessLauncher.DefaultLogDirectory,Log,PortableGames.LaunchEnvironment(folder));
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
                row.SetIcon(CatalogueArtwork.ForProject(row.Project,ArchiveBox.Text,GamesBox.Text));
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
        _previewTimer.Stop();
        try
        {
            if (_operationTask != null) { try { await _operationTask; } catch { } }
            await _service.DisposeAsync();
        }
        catch (Exception error) { Log("Transfer shutdown: " + error.Message); }
        finally { _lifetime.Dispose(); _closed = true; StatusChanged?.Invoke(new([],"Torrents Stopped")); Close(); _shutdown.TrySetResult(); }
    }
}
