using System.Buffers.Binary;
using System.IO;

namespace IndieGames.BuildManager.Services;

/// <summary>Converts normalized legacy XNA font pixels to premultiplied RGBA.</summary>
public static class LegacyFontAtlas
{
    public static byte[] DecodePremultiplied(int format, int width, int height, byte[] data)
    {
        if (width < 1 || height < 1) throw new InvalidDataException("Invalid font dimensions.");
        byte[] rgba = new byte[checked(width * height * 4)];
        if (format == 0)
        {
            if (data.Length != rgba.Length) throw new InvalidDataException("Invalid RGBA font length.");
            data.CopyTo(rgba, 0);
        }
        else if (format is 4 or 5 or 6)
        {
            int blockSize = format == 4 ? 8 : 16;
            int columns = (width + 3) / 4, rows = (height + 3) / 4;
            if (data.Length != checked(columns * rows * blockSize)) throw new InvalidDataException("Invalid compressed font length.");
            for (int by = 0; by < rows; by++) for (int bx = 0; bx < columns; bx++)
            {
                var block = data.AsSpan((by * columns + bx) * blockSize, blockSize);
                int colorStart = format == 4 ? 0 : 8;
                int first = BinaryPrimitives.ReadUInt16LittleEndian(block[colorStart..]);
                int second = BinaryPrimitives.ReadUInt16LittleEndian(block[(colorStart + 2)..]);
                byte[][] colors = [Rgb565(first), Rgb565(second), new byte[3], new byte[3]];
                bool transparentColor = format == 4 && first <= second;
                for (int channel = 0; channel < 3; channel++)
                {
                    colors[2][channel] = (byte)(transparentColor ? (colors[0][channel] + colors[1][channel]) / 2 : (2 * colors[0][channel] + colors[1][channel]) / 3);
                    colors[3][channel] = transparentColor ? (byte)0 : (byte)((colors[0][channel] + 2 * colors[1][channel]) / 3);
                }
                uint indices = BinaryPrimitives.ReadUInt32LittleEndian(block[(colorStart + 4)..]);
                byte[] alphas = new byte[8]; ulong alphaBits = 0;
                if (format == 5) alphaBits = BinaryPrimitives.ReadUInt64LittleEndian(block);
                if (format == 6)
                {
                    alphas[0] = block[0]; alphas[1] = block[1];
                    for (int i = 2; i < (alphas[0] > alphas[1] ? 8 : 6); i++)
                        alphas[i] = (byte)(alphas[0] > alphas[1] ? ((8 - i) * alphas[0] + (i - 1) * alphas[1]) / 7 : ((6 - i) * alphas[0] + (i - 1) * alphas[1]) / 5);
                    if (alphas[0] <= alphas[1]) { alphas[6] = 0; alphas[7] = 255; }
                    for (int i = 0; i < 6; i++) alphaBits |= (ulong)block[i + 2] << (8 * i);
                }
                for (int pixel = 0; pixel < 16; pixel++)
                {
                    int x = bx * 4 + pixel % 4, y = by * 4 + pixel / 4;
                    if (x >= width || y >= height) continue;
                    int color = (int)((indices >> (2 * pixel)) & 3), offset = (y * width + x) * 4;
                    rgba[offset] = colors[color][0]; rgba[offset + 1] = colors[color][1]; rgba[offset + 2] = colors[color][2];
                    rgba[offset + 3] = format == 5 ? (byte)(((alphaBits >> (4 * pixel)) & 15) * 17)
                        : format == 6 ? alphas[(alphaBits >> (3 * pixel)) & 7]
                        : transparentColor && color == 3 ? (byte)0 : (byte)255;
                }
            }
        }
        else throw new NotSupportedException("Unsupported legacy font format " + format);
        for (int i = 0; i < rgba.Length; i += 4)
            for (int channel = 0; channel < 3; channel++) rgba[i + channel] = (byte)((rgba[i + channel] * rgba[i + 3] + 127) / 255);
        return rgba;
    }
    static byte[] Rgb565(int value) => [(byte)(((value >> 11) & 31) * 255 / 31), (byte)(((value >> 5) & 63) * 255 / 63), (byte)((value & 31) * 255 / 31)];
}
