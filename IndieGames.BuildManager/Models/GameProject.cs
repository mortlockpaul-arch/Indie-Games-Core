using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace IndieGames.BuildManager.Models;
public sealed class GameProject : INotifyPropertyChanged
{
    public string Name { get; init; } = "";
    public string Path { get; init; } = "";
    public string IconPath { get; init; } = "";
    public string TargetFramework { get; init; } = "";
    private string _platformTarget = "", _platforms = "", _runtimeIdentifier = "";
    public string PlatformTarget { get => _platformTarget; set { _platformTarget = value; PropertyChanged?.Invoke(this, new(nameof(PlatformTarget))); } }
    public string Platforms { get => _platforms; set { _platforms = value; PropertyChanged?.Invoke(this, new(nameof(Platforms))); } }
    public string RuntimeIdentifier { get => _runtimeIdentifier; set { _runtimeIdentifier = value; PropertyChanged?.Invoke(this, new(nameof(RuntimeIdentifier))); } }
    public string OutputType { get; init; } = "";
    private string _buildArchitecture = "";
    public string BuildArchitecture { get => _buildArchitecture; set { _buildArchitecture = value; PropertyChanged?.Invoke(this, new(nameof(BuildArchitecture))); } }
    private bool _isInSolution;
    public bool IsInSolution { get => _isInSolution; set { if (_isInSolution == value) return; _isInSolution=value; PropertyChanged?.Invoke(this,new(nameof(IsInSolution))); } }
    private string _status = "No build or run recorded";
    public DateTimeOffset? StatusUpdatedAt { get; private set; }
    public string StatusDisplay => StatusUpdatedAt is { } updated ? $"{Status} · {updated.ToLocalTime():g}" : Status;
    public string Status { get => _status; set => SetStatus(value, DateTimeOffset.UtcNow); }
    public void SetStatus(string status, DateTimeOffset? updated)
    {
        _status = status;
        StatusUpdatedAt = updated;
        PropertyChanged?.Invoke(this, new(nameof(StatusDisplay)));
        PropertyChanged?.Invoke(this, new(nameof(Status)));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}
