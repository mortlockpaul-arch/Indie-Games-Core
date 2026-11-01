using Mono.Cecil;
using System.Collections.Generic;

internal static class Program
{
    private static readonly HashSet<string> FnaNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "Microsoft.Xna.Framework", "Microsoft.Xna.Framework.Game", "Microsoft.Xna.Framework.Graphics",
        "Microsoft.Xna.Framework.Audio", "Microsoft.Xna.Framework.Content", "Microsoft.Xna.Framework.Input",
        "Microsoft.Xna.Framework.Xact"
    };
    private static readonly HashSet<string> StubNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "Microsoft.Xna.Framework.GamerServices", "Microsoft.Xna.Framework.Net",
        "Microsoft.Xna.Framework.Storage", "Microsoft.Xna.Framework.Avatar"
    };

    private static int Main(string[] args)
    {
        try
        {
            var opts = Parse(args);
            var mode = opts.GetValueOrDefault("mode", "scan").ToLowerInvariant();
            if (mode is not ("scan" or "patch" or "verify")) throw new ArgumentException("--mode must be scan, patch, or verify");
            if (!opts.TryGetValue("input", out var input)) throw new ArgumentException("Missing --input <dll-or-folder>");
            var files = Directory.Exists(input)
                ? Directory.EnumerateFiles(input, "*.dll", SearchOption.TopDirectoryOnly).ToArray()
                : new[] { Path.GetFullPath(input) };
            if (files.Length == 0) { Console.WriteLine("No DLLs found"); return 0; }
            string? fnaPath = opts.GetValueOrDefault("fna");
            string? stubPath = opts.GetValueOrDefault("stub");
            string? output = opts.GetValueOrDefault("output");
            if (mode == "patch" && (string.IsNullOrWhiteSpace(fnaPath) || string.IsNullOrWhiteSpace(output)))
                throw new ArgumentException("Patch requires --fna <FNA.dll> and --output <folder>");

            var fnaTypes = LoadTypes(fnaPath);
            var stubTypes = LoadTypes(stubPath);
            int failures = 0, patched = 0;
            foreach (var file in files)
            {
                try
                {
                    using var assembly = AssemblyDefinition.ReadAssembly(file, new ReaderParameters { InMemory = true });
                    var module = assembly.MainModule;
                    var xna = module.AssemblyReferences.Where(r => r.Name.StartsWith("Microsoft.Xna.Framework", StringComparison.OrdinalIgnoreCase)).ToArray();
                    Console.WriteLine($"[{Path.GetFileName(file)}] XNA references: {xna.Length}");
                    foreach (var r in xna) Console.WriteLine($"  {r.FullName}");
                    if (mode == "scan" || xna.Length == 0) continue;
                    var unsupported = new SortedSet<string>(StringComparer.Ordinal);

                    var replacementAssemblies =
                        new List<(AssemblyNameReference assembly, HashSet<string> types)>();

                    if (fnaTypes != null)
                        replacementAssemblies.Add(fnaTypes.Value);

                    if (stubTypes != null)
                        replacementAssemblies.Add(stubTypes.Value);

                    // Maps each original XNA type reference to its replacement assembly.
                    var typeMappings =
                        new Dictionary<TypeReference, AssemblyNameReference>();

                    var typeRefs = module.GetTypeReferences().ToArray();

                    foreach (var tr in typeRefs)
                    {
                        var top = tr;

                        while (top.DeclaringType != null)
                            top = top.DeclaringType;

                        if (top.Scope is not AssemblyNameReference original ||
                            !original.Name.StartsWith(
                                "Microsoft.Xna.Framework",
                                StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        // Discover which replacement assembly actually defines the type.
                        var matches = replacementAssemblies
                            .Where(candidate => candidate.types.Contains(top.FullName))
                            .ToArray();

                        if (matches.Length == 0)
                        {
                            unsupported.Add(
                                $"Missing replacement for {top.FullName} " +
                                $"(original: {original.Name})");

                            continue;
                        }

                        if (matches.Length > 1)
                        {
                            unsupported.Add(
                                $"Ambiguous replacement for {top.FullName}: " +
                                string.Join(", ", matches.Select(m => m.assembly.Name)));

                            continue;
                        }

                        typeMappings[top] = matches[0].assembly;
                    }

                    if (unsupported.Count > 0)
                    {
                        foreach (var item in unsupported.Take(30))
                            Console.WriteLine($"  UNSUPPORTED: {item}");

                        Console.WriteLine(
                            $"  SKIPPED: {unsupported.Count} incompatible or unmapped type(s)");

                        failures++;
                        continue;
                    }

                    if (mode == "verify")
                    {
                        Console.WriteLine(
                            $"  Type-level verification passed " +
                            $"({typeMappings.Count} type references mapped; members NOT verified)");

                        continue;
                    }
                    // Rewrite each mapped XNA type reference to its actual replacement assembly.
                    foreach (var (type, targetAssembly) in typeMappings)
                    {
                        var targetReference = module.AssemblyReferences
                            .FirstOrDefault(r => r.FullName == targetAssembly.FullName);

                        if (targetReference == null)
                        {
                            targetReference = new AssemblyNameReference(
                                targetAssembly.Name,
                                targetAssembly.Version)
                            {
                                Culture = targetAssembly.Culture,
                                PublicKeyToken = targetAssembly.PublicKeyToken
                            };

                            module.AssemblyReferences.Add(targetReference);
                        }

                        type.Scope = targetReference;
                    }

                    // Remove original XNA references only if no remaining metadata uses them.
                    // Do not remove an assembly reference merely because GetTypeReferences()
                    // no longer reports a matching type: other metadata may still depend on it.
                    // Preserve untouched XNA references only if not remapped; remove mapped ones
                    // if no TypeReference still uses them (method and field refs share TypeRefs).
                    // Keep original XNA assembly references for now.
                    // The new typeMappings dictionary handles type-level redirection.
                    //
                    // A future metadata verification pass can safely remove
                    // unused XNA assembly references.
                    Directory.CreateDirectory(output!);
                    var destination = Path.Combine(output!, Path.GetFileName(file));
                    if (Path.GetFullPath(destination).Equals(Path.GetFullPath(file), StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("Output must differ from original folder");
                    if (assembly.Name.HasPublicKey)
                    {
                        Console.WriteLine("  SKIPPED: strong-named assembly; rewriting would invalidate its signature");
                        failures++;
                        continue;
                    }
                    assembly.Write(destination);
                    patched++;
                    Console.WriteLine($"  PATCHED: {destination} (type-level check only; member compatibility unverified)");
                }
                catch (Exception ex) { failures++; Console.Error.WriteLine($"ERROR {file}: {ex.Message}"); }
            }
            Console.WriteLine($"Finished: {files.Length} scanned, {patched} patched, {failures} skipped/failed");
            return failures > 0 ? 2 : 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex.Message); return 1; }
    }

    private static (AssemblyNameReference assembly, HashSet<string> types)? LoadTypes(string? path)
    {
        if (string.IsNullOrWhiteSpace(path)) return null;
        using var asm = AssemblyDefinition.ReadAssembly(path);
        var name = asm.Name;
        var reference = new AssemblyNameReference(name.Name, name.Version)
        {
            Culture = name.Culture,
            PublicKeyToken = name.PublicKeyToken
        };
        var types = new HashSet<string>(StringComparer.Ordinal);
        void Add(TypeDefinition type)
        {
            types.Add(type.FullName);
            foreach (var nested in type.NestedTypes) Add(nested);
        }
        foreach (var type in asm.MainModule.Types) Add(type);
        return (reference, types);
    }

    private static Dictionary<string, string> Parse(string[] args)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < args.Length; i++)
        {
            if (!args[i].StartsWith("--") || i + 1 >= args.Length)
                throw new ArgumentException($"Expected --option value near '{args[i]}'");
            result[args[i][2..]] = args[++i];
        }
        return result;
    }
}
