using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

public static class CatalogueArtwork
{
    private static readonly GameIconConverter Icons = new();

    public static ImageSource? ForProject(TorrentProject project, string archive, string games)
    {
        if (!string.IsNullOrEmpty(project.IconBase64))
        {
            try
            {
                TorrentCatalogue.ValidateIcon(project.IconBase64);
                using var stream = new MemoryStream(Convert.FromBase64String(project.IconBase64));
                var frame = BitmapFrame.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                frame.Freeze(); return frame;
            }
            catch (Exception ex) when (ex is IOException or FormatException or NotSupportedException or ArgumentException) { }
        }
        foreach (string root in new[] { archive, games })
        {
            try
            {
                ProjectBundle.ValidateSegment(project.FolderName);
                if (FromFolder(Path.Combine(root, project.FolderName)) is { } icon) return icon;
            }
            catch (Exception ex) when (ex is IOException or ArgumentException or NotSupportedException) { }
        }
        return null;
    }

    public static ImageSource? FromFolder(string folder)
    {
        if (!Directory.Exists(folder)) return null;
        var candidates = new List<string>();
        try
        {
            foreach (string project in Directory.EnumerateFiles(folder,"*.csproj"))
                candidates.AddRange(XDocument.Load(project).Descendants().Where(e => e.Name.LocalName == "ApplicationIcon")
                    .Select(e => e.Value.Trim().Replace("$(MSBuildProjectDirectory)",folder)).Where(s => !s.Contains("$(")));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Xml.XmlException) { }
        candidates.AddRange(["DashboardIcon.ico", "DashboardIcon.png", "app.ico", "icon.ico"]);
        foreach (string candidate in candidates)
        {
            try
            {
                string path = Path.IsPathRooted(candidate) ? candidate : Path.Combine(folder,candidate);
                if (Icons.Convert(path,typeof(ImageSource),null!,CultureInfo.InvariantCulture) is ImageSource image) return image;
            }
            catch (Exception ex) when (ex is ArgumentException or NotSupportedException) { }
        }
        return null;
    }

    public static string Encode(string folder)
    {
        if (FromFolder(folder) is not { } icon) return "";
        var visual = new DrawingVisual();
        using (var draw = visual.RenderOpen()) draw.DrawImage(icon,new Rect(0,0,48,48));
        var bitmap = new RenderTargetBitmap(48,48,96,96,PixelFormats.Pbgra32); bitmap.Render(visual);
        var png = new PngBitmapEncoder(); png.Frames.Add(BitmapFrame.Create(bitmap));
        using var stream = new MemoryStream(); png.Save(stream);
        string encoded = Convert.ToBase64String(stream.ToArray());
        TorrentCatalogue.ValidateIcon(encoded); return encoded;
    }
}
