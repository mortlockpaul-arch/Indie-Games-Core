using System.Runtime.InteropServices;
// Compatibility source uses the SDL screen-saver API name. Use a process-scoped
// Windows display-power request, equally valid for DesktopGL and WindowsDX.
namespace SDL2
{
    internal static class SDL
    {
        internal enum SDL_bool { SDL_FALSE, SDL_TRUE }
        private static bool enabled = true;
        [DllImport("kernel32.dll")] private static extern uint SetThreadExecutionState(uint flags);
        internal static SDL_bool SDL_IsScreenSaverEnabled() => enabled ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE;
        internal static void SDL_EnableScreenSaver() { SetThreadExecutionState(0x80000000); enabled = true; }
        internal static void SDL_DisableScreenSaver() { SetThreadExecutionState(0x80000002); enabled = false; }
    }
}
namespace Microsoft.Xna.Framework.Input
{
    // XNA Guide keyboard dialogs are not implemented by the offline shim.
    internal static class TextInputEXT
    {
        internal static void StartTextInput() => throw new System.NotSupportedException("The offline NetStub backend does not provide Guide keyboard dialogs. Use the game's own text entry UI or Steamworks.");
        internal static void StopTextInput() { }
    }
}
namespace Microsoft.Xna.Framework
{
    internal static class FNALoggerEXT
    {
        internal static void LogInfo(string message) => System.Console.WriteLine(message);
        internal static void LogError(string message) => System.Console.Error.WriteLine(message);
    }
}
