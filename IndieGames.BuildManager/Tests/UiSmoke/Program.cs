using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
