using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IndieGames.BuildManager.Services;

namespace IndieGames.BuildManager;

public sealed class SharingHelpWindow : Window
{
    public static void ShowFor(Window owner) => new SharingHelpWindow(owner).ShowDialog();
    public static void ShowXnaHelp(Window owner) => new SharingHelpWindow(owner, true).ShowDialog();

    public SharingHelpWindow(Window owner, bool showXnaHelp=false)
    {
        Owner=owner; Title=AppVersion.WindowTitle("Build and Sharing Help"); Width=1040; Height=820; MinWidth=760; MinHeight=560;
        WindowStartupLocation=WindowStartupLocation.CenterOwner;
        Background=(Brush)new BrushConverter().ConvertFromString("#111827")!; Foreground=Brushes.White;
        Resources.MergedDictionaries.Add(new ResourceDictionary { Source=new Uri("/IndieGames.BuildManager;component/Themes/ManagerTheme.xaml",UriKind.Relative) });
        SourceInitialized+=(_,_)=>WindowAppearance.Apply(this);
        var root=new DockPanel { Margin=new Thickness(22) }; Content=root;
        var heading=new StackPanel { Margin=new Thickness(0,0,0,16) }; DockPanel.SetDock(heading,Dock.Top); root.Children.Add(heading);
        heading.Children.Add(new TextBlock { Text="Build and Sharing Help", FontSize=28, FontWeight=FontWeights.SemiBold });
        heading.Children.Add(new TextBlock { Text="A catalogue is a small .json file that tells the app how to find and verify a package. The package files are transferred from people who are seeding it.", FontSize=15, LineHeight=23, TextWrapping=TextWrapping.Wrap, Foreground=Brushes.LightGray, Margin=new Thickness(0,7,0,0) });
        var close=new Button { Content="Got it", IsCancel=true, HorizontalAlignment=HorizontalAlignment.Right, Margin=new Thickness(3,12,3,3) };
        close.Click+=(_,_)=>Close(); DockPanel.SetDock(close,Dock.Bottom); root.Children.Add(close);
        var tabs=new TabControl(); root.Children.Add(tabs);
        void Chapter(string name, params (string Title,string Body)[] sections)
        {
            var content=new StackPanel { Margin=new Thickness(14) };
            foreach(var section in sections)
            {
                content.Children.Add(new TextBlock { Text=section.Title, FontSize=19, FontWeight=FontWeights.SemiBold, Foreground=Brushes.White, Margin=new Thickness(0,12,0,10) });
                content.Children.Add(new TextBlock { Text=section.Body, FontSize=16, LineHeight=25, TextWrapping=TextWrapping.Wrap, Foreground=Brushes.LightGray, Margin=new Thickness(0,0,0,24) });
            }
            tabs.Items.Add(new TabItem { Header=name, Content=new ScrollViewer { Content=content, VerticalScrollBarVisibility=ScrollBarVisibility.Auto } });
        }
        Chapter("Create a Share",
            ("1 Choose the Game", "Open New share and choose a game project from the source archive. Starting from Share Source, Share Playable Game or Share Both in the main window preselects that game. You can type a title in the project list to jump to it."),
            ("2 Choose What to Include", "Source files let people edit and build the game. A playable game contains the built Windows files. Build and launch the game successfully before sharing a playable package. Both creates a source package and a game package so recipients can choose."),
            ("3 Review and Create", "The file preview and size update automatically. Check the included files; use Refresh files after changing the source or rebuilding. Click Create share and save catalogue, then choose where to save the .json catalogue. The app packages the files, saves the catalogue and starts seeding."),
            ("4 Send the Catalogue and Keep Seeding", "Send the saved .json file to the recipient, or host it at an HTTP/HTTPS URL. They open it in this app to download the package. Keep Build Manager running while they download; minimizing it is fine. Later edits do not change an existing snapshot—create a new share to distribute updated files."));
        Chapter("Download and Install",
            ("1 Open a Catalogue", "Use Open File to choose the .json file someone sent you, or paste its HTTP/HTTPS URL and click Load. My Catalogue shows packages created or stored by this app. Choose a Playable game package to play, or Source files to work on the project."),
            ("2 Download the Package", "Select a row and click Download / Resume. Wait until progress reaches 100%. Pause keeps the partial download so you can resume later. At least one person with the full package must be seeding."),
            ("3 Verify and Install", "Select the completed package and choose Verify and install. This checks its contents and unpacks it into Source Archive or Playable Games, depending on package type. Seeding at 100% means the package is available to share; installation is a separate step."),
            ("4 Play or Build", "For a Playable game, select the installed package and click Run installed game. Source packages appear in the main project list for building. Open Games Folder lets you inspect installed Windows builds."));
        Chapter("Troubleshooting",
            ("No Peers or Progress Stays at 0%", "Ask the sender to keep the app running and confirm they are seeding this snapshot. Peer discovery can take a little time. If internet peers cannot connect, check the chosen TCP/UDP port and router settings. Optional UPnP may help on supported routers. Network changes take effect after Stop Torrents in the main window."),
            ("No Playable Build Was Found", "Build the selected game in the main window, then use Refresh files in the share editor. Check that the project belongs to the selected game. A playable .NET build includes the executable and its matching runtime configuration and dependency files."),
            ("The Game Downloaded But Will Not Run", "Use Verify and install first. If the installed game still fails, use the main window's architecture check and review its launch error. The game and its native libraries must use compatible architectures. Run game captures console output in the app; Diagnostics → Open game logs opens the saved output and error files."),
            ("What Happens When I Close a Window?", "Closing the catalogue or share editor keeps existing transfers running in the main app. Use View Torrents to monitor them. Stop Torrents stops transfers; exiting Build Manager also stops them. Remove Selected removes catalogue entries and stops those transfers while keeping downloaded packages, source and installed games."));
        using (var stream=typeof(SharingHelpWindow).Assembly.GetManifestResourceStream("IndieGames.BuildManager.BuildHelp.json")
            ?? throw new InvalidOperationException("Build help resource is missing."))
        using (var document=System.Text.Json.JsonDocument.Parse(stream))
        {
            foreach(var chapter in document.RootElement.GetProperty("chapters").EnumerateArray())
            {
                Chapter(chapter.GetProperty("title").GetString()!,chapter.GetProperty("sections").EnumerateArray()
                    .Select(section=>(section.GetProperty("title").GetString()!,section.GetProperty("body").GetString()!)).ToArray());
            }
        }
        if(showXnaHelp) tabs.SelectedItem=tabs.Items.Cast<TabItem>().First(tab=>tab.Header.ToString()=="XNA DLL Repair");
    }
}
