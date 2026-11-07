using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using IndieGames.BuildManager.Models;

namespace IndieGames.BuildManager.Services;

public sealed class ProjectStatusStore(string directory, Action<string> reportError)
{
    private readonly Dictionary<string, List<WeakReference<GameProject>>> tracked = new(StringComparer.OrdinalIgnoreCase);
    private bool synchronizing;
    public sealed record Entry(string ProjectPath, string Status, DateTimeOffset? Updated, int OwnerProcessId);

    private string FileFor(string path) => Path.Combine(directory,
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Path.GetFullPath(path).ToUpperInvariant()))) + ".json");

    public void Track(GameProject project)
    {
        try
        {
            string file = FileFor(project.Path);
            if (File.Exists(file) && JsonSerializer.Deserialize<Entry>(File.ReadAllText(file)) is { } entry &&
                Path.GetFullPath(entry.ProjectPath).Equals(Path.GetFullPath(project.Path), StringComparison.OrdinalIgnoreCase))
            {
                string status = entry.Status;
                if (entry.OwnerProcessId != Environment.ProcessId && status.StartsWith("In progress: ", StringComparison.Ordinal))
                    status = "Previous session ended before completion: " + status[13..];
                project.SetStatus(status, entry.Updated);
            }
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException or JsonException or ArgumentException)
        { reportError($"Could not restore status for {project.Name}: {error.Message}"); }
        string key = Path.GetFullPath(project.Path);
        if (!tracked.TryGetValue(key, out var rows)) tracked[key] = rows = [];
        rows.RemoveAll(row => !row.TryGetTarget(out _));
        if (rows.Any(row => row.TryGetTarget(out var target) && ReferenceEquals(target, project))) return;
        rows.Add(new(project));
        project.PropertyChanged += Save;
    }

    public void Untrack(GameProject project) => project.PropertyChanged -= Save;

    private void Save(object? sender, PropertyChangedEventArgs change)
    {
        if (synchronizing || change.PropertyName != nameof(GameProject.Status) || sender is not GameProject project) return;
        synchronizing = true;
        try
        {
            if (tracked.TryGetValue(Path.GetFullPath(project.Path), out var rows))
                foreach (var row in rows)
                    if (row.TryGetTarget(out var target) && !ReferenceEquals(target, project))
                        target.SetStatus(project.Status, project.StatusUpdatedAt);
        }
        finally { synchronizing = false; }
        string? temporary = null;
        try
        {
            Directory.CreateDirectory(directory);
            string file = FileFor(project.Path);
            temporary = file + "." + Guid.NewGuid().ToString("N") + ".tmp";
            File.WriteAllText(temporary, JsonSerializer.Serialize(new Entry(project.Path, project.Status,
                project.StatusUpdatedAt, Environment.ProcessId)));
            File.Move(temporary, file, true);
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException)
        { reportError($"Could not save status for {project.Name}: {error.Message}"); }
        finally
        {
            if (temporary is not null)
                try { File.Delete(temporary); } catch (IOException) { } catch (UnauthorizedAccessException) { }
        }
    }
}
