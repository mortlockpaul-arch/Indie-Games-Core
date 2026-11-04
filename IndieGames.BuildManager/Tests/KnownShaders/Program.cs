using System.Security.Cryptography;
using System.Text.Json;
using System.Xml.Linq;
using IndieGames.BuildManager.Services;

if(args.Length>0 && args[0]=="--verify-published")
{
    var assembly=System.Reflection.Assembly.LoadFrom(Path.GetFullPath(args[1]));
    foreach(string name in new[]{"RoundLine.fx","spotlight.fx","Blur.fx","ParticleEffect.fx","BloomExtract.fx","BloomCombine.fx","GaussianBlur.fx","BuildKnownShaders.ps1"})
        if(!assembly.GetManifestResourceNames().Any(n=>n.EndsWith(".KnownShaders."+name,StringComparison.Ordinal)))throw new Exception("Published resource missing: "+name);
    var type=assembly.GetType("IndieGames.BuildManager.Services.KnownShaderRepair",throwOnError:true)!;
    var task=(Task)type.GetMethod("RepairAsync")!.Invoke(null,[args[2],(Action<string>)Console.WriteLine,CancellationToken.None,null])!;
    await task;
    var results=(System.Collections.IEnumerable)task.GetType().GetProperty("Result")!.GetValue(task)!;
    int count=0;
    foreach(var result in results){if((string?)result.GetType().GetProperty("Status")!.GetValue(result)!="UpToDate")throw new Exception("Published service did not recognize installed fix.");count++;}
    if(count!=4)throw new Exception("Published service did not recognize all four shaders.");
    Console.WriteLine("PASS: published app includes all templates and its actual repair service recognizes all four installed shaders.");
    return;
}

if(args.Length>0 && args[0]=="--repair")
{
    var results=await KnownShaderRepair.RepairAsync(args[1],Console.WriteLine,CancellationToken.None);
    if(results.Any(r=>r.Status is "Conflict" or "Unsupported")) Environment.ExitCode=1;
    return;
}
string work=args.Length>0?Path.GetFullPath(args[0]):Path.Combine(Path.GetTempPath(),"KnownShaderRepair.Tests");
Directory.CreateDirectory(work);
string fixtures=Path.Combine(AppContext.BaseDirectory,"Fixtures");
string[] names=["RoundLine","spotlight","Blur","ParticleEffect"];
byte[] Original(string name)=>File.ReadAllBytes(Path.Combine(fixtures,name+".xnb"));
string Suite=Path.Combine(work,"shader-repair-tests",Guid.NewGuid().ToString("N"));Directory.CreateDirectory(Suite);
string Fixture(string name)
{
    string root=Path.Combine(Suite,name);Directory.CreateDirectory(Path.Combine(root,"Content","Effects"));
    File.WriteAllText(Path.Combine(root,"Game.csproj"),"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0-windows</TargetFramework><ImplicitUsings>enable</ImplicitUsings></PropertyGroup><ItemGroup><Reference Include=\"FNA\"><HintPath>C:\\source\\Indie-Games-Core\\FNA\\bin\\Debug\\net8.0\\FNA.dll</HintPath></Reference></ItemGroup></Project>");
    foreach(string effect in names)File.WriteAllBytes(Path.Combine(root,"Content","Effects",effect+".xnb"),Original(effect));
    Directory.CreateDirectory(Path.Combine(root,"Maximinus"));
    File.Copy(Path.Combine(fixtures,"RoundLineManager.cs.txt"),Path.Combine(root,"Maximinus","RoundLineManager.cs"));
    foreach(string source in new[]{"RoundLine.cs","RoundLineVertex.cs"})File.Copy(Path.Combine(fixtures,source+".txt"),Path.Combine(root,"Maximinus",source));
    return root;
}
void Require(bool condition,string message){if(!condition)throw new Exception(message);}
Dictionary<string,string> Snapshot(string root)=>Directory.EnumerateFiles(root,"*",SearchOption.AllDirectories)
    .Where(p=>!p.Contains(Path.DirectorySeparatorChar+"obj"+Path.DirectorySeparatorChar)&&!p.Contains("shader-repair-backups"))
    .ToDictionary(p=>Path.GetRelativePath(root,p),p=>Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(p))));
