using System.IO;

namespace IndieGames.BuildManager.Services;

public static class SolutionProjects
{
    public static async Task UpdateAsync(string solution, IEnumerable<string> projects, bool add,
        Action<string> log, CancellationToken token)
    {
        solution = Path.GetFullPath(solution);
        if (!File.Exists(solution) || Path.GetExtension(solution).ToLowerInvariant() is not (".sln" or ".slnx"))
            throw new IOException("Choose an existing .sln or .slnx solution file.");
        var paths = projects.Select(Path.GetFullPath).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
        if (paths.Length == 0) throw new ArgumentException("Select one or more project rows.", nameof(projects));
        foreach (string path in paths)
            if (!File.Exists(path) || !Path.GetExtension(path).Equals(".csproj", StringComparison.OrdinalIgnoreCase))
                throw new IOException($"Project not found: {path}");
        token.ThrowIfCancellationRequested();
        byte[] previous = await File.ReadAllBytesAsync(solution, token);
        string backup = solution + ".before-project-changes.bak";
        if (!File.Exists(backup)) File.Copy(solution, backup);
        try
        {
            var arguments = new List<string> { "sln", solution, add ? "add" : "remove" };
            arguments.AddRange(paths);
            int exit = await ProcessRunner.RunAsync("dotnet", arguments, Path.GetDirectoryName(solution)!, log, token);
            if (exit != 0) throw new IOException($"Solution update failed (exit {exit}). Original solution restored.");
        }
        catch
        {
            await File.WriteAllBytesAsync(solution, previous, CancellationToken.None);
            throw;
        }
        log($"{(add ? "Added" : "Removed")} {paths.Length} selected project(s) {(add ? "to" : "from")} {solution}. Project files are unchanged.");
    }
}
