using System.IO;
namespace IndieGames.BuildManager.Services;
public sealed record CleanupFolder(string Root, string Path, long Bytes);
public static class StorageCleanup
{
    private static string Full(string p) => System.IO.Path.TrimEndingDirectorySeparator(System.IO.Path.GetFullPath(p));
    private static void Check(string root, string path)
    {
        root = Full(root); path = Full(path);
        if (!path.StartsWith(root + System.IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new IOException("Cleanup target is outside its root.");
        for (string? current = path; current != null; current = Directory.GetParent(current)?.FullName)
        {
            if ((File.GetAttributes(current) & FileAttributes.ReparsePoint) != 0) throw new IOException("Cleanup refuses linked folders.");
        }
    }
    private static long Inspect(string root, string path)
    {
        Check(root, path); long bytes = 0;
        foreach (var f in Directory.EnumerateFiles(path)) { if ((File.GetAttributes(f) & FileAttributes.ReparsePoint) != 0) throw new IOException("Cleanup refuses linked files."); bytes += new FileInfo(f).Length; }
        foreach (var d in Directory.EnumerateDirectories(path)) bytes += Inspect(root, d);
        return bytes;
    }
    public static IReadOnlyList<CleanupFolder> Preview(LibraryStorage storage, bool legacy)
    {
        storage.Validate(); var found = new List<CleanupFolder>();
        if (!legacy)
        {
            foreach (var name in new[] { "bin", "obj" })
            {
                string path = System.IO.Path.Combine(storage.BuildCache, name);
                if (Directory.Exists(path)) found.Add(new(Full(storage.BuildCache), Full(path), Inspect(storage.BuildCache, path)));
            }
        }
        else
        {
            void Walk(string dir)
            {
                if ((File.GetAttributes(dir) & FileAttributes.ReparsePoint) != 0) return;
                bool project = Directory.EnumerateFiles(dir, "*.csproj").Any();
                foreach (var child in Directory.EnumerateDirectories(dir))
                {
                    string name = System.IO.Path.GetFileName(child);
                    if (name is "bin" or "obj") { if (project) found.Add(new(Full(storage.Archive), Full(child), Inspect(storage.Archive, child))); }
                    else if (!name.StartsWith('.') && !name.Equals("backups", StringComparison.OrdinalIgnoreCase) && !name.Equals("backup", StringComparison.OrdinalIgnoreCase) && !name.StartsWith("584E", StringComparison.OrdinalIgnoreCase)) Walk(child);
                }
            }
            if (Directory.Exists(storage.Archive)) Walk(storage.Archive);
        }
        return found;
    }
    public static void Delete(CleanupFolder item)
    {
        // Revalidate every path after preview, including links introduced since it was displayed.
        Inspect(item.Root, item.Path);
        void Remove(string path)
        {
            Check(item.Root, path);
            foreach (var file in Directory.EnumerateFiles(path)) { if ((File.GetAttributes(file) & FileAttributes.ReparsePoint) != 0) throw new IOException("Linked file."); File.Delete(file); }
            foreach (var child in Directory.EnumerateDirectories(path)) Remove(child);
            Directory.Delete(path);
        }
        Remove(item.Path);
    }
}
