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
        heading.Children.Add(new TextBlock { Text="The global catalogue loads approved projects automatically from GitHub. Package files transfer from people who are seeding them. No catalogue files need to be exchanged.", FontSize=15, LineHeight=23, TextWrapping=TextWrapping.Wrap, Foreground=Brushes.LightGray, Margin=new Thickness(0,7,0,0) });
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
        Chapter("Publish a Project",
            ("1 Connect and Choose the Game", "Choose Connect GitHub and complete browser sign-in. If GitHub CLI is missing, its installer opens first. Publishing uses your own GitHub account. Choose Publish project and select a game. Starting from Share Source, Share Playable Game or Share Both in the main window preselects it."),
            ("2 Choose What to Include", "Source files let people edit and build the game. A playable game contains the built Windows files. Build and launch the game successfully before sharing a playable package. Both creates a source package and a game package so recipients can choose."),
            ("3 Review and Submit", "The file preview and size update automatically. Check the included files and publish only material you have permission to share. Choose Publish for approval. Build Manager packages the files, starts seeding and submits their metadata to GitHub for review. There is no file to save or send."),
            ("4 Wait for Approval and Keep Seeding", "My submissions opens your requests on GitHub. The catalogue owner reviews and merges accepted submissions. They then appear for everyone automatically. Keep Build Manager running so people can download your package. If submission fails, your package stays local; select it and use Publish selected to retry without duplicating the request."));
        Chapter("Download and Install",
            ("1 Browse the Global Catalogue", "Approved entries load automatically when the catalogue opens and refresh every five minutes while visible. Refresh catalogue checks immediately. No GitHub account is needed to download. Offline, the last verified catalogue remains available. Local or awaiting-approval rows are your own packages and are not yet public."),
            ("2 Download the Package", "Select a row and click Download / Resume. Wait until progress reaches 100%. Pause keeps the partial download so you can resume later. At least one person with the full package must be seeding."),
            ("3 Verify and Install", "Select the completed package and choose Verify and install. This checks its contents and unpacks it into Source Archive or Playable Games, depending on package type. Seeding at 100% means the package is available to share; installation is a separate step."),
            ("4 Play or Build", "For a Playable game, select the installed package and click Run installed game. Source packages appear in the main project list for building. Open Games Folder lets you inspect installed Windows builds."));
        Chapter("Troubleshooting",
            ("No Peers or Progress Stays at 0%", "Ask the sender to keep the app running and confirm they are seeding this snapshot. Peer discovery can take a little time. If internet peers cannot connect, check the chosen TCP/UDP port and router settings. Optional UPnP may help on supported routers. Network changes take effect after Stop Torrents in the main window."),
            ("No Playable Build Was Found", "Build the selected game in the main window, then use Refresh files in the share editor. Check that the project belongs to the selected game. A playable .NET build includes the executable and its matching runtime configuration and dependency files."),
            ("The Game Downloaded But Will Not Run", "Use Verify and install first. If the installed game still fails, use the main window's architecture check and review its launch error. The game and its native libraries must use compatible architectures. Run game captures console output in the app; Diagnostics → Open game logs opens the saved output and error files."),
            ("What Happens When I Close a Window?", "Closing the catalogue or share editor keeps transfers running. Stop Torrents stops them; exiting Build Manager also stops them. Remove Selected hides packages on this PC and stops their transfers while keeping files. It does not remove approved entries globally. Show hidden packages restores your local view."),
            ("How Are Submissions Approved?", "Review submissions opens the GitHub review queue. The owner reviews each request and merges accepted packages. Closing a request rejects it. The app never merges submissions automatically; GitHub hosts metadata, and actual package downloads still need a seeder."));
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
