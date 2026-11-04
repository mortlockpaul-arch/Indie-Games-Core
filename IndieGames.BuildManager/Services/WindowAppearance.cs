using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace IndieGames.BuildManager.Services;

public static class WindowAppearance
{
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
