using IndieGames.BuildManager.Services;

string root = Path.Combine(Path.GetTempPath(), "SolutionButtons-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
try
{
    string[] projects = new[] { "Game One", "Player's Game" }.Select(name => {
        string folder = Path.Combine(root, name); Directory.CreateDirectory(folder);
        string project = Path.Combine(folder, name + ".csproj");
        File.WriteAllText(project, "<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net10.0</TargetFramework></PropertyGroup></Project>");
        return project;
    }).ToArray();
    var originals = projects.Select(File.ReadAllText).ToArray();
    foreach (string extension in new[] { ".sln", ".slnx" })
    {
        string path = Path.Combine(root, "Example" + extension);
        string initial = extension == ".slnx" ? "<Solution />" :
            "Microsoft Visual Studio Solution File, Format Version 12.00\r\n# Visual Studio Version 17\r\nGlobal\r\nEndGlobal\r\n";
        File.WriteAllText(path, initial);
        await SolutionProjects.UpdateAsync(path, projects, true, _ => { }, default);
        string added = File.ReadAllText(path);
        var membership=SolutionMembership.Read(path);
        if(!projects.All(p=>membership.Contains(Path.GetFullPath(p))))throw new Exception("Solution membership did not resolve project paths");
        if (!projects.All(p => added.Contains(Path.GetFileName(p)))) throw new Exception("Projects were not added to " + extension);
        await SolutionProjects.UpdateAsync(path, projects, false, _ => { }, default);
        if (File.ReadAllText(path).Contains(".csproj")) throw new Exception("Projects were not removed from " + extension);
        if(SolutionMembership.Read(path).Count!=0)throw new Exception("Removed projects remain in membership");
        if (File.ReadAllText(path + ".before-project-changes.bak") != initial) throw new Exception("Original backup changed");
        if (!projects.Select(File.ReadAllText).SequenceEqual(originals)) throw new Exception("Source files changed");
        Console.WriteLine("PASS " + extension + " batch add/remove, paths with spaces/apostrophes, backups and source preservation");
    }
    string broken = Path.Combine(root, "Broken.slnx");
    File.WriteAllText(broken, "invalid solution");
    try { await SolutionProjects.UpdateAsync(broken, projects, true, _ => { }, default); throw new Exception("Invalid solution accepted"); }
    catch (IOException) { }
    if (File.ReadAllText(broken) != "invalid solution") throw new Exception("Failed edit was not restored");
    Console.WriteLine("PASS failed edit rollback");
    return 0;
}
finally { Directory.Delete(root, recursive: true); }
