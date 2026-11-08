using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

public sealed record GameBuildProfile(string Framework, string Network, string Targets, bool ConvertXna, string AdapterProperties = "")
{
    public IEnumerable<string> Arguments => Targets.Length==0 ? [] : new[] { "-p:CustomAfterDirectoryBuildTargets="+Targets, "-p:NetworkBackend=None" }.Concat(AdapterProperties.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(p=>"-p:"+p));
}
public sealed record GameBuildCapabilities(string Framework, bool ManagedSource, bool Overlay, string Reason);
public static class GameBuildProfiles
{
    static string Name(XElement e) => e.Attribute("Include")?.Value ?? "";
    static string Property(XDocument xml,string name) => xml.Descendants().LastOrDefault(e=>e.Name.LocalName==name)?.Value.Trim() ?? "";
    static IEnumerable<XElement> References(XDocument xml) => xml.Descendants().Where(e=>e.Name.LocalName is "ProjectReference" or "PackageReference" or "Reference");
    static bool IsFna(string value) => value.Replace('\\','/').Split('/').Last().ToLowerInvariant() is "fna.csproj" or "fna.core.csproj" or "fna.contents.csproj" or "fna" or "fna.dll";
    static bool IsMono(string value) => value.Contains("MonoGame.Framework",StringComparison.OrdinalIgnoreCase) || value.EndsWith("Monogame.Contents.csproj",StringComparison.OrdinalIgnoreCase);
    static bool IsShim(string value) => new[]{"FNA.NetStub","FNA.Steamworks","FNA.AvatarCompat","IndieGames.Compatibility.MonoGame","IndieGames.Overlay."}.Any(s=>value.Contains(s,StringComparison.OrdinalIgnoreCase));
    static string Detect(XDocument xml)
    {
        var refs=References(xml).Select(Name).ToArray();
        bool fna=refs.Any(IsFna), mono=refs.Any(IsMono);
        return fna&&mono?"Mixed":fna?"FNA":mono?"MonoGame":refs.Any(r=>r.StartsWith("Microsoft.Xna.Framework",StringComparison.OrdinalIgnoreCase))?"XNA":"Unknown";
    }
    public static GameBuildCapabilities Inspect(string project)
    {
        try {
            var xml=XDocument.Load(project);string framework=Detect(xml);
            bool source=xml.Root?.Attribute("Sdk")!=null && System.Text.RegularExpressions.Regex.IsMatch(Property(xml,"TargetFramework"),@"^net(?:9|[1-9]\d)\.\d-windows") && Property(xml,"OutputType") is "Exe" or "WinExe";
            bool overlay=source && framework is "FNA" or "MonoGame";
            string reason=overlay?framework+" source project: F1 supports this framework; custom hosts still need an adapter."
                :framework=="XNA"?"F1 unavailable for XNA. Convert and rebuild a supported .NET 9+ Windows source project first."
                :"Framework switching and F1 require a .NET 9+ Windows executable source project with one identified FNA/MonoGame framework.";
            return new(framework,source,overlay,reason);
        } catch(Exception e) when(e is IOException or System.Xml.XmlException or UnauthorizedAccessException){return new("Unknown",false,false,e.Message);}
    }
    static string FullPath(string value,string project,string core) => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(project)!,value.Replace("$(IndieGamesCoreRoot)",core,StringComparison.OrdinalIgnoreCase).Replace('\\',Path.DirectorySeparatorChar)));
    public static GameBuildProfile Prepare(string project,string requested,string network,string monoPlatform,string monoVersion,string core,string cache)
    {
        project=Path.GetFullPath(project);core=Path.GetFullPath(core);
        var xml=XDocument.Load(project);var capability=Inspect(project);string detected=capability.Framework;
        bool convert=requested=="XNA → FNA";
        string framework=requested=="Project default"?detected:convert?"FNA":requested;
        if(framework is not ("FNA" or "MonoGame")) {
            if(requested!="Project default" || network!="Project default")throw new InvalidOperationException("Identify a compatible FNA/MonoGame source project before choosing a framework or network adapter.");
            return new(framework,"Project default","",false);
        }
        if((requested!="Project default" || network!="Project default") && !capability.ManagedSource)throw new InvalidOperationException(capability.Reason);
        if(convert && detected is not ("XNA" or "FNA"))throw new InvalidOperationException("XNA → FNA is for XNA/FNA source, not MonoGame or binary-only ports.");
        var refs=References(xml).ToArray();
        if(detected!=framework && detected is "FNA" or "MonoGame") {
            foreach(var reference in refs.Where(e=>e.Name.LocalName=="ProjectReference" && !IsFna(Name(e)) && !IsMono(Name(e)) && !IsShim(Name(e)))) {
                string path=FullPath(Name(reference),project,core);
                if(!File.Exists(path) || InspectDependency(path,new()).Any(f=>f!=framework))throw new InvalidOperationException("Cannot switch this project's framework automatically: dependency "+Name(reference)+" must be ported to "+framework+" first.");
            }
            foreach(var reference in refs.Where(e=>e.Name.LocalName=="Reference" && !IsFna(Name(e)) && !IsMono(Name(e)) && !IsShim(Name(e)) && !Name(e).StartsWith("System",StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Review binary reference "+Name(reference)+" for framework compatibility before switching renderers.");
        }
        bool networkChoiceDefault=network=="Project default";
        if(network=="Project default") {
            network=refs.Any(r=>Name(r).Contains("Steamworks",StringComparison.OrdinalIgnoreCase))?"Steamworks":
                framework=="FNA" || refs.Any(r=>Name(r).Contains("NetStub",StringComparison.OrdinalIgnoreCase)||Name(r).Contains("Monogame.Contents",StringComparison.OrdinalIgnoreCase))?"NetStub":"None";
        }
        if(requested=="Project default" && networkChoiceDefault && monoPlatform=="Project default")return new(framework,network,"",false);
        if(network is not ("None" or "NetStub" or "Steamworks"))throw new InvalidOperationException("Choose Project default, None, NetStub or Steamworks.");
        if(network!="None" && !capability.ManagedSource)throw new InvalidOperationException("The managed network adapters require a .NET 9+ Windows source project. Use Project default / None for other MonoGame targets.");
        var document=new XDocument(new XElement("Project"));var root=document.Root!;
        string mainCondition="'$(MSBuildProjectFullPath)' == '$(BuildManagerMainProject)'";
        var items=new XElement("ItemGroup",new XAttribute("Condition",mainCondition));root.Add(items);
        foreach(var reference in refs.Where(e=>IsFna(Name(e))||IsMono(Name(e))||IsShim(Name(e))||Name(e).StartsWith("Microsoft.Xna.Framework",StringComparison.OrdinalIgnoreCase)))
            items.Add(new XElement(reference.Name.LocalName,new XAttribute("Remove",Name(reference))));
        foreach(string relative in new[]{"FNA.NetStub/FNA.NetStub.csproj","FNA.Steamworks/FNA.Steamworks.net9.csproj","FNA.AvatarCompat/FNA.AvatarCompat.csproj"})
            items.Add(new XElement("ProjectReference",new XAttribute("Remove",Path.Combine(core,relative).Replace('/',Path.DirectorySeparatorChar))));
        var renderer=new XElement("ProjectReference");string properties="";
        if(framework=="FNA") {
            string path=Path.Combine(core,refs.Any(r=>Name(r).Contains(".Contents",StringComparison.OrdinalIgnoreCase))||convert?"FNA.Contents/FNA.Contents.csproj":"FNA/FNA.Core.csproj");
            renderer.SetAttributeValue("Include",path);RequireFile(path);items.Add(renderer);
            if(network!="None") {string adapter=Path.Combine(core,network=="NetStub"?"FNA.NetStub/FNA.NetStub.csproj":"FNA.AvatarCompat/FNA.AvatarCompat.csproj");RequireFile(adapter);items.Add(new XElement("ProjectReference",new XAttribute("Include",adapter)));}
        } else {
            var existing=refs.FirstOrDefault(e=>IsMono(Name(e)));
            if(monoPlatform=="Project default" && existing!=null) {
                if(Name(existing).Contains("Monogame.Contents",StringComparison.OrdinalIgnoreCase)) {
                    string contents=FullPath(Name(existing),project,core);var nested=XDocument.Load(contents).Descendants().First(e=>e.Name.LocalName=="ProjectReference"&&IsMono(Name(e)));
                    existing=new XElement("ProjectReference",new XAttribute("Include",FullPath(Name(nested),contents,core)));
                }
                var copy=new XElement(existing);copy.Name=copy.Name.LocalName;items.Add(copy);
                if(copy.Name.LocalName=="PackageReference") {
                    string version=copy.Attribute("Version")?.Value??copy.Elements().FirstOrDefault(e=>e.Name.LocalName=="Version")?.Value??"";
                    if(version.Length==0 || version.Contains('$'))throw new InvalidOperationException("Resolve the MonoGame package version before selecting a managed build profile.");
                    properties="MonoGamePackage="+Name(copy)+";MonoGamePackageVersion="+version;
                } else if(copy.Name.LocalName=="ProjectReference")properties="MonoGameProject="+FullPath(Name(copy),project,core);
                else {string hint=copy.Elements().First(e=>e.Name.LocalName=="HintPath").Value;properties="MonoGameAssembly="+FullPath(hint,project,core);}
            } else {
                string package="MonoGame.Framework."+(monoPlatform=="WindowsDX"?"WindowsDX":"DesktopGL");
                if(!System.Text.RegularExpressions.Regex.IsMatch(monoVersion,@"^\d+\.\d+\.\d+(?:[.\-][0-9A-Za-z.\-]+)?$"))throw new InvalidOperationException("Enter a concrete MonoGame package version.");
                items.Add(new XElement("PackageReference",new XAttribute("Include",package),new XAttribute("Version",monoVersion)));
                properties="MonoGamePackage="+package+";MonoGamePackageVersion="+monoVersion;
            }
            properties+=";MonoGameVariant="+Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(properties)))[..12];
            if(network!="None") {
                string adapter=Path.Combine(core,"IndieGames.Compatibility.MonoGame/IndieGames.Compatibility.MonoGame.csproj");RequireFile(adapter);
                items.Add(new XElement("ProjectReference",new XAttribute("Include",adapter),new XAttribute("AdditionalProperties",properties+";GameNetworkBackend="+network)));
            }
        }
        var overlay=refs.FirstOrDefault(r=>Name(r).Contains("IndieGames.Overlay.",StringComparison.OrdinalIgnoreCase));
        if(overlay!=null) {
            string path=Path.Combine(core,"IndieGames.Overlay."+framework,"IndieGames.Overlay."+framework+".csproj");RequireFile(path);
            var reference=new XElement("ProjectReference",new XAttribute("Include",path));if(properties.Length>0)reference.SetAttributeValue("AdditionalProperties",properties);items.Add(reference);
        }
        if(framework=="MonoGame") {
            // These are archive-wide FNA deployment hooks, not MonoGame dependencies.
            string nativeOff=Path.Combine(cache,"framework-profiles","MonoGame.Native.targets");Directory.CreateDirectory(Path.GetDirectoryName(nativeOff)!);
            var off=new XDocument(new XElement("Project",new[]{"CopyXbligNativeDependencies","CopyTheorafileNative","RemoveUnusedSteamworksDependencies"}.Select(n=>new XElement("Target",new XAttribute("Name",n)))));
            WriteIfChanged(nativeOff,off.ToString());root.Add(new XElement("Import",new XAttribute("Project",nativeOff),new XAttribute("Condition",mainCondition)));
        }
        var staleNames = framework=="MonoGame"
            ? new[]{"FNA.dll","FNA.Contents.dll","FNA.NetStub.dll","FNA.Steamworks.dll","FNA.AvatarCompat.dll","IndieGames.Overlay.FNA.dll"}
            : new[]{"MonoGame.Framework.dll","Monogame.Contents.dll","IndieGames.MonoGame.NetStub.dll","IndieGames.MonoGame.Steamworks.dll","IndieGames.Overlay.MonoGame.dll"};
        var cleanup=new XElement("Target",new XAttribute("Name","BuildManagerRemoveObsoleteFrameworkAssemblies"),new XAttribute("AfterTargets","Build"),new XAttribute("Condition",mainCondition+" and '$(TargetDir)' != '' and ('$(OutputType)' == 'Exe' or '$(OutputType)' == 'WinExe')"));
        cleanup.Add(new XElement("ItemGroup",new XElement("_BuildManagerStaleFrameworkFile",new XAttribute("Include",string.Join(";",staleNames.Select(n=>"$(TargetDir)"+n))),new XAttribute("Exclude","@(ReferenceCopyLocalPaths->'$(TargetDir)%(DestinationSubDirectory)%(Filename)%(Extension)')"))));
        cleanup.Add(new XElement("Delete",new XAttribute("Files","@(_BuildManagerStaleFrameworkFile)")));root.Add(cleanup);
        string identity=project+"|"+framework+"|"+network+"|"+properties;
        string target=Path.Combine(cache,"framework-profiles",Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(identity)))+".targets");
        Directory.CreateDirectory(Path.GetDirectoryName(target)!);WriteIfChanged(target,document.ToString());
        return new(framework,network,target,convert,framework=="MonoGame" ? properties+";GameNetworkBackend="+network : "");
    }
    static IEnumerable<string> InspectDependency(string path,HashSet<string> seen) {
        if(!seen.Add(path))yield break;
        var xml=XDocument.Load(path);string framework=Detect(xml);if(framework is "FNA" or "MonoGame" or "Mixed")yield return framework;
        foreach(var e in References(xml).Where(e=>e.Name.LocalName=="ProjectReference"&&!IsFna(Name(e))&&!IsMono(Name(e))&&!IsShim(Name(e)))) {
            string child=Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path)!,Name(e)));
            if(File.Exists(child))foreach(string item in InspectDependency(child,seen))yield return item;
        }
    }
    static void RequireFile(string file){if(!File.Exists(file))throw new FileNotFoundException("Required framework adapter is missing from the configured core checkout.",file);}
    static void WriteIfChanged(string file,string text){if(!File.Exists(file)||File.ReadAllText(file)!=text)File.WriteAllText(file,text);}
    public static string BackupConversion(string project) {
        string folder=Path.GetDirectoryName(Path.GetFullPath(project))!;string directory=Path.Combine(folder,".backups");Directory.CreateDirectory(directory);
        string path=Path.Combine(directory,"xna-to-fna-"+Guid.NewGuid().ToString("N")+".zip");
        using var zip=ZipFile.Open(path,ZipArchiveMode.Create);
        void Walk(string current) {foreach(var file in Directory.EnumerateFiles(current)){if((File.GetAttributes(file)&FileAttributes.ReparsePoint)==0 && Path.GetExtension(file) is ".cs" or ".csproj")zip.CreateEntryFromFile(file,Path.GetRelativePath(folder,file));}foreach(var child in Directory.EnumerateDirectories(current))if((File.GetAttributes(child)&FileAttributes.ReparsePoint)==0 && Path.GetFileName(child) is not ("bin" or "obj") && !Path.GetFileName(child).StartsWith('.'))Walk(child);}
        Walk(folder);return path;
    }
}
