using System.IO;

namespace IndieGames.BuildManager.Services;

public sealed record GlobalCatalogueResult(TorrentCatalogue Catalogue, bool Offline, string Message);

public sealed class GlobalCatalogue
{
    public const string Owner = "mortlockpaul-arch";
    public const string Repository = "Indie-Games-Catalogue";
    public const string RepositoryName = Owner + "/" + Repository;
    public const string Website = "https://github.com/" + RepositoryName;
    public const string CatalogueUrl = "https://raw.githubusercontent.com/" + RepositoryName + "/catalogue/catalogue.json";
    private readonly string _cachePath;
    private readonly Func<CancellationToken, Task<TorrentCatalogue>> _fetch;
    public GlobalCatalogue(string stateDirectory, Func<CancellationToken, Task<TorrentCatalogue>>? fetch = null)
    {
        _cachePath = Path.Combine(stateDirectory, "global-catalogue.json");
        _fetch = fetch ?? (token => TorrentCatalogue.LoadAsync(CatalogueUrl, token));
    }

    public async Task<GlobalCatalogueResult> RefreshAsync(CancellationToken token)
    {
        TorrentCatalogue catalogue;
        try { catalogue = await _fetch(token); }
        catch (Exception) when (!token.IsCancellationRequested)
        {
            if (!File.Exists(_cachePath))
                return new(new(), true, "Global catalogue unavailable. No offline copy yet. Check your connection and choose Refresh catalogue.");
            catalogue = await TorrentCatalogue.LoadAsync(_cachePath, token);
            return new(catalogue, true, "Offline — showing the last verified global catalogue. Choose Refresh catalogue to retry.");
        }
        // Only a completely validated download replaces the last good copy.
        try { await catalogue.SaveAsync(_cachePath, token); }
        catch(Exception error) when(error is IOException or UnauthorizedAccessException)
        { return new(catalogue, false, $"{catalogue.Projects.Count} approved package(s) · offline cache could not be saved; check the torrent cache folder in Storage settings."); }
        return new(catalogue, false, $"{catalogue.Projects.Count} approved package(s) · updated {DateTime.Now:t}");
    }
}
