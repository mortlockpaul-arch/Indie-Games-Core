using System.Text.Json;
using IndieGames.BuildManager.Services;
string root=Path.Combine(Path.GetTempPath(),"ZXBox integration "+Guid.NewGuid().ToString("N"));
foreach(string backend in new[]{"ZXBox.FNA","ZXBox.Monogame"}) {
 string directory=Path.Combine(root,"Platforms",backend); Directory.CreateDirectory(directory);
 File.WriteAllText(Path.Combine(directory,backend+".csproj"),"<Project/>");
}
var storage=new LibraryStorage {ZxBoxRoot=root,BuildCache=Path.Combine(root,"cache & outputs"),Solution=Path.Combine(root,"core","Games.slnx")};
var fna=ZxBoxLauncher.Plan(storage,"FNA"); var mono=ZxBoxLauncher.Plan(storage,"MonoGame");
if(fna.Output==mono.Output || fna.Executable==mono.Executable || fna.GamesFolder!=mono.GamesFolder)throw new Exception("Backends must isolate output and share their games library");
if(!fna.BuildArguments.Contains(fna.Project) || !fna.BuildArguments.Contains(fna.Output) || !fna.BuildArguments.Contains("-p:PlatformTarget=x64"))throw new Exception("Build paths/architecture lost");
if(JsonSerializer.Deserialize<LibraryStorage>(JsonSerializer.Serialize(storage))!.ZxBoxRoot!=root)throw new Exception("Checkout preference did not persist");
try { ZxBoxLauncher.Plan(storage,"../unexpected"); throw new Exception("Invalid backend accepted"); } catch(ArgumentException){}
try { ZxBoxLauncher.Plan(new LibraryStorage{ZxBoxRoot="relative"},"FNA"); throw new Exception("Relative checkout accepted"); } catch(IOException){}
try { ZxBoxLauncher.Start(fna); throw new Exception("Missing output accepted"); } catch(FileNotFoundException){}
Console.WriteLine("PASS: backend selection, isolated output, shared library, x64 build arguments, paths with spaces, preference round trip and missing-output validation");
