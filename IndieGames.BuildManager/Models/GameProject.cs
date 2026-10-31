using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace IndieGames.BuildManager.Models;
public sealed class GameProject : INotifyPropertyChanged
{
    public string Name { get; init; } = "";
    public string Path { get; init; } = "";
    public string TargetFramework { get; init; } = "";
    public string PlatformTarget { get; init; } = "";
    public string Platforms { get; init; } = "";
    public string RuntimeIdentifier { get; init; } = "";
    public string OutputType { get; init; } = "";
    private string _status = "Not built";
    public string Status { get => _status; set { _status = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status))); } }
    public event PropertyChangedEventHandler? PropertyChanged;
}
