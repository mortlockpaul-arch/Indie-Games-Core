using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IndieGames.BuildManager;
using IndieGames.BuildManager.Services;

internal static class Program
{
    [STAThread]
    private static int Main()
    {
        string root=Path.Combine(Path.GetTempPath(),"BuildManagerUiSmoke-"+Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        var app=new Application { ShutdownMode=ShutdownMode.OnExplicitShutdown };
        SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
        try
        {
            if(Environment.GetEnvironmentVariable("UI_HELP_ONLY")=="1")
            {
                var owner=new Window();owner.Show();
                var help=new SharingHelpWindow(owner,true);help.Show();help.UpdateLayout();
                var tabs=((DockPanel)help.Content).Children.OfType<TabControl>().Single();
                if(((TabItem)tabs.SelectedItem).Header.ToString()!="XNA DLL repair")throw new Exception("XNA help selection incorrect");
                var shared=tabs.Items.Cast<TabItem>().Single(t=>t.Header.ToString()=="Shared SunBurn");tabs.SelectedItem=shared;help.UpdateLayout();
                var text=string.Join("\n",((StackPanel)((ScrollViewer)shared.Content).Content).Children.OfType<TextBlock>().Select(t=>t.Text));
                if(!text.Contains("UseSharedSunBurn") || !text.Contains("--replacement") || !text.Contains("DigitalRune"))throw new Exception("Shared help content missing");
                string? shot=Environment.GetEnvironmentVariable("BUILDMANAGER_HELP_SCREENSHOT");if(shot!=null)SaveImage(help,shot);
                help.Close();owner.Close();app.Shutdown();
                Console.WriteLine("PASS embedded shared help, XNA tab selection and SunBurn documentation rendering");return 0;
            }
            string source=Path.Combine(root,"Example"), game=Path.Combine(root,"Built Example");
            Directory.CreateDirectory(source); Directory.CreateDirectory(game);
            File.WriteAllText(Path.Combine(source,"Example.csproj"),"<Project><PropertyGroup><OutputType>Exe</OutputType></PropertyGroup></Project>");
            var pixels=Enumerable.Repeat((byte)180,16*16*4).ToArray();
            var artwork=BitmapSource.Create(16,16,96,96,PixelFormats.Bgra32,null,pixels,16*4);
            var artworkEncoder=new PngBitmapEncoder(); artworkEncoder.Frames.Add(BitmapFrame.Create(artwork));
            using(var artworkFile=File.Create(Path.Combine(source,"DashboardIcon.png")))artworkEncoder.Save(artworkFile);
            string encodedArtwork=CatalogueArtwork.Encode(source);
            if(CatalogueArtwork.ForProject(new TorrentProject { IconBase64=encodedArtwork },root,root)==null)throw new Exception("Portable catalogue icon could not be decoded");
            using(var unlocked=File.Open(Path.Combine(source,"DashboardIcon.png"),FileMode.Open,FileAccess.Read,FileShare.None)) { }
            foreach(string file in new[]{"Example.exe","Example.deps.json","Example.runtimeconfig.json"})File.WriteAllText(Path.Combine(game,file),"fixture");
            string otherSource=Path.Combine(root,"Other"); Directory.CreateDirectory(otherSource);
            string otherProject=Path.Combine(otherSource,"Other.csproj"); File.WriteAllText(otherProject,"<Project><PropertyGroup><OutputType>Exe</OutputType></PropertyGroup></Project>");
            string otherGame=new LibraryStorage { Games=Path.Combine(root,"games") }.GameFolder(otherProject); Directory.CreateDirectory(otherGame);
            foreach(string file in new[]{"Other.exe","Other.deps.json","Other.runtimeconfig.json"})File.WriteAllText(Path.Combine(otherGame,file),"fixture");
            if(Environment.GetEnvironmentVariable("UI_MAIN_ONLY")!="1") { var catalogue=new TorrentWindow(root,source,Path.Combine(root,"cache"),Path.Combine(root,"games"),game,
                globalCatalogue:new GlobalCatalogue(Path.Combine(root,"cache"),_=>Task.FromResult(new TorrentCatalogue())));
            catalogue.Show();
            if(catalogue.FindName("CatalogueLocationBox")!=null)throw new Exception("Manual catalogue loading is still exposed");
            if(catalogue.FindName("GlobalCatalogueStatus") is not TextBlock)throw new Exception("Automatic catalogue status is missing");
            if(((Button)catalogue.FindName("CreateShareButton")).Content.ToString()!="Publish for approval")throw new Exception("Publish approval action is missing");
            foreach(int kind in new[]{0,1,2})
            {
                Console.WriteLine($"Checking automatic share preview for package type {kind}");
                Exception? failure=null; bool inspected=false; int phase=0; DateTime deadline=DateTime.UtcNow.AddSeconds(20);
                var timer=new DispatcherTimer { Interval=TimeSpan.FromMilliseconds(50) };
                timer.Tick+=(_,_)=>
                {
                    var share=app.Windows.OfType<ProjectShareWindow>().SingleOrDefault();
                    if (share == null) return;
                    if (failure != null) { share.Close(); if(!share.IsVisible)timer.Stop(); return; }
                    if (!((Button)catalogue.FindName("CreateShareButton")).IsEnabled && DateTime.UtcNow<deadline) return;
                    try
                    {
                        if(!share.Title.EndsWith(" Version "+AppVersion.Current))throw new Exception("Share window version is missing");
                        if (!((Button)catalogue.FindName("CreateShareButton")).IsEnabled) throw new Exception("Automatic share preview did not finish: "+((TextBlock)catalogue.FindName("ShareSummary")).Text);
                        var sourceRow=(FrameworkElement)catalogue.FindName("SourceShareRow");
                        var gameRow=(FrameworkElement)catalogue.FindName("GameShareRow");
                        if (sourceRow.Visibility!=Visibility.Visible || (gameRow.Visibility==Visibility.Visible)!=(kind!=0)) throw new Exception("Share kind shows incorrect folder fields");
                        if (!((TextBlock)catalogue.FindName("ShareSummary")).Text.StartsWith(kind==2 ? "2 package(s)" : "1 package(s)"))throw new Exception("Wrong preview package count");
                        var sources=(ComboBox)catalogue.FindName("ShareFolderBox");
                        var output=(TextBox)catalogue.FindName("GameFolderBox");
                        if (!output.IsReadOnly || sources.Items.Count!=3)throw new Exception("Source/playable dropdown or automatic output is incorrect");
                        var create=(Button)catalogue.FindName("CreateShareButton");
                        if(!create.IsVisible || create.TransformToAncestor(share).Transform(new Point(0,create.ActualHeight)).Y>share.ActualHeight)throw new Exception("Primary share action is not visible in the sticky footer");
                        string? shareScreenshot=Environment.GetEnvironmentVariable("BUILDMANAGER_SHARE_SCREENSHOT");
                        if(kind==0 && phase==0 && !string.IsNullOrWhiteSpace(shareScreenshot))SaveImage(share,shareScreenshot);
                        if (phase==0) { phase=1; sources.SelectedValue=otherSource; return; }
                        if (phase==1) {
                            if(output.Text!=otherGame || !((TextBox)catalogue.FindName("ShareFilesBox")).Text.Contains("Other"))throw new Exception("Dropdown selection did not refresh output and preview");
                            phase=2; sources.SelectedValue=source; sources.SelectedValue=otherSource; sources.SelectedValue=source; return;
                        }
                        if(output.Text!=game || ((TextBox)catalogue.FindName("ShareFilesBox")).Text.Contains("Other"))throw new Exception("Rapid selection changes left a stale preview");
                        inspected=true;
                    }
                    catch(Exception e){failure=e; Console.Error.WriteLine(e.Message);}
                    if(inspected)timer.Stop(); share.Close();
                };
                timer.Start(); catalogue.OpenShare(kind,new(source,game,"Example")); timer.Stop();
                if(failure!=null)throw failure;
                if(!inspected)throw new Exception("Share window was not inspected");
                if(((ContentControl)catalogue.FindName("ShareHost")).Content==null)throw new Exception("Share editor was not restored after closing");
            }
            var rows=(System.Collections.IList)typeof(TorrentWindow).GetField("_rows",BindingFlags.NonPublic|BindingFlags.Instance)!.GetValue(catalogue)!;
            var createRow=typeof(TorrentWindow).GetMethod("CreateRow",BindingFlags.NonPublic|BindingFlags.Instance)!;
            rows.Add(createRow.Invoke(catalogue,[new TorrentProject { Name="Example game",FolderName="Example",PackageKind="Game",PackageBytes=52107538,IconBase64=encodedArtwork }])!);
            ((DataGrid)catalogue.FindName("CatalogueGrid")).SelectedIndex=0;
            catalogue.UpdateLayout();
            string? catalogueScreenshot=Environment.GetEnvironmentVariable("BUILDMANAGER_CATALOGUE_SCREENSHOT");
            if(!string.IsNullOrWhiteSpace(catalogueScreenshot))SaveImage(catalogue,catalogueScreenshot);
            var help=new SharingHelpWindow(catalogue);help.Show();help.UpdateLayout();
            string? helpScreenshot=Environment.GetEnvironmentVariable("BUILDMANAGER_HELP_SCREENSHOT");
            if(!string.IsNullOrWhiteSpace(helpScreenshot))SaveImage(help,helpScreenshot);
            help.Close();
            Console.WriteLine("PASS portable artwork, unlocked icon files, sticky share actions and help window");
            catalogue.Close();
            if(catalogue.IsVisible)throw new Exception("Closing torrent manager should hide it");
            catalogue.ShutdownAsync().GetAwaiter().GetResult();
            Console.WriteLine("PASS WPF source dropdown, automatic output/preview, rapid selection changes, all package types and editor reuse");

            } var main=new MainWindow { WindowState=WindowState.Normal };
            ((TextBox)main.FindName("RootBox")).Text=root;
            main.Show();
            var frame=new DispatcherFrame(); Exception? scanFailure=null; DateTime scanDeadline=DateTime.UtcNow.AddSeconds(20);
            var scanTimer=new DispatcherTimer { Interval=TimeSpan.FromMilliseconds(50) };
            scanTimer.Tick+=(_,_)=> {
                string status=((TextBlock)main.FindName("FolderScanStatus")).Text;
                if(!status.StartsWith("Scan Complete") && DateTime.UtcNow<scanDeadline)return;
                if(!status.StartsWith("Scan Complete"))scanFailure=new Exception(status);
                scanTimer.Stop(); frame.Continue=false;
            };
            scanTimer.Start(); Dispatcher.PushFrame(frame);
            if(scanFailure!=null)throw scanFailure;
            var projectsGrid=(DataGrid)main.FindName("ProjectsGrid");
            if(projectsGrid.Columns.Any(c=>c.Header?.ToString()=="Repair"))throw new Exception("Game repair checkbox column is still present");
            if(projectsGrid.Items.Count<2)throw new Exception("Selection fixtures were not scanned");
            ((DispatcherTimer)typeof(MainWindow).GetField("_membershipTimer",BindingFlags.NonPublic|BindingFlags.Instance)!.GetValue(main)!).Stop(); Dispatcher.CurrentDispatcher.Invoke(()=>{},DispatcherPriority.ApplicationIdle); ((IndieGames.BuildManager.Models.GameProject)projectsGrid.Items[0]).IsInSolution=true;
            projectsGrid.SelectedIndex=1;main.UpdateLayout(); Dispatcher.CurrentDispatcher.Invoke(()=>{},DispatcherPriority.ApplicationIdle);
            var memberRow=(DataGridRow)projectsGrid.ItemContainerGenerator.ContainerFromIndex(0);
            if(memberRow.Background is not SolidColorBrush memberBrush || (memberBrush.Color!=Color.FromRgb(30,58,52) && memberBrush.Color!=Color.FromRgb(41,78,68)))throw new Exception("Solution member is not tinted green: "+memberRow.Background+" membership="+((IndieGames.BuildManager.Models.GameProject)projectsGrid.Items[0]).IsInSolution);
            projectsGrid.SelectedIndex=0;
            main.UpdateLayout();
            if(memberRow.Background is not SolidColorBrush selectedBrush || selectedBrush.Color!=Color.FromRgb(37,85,138))throw new Exception("Selection did not override the solution tint");
            var selected=(IndieGames.BuildManager.Models.GameProject)projectsGrid.SelectedItem;
            if(((TextBox)main.FindName("SelectedGameFolderBox")).Text!=Path.GetDirectoryName(selected.Path))throw new Exception("Selected game folder is incorrect");
            if(!((Button)main.FindName("OpenSelectedFolderButton")).IsEnabled)throw new Exception("Open folder is disabled for a selected row");
            projectsGrid.SelectedItems.Add(projectsGrid.Items[1]);
            if(((TextBlock)main.FindName("GameSelectionCount")).Text!="2 games selected")throw new Exception("Multiple row selection count is incorrect");
            foreach(var size in new[]{new Size(1400,850),new Size(1000,660)})
            {
                main.Width=size.Width; main.Height=size.Height; main.UpdateLayout();
                var info=(TextBox)main.FindName("SelectedGameFolderBox");
                if(info.TransformToAncestor(main).Transform(new Point()).Y < projectsGrid.TransformToAncestor(main).Transform(new Point(0,projectsGrid.ActualHeight)).Y)
                    throw new Exception("Game info is not below the grid");
                if(projectsGrid.ActualHeight<60)throw new Exception("Project grid has no usable height");
            }
            projectsGrid.UnselectAll();
            if(((Button)main.FindName("OpenSelectedFolderButton")).IsEnabled || ((TextBlock)main.FindName("SelectedGameName")).Text!="No game selected")
                throw new Exception("Game info was not cleared after deselection");
            main.Width=1400; main.Height=850; projectsGrid.SelectedIndex=0; main.UpdateLayout();
            string? screenshot=Environment.GetEnvironmentVariable("BUILDMANAGER_UI_SCREENSHOT");
            if(!string.IsNullOrWhiteSpace(screenshot))
            {
                var bitmap=new RenderTargetBitmap((int)main.ActualWidth,(int)main.ActualHeight,96,96,PixelFormats.Pbgra32);
                bitmap.Render(main); var encoder=new PngBitmapEncoder(); encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using var imageStream=File.Create(screenshot); encoder.Save(imageStream);
            }
            Console.WriteLine("PASS row selection, game info, removal of repair checkboxes and layout at normal/minimum sizes");
            if(!main.Title.EndsWith(" Version "+AppVersion.Current))throw new Exception("Main window version is missing");
            var bar=(ProgressBar)main.FindName("FolderScanProgress");
            if(bar.IsIndeterminate || bar.Value!=bar.Maximum)throw new Exception("Completed scan progress bar is incorrect");
            typeof(MainWindow).GetMethod("UpdateTorrentStatus",BindingFlags.Instance|BindingFlags.NonPublic)!.Invoke(main,[new TorrentStatusSnapshot([new("Example","Source","Seeding",100,1,0,1024)],"Seeding Example")]);
            if(((DataGrid)main.FindName("TorrentTransfersGrid")).Items.Count!=1 || !((TextBlock)main.FindName("TorrentSummary")).Text.Contains("1 Seeding"))throw new Exception("Main torrent status did not update");
            main.Close(); Console.WriteLine("PASS WPF main scan completion/progress bar and torrent status bindings");
            return 0;
        }
        catch(Exception e){Console.Error.WriteLine(e);return 1;}
        finally { app.Shutdown(); }
    }

    private static void SaveImage(Window window,string path)
    {
        window.UpdateLayout();
        window.Dispatcher.Invoke(()=>{},DispatcherPriority.ApplicationIdle);window.UpdateLayout();
        var bitmap=new RenderTargetBitmap((int)window.ActualWidth,(int)window.ActualHeight,96,96,PixelFormats.Pbgra32);
        bitmap.Render(window);var encoder=new PngBitmapEncoder();encoder.Frames.Add(BitmapFrame.Create(bitmap));
        using var stream=File.Create(path);encoder.Save(stream);
    }
}
