using IndieGames.BuildManager.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace IndieGames.BuildManager;

public sealed class ProjectCleanupWindow : Window
{
    private readonly string _archive;
    private readonly DataGrid _grid = new() { AutoGenerateColumns=false, IsReadOnly=true, CanUserAddRows=false, SelectionMode=DataGridSelectionMode.Extended };
    private readonly TextBlock _summary = new() { Text="Scan the archive to preview cleanup.", Margin=new Thickness(4), TextWrapping=TextWrapping.Wrap };
    private readonly TextBlock _progress = new() { Margin=new Thickness(4), TextTrimming=TextTrimming.CharacterEllipsis };
    private readonly ProgressBar _scanProgress = new() { Height=8, Maximum=1, Margin=new Thickness(4) };
    private readonly Button _scan = new() { Content="Scan archive" };
    private readonly Button _apply = new() { Content="Apply previewed cleanup", IsEnabled=false };
    private readonly Button _export = new() { Content="Export report…", IsEnabled=false };
    private readonly Button _cancel = new() { Content="Cancel", IsEnabled=false };
    private readonly TextBox _search = new() { MinWidth=260, Margin=new Thickness(4) };
    private readonly ComboBox _filter = new() { ItemsSource=new[] { "All", "Disable", "Delete backup", "Keep", "Review", "Failed" }, SelectedIndex=0, Margin=new Thickness(4) };
    private readonly DataGrid _backupGrid = new() { AutoGenerateColumns=false, IsReadOnly=true, CanUserAddRows=false };
    private readonly Button _backupPreview = new() { Content="Preview all backups" };
    private readonly Button _backupDelete = new() { Content="Permanently delete previewed backups", IsEnabled=false };
    private readonly TextBlock _backupStatus = new() { Text="No backups scanned.", Margin=new Thickness(4), TextWrapping=TextWrapping.Wrap };
    private IReadOnlyList<BackupCleanupEntry> _backupEntries = Array.Empty<BackupCleanupEntry>();
    private bool _backupBusy;
    private bool _backupDeleteAttempted;
    private ProjectCleanupPlan? _plan;
    private CancellationTokenSource? _cts;
    public bool Changed { get; private set; }
    public ProjectCleanupWindow(string archive)
    {
        _archive=archive;
        Title=AppVersion.WindowTitle("Archive Maintenance"); Width=1250; Height=740; MinWidth=850; MinHeight=480; WindowStartupLocation=WindowStartupLocation.CenterOwner;
        var panel = new DockPanel { Margin=new Thickness(16) }; Content=panel;
        var header = new StackPanel(); DockPanel.SetDock(header,Dock.Top); panel.Children.Add(header);
        header.Children.Add(new TextBlock { Text=archive, FontWeight=FontWeights.SemiBold, TextWrapping=TextWrapping.Wrap });
        header.Children.Add(new TextBlock { Text="Keep folder-named executable projects and referenced libraries. Disable redundant projects and permanently delete .bak files. Missing or invalid main projects require review. Linked paths are excluded.", TextWrapping=TextWrapping.Wrap, Margin=new Thickness(4,8,4,8) });
        var buttons = new WrapPanel(); header.Children.Add(buttons);
        foreach (var button in new[] { _scan,_apply,_export,_cancel }) { button.Margin=new Thickness(4); button.Padding=new Thickness(10,6,10,6); buttons.Children.Add(button); }
        header.Children.Add(_summary);
        header.Children.Add(_scanProgress);
        var filters = new WrapPanel(); filters.Children.Add(new TextBlock { Text="Show:", VerticalAlignment=VerticalAlignment.Center }); filters.Children.Add(_filter); filters.Children.Add(new TextBlock { Text="Search report:", VerticalAlignment=VerticalAlignment.Center }); filters.Children.Add(_search); header.Children.Add(filters);
        DockPanel.SetDock(_progress,Dock.Bottom); panel.Children.Add(_progress);
        foreach (var column in new[] { ("Action","Action",110d),("Status","Status",130d),("Path","Path",340d),("Reason","Reason",330d),("Disabled destination","Destination",340d) })
            _grid.Columns.Add(new DataGridTextColumn { Header=column.Item1, Binding=new Binding(column.Item2), Width=column.Item3 });
        var tabs = new TabControl();
        panel.Children.Add(tabs);
        tabs.Items.Add(new TabItem { Header="Project cleanup", Content=_grid });
        var backupPanel = new DockPanel { Margin=new Thickness(8) };
        var backupHeader = new StackPanel();
        DockPanel.SetDock(backupHeader,Dock.Top);
        backupPanel.Children.Add(backupHeader);
        backupHeader.Children.Add(new TextBlock { Text="Preview recognized backup folders and loose .bak / .before-* files. Deletion is permanent, not sent to the Recycle Bin. Linked or changed paths are refused by the cleanup service.", TextWrapping=TextWrapping.Wrap, Margin=new Thickness(4,4,4,8) });
        var backupButtons = new WrapPanel();
        backupHeader.Children.Add(backupButtons);
        foreach (var button in new[] { _backupPreview, _backupDelete }) { button.Margin=new Thickness(4); button.Padding=new Thickness(10,6,10,6); backupButtons.Children.Add(button); }
        backupHeader.Children.Add(_backupStatus);
        _backupGrid.Columns.Add(new DataGridTextColumn { Header="Backup", Binding=new Binding("Path"), Width=new DataGridLength(1,DataGridLengthUnitType.Star) });
        _backupGrid.Columns.Add(new DataGridTextColumn { Header="Bytes", Binding=new Binding("Bytes"), Width=130 });
        _backupGrid.Columns.Add(new DataGridTextColumn { Header="Files", Binding=new Binding("Files"), Width=90 });
        backupPanel.Children.Add(_backupGrid);
        tabs.Items.Add(new TabItem { Header="All backups", Content=backupPanel });
        _backupPreview.Click += async (_,_) => await PreviewBackups();
        _backupDelete.Click += async (_,_) => await DeleteBackups();
        _scan.Click += async (_,_) => await Scan();
        _apply.Click += async (_,_) => await Apply();
        _cancel.Click += (_,_) => _cts?.Cancel();
        _filter.SelectionChanged += (_,_) => RefreshFilter(); _search.TextChanged += (_,_) => RefreshFilter();
        _export.Click += (_,_) => {
            if (_plan == null) return;
            var dialog = new SaveFileDialog { Filter="CSV report (*.csv)|*.csv", FileName="game-project-cleanup.csv" };
            if (dialog.ShowDialog(this) == true) { try { ArchiveProjectCleanup.Export(_plan,dialog.FileName); _progress.Text="Report saved: " + dialog.FileName; } catch (Exception e) { MessageBox.Show(this,e.Message,Title); } }
        };
        Closing += (_,e) => { if (_backupBusy) { e.Cancel=true; _backupStatus.Text="Wait for the backup operation to finish before closing."; return; } if (_cts != null) { e.Cancel=true; _cts.Cancel(); _progress.Text="Cancelling… Close this window when the operation stops."; } };
    }

