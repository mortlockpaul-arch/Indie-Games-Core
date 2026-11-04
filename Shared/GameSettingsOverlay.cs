using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Drawing = System.Drawing;

namespace IndieGames.Overlay;

public enum OverlayPage { Settings, Info, Logs }
public enum CanvasFilterMode { GameDefault, Nearest, Linear, Integer, Scanlines }

/// <summary>Desktop settings component shared as source between FNA and MonoGame.</summary>
public sealed class GameSettingsOverlay : DrawableGameComponent
{
    readonly GraphicsDeviceManager graphics;
    readonly List<(Rectangle Bounds, Action Click)> actions = new();
    readonly string settingsFile;
    readonly OverlayMetricSampler metrics = new();
    SpriteBatch? batch;
    Texture2D? pixel, fontTexture;
    readonly Rectangle[] glyphs = new Rectangle[128];
    readonly int[] advances = new int[128];
    KeyboardState previousKeys;
    MouseState previousMouse;
    int selected, resolutionIndex;
    int infoScroll, logScroll;
    bool startupLog;
    readonly string cpuName = ReadCpuName();
    readonly string gpuNames = ReadGpuNames();
    bool wasMouseVisible;
    string message = "F1 opens settings • Escape resumes the game";
    static readonly Point[] Resolutions = { new(960,540),new(1280,720),new(1600,900),new(1920,1080),new(2560,1440) };
    public bool IsOpen { get; private set; }
    public bool ShowHint { get; set; } = true;
    public bool DebugHud { get; set; }
    public OverlayPage Page { get; set; }
    public OverlayMetrics CurrentMetrics => metrics.Current;
    public void SetCanvasFilter(CanvasFilterMode filter) { GameCanvas.Filter=Enum.IsDefined(filter)?filter:CanvasFilterMode.GameDefault;Save(); }
    public Action<bool>? PauseChanged { get; set; }
    public string GameTitle { get; set; }
    public GameSettingsOverlay(Game game, GraphicsDeviceManager manager) : base(game)
    {
        graphics=manager;DrawOrder=int.MaxValue;
        GameTitle=Assembly.GetEntryAssembly()?.GetName().Name ?? "Game";
        string key=string.Concat(GameTitle.Split(Path.GetInvalidFileNameChars()));
        settingsFile=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"IndieGames","GameSettings",key+".json");
        try
        {
            var saved=JsonSerializer.Deserialize<Saved>(File.ReadAllText(settingsFile));
            if(saved!=null && saved.Width>=640 && saved.Height>=360 && saved.Width<=7680 && saved.Height<=4320)
            {
                graphics.PreferredBackBufferWidth=saved.Width;graphics.PreferredBackBufferHeight=saved.Height;graphics.IsFullScreen=saved.Fullscreen;
                SoundEffect.MasterVolume=Math.Clamp(saved.Volume,0,1);MediaPlayer.Volume=SoundEffect.MasterVolume;
                GameCanvas.Filter=Enum.IsDefined(typeof(CanvasFilterMode),saved.Filter)?(CanvasFilterMode)saved.Filter:CanvasFilterMode.GameDefault;DebugHud=saved.Hud;
            }
        }
        catch(IOException){} catch(JsonException){} catch(UnauthorizedAccessException){}
        for(int i=0;i<Resolutions.Length;i++)if(Resolutions[i].X==graphics.PreferredBackBufferWidth && Resolutions[i].Y==graphics.PreferredBackBufferHeight)resolutionIndex=i;
    }
    sealed record Saved(int Width,int Height,bool Fullscreen,float Volume,int Filter=0,bool Hud=false);
    protected override void LoadContent()
    {
        batch=new SpriteBatch(GraphicsDevice);pixel=new Texture2D(GraphicsDevice,1,1);pixel.SetData(new[]{Color.White});
        using var bitmap=new Drawing.Bitmap(1024,256,Drawing.Imaging.PixelFormat.Format32bppArgb);
        using(var g=Drawing.Graphics.FromImage(bitmap))
        using(var font=new Drawing.Font("Segoe UI",16,Drawing.FontStyle.Regular,Drawing.GraphicsUnit.Pixel))
        {
            g.Clear(Drawing.Color.Transparent);g.TextRenderingHint=Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            for(int c=32;c<128;c++)
            {
                int x=(c-32)%32*32,y=(c-32)/32*64;
                string text=((char)c).ToString();advances[c]=c==32?6:Math.Max(2,(int)Math.Ceiling(g.MeasureString(text,font,Drawing.PointF.Empty,Drawing.StringFormat.GenericTypographic).Width));
                glyphs[c]=new Rectangle(x+1,y+1,Math.Min(30,advances[c]+3),26);
                g.DrawString(text,font,Drawing.Brushes.White,x+2,y+2,Drawing.StringFormat.GenericTypographic);
            }
        }
        var bits=bitmap.LockBits(new Drawing.Rectangle(0,0,bitmap.Width,bitmap.Height),Drawing.Imaging.ImageLockMode.ReadOnly,Drawing.Imaging.PixelFormat.Format32bppArgb);
        try
        {
            var bytes=new byte[bitmap.Width*bitmap.Height*4];Marshal.Copy(bits.Scan0,bytes,0,bytes.Length);var colors=new Color[bitmap.Width*bitmap.Height];
            for(int i=0;i<colors.Length;i++){byte alpha=bytes[i*4+3];colors[i]=new Color(alpha,alpha,alpha,alpha);}
            fontTexture=new Texture2D(GraphicsDevice,bitmap.Width,bitmap.Height);fontTexture.SetData(colors);
        }
        finally{bitmap.UnlockBits(bits);}
        base.LoadContent();
    }
    public override void Update(GameTime gameTime)
    {
        var keys=Keyboard.GetState();var mouse=Mouse.GetState();
        bool Press(Keys key)=>keys.IsKeyDown(key)&&previousKeys.IsKeyUp(key);
        if(Game.IsActive && (Press(Keys.F1)||IsOpen&&Press(Keys.Escape))) SetOpen(!IsOpen);
        if(Game.IsActive && Press(Keys.F2)){DebugHud=!DebugHud;Save();}
        if(Game.IsActive && IsOpen)
        {
            if(Press(Keys.Tab)){Page=(OverlayPage)(((int)Page+1)%3);infoScroll=logScroll=0;}
            if(Page==OverlayPage.Settings)
            {
                if(Press(Keys.Up))selected=(selected+7)%8;
                if(Press(Keys.Down))selected=(selected+1)%8;
                if(Press(Keys.Left))Change(-1);if(Press(Keys.Right))Change(1);
                if(Press(Keys.Enter))Change(1);
            }
            else
            {
                int delta=(Press(Keys.Down)?1:0)-(Press(Keys.Up)?1:0)+(Press(Keys.PageDown)?10:0)-(Press(Keys.PageUp)?10:0)-(mouse.ScrollWheelValue-previousMouse.ScrollWheelValue)/120;
                if(Page==OverlayPage.Info)infoScroll=Math.Clamp(infoScroll+delta,0,Math.Max(0,InfoLines().Length-11));
                else logScroll=Math.Clamp(logScroll+delta,0,Math.Max(0,OverlayDiagnostics.Lines(startupLog).Length-12));
            }
            if(mouse.LeftButton==ButtonState.Pressed&&previousMouse.LeftButton==ButtonState.Released)
            {
                var point=ToPanel(mouse.X,mouse.Y);
                foreach(var action in actions)if(action.Bounds.Contains(point)){action.Click();break;}
            }
        }
        previousKeys=keys;previousMouse=mouse;base.Update(gameTime);
    }
    public void SetOpen(bool open)
    {
        if(IsOpen==open)return;IsOpen=open;
        if(open){wasMouseVisible=Game.IsMouseVisible;Game.IsMouseVisible=true;}
        else Game.IsMouseVisible=wasMouseVisible;
        PauseChanged?.Invoke(open);
    }
    void Change(int direction)
    {
        switch(selected)
        {
            case 0:resolutionIndex=(resolutionIndex+direction+Resolutions.Length)%Resolutions.Length;break;
            case 1:Apply(Resolutions[resolutionIndex].X,Resolutions[resolutionIndex].Y,!graphics.IsFullScreen);break;
            case 2:SoundEffect.MasterVolume=Math.Clamp(SoundEffect.MasterVolume+direction*.1f,0,1);MediaPlayer.Volume=SoundEffect.MasterVolume;Save();break;
            case 3:if(GameCanvas.ActiveCount>0){GameCanvas.Filter=(CanvasFilterMode)(((int)GameCanvas.Filter+direction+5)%5);Save();}else message="This game needs a GameCanvas adapter before frame filters can be enabled.";break;
            case 4:DebugHud=!DebugHud;Save();break;
            case 5:Apply(Resolutions[resolutionIndex].X,Resolutions[resolutionIndex].Y,graphics.IsFullScreen);break;
            case 6:SetOpen(false);break;
            case 7:Game.Exit();break;
        }
    }
    public void ApplyDisplay(int width,int height,bool fullscreen) => Apply(width,height,fullscreen);
    void Apply(int width,int height,bool fullscreen)
    {
        if(width<640||height<360||width>7680||height>4320){message="Choose a supported resolution between 640 x 360 and 7680 x 4320.";return;}
        int oldWidth=graphics.PreferredBackBufferWidth,oldHeight=graphics.PreferredBackBufferHeight;bool oldFullscreen=graphics.IsFullScreen;
        try{graphics.PreferredBackBufferWidth=width;graphics.PreferredBackBufferHeight=height;graphics.IsFullScreen=fullscreen;graphics.ApplyChanges();Save();message="Display settings applied.";}
        catch(Exception ex)
        {
            graphics.PreferredBackBufferWidth=oldWidth;graphics.PreferredBackBufferHeight=oldHeight;graphics.IsFullScreen=oldFullscreen;
            try{graphics.ApplyChanges();}catch{}
            message="Display change failed: "+ex.GetBaseException().Message;
        }
    }
    void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(settingsFile)!);string temp=settingsFile+".tmp";
            File.WriteAllText(temp,JsonSerializer.Serialize(new Saved(graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight,graphics.IsFullScreen,SoundEffect.MasterVolume,(int)GameCanvas.Filter,DebugHud)));File.Move(temp,settingsFile,true);
        }
        catch(Exception ex)when(ex is IOException or UnauthorizedAccessException){message="Settings could not be saved: "+ex.Message;}
    }
    float Scale=>Math.Min(GraphicsDevice.Viewport.Width/900f,GraphicsDevice.Viewport.Height/640f);
    Vector2 Offset=>new((GraphicsDevice.Viewport.Width-760*Scale)/2,(GraphicsDevice.Viewport.Height-510*Scale)/2);
    Point ToPanel(int x,int y)=>new((int)((x-Offset.X)/Scale),(int)((y-Offset.Y)/Scale));
    static Color Glass(int r,int g,int b,int a)=>Color.FromNonPremultiplied(r,g,b,a);
    void Rect(Rectangle rect,Color color)=>batch!.Draw(pixel,rect,color);
    void Round(Rectangle r,Color color)
    {
        Rect(new Rectangle(r.X,r.Y+8,r.Width,r.Height-16),color);
        for(int y=0;y<8;y++){int inset=8-(int)Math.Sqrt(64-(8-y)*(8-y));Rect(new Rectangle(r.X+inset,r.Y+y,r.Width-inset*2,1),color);Rect(new Rectangle(r.X+inset,r.Bottom-1-y,r.Width-inset*2,1),color);}
    }
    void Text(string text,int x,int y,Color color,float scale=1)
    {
        foreach(char c in text){int code=c>=32&&c<128?c:'?';batch!.Draw(fontTexture,new Vector2(x,y),glyphs[code],color,0,Vector2.Zero,scale,SpriteEffects.None,0);x+=(int)(advances[code]*scale);}
    }
    void TextFit(string text,int x,int y,Color color,float scale=1,int width=700)
    {
        int size=0,count=0;foreach(char c in text){int code=c>=32&&c<128?c:'?';size+=(int)(advances[code]*scale);if(size>width-18)break;count++;}
        Text(count<text.Length?text[..count]+"...":text,x,y,color,scale);
    }
    void Button(string label,Rectangle bounds,Action action,bool active=false)
    {
        var mouse=Mouse.GetState();Round(bounds,active||bounds.Contains(ToPanel(mouse.X,mouse.Y))?Glass(56,117,183,210):Glass(65,82,107,150));TextFit(label,bounds.X+12,bounds.Y+10,Color.White,.9f,bounds.Width-20);actions.Add((bounds,action));
    }
    static string ReadCpuName()
    {
        try{using var key=Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");return (key?.GetValue("ProcessorNameString") as string)?.Trim()??RuntimeInformation.ProcessArchitecture.ToString();}catch{return RuntimeInformation.ProcessArchitecture.ToString();}
    }
    static string ReadGpuNames()
    {
        try
        {
            var names=new List<string>();using var video=Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Video");
            if(video!=null)foreach(string key in video.GetSubKeyNames()){using var adapter=video.OpenSubKey(key+@"\0000");if(adapter?.GetValue("DriverDesc") is string name&&!names.Contains(name))names.Add(name);}
            return names.Count>0?string.Join("; ",names):"not reported";
        }
        catch{return "not reported";}
    }
    public string[] InfoLines()
    {
        var m=metrics.Current;var p=GraphicsDevice.PresentationParameters;var assembly=typeof(Game).Assembly;
        string version=assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion??assembly.GetName().Version?.ToString()??"unknown";
        string renderer=OverlayDiagnostics.Renderer();if(renderer.Length==0)renderer=GraphicsDevice.Adapter.Description;
        var lines=new List<string>{m.Summary,m.Gpu,$"Managed heap {m.ManagedBytes/1048576d:0.0} MB | Shared GPU memory (OS estimate) {(m.SharedGpuBytes.HasValue?$"{m.SharedGpuBytes/1048576d:0.0} MB":"unavailable")}",
            $"Framework: {assembly.GetName().Name} {version}",$"Runtime: {RuntimeInformation.FrameworkDescription} / {RuntimeInformation.ProcessArchitecture}","CPU: "+cpuName,"Installed GPU(s): "+gpuNames,"Renderer / adapter: "+renderer,
            $"Active display: {p.BackBufferWidth} x {p.BackBufferHeight}, {(p.IsFullScreen?"fullscreen":"windowed")}",$"VSync: {p.PresentationInterval}; update target {1/Math.Max(.001,Game.TargetElapsedTime.TotalSeconds):0.0} Hz",
            $"Graphics profile: {GraphicsDevice.GraphicsProfile}; format {p.BackBufferFormat}; MSAA {p.MultiSampleCount}",
            "Canvas: "+GameCanvas.Status,$"Audio: SFX {SoundEffect.MasterVolume*100:0}% / music {MediaPlayer.Volume*100:0}%", "Base directory: "+AppContext.BaseDirectory,"Working directory: "+Directory.GetCurrentDirectory()};
        foreach(string key in new[]{"FNA3D_FORCE_DRIVER","FNA3D_FORCE_SDL3GPU","SDL_GPU_DRIVER","SDL_VIDEODRIVER","SDL_AUDIODRIVER","FNA_NUKE_STEAM_INPUT","FNA_GAMEPAD_NUM_GAMEPADS","SDL_GAMECONTROLLERCONFIG_FILE","FNA_KEYBOARD_USE_SCANCODES"})lines.Add(key+" = "+(Environment.GetEnvironmentVariable(key)??"auto / unset"));
        foreach(var loaded in AppDomain.CurrentDomain.GetAssemblies().Where(a=>(a.GetName().Name??"").StartsWith("FNA",StringComparison.OrdinalIgnoreCase)))lines.Add("Loaded: "+loaded.GetName().Name+" "+loaded.GetName().Version);
        return lines.ToArray();
    }
    public string SaveDiagnosticReport()
    {
        string folder=Path.Combine(Path.GetDirectoryName(settingsFile)!,"diagnostics");Directory.CreateDirectory(folder);string path=Path.Combine(folder,Path.GetFileNameWithoutExtension(settingsFile)+"-"+DateTime.Now.ToString("yyyyMMdd-HHmmss-fff")+".txt");
        File.WriteAllLines(path,new[]{GameTitle,DateTimeOffset.Now.ToString("O"),"GPU metrics are per-process OS estimates; unavailable counters are not reported as zero."}.Concat(InfoLines()).Concat(new[]{"","STARTUP OUTPUT"}).Concat(OverlayDiagnostics.Lines(true)).Concat(new[]{"","RECENT OUTPUT"}).Concat(OverlayDiagnostics.Lines()));
        message="Saved diagnostic report: "+path;return path;
    }
    void ExportReport(){try{SaveDiagnosticReport();}catch(Exception ex)when(ex is IOException or UnauthorizedAccessException){message="Report could not be saved: "+ex.Message;}}
    public override void Draw(GameTime gameTime)
    {
        metrics.Frame();
        if((!IsOpen&&!ShowHint&&!DebugHud)||batch==null||pixel==null||fontTexture==null)return;
        var blend=GraphicsDevice.BlendState;var depth=GraphicsDevice.DepthStencilState;var raster=GraphicsDevice.RasterizerState;var sampler=GraphicsDevice.SamplerStates[0];
        try
        {
            if(!IsOpen)
            {
                batch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullNone);
                int x=GraphicsDevice.Viewport.Width-156,y=GraphicsDevice.Viewport.Height-38;
                if(ShowHint){Round(new Rectangle(x,y,140,28),Glass(16,25,40,150));Text("F1  Settings",x+12,y+6,Color.LightGray,.9f);}
                if(DebugHud){Round(new Rectangle(12,12,390,52),Glass(12,20,32,190));TextFit(metrics.Current.Summary,22,21,Color.White,.78f,370);TextFit(metrics.Current.Gpu,22,42,Color.LightSkyBlue,.73f,370);}
                batch.End();return;
            }
            actions.Clear();batch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullNone,null,Matrix.CreateScale(Scale)*Matrix.CreateTranslation(Offset.X,Offset.Y,0));
            Round(new Rectangle(7,10,760,510),Glass(0,0,0,95));Round(new Rectangle(0,0,760,510),Glass(15,24,40,225));Rect(new Rectangle(15,0,730,1),Glass(190,225,255,120));
            TextFit(GameTitle,28,20,Color.White,1.25f);Text("GAME SETTINGS",28,57,Color.LightSkyBlue);
            Button("Settings",new Rectangle(370,48,112,35),()=>Page=OverlayPage.Settings,Page==OverlayPage.Settings);Button("Info",new Rectangle(490,48,104,35),()=>Page=OverlayPage.Info,Page==OverlayPage.Info);Button("Logs",new Rectangle(602,48,132,35),()=>Page=OverlayPage.Logs,Page==OverlayPage.Logs);
            if(Page==OverlayPage.Settings)
            {
            string[] labels={"Resolution", "Display", "Volume", "Canvas scaler / filter", "Small debug HUD"};
            string[] filterNames={"Game default","Nearest / crisp","Linear / smooth","Integer scale","Scanlines"};
            string[] values={$"{Resolutions[resolutionIndex].X} x {Resolutions[resolutionIndex].Y}",graphics.IsFullScreen?"Fullscreen":"Windowed",$"{Math.Round(SoundEffect.MasterVolume*100)}%",GameCanvas.ActiveCount>0?filterNames[(int)GameCanvas.Filter]:"Needs canvas adapter",DebugHud?"On (F2)":"Off (F2)"};
            Point mouse=ToPanel(Mouse.GetState().X,Mouse.GetState().Y);
            for(int i=0;i<labels.Length;i++)
            {
                int index=i;var row=new Rectangle(26,96+i*45,708,38);bool hover=row.Contains(mouse);
                Round(row,i==selected||hover?Glass(56,117,183,200):Glass(65,82,107,125));Text(labels[i],42,row.Y+11,Color.White);Text(values[i],440,row.Y+11,Color.White);
                if(i is 0 or 2)
                {
                    var minus=new Rectangle(636,row.Y+5,38,32);var plus=new Rectangle(682,row.Y+5,38,32);
                    Round(minus,Glass(100,130,165,125));Round(plus,Glass(100,130,165,125));Text("-",650,row.Y+11,Color.White);Text("+",695,row.Y+11,Color.White);
                    actions.Add((minus,()=>{selected=index;Change(-1);}));actions.Add((plus,()=>{selected=index;Change(1);}));
                }
                actions.Add((row,()=>{selected=index;Change(1);}));
            }
            Button("Apply resolution",new Rectangle(26,335,224,38),()=>{selected=5;Change(1);},selected==5);Button("Resume game",new Rectangle(268,335,224,38),()=>SetOpen(false),selected==6);Button("Exit game",new Rectangle(510,335,224,38),()=>Game.Exit(),selected==7);
            string controller="No controller detected";for(int i=0;i<4;i++)if(GamePad.GetState((PlayerIndex)i).IsConnected){controller="Controller connected: slot "+(i+1);break;}
            Text(controller,28,392,Color.LightSkyBlue);TextFit(metrics.Current.Summary,28,421,Color.LightGray,.9f);Text("Tab: switch pages | F2: HUD | F1 / Escape: resume",28,450,Color.LightGray,.85f);
            }
            else if(Page==OverlayPage.Info)
            {
                var lines=InfoLines();infoScroll=Math.Clamp(infoScroll,0,Math.Max(0,lines.Length-11));for(int i=0;i<11&&infoScroll+i<lines.Length;i++)TextFit(lines[infoScroll+i],28,106+i*26,i<3?Color.LightSkyBlue:Color.White,.85f);
                Button("Save diagnostic report",new Rectangle(26,412,262,38),ExportReport);Text("Up / Down / wheel: more settings",310,425,Color.LightGray,.8f);
            }
            else
            {
                Button(startupLog?"Startup output":"Recent output",new Rectangle(26,98,220,35),()=>{startupLog=!startupLog;logScroll=0;});Button("Save diagnostic report",new Rectangle(474,98,260,35),ExportReport);
                var lines=OverlayDiagnostics.Lines(startupLog);int end=startupLog?Math.Min(lines.Length,logScroll+12):Math.Max(0,lines.Length-logScroll);int start=startupLog?logScroll:Math.Max(0,end-12);
                for(int i=start;i<end;i++)TextFit(lines[i],28,148+(i-start)*24,Color.LightGray,.8f);
                if(lines.Length==0)Text("No captured output yet. Managed console output is captured automatically.",28,156,Color.LightGray,.8f);
                Text("Wheel / Page Up / Page Down to scroll. Native output needs launch capture.",28,451,Color.LightSkyBlue,.8f);
            }
            TextFit(message,28,478,Color.LightGray,.8f);batch.End();
        }
        finally{GraphicsDevice.BlendState=blend;GraphicsDevice.DepthStencilState=depth;GraphicsDevice.RasterizerState=raster;GraphicsDevice.SamplerStates[0]=sampler;}
        base.Draw(gameTime);
    }
    protected override void Dispose(bool disposing){if(disposing){metrics.Dispose();batch?.Dispose();pixel?.Dispose();fontTexture?.Dispose();}base.Dispose(disposing);}
}

