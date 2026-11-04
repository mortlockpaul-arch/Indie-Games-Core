using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

/// <summary>Uses the configured extractor's decoder without loading its FNA into the UI context.</summary>
public static class SpriteFontConverter
{
    private sealed class ExtractorContext(string assemblyPath) : AssemblyLoadContext(isCollectible: true)
    {
        private readonly AssemblyDependencyResolver _resolver = new(assemblyPath);
        protected override Assembly? Load(AssemblyName name)
        {
            string? path = _resolver.ResolveAssemblyToPath(name);
            return path == null ? null : LoadFromAssemblyPath(path);
        }
    }

    public sealed record Result(string Asset, string Status, string Detail);

    public static IReadOnlyList<Result> ConvertFonts(string contentDirectory, string extractorExe,
        Action<string> log, CancellationToken token, bool install = true)
        => ConvertAssets(contentDirectory, extractorExe, log, token, install, textures: false);

    public static IReadOnlyList<Result> ConvertTextures(string contentDirectory, string extractorExe,
        Action<string> log, CancellationToken token, bool install = true)
        => ConvertAssets(contentDirectory, extractorExe, log, token, install, textures: true);

    private static IReadOnlyList<Result> ConvertAssets(string contentDirectory, string extractorExe,
        Action<string> log, CancellationToken token, bool install, bool textures)
    {
        string kind = textures ? "texture" : "font";
        string readerName = textures ? "Texture2DReader" : "SpriteFontReader";
        string assemblyPath = Path.ChangeExtension(Path.GetFullPath(extractorExe), ".dll");
        if (!File.Exists(assemblyPath)) throw new FileNotFoundException("XnbExtractor.dll must be beside the configured extractor executable.", assemblyPath);
        string content = Path.TrimEndingDirectorySeparator(Path.GetFullPath(contentDirectory));
        string staging = Path.Combine(Path.GetDirectoryName(content)!, kind + "-conversion", Guid.NewGuid().ToString("N"));
        var results = new List<Result>();
        var context = new ExtractorContext(assemblyPath);
        try
        {
            var assembly = context.LoadFromAssemblyPath(assemblyPath);
            var program = assembly.GetType("XnbExtractor.Program", throwOnError: true)!;
            program.GetMethod("RegisterReaders", BindingFlags.NonPublic | BindingFlags.Static)!.Invoke(null, null);
            var load = program.GetNestedType("XnbCore", BindingFlags.Public | BindingFlags.NonPublic)!.GetMethod("LoadAsset")!;
            var xnbType = assembly.GetType("XnbExtractor.Xnb.XnbFile", throwOnError: true)!;
            foreach (string source in Directory.EnumerateFiles(content, "*.xnb", SearchOption.AllDirectories))
            {
                token.ThrowIfCancellationRequested();
                string relative = Path.GetRelativePath(content, source);
                try
                {
                    // Decode the reader table first: filenames do not determine asset types.
                    byte[] header = new byte[6];
                    using (var stream = File.OpenRead(source)) stream.ReadExactly(header);
                    if (header[0] != 'X' || header[1] != 'N' || header[2] != 'B') continue;
                    using var xnb = (IDisposable)Activator.CreateInstance(xnbType, source)!;
                    byte[] payload = (byte[])xnbType.GetMethod("Decompress")!.Invoke(xnb, null)!;
                    using var reader = new BinaryReader(new MemoryStream(payload));
                    int count = reader.Read7BitEncodedInt();
                    if (count < 1 || count > 1024) throw new InvalidDataException("Invalid reader count.");
                    var readers = new List<string>();
                    for (int i = 0; i < count; i++) { readers.Add(reader.ReadString()); reader.ReadInt32(); }
                    reader.Read7BitEncodedInt(); // shared resource count
                    int primary = reader.Read7BitEncodedInt();
                    if (primary < 1 || primary > count || readers[primary - 1].Split(',')[0].Trim() != "Microsoft.Xna.Framework.Content." + readerName) continue;
                    string backup = source + $".before-{kind}-conversion.bak";
                    string decodeSource = source;
                    if (header[3] == 'w' && header[4] == 5 && File.Exists(backup))
                    {
                        byte[] originalHeader = new byte[6];
                        using (var original = File.OpenRead(backup)) original.ReadExactly(originalHeader);
                        if (originalHeader[0] == 'X' && originalHeader[1] == 'N' && originalHeader[2] == 'B' && originalHeader[3] == 'x')
                        {
                            decodeSource = backup;
                            header = originalHeader;
                        }
                    }
                    if (header[3] == 'w' && header[4] == 5)
                    {
                        results.Add(new(relative, "Skipped", "Already Windows XNB version 5"));
                        log($"SKIP {kind}: {relative} (already Windows version 5)");
                        continue;
                    }
                    object loaded = load.Invoke(null, [decodeSource])!;
                    object asset = Member(loaded, "Asset");
                    if (header[3] == 'x') NormalizeXboxTexture(textures ? asset : Member(asset, "Texture"), header[4]);
                    byte[] converted = textures ? WriteTexture(asset) : WriteFont(asset, legacyStraightAlpha: header[4] < 5);
                    if (File.ReadAllBytes(source).AsSpan().SequenceEqual(converted))
                    {
                        results.Add(new(relative, "Skipped", "Conversion already up to date"));
                        log($"SKIP {kind}: {relative} (conversion already up to date)");
                        continue;
                    }
                    string output = Path.Combine(staging, relative);
                    Directory.CreateDirectory(Path.GetDirectoryName(output)!);
                    File.WriteAllBytes(output, converted);
                    token.ThrowIfCancellationRequested();
                    if (install)
                    {
                        if (!File.Exists(backup)) File.Copy(source, backup);
                        // Copy beside the destination first, so a failed conversion never truncates it.
                        string temporary = source + $".{kind}-conversion-" + Guid.NewGuid().ToString("N");
                        try { File.Copy(output, temporary); File.Move(temporary, source, overwrite: true); }
                        finally { if (File.Exists(temporary)) File.Delete(temporary); }
                    }
                    results.Add(new(relative, install ? "Converted" : "Staged", output));
                    log($"{(install ? "Converted" : "Staged")} {kind}: {relative}");
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception error)
                {
                    string detail = error.GetBaseException().Message;
                    results.Add(new(relative, "Failed", detail));
                    log($"{kind} inspection/conversion failed: {relative}: {detail}");
                }
            }
            Directory.CreateDirectory(staging);
            string report = Path.Combine(staging, "report.json");
            File.WriteAllText(report, JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true }));
            log($"{kind} report: {report}");
            return results;
        }
        finally { context.Unload(); }
    }

    private static object Member(object value, string name) =>
        value.GetType().GetProperty(name)?.GetValue(value) ??
        value.GetType().GetField(name)?.GetValue(value) ??
        throw new InvalidDataException($"Missing extractor member: {value.GetType().Name}.{name}");

    private static object[] Items(object value, string name) => ((IEnumerable)Member(value, name)).Cast<object>().ToArray();

    // Match FNA's Texture2DReader/X360TexUtil. XNB image payloads are linear;
    // Xbox byte order still applies even though scalar XNB fields are LE.
    private static void NormalizeXboxTexture(object texture, byte version)
    {
        int raw = (int)Member(texture, "RawFormat");
        bool legacyBgra = version < 5 && raw == 1;
        int format = version < 5 ? raw switch
        {
            1 => 0, 28 => 4, 30 => 5, 32 => 6,
            _ => throw new NotSupportedException($"Unsupported legacy Xbox texture format {raw}.")
        } : raw;
        var property = texture.GetType().GetProperty("WindowsFormat")!;
        Type enumType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
        property.SetValue(texture, Enum.ToObject(enumType, format));
        int width = (int)Member(texture, "Width"), height = (int)Member(texture, "Height");
        var mips = Items(texture, "MipData");
        for (int level = 0; level < mips.Length; level++)
        {
            byte[] data = (byte[])mips[level];
            int w = Math.Max(1, width >> level), h = Math.Max(1, height >> level);
            long expected = format switch
            {
                0 => (long)w * h * 4,
                4 => (long)((w + 3) / 4) * ((h + 3) / 4) * 8,
                5 or 6 => (long)((w + 3) / 4) * ((h + 3) / 4) * 16,
                _ => data.Length
            };
            if (data.Length != expected) throw new InvalidDataException($"Unexpected Xbox mip length: expected {expected}, got {data.Length}.");
            if (format == 0)
            {
                for (int i = 0; i < data.Length; i += 4)
                {
                    byte a = data[i], b = data[i + 1], c = data[i + 2], d = data[i + 3];
                    // Legacy ARGB -> RGBA; XNA4 ABGR -> RGBA.
                    data[i] = legacyBgra ? b : d; data[i + 1] = c;
                    data[i + 2] = legacyBgra ? d : b; data[i + 3] = a;
                }
            }
            else if (format is 4 or 5 or 6)
            {
                // DXT colour endpoints, indices and alpha are stored in BE words.
                for (int i = 0; i < data.Length; i += 2) (data[i], data[i + 1]) = (data[i + 1], data[i]);
            }
        }
    }

    private static void WriteTexturePayload(BinaryWriter writer, object texture)
    {
        var mips = Items(texture, "MipData");
        int mipCount = System.Convert.ToInt32(Member(texture, "MipCount"));
        int width = System.Convert.ToInt32(Member(texture, "Width"));
        int height = System.Convert.ToInt32(Member(texture, "Height"));
        if (mips.Length != mipCount || mipCount < 1 || width < 1 || height < 1)
            throw new InvalidDataException("Invalid texture dimensions or mip count.");
        writer.Write(System.Convert.ToInt32(Member(texture, "WindowsFormat")));
        writer.Write(width); writer.Write(height); writer.Write(mipCount);
        foreach (byte[] mip in mips)
        {
            if (mip.Length == 0) throw new InvalidDataException("Empty texture mip.");
            writer.Write(mip.Length); writer.Write(mip);
        }
    }

    private static byte[] WriteTexture(object texture)
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
        writer.Write(new byte[] { 88, 78, 66, 119, 5, 0 });
        writer.Write(0);
        writer.Write7BitEncodedInt(1);
        writer.Write("Microsoft.Xna.Framework.Content.Texture2DReader, FNA");
        writer.Write(0);
        writer.Write7BitEncodedInt(0);
        writer.Write7BitEncodedInt(1);
        WriteTexturePayload(writer, texture);
        writer.Flush();
        int length = checked((int)stream.Length);
        stream.Position = 6; writer.Write(length); writer.Flush();
        return stream.ToArray();
    }

    private static byte[] WriteFont(object font, bool legacyStraightAlpha)
    {
        var glyphs = Items(font, "Glyphs");
        var cropping = Items(font, "Cropping");
        var characters = Items(font, "Characters");
        var kerning = Items(font, "Kerning");
        if (characters.Length == 0 || glyphs.Length != characters.Length || cropping.Length != characters.Length || kerning.Length != characters.Length)
            throw new InvalidDataException("Font character, glyph, cropping and kerning counts must match.");
        object texture = Member(font, "Texture");
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true);
        static string Reader(string name) => "Microsoft.Xna.Framework.Content." + name + ", FNA";
        string[] readers = [Reader("SpriteFontReader"), Reader("Texture2DReader"),
            Reader("ListReader`1[[Microsoft.Xna.Framework.Rectangle, FNA]]"),
            Reader("ListReader`1[[System.Char, mscorlib]]"),
            Reader("ListReader`1[[Microsoft.Xna.Framework.Vector3, FNA]]"),
            Reader("RectangleReader"), Reader("CharReader"), Reader("Vector3Reader")];
        writer.Write(new byte[] { 88, 78, 66, 119, 5, 0 });
        writer.Write(0); // patched with final file length
        writer.Write7BitEncodedInt(readers.Length);
        foreach (string reader in readers) { writer.Write(reader); writer.Write(0); }
        writer.Write7BitEncodedInt(0); // shared resources
        writer.Write7BitEncodedInt(1); // SpriteFont
        writer.Write7BitEncodedInt(2); // embedded Texture2D
        if (legacyStraightAlpha)
        {
            var mips = Items(texture, "MipData");
            int width = (int)Member(texture, "Width"), height = (int)Member(texture, "Height");
            int format = System.Convert.ToInt32(Member(texture, "WindowsFormat"));
            writer.Write(0); writer.Write(width); writer.Write(height); writer.Write(mips.Length);
            for (int level = 0; level < mips.Length; level++)
            {
                byte[] rgba = LegacyFontAtlas.DecodePremultiplied(format, Math.Max(1, width >> level), Math.Max(1, height >> level), (byte[])mips[level]);
                writer.Write(rgba.Length); writer.Write(rgba);
            }
        }
        else WriteTexturePayload(writer, texture);
        void Rectangles(object[] rectangles)
        {
            writer.Write7BitEncodedInt(3);
            writer.Write(rectangles.Length);
            foreach (object rectangle in rectangles)
                foreach (string name in new[] { "X", "Y", "Width", "Height" }) writer.Write((int)Member(rectangle, name));
        }
        Rectangles(glyphs); Rectangles(cropping);
        writer.Write7BitEncodedInt(4); writer.Write(characters.Length);
        foreach (char character in characters) writer.Write(character);
        writer.Write((int)Member(font, "LineSpacing")); writer.Write((float)Member(font, "Spacing"));
        writer.Write7BitEncodedInt(5); writer.Write(kerning.Length);
        foreach (object vector in kerning)
            foreach (string name in new[] { "X", "Y", "Z" }) writer.Write((float)Member(vector, name));
        object? fallback = font.GetType().GetProperty("DefaultCharacter")!.GetValue(font);
        writer.Write(fallback != null); if (fallback != null) writer.Write((char)fallback);
        writer.Flush();
        int length = checked((int)stream.Length);
        stream.Position = 6; writer.Write(length); writer.Flush();
        return stream.ToArray();
    }
}
