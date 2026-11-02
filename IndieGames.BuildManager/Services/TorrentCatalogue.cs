using MonoTorrent;
using System.IO;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

public sealed record TorrentProject
{
    public string PackageKind { get; init; } = "Source";
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string FolderName { get; init; } = "";
    public string PackageName { get; init; } = "";
    public long PackageBytes { get; init; }
    public long UnpackedBytes { get; init; }
    public int FileCount { get; init; }
    public string Sha256 { get; init; } = "";
    public string InfoHash { get; init; } = "";
    public string TorrentBase64 { get; init; } = "";
    public DateTimeOffset CreatedUtc { get; init; }
    public string SizeDisplay => $"{PackageBytes / 1048576.0:N1} MB";
}

public sealed class TorrentCatalogue
{
    public int Version { get; init; } = 1;
    public List<TorrentProject> Projects { get; init; } = [];
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true, PropertyNameCaseInsensitive = true };
    public const int MaximumCatalogueBytes = 8 * 1024 * 1024;

    public static async Task<TorrentCatalogue> LoadAsync(string location, CancellationToken token)
    {
        using var memory = new MemoryStream();
        if (Uri.TryCreate(location, UriKind.Absolute, out var uri) && (uri.Scheme == "https" || uri.Scheme == "http"))
        {
            using var client = new System.Net.Http.HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            using var response = await client.GetAsync(uri, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, token);
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync(token);
            await CopyBoundedAsync(stream, memory, token);
        }
        else
        {
            await using var stream = File.OpenRead(location);
            await CopyBoundedAsync(stream, memory, token);
        }
        var catalogue = JsonSerializer.Deserialize<TorrentCatalogue>(memory.ToArray(), JsonOptions) ?? throw new InvalidDataException("Empty catalogue.");
        if (catalogue.Version != 1 || catalogue.Projects == null || catalogue.Projects.Count > 1000)
            throw new InvalidDataException("Unsupported catalogue version or project count.");
        var hashes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var project in catalogue.Projects)
        {
            await ValidateAsync(project);
            if (!hashes.Add(project.InfoHash)) throw new InvalidDataException("Duplicate torrent in catalogue.");
        }
        return catalogue;
    }

    private static async Task CopyBoundedAsync(Stream input, Stream output, CancellationToken token)
    {
        byte[] buffer = new byte[81920]; int read;
        while ((read = await input.ReadAsync(buffer, token)) != 0)
        {
            if (output.Length + read > MaximumCatalogueBytes) throw new InvalidDataException("Catalogue exceeds 8 MB.");
            await output.WriteAsync(buffer.AsMemory(0, read), token);
        }
    }

    public async Task SaveAsync(string path, CancellationToken token)
    {
        byte[] json = JsonSerializer.SerializeToUtf8Bytes(this, JsonOptions);
        if (json.Length > MaximumCatalogueBytes) throw new InvalidDataException("Catalogue exceeds 8 MB; export fewer projects.");
        string full = Path.GetFullPath(path);
        Directory.CreateDirectory(Path.GetDirectoryName(full)!);
        string temporary = full + ".tmp-" + Guid.NewGuid().ToString("N");
        try { await File.WriteAllBytesAsync(temporary, json, token); File.Move(temporary, full, true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }

    public static async Task<Torrent> ValidateAsync(TorrentProject project)
    {
        if (project == null || project.PackageKind is not ("Source" or "Game") || !Guid.TryParseExact(project.Id, "N", out _) || string.IsNullOrWhiteSpace(project.Name) || project.Name.Length > 200)
            throw new InvalidDataException("Invalid project identity.");
        ProjectBundle.ValidateSegment(project.FolderName);
        if (project.FolderName.StartsWith('.')) throw new InvalidDataException("Project folder cannot start with a dot.");
        if (project.PackageName != project.Id + ".zip" || project.PackageBytes <= 0 || project.PackageBytes > 101L * 1024 * 1024 * 1024 || project.UnpackedBytes < 0 ||
            project.UnpackedBytes > 100L * 1024 * 1024 * 1024 || project.FileCount is < 1 or > 100000)
            throw new InvalidDataException("Invalid package name, size or file count.");
        if (string.IsNullOrEmpty(project.Sha256) || project.Sha256.Length != 64 || !project.Sha256.All(Uri.IsHexDigit) || string.IsNullOrEmpty(project.InfoHash) || project.InfoHash.Length != 40 || !project.InfoHash.All(Uri.IsHexDigit))
            throw new InvalidDataException("Invalid package hash.");
        if (string.IsNullOrEmpty(project.TorrentBase64) || project.TorrentBase64.Length > 4 * 1024 * 1024) throw new InvalidDataException("Torrent metadata is missing or too large.");
        var torrent = await Torrent.LoadAsync(Convert.FromBase64String(project.TorrentBase64));
        if (torrent.Name != project.PackageName || torrent.Files.Count != 1 || torrent.Files[0].Path != project.PackageName ||
            torrent.Size != project.PackageBytes || !string.Equals(torrent.InfoHashes.V1?.ToHex(), project.InfoHash, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("Torrent metadata does not match the catalogue entry.");
        return torrent;
    }
}
