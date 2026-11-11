using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Mono.Cecil;

internal static class Program
{
    static IEnumerable<TypeDefinition> Walk(TypeDefinition t) { yield return t; foreach (var n in t.NestedTypes) foreach (var x in Walk(n)) yield return x; }
    static IEnumerable<FieldDefinition> Fields(AssemblyDefinition a) => a.MainModule.Types.SelectMany(Walk).SelectMany(t => t.Fields);
    static string Key(FieldDefinition f) => f.DeclaringType.FullName + "::" + f.Name;
    static int Main(string[] args)
    {
        if (args.Length != 4) { Console.Error.WriteLine("Usage: FieldRvaRestorer SANITIZED REBUILT OUTPUT REPORT"); return 2; }
        var rows = new List<object>(); var errors = new List<string>();
        try
        {
            var srcPath = Path.GetFullPath(args[0]); var rebuiltPath = Path.GetFullPath(args[1]);
            var outPath = Path.GetFullPath(args[2]); var reportPath = Path.GetFullPath(args[3]);
            if (new[] { srcPath, rebuiltPath, reportPath }.Any(p => string.Equals(p, outPath, StringComparison.OrdinalIgnoreCase))) throw new Exception("Output path overlaps an input/report path");
            using var source = AssemblyDefinition.ReadAssembly(srcPath, new ReaderParameters { ReadSymbols = false, InMemory = true });
            using var target = AssemblyDefinition.ReadAssembly(rebuiltPath, new ReaderParameters { ReadSymbols = false, InMemory = true });
            var originals = Fields(source).Where(f => f.HasFieldRVA).ToList();
            var targets = Fields(target).GroupBy(Key).ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);
            int added = 0, identical = 0, existingOther = 0, bytesAdded = 0;
            var originalKeys = new HashSet<string>(originals.Select(Key), StringComparer.Ordinal);
            foreach (var f in Fields(target).Where(f => f.HasFieldRVA && !originalKeys.Contains(Key(f))))
            {
                existingOther++;
                rows.Add(new { field = Key(f), status = "compiler-generated-or-unmatched", bytes = f.InitialValue?.Length ?? 0 });
            }
            foreach (var f in originals)
            {
                var key = Key(f); var data = f.InitialValue;
                if (data == null || data.Length == 0) { errors.Add(key + ": source initializer unavailable"); continue; }
                if (!targets.TryGetValue(key, out var matches) || matches.Count != 1) { errors.Add(key + ": missing or ambiguous rebuilt field"); continue; }
                var dest = matches[0];
                if (!f.IsStatic || !dest.IsStatic || f.FieldType.FullName != dest.FieldType.FullName)
                { errors.Add(key + ": static/type mismatch"); continue; }
                if (dest.HasFieldRVA)
                {
                    if (dest.InitialValue == null || !dest.InitialValue.SequenceEqual(data))
                    { errors.Add(key + ": conflicting existing initializer; refusing overwrite"); continue; }
                    identical++;
                    rows.Add(new { field = key, status = "already-identical", bytes = data.Length });
                    continue;
                }
                dest.InitialValue = (byte[])data.Clone();
                dest.HasFieldRVA = true;
                added++; bytesAdded += data.Length;
                rows.Add(new { field = key, status = "restored", bytes = data.Length });
            }
            if (originals.Count == 0) errors.Add("Source contains no initialized FieldRVA fields");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);
            void Report(bool verified) => File.WriteAllText(reportPath, JsonSerializer.Serialize(new {
                sanitized = srcPath, rebuilt = rebuiltPath, output = outPath,
                originalFieldRva = originals.Count, originalBytes = originals.Sum(f => f.InitialValue?.Length ?? 0),
                rebuiltFieldRva = Fields(target).Count(f => f.HasFieldRVA) - added,
                alreadyIdentical = identical, added, bytesAdded, unrelatedExisting = existingOther,
                outputFieldRva = Fields(target).Count(f => f.HasFieldRVA),
                verified, errors, fields = rows,
                note = "Extra compiler-generated FieldRVA entries are preserved, not assumed duplicates. Runtime behavior remains unverified."
            }, new JsonSerializerOptions { WriteIndented = true }));
            if (errors.Count != 0) { Report(false); foreach (var e in errors) Console.Error.WriteLine("ERROR: " + e); Console.Error.WriteLine("Refusing to write output. Report: " + reportPath); return 3; }
            if (File.Exists(outPath)) throw new IOException("Output already exists: " + outPath);
            Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);
            target.Write(outPath, new WriterParameters { WriteSymbols = false });
            using var checkedAssembly = AssemblyDefinition.ReadAssembly(outPath, new ReaderParameters { ReadSymbols = false, InMemory = true });
            var checkedFields = Fields(checkedAssembly).GroupBy(Key).ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);
            foreach (var f in originals)
            {
                if (!checkedFields.TryGetValue(Key(f), out var candidates) || candidates.Count != 1 || !candidates[0].HasFieldRVA || !candidates[0].InitialValue.SequenceEqual(f.InitialValue))
                    errors.Add(Key(f) + ": post-write verification failed");
            }
            if (errors.Count > 0) { File.Delete(outPath); Report(false); return 4; }
            Report(true);
            Console.WriteLine($"Verified {originals.Count} source fields: {added} restored, {identical} already identical, {existingOther} other rebuilt fields retained. Output: {outPath}");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }
}
