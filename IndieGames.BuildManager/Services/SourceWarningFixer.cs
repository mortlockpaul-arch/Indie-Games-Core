using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IndieGames.BuildManager.Services;

public sealed record WarningFileEdit(string Path, byte[] Original, string Updated, int Count);
public sealed record WarningRepairPlan(IReadOnlyList<WarningFileEdit> Files, IReadOnlyList<string> Review);

public static class SourceWarningFixer
{
    static readonly Regex Diagnostic = new(@"^(?<file>.+\.cs)\((?<line>\d+),(?<column>\d+)\): warning (?<code>\w+): (?<message>.*)$", RegexOptions.Multiline);

    public static WarningRepairPlan Plan(string buildLog, string sourceRoot)
    {
        string prefix = Path.TrimEndingDirectorySeparator(Path.GetFullPath(sourceRoot)) + Path.DirectorySeparatorChar;
        var review = new HashSet<string>();
        var files = new List<WarningFileEdit>();
        foreach (var group in Diagnostic.Matches(buildLog).Cast<Match>().GroupBy(m => m.Groups["file"].Value, StringComparer.OrdinalIgnoreCase))
        {
            string path = Path.GetFullPath(group.Key);
            if (!path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ||
                Path.GetRelativePath(sourceRoot, path).Split(Path.DirectorySeparatorChar).Any(p => p is "bin" or "obj" or ".backups")) continue;
            byte[] original = File.ReadAllBytes(path);
            using var reader = new StreamReader(new MemoryStream(original), Encoding.UTF8, true);
            string source = reader.ReadToEnd();
            var tree = CSharpSyntaxTree.ParseText(source);
            var root = tree.GetRoot();
            var lines = tree.GetText().Lines;
            var removals = new HashSet<SyntaxNode>();
            foreach (var diagnostic in group)
            {
                string code = diagnostic.Groups["code"].Value;
                int line = int.Parse(diagnostic.Groups["line"].Value) - 1;
                int column = int.Parse(diagnostic.Groups["column"].Value) - 1;
                if (line < 0 || line >= lines.Count || column < 0 || column >= lines[line].Span.Length) continue;
                var token = root.FindToken(lines[line].Start + column);
                SyntaxNode? remove = null;
                if (code == "CS0219")
                {
                    var variable = token.Parent?.AncestorsAndSelf().OfType<VariableDeclaratorSyntax>().FirstOrDefault();
                    if (variable?.Parent is VariableDeclarationSyntax declaration && declaration.Variables.Count == 1 &&
                        declaration.Parent is LocalDeclarationStatementSyntax local && local.UsingKeyword.IsKind(SyntaxKind.None) &&
                        (declaration.Type is PredefinedTypeSyntax || declaration.Type.IsVar) && IsLiteral(variable.Initializer?.Value))
                    {
                        var scope = local.Ancestors().FirstOrDefault(n => n is BaseMethodDeclarationSyntax or AccessorDeclarationSyntax or LocalFunctionStatementSyntax);
                        if (scope != null && !scope.DescendantNodes().OfType<IdentifierNameSyntax>().Any(n => n.Identifier.ValueText == variable.Identifier.ValueText))
                            remove = local;
                    }
                }
                else if (code == "CS1717")
                {
                    var assignment = token.Parent?.AncestorsAndSelf().OfType<AssignmentExpressionSyntax>().FirstOrDefault();
                    if (assignment?.Left is IdentifierNameSyntax left && assignment.Right is IdentifierNameSyntax right &&
                        left.Identifier.ValueText == right.Identifier.ValueText && assignment.Parent is ExpressionStatementSyntax statement &&
                        statement.Parent is BlockSyntax)
                    {
                        string name = left.Identifier.ValueText;
                        // Only ordinary local variables; fields, properties, ref locals
                        // and parameters can have externally observable writes.
                        bool local = statement.Ancestors().OfType<BlockSyntax>().Any(block => block.Statements
                            .OfType<LocalDeclarationStatementSyntax>().Any(d => d.Span.End < statement.Span.Start &&
                                d.Declaration.Type is not RefTypeSyntax && d.Declaration.Variables.Any(v => v.Identifier.ValueText == name)));
                        if (local) remove = statement;
                    }
                }
                if (remove != null && !remove.DescendantTrivia().Any(t => t.IsDirective)) removals.Add(remove);
                else review.Add($"{path}({line + 1}): {code}: {diagnostic.Groups["message"].Value}");
            }
            if (removals.Count > 0)
            {
                var updated = root.RemoveNodes(removals, SyntaxRemoveOptions.KeepExteriorTrivia)?.ToFullString() ?? source;
                if (updated != source) files.Add(new(path, original, updated, removals.Count));
            }
        }
        return new(files, review.ToArray());
    }

    static bool IsLiteral(ExpressionSyntax? value) => value switch
    {
        LiteralExpressionSyntax literal => literal.Kind() is SyntaxKind.NumericLiteralExpression or SyntaxKind.StringLiteralExpression or
            SyntaxKind.CharacterLiteralExpression or SyntaxKind.TrueLiteralExpression or SyntaxKind.FalseLiteralExpression or SyntaxKind.NullLiteralExpression,
        PrefixUnaryExpressionSyntax unary when unary.Kind() is SyntaxKind.UnaryMinusExpression or SyntaxKind.UnaryPlusExpression => IsLiteral(unary.Operand),
        ParenthesizedExpressionSyntax parentheses => IsLiteral(parentheses.Expression),
        _ => false
    };

    public static string Apply(WarningRepairPlan plan, string sourceRoot)
    {
        foreach (var file in plan.Files)
            if (!File.ReadAllBytes(file.Path).SequenceEqual(file.Original)) throw new IOException($"Source changed during preview: {file.Path}");
        string backup = Path.Combine(sourceRoot, ".backups", "warnings-" + DateTime.Now.ToString("yyyyMMdd-HHmmss-fffffff"));
        foreach (var file in plan.Files)
        {
            string copy = Path.Combine(backup, Path.GetRelativePath(sourceRoot, file.Path));
            Directory.CreateDirectory(Path.GetDirectoryName(copy)!);
            File.WriteAllBytes(copy, file.Original);
            using var reader = new StreamReader(new MemoryStream(file.Original), Encoding.UTF8, true);
            reader.ReadToEnd();
            Encoding encoding = reader.CurrentEncoding;
            if (encoding.CodePage == Encoding.UTF8.CodePage)
                encoding = new UTF8Encoding(file.Original.Take(3).SequenceEqual(new byte[] { 0xef, 0xbb, 0xbf }));
            File.WriteAllText(file.Path, file.Updated, encoding);
        }
        return backup;
    }
}
