using System.IO;
namespace IndieGames.BuildManager.Services;
public sealed record ShareSelection(string Source, string PlayableGame, string Name)
{
    public static ShareSelection ForPlayable(string folder)
    {
        var game = PortableGames.Inspect(Path.GetFullPath(folder));
        _ = PortableGames.ResolveExecutable(game.Folder);
        return new(game.Folder, game.Folder, game.Name);
    }
    public static IReadOnlyList<string> SourceFolders(string archive)
    {
        string root=Path.GetFullPath(archive);
        if (!Directory.Exists(root)) return [];
        var folders=Directory.EnumerateDirectories(root).Where(folder=>(File.GetAttributes(folder)&FileAttributes.ReparsePoint)==0 && Directory.EnumerateFiles(folder,"*.csproj").Any()).ToList();
        if (Directory.EnumerateFiles(root,"*.csproj").Any()) folders.Add(root);
        return folders.OrderBy(Path.GetFileName,StringComparer.OrdinalIgnoreCase).ToArray();
    }
    public static ShareSelection ForSource(string archive, string source, LibraryStorage storage)
    {
        string canonical=Path.Combine(source,Path.GetFileName(Path.TrimEndingDirectorySeparator(source))+".csproj");
        bool Executable(string path)
        {
            try { return System.Xml.Linq.XDocument.Load(path).Descendants().Any(e=>e.Name.LocalName=="OutputType" && (e.Value.Trim().Equals("Exe",StringComparison.OrdinalIgnoreCase) || e.Value.Trim().Equals("WinExe",StringComparison.OrdinalIgnoreCase))); }
            catch (System.Xml.XmlException) { return false; }
        }
        string project=File.Exists(canonical) ? canonical : Directory.EnumerateFiles(source,"*.csproj").OrderByDescending(Executable).ThenBy(p=>p,StringComparer.OrdinalIgnoreCase).FirstOrDefault()
            ?? throw new IOException("The selected source folder has no active project.");
        return ForProject(archive,project,storage);
    }
    public static ShareSelection ForProject(string archive, string project, LibraryStorage storage)
    {
        string root=Path.TrimEndingDirectorySeparator(Path.GetFullPath(archive));
        string file=Path.GetFullPath(project);
        if (!file.StartsWith(root+Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase)) throw new IOException("Select a project inside the archive.");
        string relative=Path.GetRelativePath(root,file);
        string source=Path.Combine(root,relative.Split(Path.DirectorySeparatorChar)[0]);
        if (Path.GetDirectoryName(file)!.Equals(root,StringComparison.OrdinalIgnoreCase)) source=root;
        string preferred=storage.GameFolder(file);
        bool Playable(string folder) => Directory.Exists(folder) && Directory.EnumerateFiles(folder,"*.exe").Any(exe=>File.Exists(Path.ChangeExtension(exe,".deps.json")) && File.Exists(Path.ChangeExtension(exe,".runtimeconfig.json")));
        IEnumerable<string> Folders(string folder)
        {
            if (!Directory.Exists(folder) || (File.GetAttributes(folder)&FileAttributes.ReparsePoint)!=0) yield break;
            yield return folder;
            foreach (string child in Directory.EnumerateDirectories(folder)) foreach (string item in Folders(child)) yield return item;
        }
        string playable=Playable(preferred) ? preferred : Folders(Path.Combine(Path.GetDirectoryName(file)!,"bin"))
            .Where(Playable).OrderByDescending(Directory.GetLastWriteTimeUtc).FirstOrDefault() ?? preferred;
        return new(source,playable,Path.GetFileName(source));
    }
}
