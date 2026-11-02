using MonoTorrent;
using MonoTorrent.Client;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace IndieGames.BuildManager.Services;

public sealed record ProjectTransfer(TorrentProject Project, TorrentManager Manager, string PackagePath);

public sealed class ProjectTorrentService : IAsyncDisposable
{
    public string StateDirectory { get; }
    public string LocalCataloguePath => Path.Combine(StateDirectory, "my-catalogue.json");
    public List<ProjectTransfer> Transfers { get; } = [];
    private ClientEngine? _engine;
    private readonly EngineSettings? _settings;
    public int ListenPort { get; set; } = 6881;
    public bool AllowPortForwarding { get; set; }
    public bool NetworkStarted => _engine != null;

    public ProjectTorrentService(string stateDirectory, EngineSettings? settings = null)
    { StateDirectory = Path.GetFullPath(stateDirectory); _settings = settings; }

    internal ClientEngine Engine => _engine ??= new ClientEngine(_settings ?? new EngineSettingsBuilder
    {
        CacheDirectory = Path.Combine(StateDirectory, "engine-cache"),
        AutoSaveLoadFastResume = true, AutoSaveLoadDhtCache = true,
        AllowPortForwarding = AllowPortForwarding, AllowLocalPeerDiscovery = true,
        ListenEndPoints = new Dictionary<string, IPEndPoint> { ["ipv4"] = new(IPAddress.Any, ListenPort) },
        DhtEndPoint = new IPEndPoint(IPAddress.Any, ListenPort)
    }.ToSettings());

    public async Task<TorrentProject> CreateShareAsync(SharePlan plan, IReadOnlyList<string> trackers, CancellationToken token)
    {
        string id = Guid.NewGuid().ToString("N");
        string directory = Path.Combine(StateDirectory, "shares", id);
        string package = Path.Combine(directory, id + ".zip");
        Directory.CreateDirectory(directory);
        await ProjectBundle.CreateAsync(plan, package, token);
        var creator = new TorrentCreator(TorrentType.V1Only) { Comment = "Indie Games Build Manager project snapshot" };
        foreach (string tracker in trackers)
        {
            if (!Uri.TryCreate(tracker, UriKind.Absolute, out var uri) || uri.Scheme is not ("udp" or "http" or "https")) throw new InvalidDataException("Invalid tracker URL.");
            creator.Announces.Add(new List<string> { tracker });
        }
        string torrentPath = Path.Combine(directory, id + ".torrent");
        await creator.CreateAsync(new TorrentFileSource(package), torrentPath, token);
        byte[] metadata = await File.ReadAllBytesAsync(torrentPath, token);
        var torrent = await Torrent.LoadAsync(metadata);
        await using var input = File.OpenRead(package);
        var project = new TorrentProject { PackageKind = plan.PackageKind, Id = id, Name = plan.Name, FolderName = Path.GetFileName(plan.Folder), PackageName = id + ".zip",
            PackageBytes = input.Length, UnpackedBytes = plan.TotalBytes, FileCount = plan.Files.Count,
            Sha256 = Convert.ToHexString(await SHA256.HashDataAsync(input, token)), InfoHash = torrent.InfoHashes.V1!.ToHex(),
            TorrentBase64 = Convert.ToBase64String(metadata), CreatedUtc = DateTimeOffset.UtcNow };
        await TorrentCatalogue.ValidateAsync(project);
        var catalogue = File.Exists(LocalCataloguePath) ? await TorrentCatalogue.LoadAsync(LocalCataloguePath, token) : new TorrentCatalogue();
        catalogue.Projects.Add(project);
        await catalogue.SaveAsync(LocalCataloguePath, token);
        return project;
    }

    public async Task<ProjectTransfer> StartAsync(TorrentProject project, bool seed, CancellationToken token)
    {
        var torrent = await TorrentCatalogue.ValidateAsync(project);
        token.ThrowIfCancellationRequested();
        var existing = Transfers.FirstOrDefault(t => t.Project.InfoHash.Equals(project.InfoHash, StringComparison.OrdinalIgnoreCase));
        if (existing != null)
        {
            if (seed && !existing.Manager.Complete) throw new InvalidOperationException("Finish downloading this package before seeding it.");
            await existing.Manager.StartAsync(); return existing;
        }
        string own = Path.Combine(StateDirectory, "shares", project.Id, project.PackageName);
        string downloaded = Path.Combine(StateDirectory, "downloads", project.InfoHash.ToLowerInvariant(), project.PackageName);
        string package = seed && File.Exists(own) ? own : downloaded;
        if (seed)
        {
            if (!File.Exists(package)) throw new FileNotFoundException("No cached package for this project. Download it first or create its share here.");
            await using var input = File.OpenRead(package);
            if (input.Length != project.PackageBytes || !Convert.ToHexString(await SHA256.HashDataAsync(input, token)).Equals(project.Sha256, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Cached package failed integrity verification.");
        }
        Directory.CreateDirectory(Path.GetDirectoryName(package)!);
        long remaining = project.PackageBytes - (File.Exists(package) ? new FileInfo(package).Length : 0);
        if (!seed && new DriveInfo(Path.GetPathRoot(package)!).AvailableFreeSpace < remaining + 1024 * 1024)
            throw new IOException("Not enough free space for this download in the torrent cache.");
        var manager = await Engine.AddAsync(torrent, Path.GetDirectoryName(package)!, new TorrentSettingsBuilder { MaximumConnections = 40 }.ToSettings());
        var transfer = new ProjectTransfer(project, manager, package);
        Transfers.Add(transfer);
        await manager.StartAsync();
        return transfer;
    }

    public async Task RemoveAsync(TorrentProject project, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var catalogue = File.Exists(LocalCataloguePath)
            ? await TorrentCatalogue.LoadAsync(LocalCataloguePath, token) : null;
        var transfer = Transfers.FirstOrDefault(t => t.Project.InfoHash.Equals(project.InfoHash, StringComparison.OrdinalIgnoreCase));
        if (transfer != null)
        {
            await transfer.Manager.StopAsync();
            await Engine.RemoveAsync(transfer.Manager);
            Transfers.Remove(transfer);
        }
        // Remove only this torrent identity: different snapshots may have the same display name.
        // Packages, resume data, source folders and installed games are deliberately retained.
        if (catalogue != null && catalogue.Projects.RemoveAll(p => p.InfoHash.Equals(project.InfoHash, StringComparison.OrdinalIgnoreCase)) > 0)
            await catalogue.SaveAsync(LocalCataloguePath, CancellationToken.None);
    }

    public async Task<string> InstallAsync(ProjectTransfer transfer, string archiveRoot, CancellationToken token)
    {
        if (!transfer.Manager.Complete || !transfer.Manager.HashChecked) throw new InvalidOperationException("Wait for the torrent to finish and verify all pieces.");
        await Engine.DiskManager.FlushAsync(transfer.Manager);
        return await ProjectBundle.InstallAsync(transfer.Project, transfer.PackagePath, archiveRoot, token);
    }

    public async ValueTask DisposeAsync()
    {
        if (_engine == null) return;
        try { await Task.WhenAll(Transfers.Select(t => t.Manager.StopAsync())).WaitAsync(TimeSpan.FromSeconds(15)); }
        catch (TimeoutException) { /* Dispose still closes the peer listeners; cached pieces can be rechecked next time. */ }
        finally { _engine.Dispose(); _engine = null; }
    }
}
