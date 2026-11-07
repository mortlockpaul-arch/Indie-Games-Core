using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using IndieGames.BuildManager.Services;
using MonoTorrent;

string root=Path.Combine(Path.GetTempPath(),"GlobalCatalogueTests-"+Guid.NewGuid().ToString("N")); Directory.CreateDirectory(root);
using var deadline=new CancellationTokenSource(TimeSpan.FromMinutes(2)); var token=deadline.Token;
string id=Guid.NewGuid().ToString("N"), package=Path.Combine(root,id+".zip"); await File.WriteAllTextAsync(package,"synthetic test package",token);
string torrentFile=Path.Combine(root,"test.torrent"); await new TorrentCreator(TorrentType.V1Only).CreateAsync(new TorrentFileSource(package),torrentFile,token);
byte[] metadata=await File.ReadAllBytesAsync(torrentFile,token); var torrent=await Torrent.LoadAsync(metadata);
var project=new TorrentProject { PackageVersion="1.0.1",Id=id,Name="Synthetic approval test",FolderName="Example",PackageName=id+".zip",PackageBytes=new FileInfo(package).Length,
    UnpackedBytes=4,FileCount=1,Sha256=Convert.ToHexString(SHA256.HashData(await File.ReadAllBytesAsync(package,token))),
    InfoHash=torrent.InfoHashes.V1!.ToHex(),TorrentBase64=Convert.ToBase64String(metadata),CreatedUtc=DateTimeOffset.UtcNow };
var accepted=new TorrentCatalogue { Projects=[project] };
string state=Path.Combine(root,"cache");
var online=new GlobalCatalogue(state,_=>Task.FromResult(accepted));
if((await online.RefreshAsync(token)).Offline)throw new Exception("Online refresh marked offline");
var offline=new GlobalCatalogue(state,_=>throw new HttpRequestException("offline"));
var cached=await offline.RefreshAsync(token);
if(!cached.Offline || cached.Catalogue.Projects.Single().InfoHash!=project.InfoHash)throw new Exception("Offline cache was lost");
var empty=await new GlobalCatalogue(Path.Combine(root,"first-run"),_=>throw new HttpRequestException()).RefreshAsync(token);
if(!empty.Offline || empty.Catalogue.Projects.Count!=0)throw new Exception("Offline first run failed");
string blocked=Path.Combine(root,"blocked-cache");await File.WriteAllTextAsync(blocked,"not a directory",token);
var uncached=await new GlobalCatalogue(blocked,_=>Task.FromResult(accepted)).RefreshAsync(token);
if(uncached.Offline || uncached.Catalogue.Projects.Count!=1)throw new Exception("A cache write failure blocked online browsing");
var invalid=new GlobalCatalogue(state,async ct=>await TorrentCatalogue.ParseAsync(Encoding.UTF8.GetBytes("{\"Version\":999,\"Projects\":[]}"),ct));
if(!(await invalid.RefreshAsync(token)).Offline)throw new Exception("Invalid online catalogue did not preserve cache");
using(var cancelled=new CancellationTokenSource()) {
    cancelled.Cancel();
    try {await new GlobalCatalogue(state,ct=>Task.FromCanceled<TorrentCatalogue>(ct)).RefreshAsync(cancelled.Token);throw new Exception("Cancellation swallowed");}catch(OperationCanceledException){}
}
Console.WriteLine("PASS approved catalogue loading, offline first run, verified cache, invalid-download preservation and cancellation");

Task<TorrentCatalogue> NoApproved(CancellationToken _) => Task.FromResult(new TorrentCatalogue());
var api=new FakeApi(); var publisher=new CataloguePublisher(api,NoApproved);
var submission=await publisher.SubmitAsync([project],token);
if(submission.State!="Awaiting approval" || !submission.Url.EndsWith("/1"))throw new Exception("Submission was not pending");
if(!api.Calls.Any(c=>c.Path.EndsWith("/forks")) || !api.Calls.Any(c=>c.Path.EndsWith("/pulls") && c.Method==HttpMethod.Post))throw new Exception("Fork/PR flow was skipped");
if(api.Calls.Any(c=>c.Path.Contains("refs/heads/main") && c.Method!=HttpMethod.Get))throw new Exception("Submission wrote to main");
var tree=api.Calls.Single(c=>c.Path.EndsWith("/git/trees")).Body!.Value;
var file=tree.GetProperty("tree")[0];
if(file.GetProperty("path").GetString()!="entries/"+project.InfoHash.ToLowerInvariant()+".json")throw new Exception("Wrong submission path");
var uploaded=await TorrentCatalogue.ParseAsync(Encoding.UTF8.GetBytes(file.GetProperty("content").GetString()!),token);
if(uploaded.Projects.Single().PackageVersion!="1.0.1" || !api.Calls.Single(c=>c.Path.EndsWith("/pulls") && c.Method==HttpMethod.Post).Body!.Value.GetProperty("body").GetString()!.Contains("Version 1.0.1"))throw new Exception("Submission version lost");
if(uploaded.Projects.Single().InfoHash!=project.InfoHash)throw new Exception("Submission corrupted metadata");
int mutations=api.Calls.Count(c=>c.Method!=HttpMethod.Get);
if((await publisher.SubmitAsync([project],token)).Url!=submission.Url || api.Calls.Count(c=>c.Method!=HttpMethod.Get)!=mutations)throw new Exception("Retry created duplicate request");
api.Closed=true;
if((await publisher.SubmitAsync([project],token)).State!="Closed")throw new Exception("Rejected submission silently reopened");
Console.WriteLine("PASS contributor fork, metadata-only PR, approval gate, idempotent retry and closed-request handling");

