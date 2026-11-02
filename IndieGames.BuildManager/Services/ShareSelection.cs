using System.IO;
namespace IndieGames.BuildManager.Services;
public sealed record ShareSelection(string Source, string PlayableGame, string Name)
{
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
