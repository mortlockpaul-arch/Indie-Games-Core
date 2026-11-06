using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Win32;
using IndieGames.BuildManager.Services;

namespace IndieGames.BuildManager;

public sealed class PortableGamesWindow : Window
{
    readonly LibraryStorage storage;
    readonly Action<ShareSelection> share;
    readonly Action<string> parentLog;
    readonly DataGrid grid = new() { AutoGenerateColumns=false, IsReadOnly=true, CanUserAddRows=false, SelectionMode=DataGridSelectionMode.Single, SelectionUnit=DataGridSelectionUnit.FullRow, AlternationCount=2, Background=new SolidColorBrush(Color.FromRgb(24,34,52)), GridLinesVisibility=DataGridGridLinesVisibility.Horizontal, HorizontalGridLinesBrush=new SolidColorBrush(Color.FromRgb(45,59,80)), HeadersVisibility=DataGridHeadersVisibility.Column };
    readonly TextBlock details = new() { TextWrapping=TextWrapping.Wrap, Margin=new Thickness(6), Foreground=Brushes.LightSteelBlue };
    readonly TextBox log = new() { IsReadOnly=true, AcceptsReturn=true, Height=85, VerticalScrollBarVisibility=ScrollBarVisibility.Auto, FontSize=12 };
    readonly ProgressBar progress = new() { Height=5, Margin=new Thickness(4) };
    readonly WrapPanel buttons = new();
    readonly CheckBox decompPort = new() { Content = "Import as Decomp Port", Foreground = Brushes.LightSteelBlue, Margin = new Thickness(4) };
    string ImportRoot => decompPort.IsChecked == true ? storage.DecompPorts : storage.Games;
    CancellationTokenSource? work;
    PortableGame Selected => grid.SelectedItem as PortableGame ?? throw new IOException("Select a playable game first.");
    public PortableGamesWindow(LibraryStorage library, Action<ShareSelection> onShare, Action<string> onLog)
    {
        storage=library;share=onShare;parentLog=onLog;
        Title=AppVersion.WindowTitle("Playable Games & Ports");Width=1120;Height=740;MinWidth=850;MinHeight=570;WindowStartupLocation=WindowStartupLocation.CenterOwner;
        Background=new SolidColorBrush(Color.FromRgb(17,24,39));Foreground=Brushes.White;
        Resources.MergedDictionaries.Add(new ResourceDictionary {Source=new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml",UriKind.Relative)});
        var panel=new DockPanel{Margin=new Thickness(18)};Content=panel;
        var top=new StackPanel();DockPanel.SetDock(top,Dock.Top);panel.Children.Add(top);
        top.Children.Add(new TextBlock{Text="Playable Games & Ports",FontSize=24,FontWeight=FontWeights.SemiBold,Margin=new Thickness(4)});
        top.Children.Add(new TextBlock{Text="Import Windows ports, run installed games or share their playable files.",Foreground=Brushes.LightSteelBlue,Margin=new Thickness(4,2,4,10)});
        top.Children.Add(new TextBlock{Text="Games: " + storage.Games + Environment.NewLine + "Decomp Ports: " + storage.DecompPorts,Foreground=Brushes.LightSteelBlue,Margin=new Thickness(4,0,4,10),TextWrapping=TextWrapping.Wrap});
        top.Children.Add(decompPort);
        top.Children.Add(buttons);
        Add("Import ZIPs…",ImportZips);Add("Import ZIP Folder…",ImportZipFolder);Add("Import Extracted Folder…",ImportFolder);Add("Refresh",()=>Run(Refresh));Add("Help",()=>SharingHelpWindow.ShowFor(this));
        var footer=new StackPanel();DockPanel.SetDock(footer,Dock.Bottom);panel.Children.Add(footer);
        footer.Children.Add(details);
        var actions=new WrapPanel();footer.Children.Add(actions);
        void Action(string name, System.Action handler) {var button=new Button{Content=name};button.Click+=(_,_)=>{try{handler();}catch(Exception ex){Log(ex.Message);}};actions.Children.Add(button);}
        Action("Run Game",RunGame);Action("Choose Executable…",ChooseExecutable);Action("Share Playable Game…",()=>share(ShareSelection.ForPlayable(Selected.Folder)));Action("Open Folder",()=>Process.Start(new ProcessStartInfo(Selected.Folder){UseShellExecute=true}));
        var cancel=new Button{Content="Cancel Import"};cancel.Click+=(_,_)=>work?.Cancel();actions.Children.Add(cancel);
        footer.Children.Add(progress);footer.Children.Add(log);
        grid.Columns.Add(new DataGridTextColumn{Header="Game",Binding=new Binding("Name"),Width=new DataGridLength(2,DataGridLengthUnitType.Star)});
        grid.Columns.Add(new DataGridTextColumn{Header="Library Type",Binding=new Binding("Kind"),Width=130});
        grid.Columns.Add(new DataGridTextColumn{Header="Executable",Binding=new Binding("EntryPoint"),Width=new DataGridLength(2,DataGridLengthUnitType.Star)});
        grid.Columns.Add(new DataGridTextColumn{Header="Version",Binding=new Binding(nameof(PortableGame.ExecutableVersion)){TargetNullValue="—"},Width=110});
        grid.Columns.Add(new DataGridTextColumn{Header="Status",Binding=new Binding("Status"),Width=180});
        grid.SelectionChanged+=(_,_)=>{if(grid.SelectedItem is PortableGame g)details.Text=g.Folder+Environment.NewLine+g.EntryPoint;};
        grid.MouseDoubleClick+=(_,_)=>{try{RunGame();}catch(Exception ex){Log(ex.Message);}};
        panel.Children.Add(grid);Loaded+=(_,_)=>Run(Refresh);
        Closing+=(_,e)=>{if(work!=null){work.Cancel();e.Cancel=true;Log("Cancelling import. Close again after it finishes.");}};
    }
    void Add(string name, System.Action handler){var button=new Button{Content=name};button.Click+=(_,_)=>{try{handler();}catch(Exception ex){Log(ex.Message);}};buttons.Children.Add(button);}
    void Log(string message){if(!Dispatcher.CheckAccess()){Dispatcher.InvokeAsync(()=>Log(message));return;}log.AppendText(message+Environment.NewLine);log.ScrollToEnd();parentLog(message);}
    async void Run(Func<CancellationToken,Task> action)
    {
        if(work!=null)return;work=new();buttons.IsEnabled=false;progress.IsIndeterminate=true;
        try{await action(work.Token);}catch(OperationCanceledException){Log("Import cancelled; original files kept.");}catch(Exception ex){Log(ex.GetBaseException().Message);}
        finally{work.Dispose();work=null;buttons.IsEnabled=true;progress.IsIndeterminate=false;}
    }
    async Task Refresh(CancellationToken token){var games=await Task.Run(() => PortableGames.Scan(storage.Games)
            .Concat(PortableGames.Scan(storage.DecompPorts).Select(game => game with { Kind = "Decomp Port" }))
            .ToArray(),token);grid.ItemsSource=games;Log($"{games.Length} playable games found.");}
    void ImportZips(){var dialog=new OpenFileDialog{Title="Import playable game ZIPs",Filter="ZIP archives (*.zip)|*.zip",Multiselect=true};if(dialog.ShowDialog(this)==true)Import(dialog.FileNames);}
    void ImportZipFolder(){var dialog=new OpenFolderDialog{Title="Choose a folder of playable game ZIPs"};if(dialog.ShowDialog(this)==true)Import(Directory.GetFiles(dialog.FolderName,"*.zip"));}
    void Import(IEnumerable<string> paths)
    {
        string root = ImportRoot;
        Run(async token=>{foreach(var path in paths){token.ThrowIfCancellationRequested();try{await Task.Run(()=>PortableGames.ImportZipAsync(path,root,null,Log,token),token);}catch(Exception ex)when(ex is not OperationCanceledException){Log(Path.GetFileName(path)+": "+ex.Message);}}await Refresh(token);});
    }
    void ImportFolder(){var dialog=new OpenFolderDialog{Title="Choose an extracted playable game folder"};string root = ImportRoot;if(dialog.ShowDialog(this)==true)Run(async token=>{await Task.Run(()=>PortableGames.ImportFolderAsync(dialog.FolderName,root,Log,token),token);await Refresh(token);});}
    void RunGame(){var game=Selected;var exe=PortableGames.ResolveExecutable(game.Folder);GameProcessLauncher.Start(exe,GameProcessLauncher.DefaultLogDirectory,Log,PortableGames.LaunchEnvironment(game.Folder));}
    void ChooseExecutable()
    {
        var game=Selected;var dialog=new OpenFileDialog{Title="Choose the game executable",Filter="Windows executables (*.exe)|*.exe",InitialDirectory=game.Folder};
        if(dialog.ShowDialog(this)==true){PortableGames.SetExecutable(game.Folder,Path.GetRelativePath(game.Folder,dialog.FileName));Run(Refresh);}
    }
}
