using IndieGames.BuildManager.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace IndieGames.BuildManager;
public sealed class StorageWindow : Window
{
    
    private readonly TextBox[] _paths = new TextBox[4];
    private readonly TextBox _preview = new() { IsReadOnly = true, Height = 190, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, AcceptsReturn = true };
    private readonly Button _delete = new() { Content = "Delete previewed folders", IsEnabled = false, Margin = new Thickness(4) };
    private IReadOnlyList<CleanupFolder> _folders = [];
    private int _previewVersion;
    private LibraryStorage Draft() => new() { Archive = _paths[0].Text.Trim(), Games = _paths[1].Text.Trim(), BuildCache = _paths[2].Text.Trim(), TorrentCache = _paths[3].Text.Trim() };
    public StorageWindow(LibraryStorage storage)
    {
        Title = "Storage folders and cleanup"; Width = 900; Height = 760; MinWidth = 700; MinHeight = 500; WindowStartupLocation = WindowStartupLocation.CenterOwner;
        var panel = new StackPanel { Margin = new Thickness(16) }; Content = new ScrollViewer { Content=panel, VerticalScrollBarVisibility=ScrollBarVisibility.Auto };
        panel.Children.Add(new TextBlock { Text = "Source stays in the archive. Playable builds, intermediate files and torrent snapshots use separate folders.", TextWrapping = TextWrapping.Wrap });
        string[] labels = ["Source archive", "Playable Windows games", "Disposable build cache", "Torrent snapshots / downloads"];
        string[] values = [storage.Archive, storage.Games, storage.BuildCache, storage.TorrentCache];
        for (int i = 0; i < 4; i++)
        {
            int index = i; panel.Children.Add(new TextBlock { Text = labels[i], Margin = new Thickness(0,8,0,2) });
            var row = new DockPanel(); var open = new Button { Content = "Open", Padding = new Thickness(8,4,8,4) };
            DockPanel.SetDock(open, Dock.Right); row.Children.Add(open);
            open.Click += (_, _) => Try(() => { string folder=Path.GetFullPath(_paths[index].Text.Trim()); Directory.CreateDirectory(folder); System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(folder) { UseShellExecute=true }); });
            var browse = new Button { Content = "Browse…", Padding = new Thickness(8,4,8,4) };
            DockPanel.SetDock(browse, Dock.Right); row.Children.Add(browse); _paths[i] = new TextBox { Text = values[i], Padding = new Thickness(4) }; row.Children.Add(_paths[i]); panel.Children.Add(row);
            _paths[i].TextChanged += (_, _) => { _previewVersion++; _folders = []; _delete.IsEnabled = false; };
            browse.Click += (_, _) => { var d = new OpenFolderDialog(); if (d.ShowDialog(this) == true) _paths[index].Text = d.FolderName; };
        }
        var buttons = new WrapPanel(); panel.Children.Add(buttons);
        Button Add(string label, RoutedEventHandler action) { var b = new Button { Content = label, Margin = new Thickness(4), Padding = new Thickness(6) }; b.Click += action; buttons.Children.Add(b); return b; }
        Add("Save folders", (_, _) => Try(() => { var d = Draft(); d.Save(); storage.Archive=d.Archive; storage.Games=d.Games; storage.BuildCache=d.BuildCache; storage.TorrentCache=d.TorrentCache; DialogResult=true; }));
        Add("Preview build cache cleanup", async (_, _) => await Preview(false));
        Add("Preview archive bin/obj cleanup", async (_, _) => await Preview(true));
        panel.Children.Add(_preview); panel.Children.Add(_delete);
        _delete.Click += async (_, _) => {
            var folders=_folders.ToArray();
            if (MessageBox.Show(this, $"Permanently delete these {folders.Length} previewed folders ({folders.Sum(f => f.Bytes)/1048576.0:N1} MB)? Close games and Visual Studio builds first.", "Delete build files", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            panel.IsEnabled=false;
            try { await Task.Run(() => { foreach (var folder in folders) StorageCleanup.Delete(folder); }); _preview.Text="Previewed folders deleted. Restore/build recreates required cache files."; }
            catch(Exception error) { _preview.Text="Cleanup stopped: " + error.Message + " Re-preview remaining folders."; }
            finally { _folders=[]; _delete.IsEnabled=false; panel.IsEnabled=true; }
        };
        panel.Children.Add(new TextBlock { Text = "Saving changes future BuildManager builds only. Existing files are not moved. Torrent snapshots are retained for seeding/resume. Visual Studio keeps its own output settings.", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0,8,0,0) });
    }
    private void Try(Action action) { try { action(); } catch (Exception e) { MessageBox.Show(this,e.Message,Title); } }
    private async Task Preview(bool legacy)
    {
        _delete.IsEnabled=false;
        int version=++_previewVersion;
        try { var draft=Draft(); _preview.Text="Scanning…"; var folders=await Task.Run(() => StorageCleanup.Preview(draft,legacy)); if(version!=_previewVersion) return; _folders=folders;
            _preview.Text=string.Join(Environment.NewLine,folders.Select(f=>$"{f.Bytes/1048576.0:N1} MB  {f.Path}")) + $"\nTotal: {folders.Sum(f=>f.Bytes)/1048576.0:N1} MB"; _delete.IsEnabled=folders.Count>0;
        } catch(Exception e) { _preview.Text=e.Message; }
    }
}
