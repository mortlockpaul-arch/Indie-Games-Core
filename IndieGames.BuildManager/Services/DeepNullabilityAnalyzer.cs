using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace IndieGames.BuildManager.Services;

public sealed record NullabilityFinding(string File, int Line, string Type, string Member,
    string Status, string Reason, IReadOnlyList<string> Assignments, int References);
public sealed record DeepNullabilityPlan(WarningRepairPlan Repairs, IReadOnlyList<NullabilityFinding> Findings,
    int WarningCount, IReadOnlyList<string> Blockers);

/// <summary>Uses the evaluated project's real compile items, references and defines.
/// Nullable annotations are accepted only when recompilation introduces no diagnostics.
/// Runtime initialization, public contracts and content schemas are never guessed.</summary>
public static class DeepNullabilityAnalyzer
{
    sealed record Candidate(IFieldSymbol Symbol, FieldDeclarationSyntax Field, SyntaxTree Tree);
    static string Key(Diagnostic d) => d.Id + "|" + d.Location.SourceTree?.FilePath + "|" + d.GetMessage();
    static Dictionary<string, int> Counts(IEnumerable<Diagnostic> diagnostics) => diagnostics
        .Where(d => d.Severity is DiagnosticSeverity.Warning or DiagnosticSeverity.Error)
        .GroupBy(Key).ToDictionary(g => g.Key, g => g.Count());

    public static DeepNullabilityPlan Plan(string evaluatedProjectJson, string sourceRoot, CancellationToken token = default)
    {
        using var document = JsonDocument.Parse(evaluatedProjectJson);
        var json = document.RootElement;
        var properties = json.GetProperty("Properties");
        var items = json.GetProperty("Items");
        var defines = properties.GetProperty("DefineConstants").GetString()!.Split(';', StringSplitOptions.RemoveEmptyEntries);
        LanguageVersionFacts.TryParse(properties.GetProperty("LangVersion").GetString(), out var language);
        var parse = new CSharpParseOptions(language == LanguageVersion.Default ? LanguageVersion.Latest : language,
            preprocessorSymbols: defines);
        var trees = items.GetProperty("Compile").EnumerateArray().Select(i => i.GetProperty("FullPath").GetString()!)
            .Distinct(StringComparer.OrdinalIgnoreCase).Where(File.Exists)
            .Select(path => CSharpSyntaxTree.ParseText(SourceText.From(File.ReadAllText(path), Encoding.UTF8), parse, path, token)).ToArray();
        var references = items.GetProperty("ReferencePath").EnumerateArray().Select(i => i.GetProperty("FullPath").GetString()!)
            .Distinct(StringComparer.OrdinalIgnoreCase).Where(File.Exists).Select(p => MetadataReference.CreateFromFile(p));
        var nullable = properties.GetProperty("Nullable").GetString() switch {
            "enable" => NullableContextOptions.Enable,
            "warnings" => NullableContextOptions.Warnings,
            "annotations" => NullableContextOptions.Annotations,
            _ => NullableContextOptions.Disable
        };
        var compilation = CSharpCompilation.Create("DeepNullabilityPreview", trees, references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true,
                nullableContextOptions: nullable));
        var baseline = compilation.GetDiagnostics(token);
        var blockers = baseline.Where(d => d.Severity == DiagnosticSeverity.Error).Select(d => d.ToString()).ToArray();
        var warnings = baseline.Where(d => d.Id == "CS8618").ToArray();
        var baselineCounts = Counts(baseline);
        var findings = new List<NullabilityFinding>();
        var candidates = new List<Candidate>();
        var candidateFindings = new Dictionary<Candidate, int>();
        string prefix = Path.TrimEndingDirectorySeparator(Path.GetFullPath(sourceRoot)) + Path.DirectorySeparatorChar;
        var seen = new HashSet<ISymbol>(SymbolEqualityComparer.Default);

