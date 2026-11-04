using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace IndieGames.BuildManager.Services;

public sealed record SnapshotFile(long Bytes, string Sha256);
public sealed record SnapshotContents(string Fingerprint, IReadOnlyDictionary<string, SnapshotFile> Files)
{
    public static async Task<SnapshotContents> ReadAsync(string package, CancellationToken token)
    {
        using var zip = ZipFile.OpenRead(package);
        var files = new Dictionary<string, SnapshotFile>(StringComparer.Ordinal);
        using var fingerprint = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        foreach (var entry in zip.Entries.Where(e => e.Name.Length > 0).OrderBy(e => e.FullName, StringComparer.Ordinal))
        {
            token.ThrowIfCancellationRequested();
            string name = entry.FullName.Replace('\\', '/');
            if (entry.Length > 100L * 1024 * 1024 * 1024 || files.Count >= 100000) throw new InvalidDataException("Snapshot exceeds content limits.");
            await using var input = entry.Open(); byte[] hash = await SHA256.HashDataAsync(input, token);
            if (!files.TryAdd(name, new(entry.Length, Convert.ToHexString(hash)))) throw new InvalidDataException("Duplicate file in snapshot.");
            using var descriptor = new MemoryStream();
            using (var writer = new BinaryWriter(descriptor, Encoding.UTF8, leaveOpen: true))
            { writer.Write(name); writer.Write(entry.Length); writer.Write(hash); }
            fingerprint.AppendData(descriptor.ToArray());
        }
        return new(Convert.ToHexString(fingerprint.GetHashAndReset()), files);
    }
}
