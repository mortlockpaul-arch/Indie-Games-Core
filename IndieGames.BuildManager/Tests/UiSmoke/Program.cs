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
            string source=Path.Combine(root,"Example"), game=Path.Combine(root,"Built Example");
            Directory.CreateDirectory(source); Directory.CreateDirectory(game);
            File.WriteAllText(Path.Combine(source,"Example.csproj"),"<Project><PropertyGroup><OutputType>Exe</OutputType></PropertyGroup></Project>");
            foreach(string file in new[]{"Example.exe","Example.deps.json","Example.runtimeconfig.json"})File.WriteAllText(Path.Combine(game,file),"fixture");
            string otherSource=Path.Combine(root,"Other"); Directory.CreateDirectory(otherSource);
            string otherProject=Path.Combine(otherSource,"Other.csproj"); File.WriteAllText(otherProject,"<Project><PropertyGroup><OutputType>Exe</OutputType></PropertyGroup></Project>");
            string otherGame=new LibraryStorage { Games=Path.Combine(root,"games") }.GameFolder(otherProject); Directory.CreateDirectory(otherGame);
            foreach(string file in new[]{"Other.exe","Other.deps.json","Other.runtimeconfig.json"})File.WriteAllText(Path.Combine(otherGame,file),"fixture");
            var catalogue=new TorrentWindow(root,source,Path.Combine(root,"cache"),Path.Combine(root,"games"),game);
            catalogue.Show();
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
                        if (!output.IsReadOnly || sources.Items.Count!=2)throw new Exception("Source dropdown or automatic output is incorrect");
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
            catalogue.Close();
            if(catalogue.IsVisible)throw new Exception("Closing torrent manager should hide it");
            catalogue.ShutdownAsync().GetAwaiter().GetResult();
            Console.WriteLine("PASS WPF source dropdown, automatic output/preview, rapid selection changes, all package types and editor reuse");

            var main=new MainWindow();
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
            projectsGrid.SelectedIndex=0;
            var selected=(IndieGames.BuildManager.Models.GameProject)projectsGrid.SelectedItem;
            if(((TextBox)main.FindName("SelectedGameFolderBox")).Text!=Path.GetDirectoryName(selected.Path))throw new Exception("Selected game folder is incorrect");
            if(!((Button)main.FindName("OpenSelectedFolderButton")).IsEnabled)throw new Exception("Open folder is disabled for a selected row");
            projectsGrid.SelectedItems.Add(projectsGrid.Items[1]);
            if(((TextBlock)main.FindName("GameSelectionCount")).Text!="2 games selected")throw new Exception("Multiple row selection count is incorrect");
            foreach(var size in new[]{new Size(1300,790),new Size(950,620)})
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
            main.Width=1300; main.Height=790; projectsGrid.SelectedIndex=0; main.UpdateLayout();
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
}
