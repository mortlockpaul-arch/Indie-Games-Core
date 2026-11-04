using IndieGames.BuildManager.Services;
using MonoTorrent;
using MonoTorrent.Client;
using System.Net;
using System.Net.Sockets;
using System.IO.Compression;
using System.Security.Cryptography;
string root=(args.Length > 0 ? Path.GetFullPath(args[0]) : Path.Combine(Path.GetTempPath(),"IndieGamesTorrentTest-"+Guid.NewGuid().ToString("N")));Directory.CreateDirectory(root);
string project=Path.Combine(root,"Demo Indie Project");Directory.CreateDirectory(Path.Combine(project,"Content"));
File.WriteAllText(Path.Combine(project,"Demo.csproj"),"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net10.0</TargetFramework></PropertyGroup></Project>");
File.WriteAllText(Path.Combine(project,"Program.cs"),"// synthetic test source");
byte[] content=new byte[2*1024*1024];RandomNumberGenerator.Fill(content);File.WriteAllBytes(Path.Combine(project,"Content","test.bin"),content);
foreach(string folder in new[]{"bin","obj","logs","texture-conversion","shader-repair-backups","font-repair-backups","video-conversion","584E07D1"}){Directory.CreateDirectory(Path.Combine(project,folder));File.WriteAllText(Path.Combine(project,folder,"excluded.txt"),"excluded");}
File.WriteAllText(Path.Combine(project,"Program.cs.before-fix.bak"),"excluded");File.WriteAllText(Path.Combine(project,".env"),"excluded");
var plan=ProjectBundle.Preview(project);if(plan.Files.Count!=3)throw new Exception("Bad preview exclusion count "+plan.Files.Count);
int Port(){var listener=new TcpListener(IPAddress.Loopback,0);listener.Start();int port=((IPEndPoint)listener.LocalEndpoint).Port;listener.Stop();return port;}
EngineSettings Settings(string name,int port)=>new EngineSettingsBuilder{CacheDirectory=Path.Combine(root,name,"cache"),DhtEndPoint=null,AllowLocalPeerDiscovery=false,AllowPortForwarding=false,AutoSaveLoadDhtCache=false,AutoSaveLoadFastResume=true,MaximumUploadRate=512*1024,ListenEndPoints=new(){["ipv4"]=new IPEndPoint(IPAddress.Loopback,port)}}.ToSettings();
int seedPort=Port();await using var seed=new ProjectTorrentService(Path.Combine(root,"seed"),Settings("seed",seedPort));
using var timeout=new CancellationTokenSource(TimeSpan.FromSeconds(55));var token=timeout.Token;
string icon="iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+jD34AAAAASUVORK5CYII=";
var entry=await seed.CreateShareAsync(plan,Array.Empty<string>(),token,icon);
File.SetLastWriteTimeUtc(Path.Combine(project,"Program.cs"),DateTime.UtcNow.AddMinutes(1));
var reused=await seed.CreateShareAsync(plan,[],token);
if(reused.Id!=entry.Id || reused.InfoHash!=entry.InfoHash || Directory.GetDirectories(Path.Combine(seed.StateDirectory,"shares")).Length!=1)throw new Exception("Unchanged source must reuse its snapshot without orphan packages.");
await new TorrentCatalogue{Projects=[entry with{ContentSha256=""}]}.SaveAsync(seed.LocalCataloguePath,token);
var legacyReuse=await seed.CreateShareAsync(plan,[],token);
if(legacyReuse.Id!=entry.Id || legacyReuse.ContentSha256.Length!=64)throw new Exception("Legacy snapshots must be compared by unpacked content.");
var inspected=await seed.InspectContentsAsync(entry,token);
if(inspected.Files.Count!=3 || inspected.Fingerprint!=reused.ContentSha256)throw new Exception("Snapshot inspection mismatch.");
Console.WriteLine("PASS unchanged and legacy snapshot reuse, metadata-independent content hashes, inspection and cleanup");
string cataloguePath=Path.Combine(root,"catalogue.json");await new TorrentCatalogue{Projects=[entry]}.SaveAsync(cataloguePath,token);
var catalogue=await TorrentCatalogue.LoadAsync(cataloguePath,token);if(catalogue.Projects.Count!=1 || catalogue.Projects[0].IconBase64!=icon)throw new Exception("Catalogue artwork round trip failed");
foreach(string badIcon in new[]{"not base64",Convert.ToBase64String(new byte[70]),new string('A',65540)}) {
    try{TorrentCatalogue.ValidateIcon(badIcon);throw new Exception("Invalid artwork accepted");}catch(InvalidDataException){}
}
byte[] hugeIcon=Convert.FromBase64String(icon);System.Buffers.Binary.BinaryPrimitives.WriteUInt32BigEndian(hugeIcon.AsSpan(16,4),5000);
try{TorrentCatalogue.ValidateIcon(Convert.ToBase64String(hugeIcon));throw new Exception("Oversized icon dimensions accepted");}catch(InvalidDataException){}
var legacyJson=System.Text.Json.Nodes.JsonNode.Parse(await File.ReadAllTextAsync(cataloguePath,token))!;
legacyJson["Projects"]![0]!.AsObject().Remove("IconBase64");
string legacyPath=Path.Combine(root,"legacy-catalogue.json");await File.WriteAllTextAsync(legacyPath,legacyJson.ToJsonString(),token);
if((await TorrentCatalogue.LoadAsync(legacyPath,token)).Projects[0].IconBase64!="")throw new Exception("Legacy catalogue compatibility failed");
Console.WriteLine("PASS portable artwork, invalid icon bounds and legacy catalogue compatibility");
var listener=new TcpListener(IPAddress.Loopback,0);listener.Start();int httpPort=((IPEndPoint)listener.LocalEndpoint).Port;
var serve=Task.Run(async()=>{using var client=await listener.AcceptTcpClientAsync(token);await using var stream=client.GetStream();using var requestReader=new StreamReader(stream,System.Text.Encoding.ASCII,false,1024,true);while(await requestReader.ReadLineAsync(token) is {Length:>0} line){if(line.Length>8192)throw new InvalidDataException("Oversized HTTP request");}byte[] body=File.ReadAllBytes(cataloguePath);byte[] header=System.Text.Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nContent-Type: application/json\r\nContent-Length: {body.Length}\r\nConnection: close\r\n\r\n");await stream.WriteAsync(header,token);await stream.WriteAsync(body,token);});
var remoteCatalogue=await TorrentCatalogue.LoadAsync($"http://127.0.0.1:{httpPort}/catalogue.json",token);await serve;listener.Stop();
if(remoteCatalogue.Projects[0].InfoHash!=entry.InfoHash)throw new Exception("URL catalogue mismatch");
Console.WriteLine("PASS catalogue loading over loopback HTTP");
var sharing=await seed.StartAsync(entry,true,token);
while(sharing.Manager.State!=TorrentState.Seeding){token.ThrowIfCancellationRequested();await Task.Delay(100,token);}
Console.WriteLine("PASS snapshot exclusions, catalogue round trip and seeding");
await using var receiver=new ProjectTorrentService(Path.Combine(root,"receiver"),Settings("receiver",Port()));
var download=await receiver.StartAsync(catalogue.Projects[0],false,token);
while(download.Manager.State!=TorrentState.Downloading){token.ThrowIfCancellationRequested();await Task.Delay(100,token);}
await download.Manager.PauseAsync();if(download.Manager.State!=TorrentState.Paused)throw new Exception("Pause failed");await download.Manager.StartAsync();
await download.Manager.AddPeerAsync(new PeerInfo(new Uri("ipv4://127.0.0.1:"+seedPort)));
while(!download.Manager.Complete){token.ThrowIfCancellationRequested();if(download.Manager.State==TorrentState.Error)throw new Exception("Download errored");await Task.Delay(250,token);}
Console.WriteLine("PASS real loopback torrent transfer and pause/resume");
string archive=Path.Combine(root,"installed");string installed=await receiver.InstallAsync(download,archive,token);
if(!File.ReadAllBytes(Path.Combine(installed,"Content","test.bin")).SequenceEqual(content))throw new Exception("Installed content differs");
try{await receiver.InstallAsync(download,archive,token);throw new Exception("Existing install overwritten");}catch(IOException){}
try{await ProjectBundle.InstallAsync(entry with{Sha256=new string('0',64)},download.PackagePath,Path.Combine(root,"wronghash"),token);throw new Exception("Bad checksum accepted");}catch(InvalidDataException){}
string malicious=Path.Combine(root,"unsafe.zip");using(var zip=ZipFile.Open(malicious,ZipArchiveMode.Create)){using var writer=new StreamWriter(zip.CreateEntry("../outside.csproj").Open());writer.Write("evil");}
string maliciousId=Guid.NewGuid().ToString("N");string maliciousPackage=Path.Combine(root,maliciousId+".zip");File.Copy(malicious,maliciousPackage);
string torrentFile=Path.Combine(root,"unsafe.torrent");await new TorrentCreator(TorrentType.V1Only).CreateAsync(new TorrentFileSource(maliciousPackage),torrentFile,token);
byte[] maliciousMetadata=File.ReadAllBytes(torrentFile);var maliciousTorrent=await Torrent.LoadAsync(maliciousMetadata);
var unsafeEntry=entry with{Id=maliciousId,PackageName=maliciousId+".zip",PackageBytes=new FileInfo(maliciousPackage).Length,FileCount=1,UnpackedBytes=4,FolderName="Unsafe project",Sha256=Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(maliciousPackage))),InfoHash=maliciousTorrent.InfoHashes.V1!.ToHex(),TorrentBase64=Convert.ToBase64String(maliciousMetadata)};
try{await ProjectBundle.InstallAsync(unsafeEntry,maliciousPackage,Path.Combine(root,"unsafe-install"),token);throw new Exception("ZIP traversal accepted");}catch(InvalidDataException){}
if(File.Exists(Path.Combine(root,"outside.csproj"))||File.Exists(Path.Combine(root,"unsafe-install","outside.csproj")))throw new Exception("ZIP escaped destination");
await download.Manager.StopAsync();await receiver.StartAsync(entry,false,token);
while(!download.Manager.Complete||!download.Manager.HashChecked){token.ThrowIfCancellationRequested();await Task.Delay(100,token);}
Console.WriteLine("PASS malicious ZIP rejection and stopped-transfer resume");
// Direct path validation is also tested against Windows aliases and traversal.
foreach(string bad in new[]{"..","CON","file:stream","folder\\escape","trailing."}){try{ProjectBundle.ValidateSegment(bad);throw new Exception("Unsafe segment accepted");}catch(InvalidDataException){}}
var cancelled=new CancellationToken(true);try{await ProjectBundle.CreateAsync(plan,Path.Combine(root,"cancelled.zip"),cancelled);throw new Exception("Cancellation ignored");}catch(OperationCanceledException){}
Console.WriteLine("PASS verified install, existing-folder protection, bad checksum, unsafe names and cancellation");