void Same(Dictionary<string,string> before,string root)=>Require(before.OrderBy(p=>p.Key).SequenceEqual(Snapshot(root).OrderBy(p=>p.Key)),"Game files changed unexpectedly: "+root);
var token=CancellationToken.None;
string valid=Fixture("valid");
var repaired=await KnownShaderRepair.RepairAsync(Path.Combine(valid,"Game.csproj"),_=>{},token);
Require(repaired.Count(r=>r.Status=="Repaired")==4,"All four known shaders must repair.");
foreach(string name in names)
{
    byte[] bytes=File.ReadAllBytes(Path.Combine(valid,"Content","Effects",name+".xnb"));
    Require(bytes[3]=='w' && BitConverter.ToInt32(bytes,6)==bytes.Length,"Invalid Windows XNB: "+name);
}
var project=XDocument.Load(Path.Combine(valid,"Game.csproj"));
Require(project.Descendants("Target").Count(e=>(string?)e.Attribute("Name")=="BuildKnownShaders")==1,"Rebuild target missing or duplicated.");
Require(project.Descendants("Compile").Any(e=>e.Attribute("Remove")?.Value.Contains("shader-repair-backups")==true),"Backed-up C# would be compiled.");
string renderer=File.ReadAllText(Path.Combine(valid,"Maximinus","RoundLineManager.cs"));
Require(!renderer.Contains("numVertices * bytesPerVertex, BufferUsage.None")&&!renderer.Contains("numIndices * 2, BufferUsage.None"),"Buffer repairs missing.");
string backup=Directory.GetDirectories(Path.Combine(valid,"shader-repair-backups")).Single();
Require(names.All(n=>File.ReadAllBytes(Path.Combine(backup,"Content","Effects",n+".xnb")).SequenceEqual(Original(n))),"Original shaders were not backed up exactly.");
var snapshot=Snapshot(valid);
var repeated=await KnownShaderRepair.RepairAsync(Path.Combine(valid,"Game.csproj"),_=>{},token,Path.Combine(Suite,"missing-fxc.exe"));
Require(repeated.Count(r=>r.Status=="UpToDate")==4,"Repeat must skip all four shaders without requiring the SDK.");Same(snapshot,valid);
Require(Directory.GetDirectories(Path.Combine(valid,"shader-repair-backups")).Length==1,"Repeat created redundant backups.");
Console.WriteLine("PASS: four repairs, byte-exact backups, buffer fixes, rebuild hook, and repeat-run idempotency.");
string unknown=Fixture("unknown");
foreach(string n in names)File.WriteAllBytes(Path.Combine(unknown,"Content","Effects",n+".xnb"),"XNBx-unknown"u8.ToArray());
snapshot=Snapshot(unknown);
var unsupported=await KnownShaderRepair.RepairAsync(Path.Combine(unknown,"Game.csproj"),_=>{},token);
Require(unsupported.Count(r=>r.Status=="Unsupported")==4,"Unknown shaders must not be replaced.");Same(snapshot,unknown);
string custom=Fixture("custom");
foreach(string n in names)File.WriteAllText(Path.Combine(custom,"Content","Effects",n+".fx"),"// User custom shader");
snapshot=Snapshot(custom);
var conflicts=await KnownShaderRepair.RepairAsync(Path.Combine(custom,"Game.csproj"),_=>{},token);
Require(conflicts.Count(r=>r.Status=="Conflict")==4,"Custom source must be protected.");Same(snapshot,custom);
Console.WriteLine("PASS: unknown binaries and custom shader sources are preserved.");
string missing=Fixture("missing-sdk");snapshot=Snapshot(missing);
try{await KnownShaderRepair.RepairAsync(Path.Combine(missing,"Game.csproj"),_=>{},token,Path.Combine(Suite,"no-fxc.exe"));throw new Exception("Missing SDK was accepted.");}catch(FileNotFoundException){}
Same(snapshot,missing);
string failure=Fixture("compiler-failure");snapshot=Snapshot(failure);
try{await KnownShaderRepair.RepairAsync(Path.Combine(failure,"Game.csproj"),_=>{},token,Path.Combine(Environment.SystemDirectory,"where.exe"));throw new Exception("Bad compiler was accepted.");}catch(InvalidDataException){}
Same(snapshot,failure);
string cancelled=Fixture("cancelled");snapshot=Snapshot(cancelled);
try{await KnownShaderRepair.RepairAsync(Path.Combine(cancelled,"Game.csproj"),_=>{},new CancellationToken(true));throw new Exception("Cancellation was ignored.");}catch(OperationCanceledException){}
Same(snapshot,cancelled);
Console.WriteLine("PASS: missing SDK, compilation failure, and cancellation do not modify game files.");
string locked=Fixture("locked-project");snapshot=Snapshot(locked);
using(var handle=new FileStream(Path.Combine(locked,"Game.csproj"),FileMode.Open,FileAccess.Read,FileShare.Read))
{
    try{await KnownShaderRepair.RepairAsync(Path.Combine(locked,"Game.csproj"),_=>{},token);throw new Exception("Locked project was overwritten.");}catch(Exception ex) when(ex is IOException or UnauthorizedAccessException){}
}
Same(snapshot,locked);
Console.WriteLine("PASS: a failed commit rolls back previously written shader/source files.");
string traversal=Fixture("bad-manifest");Directory.CreateDirectory(Path.Combine(traversal,"build"));
File.WriteAllText(Path.Combine(traversal,"build","KnownShaders.json"),JsonSerializer.Serialize(new KnownShaderRepair.Manifest(1,[new("spotlight","../escape.fx","Content/Effects/spotlight.xnb","","")])));
snapshot=Snapshot(traversal);
try{await KnownShaderRepair.RepairAsync(Path.Combine(traversal,"Game.csproj"),_=>{},token);throw new Exception("Escaping manifest was accepted.");}catch(InvalidDataException){}
Same(snapshot,traversal);
Console.WriteLine("PASS: manifests cannot write outside the game project.");
File.WriteAllText(Path.Combine(work,"known-shader-valid-fixture.txt"),valid);
Console.WriteLine("VALID FIXTURE: "+valid);

