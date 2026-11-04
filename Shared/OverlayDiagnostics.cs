using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IndieGames.Overlay;

public static class OverlayDiagnostics
{
    static readonly object Gate=new();
    static readonly Queue<string> Recent=new();
    static readonly List<string> Startup=new();
    static bool attached;
    public static void StartCapture()
    {
        lock(Gate)
        {
            if(attached)return;attached=true;
            Console.SetOut(TextWriter.Synchronized(new CaptureWriter(Console.Out)));
            Console.SetError(TextWriter.Synchronized(new CaptureWriter(Console.Error)));
#if FNA_OVERLAY
            Microsoft.Xna.Framework.FNALoggerEXT.LogInfo ??= Console.WriteLine;
            Microsoft.Xna.Framework.FNALoggerEXT.LogWarn ??= Console.WriteLine;
            Microsoft.Xna.Framework.FNALoggerEXT.LogError ??= Console.WriteLine;
#endif
        }
    }
    public static void Add(string line)
    {
        if(string.IsNullOrWhiteSpace(line))return;
        line=line.Length>2000?line[..2000]:line;
        lock(Gate)
        {
            if(Recent.Count>0&&Recent.Last()==line)return;
            if(Startup.Count<300)Startup.Add(line);
            Recent.Enqueue(line);while(Recent.Count>400)Recent.Dequeue();
        }
    }
    public static string[] Lines(bool startup=false){lock(Gate)return startup?Startup.ToArray():Recent.ToArray();}
    public static string Renderer()
    {
        foreach(string line in Lines().Reverse())
            foreach(string tag in new[]{"Vulkan Device:","OpenGL Renderer:","D3D11 Device:","SDL_GPU Device:"})
                if(line.Contains(tag,StringComparison.OrdinalIgnoreCase))return line[(line.IndexOf(tag,StringComparison.OrdinalIgnoreCase)+tag.Length)..].Trim();
        return "";
    }
    sealed class CaptureWriter(TextWriter original) : TextWriter
    {
        readonly StringBuilder pending=new();
        public override Encoding Encoding=>original.Encoding;
        public override void Write(char value)=>Write(value.ToString());
        public override void Write(string? value)
        {
            try{original.Write(value);}catch(IOException){}catch(ObjectDisposedException){}
            if(value==null)return;
            foreach(char c in value){if(c=='\n'){Add(pending.ToString().TrimEnd('\r'));pending.Clear();}else if(pending.Length<2000)pending.Append(c);}
        }
        public override void WriteLine(string? value)=>Write((value??"")+Environment.NewLine);
        public override void WriteLine()=>Write(Environment.NewLine);
        public override void Flush(){try{original.Flush();}catch(IOException){}catch(ObjectDisposedException){}}
    }
}

public sealed record OverlayMetrics(double Fps,double CpuPercent,long WorkingSet,long ManagedBytes,double? GpuPercent,double? DedicatedGpuBytes,double? SharedGpuBytes)
{
    public string Summary=>WorkingSet==0?"Collecting performance samples...":$"{Fps:0.0} FPS | CPU app {CpuPercent:0.0}% | RAM {WorkingSet/1048576d:0} MB";
    public string Gpu=>$"GPU 3D app {(GpuPercent.HasValue?$"{GpuPercent:0.0}%":"unavailable")} | Mem {(DedicatedGpuBytes.HasValue?$"{DedicatedGpuBytes/1048576d:0} MB":"unavailable")} + {(SharedGpuBytes.HasValue?$"{SharedGpuBytes/1048576d:0} MB shared":"shared n/a")}";
}