// Playable packages are independent of source and require runtime entry-point metadata.
string game=Path.Combine(root,"Demo playable game");Directory.CreateDirectory(Path.Combine(game,"Content"));
foreach(string name in new[]{"Demo.exe","Demo.dll","Demo.deps.json","Demo.runtimeconfig.json","FAudio.dll","Demo.pdb","Demo.cs"})File.WriteAllText(Path.Combine(game,name),"synthetic fixture");
File.WriteAllBytes(Path.Combine(game,"Content","sprite.bin"),content[..1024]);
var gamePlan=ProjectBundle.Preview(game,packageKind:"Game") with { Name="Demo Game (Shared)" };
if(gamePlan.Files.Any(f=>f.RelativePath.EndsWith(".pdb")||f.RelativePath.EndsWith(".cs")))throw new Exception("Game package included development files");
var gameEntry=await seed.CreateShareAsync(gamePlan,[],token);
if(gameEntry.PackageKind!="Game" || gameEntry.FolderName!="Demo Game (Shared)")throw new Exception("Game kind or stable install-folder name lost");
var both=await TorrentCatalogue.LoadAsync(seed.LocalCataloguePath,token);
if(both.Projects.Count!=2 || both.Projects.Select(p=>p.PackageKind).Distinct().Count()!=2)throw new Exception("Source and game are not separate catalogue packages");
var gameSeed=await seed.StartAsync(gameEntry,true,token);
while(gameSeed.Manager.State!=TorrentState.Seeding){token.ThrowIfCancellationRequested();await Task.Delay(50,token);}
var gameDownload=await receiver.StartAsync(gameEntry,false,token);
while(gameDownload.Manager.State!=TorrentState.Downloading){token.ThrowIfCancellationRequested();await Task.Delay(50,token);}
await gameDownload.Manager.AddPeerAsync(new PeerInfo(new Uri("ipv4://127.0.0.1:"+seedPort)));
while(!gameDownload.Manager.Complete || !gameDownload.Manager.HashChecked){token.ThrowIfCancellationRequested();await Task.Delay(100,token);}
string gamesRoot=Path.Combine(root,"games");
string installedGame=await receiver.InstallAsync(gameDownload,gamesRoot,token);
if(!File.Exists(Path.Combine(installedGame,"Demo.exe")) || Directory.EnumerateFiles(installedGame,"*.csproj").Any())throw new Exception("Game installation requires source");
File.Delete(Path.Combine(game,"Demo.runtimeconfig.json"));
try{ProjectBundle.Preview(game,packageKind:"Game");throw new Exception("Incomplete game accepted");}catch(InvalidDataException){}
try{await TorrentCatalogue.ValidateAsync(entry with{PackageKind="Unknown"});throw new Exception("Unknown package kind accepted");}catch(InvalidDataException){}
Console.WriteLine("PASS independent source/game packages, real playable-package transfer/install and missing-runtime rejection");
var storage=new LibraryStorage {Archive=project,Games=gamesRoot,BuildCache=Path.Combine(root,"build-cache"),TorrentCache=Path.Combine(root,"torrent-cache")};
storage.Validate();
Directory.CreateDirectory(Path.Combine(storage.BuildCache,"obj","test"));File.WriteAllText(Path.Combine(storage.BuildCache,"obj","test","generated.tmp"),"cache");
File.WriteAllText(Path.Combine(storage.BuildCache,"keep.txt"),"not managed cache");
var cacheCleanup=StorageCleanup.Preview(storage,false);
if(cacheCleanup.Count!=1)throw new Exception("Unexpected cache cleanup targets");
foreach(var folder in cacheCleanup)StorageCleanup.Delete(folder);
if(!File.Exists(Path.Combine(storage.BuildCache,"keep.txt")) || Directory.Exists(Path.Combine(storage.BuildCache,"obj")))throw new Exception("Cache cleanup scope error");
var legacy=StorageCleanup.Preview(storage,true);
if(legacy.Count!=2 || legacy.Any(f=>Path.GetFileName(f.Path) is not ("bin" or "obj")))throw new Exception("Legacy cleanup scope error");
try{StorageCleanup.Delete(new CleanupFolder(project,game,0));throw new Exception("Cleanup escaped its root");}catch(IOException){}
try{new LibraryStorage {Archive=project,Games=Path.Combine(project,"games"),BuildCache=storage.BuildCache,TorrentCache=storage.TorrentCache}.Validate();throw new Exception("Nested storage roots accepted");}catch(IOException){}
if(LibraryStorage.ProjectKey(Path.Combine(root,"one","Demo.csproj"))==LibraryStorage.ProjectKey(Path.Combine(root,"two","Demo.csproj")))throw new Exception("Project identities collide");
Console.WriteLine("PASS cleanup boundaries, unrelated-file preservation, legacy bin/obj preview and unique project identities");
File.WriteAllText(Path.Combine(project,"Program.cs"),new string('x',checked((int)new FileInfo(Path.Combine(project,"Program.cs")).Length)));
var duplicate=await seed.CreateShareAsync(plan,[],token);
if(duplicate.Name!=entry.Name || duplicate.InfoHash==entry.InfoHash)throw new Exception("Duplicate fixture must share a name but have a distinct torrent identity");
string originalPackage=sharing.PackagePath;
await seed.RemoveAsync(duplicate,token); // An entry that has never started must also be removable.
var afterReadyRemoval=await TorrentCatalogue.LoadAsync(seed.LocalCataloguePath,token);
if(afterReadyRemoval.Projects.Any(p=>p.InfoHash==duplicate.InfoHash) || !afterReadyRemoval.Projects.Any(p=>p.InfoHash==entry.InfoHash))throw new Exception("Removal selected the wrong same-name snapshot");
await seed.RemoveAsync(entry,token);
var afterActiveRemoval=await TorrentCatalogue.LoadAsync(seed.LocalCataloguePath,token);
if(afterActiveRemoval.Projects.Count!=1 || afterActiveRemoval.Projects[0].InfoHash!=gameEntry.InfoHash || seed.Transfers.Any(t=>t.Project.InfoHash==entry.InfoHash))throw new Exception("Active transfer/catalogue entry survived removal or other package removed");
if(!File.Exists(originalPackage) || !File.Exists(Path.Combine(installedGame,"Demo.exe")) || !File.Exists(Path.Combine(project,"Program.cs")))throw new Exception("Removal deleted package, installed game or source files");
await seed.RemoveAsync(entry,token); // Repeat removal is harmless.
await seed.StartAsync(entry,true,token); // Engine deregistration permits re-adding retained packages.
Console.WriteLine("PASS ready/active removal, same-name snapshot identity, persistent catalogue update, retained files and re-adding transfers");
Console.WriteLine("TEST ROOT "+root);