string bloom = Path.Combine(Suite,"bloom");
Directory.CreateDirectory(Path.Combine(bloom,"Content","Shaders"));
File.WriteAllText(Path.Combine(bloom,"Game.csproj"),"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0-windows</TargetFramework></PropertyGroup></Project>");
string[] bloomNames=["BloomExtract","BloomCombine","GaussianBlur"];
foreach(var name in bloomNames)File.WriteAllBytes(Path.Combine(bloom,"Content","Shaders",name+".xnb"),Original(name));
var bloomResults=await KnownShaderRepair.RepairAsync(Path.Combine(bloom,"Game.csproj"),Console.WriteLine,token);
Require(bloomResults.Count(r=>r.Status=="Repaired")==3,"Three fingerprinted bloom effects must repair.");
var bloomBackup=Directory.GetDirectories(Path.Combine(bloom,"shader-repair-backups")).Single();
foreach(var name in bloomNames)Require(File.ReadAllBytes(Path.Combine(bloomBackup,"Content","Shaders",name+".xnb")).SequenceEqual(Original(name)),"Bloom backup differs: "+name);
snapshot=Snapshot(bloom);
var bloomRepeat=await KnownShaderRepair.RepairAsync(Path.Combine(bloom,"Game.csproj"),_=>{},token,Path.Combine(Suite,"no-sdk.exe"));
Require(bloomRepeat.Count(r=>r.Status=="UpToDate")==3,"Bloom repeat must be idempotent.");Same(snapshot,bloom);
Console.WriteLine("PASS: bloom repairs, exact backups, and repeat-run idempotency.");
File.WriteAllText(Path.Combine(work,"known-bloom-valid-fixture.txt"),bloom);

