using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace IndieGames.BuildManager.Services;

public static class WindowAppearance
{
    [StructLayout(LayoutKind.Sequential)]
    private struct NativeRect { public int Left,Top,Right,Bottom; }
    [StructLayout(LayoutKind.Sequential)]
    private struct MonitorInfo { public int Size; public NativeRect Monitor,Work; public uint Flags; }
    [DllImport("user32.dll")] private static extern nint GetForegroundWindow();
    [DllImport("user32.dll")] private static extern nint MonitorFromWindow(nint window,uint flags);
    [DllImport("user32.dll",CharSet=CharSet.Unicode)] private static extern bool GetMonitorInfo(nint monitor,ref MonitorInfo info);
    [DllImport("shcore.dll")] private static extern int GetDpiForMonitor(nint monitor,int type,out uint x,out uint y);
    [DllImport("user32.dll")] private static extern bool SetWindowPos(nint window,nint after,int x,int y,int width,int height,uint flags);

    public static Rect CalculateBounds(Rect workArea,double minimumWidth,double minimumHeight,double scale)
    {
        double width=Math.Clamp(workArea.Width*.9,Math.Min(minimumWidth*scale,workArea.Width),workArea.Width);
        double height=Math.Clamp(workArea.Height*.9,Math.Min(minimumHeight*scale,workArea.Height),workArea.Height);
        return new(workArea.Left+(workArea.Width-width)/2,workArea.Top+(workArea.Height-height)/2,width,height);
    }

    public static void SizeToWorkArea(Window window)
    {
        nint reference=window.Owner == null ? GetForegroundWindow() : new WindowInteropHelper(window.Owner).Handle;
        PlaceOnMonitor(window,MonitorFromWindow(reference,2));
    }

    public static void FollowLaunchingWindow(Window window)
    {
        nint reference=GetForegroundWindow(), handle=new WindowInteropHelper(window).Handle;
        if(reference==0 || reference==handle)return;
        nint target=MonitorFromWindow(reference,2);
        if(target!=MonitorFromWindow(handle,2))PlaceOnMonitor(window,target);
    }

    private static void PlaceOnMonitor(Window window,nint monitor)
    {
        var info=new MonitorInfo { Size=Marshal.SizeOf<MonitorInfo>() };
        if(!GetMonitorInfo(monitor,ref info))return;
        double scale=1;
        if(GetDpiForMonitor(monitor,0,out uint dpi,out _)==0 && dpi>0)scale=dpi/96.0;
        var area=new Rect(info.Work.Left,info.Work.Top,info.Work.Right-info.Work.Left,info.Work.Bottom-info.Work.Top);
        var bounds=CalculateBounds(area,window.MinWidth,window.MinHeight,scale);
        window.MinWidth=Math.Min(window.MinWidth,area.Width/scale);
        window.MinHeight=Math.Min(window.MinHeight,area.Height/scale);
        window.WindowStartupLocation=WindowStartupLocation.Manual;
        window.WindowState=WindowState.Normal;
        window.Width=bounds.Width/scale;window.Height=bounds.Height/scale;
        window.Left=bounds.Left/scale;window.Top=bounds.Top/scale;
        void Position()
        {
            // Native coordinates keep placement correct across monitors with different DPI and negative origins.
            SetWindowPos(new WindowInteropHelper(window).Handle,0,(int)Math.Round(bounds.Left),(int)Math.Round(bounds.Top),
                (int)Math.Round(bounds.Width),(int)Math.Round(bounds.Height),0x0014);
        }
        if(new WindowInteropHelper(window).Handle!=0)Position();
        else window.SourceInitialized+=(_,_)=>Position();
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(nint window, int attribute, ref int value, int size);

    public static void Apply(Window window)
    {
        window.Icon ??= BitmapFrame.Create(new Uri("pack://application:,,,/IndieGames.BuildManager;component/DashboardIcon.ico"));
        nint handle = new WindowInteropHelper(window).Handle;
        if (handle == 0 || !OperatingSystem.IsWindows()) return;
        int enabled = 1;
        if (DwmSetWindowAttribute(handle, 20, ref enabled, sizeof(int)) != 0)
            DwmSetWindowAttribute(handle, 19, ref enabled, sizeof(int));
        // DWM colors are COLORREF values (00BBGGRR). Unsupported attributes
        // simply leave the operating system's dark caption colors in place.
        int caption = 0x00271811, text = 0x00EBE7E5, border = 0x00514137;
        DwmSetWindowAttribute(handle, 35, ref caption, sizeof(int));
        DwmSetWindowAttribute(handle, 36, ref text, sizeof(int));
        DwmSetWindowAttribute(handle, 34, ref border, sizeof(int));
    }
}
