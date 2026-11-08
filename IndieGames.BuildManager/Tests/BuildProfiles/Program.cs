using System.Xml.Linq;
using IndieGames.BuildManager.Services;
string core=args.Length>0?Path.GetFullPath(args[0]):Path.GetFullPath("../../../..");
string root=args.Length>1?Path.GetFullPath(args[1]):Path.Combine(Path.GetTempPath(),"framework-profiles-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(root);
void Require(bool good,string why){if(!good)throw new Exception(why);}
string Fixture(string name,string references,string tfm="net9.0-windows"){
 string folder=Path.Combine(root,name);Directory.CreateDirectory(folder);string project=Path.Combine(folder,name+".csproj");
 File.WriteAllText(project,$"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><OutputType>Exe</OutputType><TargetFramework>{tfm}</TargetFramework><ImplicitUsings>enable</ImplicitUsings><PlatformTarget>x64</PlatformTarget></PropertyGroup><ItemGroup>{references}</ItemGroup></Project>");
 File.WriteAllText(Path.Combine(folder,"Program.cs"),"using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.GamerServices; using Microsoft.Xna.Framework.Net; System.Console.WriteLine(typeof(Game).Assembly.GetName().Name); System.Console.WriteLine(typeof(Gamer).Assembly.GetName().Name); System.Console.WriteLine(typeof(NetworkSession).Assembly.GetName().Name);");return project;
}
string fna=Fixture("FnaGame",$"<ProjectReference Include=\"{core}/FNA/FNA.Core.csproj\"/><ProjectReference Include=\"{core}/IndieGames.Overlay.FNA/IndieGames.Overlay.FNA.csproj\"/>");
string original=File.ReadAllText(fna);
foreach(string framework in new[]{"FNA","MonoGame"})foreach(string network in new[]{"NetStub","Steamworks"}) {
 var plan=GameBuildProfiles.Prepare(fna,framework,network,"DesktopGL","3.8.1.303",core,Path.Combine(root,"cache"));
 var targets=XDocument.Load(plan.Targets); File.WriteAllLines(Path.Combine(root,$"{framework}-{network}.properties"),plan.Arguments);
 Require(plan.Framework==framework&&plan.Network==network,"Wrong profile");
 Require(targets.Descendants("ProjectReference").Any(e=>e.Attribute("Include")!=null && Path.GetFullPath((string)e.Attribute("Include")!)==(framework=="MonoGame"?$"{core}/IndieGames.Compatibility.MonoGame/IndieGames.Compatibility.MonoGame.csproj".Replace('/',Path.DirectorySeparatorChar):network=="NetStub"?$"{core}/FNA.NetStub/FNA.NetStub.csproj".Replace('/',Path.DirectorySeparatorChar):$"{core}/FNA.AvatarCompat/FNA.AvatarCompat.csproj".Replace('/',Path.DirectorySeparatorChar))),"Wrong network adapter");
 File.WriteAllText(Path.Combine(root,$"{framework}-{network}.args"),plan.Targets); File.WriteAllLines(Path.Combine(root,$"{framework}-{network}.properties"),plan.Arguments);
}
Require(File.ReadAllText(fna)==original,"Build profiles changed submission source");
string xna=Fixture("XnaGame","<Reference Include=\"Microsoft.Xna.Framework\"/>");
Require(!GameBuildProfiles.Inspect(xna).Overlay,"F1 enabled for XNA");
Require(GameBuildProfiles.Prepare(xna,"XNA → FNA","NetStub","Project default","",core,Path.Combine(root,"cache")).ConvertXna,"Missing conversion option");
string legacy=Fixture("LegacyGame","<Reference Include=\"Microsoft.Xna.Framework\"/>","net48");Require(!GameBuildProfiles.Inspect(legacy).ManagedSource,"Legacy switching exposed");
try{GameBuildProfiles.Prepare(legacy,"FNA","NetStub","Project default","",core,root);throw new Exception("Legacy switch accepted");}catch(InvalidOperationException){}
string mono=Fixture("MonoGame","<PackageReference Include=\"MonoGame.Framework.DesktopGL\" Version=\"3.8.1.303\"/>");Require(GameBuildProfiles.Inspect(mono).Overlay,"Supported MonoGame F1 disabled");
var native=GameBuildProfiles.Prepare(mono,"Project default","Project default","Project default","",core,Path.Combine(root,"cache"));Require(native.Framework=="MonoGame"&&native.Network=="None"&&native.Targets.Length==0,"Plain MonoGame should not receive an XNA network shim");
// Backup preview never modifies files; changed files are rejected before permanent deletion.
string backupRoot=Path.Combine(root,"backup-tests");Directory.CreateDirectory(backupRoot);File.WriteAllText(Path.Combine(backupRoot,"Game.csproj"),"<Project/>");File.WriteAllText(Path.Combine(backupRoot,"Game.cs"),"source");string bak=Path.Combine(backupRoot,"Game.cs.before-version.bak");File.WriteAllText(bak,"backup");Directory.CreateDirectory(Path.Combine(backupRoot,".backups"));File.WriteAllText(Path.Combine(backupRoot,".backups","repair.zip"),"backup zip");
var backups=BackupCleanup.Preview(backupRoot);Require(backups.Count==2&&File.Exists(bak),"Backup preview changed files or missed backups");File.AppendAllText(bak,"changed");
try{BackupCleanup.Delete(backups.Single(e=>!e.Directory));throw new Exception("Changed backup deleted");}catch(IOException){}
Require(File.Exists(bak)&&File.Exists(Path.Combine(backupRoot,"Game.cs")),"Cleanup touched changed backups or source");
Console.WriteLine("PASS framework/network profiles, source preservation, XNA/F1 availability, plain MonoGame defaults, backup preview and changed-item rejection");
Console.WriteLine("FIXTURES "+root);

long deleted=0;
var remaining=BackupCleanup.Preview(backupRoot);
foreach(var entry in remaining)BackupCleanup.Delete(entry,bytes=>deleted+=bytes);
Require(deleted=="backupchanged".Length+"backup zip".Length,"Wrong deleted-byte count");
Require(!File.Exists(bak)&&!Directory.Exists(Path.Combine(backupRoot,".backups"))&&File.Exists(Path.Combine(backupRoot,"Game.cs")),"Permanent cleanup did not preserve source");
Console.WriteLine("PASS permanent backup deletion and exact space-saved accounting");