var ownerApi=new FakeApi { Login=GlobalCatalogue.Owner };
await new CataloguePublisher(ownerApi,NoApproved).SubmitAsync([project],token);
if(ownerApi.Calls.Any(c=>c.Path.EndsWith("/forks")) || !ownerApi.Calls.Any(c=>c.Path.EndsWith("/pulls") && c.Method==HttpMethod.Post))throw new Exception("Owner submission bypassed review");
var approvedApi=new FakeApi();
if((await new CataloguePublisher(approvedApi,_=>Task.FromResult(accepted)).SubmitAsync([project],token)).State!="Approved" || approvedApi.Calls.Count!=0)throw new Exception("Already-approved package was resubmitted");
var failedApi=new FakeApi { FailPull=true }; var retryPublisher=new CataloguePublisher(failedApi,NoApproved);
try{await retryPublisher.SubmitAsync([project],token);throw new Exception("Expected interrupted submission");}catch(HttpRequestException){}
failedApi.FailPull=false;
await retryPublisher.SubmitAsync([project],token);
if(!failedApi.Calls.Any(c=>c.Method==HttpMethod.Patch && c.Path.Contains("/git/refs/heads/submission-")))throw new Exception("Partial submission did not reuse branch");
Console.WriteLine("PASS owner review path, approved-entry deduplication and recovery after PR creation failure");

var view=new CatalogueLocalState(); view.Hidden.Add(project.InfoHash); view.Submissions[project.InfoHash]=submission; view.Save(state);
var restored=CatalogueLocalState.Load(state);
if(!restored.Hidden.Contains(project.InfoHash) || restored.Publication(project,new HashSet<string>())!="Awaiting approval" || restored.Publication(project,new HashSet<string>{project.InfoHash})!="Approved")throw new Exception("Local publication state failed");
var handler=new CaptureHandler(); using(var http=new GitHubApi("synthetic-not-a-secret",handler))await http.SendAsync(HttpMethod.Post,"repos/example/test/git/trees",new{value="metadata"},token);
if(handler.Authorization!="Bearer synthetic-not-a-secret" || handler.Body!.Contains("synthetic-not-a-secret"))throw new Exception("Credential leaked into request data");
Console.WriteLine("PASS local-only hiding, pending/approved separation and credential confinement to the authorization header");
string fixture=Path.Combine(root,"entry.json"); await accepted.SaveAsync(fixture,token);
Console.WriteLine("VALIDATOR_FIXTURE "+fixture);

// Opt-in integration check against the dedicated catalogue repository. Never merges a request.
if (args.Contains("--live"))
{
    string credential=Environment.GetEnvironmentVariable("BM_CATALOGUE_TEST_CREDENTIAL") ?? throw new Exception("Live test credential is missing.");
    using var live=new GitHubApi(credential);
    var result=await new CataloguePublisher(live).SubmitAsync([project],token);
    Console.WriteLine("LIVE_TEST_REQUEST "+result.Url);
    Console.WriteLine("LIVE_TEST_BRANCH "+result.Branch);
}

sealed class FakeApi : IGitHubApi
{
    public string Login="contributor"; public bool Closed,FailPull; private string? _branch; private bool _hasBranch,_hasPull;
    public List<(HttpMethod Method,string Path,JsonElement? Body)> Calls=[];
    static JsonElement Json(object value)=>JsonSerializer.SerializeToElement(value);
    public Task<JsonElement> SendAsync(HttpMethod method,string path,object? body,CancellationToken token)
    {
        token.ThrowIfCancellationRequested(); JsonElement? data=body==null?null:Json(body); Calls.Add((method,path,data));
        if(path=="user")return Task.FromResult(Json(new{login=Login}));
        if(path.Contains("/pulls?"))return Task.FromResult(_hasPull ? Json(new[]{new{head=new{@ref=_branch},state=Closed?"closed":"open",merged_at=(string?)null,html_url=GlobalCatalogue.Website+"/pull/1"}}) : Json(Array.Empty<object>()));
        if(path.EndsWith("/forks"))return Task.FromResult(Json(new{full_name=Login+"/"+GlobalCatalogue.Repository}));
        if(path=="repos/"+Login+"/"+GlobalCatalogue.Repository)return Task.FromResult(Json(new{parent=new{full_name=GlobalCatalogue.RepositoryName}}));
        if(path.Contains("/git/ref/heads/submission-") && !_hasBranch)throw new GitHubApiException(HttpStatusCode.NotFound,"not found");
        if(path.Contains("/git/ref/"))return Task.FromResult(Json(new{@object=new{sha="base-sha"}}));
        if(path.Contains("/git/commits/") && method==HttpMethod.Get)return Task.FromResult(Json(new{tree=new{sha="base-tree"}}));
        if(path.EndsWith("/git/trees"))return Task.FromResult(Json(new{sha="new-tree"}));
        if(path.EndsWith("/git/commits"))return Task.FromResult(Json(new{sha="new-commit"}));
        if(path.EndsWith("/git/refs") || method==HttpMethod.Patch) { _branch=data?.TryGetProperty("ref",out var reference)==true?reference.GetString()!["refs/heads/".Length..]:_branch; _hasBranch=true;return Task.FromResult(Json(new{sha="new-commit"})); }
        if(path.EndsWith("/pulls")) { if(FailPull)throw new HttpRequestException("Simulated interrupted response");_hasPull=true;return Task.FromResult(Json(new{html_url=GlobalCatalogue.Website+"/pull/1"})); }
        throw new Exception("Unexpected GitHub call: "+method+" "+path);
    }
}
sealed class CaptureHandler : HttpMessageHandler
{
    public string? Authorization,Body;
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)
    { Authorization=request.Headers.Authorization?.ToString();Body=request.Content==null?null:await request.Content.ReadAsStringAsync(token);return new(HttpStatusCode.OK){Content=new StringContent("{}")}; }
}
