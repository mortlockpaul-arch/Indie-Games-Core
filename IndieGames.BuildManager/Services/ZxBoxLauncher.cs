using System.Diagnostics;
using System.IO;

namespace IndieGames.BuildManager.Services;

public sealed record ZxBoxLaunchPlan(string Backend, string Project, string Output, string Executable, string GamesFolder,
    IReadOnlyList<string> BuildArguments);

public static class ZxBoxLauncher
{
    public static ZxBoxLaunchPlan Plan(LibraryStorage storage, string backend)
    {
        string projectName = backend switch {
            "FNA" => "ZXBox.FNA",
            "MonoGame" => "ZXBox.Monogame",
            _ => throw new ArgumentException("Choose FNA or MonoGame for ZXBox.", nameof(backend))
        };
        if (!Path.IsPathFullyQualified(storage.ZxBoxRoot)) throw new IOException("Set an absolute ZXBox checkout folder in Settings.");
        string root = Path.GetFullPath(storage.ZxBoxRoot);
        string project = Path.Combine(root, "Platforms", projectName, projectName + ".csproj");
        if (!File.Exists(project)) throw new FileNotFoundException("ZXBox checkout not found. Select the ZXSpectrum source folder in Settings.", project);
        if (!Path.IsPathFullyQualified(storage.BuildCache)) throw new IOException("Set an absolute build cache folder in Settings.");
        string output = Path.Combine(storage.BuildCache, "zxbox", backend);
        var args = new List<string> { "build", project, "-c", "Release", "-p:Platform=x64", "-p:PlatformTarget=x64", "-o", output, "--nologo" };
        if (backend == "FNA")
        {
            string core = Path.GetDirectoryName(Path.GetFullPath(storage.Solution))!;
            string assembly = Path.Combine(core, "FNA.Contents", "bin", "x64", "Debug", "net9.0-windows", "FNA.dll");
            if (File.Exists(assembly)) args.Add("-p:FnaAssembly=" + assembly.Replace(";", "%3B").Replace(",", "%2C"));
        }
        return new(backend, project, output, Path.Combine(output, projectName + ".exe"), Path.Combine(root, "Games"), args);
    }

    public static Process Start(ZxBoxLaunchPlan plan)
    {
        if (!File.Exists(plan.Executable)) throw new FileNotFoundException("Build ZXBox before running it.", plan.Executable);
        Directory.CreateDirectory(plan.GamesFolder);
        var info = new ProcessStartInfo(plan.Executable) { UseShellExecute = false, WorkingDirectory = plan.Output };
        info.ArgumentList.Add("--library"); info.ArgumentList.Add(plan.GamesFolder);
        return Process.Start(info) ?? throw new IOException("Could not start ZXBox.");
    }
}
