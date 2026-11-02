using IndieGames.BuildManager.Services;

string root = Path.Combine(Path.GetTempPath(), "BuildManagerCleanupTests-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
void Assert(bool condition, string message) { if (!condition) throw new Exception(message); }
void Write(string path, string content) { Directory.CreateDirectory(Path.GetDirectoryName(path)!); File.WriteAllText(path, content); }
string Game(string name, string? xml = "<Project><PropertyGroup><OutputType>WinExe</OutputType></PropertyGroup></Project>")
{
    string folder=Path.Combine(root,name); Directory.CreateDirectory(folder);
    if (xml != null) Write(Path.Combine(folder,name+".csproj"),xml);
    return folder;
}
try
{
    string good=Game("Good, Game", "<Project><PropertyGroup><OutputType>Exe</OutputType></PropertyGroup><ItemGroup><ProjectReference Include='Required.csproj'/></ItemGroup></Project>");
    string required=Path.Combine(good,"Required.csproj"), old=Path.Combine(good,"Old.csproj"), backup=Path.Combine(good,"source.cs.bak");
    Write(required,"<Project />"); Write(old,"<Project />"); Write(old+".disabled","existing disabled copy"); Write(backup,"old source");
    string bad=Game("Broken", "<Project><bad></Project>"); Write(Path.Combine(bad,"Old.csproj"),"<Project />");
    string missing=Game("Missing",null); Write(Path.Combine(missing,"Old.csproj"),"<Project />");
    var plan=ArchiveProjectCleanup.Preview(root);
    Assert(plan.Rows.Count(r=>r.Action=="Disable")==1,"Only the unreferenced project should be disabled");
    Assert(plan.Rows.Count(r=>r.Action=="Review")==2,"Missing/invalid main projects need review");
    Assert(File.Exists(old)&&File.Exists(backup),"Preview must not mutate files");
    Assert(plan.Rows.Single(r=>r.Path==required).Action=="Keep","Referenced library must be preserved");
    ArchiveProjectCleanup.Apply(plan);
    var disabled=plan.Rows.Single(r=>r.Action=="Disable");
    Assert(!File.Exists(old)&&File.Exists(disabled.Destination),"Disable should rename the active project");
    Assert(File.ReadAllText(old+".disabled")=="existing disabled copy","Collision must preserve existing disabled file");
    Assert(File.Exists(required)&&!File.Exists(backup),"Keep reference and remove backup");
    Assert(File.Exists(Path.Combine(bad,"Old.csproj"))&&File.Exists(Path.Combine(missing,"Old.csproj")),"Protect games with invalid/missing main projects");
    Assert(plan.Rows.Where(r=>r.Actionable).All(r=>r.Status=="Applied"),"Report must show action results");
    string csv=Path.Combine(root,"report.csv"); ArchiveProjectCleanup.Export(plan,csv);
    Assert(File.ReadAllText(csv).Contains("\""+old+"\"") && File.ReadAllText(csv).Contains("\"Disable\""),"CSV should quote paths containing commas");
    Console.WriteLine("PASS preview, reference preservation, invalid/missing main projects, collision handling, backup deletion and CSV results");

    Write(old,"<Project />"); var stale=ArchiveProjectCleanup.Preview(root);
    Write(required,"<Project><ItemGroup><ProjectReference Include='Old.csproj'/></ItemGroup></Project>");
    bool rejected=false; try { ArchiveProjectCleanup.Apply(stale); } catch(IOException) { rejected=true; }
    Assert(rejected&&File.Exists(old),"Changed dependencies must invalidate preview");
    var linked=ArchiveProjectCleanup.Preview(root); Assert(linked.Rows.Single(r=>r.Path==old).Action=="Keep","New dependency must be preserved");
    Console.WriteLine("PASS stale-preview rejection and dependency changes");

    Write(required,"<Project />");
    var collision=ArchiveProjectCleanup.Preview(root);
    var pending=collision.Rows.Single(r=>r.Path==old);
    Write(pending.Destination,"created after preview");
    ArchiveProjectCleanup.Apply(collision);
    Assert(pending.Status.StartsWith("Failed:") && File.Exists(old) && File.ReadAllText(pending.Destination)=="created after preview", "Late collisions must be reported without overwriting files");
    Console.WriteLine("PASS per-file failure reporting and no-overwrite behavior");

    Write(required,"<Project><ItemGroup><ProjectReference Include='$(Library).csproj'/></ItemGroup></Project>");
    Assert(!ArchiveProjectCleanup.Preview(root).Rows.Any(r=>r.Action=="Disable"),"Dynamic dependencies require manual review");
    using var cancelled=new CancellationTokenSource(); cancelled.Cancel();
    bool stopped=false; try { ArchiveProjectCleanup.Preview(root,cancelled.Token); } catch(OperationCanceledException) { stopped=true; }
    Assert(stopped,"Cancelled scans must stop");
    Console.WriteLine("PASS dynamic-reference protection and cancellation");
}
finally { Directory.Delete(root,true); }