    private void BackupBusy(bool busy)
    {
        _backupBusy=busy;
        _backupPreview.IsEnabled=!busy && _cts==null;
        _backupDelete.IsEnabled=!busy && _cts==null && !_backupDeleteAttempted && _backupEntries.Count>0;
        _scan.IsEnabled=!busy && _cts==null;
        _apply.IsEnabled=!busy && _cts==null && _plan?.Rows.Any(r=>r.Actionable && r.Status=="Preview")==true;
        _cancel.IsEnabled=!busy && _cts!=null;
    }

    private async Task PreviewBackups()
    {
        BackupBusy(true);
        _backupEntries=Array.Empty<BackupCleanupEntry>();
        _backupDeleteAttempted=false;
        _backupGrid.ItemsSource=null;
        _backupStatus.Text="Scanning backups…";
        try
        {
            var found=await Task.Run(()=>BackupCleanup.Preview(_archive));
            _backupEntries=found;
            _backupGrid.ItemsSource=found;
            _backupStatus.Text=$"{found.Count:N0} backup items, {found.Sum(e=>e.Files):N0} files, {found.Sum(e=>e.Bytes)/1048576d:N1} MiB. Nothing deleted.";
        }
        catch(Exception ex) { _backupStatus.Text="Backup preview failed: "+ex.Message; }
        finally { BackupBusy(false); }
    }

    private async Task DeleteBackups()
    {
        if(_backupEntries.Count==0 || _backupDeleteAttempted) return;
        var selected=_backupEntries;
        if(MessageBox.Show(this,$"Permanently delete {selected.Count:N0} previewed backup items ({selected.Sum(e=>e.Bytes)/1048576d:N1} MiB)?\n\nThis cannot be undone. No Recycle Bin. Changed or linked paths will be refused.","Confirm backup cleanup",MessageBoxButton.YesNo,MessageBoxImage.Warning)!=MessageBoxResult.Yes) return;
        _backupDeleteAttempted=true;
        BackupBusy(true);
        int completed=0, deletedFiles=0;
        long deletedBytes=0;
        try
        {
            var result=await Task.Run(()=>
            {
                int items=0, files=0; long bytes=0;
                foreach(var entry in selected)
                {
                    BackupCleanup.Delete(entry,size=>{bytes+=size;files++;});
                    items++;
                }
                return (items,files,bytes);
            });
            (completed,deletedFiles,deletedBytes)=result;
            Changed=true;
            _backupStatus.Text=$"Deleted {completed:N0} backup items ({deletedFiles:N0} files). Space recovered: {deletedBytes/1048576d:N1} MiB ({deletedBytes:N0} bytes). Scan again to refresh.";
        }
        catch(Exception ex)
        {
            _backupStatus.Text="Backup deletion stopped: "+ex.Message+". Some files may have been deleted. Scan again before retrying.";
            Changed=true;
        }
        finally
        {
            _backupEntries=Array.Empty<BackupCleanupEntry>();
            _backupGrid.ItemsSource=null;
            BackupBusy(false);
        }
    }