/// <summary>Optional fixed-resolution canvas for games with hard-coded layout coordinates.</summary>
public sealed class GameCanvas : IDisposable
{
    static int active;
    bool disposed;
    public static int ActiveCount=>System.Threading.Volatile.Read(ref active);
    public static CanvasFilterMode Filter { get; set; }
    public static string Status { get; private set; } = "not integrated";
    readonly GraphicsDevice device;
    readonly RenderTarget2D target;
    readonly SpriteBatch batch;
    readonly Texture2D pixel;
    RenderTargetBinding[] previousTargets = Array.Empty<RenderTargetBinding>();
    Viewport previousViewport;
    public GameCanvas(GraphicsDevice graphics,int width,int height)
    {
        device=graphics;target=new RenderTarget2D(device,width,height,false,SurfaceFormat.Color,DepthFormat.None);batch=new SpriteBatch(device);pixel=new Texture2D(device,1,1);pixel.SetData(new[]{Color.White});System.Threading.Interlocked.Increment(ref active);
    }
    public void Begin()
    {
        previousTargets=device.GetRenderTargets();previousViewport=device.Viewport;device.SetRenderTarget(target);
    }
    public void Present(bool smooth=true)
    {
        device.SetRenderTargets(previousTargets);device.Viewport=previousViewport;device.Clear(Color.Black);
        float scale=Math.Min(previousViewport.Width/(float)target.Width,previousViewport.Height/(float)target.Height);
        if(Filter==CanvasFilterMode.Integer&&scale>=1)scale=(float)Math.Floor(scale);
        int width=(int)Math.Round(target.Width*scale),height=(int)Math.Round(target.Height*scale);
        bool linear=Filter==CanvasFilterMode.Linear || Filter==CanvasFilterMode.GameDefault&&smooth;
        var destination=new Rectangle((previousViewport.Width-width)/2,(previousViewport.Height-height)/2,width,height);
        batch.Begin(SpriteSortMode.Deferred,BlendState.Opaque,linear?SamplerState.LinearClamp:SamplerState.PointClamp,DepthStencilState.None,RasterizerState.CullNone);
        batch.Draw(target,destination,Color.White);batch.End();
        if(Filter==CanvasFilterMode.Scanlines)
        {
            batch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointClamp,DepthStencilState.None,RasterizerState.CullNone);
            int step=Math.Max(2,(int)Math.Round(scale*2)),line=Math.Max(1,step/2);
            for(int y=destination.Y;y<destination.Bottom;y+=step)batch.Draw(pixel,new Rectangle(destination.X,y,destination.Width,Math.Min(line,destination.Bottom-y)),new Color(0,0,0,55));batch.End();
        }
        Status=$"{target.Width}x{target.Height} -> {width}x{height}; {Filter} ({(linear?"linear":"point")})";
    }
    public void PresentLastFrame(bool smooth=true)
    {
        previousTargets=device.GetRenderTargets();previousViewport=device.Viewport;Present(smooth);
    }
    public void Dispose(){if(disposed)return;disposed=true;System.Threading.Interlocked.Decrement(ref active);target.Dispose();batch.Dispose();pixel.Dispose();}
}