public sealed class OverlayMetricSampler : IDisposable
{
    readonly CancellationTokenSource stop=new();
    readonly Task worker;
    readonly Task gpuWorker;
    sealed record GpuSample(double? Usage,double? Dedicated,double? Shared);
    GpuSample gpuLatest=new(null,null,null);
    OverlayMetrics latest=new(0,0,0,0,null,null,null);
    int frames,disposed;
    public OverlayMetrics Current=>Volatile.Read(ref latest);
    public void Frame()=>Interlocked.Increment(ref frames);
    public OverlayMetricSampler(){OverlayDiagnostics.StartCapture();worker=Task.Run(Collect);gpuWorker=Task.Run(CollectGpu);}
    async Task CollectGpu()
    {
        using var gpu=new WindowsGpuCounters();using var timer=new PeriodicTimer(TimeSpan.FromSeconds(1));
        try{while(await timer.WaitForNextTickAsync(stop.Token)){var sample=gpu.Read();Volatile.Write(ref gpuLatest,new(sample.Usage,sample.Dedicated,sample.Shared));}}
        catch(OperationCanceledException){}
        catch(Exception ex){OverlayDiagnostics.Add("GPU counters unavailable: "+ex.GetBaseException().Message);}
    }
    async Task Collect()
    {
        using var process=Process.GetCurrentProcess();using var timer=new PeriodicTimer(TimeSpan.FromSeconds(1));
        var clock=Stopwatch.StartNew();double lastTime=0;TimeSpan lastCpu=process.TotalProcessorTime;
        var offsets=new Dictionary<string,long>(StringComparer.OrdinalIgnoreCase);
        try
        {
            while(await timer.WaitForNextTickAsync(stop.Token))
            {
                double now=clock.Elapsed.TotalSeconds,elapsed=now-lastTime;int drawnFrames=Interlocked.Exchange(ref frames,0);TimeSpan cpu=process.TotalProcessorTime;process.Refresh();
                var counters=Volatile.Read(ref gpuLatest);
                Volatile.Write(ref latest,new OverlayMetrics(drawnFrames/Math.Max(.001,elapsed),Math.Clamp((cpu-lastCpu).TotalSeconds/Math.Max(.001,elapsed)/Environment.ProcessorCount*100,0,100),process.WorkingSet64,GC.GetTotalMemory(false),counters.Usage,counters.Dedicated,counters.Shared));
                lastTime=now;lastCpu=cpu;
                string directory=Environment.GetEnvironmentVariable("INDIE_GAME_LOG_DIRECTORY")??Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"IndieGames.BuildManager","game-logs");
                var paths=new[]{Environment.GetEnvironmentVariable("INDIE_GAME_LOG_FILE"),Path.Combine(directory,Path.GetFileNameWithoutExtension(Environment.ProcessPath??"UnknownGame")+".log")};
                foreach(string? path in paths.Where(p=>!string.IsNullOrWhiteSpace(p)).Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    try
                    {
                        using var file=new FileStream(path!,FileMode.Open,FileAccess.Read,FileShare.ReadWrite|FileShare.Delete);
                        bool first=!offsets.TryGetValue(path!,out long position);
                        position=first?Math.Max(0,file.Length-131072):Math.Min(position,file.Length);
                        if(file.Length-position>131072)position=file.Length-131072;
                        file.Position=position;byte[] bytes=new byte[(int)Math.Min(131072,file.Length-position)];int count=file.Read(bytes);offsets[path!]=file.Position;
                        string text=Encoding.UTF8.GetString(bytes,0,count);if(first){int marker=text.LastIndexOf("===== Game started =====",StringComparison.Ordinal);if(marker>=0)text=text[marker..];}
                        foreach(string line in text.Split('\n'))OverlayDiagnostics.Add(line.TrimEnd('\r'));
                    }
                    catch(Exception ex)when(ex is IOException or UnauthorizedAccessException or ArgumentException){}
                }
            }
        }
        catch(OperationCanceledException){}
        catch(Exception ex){OverlayDiagnostics.Add("Metrics sampling stopped: "+ex.GetBaseException().Message);}
    }
    public void Dispose(){if(Interlocked.Exchange(ref disposed,1)!=0)return;stop.Cancel();_ = Task.WhenAll(worker,gpuWorker).ContinueWith(_=>stop.Dispose(),TaskScheduler.Default);}
}

