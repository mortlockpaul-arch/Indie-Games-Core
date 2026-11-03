using System.IO;
using System.Xml.Linq;
using IndieGames.BuildManager.Models;
namespace IndieGames.BuildManager.Services;

public sealed record ProjectScanProgress(int FoldersScanned, int TotalFolders, int ProjectsFound, string Folder, bool Discovering);
public static class ProjectScanner
{
    public static List<GameProject> Scan(string root, CancellationToken token=default, IProgress<ProjectScanProgress>? progress=null)
    {
        if (!Directory.Exists(root)) return [];
        var folders=new List<string>(); var pending=new Stack<string>(); pending.Push(Path.GetFullPath(root));
        long last=0;
        void Report(ProjectScanProgress value, bool force=false)
        {
            long now=Environment.TickCount64;
            if (force || now-last>=100) { progress?.Report(value); last=now; }
        }
        while (pending.TryPop(out string? folder))
        {
            token.ThrowIfCancellationRequested();
            if ((File.GetAttributes(folder)&FileAttributes.ReparsePoint)!=0) continue;
            folders.Add(folder);
            Report(new(0,folders.Count,0,folder,true));
            foreach (string child in Directory.EnumerateDirectories(folder))
                if (Path.GetFileName(child) is not ("obj" or "bin" or ".git" or ".vs")) pending.Push(child);
        }
        var result=new List<GameProject>();
        Report(new(0,folders.Count,0,root,false),true);
        for (int i=0;i<folders.Count;i++)
        {
            token.ThrowIfCancellationRequested();
            foreach (string file in Directory.EnumerateFiles(folders[i],"*.csproj"))
            {
                token.ThrowIfCancellationRequested();
                try
                {
                    var doc=XDocument.Load(file);
                    string Read(string name) {
                        var saved = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "PropertyGroup" &&
                            (string?)e.Attribute("Label") == ProjectArchitectureUpdater.Label)?
                            .Elements().FirstOrDefault(e => e.Name.LocalName == name);
                        return saved?.Value.Trim() ?? string.Join("; ",doc.Descendants().Where(e=>e.Name.LocalName==name).Select(e=>e.Value.Trim()).Where(v=>v.Length>0).Distinct());
                    }
                    result.Add(new GameProject {
                        Name=Path.GetFileNameWithoutExtension(file),Path=file,
                        TargetFramework=Read("TargetFrameworks") is { Length:>0 } multi ? multi : Read("TargetFramework"),
                        PlatformTarget=Read("PlatformTarget"),Platforms=Read("Platforms"),
                        RuntimeIdentifier=Read("RuntimeIdentifiers") is { Length:>0 } ris ? ris : Read("RuntimeIdentifier"),OutputType=Read("OutputType") });
                }
                catch (System.Xml.XmlException) { }
            }
            Report(new(i+1,folders.Count,result.Count,folders[i],false),i==folders.Count-1);
        }
        return result.OrderBy(p=>p.Name,StringComparer.OrdinalIgnoreCase).ToList();
    }
}
