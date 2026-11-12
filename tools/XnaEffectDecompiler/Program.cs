using HlslDecompiler.DirectXShaderModel;
using HlslDecompiler.Hlsl;
using System.Buffers.Binary;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

if (args.Length < 2)
{
    Console.Error.WriteLine("XnaEffectDecompiler <input.xnb|input.effect.bin> <output.fx> [--preserve-centroid] [--force]");
    return 2;
}

try
{
    string input = Path.GetFullPath(args[0]), output = Path.GetFullPath(args[1]);
    if (input.Equals(output, StringComparison.OrdinalIgnoreCase))
        throw new IOException("Input and output must differ.");
    if (File.Exists(output) && !args.Contains("--force"))
        throw new IOException("Output exists; use --force to replace it.");
    byte[] data = File.ReadAllBytes(input);
    if (data.Length >= 10 && Encoding.ASCII.GetString(data, 0, 3) == "XNB")
    {
        if (BitConverter.ToInt32(data, 6) != data.Length)
            throw new InvalidDataException("XNB length mismatch");
        byte[] payload;
        if ((data[5] & 0x80) != 0)
        {
            int size = BitConverter.ToInt32(data, 10);
            if (size < 0 || size > 256 * 1024 * 1024)
                throw new InvalidDataException("Invalid decoded size");
            using var packed = new MemoryStream(data, 14, data.Length - 14);
            using var decoded = new MemoryStream();
            var type = typeof(Microsoft.Xna.Framework.Color).Assembly.GetType("Microsoft.Xna.Framework.Content.LzxDecoder")!;
            var decoder = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[] { 16 }, null)!;
            var method = type.GetMethod("Decompress", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;
            while (packed.Position < packed.Length && decoded.Length < size)
            {
                int hi = packed.ReadByte(), lo = packed.ReadByte(), frame = 32768, block = (hi << 8) | lo;
                if (hi == 255)
                {
                    frame = (lo << 8) | packed.ReadByte();
                    block = (packed.ReadByte() << 8) | packed.ReadByte();
                }

                if (block <= 0 || frame <= 0)
                    break;
                long end = packed.Position + block;
                if (end > packed.Length)
                    throw new InvalidDataException("Invalid LZX block");
                var result = method.Invoke(decoder, new object[] { packed, block, decoded, frame });
                if (result is int code && code != 0)
                    throw new InvalidDataException("LZX decode failed");
                packed.Position = end;
            }

            payload = decoded.ToArray();
            if (payload.Length != size)
                throw new InvalidDataException("Decoded length mismatch");
        }
        else
            payload = data[10..];
        using var r = new BinaryReader(new MemoryStream(payload));
        int count = r.Read7BitEncodedInt();
        if (count < 1 || count > 64)
            throw new InvalidDataException("Invalid reader count");
        var readers = new List<string>();
        for (int i = 0; i < count; i++)
        {
            readers.Add(r.ReadString());
            r.ReadInt32();
        }

        if (r.Read7BitEncodedInt() != 0)
            throw new InvalidDataException("Shared resources are unsupported");
        int index = r.Read7BitEncodedInt();
        if (index < 1 || index > count)
            throw new InvalidDataException("Invalid effect reader index");

        string readerName = readers[index - 1];

        if (readerName != "Microsoft.Xna.Framework.Content.EffectReader" &&
            !readerName.StartsWith(
                "Microsoft.Xna.Framework.Content.EffectReader,",
                StringComparison.Ordinal))
        {
            throw new InvalidDataException(
                "Not an effect XNB: " + string.Join("; ", readers));
        }
        int length = r.ReadInt32();
        if (length < 0 || length > payload.Length - r.BaseStream.Position)
            throw new InvalidDataException("Invalid effect length");
        data = r.ReadBytes(length);
    }

    if (data.Length >= 8 && BinaryPrimitives.ReadUInt32BigEndian(data) == EffectReader.Fx20)
        data = Convert.FromHexString("BCF00BCF000000100000000000000000").Concat(data).ToArray();
    using var stream = new MemoryStream(data);
    using var reader = new EffectReader(stream);
    var effect = reader.ReadXboxEffect();
    using var text = new StringWriter();
    new D3D9EffectWriter(effect, false, false, true).Write(text);
    string original = text.ToString();
    bool adapt = original.Contains("centroid ") && !args.Contains("--preserve-centroid");
    string hlsl = adapt ? "// FNA SDL_GPU compatibility: centroid replaced by standard interpolation.\n" + Regex.Replace(original, @"\bcentroid\s+", "") : original;
    Directory.CreateDirectory(Path.GetDirectoryName(output)!);
    if (adapt)
        File.WriteAllText(Path.ChangeExtension(output, "original.fx"), original);
    File.WriteAllText(output, hlsl);
    File.WriteAllText(Path.ChangeExtension(output, "metadata.json"), JsonSerializer.Serialize(new { input, upstream = "db004b9094e9d7ef1564ad1815e738d2273b2325", centroidAdapted = adapt, parameters = effect.Parameters.Select(p => new { p.Name, type = p.Type.ToString(), typeClass = p.Class.ToString(), p.Rows, p.Columns, p.Elements }), techniques = effect.Techniques.Select(t => new { t.Name, passes = t.Passes.Select(p => p.Name) }) }, new JsonSerializerOptions { WriteIndented = true }));
    Console.WriteLine("Recovered " + output + (adapt ? " (standard interpolation; original saved)" : ""));
    return 0;
}
catch (Exception e)
{
    Console.Error.WriteLine(e.ToString());
    return 1;
}
