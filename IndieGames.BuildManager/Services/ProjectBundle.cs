using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace IndieGames.BuildManager.Services;

public sealed record ShareFile(string RelativePath, long Bytes);
public sealed record SharePlan(string Folder, string Name, IReadOnlyList<ShareFile> Files)
{
    public string PackageKind { get; init; } = "Source";
    public long TotalBytes => Files.Sum(f => f.Bytes);
}

public static class ProjectBundle
{
    private static readonly HashSet<string> ExcludedDirectories = new(StringComparer.OrdinalIgnoreCase)
    { "bin", "obj", ".git", ".svn", ".hg", ".vs", ".idea", "logs", "backup", "backups", ".backup", ".backups", ".xnatofna-backup", "node_modules", ".torrent-sharing", "torrent-sharing",
      "audio-conversion", "font-conversion", "texture-conversion", "wavebank-conversion", "effect-recovery", "shader-repair-backups", "font-repair-backups", "video-conversion" };

    public static SharePlan Preview(string folder, CancellationToken token = default, string packageKind = "Source")
    {
        if (packageKind is not ("Source" or "Game")) throw new InvalidDataException("Unknown package kind.");
        string root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(folder));
        var files = new List<ShareFile>();
        void Walk(string directory)
        {
            token.ThrowIfCancellationRequested();
            if ((File.GetAttributes(directory) & FileAttributes.ReparsePoint) != 0) return;
            foreach (var child in Directory.EnumerateDirectories(directory))
            {
                string name = Path.GetFileName(child);
                if ((!ExcludedDirectories.Contains(name) || packageKind == "Game" && name.Equals("bin", StringComparison.OrdinalIgnoreCase)) && !name.StartsWith("584E", StringComparison.OrdinalIgnoreCase) && !name.StartsWith(".torrent-install-", StringComparison.OrdinalIgnoreCase)) Walk(child);
            }
            foreach (var file in Directory.EnumerateFiles(directory))
            {
                token.ThrowIfCancellationRequested();
                var info = new FileInfo(file); string name = info.Name;
                if (packageKind == "Game" && (name.EndsWith(".runtimeconfig.dev.json", StringComparison.OrdinalIgnoreCase) || name.Equals(".buildmanager-files", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".pdb", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))) continue;
                if ((info.Attributes & FileAttributes.ReparsePoint) != 0 || name.EndsWith(".bak", StringComparison.OrdinalIgnoreCase) ||
                    name.Contains(".bak.", StringComparison.OrdinalIgnoreCase) || name.Contains(".before-", StringComparison.OrdinalIgnoreCase) ||
                    name.StartsWith(".env", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".user", StringComparison.OrdinalIgnoreCase) ||
                    name.EndsWith(".suo", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".torrent", StringComparison.OrdinalIgnoreCase) ||
                    name.EndsWith(".log", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".tmp", StringComparison.OrdinalIgnoreCase)) continue;
                string relative = Path.GetRelativePath(root, file);
                foreach (string segment in relative.Split(Path.DirectorySeparatorChar)) ValidateSegment(segment);
                files.Add(new(relative, info.Length));
            }
        }
        Walk(root);
        if (!files.Any(f => packageKind == "Source" ? f.RelativePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) : IsGameEntry(f.RelativePath, files.Select(x => x.RelativePath))))
            throw new InvalidDataException("Choose source containing a .csproj, or a playable Windows game folder containing a game executable. Modern .NET builds also need their matching runtimeconfig/deps files.");
        if (packageKind == "Game" && PortableGames.ReadManifest(root) is { EntryPoint.Length: > 0 }) _ = PortableGames.ResolveExecutable(root);
        string projectName = Path.GetFileName(root); ValidateSegment(projectName);
        if (files.Count > 100000 || files.Sum(f => f.Bytes) > 100L * 1024 * 1024 * 1024) throw new InvalidDataException("Project snapshot exceeds the supported size.");
        return new(root, projectName, files.OrderBy(f => f.RelativePath, StringComparer.OrdinalIgnoreCase).ToArray()) { PackageKind = packageKind };
    }

    private static bool IsGameEntry(string path, IEnumerable<string> files) => PortableGames.IsEntry(path, files);

    public static async Task CreateAsync(SharePlan plan, string zipPath, CancellationToken token)
    {
        if ((File.GetAttributes(plan.Folder) & FileAttributes.ReparsePoint) != 0) throw new IOException("Project folder is a link. Choose its actual source folder.");
        Directory.CreateDirectory(Path.GetDirectoryName(zipPath)!);
        await using var file = File.Create(zipPath);
        using var zip = new ZipArchive(file, ZipArchiveMode.Create, leaveOpen: true);
        foreach (var item in plan.Files)
        {
            token.ThrowIfCancellationRequested();
            string source = Path.GetFullPath(Path.Combine(plan.Folder, item.RelativePath));
            if (!source.StartsWith(plan.Folder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("File outside project folder.");
            string current = source;
            while (!string.Equals(current, plan.Folder, StringComparison.OrdinalIgnoreCase))
            {
                if ((File.GetAttributes(current) & FileAttributes.ReparsePoint) != 0) throw new IOException("Project changed: a path is now a link. Preview again.");
                current = Path.GetDirectoryName(current)!;
            }
            await using var input = File.OpenRead(source);
            if (input.Length != item.Bytes) throw new IOException("Project changed since preview. Preview again.");
            var entry = zip.CreateEntry(item.RelativePath.Replace('\\', '/'), CompressionLevel.Fastest);
            entry.LastWriteTime = new DateTimeOffset(1980, 1, 1, 0, 0, 0, TimeSpan.Zero);
            await using var output = entry.Open();
            await input.CopyToAsync(output, token);
        }
    }

    public static async Task<string> InstallAsync(TorrentProject project, string zipPath, string archiveRoot, CancellationToken token)
    {
        await TorrentCatalogue.ValidateAsync(project);
        await using (var input = File.OpenRead(zipPath))
        {
            if (input.Length != project.PackageBytes || !string.Equals(Convert.ToHexString(await SHA256.HashDataAsync(input, token)), project.Sha256, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Downloaded package failed SHA-256 verification.");
        }
        string root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(archiveRoot));
        Directory.CreateDirectory(root);
        string destination = Path.Combine(root, project.FolderName);
        if (Directory.Exists(destination) || File.Exists(destination)) throw new IOException("This project folder already exists. Choose a different archive directory; existing files are never overwritten.");
        string staging = Path.Combine(root, ".torrent-install-" + Guid.NewGuid().ToString("N"));
        using var zip = ZipFile.OpenRead(zipPath);
        if (zip.Entries.Count > 200000) throw new InvalidDataException("Too many archive entries.");
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        long total = 0; int files = 0; bool hasProject = false;
        foreach (var entry in zip.Entries)
        {
            string relative = entry.FullName.Replace('\\', '/');
            string[] parts = relative.TrimEnd('/').Split('/');
            foreach (string part in parts) ValidateSegment(part);
            if (!seen.Add(relative.TrimEnd('/')) || ((entry.ExternalAttributes >> 16) & 0xF000) == 0xA000 ||
                (entry.ExternalAttributes & (int)FileAttributes.ReparsePoint) != 0) throw new InvalidDataException("Duplicate or linked archive entry.");
            if (relative.EndsWith('/')) continue;
            total = checked(total + entry.Length); files++;
            if (files > project.FileCount || total > project.UnpackedBytes) throw new InvalidDataException("Archive exceeds the catalogue's declared size.");
            hasProject |= project.PackageKind == "Source" ? relative.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) : IsGameEntry(relative, zip.Entries.Select(x => x.FullName.Replace('\\', '/')));
        }
        if (!hasProject || total != project.UnpackedBytes || files != project.FileCount) throw new InvalidDataException("Archive does not match the declared project contents.");
        var drive = new DriveInfo(Path.GetPathRoot(root)!);
        if (drive.AvailableFreeSpace < total + 1024 * 1024) throw new IOException("Not enough free disk space to install this project.");
        Directory.CreateDirectory(staging);
        try
        {
            foreach (var entry in zip.Entries)
            {
                token.ThrowIfCancellationRequested();
                string relative = entry.FullName.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
                string path = Path.GetFullPath(Path.Combine(staging, relative));
                if (!path.StartsWith(staging + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Unsafe archive path.");
                if (entry.FullName.EndsWith('/') || entry.FullName.EndsWith('\\')) { Directory.CreateDirectory(path); continue; }
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                await using var input = entry.Open(); await using var output = new FileStream(path, FileMode.CreateNew);
                await input.CopyToAsync(output, token);
            }
            if (project.PackageKind == "Game" && PortableGames.ReadManifest(staging) is { EntryPoint.Length: > 0 }) _ = PortableGames.ResolveExecutable(staging);
            token.ThrowIfCancellationRequested();
            Directory.Move(staging, destination);
            return destination;
        }
        finally
        {
            // Staging is a fresh GUID directory under the selected archive root.
            string cleanup = Path.GetFullPath(staging);
            if (Directory.Exists(cleanup) && string.Equals(Path.GetDirectoryName(cleanup), root, StringComparison.OrdinalIgnoreCase) &&
                Path.GetFileName(cleanup).StartsWith(".torrent-install-", StringComparison.Ordinal)) Directory.Delete(cleanup, recursive: true);
        }
    }

    public static void ValidateSegment(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name is "." or ".." || name.Length > 200 || name.EndsWith('.') || name.EndsWith(' ') ||
            name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || name.IndexOfAny(['/', '\\', ':']) >= 0)
            throw new InvalidDataException("Unsafe project or archive filename.");
        string stem = name.Split('.')[0].ToUpperInvariant();
        if (stem is "CON" or "PRN" or "AUX" or "NUL" || (stem.Length == 4 && (stem.StartsWith("COM") || stem.StartsWith("LPT")) && stem[3] is >= '0' and <= '9'))
            throw new InvalidDataException("Reserved Windows filename.");
    }
}
