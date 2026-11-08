using IndieGames.BuildManager.Services;
using Microsoft.CodeAnalysis.Text;
using System.Text;
if (args.Length >= 2) {
 string logPath = Path.GetFullPath(args[0]), rootPath = Path.GetFullPath(args[1]);
 var actualPlan = SourceWarningFixer.Plan(File.ReadAllText(logPath), rootPath);
 foreach (var edit in actualPlan.Files) Console.WriteLine($"{edit.Count}: {edit.Path}");
 Console.WriteLine($"Total: {actualPlan.Files.Sum(f => f.Count)} safe repairs across {actualPlan.Files.Count} files; {actualPlan.Review.Count} warnings require review.");
 if (args.Contains("--apply") && actualPlan.Files.Count > 0) Console.WriteLine("BACKUP: " + SourceWarningFixer.Apply(actualPlan, rootPath));
 if (args.Length >= 3 && !args[2].StartsWith("--")) File.WriteAllText(args[2], System.Text.Json.JsonSerializer.Serialize(actualPlan.Files.Select(f => new { f.Path, f.Count }), new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
 return;
}
string folder = Path.Combine(Path.GetTempPath(), "warning-repairs-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(folder);
string file = Path.Combine(folder, "Game.cs");
string source = """
class Game {
 int field;
 void Draw() {
  int unused = 12;
  int sideEffects = Factory();
  Custom conversion = "hello";
  int assignedLater = 1;
  assignedLater = 2;
  int value = 0;
  value = value;
  field = field;
  _ = value;
 }
}
""";
File.WriteAllText(file, source, new UTF8Encoding(true));
var before = File.ReadAllBytes(file);
string Diagnostic(string text, string code) {
 int offset = source.IndexOf(text, StringComparison.Ordinal);
 var point = SourceText.From(source).Lines.GetLinePosition(offset);
 return $"{file}({point.Line + 1},{point.Character + 1}): warning {code}: Test warning\n";
}
string log = Diagnostic("unused", "CS0219") + Diagnostic("sideEffects", "CS0219") + Diagnostic("conversion", "CS0219") +
 Diagnostic("assignedLater", "CS0219") + Diagnostic("value = value", "CS1717") + Diagnostic("field = field", "CS1717");
var plan = SourceWarningFixer.Plan(log + log, folder);
if (plan.Files.Count != 1 || plan.Files[0].Count != 2) throw new Exception("Expected exactly two safe repairs.");
if (!File.ReadAllBytes(file).SequenceEqual(before)) throw new Exception("Preview modified source.");
string updated = plan.Files[0].Updated;
if (updated.Contains("int unused") || updated.Contains("value = value")) throw new Exception("Safe repairs missing.");
foreach (string retained in new[] { "Factory()", "Custom conversion", "assignedLater = 2", "field = field" })
 if (!updated.Contains(retained)) throw new Exception("Unsafe removal: " + retained);
string backup = SourceWarningFixer.Apply(plan, folder);
if (!File.ReadAllBytes(Path.Combine(backup, "Game.cs")).SequenceEqual(before)) throw new Exception("Backup mismatch.");
if (!File.ReadAllBytes(file).Take(3).SequenceEqual(new byte[] { 0xef, 0xbb, 0xbf })) throw new Exception("Encoding changed.");
try { SourceWarningFixer.Apply(plan, folder); throw new Exception("Stale plan accepted."); } catch (IOException) { }
Console.WriteLine("PASS: safe local repairs, no side-effect/custom conversion/field removal, read-only preview, deduplication, backups, BOM preservation and stale-source guard.");
