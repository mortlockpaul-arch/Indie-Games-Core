using System.Windows;
using System.IO;
using System.Text.Json;
using IndieGames.BuildManager.Services;
namespace IndieGames.BuildManager;
public partial class App : Application
{
    private SingleInstanceHost? _instance;
    private ProjectTorrentService? _torrents;
    private CancellationTokenSource? _seedCancellation;
    private Task? _seedTask;
    private string? _cacheOverride;
    public bool BackgroundSeeding { get; private set; }
    public ProjectTorrentService Torrents => _torrents ??= new ProjectTorrentService(_cacheOverride ?? LibraryStorage.Load().TorrentCache);
    private static string StatusDirectory => Path.GetDirectoryName(LibraryStorage.SettingsPath)!;

    protected override void OnStartup(StartupEventArgs e)
    {
        bool seedOnly=e.Args.Contains("--seed-only",StringComparer.OrdinalIgnoreCase);
        int cache=Array.IndexOf(e.Args,"--torrent-cache");
        if(cache>=0 && cache+1<e.Args.Length)_cacheOverride=Path.GetFullPath(e.Args[cache+1]);
        _instance=new SingleInstanceHost();
        if(!_instance.TryStart(()=>Dispatcher.BeginInvoke(new Action(ShowMainWindow)),seedOnly,
            ()=>Dispatcher.BeginInvoke(new Action(StartBackgroundSeeding)),
            ()=>Dispatcher.BeginInvoke(new Action(async()=>await StopBackgroundSeedingAsync())))) {Shutdown();return;}
        EventManager.RegisterClassHandler(typeof(Window),FrameworkElement.LoadedEvent,new RoutedEventHandler((sender,_)=>{
            if(sender is Window window)WindowAppearance.Apply(window);
        }));
        base.OnStartup(e);
        if(seedOnly)StartBackgroundSeeding();else ShowMainWindow();
    }

    private void ShowMainWindow()
    {
        if(MainWindow is not IndieGames.BuildManager.MainWindow)MainWindow=new MainWindow();
        else WindowAppearance.FollowLaunchingWindow(MainWindow);
        if(!BackgroundSeeding)ShutdownMode=ShutdownMode.OnMainWindowClose;
        if(BackgroundSeeding && MainWindow.FindName("StopTorrentsButton") is System.Windows.Controls.Button stop)stop.IsEnabled=true;
        MainWindow.Show();if(MainWindow.WindowState==WindowState.Minimized)MainWindow.WindowState=WindowState.Normal;MainWindow.Activate();
    }

    private void StartBackgroundSeeding()
    {
        BackgroundSeeding=true;ShutdownMode=ShutdownMode.OnExplicitShutdown;
        if(_seedTask is {IsCompleted:false})return;
        _seedCancellation?.Dispose();_seedCancellation=new CancellationTokenSource();
        WriteStatus("Checking completed local packages",0);
        _seedTask=SeedAsync(_seedCancellation.Token);
    }

    private async Task SeedAsync(CancellationToken token)
    {
        try {int count=await Torrents.SeedLocalCatalogueAsync(LogSeeding,token);WriteStatus($"Seeding {count} completed package(s)",count);}
        catch(OperationCanceledException) { }
        catch(Exception error) {LogSeeding("Seeding failed: "+error.Message);WriteStatus("Seeding failed — open Build Manager",0);}
    }

    public async Task StopBackgroundSeedingAsync()
    {
        _seedCancellation?.Cancel();
        try {if(_seedTask!=null)await _seedTask;if(_torrents!=null)await _torrents.DisposeAsync();}
        catch(Exception error){LogSeeding("Stop error: "+error.Message);}
        BackgroundSeeding=false;WriteStatus("Seeding stopped",0);
        if(MainWindow==null || !MainWindow.IsVisible)Shutdown();else ShutdownMode=ShutdownMode.OnMainWindowClose;
    }

    private static void LogSeeding(string text)
    {
        try {Directory.CreateDirectory(StatusDirectory);File.AppendAllText(Path.Combine(StatusDirectory,"seeding.log"),DateTimeOffset.Now.ToString("O")+" "+text+Environment.NewLine);}
        catch(Exception error) when(error is IOException or UnauthorizedAccessException) { }
    }
    private void WriteStatus(string text,int count)
    {
        try {
            Directory.CreateDirectory(StatusDirectory);string file=Path.Combine(StatusDirectory,"seeding-status.json");
            File.WriteAllText(file+".tmp",JsonSerializer.Serialize(new {pid=Environment.ProcessId,background=BackgroundSeeding,status=text,packages=count}));File.Move(file+".tmp",file,true);
        }catch(Exception error) when(error is IOException or UnauthorizedAccessException) { }
    }
    protected override void OnExit(ExitEventArgs e)
    {
        _seedCancellation?.Cancel();
        if(_torrents!=null)Task.Run(async()=>await _torrents.DisposeAsync()).GetAwaiter().GetResult();
        BackgroundSeeding=false;WriteStatus("Seeding stopped",0);_instance?.Dispose();_seedCancellation?.Dispose();base.OnExit(e);
    }
}
