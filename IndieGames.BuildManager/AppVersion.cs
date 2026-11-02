using System.Reflection;
namespace IndieGames.BuildManager;

public static class AppVersion
{
    public static string Current { get; } = typeof(AppVersion).Assembly
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0]
        ?? typeof(AppVersion).Assembly.GetName().Version?.ToString(3) ?? "1.0.0";
    public static string WindowTitle(string title) => $"{title} Version {Current}";
}