string selectionRoot=Path.Combine(root,"selection-archive"), selectionGame=Path.Combine(selectionRoot,"Selected Game");
Directory.CreateDirectory(selectionGame);
string selectionProject=Path.Combine(selectionGame,"Selected Game.csproj");
File.WriteAllText(selectionProject,"<Project><PropertyGroup><TargetFramework>net10.0</TargetFramework></PropertyGroup></Project>");
var selectionStorage=new LibraryStorage { Games=Path.Combine(root,"selection-games") };
var selected=ShareSelection.ForProject(selectionRoot,selectionProject,selectionStorage);
if(selected.Source!=selectionGame || selected.Name!="Selected Game" || selected.PlayableGame!=selectionStorage.GameFolder(selectionProject))throw new Exception("Sharing defaults do not follow selected project");
string fallback=Path.Combine(selectionGame,"bin","Release","net10.0-windows"); Directory.CreateDirectory(fallback);
void Playable(string directory){Directory.CreateDirectory(directory);foreach(string name in new[]{"Selected.exe","Selected.deps.json","Selected.runtimeconfig.json"})File.WriteAllText(Path.Combine(directory,name),"fixture");}
Playable(fallback);
if(ShareSelection.ForProject(selectionRoot,selectionProject,selectionStorage).PlayableGame!=fallback)throw new Exception("Legacy playable output was not detected");
Playable(selected.PlayableGame);
if(ShareSelection.ForProject(selectionRoot,selectionProject,selectionStorage).PlayableGame!=selected.PlayableGame)throw new Exception("Configured output must take priority");
try{ShareSelection.ForProject(selectionRoot,Path.Combine(root,"outside.csproj"),selectionStorage);throw new Exception("Outside project accepted");}catch(IOException){}
Console.WriteLine("PASS source/game sharing defaults, legacy output discovery and preferred build output");

