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

public enum OverlayPage { Settings, Info, Logs, Games }
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
    readonly GamePadState[] previousPads = new GamePadState[4];
    bool controllerConnected;
    int selected, resolutionIndex;
    int infoScroll, logScroll, gameSelection;
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
    public IReadOnlyList<(string Title, Action Launch)> Games { get; init; } = Array.Empty<(string, Action)>();
    public int CurrentGame { get; init; } = -1;
    public string GameTitle { get; set; }
    public GameSettingsOverlay(Game game, GraphicsDeviceManager manager) : base(game)
    {
        graphics=manager;DrawOrder=int.MaxValue;
        GameTitle=Assembly.GetEntryAssembly()?.GetName().Name ?? "Game";
        string key=string.Concat(GameTitle.Split(Path.GetInvalidFileNameChars()));
        settingsFile=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"IndieGames","GameSettings",key+".json");
        try
        {
            var saved=File.Exists(settingsFile) ? JsonSerializer.Deserialize<Saved>(File.ReadAllText(settingsFile)) : null;
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
        using(var font=new Drawing.Font("Segoe UI",18,Drawing.FontStyle.Regular,Drawing.GraphicsUnit.Pixel))
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
        var pads=Enumerable.Range(0,4).Select(i=>GamePad.GetState((PlayerIndex)i)).ToArray();
        controllerConnected=pads.Any(p=>p.IsConnected);
        bool Pad(Buttons button)=>Enumerable.Range(0,4).Any(i=>pads[i].IsConnected && pads[i].IsButtonDown(button) && !previousPads[i].IsButtonDown(button));
        bool Stick(int axis,int direction)=>Enumerable.Range(0,4).Any(i=>pads[i].IsConnected && (axis==0?pads[i].ThumbSticks.Left.X:pads[i].ThumbSticks.Left.Y)*direction>.55f && (axis==0?previousPads[i].ThumbSticks.Left.X:previousPads[i].ThumbSticks.Left.Y)*direction<=.55f);
        bool Press(Keys key)=>keys.IsKeyDown(key)&&previousKeys.IsKeyUp(key) || key switch {
            Keys.Up=>Pad(Buttons.DPadUp)||Stick(1,1), Keys.Down=>Pad(Buttons.DPadDown)||Stick(1,-1),
            Keys.Left=>Pad(Buttons.DPadLeft)||Stick(0,-1), Keys.Right=>Pad(Buttons.DPadRight)||Stick(0,1),
            Keys.Enter=>Pad(Buttons.A), Keys.Escape=>Pad(Buttons.B), Keys.F1=>Pad(Buttons.Start), Keys.F2=>IsOpen&&Pad(Buttons.X), _=>false };
        if(Game.IsActive && (Press(Keys.F1)||IsOpen&&Press(Keys.Escape))) SetOpen(!IsOpen);
        if(Game.IsActive && Press(Keys.F2)){DebugHud=!DebugHud;Save();}
        if(Game.IsActive && IsOpen)
        {
            int pageCount=Games.Count>0?4:3;
            if(Press(Keys.Tab)||Pad(Buttons.RightShoulder)){Page=(OverlayPage)(((int)Page+1)%pageCount);infoScroll=logScroll=0;}
            if(Pad(Buttons.LeftShoulder)){Page=(OverlayPage)(((int)Page+pageCount-1)%pageCount);infoScroll=logScroll=0;}
            if(Pad(Buttons.Y)) {
                if(Page==OverlayPage.Settings)Apply(Resolutions[resolutionIndex].X,Resolutions[resolutionIndex].Y,graphics.IsFullScreen);
                else if(Page==OverlayPage.Info)ExportReport();
                else if(Page==OverlayPage.Logs){startupLog=!startupLog;logScroll=0;}
            }
            if(Page==OverlayPage.Settings)
            {
                if(Press(Keys.Up))selected=(selected+7)%8;
                if(Press(Keys.Down))selected=(selected+1)%8;
                if(Press(Keys.Left))Change(-1);if(Press(Keys.Right))Change(1);
                if(Press(Keys.Enter))Change(1);
            }
            else if(Page==OverlayPage.Games && Games.Count>0)
            {
                if(Press(Keys.Up))gameSelection=(gameSelection+Games.Count-1)%Games.Count;
                if(Press(Keys.Down))gameSelection=(gameSelection+1)%Games.Count;
                if(Press(Keys.Enter))Games[gameSelection].Launch();
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
        previousKeys=keys;previousMouse=mouse;Array.Copy(pads,previousPads,4);base.Update(gameTime);
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
    static readonly Color Accent = new(157, 242, 105);
    void Frost(Rectangle bounds, bool focused=false)
    {
        Round(new Rectangle(bounds.X,bounds.Y+3,bounds.Width,bounds.Height),Glass(0,0,0,50));
        Round(bounds,focused?Glass(82,127,74,210):Glass(92,110,116,80));
        Round(new Rectangle(bounds.X+1,bounds.Y+1,bounds.Width-2,bounds.Height-2),focused?Glass(28,58,35,205):Glass(23,34,39,156));
        Rect(new Rectangle(bounds.X+10,bounds.Y+1,bounds.Width-20,1),focused?Glass(191,255,163,155):Glass(220,242,247,60));
        if(focused)Rect(new Rectangle(bounds.X+1,bounds.Y+9,3,bounds.Height-18),Accent);
    }
    void FrostedBackdrop()
    {
        batch!.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullNone);
        if(GameCanvas.LastFrame is { IsDisposed:false } frame)
        {
            var destination=GameCanvas.LastDestination;
            foreach(var offset in new[]{new Point(-5,-5),new Point(0,-5),new Point(5,-5),new Point(-5,0),new Point(5,0),new Point(-5,5),new Point(0,5),new Point(5,5)})
                batch.Draw(frame,new Rectangle(destination.X+offset.X,destination.Y+offset.Y,destination.Width,destination.Height),Color.White*.16f);
        }
        Rect(GraphicsDevice.Viewport.Bounds,Glass(5,12,14,115));
        batch.End();
    }
    void Text(string text,int x,int y,Color color,float scale=1)
    {
        DrawText(text,x+2,y+2,Color.Black * .8f,scale);
        for (int dy=-1;dy<=1;dy++)
            for (int dx=-1;dx<=1;dx++)
                if (dx!=0 || dy!=0) DrawText(text,x+dx,y+dy,Color.Black,scale);
        DrawText(text,x,y,color,scale);
    }
    void DrawText(string text,int x,int y,Color color,float scale)
    {
        foreach(char c in text){int code=c>=32&&c<128?c:'?';batch!.Draw(fontTexture,new Vector2(x,y),glyphs[code],color,0,Vector2.Zero,scale,SpriteEffects.None,0);x+=(int)(advances[code]*scale);}
    }
    void DrawHint()
    {
        string label = controllerConnected ? "MENU / F1  Settings" : "F1  Settings";
        const float scale = .9f;
        int textWidth = label.Sum(c => (int)(advances[c] * scale));
        int width = textWidth + 24;
        int x = Math.Max(0,GraphicsDevice.Viewport.Width-width-16);
        int y = Math.Max(0,GraphicsDevice.Viewport.Height-38);
        Round(new Rectangle(x,y,width,28),Glass(15,29,24,200));
        Rect(new Rectangle(x+1,y+7,2,14),Accent);
        Text(label,x+(width-textWidth)/2,y+4,Color.LightGray,scale);
    }
    void ControllerIcon(string label,int x,int y,Color tint)
    {
        if(label.Length==1) {
            for(int row=-11;row<=11;row++) {
                int half=(int)Math.Sqrt(121-row*row);
                Rect(new Rectangle(x+12-half,y+12+row,half*2+1,1),Glass(tint.R,tint.G,tint.B,190));
                if(half>1)Rect(new Rectangle(x+13-half,y+12+row,half*2-1,1),Glass(12,20,19,245));
            }
            Text(label,x+7,y+3,tint,.8f);
        } else {
            Round(new Rectangle(x,y+1,32,23),Glass(190,217,204,130));
            Round(new Rectangle(x+1,y+2,30,21),Glass(22,32,29,240));
            Text(label,x+5,y+3,Color.White,.65f);
        }
    }
    void ControllerHints()
    {
        int y=447;
        ControllerIcon("A",28,y,new Color(126,228,114));Text("Select",57,y+3,Color.White,.75f);
        ControllerIcon("B",119,y,new Color(255,125,123));Text("Resume",148,y+3,Color.White,.75f);
        ControllerIcon("X",221,y,new Color(116,191,255));Text("HUD",250,y+3,Color.White,.75f);
        ControllerIcon("Y",305,y,new Color(255,220,108));Text(Page==OverlayPage.Info?"Report":Page==OverlayPage.Logs?"Log view":"Apply",334,y+3,Color.White,.75f);
        ControllerIcon("LB",415,y,Color.White);ControllerIcon("RB",452,y,Color.White);Text("Tabs",491,y+3,Color.White,.75f);
        Text("D-pad / stick: move",553,y+3,Color.LightGray,.7f);
    }
    void TextFit(string text,int x,int y,Color color,float scale=1,int width=700)
    {
        int size=0,count=0;foreach(char c in text){int code=c>=32&&c<128?c:'?';size+=(int)(advances[code]*scale);if(size>width-18)break;count++;}
        Text(count<text.Length?text[..count]+"...":text,x,y,color,scale);
    }
    void Button(string label,Rectangle bounds,Action action,bool active=false)
    {
        var mouse=Mouse.GetState();Frost(bounds,active||bounds.Contains(ToPanel(mouse.X,mouse.Y)));TextFit(label,bounds.X+12,bounds.Y+8,Color.White,bounds.Width<=110?.8f:.9f,bounds.Width-12);actions.Add((bounds,action));
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
                if(ShowHint)DrawHint();
                if(DebugHud){Round(new Rectangle(12,12,390,52),Glass(12,20,32,190));TextFit(metrics.Current.Summary,22,21,Color.White,.78f,370);TextFit(metrics.Current.Gpu,22,42,Accent,.73f,370);}
                batch.End();return;
            }
            FrostedBackdrop();
            actions.Clear();batch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullNone,null,Matrix.CreateScale(Scale)*Matrix.CreateTranslation(Offset.X,Offset.Y,0));
            Round(new Rectangle(-8,10,776,514),Glass(0,0,0,60));
            Round(new Rectangle(0,0,760,510),Glass(163,192,190,80));
            Round(new Rectangle(1,1,758,508),Glass(18,30,31,219));
            for(int y=10;y<84;y++)Rect(new Rectangle(10,y,740,1),Glass(172,213,206,(int)(18*(1-(y-10)/74f))));
            Rect(new Rectangle(20,0,720,1),Glass(225,255,238,135));
            Rect(new Rectangle(28,86,704,1),Glass(210,245,219,45));
            Rect(new Rectangle(28,86,88,2),Accent);
            for(int y=112;y<500;y+=13)for(int x=14;x<748;x+=19)Rect(new Rectangle(x+(y%7),y,1,1),Glass(225,250,245,9));
            TextFit(GameTitle,28,20,Color.White,1.25f,580);Button("Close",new Rectangle(638,14,96,30),()=>SetOpen(false));Text("GAME SETTINGS",28,57,Accent);
            int tabX = Games.Count>0 ? 326 : 430;
            Button("Settings",new Rectangle(tabX,48,96,35),()=>Page=OverlayPage.Settings,Page==OverlayPage.Settings);
            Button("Info",new Rectangle(tabX+104,48,96,35),()=>Page=OverlayPage.Info,Page==OverlayPage.Info);
            Button("Logs",new Rectangle(tabX+208,48,96,35),()=>Page=OverlayPage.Logs,Page==OverlayPage.Logs);
            if(Games.Count>0)Button("Games",new Rectangle(tabX+312,48,96,35),()=>Page=OverlayPage.Games,Page==OverlayPage.Games);
            if(Page==OverlayPage.Settings)
            {
            string[] labels={"Resolution", "Display", "Volume", "Canvas scaler / filter", "Small debug HUD"};
            string[] filterNames={"Game default","Nearest / crisp","Linear / smooth","Integer scale","Scanlines"};
            string[] values={$"{Resolutions[resolutionIndex].X} x {Resolutions[resolutionIndex].Y}",graphics.IsFullScreen?"Fullscreen":"Windowed",$"{Math.Round(SoundEffect.MasterVolume*100)}%",GameCanvas.ActiveCount>0?filterNames[(int)GameCanvas.Filter]:"Needs canvas adapter",DebugHud?"On (F2)":"Off (F2)"};
            Point mouse=ToPanel(Mouse.GetState().X,Mouse.GetState().Y);
            for(int i=0;i<labels.Length;i++)
            {
                int index=i;var row=new Rectangle(26,96+i*45,708,38);bool hover=row.Contains(mouse);
                Frost(row,i==selected||hover);Text(labels[i],42,row.Y+11,Color.White);Text(values[i],440,row.Y+11,Color.White);
                if(i is 0 or 2)
                {
                    var minus=new Rectangle(636,row.Y+5,38,32);var plus=new Rectangle(682,row.Y+5,38,32);
                    Frost(minus);Frost(plus);Text("-",650,row.Y+11,Color.White);Text("+",695,row.Y+11,Color.White);
                    actions.Add((minus,()=>{selected=index;Change(-1);}));actions.Add((plus,()=>{selected=index;Change(1);}));
                }
                actions.Add((row,()=>{selected=index;Change(1);}));
            }
            Button("Apply resolution",new Rectangle(26,335,224,38),()=>{selected=5;Change(1);},selected==5);Button("Resume game",new Rectangle(268,335,224,38),()=>SetOpen(false),selected==6);Button("Quit game",new Rectangle(510,335,224,38),()=>Game.Exit(),selected==7);
            string controller="No controller detected";for(int i=0;i<4;i++)if(GamePad.GetState((PlayerIndex)i).IsConnected){controller="Controller connected: slot "+(i+1);break;}
            Text(controller,28,392,Accent);TextFit(metrics.Current.Summary,28,421,Color.LightGray,.9f);
            }
            else if(Page==OverlayPage.Info)
            {
                var lines=InfoLines();infoScroll=Math.Clamp(infoScroll,0,Math.Max(0,lines.Length-11));for(int i=0;i<11&&infoScroll+i<lines.Length;i++)TextFit(lines[infoScroll+i],28,106+i*26,i<3?Accent:Color.White,.85f);
                Button("Save diagnostic report",new Rectangle(26,412,262,38),ExportReport);Text("Up / Down / wheel: more settings",310,425,Color.LightGray,.8f);
            }
            else if(Page==OverlayPage.Games)
            {
                Text("SELECT A GAME",28,106,Accent);
                int first = Math.Max(0,gameSelection-5);
                for(int i=first;i<Games.Count && i<first+6;i++)
                {
                    int index=i;
                    string label=Games[index].Title+(index==CurrentGame?" (current)":"");
                    Button(label,new Rectangle(26,138+(i-first)*46,708,38),
                        ()=>{gameSelection=index;Games[index].Launch();},index==gameSelection);
                }

            }
            else
            {
                Button(startupLog?"Startup output":"Recent output",new Rectangle(26,98,220,35),()=>{startupLog=!startupLog;logScroll=0;});Button("Save diagnostic report",new Rectangle(474,98,260,35),ExportReport);
                var lines=OverlayDiagnostics.Lines(startupLog);int end=startupLog?Math.Min(lines.Length,logScroll+12):Math.Max(0,lines.Length-logScroll);int start=startupLog?logScroll:Math.Max(0,end-12);
                for(int i=start;i<end;i++)TextFit(lines[i],28,148+(i-start)*24,Color.LightGray,.8f);
                if(lines.Length==0)Text("No captured output yet. Managed console output is captured automatically.",28,156,Color.LightGray,.8f);

            }
            if(controllerConnected)ControllerHints();
            else Text("Tab: pages | Enter: select | F2: HUD | F1 / Escape: resume",28,450,Color.LightGray,.8f);
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
    internal static Texture2D? LastFrame { get; private set; }
    internal static Rectangle LastDestination { get; private set; }
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
        LastFrame=target;LastDestination=destination;
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
    public void Dispose(){if(disposed)return;disposed=true;if(ReferenceEquals(LastFrame,target))LastFrame=null;System.Threading.Interlocked.Decrement(ref active);target.Dispose();batch.Dispose();pixel.Dispose();}
}
