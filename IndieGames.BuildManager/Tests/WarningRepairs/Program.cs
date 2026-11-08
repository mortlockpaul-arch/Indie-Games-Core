using IndieGames.BuildManager.Services;
using Microsoft.CodeAnalysis.Text;
using System.Text;
if (args.Length >= 3 && args[0] == "--deep") {
 var deep=DeepNullabilityAnalyzer.Plan(File.ReadAllText(args[1]),args[2]);
 Console.WriteLine($"Deep analysis: {deep.WarningCount} CS8618 warnings; {deep.Findings.Count} members; {deep.Repairs.Files.Sum(f=>f.Count)} safe annotations; {deep.Blockers.Count} compilation blockers.");
 foreach(var group in deep.Findings.GroupBy(f=>f.Status+": "+f.Reason))Console.WriteLine($"{group.Count()}: {group.Key}");
 foreach(var error in deep.Blockers.Take(8)) Console.WriteLine(error);
 int report=Array.IndexOf(args,"--report");
 if(report>=0)File.WriteAllText(args[report+1],System.Text.Json.JsonSerializer.Serialize(deep,new System.Text.Json.JsonSerializerOptions{WriteIndented=true}));
 if(args.Contains("--apply") && deep.Repairs.Files.Count>0)Console.WriteLine("BACKUP: "+SourceWarningFixer.Apply(deep.Repairs,args[2]));
 return;
}
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

string eventFile = Path.Combine(folder, "Events.cs");
string eventSource = """
#nullable enable
using System;
class Events {
 event EventHandler<EventArgs>? Changed;
 void Subscribe() { Changed += Unused; Changed += Unused; Changed += Used; }
 void Unused(object sender, EventArgs e) { }
 void Used(object sender, EventArgs e) { Console.WriteLine(sender.ToString()); }
}
""";
File.WriteAllText(eventFile, eventSource);
string EventDiagnostic(string name) {
 int offset=eventSource.IndexOf("+= " + name, StringComparison.Ordinal)+3;
 var point=SourceText.From(eventSource).Lines.GetLinePosition(offset);
 return $"{eventFile}({point.Line+1},{point.Character+1}): warning CS8622: Nullability of reference types in type of parameter 'sender' of 'void Events.{name}(object sender, EventArgs e)' doesn't match the target delegate 'EventHandler<EventArgs>'.\n";
}
var eventPlan=SourceWarningFixer.Plan(EventDiagnostic("Unused")+EventDiagnostic("Unused")+EventDiagnostic("Used"),folder);
if(eventPlan.Files.Count!=1 || eventPlan.Files[0].Count!=1 || eventPlan.Review.Count!=1)throw new Exception("Event sender repairs were not conservative or deduplicated");
if(!eventPlan.Files[0].Updated.Contains("void Unused(object? sender") || !eventPlan.Files[0].Updated.Contains("void Used(object sender"))throw new Exception("Incorrect event sender annotations");
var eventTree=Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(eventPlan.Files[0].Updated);
if(eventTree.GetDiagnostics().Any(d=>d.Severity==Microsoft.CodeAnalysis.DiagnosticSeverity.Error))throw new Exception("Event fix created invalid syntax");
Console.WriteLine("PASS: unused EventHandler sender annotation, duplicate subscriptions and used sender left for review");
string deepFile=Path.Combine(folder,"Deep.cs");
File.WriteAllText(deepFile,"""
#nullable enable
class Deep {
 private string optional;
 private string required;
 public string external;
 void Read() { System.Console.WriteLine(optional?.Length); System.Console.WriteLine(required.Length); }
}
""");
var deepJson=System.Text.Json.JsonSerializer.Serialize(new {
 Properties=new {DefineConstants="",LangVersion="13.0",Nullable="enable"},
 Items=new {Compile=new[]{new {FullPath=deepFile}}, ReferencePath=((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!).Split(Path.PathSeparator).Select(p=>new {FullPath=p})}
});
var deepBefore=File.ReadAllBytes(deepFile);
var deepPlan=DeepNullabilityAnalyzer.Plan(deepJson,folder);
if(deepPlan.Blockers.Count!=0 || deepPlan.WarningCount!=3 || deepPlan.Repairs.Files.Sum(f=>f.Count)!=1)throw new Exception("Deep analysis failed to separate safe optional state from unsafe/public members");
if(!deepPlan.Repairs.Files[0].Updated.Contains("string? optional") || deepPlan.Repairs.Files[0].Updated.Contains("string? required") || deepPlan.Repairs.Files[0].Updated.Contains("string? external"))throw new Exception("Unsafe deep nullability edit");
if(!File.ReadAllBytes(deepFile).SequenceEqual(deepBefore))throw new Exception("Deep preview wrote source");
using(var cancelled=new CancellationTokenSource()) { cancelled.Cancel(); try { DeepNullabilityAnalyzer.Plan(deepJson,folder,cancelled.Token); throw new Exception("Deep cancellation ignored"); } catch(OperationCanceledException){} }
Console.WriteLine("PASS: deep semantic analysis, safe optional annotation, unsafe access rejection, public-contract preservation, read-only preview and cancellation");