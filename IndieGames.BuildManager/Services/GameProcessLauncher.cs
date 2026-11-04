using System.Diagnostics;
using System.IO;
using System.Text;

namespace IndieGames.BuildManager.Services;

public sealed record GameLaunch(int ProcessId, string LogPath, Task<int> Completion);

public static class GameProcessLauncher
{
    public static string DefaultLogDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"IndieGames.BuildManager","game-logs");
    public static GameLaunch Start(string executable, string logsDirectory, Action<string> log, IReadOnlyDictionary<string, string>? environment = null)
    {
        executable=Path.GetFullPath(executable);
        Directory.CreateDirectory(logsDirectory);
        string name=Path.GetFileNameWithoutExtension(executable);
        string path=Path.Combine(logsDirectory,$"{name}-{DateTime.Now:yyyyMMdd-HHmmss-fff}-{Guid.NewGuid():N}.log");
        var writer=new StreamWriter(path,false,new UTF8Encoding(false)) { AutoFlush=true };
        var process=new Process { StartInfo=new ProcessStartInfo(executable) {
            WorkingDirectory=Path.GetDirectoryName(executable)!, UseShellExecute=false,
            RedirectStandardOutput=true, RedirectStandardError=true, CreateNoWindow=true
        } };
        if (environment != null) foreach (var pair in environment) process.StartInfo.Environment[pair.Key] = pair.Value;
        process.StartInfo.Environment["INDIE_GAME_CAPTURED"]="1";
        process.StartInfo.Environment["INDIE_GAME_LOG_FILE"]=Path.GetFullPath(path);
        process.StartInfo.Environment["INDIE_GAME_LOG_DIRECTORY"]=Path.GetFullPath(logsDirectory);
        try { process.Start(); }
        catch { writer.Dispose(); process.Dispose(); throw; }
        int pid=process.Id;
        writer.WriteLine($"Executable: {executable}\nProcess: {pid}\nStarted: {DateTimeOffset.Now:O}");
        log("Game log: " + path);
        var gate=new object();
        var completion=Task.Run(async () => {
            void Output(string line, bool error)
            {
                string text=(error ? "[stderr] " : "")+line;
                lock(gate)writer.WriteLine(text);
                try { log($"[{name}] {text}"); } catch (InvalidOperationException) { }
            }
            async Task Pump(StreamReader stream,bool error)
            {
                while(await stream.ReadLineAsync() is { } line)Output(line,error);
            }
            try
            {
                await Task.WhenAll(Pump(process.StandardOutput,false),Pump(process.StandardError,true),process.WaitForExitAsync());
                int code=process.ExitCode; Output($"Exited with code {code}.",false); return code;
            }
            catch(Exception ex)
            {
                try { log($"Game output capture ended: {ex.Message}. Log: {path}"); } catch (InvalidOperationException) { }
                return -1;
            }
            finally { writer.Dispose(); process.Dispose(); }
        });
        return new(pid,path,completion);
    }
}