        // One symbol-aware reference index for all members, including partial classes.
        var referencesBySymbol = new Dictionary<ISymbol, List<IdentifierNameSyntax>>(SymbolEqualityComparer.Default);
        var names = warnings.Select(d => Regex.Match(d.GetMessage(), @"(?:field|property|event) '([^']+)'").Groups[1].Value).ToHashSet();
        foreach (var tree in trees)
        {
            token.ThrowIfCancellationRequested();
            var model = compilation.GetSemanticModel(tree);
            foreach (var id in tree.GetRoot(token).DescendantNodes().OfType<IdentifierNameSyntax>().Where(n => names.Contains(n.Identifier.ValueText)))
                if (model.GetSymbolInfo(id, token).Symbol is { } symbol)
                {
                    if (!referencesBySymbol.TryGetValue(symbol, out var usages)) referencesBySymbol[symbol] = usages = [];
                    usages.Add(id);
                }
        }
        foreach (var diagnostic in warnings)
        {
            token.ThrowIfCancellationRequested();
            var tree = diagnostic.Location.SourceTree!;
            var name = Regex.Match(diagnostic.GetMessage(), @"(?:field|property|event) '([^']+)'").Groups[1].Value;
            var node = tree.GetRoot(token).FindToken(diagnostic.Location.SourceSpan.Start).Parent;
            var type = node?.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().FirstOrDefault();
            var model = compilation.GetSemanticModel(tree);
            var symbol = type == null ? null : (model.GetDeclaredSymbol(type, token) as INamedTypeSymbol)?.GetMembers(name).FirstOrDefault();
            if (symbol == null || !seen.Add(symbol)) continue;
            var declaration = symbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax(token);
            var field = declaration?.AncestorsAndSelf().OfType<FieldDeclarationSyntax>().FirstOrDefault();
            var usages = referencesBySymbol.GetValueOrDefault(symbol) ?? [];
            var assignments = usages.Where(u => u.Parent is AssignmentExpressionSyntax a && a.Left == u ||
                u.Parent is MemberAccessExpressionSyntax m && m.Parent is AssignmentExpressionSyntax a2 && a2.Left == m)
                .Select(u => {
                    var method = u.Ancestors().FirstOrDefault(n => n is BaseMethodDeclarationSyntax or AccessorDeclarationSyntax);
                    string owner = method is MethodDeclarationSyntax m ? m.Identifier.ValueText : method is ConstructorDeclarationSyntax c ? c.Identifier.ValueText : "initializer/accessor";
                    return $"{Path.GetFileName(u.SyntaxTree.FilePath)}:{u.GetLocation().GetLineSpan().StartLinePosition.Line + 1} ({owner})";
                }).Distinct().ToArray();
            bool content = symbol.ContainingType.GetAttributes().Any(a => a.AttributeClass?.Name.Contains("ContentSerializer", StringComparison.Ordinal) == true);
            string reason = content ? "Content-loaded data: required fields and serializer initialization need a content-specific contract." :
                symbol.DeclaredAccessibility != Microsoft.CodeAnalysis.Accessibility.Private ? "Exposed member: external callers, serializers and derived classes need review." :
                assignments.Length > 0 ? "Assigned after construction; checking null handling across all compiled consumers." :
                "No assignment found in the compiled source; check whether this member is missing initialization or is unused.";
            int index = findings.Count;
            findings.Add(new(tree.FilePath, diagnostic.Location.GetLineSpan().StartLinePosition.Line + 1,
                symbol.ContainingType.ToDisplayString(), name, "Review", reason, assignments, usages.Count));
            if (blockers.Length == 0 && symbol is IFieldSymbol f && f.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Private &&
                !f.IsStatic && !content && f.Type.IsReferenceType && f.Type.TypeKind != TypeKind.Error &&
                field != null && field.Declaration.Variables.Count == 1 && field.AttributeLists.Count == 0 &&
                field.Declaration.Type is not NullableTypeSyntax && !field.DescendantTrivia().Any(t => t.IsDirective) &&
                field.SyntaxTree.FilePath.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) &&
                !Path.GetRelativePath(sourceRoot, field.SyntaxTree.FilePath).Split(Path.DirectorySeparatorChar).Any(p => p is "bin" or "obj" or ".backups"))
            {
                var candidate = new Candidate(f, field, field.SyntaxTree);
                candidates.Add(candidate); candidateFindings[candidate] = index;
            }
        }
        // Annotate candidates together, then reject any implicated in a new diagnostic.
        // Recompute until the accepted set compiles with no diagnostic regressions.
        var active = candidates.ToList();
        var updatedTrees = new Dictionary<SyntaxTree, SyntaxTree>();
        for (int pass = 0; active.Count > 0; pass++)
        {
            token.ThrowIfCancellationRequested();
            var trial = compilation;
            updatedTrees.Clear();
            foreach (var group in active.GroupBy(c => c.Tree))
            {
                var changes = group.Select(c => new TextChange(new TextSpan(c.Field.Declaration.Type.Span.End, 0), "?"));
                var updated = CSharpSyntaxTree.ParseText(group.Key.GetText(token).WithChanges(changes), parse, group.Key.FilePath, token);
                updatedTrees[group.Key] = updated; trial = trial.ReplaceSyntaxTree(group.Key, updated);
            }
            var diagnostics = trial.GetDiagnostics(token);
            var counts = Counts(diagnostics);
            var newKeys = counts.Where(p => p.Value > baselineCounts.GetValueOrDefault(p.Key)).Select(p => p.Key).ToHashSet();
            if (newKeys.Count == 0) break;
            var rejected = new HashSet<Candidate>();
            foreach (var diagnostic in diagnostics.Where(d => newKeys.Contains(Key(d))))
            {
                if (diagnostic.Location.SourceTree is not { } diagnosticTree) { rejected.UnionWith(active); break; }
                var model = trial.GetSemanticModel(diagnosticTree);
                var node = diagnosticTree.GetRoot(token).FindToken(diagnostic.Location.SourceSpan.Start).Parent;
                var scope = node?.AncestorsAndSelf().FirstOrDefault(n => n is StatementSyntax or MemberDeclarationSyntax) ?? node;
                foreach (var id in scope?.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>() ?? [])
                    if (model.GetSymbolInfo(id, token).Symbol is IFieldSymbol f)
                        foreach (var candidate in active.Where(c => c.Symbol.Name == f.Name &&
                            c.Symbol.ContainingType.ToDisplayString() == f.ContainingType.ToDisplayString())) rejected.Add(candidate);
            }
            // Unknown cross-member effects are never accepted. Bound repeated analysis.
            if (rejected.Count == 0 || pass >= 7) rejected.UnionWith(active);
            foreach (var candidate in rejected)
            {
                int i = candidateFindings[candidate];
                findings[i] = findings[i] with { Reason = "Making this field nullable introduces compiler warnings/errors; initialization or access needs a lifecycle-specific fix." };
            }
            active.RemoveAll(rejected.Contains);
        }
        var edits = new List<WarningFileEdit>();
        foreach (var group in active.GroupBy(c => c.Tree))
        {
            string path = group.Key.FilePath;
            var text = group.Key.GetText(token).WithChanges(group.Select(c => new TextChange(new TextSpan(c.Field.Declaration.Type.Span.End, 0), "?")));
            edits.Add(new(path, File.ReadAllBytes(path), text.ToString(), group.Count()));
            foreach (var c in group)
            {
                int i = candidateFindings[c];
                findings[i] = findings[i] with { Status = "Safe Annotation", Reason = "Private field can be absent: all compiled consumers accept nullable state without new warnings or errors. Runtime values are unchanged." };
            }
        }
        return new(new(edits, findings.Where(f => f.Status == "Review").Select(f => $"{f.File}({f.Line}): {f.Type}.{f.Member}: {f.Reason}").ToArray()),
            findings, warnings.Length, blockers);
    }
}