string recoveredRoot=Path.Combine(Suite,"recovered");Directory.CreateDirectory(Path.Combine(recoveredRoot,"Content"));
File.WriteAllText(Path.Combine(recoveredRoot,"Game.csproj"),"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0-windows</TargetFramework></PropertyGroup></Project>");
var recoveredFixtures=Directory.GetFiles(Path.Combine(fixtures,"Recovered"),"*.xnb",SearchOption.AllDirectories);
Require(recoveredFixtures.Length==36,"Expected 36 recovered custom-effect fixtures.");
foreach(var file in recoveredFixtures){string to=Path.Combine(recoveredRoot,"Content",Path.GetRelativePath(Path.Combine(fixtures,"Recovered"),file));Directory.CreateDirectory(Path.GetDirectoryName(to)!);File.Copy(file,to);}
var recoveredResults=await KnownShaderRepair.RepairAsync(Path.Combine(recoveredRoot,"Game.csproj"),_=>{},token);
Require(recoveredResults.Count(r=>r.Status=="Repaired")==36,"All recovered custom effects must repair.");
snapshot=Snapshot(recoveredRoot);
var recoveredRepeat=await KnownShaderRepair.RepairAsync(Path.Combine(recoveredRoot,"Game.csproj"),_=>{},token,Path.Combine(Suite,"no-sdk.exe"));
Require(recoveredRepeat.Count(r=>r.Status=="UpToDate")==36,"Custom repairs must be idempotent.");Same(snapshot,recoveredRoot);
var recoveredBackup=Directory.GetDirectories(Path.Combine(recoveredRoot,"shader-repair-backups")).Single();
foreach(var file in recoveredFixtures){var original=Path.Combine(recoveredBackup,"Content",Path.GetRelativePath(Path.Combine(fixtures,"Recovered"),file));Require(File.ReadAllBytes(original).SequenceEqual(File.ReadAllBytes(file)),"Custom-effect original backup mismatch.");}
Console.WriteLine("PASS: 36 recovered custom effects, exact backups, variant selection, and idempotency.");
string recoveredManifestPath=Path.Combine(recoveredRoot,"build","KnownShaders.json");
var recoveredManifest=JsonSerializer.Deserialize<KnownShaderRepair.Manifest>(File.ReadAllText(recoveredManifestPath))!;
var oldEntry=recoveredManifest.Effects[0];string ownedSource=Path.Combine(recoveredRoot,oldEntry.Source.Replace('/',Path.DirectorySeparatorChar));
File.AppendAllText(ownedSource,"\n// Earlier generated template\n");
recoveredManifest.Effects[0]=oldEntry with {TemplateHash=Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(ownedSource)))};
File.WriteAllText(recoveredManifestPath,JsonSerializer.Serialize(recoveredManifest));
var upgraded=await KnownShaderRepair.RepairAsync(Path.Combine(recoveredRoot,"Game.csproj"),_=>{},token);
Require(upgraded.Count(r=>r.Status=="Repaired")==1,"Owned generated source must upgrade.");
File.AppendAllText(ownedSource,"\n// User edit\n");snapshot=Snapshot(recoveredRoot);
var edited=await KnownShaderRepair.RepairAsync(Path.Combine(recoveredRoot,"Game.csproj"),_=>{},token);
Require(edited.Count(r=>r.Status=="Conflict")==1,"Edited source must not be overwritten during upgrade.");Same(snapshot,recoveredRoot);
Console.WriteLine("PASS: generated template upgrades preserve custom edits.");
File.WriteAllText(Path.Combine(work,"known-recovered-valid-fixture.txt"),recoveredRoot);
