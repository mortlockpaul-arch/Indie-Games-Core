using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Mono.Cecil;

internal static class Program
{
    static IEnumerable<TypeDefinition> AllTypes(AssemblyDefinition asm) => asm.MainModule.Types.SelectMany(Walk);
    static IEnumerable<TypeDefinition> Walk(TypeDefinition t) { yield return t; foreach (var child in t.NestedTypes) foreach (var x in Walk(child)) yield return x; }
    static bool Visible(TypeDefinition t) => t.IsPublic || (t.IsNestedPublic && (t.DeclaringType == null || Visible(t.DeclaringType)));
    static bool Visible(MethodDefinition m) => m.IsPublic || m.IsFamily || m.IsFamilyOrAssembly;
    static bool Visible(FieldDefinition f) => f.IsPublic || f.IsFamily || f.IsFamilyOrAssembly;
    static string T(TypeReference t) => t.FullName;
    static IEnumerable<string> Api(AssemblyDefinition asm)
    {
        foreach (var type in AllTypes(asm).Where(Visible))
        {
            yield return "T|" + type.FullName + "|" + (type.IsInterface ? "interface" : type.IsEnum ? "enum" : type.IsValueType ? "struct" : "class");
            foreach (var m in type.Methods.Where(Visible))
                yield return "M|" + type.FullName + "|" + m.Name + "|" + m.GenericParameters.Count + "|" + T(m.ReturnType) + "|" + string.Join(",", m.Parameters.Select(p => T(p.ParameterType)));
            foreach (var f in type.Fields.Where(Visible)) yield return "F|" + type.FullName + "|" + f.Name + "|" + T(f.FieldType);
        }
    }
    static object Summary(AssemblyDefinition asm)
    {
        var types = AllTypes(asm).ToList();
        return new { name = asm.Name.FullName, types = types.Count, publicTypes = types.Count(Visible), methods = types.Sum(t => t.Methods.Count), fields = types.Sum(t => t.Fields.Count), fieldRvaCount = types.SelectMany(t => t.Fields).Count(f => f.HasFieldRVA), fieldRvaBytes = types.SelectMany(t => t.Fields).Where(f => f.HasFieldRVA).Sum(f => f.InitialValue?.Length ?? 0), resources = asm.MainModule.Resources.Select(r => r.Name).OrderBy(s => s).ToArray(), references = asm.MainModule.AssemblyReferences.Select(r => r.FullName).OrderBy(s => s).ToArray() };
    }
    static int Main(string[] args)
    {
        if (args.Length != 4) { Console.Error.WriteLine("Usage: SunBurnCompatibilityAudit SANITIZED REBUILT RESTORED REPORT.json"); return 2; }
        try
        {
            using var src = AssemblyDefinition.ReadAssembly(args[0]);
            using var build = AssemblyDefinition.ReadAssembly(args[1]);
            using var restored = AssemblyDefinition.ReadAssembly(args[2]);
            var a = Api(src).ToHashSet(StringComparer.Ordinal);
            var b = Api(build).ToHashSet(StringComparer.Ordinal);
            var c = Api(restored).ToHashSet(StringComparer.Ordinal);
            var frameworkNames = new HashSet<string>(StringComparer.Ordinal) {
                "System.SerializableAttribute", "System.Runtime.Serialization.ISerializable", "System.Runtime.Serialization.SerializationInfo",
                "System.Runtime.Serialization.SerializationEntry", "System.Runtime.Serialization.FormatterConverter",
                "System.Runtime.Serialization.IFormatterConverter", "System.Runtime.Serialization.SerializationException"
            };
            var duplicateCandidates = AllTypes(build).Where(t => frameworkNames.Contains(t.FullName)).Select(t => t.FullName).OrderBy(s => s).ToArray();
            var report = new {
                sanitized = Summary(src), rebuilt = Summary(build), restored = Summary(restored),
                sanitizedPublicApiCount = a.Count, rebuiltPublicApiCount = b.Count, restoredPublicApiCount = c.Count,
                missingFromRebuilt = a.Except(b).OrderBy(s => s).ToArray(), extraInRebuilt = b.Except(a).OrderBy(s => s).ToArray(),
                missingFromRestored = a.Except(c).OrderBy(s => s).ToArray(), differencesBetweenRebuiltAndRestored = b.SymmetricExcept(c).OrderBy(s => s).ToArray(),
                duplicateFrameworkTypeCandidates = duplicateCandidates,
                warning = "Signature comparison ignores assembly identity, generic constraints, attributes, method bodies, and serialization behavior. A clean report does not prove runtime compatibility. Framework duplicate candidates are not automatically safe to remove."
            };
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(args[3]))!);
            File.WriteAllText(args[3], JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine($"Sanitized public API: {a.Count}; rebuilt: {b.Count}; restored: {c.Count}");
            Console.WriteLine($"Missing rebuilt: {a.Except(b).Count()}; extra rebuilt: {b.Except(a).Count()}; missing restored: {a.Except(c).Count()}");
            Console.WriteLine($"Rebuilt/restored API differences: {b.SymmetricExcept(c).Count()}; framework duplicates: {duplicateCandidates.Length}");
            Console.WriteLine("Report: " + args[3]);
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }
    static IEnumerable<T> SymmetricExcept<T>(this IEnumerable<T> x, IEnumerable<T> y) { var a = x.ToHashSet(); var b = y.ToHashSet(); return a.Except(b).Concat(b.Except(a)); }
}
