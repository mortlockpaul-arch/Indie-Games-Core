using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

public interface IGitHubApi
{
    Task<JsonElement> SendAsync(HttpMethod method, string path, object? body, CancellationToken token);
}

public sealed class GitHubApiException(HttpStatusCode status, string message) : IOException(message)
{
    public HttpStatusCode Status { get; } = status;
}

public sealed class GitHubApi(string credential, HttpMessageHandler? handler = null) : IGitHubApi, IDisposable
{
    private readonly HttpClient _client = CreateClient(credential, handler);
    private static HttpClient CreateClient(string credential, HttpMessageHandler? handler)
    {
        var client = handler == null ? new HttpClient() : new HttpClient(handler);
        client.BaseAddress = new Uri("https://api.github.com/"); client.Timeout = TimeSpan.FromSeconds(60);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", credential);
        client.DefaultRequestHeaders.UserAgent.ParseAdd("IndieGames-BuildManager/1.0");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        return client;
    }
    public async Task<JsonElement> SendAsync(HttpMethod method, string path, object? body, CancellationToken token)
    {
        if (Uri.TryCreate(path, UriKind.Absolute, out _)) throw new ArgumentException("Use a GitHub API path.");
        using var request = new HttpRequestMessage(method, path.TrimStart('/'));
        if (body != null) request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token);
        await using var stream = await response.Content.ReadAsStreamAsync(token);
        using var memory = new MemoryStream(); byte[] buffer = new byte[81920]; int read;
        while ((read = await stream.ReadAsync(buffer, token)) != 0)
        {
            if (memory.Length + read > 16 * 1024 * 1024) throw new IOException("GitHub response exceeded the size limit.");
            memory.Write(buffer, 0, read);
        }
        if (!response.IsSuccessStatusCode)
            throw new GitHubApiException(response.StatusCode, $"GitHub returned {(int)response.StatusCode}. " +
                (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden
                    ? "Check GitHub sign-in, account permissions and API rate limits." : "Try again, or open My submissions to check whether your request was received."));
        if (memory.Length == 0) return JsonSerializer.SerializeToElement(new { });
        using var document = JsonDocument.Parse(memory.ToArray()); return document.RootElement.Clone();
    }
    public void Dispose() => _client.Dispose();
}

public sealed record CatalogueSubmission(string Url, string State, string Branch);

public sealed class CataloguePublisher(IGitHubApi api, Func<CancellationToken, Task<TorrentCatalogue>>? approved = null)
{
    private readonly Func<CancellationToken, Task<TorrentCatalogue>> _approved = approved ?? (token => TorrentCatalogue.LoadAsync(GlobalCatalogue.CatalogueUrl, token));
    private static string Required(JsonElement value, string key) => value.GetProperty(key).GetString() ?? throw new InvalidDataException("GitHub response is missing " + key);
    public async Task<string> LoginAsync(CancellationToken token) => Required(await api.SendAsync(HttpMethod.Get, "user", null, token), "login");

