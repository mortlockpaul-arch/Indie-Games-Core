using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace IndieGames.BuildManager.Services;

public static class GameTitleRepair
{
    private const string Marker="// INDIE_BUILD_MANAGER_WINDOW_TITLE";
    public static string Apply(string project)
    {
        var document=XDocument.Load(project);
        string title=document.Descendants().LastOrDefault(e=>e.Name.LocalName=="AssemblyName")?.Value.Trim() ?? Path.GetFileNameWithoutExtension(project);
        string literal="\""+title.Replace("\\","\\\\").Replace("\"","\\\"").Replace("\r"," ").Replace("\n"," ")+"\"";
        string root=Path.GetDirectoryName(project)!;
        var edits=new Dictionary<string,string>();
        var sources=Directory.EnumerateFiles(root,"*.cs",SearchOption.AllDirectories)
            .Where(p=>!p.Split(Path.DirectorySeparatorChar).Any(s=>s is "bin" or "obj" or ".git" or "shader-repair-backups"))
            .ToDictionary(p=>p,File.ReadAllText);
        var candidates=new List<(string Path,string Text,Match Constructor)>();
        foreach(var source in sources)
        {
            if(source.Value.Contains(Marker))
            {
                string updated=Regex.Replace(source.Value,Regex.Escape(Marker)+@"(?<gap>\s*Window\.Title\s*=\s*)""(?:\\.|[^""\\])*""",m=>Marker+m.Groups["gap"].Value+literal);
                if(updated!=source.Value)edits[source.Key]=updated;
                candidates.Clear(); break;
            }
            foreach(Match game in Regex.Matches(source.Value,@"(?m)^[ \t]*(?:(?:public|internal|sealed|partial|abstract)\s+)*class\s+(?<name>\w+)\s*:\s*(?:Microsoft\.Xna\.Framework\.)?Game\b"))
                foreach(Match constructor in Regex.Matches(source.Value,@"(?m)^[ \t]*(?:public|internal|protected|private)\s+"+Regex.Escape(game.Groups["name"].Value)+@"\s*\([^)]*\)\s*(?::\s*(?:base|this)\s*\([^)]*\)\s*)?\{"))
                    candidates.Add((source.Key,source.Value,constructor));
        }
        bool already=sources.Values.Any(s=>s.Contains(Marker));
        if(!already && candidates.Count!=1)return $"Skipped: found {candidates.Count} unambiguous Game constructors. Set Window.Title manually for this project.";
        if(!already)
        {
            var candidate=candidates[0]; string newline=candidate.Text.Contains("\r\n") ? "\r\n" : "\n";
            edits[candidate.Path]=candidate.Text.Insert(candidate.Constructor.Index+candidate.Constructor.Length,newline+"        "+Marker+newline+"        Window.Title = "+literal+";");
        }
        foreach(var source in sources)
        {
            string value=edits.GetValueOrDefault(source.Key) ?? source.Value;
            value=Regex.Replace(value,@"(?<prefix>\[assembly:\s*(?:System\.Reflection\.)?Assembly(?:Title|Product)\s*\()""(?:\\.|[^""\\])*""",m=>m.Groups["prefix"].Value+literal);
            if(value!=source.Value)edits[source.Key]=value;
        }
        foreach(var edit in edits)
        {
            string backup=edit.Key+".before-window-title.bak";
            if(!File.Exists(backup))File.Copy(edit.Key,backup);
            File.WriteAllText(edit.Key,edit.Value,new UTF8Encoding(false));
        }
        return edits.Count==0 ? $"Window title already set to {title}." : $"Set window title to {title}; updated {edits.Count} file(s), with backups. Rebuild the game.";
    }
}