    private void RefreshFilter()
    {
        if (_grid.ItemsSource == null) return;
        string action=_filter.SelectedItem as string ?? "All", search=_search.Text;
        CollectionViewSource.GetDefaultView(_grid.ItemsSource).Filter = item => item is ProjectCleanupRow row
            && (action == "All" || row.Action == action || (action == "Failed" && row.Status.StartsWith("Failed:")))
            && string.Join(" ",row.Path,row.Reason,row.Status,row.Destination).Contains(search,StringComparison.OrdinalIgnoreCase);
    }
    private void Summary()
    {
        _summary.Text = _plan == null ? "No report." : string.Join(" · ",_plan.Rows.GroupBy(r=>r.Action).Select(g=>$"{g.Count():N0} {g.Key}"))
            + $" · {_plan.Rows.Count(r=>r.Status == "Applied"):N0} applied · {_plan.Rows.Count(r=>r.Status.StartsWith("Failed:")):N0} failed";
    }
    private void Busy(bool busy)
    {
        _scanProgress.IsIndeterminate=busy; _scanProgress.Value=!busy && _plan != null ? 1 : 0;
        _backupPreview.IsEnabled=!busy && !_backupBusy; _backupDelete.IsEnabled=!busy && !_backupBusy && !_backupDeleteAttempted && _backupEntries.Count>0;
        _scan.IsEnabled=!busy && !_backupBusy; _cancel.IsEnabled=busy; _export.IsEnabled=!busy && _plan != null;
        _apply.IsEnabled=!busy && !_backupBusy && _plan?.Rows.Any(r=>r.Actionable && r.Status == "Preview") == true;
    }
    private async Task Scan()
    {
        _cts=new(); _plan=null; _grid.ItemsSource=null; Busy(true); _summary.Text="Scanning archive…";
        var token=_cts.Token;
        // Throttle directory updates on large archives.
        long last=0;
        var progress=new Progress<string>(s=> { long now=Environment.TickCount64; if (now-last>150) { _progress.Text=s; last=now; } });
        try { _plan=await Task.Run(()=>ArchiveProjectCleanup.Preview(_archive,token,progress)); _grid.ItemsSource=_plan.Rows; RefreshFilter(); Summary(); _progress.Text="Preview complete. No files changed."; }
        catch (OperationCanceledException) { _summary.Text="Scan cancelled. No files changed."; }
        catch (Exception e) { _summary.Text="Scan failed: " + e.Message; }
        finally { _cts.Dispose(); _cts=null; Busy(false); }
    }
    private async Task Apply()
    {
        if (_plan == null) return;
        int disable=_plan.Rows.Count(r=>r.Action == "Disable" && r.Status == "Preview"), backups=_plan.Rows.Count(r=>r.Action == "Delete backup" && r.Status == "Preview");
        if (MessageBox.Show(this,$"Disable {disable:N0} redundant projects and permanently delete {backups:N0} .bak files?\n\nKeep and Review entries will remain unchanged. Close games and external builds before continuing.",Title,MessageBoxButton.YesNo,MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
        _cts=new(); Busy(true); var token=_cts.Token; var plan=_plan;
        long last=0;
        var progress=new Progress<string>(s=> { long now=Environment.TickCount64; if (now-last>150) { _progress.Text=s; last=now; } });
        try { await Task.Run(()=>ArchiveProjectCleanup.Apply(plan,token,progress)); _progress.Text="Cleanup finished. Export the report to save the results."; }
        catch (OperationCanceledException) { _progress.Text="Cleanup cancelled. Completed actions are shown in the report. Scan again to continue."; }
        catch (Exception e) { _progress.Text="Cleanup stopped: " + e.Message; }
        finally {
            Changed |= plan.Rows.Any(r=>r.Status == "Applied"); _grid.Items.Refresh(); RefreshFilter(); Summary();
            _cts.Dispose(); _cts=null; Busy(false); _apply.IsEnabled=false; // A new scan is required after any apply attempt.
        }
    }
}
