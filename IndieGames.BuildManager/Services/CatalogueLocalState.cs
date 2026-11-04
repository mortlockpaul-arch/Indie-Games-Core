using System.IO;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

public sealed class CatalogueLocalState
{
    public HashSet<string> Hidden { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, CatalogueSubmission> Submissions { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public static CatalogueLocalState Load(string directory)
    {
        string path = Path.Combine(directory, "catalogue-view.json");
        if (!File.Exists(path)) return new();
        var value = JsonSerializer.Deserialize<CatalogueLocalState>(File.ReadAllText(path)) ?? new();
        value.Hidden = new(value.Hidden, StringComparer.OrdinalIgnoreCase);
        value.Submissions = new(value.Submissions, StringComparer.OrdinalIgnoreCase);
        return value;
    }
    public void Save(string directory)
    {
        Directory.CreateDirectory(directory);
        string path = Path.Combine(directory, "catalogue-view.json");
        string temporary = path + ".tmp-" + Guid.NewGuid().ToString("N");
        try { File.WriteAllText(temporary, JsonSerializer.Serialize(this)); File.Move(temporary, path, true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
    public string Publication(TorrentProject project, IReadOnlySet<string> approved)
        => approved.Contains(project.InfoHash) ? "Approved" : Submissions.TryGetValue(project.InfoHash, out var submission)
            ? submission.State : "Local · not submitted";
}
