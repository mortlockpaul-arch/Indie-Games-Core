using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace IndieGames.BuildManager.Services;

public sealed record OverlayInstallResult(string Status, string Message, string? Backup = null);
public static class GameOverlayInstaller
{
    const string Field = "__buildManagerOverlay";
    static string Simple(TypeSyntax type) => type.ToString().TrimEnd('?').Split('.').Last();
    static string Qualified(ClassDeclarationSyntax cls) => string.Join(".",cls.Ancestors().OfType<BaseNamespaceDeclarationSyntax>().Reverse().Select(n=>n.Name.ToString()).Concat(cls.Ancestors().OfType<ClassDeclarationSyntax>().Reverse().Select(c=>c.Identifier.ValueText)).Append(cls.Identifier.ValueText));
    static bool CallsBase(InvocationExpressionSyntax call, string name) => call.Expression is MemberAccessExpressionSyntax { Expression: BaseExpressionSyntax } access && access.Name.Identifier.ValueText == name;
    static string Backup(string folder,string project,string? source=null)
    {
        string directory=Path.Combine(folder,".backups");Directory.CreateDirectory(directory);
        if((File.GetAttributes(directory)&FileAttributes.ReparsePoint)!=0)throw new IOException("The backup folder is linked; source was left unchanged.");
        string path=Path.Combine(directory,"f1-overlay-"+Guid.NewGuid().ToString("N")+".zip");
        using var zip=ZipFile.Open(path,ZipArchiveMode.Create);zip.CreateEntryFromFile(project,Path.GetFileName(project));
        if(source!=null)zip.CreateEntryFromFile(source,Path.GetRelativePath(folder,source).Replace('\\','/'));return path;
    }
    static IEnumerable<string> SourceFiles(string folder)
    {
        foreach(var file in Directory.EnumerateFiles(folder,"*.cs")) if((File.GetAttributes(file)&FileAttributes.ReparsePoint)==0) yield return file;
        foreach(var child in Directory.EnumerateDirectories(folder))
        {
            string name=Path.GetFileName(child);
            if(name is "bin" or "obj" || name.StartsWith('.') || name.StartsWith("584E",StringComparison.OrdinalIgnoreCase) || (File.GetAttributes(child)&FileAttributes.ReparsePoint)!=0) continue;
            foreach(var file in SourceFiles(child))yield return file;
        }
    }
    static (string Text, string Message) AddCanvas(string text, ClassDeclarationSyntax game, string overlay)
    {
        (string, string) Manual(string reason) => (text, "Canvas needs manual integration: " + reason);
        if(game.Members.OfType<FieldDeclarationSyntax>().Any(f=>Simple(f.Declaration.Type)=="GameCanvas"))
            return (text, "Existing canvas adapter preserved.");
        if(game.DescendantTokens().Any(t=>t.ValueText=="__buildManagerCanvas")) return Manual("canvas identifier already in use.");
        var draw=game.Members.OfType<MethodDeclarationSyntax>().SingleOrDefault(m=>m.Identifier.ValueText=="Draw" && m.Modifiers.Any(SyntaxKind.OverrideKeyword));
        if(draw?.Body is null || draw.ParameterList.Parameters.Count!=1 || game.Modifiers.Any(SyntaxKind.PartialKeyword)) return Manual("custom Draw host.");
        if(draw.Body.DescendantNodes().Any(n=>n is ReturnStatementSyntax or AwaitExpressionSyntax or AnonymousFunctionExpressionSyntax or LocalFunctionStatementSyntax)) return Manual("complex Draw control flow.");
        if(draw.Body.DescendantNodes().OfType<MemberAccessExpressionSyntax>().Any(a=>a.Name.Identifier.ValueText is "Viewport" or "PresentationParameters" or "PreferredBackBufferWidth" or "PreferredBackBufferHeight")) return Manual("drawing already depends on the current display size.");
        var batches=game.Members.OfType<FieldDeclarationSyntax>().Where(f=>Simple(f.Declaration.Type)=="SpriteBatch").SelectMany(f=>f.Declaration.Variables).Select(v=>v.Identifier.ValueText).ToHashSet();
        var calls=draw.Body.DescendantNodes().OfType<InvocationExpressionSyntax>().ToArray();
        bool BatchCall(InvocationExpressionSyntax c) => c.Expression is MemberAccessExpressionSyntax a && batches.Contains(a.Expression.ToString().Replace("this.","")) && a.Name.Identifier.ValueText is "Begin" or "End" or "Draw" or "DrawString";
        if(calls.Any(c=>!BatchCall(c) && !CallsBase(c,"Draw") && !(c.Expression is MemberAccessExpressionSyntax a && a.Expression.ToString() is "GraphicsDevice" or "base.GraphicsDevice" or "this.GraphicsDevice" && a.Name.Identifier.ValueText=="Clear")))
            return Manual("custom rendering calls or render targets.");
        var baseDraw=calls.Where(c=>CallsBase(c,"Draw")).ToArray();
        if(baseDraw.Length>1 || baseDraw.Any(c=>c.Parent is not ExpressionStatementSyntax stmt || stmt.Parent!=draw.Body)) return Manual("conditional component rendering.");
        static int? Number(ExpressionSyntax e) => e is LiteralExpressionSyntax l && l.Token.Value is int value ? value : null;
        var sizes=new HashSet<(int Width,int Height)>();
        foreach(var call in calls.Where(BatchCall))
        {
            if(call.Expression is not MemberAccessExpressionSyntax access || access.Name.Identifier.ValueText!="Draw" || call.ArgumentList.Arguments.Count<2)continue;
            if(call.ArgumentList.Arguments[1].Expression is not ObjectCreationExpressionSyntax rectangle || Simple(rectangle.Type)!="Rectangle" || rectangle.ArgumentList?.Arguments.Count!=4)continue;
            var args=rectangle.ArgumentList.Arguments;
            if(Number(args[0].Expression)==0 && Number(args[1].Expression)==0 && Number(args[2].Expression) is int w && Number(args[3].Expression) is int h && w>=320 && h>=180 && w<=7680 && h<=4320) sizes.Add((w,h));
        }
        if(sizes.Count!=1) return Manual("no unambiguous fixed full-frame drawing size.");
        var (width,height)=sizes.Single();
        var unload=game.Members.OfType<MethodDeclarationSyntax>().FirstOrDefault(m=>m.Identifier.ValueText=="UnloadContent"&&m.Modifiers.Any(SyntaxKind.OverrideKeyword));
        if(unload!=null && unload.Body==null) return Manual("custom content disposal.");
        string time=draw.ParameterList.Parameters[0].Identifier.Text;
        var edits=new List<TextChange> {
            new(new TextSpan(game.OpenBraceToken.Span.End,0),"\n\tprivate IndieGames.Overlay.GameCanvas? __buildManagerCanvas;\n"),
            new(new TextSpan(draw.Body.OpenBraceToken.Span.End,0),$"\n\t\tif ({overlay}.IsOpen && __buildManagerCanvas != null) {{ __buildManagerCanvas.PresentLastFrame(); base.Draw({time}); return; }}\n\t\t__buildManagerCanvas ??= new IndieGames.Overlay.GameCanvas(GraphicsDevice,{width},{height});\n\t\t__buildManagerCanvas.Begin();\n"),
            new(new TextSpan(draw.Body.CloseBraceToken.SpanStart,0),$"\n\t\t__buildManagerCanvas.Present();\n\t\tbase.Draw({time});\n")
        };
        edits.AddRange(baseDraw.Select(c=>new TextChange(c.Parent!.Span,"")));
        if(unload?.Body!=null)edits.Add(new(new TextSpan(unload.Body.OpenBraceToken.Span.End,0),"\n\t\t__buildManagerCanvas?.Dispose();\n"));
        else edits.Add(new(new TextSpan(game.CloseBraceToken.SpanStart,0),"\n\tprotected override void UnloadContent() { __buildManagerCanvas?.Dispose(); base.UnloadContent(); }\n"));
        string result=SourceText.From(text).WithChanges(edits).ToString();
        if(CSharpSyntaxTree.ParseText(result).GetDiagnostics().Any(d=>d.Severity==DiagnosticSeverity.Error)) return Manual("generated adapter failed syntax checks.");
        return (result,$"{width}×{height} canvas added for resolution scaling and frame filters.");
    }
    public static OverlayInstallResult Install(string project,string core,CancellationToken token=default)
    {
        project=Path.GetFullPath(project);core=Path.GetFullPath(core);string folder=Path.GetDirectoryName(project)!;
        if((File.GetAttributes(project)&FileAttributes.ReparsePoint)!=0)return new("Skipped","Linked project files require manual integration.");
        string originalProject=File.ReadAllText(project);
        var xml=XDocument.Parse(originalProject,LoadOptions.PreserveWhitespace);var root=xml.Root ?? throw new InvalidDataException("Invalid project XML.");XNamespace ns=root.Name.Namespace;
        string Property(string name)=>root.Descendants(ns+name).LastOrDefault()?.Value.Trim() ?? "";
        if(!System.Text.RegularExpressions.Regex.IsMatch(Property("TargetFramework"),@"^net(?:9|[1-9]\d)\.\d-windows"))return new("Skipped","Overlay installation requires a .NET 9+ Windows game project.");
        if(Property("OutputType") is not ("Exe" or "WinExe"))return new("Skipped","Select an executable game project.");
        var references=root.Descendants().Where(e=>e.Name.LocalName is "ProjectReference" or "PackageReference" or "Reference").Select(e=>e.Attribute("Include")?.Value ?? "").ToArray();
        bool mono=references.Any(r=>r.Contains("MonoGame",StringComparison.OrdinalIgnoreCase)),fna=references.Any(r=>r.Contains("FNA",StringComparison.OrdinalIgnoreCase));
        if(mono==fna)return new("Skipped","Could not identify one FNA or MonoGame backend. Configure the game framework first.");
        string backend=mono?"MonoGame":"FNA",library=Path.Combine(core,"IndieGames.Overlay."+backend,"IndieGames.Overlay."+backend+".csproj");
        if(!File.Exists(library))return new("Skipped","Overlay project missing from the configured solution folder: "+library);
        string? libraryProperties=null;
        if(mono)
        {
            var package=root.Descendants(ns+"PackageReference").FirstOrDefault(e=>(e.Attribute("Include")?.Value??"").StartsWith("MonoGame.Framework.",StringComparison.OrdinalIgnoreCase));
            if(package!=null)
            {
                string id=package.Attribute("Include")!.Value,version=package.Attribute("Version")?.Value??package.Element(ns+"Version")?.Value??"";
                if(version.Length==0 || version.Contains('$') || version.Contains(';'))return new("Skipped","Resolve the centrally configured MonoGame version before installing an overlay adapter.");
                libraryProperties=$"MonoGamePackage={id};MonoGamePackageVersion={version}";
            }
            else
            {
                var frameworkProject=root.Descendants(ns+"ProjectReference").FirstOrDefault(e=>(e.Attribute("Include")?.Value??"").Contains("MonoGame.Framework",StringComparison.OrdinalIgnoreCase));
                var contents=root.Descendants(ns+"ProjectReference").FirstOrDefault(e=>(e.Attribute("Include")?.Value??"").Contains("Monogame.Contents",StringComparison.OrdinalIgnoreCase));
                string Resolve(string value,string directory)=>Path.GetFullPath(Path.Combine(directory,value.Replace("$(IndieGamesCoreRoot)",core,StringComparison.OrdinalIgnoreCase)));
                if(frameworkProject!=null)libraryProperties="MonoGameProject="+Resolve(frameworkProject.Attribute("Include")!.Value,folder);
                else if(contents!=null)
                {
                    string path=Resolve(contents.Attribute("Include")!.Value,folder);var contentXml=XDocument.Load(path);
                    var reference=contentXml.Descendants().FirstOrDefault(e=>e.Name.LocalName=="ProjectReference"&&(e.Attribute("Include")?.Value??"").Contains("MonoGame.Framework",StringComparison.OrdinalIgnoreCase));
                    if(reference==null)return new("Skipped","The MonoGame contents project has no identifiable framework project.");
                    libraryProperties="MonoGameProject="+Resolve(reference.Attribute("Include")!.Value,Path.GetDirectoryName(path)!);
                }
                else
                {
                    var binary=root.Descendants(ns+"Reference").FirstOrDefault(e=>(e.Attribute("Include")?.Value??"").Contains("MonoGame",StringComparison.OrdinalIgnoreCase));
                    string? hint=binary?.Element(ns+"HintPath")?.Value;
                    if(hint!=null&&!hint.Contains('$')&&File.Exists(Resolve(hint,folder)))libraryProperties="MonoGameAssembly="+Resolve(hint,folder);
                    else if(!references.Any(r=>r.EndsWith("IndieGames.Overlay.MonoGame.csproj",StringComparison.OrdinalIgnoreCase)))return new("Skipped","Could not identify the matching MonoGame framework package, project or DLL.");
                }
            }
        }
        if(libraryProperties!=null)libraryProperties+=";MonoGameVariant="+Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(libraryProperties)))[..12];
        XElement LibraryReference(){var element=new XElement(ns+"ProjectReference",new XAttribute("Include",library));if(libraryProperties!=null)element.Add(new XAttribute("AdditionalProperties",libraryProperties));return element;}
        var sources=new List<(string Path,string Text,ClassDeclarationSyntax Class)>();bool platformAttribute=false;
        string startup=Property("StartupObject");var startupTypes=new HashSet<string>(StringComparer.Ordinal);
        foreach(string file in SourceFiles(folder))
        {
            token.ThrowIfCancellationRequested();string text=File.ReadAllText(file);platformAttribute|=text.Contains("SupportedOSPlatform",StringComparison.Ordinal);
            if(!text.Contains("GraphicsDeviceManager",StringComparison.Ordinal) && (startup.Length==0 || !text.Contains("Main",StringComparison.Ordinal)))continue;
            var tree=CSharpSyntaxTree.ParseText(text,new CSharpParseOptions(LanguageVersion.Preview));
            foreach(var program in tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where(c=>Qualified(c)==startup))
                foreach(var creation in program.Members.OfType<MethodDeclarationSyntax>().Where(m=>m.Identifier.ValueText=="Main").SelectMany(m=>m.DescendantNodes().OfType<ObjectCreationExpressionSyntax>()))startupTypes.Add(Simple(creation.Type));
            foreach(var cls in tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Where(c=>c.BaseList?.Types.Any(b=>b.Type.ToString() is "Game" or "Microsoft.Xna.Framework.Game")==true))sources.Add((file,text,cls));
        }
        if(sources.Count>1){var active=sources.Where(s=>startupTypes.Contains(s.Class.Identifier.ValueText)).ToList();if(active.Count==1)sources=active;}
        if(sources.Count!=1)return new("Skipped",$"Found {sources.Count} direct Game classes. Custom/inherited or multiple game hosts need manual integration.");
        var source=sources[0];var game=source.Class;
        var overlayFields=game.Members.OfType<FieldDeclarationSyntax>().Where(f=>Simple(f.Declaration.Type)=="GameSettingsOverlay").ToArray();
        if(overlayFields.Length>0)
        {
            var names=overlayFields.SelectMany(f=>f.Declaration.Variables.Select(v=>v.Identifier.ValueText)).ToArray();
            bool updated=game.Members.OfType<MethodDeclarationSyntax>().Where(m=>m.Identifier.ValueText=="Update").Any(m=>m.DescendantNodes().OfType<InvocationExpressionSyntax>().Any(c=>c.Expression is MemberAccessExpressionSyntax a && names.Contains(a.Expression.ToString()) && a.Name.Identifier.ValueText=="Update"));
            if (!updated) return new("Needs review", "An overlay field exists but its update hook is incomplete; source was left unchanged.");
            if(names.Length!=1) return new("Needs review","Multiple overlay fields require manual canvas integration.");
            var upgrade = AddCanvas(source.Text, game, names[0]);
            bool missingReference = !references.Any(r=>r.EndsWith("IndieGames.Overlay."+backend+".csproj",StringComparison.OrdinalIgnoreCase));
            if (upgrade.Text == source.Text && !missingReference)
                return new("Already installed", "Source hooks already exist. " + upgrade.Message);
            if(File.ReadAllText(project)!=originalProject || File.ReadAllText(source.Path)!=source.Text)
                return new("Needs review","Source or project changed while checking; save and try again.");
            string saved=Backup(folder,project,source.Path);
            if(missingReference) root.Add(new XElement(ns+"ItemGroup",LibraryReference()));
            token.ThrowIfCancellationRequested();
            try { File.WriteAllText(source.Path,upgrade.Text); File.WriteAllText(project,xml.ToString(SaveOptions.DisableFormatting)); }
            catch { File.WriteAllText(source.Path,source.Text); File.WriteAllText(project,originalProject); throw; }
            return new("Installed", upgrade.Message + " Rebuild to apply the update.",saved);
        }
        var ctors=game.Members.OfType<ConstructorDeclarationSyntax>().Where(c=>!c.Modifiers.Any(SyntaxKind.StaticKeyword)).ToArray();
        var managers=game.Members.OfType<FieldDeclarationSyntax>().Where(f=>Simple(f.Declaration.Type)=="GraphicsDeviceManager").SelectMany(f=>f.Declaration.Variables).ToArray();
        var update=game.Members.OfType<MethodDeclarationSyntax>().SingleOrDefault(m=>m.Identifier.ValueText=="Update"&&m.Modifiers.Any(SyntaxKind.OverrideKeyword));
        var draw=game.Members.OfType<MethodDeclarationSyntax>().SingleOrDefault(m=>m.Identifier.ValueText=="Draw"&&m.Modifiers.Any(SyntaxKind.OverrideKeyword));
        if(ctors.Length!=1 || ctors[0].Body==null || managers.Length!=1 || update?.Body==null || draw?.Body==null || update.ParameterList.Parameters.Count!=1 || draw.ParameterList.Parameters.Count!=1 || game.Modifiers.Any(SyntaxKind.PartialKeyword) || game.TypeParameterList!=null)return new("Skipped","Requires one complete non-generic Game class, one constructor/graphics manager, and Update/Draw method bodies. This host needs a manual adapter.");
        if(game.DescendantTokens().Any(t=>t.ValueText==Field || t.IsKind(SyntaxKind.IdentifierToken)&&t.ValueText.StartsWith("__overlay",StringComparison.Ordinal)) || draw.Body.DescendantNodes().OfType<ReturnStatementSyntax>().Any() || ctors[0].Body!.DescendantNodes().OfType<ReturnStatementSyntax>().Any())return new("Skipped","Custom return paths or conflicting overlay identifiers require manual integration.");
        string manager=managers[0].Identifier.Text;
        bool knownCanvasHost=Property("AssemblyName")=="Manic Miner 360"&&game.Identifier.ValueText=="Game1" || Property("AssemblyName")=="Jet Set Willy 360"&&game.Identifier.ValueText=="ZXGame";
        bool canvas=knownCanvasHost && source.Text.Contains("ZxSpectrum",StringComparison.Ordinal) && game.Members.OfType<FieldDeclarationSyntax>().Any(f=>f.Declaration.Variables.Any(v=>v.Identifier.ValueText=="Settings"));
        if(canvas && game.DescendantTokens().Any(t=>t.ValueText=="__buildManagerCanvas"))return new("Skipped","A canvas identifier already exists; manual review is required.");
        bool initialized=ctors[0].Body!.Statements.OfType<ExpressionStatementSyntax>().Any(s=>s.Expression is AssignmentExpressionSyntax a && (a.Left.ToString()==manager || a.Left.ToString()=="this."+manager) && a.Right is ObjectCreationExpressionSyntax n && Simple(n.Type)=="GraphicsDeviceManager");
        if(!initialized)return new("Skipped","Could not verify graphics-manager initialization; source was left unchanged.");
        var baseDraw=draw.Body.DescendantNodes().OfType<InvocationExpressionSyntax>().Where(c=>CallsBase(c,"Draw")).ToArray();
        if(baseDraw.Any(c=>c.Parent is not ExpressionStatementSyntax stmt || stmt.Parent!=draw.Body))return new("Skipped","Conditional base.Draw calls require manual integration.");
        string title=Property("AssemblyName");if(title.Length==0)title=Path.GetFileNameWithoutExtension(project);
        string literal=Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(title,true);
        string setup=$"\n\t\t{Field} = new IndieGames.Overlay.GameSettingsOverlay(this, {manager}) {{ Enabled = false, GameTitle = {literal} }};\n";
        setup+="\t\tbool __overlayMusicWasPlaying = false;\n";
        var audio=game.Members.OfType<FieldDeclarationSyntax>().Where(f=>Simple(f.Declaration.Type) is "SoundEffectInstance" or "DynamicSoundEffectInstance").SelectMany(f=>f.Declaration.Variables).Select(v=>v.Identifier.Text).ToArray();
        for(int i=0;i<audio.Length;i++)setup+=$"\t\tbool __overlaySound{i}WasPlaying = false;\n";
        setup+=$"\t\t{Field}.PauseChanged = paused =>\n\t\t{{\n\t\t\tif (paused)\n\t\t\t{{\n\t\t\t\t__overlayMusicWasPlaying = Microsoft.Xna.Framework.Media.MediaPlayer.State == Microsoft.Xna.Framework.Media.MediaState.Playing;\n\t\t\t\tif (__overlayMusicWasPlaying) Microsoft.Xna.Framework.Media.MediaPlayer.Pause();\n";
        for(int i=0;i<audio.Length;i++)setup+=$"\t\t\t\t__overlaySound{i}WasPlaying = {audio[i]}?.State == Microsoft.Xna.Framework.Audio.SoundState.Playing;\n\t\t\t\tif (__overlaySound{i}WasPlaying) {audio[i]}?.Pause();\n";
        setup+="\t\t\t}\n\t\t\telse\n\t\t\t{\n\t\t\t\tif (__overlayMusicWasPlaying) Microsoft.Xna.Framework.Media.MediaPlayer.Resume();\n";
        for(int i=0;i<audio.Length;i++)setup+=$"\t\t\t\tif (__overlaySound{i}WasPlaying) {audio[i]}?.Resume();\n";
        setup+=$"\t\t\t}}\n\t\t}};\n\t\tbase.Components.Add({Field});\n";
        if(source.Text.Contains("__BuildManagerOverlayBootstrap",StringComparison.Ordinal))return new("Needs review","A diagnostics bootstrap already exists; source was left unchanged.");
        string fields=$"\n\tprivate readonly IndieGames.Overlay.GameSettingsOverlay {Field};\n";
        if(canvas)fields+="\tprivate IndieGames.Overlay.GameCanvas? __buildManagerCanvas;\n";
        string drawEnd=(canvas?"\n\t\t__buildManagerCanvas.Present(Settings.PixelFiltering);":"")+$"\n\t\tbase.Draw({draw.ParameterList.Parameters[0].Identifier.Text});\n";
        var changes=new List<TextChange>{new(new TextSpan(game.OpenBraceToken.Span.End,0),fields),new(new TextSpan(ctors[0].Body!.CloseBraceToken.SpanStart,0),setup),new(new TextSpan(update.Body.OpenBraceToken.Span.End,0),$"\n\t\t{Field}.Update({update.ParameterList.Parameters[0].Identifier.Text});\n\t\tif ({Field}.IsOpen) return;\n"),new(new TextSpan(draw.Body.CloseBraceToken.SpanStart,0),drawEnd)};
        changes.Add(new(new TextSpan(source.Text.Length,0),"\ninternal static class __BuildManagerOverlayBootstrap\n{\n\t[System.Runtime.CompilerServices.ModuleInitializer]\n\tinternal static void Capture() => IndieGames.Overlay.OverlayDiagnostics.StartCapture();\n}\n"));
        if(canvas)
        {
            string time=draw.ParameterList.Parameters[0].Identifier.Text;
            changes.Add(new(new TextSpan(draw.Body.OpenBraceToken.Span.End,0),$"\n\t\tif ({Field}.IsOpen && __buildManagerCanvas != null) {{ __buildManagerCanvas.PresentLastFrame(Settings.PixelFiltering); base.Draw({time}); return; }}\n\t\t__buildManagerCanvas ??= new IndieGames.Overlay.GameCanvas(GraphicsDevice,1280,720);\n\t\t__buildManagerCanvas.Begin();\n"));
            foreach(var access in draw.Body.DescendantNodes().OfType<MemberAccessExpressionSyntax>().Where(a=>a.Expression.ToString()==manager && a.Name.Identifier.ValueText is "PreferredBackBufferWidth" or "PreferredBackBufferHeight"))changes.Add(new(access.Span,access.Name.Identifier.ValueText=="PreferredBackBufferWidth"?"1280":"720"));
            var unload=game.Members.OfType<MethodDeclarationSyntax>().FirstOrDefault(m=>m.Identifier.ValueText=="UnloadContent"&&m.Modifiers.Any(SyntaxKind.OverrideKeyword));
            if(unload?.Body!=null)changes.Add(new(new TextSpan(unload.Body.OpenBraceToken.Span.End,0),"\n\t\t__buildManagerCanvas?.Dispose();\n"));
            else if(unload==null)changes.Add(new(new TextSpan(game.CloseBraceToken.SpanStart,0),"\n\tprotected override void UnloadContent() { __buildManagerCanvas?.Dispose(); base.UnloadContent(); }\n"));
            else return new("Skipped","Custom content disposal requires manual canvas integration.");
        }
        changes.AddRange(baseDraw.Select(c=>new TextChange(c.Parent!.Span,"")));
        string edited=SourceText.From(source.Text).WithChanges(changes).ToString();
        if(CSharpSyntaxTree.ParseText(edited,new CSharpParseOptions(LanguageVersion.Preview)).GetDiagnostics().Any(d=>d.Severity==DiagnosticSeverity.Error))return new("Skipped","Generated hooks failed syntax validation; source was left unchanged.");
        string canvasMessage = canvas ? "1280×720 canvas" : "";
        if (!canvas)
        {
            var parsed = CSharpSyntaxTree.ParseText(edited).GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Single(c=>Qualified(c)==Qualified(game));
            var adapted = AddCanvas(edited, parsed, Field);
            edited = adapted.Text;
            canvasMessage = adapted.Message;
        }
        bool referenceExists=references.Any(r=>r.EndsWith("IndieGames.Overlay."+backend+".csproj",StringComparison.OrdinalIgnoreCase));
        if(!referenceExists)root.Add(new XText("\n  "),new XElement(ns+"ItemGroup",LibraryReference()),new XText("\n"));
        string platformFile=Path.Combine(folder,"Properties","BuildManager.Overlay.Windows.cs");bool addPlatform=Property("GenerateAssemblyInfo").Equals("false",StringComparison.OrdinalIgnoreCase)&&!platformAttribute;
        if(addPlatform&&File.Exists(platformFile))return new("Skipped","The generated platform file already exists; source was left unchanged.");
        if(File.ReadAllText(source.Path)!=source.Text || File.ReadAllText(project)!=originalProject)return new("Needs review","Source or project changed while checking. Try again after saving your edits.");
        string backup=Backup(folder,project,source.Path);
        token.ThrowIfCancellationRequested();
        try
        {
            File.WriteAllText(source.Path,edited);File.WriteAllText(project,xml.ToString(SaveOptions.DisableFormatting));
            if(addPlatform){Directory.CreateDirectory(Path.GetDirectoryName(platformFile)!);File.WriteAllText(platformFile,"[assembly: System.Runtime.Versioning.SupportedOSPlatform(\"windows\")]\n");}
        }
        catch{File.WriteAllText(source.Path,source.Text);File.WriteAllText(project,originalProject);if(addPlatform&&File.Exists(platformFile))File.Delete(platformFile);throw;}
        return new("Installed",$"F1 settings added to {Path.GetRelativePath(folder,source.Path)} ({backend}{(canvasMessage.Length>0?", "+canvasMessage:"")}). Rebuild to enable it. Other fixed layouts/custom audio wrappers may need a manual adapter.",backup);
    }
}
