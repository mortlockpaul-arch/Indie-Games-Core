using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace IndieGames.BuildManager.Services;

public static class WaveBankConverter
{
    public sealed record Result(string Asset, string Status, string Detail);
    private sealed record Entry(uint Flags, uint Format, uint LoopStart, uint LoopLength);

    public static async Task<IReadOnlyList<Result>> ConvertAsync(string contentDirectory, string decoderExe,
        Action<string> log, CancellationToken token, bool install = true)
    {
        if (!File.Exists(decoderExe)) throw new FileNotFoundException("vgmstream decoder not found.", decoderExe);
        string content = Path.TrimEndingDirectorySeparator(Path.GetFullPath(contentDirectory));
        string staging = Path.Combine(Path.GetDirectoryName(content)!, "wavebank-conversion", Guid.NewGuid().ToString("N"));
        var results = new List<Result>();
        foreach (string source in Directory.EnumerateFiles(content, "*.xwb", SearchOption.AllDirectories))
        {
            token.ThrowIfCancellationRequested();
            string relative = Path.GetRelativePath(content, source);
            try
            {
                string output = Path.Combine(staging, relative);
                bool converted = await ConvertOneAsync(source, output, decoderExe, log, token);
                if (!converted) { results.Add(new(relative, "Skipped", "Already little-endian PCM")); log($"SKIP wave bank: {relative} (already PCM)"); continue; }
                token.ThrowIfCancellationRequested();
                if (install)
                {
                    string backup = source + ".before-pcm-conversion.bak";
                    if (!File.Exists(backup)) File.Copy(source, backup);
                    string temporary = source + ".pcm-conversion-" + Guid.NewGuid().ToString("N");
                    try { File.Copy(output, temporary); File.Move(temporary, source, overwrite: true); }
                    finally { if (File.Exists(temporary)) File.Delete(temporary); }
                }
                results.Add(new(relative, install ? "Converted" : "Staged", output));
                log($"{(install ? "Converted" : "Staged")} wave bank: {relative}");
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception error) { results.Add(new(relative, "Failed", error.Message)); log($"Wave bank conversion failed: {relative}: {error.Message}"); }
        }
        Directory.CreateDirectory(staging);
        string report = Path.Combine(staging, "report.json");
        await File.WriteAllTextAsync(report, JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true }), token);
        log($"Wave bank report: {report}");
        return results;
    }

    private static async Task<bool> ConvertOneAsync(string source, string output, string decoder,
        Action<string> log, CancellationToken token)
    {
        uint version, headerVersion, flags, count, alignment, nameSize;
        byte[] bankName, names, buildTime;
        var entries = new List<Entry>();
        using (var file = File.OpenRead(source))
        using (var reader = new BinaryReader(file))
        {
            string signature = Encoding.ASCII.GetString(reader.ReadBytes(4));
            bool bigEndian = signature == "DNBW";
            if (!bigEndian && signature != "WBND") throw new InvalidDataException("Not an XWB wave bank.");
            uint Read(long offset)
            {
                if (offset < 0 || offset > file.Length - 4) throw new InvalidDataException("Truncated wave bank.");
                file.Position = offset;
                byte[] bytes = reader.ReadBytes(4);
                return bigEndian ? BinaryPrimitives.ReadUInt32BigEndian(bytes) : BinaryPrimitives.ReadUInt32LittleEndian(bytes);
            }
            version = Read(4);
            if (version <= 41 || version > 46) throw new NotSupportedException($"Wave bank version {version} is not supported (expected 42–46).");
            headerVersion = Read(8);
            uint bankOffset = Read(12), bankLength = Read(16), metadataOffset = Read(20), metadataLength = Read(24);
            uint namesOffset = Read(36), namesLength = Read(40);
            void Region(uint offset, uint length)
            {
                if ((ulong)offset + length > (ulong)file.Length) throw new InvalidDataException("Wave bank segment exceeds file length.");
            }
            Region(bankOffset, bankLength); Region(metadataOffset, metadataLength); Region(namesOffset, namesLength);
            if (bankLength < 96) throw new InvalidDataException("Truncated bank metadata.");
            flags = Read(bankOffset); count = Read(bankOffset + 4);
            if (count == 0 || count > 100000) throw new InvalidDataException("Invalid wave count.");
            uint stride = Read(bankOffset + 72);
            nameSize = Read(bankOffset + 76); alignment = Read(bankOffset + 80);
            if ((flags & 0x20000) != 0) throw new NotSupportedException("Compact wave banks are not supported; original preserved.");
            if (stride != 24 || (ulong)count * stride > metadataLength) throw new InvalidDataException("Unsupported or truncated entry metadata.");
            if (alignment < 4) alignment = 4;
            if (alignment > 65536 || (alignment & (alignment - 1)) != 0) throw new InvalidDataException("Invalid wave bank alignment.");
            file.Position = bankOffset + 8; bankName = reader.ReadBytes(64);
            file.Position = bankOffset + 88; buildTime = reader.ReadBytes(8);
            if (bigEndian) Array.Reverse(buildTime);
            if ((flags & 0x10000) != 0 && (nameSize == 0 || (ulong)count * nameSize > namesLength)) throw new InvalidDataException("Invalid entry name table.");
            file.Position = namesOffset; names = reader.ReadBytes(checked((int)namesLength));
            for (int i = 0; i < count; i++)
            {
                long offset = metadataOffset + i * 24L;
                entries.Add(new(Read(offset) & 15, Read(offset + 4), Read(offset + 16), Read(offset + 20)));
            }
            if (!bigEndian && entries.All(e => (e.Format & 3) == 0)) return false;
        }
        Directory.CreateDirectory(Path.GetDirectoryName(output)!);
        string work = output + ".decoded";
        Directory.CreateDirectory(work);
        string audioPath = Path.Combine(work, "pcm.bin");
        using var audio = new FileStream(audioPath, FileMode.Create, FileAccess.ReadWrite);
        using var metadata = new MemoryStream();
        using var entryWriter = new BinaryWriter(metadata, Encoding.UTF8, leaveOpen: true);
        for (int i = 0; i < entries.Count; i++)
        {
            token.ThrowIfCancellationRequested();
            string wave = Path.Combine(work, $"{i + 1:D5}.wav");
            await DecodeAsync(decoder, source, wave, i + 1, token);
            using var input = File.OpenRead(wave);
            using var reader = new BinaryReader(input);
            if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "RIFF") throw new InvalidDataException("Decoder did not produce a RIFF wave.");
            reader.ReadUInt32();
            if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "WAVE") throw new InvalidDataException("Decoder did not produce WAVE audio.");
            byte[]? format = null;
            long dataPosition = 0; uint dataLength = 0;
            while (input.Position + 8 <= input.Length)
            {
                string id = Encoding.ASCII.GetString(reader.ReadBytes(4));
                uint size = reader.ReadUInt32(); long next = input.Position + size + (size & 1);
                if (next > input.Length) throw new InvalidDataException("Truncated decoded WAV chunk.");
                if (id == "fmt ") { if (size > 65536) throw new InvalidDataException("Invalid WAV format block."); format = reader.ReadBytes((int)size); }
                if (id == "data") { dataPosition = input.Position; dataLength = size; }
                input.Position = next;
            }
            if (format == null || format.Length < 16 || dataPosition == 0 || dataLength == 0 ||
                BitConverter.ToUInt16(format, 0) != 1 || BitConverter.ToUInt16(format, 14) != 16)
                throw new InvalidDataException("Decoder must produce nonempty 16-bit PCM audio.");
            uint channels = BitConverter.ToUInt16(format, 2), rate = BitConverter.ToUInt32(format, 4), block = BitConverter.ToUInt16(format, 12);
            if (channels is < 1 or > 7 || rate is < 1 or > 262143 || block != channels * 2 || dataLength % block != 0)
                throw new InvalidDataException("PCM format cannot be represented in an XWB entry.");
            uint samples = dataLength / block;
            Entry entry = entries[i];
            if (samples > 0x0FFFFFFF || (ulong)entry.LoopStart + entry.LoopLength > samples)
                throw new InvalidDataException($"Decoded wave {i + 1} has invalid duration or loop points.");
            Pad(audio, alignment);
            uint offset = checked((uint)audio.Length);
            input.Position = dataPosition;
            byte[] buffer = new byte[81920]; long remaining = dataLength;
            while (remaining > 0)
            {
                token.ThrowIfCancellationRequested();
                int read = await input.ReadAsync(buffer.AsMemory(0, (int)Math.Min(remaining, buffer.Length)), token);
                if (read == 0) throw new EndOfStreamException();
                await audio.WriteAsync(buffer.AsMemory(0, read), token); remaining -= read;
            }
            entryWriter.Write(entry.Flags | (samples << 4));
            entryWriter.Write((channels << 2) | (rate << 5) | (block << 23) | 0x80000000u);
            entryWriter.Write(offset); entryWriter.Write(dataLength);
            entryWriter.Write(entry.LoopStart); entryWriter.Write(entry.LoopLength);
            log($"{Path.GetFileName(source)}: decoded wave {i + 1}/{count}");
        }
        entryWriter.Flush();
        uint metadataOffsetOut = 148, namesOffsetOut = checked(metadataOffsetOut + (uint)metadata.Length);
        uint dataOffset = checked((uint)(((ulong)namesOffsetOut + (uint)names.Length + alignment - 1) / alignment * alignment));
        uint audioLength = checked((uint)audio.Length);
        using (var file = File.Create(output))
        using (var writer = new BinaryWriter(file))
        {
            writer.Write(Encoding.ASCII.GetBytes("WBND")); writer.Write(version); writer.Write(headerVersion);
            foreach (var segment in new[] { (52u, 96u), (metadataOffsetOut, (uint)metadata.Length), (0u, 0u),
                (names.Length == 0 ? 0u : namesOffsetOut, (uint)names.Length), (dataOffset, audioLength) })
            { writer.Write(segment.Item1); writer.Write(segment.Item2); }
            // Preserve streaming type and entry names; PCM needs no codec seek tables.
            writer.Write(flags & ~0xA0000u); writer.Write(count); writer.Write(bankName);
            writer.Write(24u); writer.Write(nameSize); writer.Write(alignment); writer.Write(0u); writer.Write(buildTime);
            writer.Write(metadata.ToArray()); writer.Write(names); writer.Flush(); Pad(file, alignment);
            if (file.Position != dataOffset) throw new InvalidDataException("Incorrect wave-data offset.");
            audio.Position = 0; await audio.CopyToAsync(file, token);
            if (file.Length != (long)dataOffset + audioLength) throw new InvalidDataException("Incorrect converted bank length.");
        }
        return true;
    }

    private static void Pad(Stream stream, uint alignment)
    {
        while (stream.Position % alignment != 0) stream.WriteByte(0);
    }

    private static async Task DecodeAsync(string decoder, string source, string output, int index, CancellationToken token)
    {
        var start = new ProcessStartInfo(decoder) { UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true };
        foreach (string arg in new[] { "-i", "-s", index.ToString(), "-o", output, source }) start.ArgumentList.Add(arg);
        using var process = Process.Start(start) ?? throw new InvalidOperationException("Could not start vgmstream.");
        using var registration = token.Register(() => { try { if (!process.HasExited) process.Kill(entireProcessTree: true); } catch (InvalidOperationException) { } });
        Task<string> stdout = process.StandardOutput.ReadToEndAsync();
        Task<string> stderr = process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync(token);
        string error = await stderr; await stdout;
        token.ThrowIfCancellationRequested();
        if (process.ExitCode != 0 || !File.Exists(output)) throw new InvalidOperationException($"vgmstream failed on wave {index}: {error}");
    }
}
