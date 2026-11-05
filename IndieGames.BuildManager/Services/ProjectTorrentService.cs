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
    private readonly SemaphoreSlim _shareGate = new(1, 1);
    private readonly HashSet<string> _rememberedHashes = new(StringComparer.OrdinalIgnoreCase);
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

    public async Task<TorrentProject> CreateShareAsync(SharePlan plan, IReadOnlyList<string> trackers, CancellationToken token, string iconBase64 = "")
    {
        await _shareGate.WaitAsync(token);
        try { return await CreateShareCoreAsync(plan, trackers, token, iconBase64); }
        finally { _shareGate.Release(); }
    }

    private async Task<TorrentProject> CreateShareCoreAsync(SharePlan plan, IReadOnlyList<string> trackers, CancellationToken token, string iconBase64)
    {
        TorrentCatalogue.ValidateIcon(iconBase64);
        foreach (string tracker in trackers)
            if (!Uri.TryCreate(tracker, UriKind.Absolute, out var trackerUri) || trackerUri.Scheme is not ("udp" or "http" or "https")) throw new InvalidDataException("Invalid tracker URL.");
        string folderName = plan.PackageKind == "Game" ? plan.Name : Path.GetFileName(plan.Folder);
        ProjectBundle.ValidateSegment(folderName);
        string id = Guid.NewGuid().ToString("N");
        string directory = Path.Combine(StateDirectory, "shares", id);
        string package = Path.Combine(directory, id + ".zip");
        Directory.CreateDirectory(directory);
        bool retained = false;
        try
        {
        await ProjectBundle.CreateAsync(plan, package, token);
        var content = await SnapshotContents.ReadAsync(package, token);
        var catalogue = File.Exists(LocalCataloguePath) ? await TorrentCatalogue.LoadAsync(LocalCataloguePath, token) : new TorrentCatalogue();
        for (int i = 0; i < catalogue.Projects.Count; i++)
        {
            var candidate = catalogue.Projects[i];
            if (candidate.PackageKind != plan.PackageKind || !candidate.Name.Equals(plan.Name, StringComparison.OrdinalIgnoreCase) ||
                !candidate.FolderName.Equals(folderName, StringComparison.OrdinalIgnoreCase) || candidate.FileCount != plan.Files.Count || candidate.UnpackedBytes != plan.TotalBytes) continue;
            string cached = CachedPackagePath(candidate);
            if (!File.Exists(cached)) continue;
            string existingContent = candidate.ContentSha256;
            try
            {
                await VerifyCachedAsync(candidate, cached, token);
                if (string.IsNullOrEmpty(existingContent))
                {
                    existingContent = (await SnapshotContents.ReadAsync(cached, token)).Fingerprint;
                    candidate = candidate with { ContentSha256 = existingContent }; catalogue.Projects[i] = candidate;
                }
            }
            catch (Exception error) when (error is IOException or InvalidDataException) { continue; }
            if (!existingContent.Equals(content.Fingerprint, StringComparison.OrdinalIgnoreCase)) continue;
            if (!string.IsNullOrEmpty(iconBase64)) candidate = candidate with { IconBase64 = iconBase64 };
            catalogue.Projects[i] = candidate;
            await catalogue.SaveAsync(LocalCataloguePath, token);
            return candidate;
        }
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
        var project = new TorrentProject { PackageKind = plan.PackageKind, Id = id, Name = plan.Name, FolderName = folderName, PackageName = id + ".zip",
            PackageBytes = input.Length, UnpackedBytes = plan.TotalBytes, FileCount = plan.Files.Count,
            Sha256 = Convert.ToHexString(await SHA256.HashDataAsync(input, token)), ContentSha256 = content.Fingerprint, InfoHash = torrent.InfoHashes.V1!.ToHex(),
            TorrentBase64 = Convert.ToBase64String(metadata), IconBase64 = iconBase64, CreatedUtc = DateTimeOffset.UtcNow };
        await TorrentCatalogue.ValidateAsync(project);
        catalogue.Projects.Add(project);
        await catalogue.SaveAsync(LocalCataloguePath, token);
        retained = true;
        return project;
        }
        finally
        {
            if (!retained)
            {
                string prefix = Path.GetFullPath(Path.Combine(StateDirectory, "shares")) + Path.DirectorySeparatorChar;
                if (Path.GetFullPath(directory).StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && Path.GetFileName(directory) == id)
                {
                    try
                    {
                        File.Delete(package); File.Delete(Path.Combine(directory, id + ".torrent"));
                        if (Directory.Exists(directory) && !Directory.EnumerateFileSystemEntries(directory).Any()) Directory.Delete(directory);
                    }
                    catch (IOException) { }
                    catch (UnauthorizedAccessException) { }
                }
            }
        }
    }

    public string CachedPackagePath(TorrentProject project)
    {
        string own = Path.Combine(StateDirectory, "shares", project.Id, project.PackageName);
        return File.Exists(own) ? own : Path.Combine(StateDirectory, "downloads", project.InfoHash.ToLowerInvariant(), project.PackageName);
    }

    static async Task VerifyCachedAsync(TorrentProject project, string path, CancellationToken token)
    {
        await using var file = File.OpenRead(path);
        if (file.Length != project.PackageBytes || !Convert.ToHexString(await SHA256.HashDataAsync(file, token)).Equals(project.Sha256, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("Cached package is incomplete or failed its checksum.");
    }

    public async Task<SnapshotContents> InspectContentsAsync(TorrentProject project, CancellationToken token)
    {
        await TorrentCatalogue.ValidateAsync(project);
        string path = CachedPackagePath(project);
        if (!File.Exists(path)) throw new FileNotFoundException("Download or create this snapshot locally before comparing its files.", path);
        await VerifyCachedAsync(project, path, token);
        return await SnapshotContents.ReadAsync(path, token);
    }

    public async Task<ProjectTransfer> StartAsync(TorrentProject project, bool seed, CancellationToken token)
    {
        var torrent = await TorrentCatalogue.ValidateAsync(project);
        token.ThrowIfCancellationRequested();
        var existing = Transfers.FirstOrDefault(t => t.Project.InfoHash.Equals(project.InfoHash, StringComparison.OrdinalIgnoreCase));
        if (existing != null)
        {
            if (seed && !existing.Manager.Complete) throw new InvalidOperationException("Finish downloading this package before seeding it.");
            await RememberProjectAsync(project, token);
            await existing.Manager.StartAsync(); return existing;
        }
        string own = Path.Combine(StateDirectory, "shares", project.Id, project.PackageName);
        string downloaded = Path.Combine(StateDirectory, "downloads", project.InfoHash.ToLowerInvariant(), project.PackageName);
        string package = File.Exists(own) ? own : downloaded;
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
        await RememberProjectAsync(project, token);
        var manager = await Engine.AddAsync(torrent, Path.GetDirectoryName(package)!, new TorrentSettingsBuilder { MaximumConnections = 40 }.ToSettings());
        var transfer = new ProjectTransfer(project, manager, package);
        Transfers.Add(transfer);
        if(seed)
        {
            await manager.HashCheckAsync(false).WaitAsync(token);
            if(!manager.Complete)
            {
                await Engine.RemoveAsync(manager);Transfers.Remove(transfer);
                throw new InvalidDataException("Cached package does not match the torrent pieces; seeding was skipped.");
            }
        }
        await manager.StartAsync();
        return transfer;
    }

    private async Task RememberProjectAsync(TorrentProject project, CancellationToken token)
    {
        await _shareGate.WaitAsync(token);
        try
        {
            if(_rememberedHashes.Contains(project.InfoHash))return;
            var catalogue=File.Exists(LocalCataloguePath) ? await TorrentCatalogue.LoadAsync(LocalCataloguePath,token) : new TorrentCatalogue();
            _rememberedHashes.UnionWith(catalogue.Projects.Select(p=>p.InfoHash));
            if(catalogue.Projects.Any(p=>p.InfoHash.Equals(project.InfoHash,StringComparison.OrdinalIgnoreCase)))return;
            catalogue.Projects.Add(project);
            await catalogue.SaveAsync(LocalCataloguePath,token);
            _rememberedHashes.Add(project.InfoHash);
        }
        finally { _shareGate.Release(); }
    }

    public async Task<int> StartSavedTransfersAsync(Action<string> log, CancellationToken token)
    {
        var catalogue=File.Exists(LocalCataloguePath) ? await TorrentCatalogue.LoadAsync(LocalCataloguePath,token) : new TorrentCatalogue();
        var projects=catalogue.Projects.Concat(Transfers.Select(t=>t.Project)).DistinctBy(p=>p.InfoHash,StringComparer.OrdinalIgnoreCase).ToArray();
        int started=0;
        foreach(var project in projects)
        {
            token.ThrowIfCancellationRequested();
            try { await StartAsync(project,false,token);started++;log("Started / resumed: "+project.Name); }
            catch(Exception error) when(error is IOException or InvalidDataException or InvalidOperationException or ArgumentException)
            { log("Could not start "+project.Name+": "+error.Message); }
        }
        log(started==0 ? "No saved transfers started. Open the catalogue, select a package, and choose Start selected." : $"Started / resumed {started} torrent(s). Complete packages seed automatically.");
        return started;
    }

    public async Task<int> SeedLocalCatalogueAsync(Action<string> log, CancellationToken token)
    {
        if(!File.Exists(LocalCataloguePath)){log("No saved local catalogue. No transfers started.");return 0;}
        var catalogue=await TorrentCatalogue.LoadAsync(LocalCataloguePath,token);
        int started=0;
        foreach(var project in catalogue.Projects)
        {
            token.ThrowIfCancellationRequested();
            try {await StartAsync(project,true,token);started++;log("Seeding: "+project.Name);}
            catch(Exception error) when(error is IOException or InvalidDataException or InvalidOperationException or ArgumentException)
            {log("Skipped "+project.Name+": "+error.Message);}
        }
        log($"Seeding {started} completed local package(s).");return started;
    }

    public async Task RemoveAsync(TorrentProject project, CancellationToken token, bool keepLocalMetadata = false)
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
        if (!keepLocalMetadata && catalogue != null && catalogue.Projects.RemoveAll(p => p.InfoHash.Equals(project.InfoHash, StringComparison.OrdinalIgnoreCase)) > 0)
        {
            await catalogue.SaveAsync(LocalCataloguePath, CancellationToken.None);
            _rememberedHashes.Remove(project.InfoHash);
        }
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
        finally { _engine.Dispose(); _engine = null; Transfers.Clear(); }
    }
}
