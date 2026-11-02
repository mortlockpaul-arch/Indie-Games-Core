namespace IndieGames.BuildManager.Services;
public sealed record TorrentStatus(string Name, string Kind, string State, double Progress, int Peers, long DownloadRate, long UploadRate)
{
    public string ProgressDisplay => $"{Progress:N1}%";
    public string DownloadDisplay => $"{DownloadRate / 1024.0:N0} KB/s";
    public string UploadDisplay => $"{UploadRate / 1024.0:N0} KB/s";
    public static TorrentStatus Capture(ProjectTransfer transfer) => new(transfer.Project.Name,
        transfer.Project.PackageKind == "Game" ? "Playable Game" : "Source", transfer.Manager.State.ToString(),
        transfer.Manager.Progress, transfer.Manager.OpenConnections, transfer.Manager.Monitor.DownloadRate, transfer.Manager.Monitor.UploadRate);
}
public sealed record TorrentStatusSnapshot(IReadOnlyList<TorrentStatus> Transfers, string Activity)
{
    public string Summary => Transfers.Count == 0 ? "No Active Torrents" :
        $"{Transfers.Count} Torrent(s) · {Transfers.Count(t=>t.State == "Seeding")} Seeding · {Transfers.Count(t=>t.State == "Downloading")} Downloading · ↓ {Transfers.Sum(t=>t.DownloadRate)/1024.0:N0} KB/s · ↑ {Transfers.Sum(t=>t.UploadRate)/1024.0:N0} KB/s";
}
