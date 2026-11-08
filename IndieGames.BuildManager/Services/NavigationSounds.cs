using System.IO;
using System.Media;
using System.Text;

namespace IndieGames.BuildManager.Services;

/// <summary>Quiet synthesized feedback. Nothing plays until the user opts in.</summary>
public sealed class NavigationSounds : IDisposable
{
    static readonly Lazy<NavigationSounds> SharedInstance = new(() => new NavigationSounds());
    public static NavigationSounds Shared => SharedInstance.Value;
    public static void DisposeShared() { if (SharedInstance.IsValueCreated) SharedInstance.Value.Dispose(); }
    readonly Dictionary<string, (MemoryStream Stream, SoundPlayer Player)> _sounds = new();
    readonly string _setting = Path.Combine(Path.GetDirectoryName(LibraryStorage.SettingsPath)!, "repair-center-sound.txt");
    DateTime _last;
    public bool Enabled { get; private set; }

    public NavigationSounds()
    {
        try { Enabled = File.Exists(_setting) && File.ReadAllText(_setting).Trim() == "on"; }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException) { }
        Add("navigate", new[] { 220.0, 330.0 }, .16);
        Add("select", new[] { 261.63, 392.0 }, .22);
        Add("preview", new[] { 293.66, 440.0, 587.33 }, .38);
        Add("apply", new[] { 220.0, 329.63, 440.0 }, .46);
    }

    public void SetEnabled(bool enabled)
    {
        Enabled = enabled;
        if (!enabled) foreach (var sound in _sounds.Values) sound.Player.Stop();
        try { Directory.CreateDirectory(Path.GetDirectoryName(_setting)!); File.WriteAllText(_setting, enabled ? "on" : "off"); }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException) { }
        if (enabled) Play("preview");
    }

    public void Play(string name)
    {
        if (!Enabled || DateTime.UtcNow - _last < TimeSpan.FromMilliseconds(110) || !_sounds.TryGetValue(name, out var sound)) return;
        _last = DateTime.UtcNow;
        try { sound.Player.Play(); }
        catch (Exception error) when (error is InvalidOperationException or System.TimeoutException or System.ComponentModel.Win32Exception) { }
    }

    void Add(string name, double[] frequencies, double seconds)
    {
        const int rate = 22050;
        int samples = (int)(rate * seconds);
        var stream = new MemoryStream();
        using (var writer = new BinaryWriter(stream, Encoding.ASCII, true))
        {
            writer.Write(Encoding.ASCII.GetBytes("RIFF")); writer.Write(36 + samples * 2);
            writer.Write(Encoding.ASCII.GetBytes("WAVEfmt ")); writer.Write(16); writer.Write((short)1);
            writer.Write((short)1); writer.Write(rate); writer.Write(rate * 2); writer.Write((short)2); writer.Write((short)16);
            writer.Write(Encoding.ASCII.GetBytes("data")); writer.Write(samples * 2);
            for (int i = 0; i < samples; i++)
            {
                double t = (double)i / rate;
                double envelope = Math.Sin(Math.PI * i / samples);
                envelope *= envelope * Math.Exp(-2 * t / seconds);
                double value = frequencies.Sum(f => Math.Sin(2 * Math.PI * f * t) + .18 * Math.Sin(2 * Math.PI * f * 2 * t)) / frequencies.Length;
                writer.Write((short)(short.MaxValue * .10 * envelope * value));
            }
        }
        stream.Position = 0;
        var player = new SoundPlayer(stream);
        try { player.Load(); _sounds[name] = (stream, player); }
        catch (Exception error) when (error is InvalidOperationException or System.TimeoutException) { player.Dispose(); stream.Dispose(); }
    }

    public void Dispose()
    {
        foreach (var sound in _sounds.Values) { sound.Player.Stop(); sound.Player.Dispose(); sound.Stream.Dispose(); }
    }
}