    public async Task<CatalogueSubmission> SubmitAsync(IReadOnlyList<TorrentProject> projects, CancellationToken token)
    {
        if (projects.Count is < 1 or > 20) throw new IOException("Select between 1 and 20 packages per submission.");
        var unique = projects.DistinctBy(p => p.InfoHash, StringComparer.OrdinalIgnoreCase).ToArray();
        foreach (var project in unique) await TorrentCatalogue.ValidateAsync(project);
        if (JsonSerializer.SerializeToUtf8Bytes(new TorrentCatalogue { Projects = unique.ToList() }).Length > TorrentCatalogue.MaximumCatalogueBytes)
            throw new IOException("Submission exceeds 8 MB. Publish fewer packages at once.");
        var catalogue = await _approved(token);
        unique = unique.Where(p => !catalogue.Projects.Any(a => a.InfoHash.Equals(p.InfoHash, StringComparison.OrdinalIgnoreCase))).ToArray();
        if (unique.Length == 0) return new(GlobalCatalogue.Website, "Approved", "");
        string login = await LoginAsync(token);
        string identity = string.Join("\n", unique.Select(p => p.InfoHash.ToLowerInvariant()).Order(StringComparer.Ordinal));
        string branch = "submission-" + Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(identity)))[..24].ToLowerInvariant();
        string head = login + ":" + branch;
        string source = "repos/" + GlobalCatalogue.RepositoryName;
        var pulls = await api.SendAsync(HttpMethod.Get, source + "/pulls?state=all&head=" + Uri.EscapeDataString(head) + "&per_page=100", null, token);
        foreach (var pull in pulls.EnumerateArray())
        {
            if (pull.GetProperty("head").GetProperty("ref").GetString() != branch) continue;
            string state = pull.GetProperty("state").GetString() == "open" ? "Awaiting approval" : pull.GetProperty("merged_at").ValueKind != JsonValueKind.Null ? "Merged · publishing" : "Closed";
            return new(Required(pull, "html_url"), state, branch);
        }
        string destination = GlobalCatalogue.RepositoryName;
        if (!login.Equals(GlobalCatalogue.Owner, StringComparison.OrdinalIgnoreCase))
        {
            var fork = await api.SendAsync(HttpMethod.Post, source + "/forks", new { default_branch_only = true }, token);
            destination = Required(fork, "full_name");
            if (!destination.StartsWith(login + "/", StringComparison.OrdinalIgnoreCase)) throw new IOException("GitHub returned an unexpected fork owner.");
            bool ready = false;
            for (int attempt = 0; attempt < 15; attempt++)
            {
                try
                {
                    var metadata = await api.SendAsync(HttpMethod.Get, "repos/" + destination, null, token);
                    if (!metadata.TryGetProperty("parent", out var parent) || Required(parent, "full_name") != GlobalCatalogue.RepositoryName)
                        throw new IOException("A repository with this name already exists but is not a fork of the global catalogue.");
                    await api.SendAsync(HttpMethod.Get, "repos/" + destination + "/git/ref/heads/main", null, token);
                    ready = true; break;
                }
                catch (GitHubApiException error) when (error.Status == HttpStatusCode.NotFound)
                { await Task.Delay(TimeSpan.FromSeconds(2), token); }
            }
            if (!ready) throw new IOException("Your catalogue fork is still being prepared. Choose Publish again shortly; your package is retained.");
        }
        string target = "repos/" + destination;
        var mainRef = await api.SendAsync(HttpMethod.Get, source + "/git/ref/heads/main", null, token);
        string baseSha = Required(mainRef.GetProperty("object"), "sha");
        bool exists = false;
        try
        {
            var previous = await api.SendAsync(HttpMethod.Get, target + "/git/ref/heads/" + branch, null, token);
            baseSha = Required(previous.GetProperty("object"), "sha"); exists = true;
        }
        catch (GitHubApiException error) when (error.Status == HttpStatusCode.NotFound) { }
        var baseCommit = await api.SendAsync(HttpMethod.Get, target + "/git/commits/" + baseSha, null, token);
        var tree = await api.SendAsync(HttpMethod.Post, target + "/git/trees", new {
            base_tree = Required(baseCommit.GetProperty("tree"), "sha"),
            tree = unique.Select(project => new { path = "entries/" + project.InfoHash.ToLowerInvariant() + ".json", mode = "100644", type = "blob",
                content = JsonSerializer.Serialize(new TorrentCatalogue { Projects = [project] }, new JsonSerializerOptions { WriteIndented = true }) }).ToArray()
        }, token);
        var commit = await api.SendAsync(HttpMethod.Post, target + "/git/commits", new {
            message = "Submit " + unique.Length + " package(s) for catalogue approval", tree = Required(tree, "sha"), parents = new[] { baseSha }
        }, token);
        if (exists) await api.SendAsync(HttpMethod.Patch, target + "/git/refs/heads/" + branch, new { sha = Required(commit, "sha"), force = false }, token);
        else await api.SendAsync(HttpMethod.Post, target + "/git/refs", new { @ref = "refs/heads/" + branch, sha = Required(commit, "sha") }, token);
        string summary = string.Join("\n", unique.Select(p => "- " + p.Name.Replace('\r',' ').Replace('\n',' ') + " (" + p.PackageKind + ") · " + p.SizeDisplay + " · `" + p.InfoHash + "`"));
        var result = await api.SendAsync(HttpMethod.Post, source + "/pulls", new {
            title = "Catalogue submission: " + unique[0].Name.Replace('\r', ' ').Replace('\n', ' '), head, @base = "main",
            body = "Submitted from Build Manager. These packages remain unpublished until the catalogue owner reviews and merges this request.\n\n" + summary +
                "\n\nOnly torrent metadata, hashes and artwork are included. Package files remain with seeders. The submitter must have permission to share the included material."
        }, token);
        return new(Required(result, "html_url"), "Awaiting approval", branch);
    }
}