// PDH queries run on the sampler thread. Values are per-process OS estimates, not GPU hardware totals.
sealed class WindowsGpuCounters : IDisposable
{
    IntPtr query;
    readonly List<IntPtr> engines=new(),dedicated=new(),shared=new();
    int ticks;
    [StructLayout(LayoutKind.Explicit,Size=16)]struct Value{[FieldOffset(0)]public uint Status;[FieldOffset(8)]public double Number;}
    [StructLayout(LayoutKind.Sequential)]struct Info{public uint Length,Type,Version,Status;public int Scale,DefaultScale;public UIntPtr UserData,QueryUserData;public IntPtr FullPath;}
    [DllImport("pdh.dll",CharSet=CharSet.Unicode)]static extern uint PdhOpenQueryW(string? source,UIntPtr user,out IntPtr query);
    [DllImport("pdh.dll",CharSet=CharSet.Unicode)]static extern uint PdhAddEnglishCounterW(IntPtr query,string path,UIntPtr user,out IntPtr counter);
    [DllImport("pdh.dll",CharSet=CharSet.Unicode)]static extern uint PdhAddCounterW(IntPtr query,string path,UIntPtr user,out IntPtr counter);
    [DllImport("pdh.dll",CharSet=CharSet.Unicode)]static extern uint PdhGetCounterInfoW(IntPtr counter,bool explain,ref uint size,IntPtr info);
    [DllImport("pdh.dll",CharSet=CharSet.Unicode)]static extern uint PdhExpandWildCardPathW(string? source,string path,IntPtr paths,ref uint length,uint flags);
    [DllImport("pdh.dll")]static extern uint PdhCollectQueryData(IntPtr query);
    [DllImport("pdh.dll")]static extern uint PdhGetFormattedCounterValue(IntPtr counter,uint format,out uint type,out Value value);
    [DllImport("pdh.dll")]static extern uint PdhRemoveCounter(IntPtr counter);
    [DllImport("pdh.dll")]static extern uint PdhCloseQuery(IntPtr query);
    void Add(string path,List<IntPtr> output)
    {
        if(PdhAddEnglishCounterW(query,path,UIntPtr.Zero,out var temporary)!=0)return;
        try
        {
            uint size=0;PdhGetCounterInfoW(temporary,false,ref size,IntPtr.Zero);if(size==0||size>1048576)return;
            IntPtr buffer=Marshal.AllocHGlobal((int)size);
            string? localized;
            try{if(PdhGetCounterInfoW(temporary,false,ref size,buffer)!=0)return;localized=Marshal.PtrToStringUni(Marshal.PtrToStructure<Info>(buffer).FullPath);}
            finally{Marshal.FreeHGlobal(buffer);}
            if(localized==null)return;uint length=0;PdhExpandWildCardPathW(null,localized,IntPtr.Zero,ref length,0);if(length==0||length>524288)return;
            buffer=Marshal.AllocHGlobal((int)length*2);
            try
            {
                if(PdhExpandWildCardPathW(null,localized,buffer,ref length,0)!=0)return;
                string paths=Marshal.PtrToStringUni(buffer,(int)length)??"";
                foreach(string item in paths.Split('\0',StringSplitOptions.RemoveEmptyEntries).Take(256))if(PdhAddCounterW(query,item,UIntPtr.Zero,out var counter)==0)output.Add(counter);
            }
            finally{Marshal.FreeHGlobal(buffer);}
        }
        finally{PdhRemoveCounter(temporary);}
    }
    static double? ReadGroup(List<IntPtr> counters,bool sum)
    {
        var values=new List<double>();foreach(var c in counters)if(PdhGetFormattedCounterValue(c,0x200|0x8000,out _,out var value)==0&&value.Status<=1&&double.IsFinite(value.Number))values.Add(Math.Max(0,value.Number));
        return values.Count==0?null:sum?values.Sum():Math.Clamp(values.Max(),0,100);
    }
    public (double? Usage,double? Dedicated,double? Shared) Read()
    {
        try
        {
            if(query==IntPtr.Zero || engines.Count==0&&++ticks%5==0)
            {
                Dispose();if(PdhOpenQueryW(null,UIntPtr.Zero,out query)!=0){query=IntPtr.Zero;return(null,null,null);}
                string pid="pid_"+Environment.ProcessId+"_*";
                Add(@"\GPU Engine("+pid+@"engtype_3D)\Utilization Percentage",engines);
                Add(@"\GPU Process Memory("+pid+@")\Dedicated Usage",dedicated);Add(@"\GPU Process Memory("+pid+@")\Shared Usage",shared);
                PdhCollectQueryData(query);return(null,null,null);
            }
            if(PdhCollectQueryData(query)!=0)return(null,null,null);
            return(ReadGroup(engines,false),ReadGroup(dedicated,true),ReadGroup(shared,true));
        }
        catch(Exception ex)when(ex is DllNotFoundException or EntryPointNotFoundException or ExternalException){return(null,null,null);}
    }
    public void Dispose(){if(query!=IntPtr.Zero){PdhCloseQuery(query);query=IntPtr.Zero;}engines.Clear();dedicated.Clear();shared.Clear();}
}
