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
foreach(string folder in new[]{"bin","obj","logs","texture-conversion","584E07D1"}){Directory.CreateDirectory(Path.Combine(project,folder));File.WriteAllText(Path.Combine(project,folder,"excluded.txt"),"excluded");}
File.WriteAllText(Path.Combine(project,"Program.cs.before-fix.bak"),"excluded");File.WriteAllText(Path.Combine(project,".env"),"excluded");
var plan=ProjectBundle.Preview(project);if(plan.Files.Count!=3)throw new Exception("Bad preview exclusion count "+plan.Files.Count);
int Port(){var listener=new TcpListener(IPAddress.Loopback,0);listener.Start();int port=((IPEndPoint)listener.LocalEndpoint).Port;listener.Stop();return port;}
EngineSettings Settings(string name,int port)=>new EngineSettingsBuilder{CacheDirectory=Path.Combine(root,name,"cache"),DhtEndPoint=null,AllowLocalPeerDiscovery=false,AllowPortForwarding=false,AutoSaveLoadDhtCache=false,AutoSaveLoadFastResume=true,MaximumUploadRate=512*1024,ListenEndPoints=new(){["ipv4"]=new IPEndPoint(IPAddress.Loopback,port)}}.ToSettings();
int seedPort=Port();await using var seed=new ProjectTorrentService(Path.Combine(root,"seed"),Settings("seed",seedPort));
using var timeout=new CancellationTokenSource(TimeSpan.FromSeconds(55));var token=timeout.Token;
var entry=await seed.CreateShareAsync(plan,Array.Empty<string>(),token);
string cataloguePath=Path.Combine(root,"catalogue.json");await new TorrentCatalogue{Projects=[entry]}.SaveAsync(cataloguePath,token);
var catalogue=await TorrentCatalogue.LoadAsync(cataloguePath,token);if(catalogue.Projects.Count!=1)throw new Exception("Catalogue round trip failed");
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
var gamePlan=ProjectBundle.Preview(game,packageKind:"Game");
if(gamePlan.Files.Any(f=>f.RelativePath.EndsWith(".pdb")||f.RelativePath.EndsWith(".cs")))throw new Exception("Game package included development files");
var gameEntry=await seed.CreateShareAsync(gamePlan,[],token);
if(gameEntry.PackageKind!="Game")throw new Exception("Game kind lost");
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
Console.WriteLine("TEST ROOT "+root);