Directory.CreateDirectory(Path.Combine(selectionGame,"obj"));File.WriteAllText(Path.Combine(selectionGame,"obj","Excluded.csproj"),"<Project/>");
Directory.CreateDirectory(Path.Combine(selectionGame,"Nested"));File.WriteAllText(Path.Combine(selectionGame,"Nested","Broken.csproj"),"<Project>");
var updates=new List<ProjectScanProgress>();
var scanned=ProjectScanner.Scan(selectionRoot,progress:new InlineProgress<ProjectScanProgress>(updates.Add));
if(scanned.Count!=1 || !updates.Any(p=>p.Discovering) || updates.Last().FoldersScanned!=updates.Last().TotalFolders || updates.Last().ProjectsFound!=1)throw new Exception("Folder scanning/progress totals are incorrect");
using var cancelScan=new CancellationTokenSource();cancelScan.Cancel();
try{ProjectScanner.Scan(selectionRoot,cancelScan.Token);throw new Exception("Scan cancellation ignored");}catch(OperationCanceledException){}
Console.WriteLine("PASS scan discovery, determinate folder progress, excluded outputs, malformed project handling and cancellation");
var live=TorrentStatus.Capture(seed.Transfers.First(t=>t.Project.InfoHash==gameEntry.InfoHash));
if(live.Kind!="Playable Game" || live.Name!=gameEntry.Name)throw new Exception("Live transfer status identity is incorrect");
var snapshot=new TorrentStatusSnapshot([new("One","Source","Seeding",100,2,0,2048),new("Two","Playable Game","Downloading",50,1,1024,0)],"Fixture");
if(!snapshot.Summary.Contains("1 Seeding") || !snapshot.Summary.Contains("1 Downloading") || snapshot.Transfers.Sum(t=>t.UploadRate)!=2048)throw new Exception("Torrent summary aggregation failed");
Console.WriteLine("PASS live transfer status, package labels and aggregate torrent summary");

sealed class InlineProgress<T>(Action<T> report) : IProgress<T> { public void Report(T value)=>report(value); }


