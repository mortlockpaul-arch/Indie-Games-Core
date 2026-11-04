using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IndieGames.BuildManager.Services;

public sealed class GameIconConverter : IValueConverter
{
    private readonly Dictionary<string, (DateTime Modified, ImageSource Image)> _cache = new(StringComparer.OrdinalIgnoreCase);

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string path || string.IsNullOrWhiteSpace(path)) return null;
        try
        {
            DateTime modified = File.GetLastWriteTimeUtc(path);
            if (_cache.TryGetValue(path, out var cached) && cached.Modified == modified) return cached.Image;
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            var frame = decoder.Frames.OrderBy(f => f.PixelWidth >= 40 ? 0 : 1)
                .ThenBy(f => Math.Abs(f.PixelWidth - 40)).First();
            frame.Freeze();
            _cache[path] = (modified, frame);
            return frame;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or NotSupportedException or ArgumentException or FileFormatException)
        {
            return null;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
