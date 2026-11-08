using IndieGames.BuildManager.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace IndieGames.BuildManager;
public sealed class StorageWindow : Window
{
    
    private readonly TextBox[] _paths = new TextBox[5];
    private readonly TextBox _solution = new();
    private readonly TextBox _preview = new() { IsReadOnly = true, Height = 160, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, AcceptsReturn = true, FontFamily = new FontFamily("Consolas"), FontSize = 12, Text = "Choose a cleanup preview to see folders and their sizes." };
    private readonly Button _delete = new() { Content = "Delete previewed folders", IsEnabled = false, Margin = new Thickness(4) };
    private IReadOnlyList<CleanupFolder> _folders = [];
    private int _previewVersion;
    private LibraryStorage Draft() => new() { Archive = _paths[0].Text.Trim(), Games = _paths[1].Text.Trim(), BuildCache = _paths[2].Text.Trim(), TorrentCache = _paths[3].Text.Trim(), Scripts = _paths[4].Text.Trim(), Solution = _solution.Text.Trim() };
    public StorageWindow(LibraryStorage storage)
    {
        Title = AppVersion.WindowTitle("Settings"); Width = 940; Height = 850; MinWidth = 740; MinHeight = 620; WindowStartupLocation = WindowStartupLocation.CenterOwner;
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml", UriKind.Relative) });
        Background = Brush("#111827"); Foreground = Brush("#E5E7EB");
        var layout = new DockPanel { Margin = new Thickness(22) }; Content = layout;
        var header = new StackPanel { Margin = new Thickness(0,0,0,18) }; DockPanel.SetDock(header, Dock.Top); layout.Children.Add(header);
        header.Children.Add(new TextBlock { Text = "Settings", FontSize = 24, FontWeight = FontWeights.SemiBold });
        header.Children.Add(new TextBlock { Text = "Manage your solution, library folders and build cache.", Foreground = Brush("#9CA3AF"), Margin = new Thickness(0,5,0,0) });
        var footer = new DockPanel { Margin = new Thickness(0,14,0,0) }; DockPanel.SetDock(footer, Dock.Bottom); layout.Children.Add(footer);
        var footerActions = new WrapPanel { HorizontalAlignment = HorizontalAlignment.Right }; DockPanel.SetDock(footerActions, Dock.Right); footer.Children.Add(footerActions);
        var save = new Button { Content = "Save Settings", Background = Brush("#2563EB"), IsDefault = true };
        var cancel = new Button { Content = "Cancel", IsCancel = true };
        footerActions.Children.Add(cancel); footerActions.Children.Add(save);
        var panel = new StackPanel { Margin = new Thickness(0,0,8,0) };
        layout.Children.Add(new ScrollViewer { Content=panel, VerticalScrollBarVisibility=ScrollBarVisibility.Auto, HorizontalScrollBarVisibility=ScrollBarVisibility.Disabled });
        StackPanel Section(string title, string description)
        {
            var section = new StackPanel();
            section.Children.Add(new TextBlock { Text = title, FontSize = 16, FontWeight = FontWeights.SemiBold });
            section.Children.Add(new TextBlock { Text = description, Foreground = Brush("#9CA3AF"), FontSize = 12, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0,5,0,10) });
            panel.Children.Add(new Border { Background = Brush("#182234"), BorderBrush = Brush("#374151"), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(10), Padding = new Thickness(16), Margin = new Thickness(0,0,0,14), Child = section });
            return section;
        }
        var experience = Section("App Experience", "Preferences apply immediately across Build Manager and are remembered.");
        var weird = new CheckBox {
            Content = "Weird Mode", IsChecked = NavigationSounds.Shared.Enabled,
            Foreground = Foreground, Margin = new Thickness(0, 2, 0, 4),
            ToolTip = "Quiet synthesized feedback throughout the app. Turn off to mute immediately."
        };
        weird.Checked += (_, _) => NavigationSounds.Shared.SetEnabled(true);
        weird.Unchecked += (_, _) => NavigationSounds.Shared.SetEnabled(false);
        experience.Children.Add(weird);
        experience.Children.Add(new TextBlock { Text = "Quiet sounds for buttons, options and navigation.", FontSize = 12, Foreground = Brush("#9CA3AF"), TextWrapping = TextWrapping.Wrap });
        var solutionSection = Section("Solution", "Add and Remove project actions use this solution automatically.");
        var foldersSection = Section("Library Folders", "Keep source files, playable games, build cache and torrent snapshots in separate folders.");
        string[] labels = ["Source archive", "Playable Windows games", "Disposable build cache", "Torrent snapshots / downloads", "Shared scripts (same folder as the Python app)"];
        string[] values = [storage.Archive, storage.Games, storage.BuildCache, storage.TorrentCache, storage.Scripts];
        for (int i = 0; i < 5; i++)
        {
            int index = i; foldersSection.Children.Add(new TextBlock { Text = labels[i], Margin = new Thickness(3,8,0,2), FontSize = 12 });
            var row = new DockPanel { Margin = new Thickness(0,0,0,3) }; var open = new Button { Content = "Open", MinWidth = 64, VerticalAlignment = VerticalAlignment.Center };
            DockPanel.SetDock(open, Dock.Right); row.Children.Add(open);
            open.Click += (_, _) => Try(() => { string folder=Path.GetFullPath(_paths[index].Text.Trim()); Directory.CreateDirectory(folder); System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(folder) { UseShellExecute=true }); });
            var browse = new Button { Content = "Browse…", MinWidth = 76, VerticalAlignment = VerticalAlignment.Center };
            DockPanel.SetDock(browse, Dock.Right); row.Children.Add(browse); _paths[i] = new TextBox { Text = values[i], Height = 34, VerticalContentAlignment = VerticalAlignment.Center, FontSize = 12 }; row.Children.Add(_paths[i]); foldersSection.Children.Add(row);
            _paths[i].TextChanged += (_, _) => { _previewVersion++; _folders = []; _delete.IsEnabled = false; };
            browse.Click += (_, _) => { var d = new OpenFolderDialog(); if (d.ShowDialog(this) == true) _paths[index].Text = d.FolderName; };
        }
        var solutionRow = new DockPanel();
        var solutionBrowse = new Button { Content = "Browse…", MinWidth = 76, VerticalAlignment = VerticalAlignment.Center };
        DockPanel.SetDock(solutionBrowse, Dock.Right); solutionRow.Children.Add(solutionBrowse);
        _solution.Text = storage.Solution; _solution.Height = 34; _solution.VerticalContentAlignment = VerticalAlignment.Center; _solution.FontSize = 12; solutionRow.Children.Add(_solution);
        solutionSection.Children.Add(solutionRow);
        solutionBrowse.Click += (_, _) => {
            var picker = new OpenFileDialog { Title = "Set solution", Filter = "Solutions (*.sln;*.slnx)|*.sln;*.slnx", CheckFileExists = true };
            if (picker.ShowDialog(this) == true) _solution.Text = picker.FileName;
        };
        var cleanup = Section("Build Cleanup", "Preview disposable build folders before removing them. Source files and torrent snapshots are kept.");
        var buttons = new WrapPanel { Margin = new Thickness(0,0,0,7) }; cleanup.Children.Add(buttons);
        Button Add(string label, RoutedEventHandler action) { var b = new Button { Content = label }; b.Click += action; buttons.Children.Add(b); return b; }
        save.Click += (_, _) => Try(() => {
            var d = Draft();
            if (!Path.IsPathFullyQualified(d.Solution) || !File.Exists(d.Solution) || Path.GetExtension(d.Solution).ToLowerInvariant() is not (".sln" or ".slnx"))
                throw new IOException("Set an existing .sln or .slnx solution file.");
            if (!Path.IsPathFullyQualified(d.Scripts) || !Directory.Exists(d.Scripts)) throw new IOException("Set an existing shared scripts folder.");
            d.Save(); storage.Archive=d.Archive; storage.Games=d.Games; storage.BuildCache=d.BuildCache; storage.TorrentCache=d.TorrentCache; storage.Solution=d.Solution; storage.Scripts=d.Scripts; DialogResult=true;
        });
        Add("Preview build cache", async (_, _) => await Preview(false));
        Add("Preview archive bin / obj", async (_, _) => await Preview(true));
        cleanup.Children.Add(_preview);
        _delete.HorizontalAlignment = HorizontalAlignment.Left; _delete.Background = Brush("#713F46"); _delete.BorderBrush = Brush("#A65B65"); _delete.Margin = new Thickness(3,10,3,3);
        cleanup.Children.Add(_delete);
        _delete.Click += async (_, _) => {
            var folders=_folders.ToArray();
            if (MessageBox.Show(this, $"Permanently delete these {folders.Length} previewed folders ({folders.Sum(f => f.Bytes)/1048576.0:N1} MB)? Close games and Visual Studio builds first.", "Delete build files", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            panel.IsEnabled=false; footer.IsEnabled=false;
            try { await Task.Run(() => { foreach (var folder in folders) StorageCleanup.Delete(folder); }); _preview.Text="Previewed folders deleted. Restore/build recreates required cache files."; }
            catch(Exception error) { _preview.Text="Cleanup stopped: " + error.Message + " Re-preview remaining folders."; }
            finally { _folders=[]; _delete.IsEnabled=false; panel.IsEnabled=true; footer.IsEnabled=true; }
        };
        panel.Children.Add(new TextBlock { Text = "Folder changes apply to future builds. Existing files stay where they are; Visual Studio retains its own output settings.", Foreground = Brush("#9CA3AF"), FontSize = 12, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(3,0,3,5) });
    }
    private static SolidColorBrush Brush(string color) => (SolidColorBrush)new BrushConverter().ConvertFromString(color)!;
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
