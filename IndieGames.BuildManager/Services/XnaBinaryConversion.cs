using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text.Json;
using System.Reflection.PortableExecutable;
using Mono.Cecil;

namespace IndieGames.BuildManager.Services;

public sealed record XnaConversionCheck(bool Compatible, string[] Details);
public static class XnaBinaryConversion
{
    public const string ToolVersion = "Upstream v18.05.1 (legacy .NET Framework)";
    static readonly JsonSerializerOptions Json = new() { WriteIndented = true };
    static string Hash(string path) { using var stream=File.OpenRead(path); return Convert.ToHexString(SHA256.HashData(stream)); }
    static AssemblyDefinition Read(string path) => AssemblyDefinition.ReadAssembly(path, new ReaderParameters { ReadingMode=ReadingMode.Immediate });
    public static string? FindOriginalExecutable(string? projectPath)
    {
        if (string.IsNullOrWhiteSpace(projectPath)) return null;
        string originalFolder = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(projectPath))!, "584E07D1");
        if (!Directory.Exists(originalFolder)) return null;

        var matches = new List<string>();
        foreach (string file in Directory.EnumerateFiles(originalFolder, "*.exe"))
        {
            try
            {
                using var game = Read(file);
                var references = game.MainModule.AssemblyReferences;
                if (game.EntryPoint is not null &&
                    references.Any(r => r.Name.StartsWith("Microsoft.Xna.Framework", StringComparison.Ordinal)) &&
                    !references.Any(r => r.Name is "FNA" or "XnaToFna"))
                    matches.Add(file);
            }
            catch (Exception error) when (error is IOException or BadImageFormatException or UnauthorizedAccessException) { }
        }
        return matches.Count == 1 ? matches[0] : null;
    }

    public static XnaConversionCheck Check(string executable, string runtime)
    {
        var details=new List<string>(); bool valid=true;
        try
        {
            if(Directory.EnumerateFiles(Path.GetDirectoryName(Path.GetFullPath(executable))!,"*.csproj").Any()) throw new InvalidDataException("Choose a clean original game folder, separate from decompiled source and build outputs. Re-extract an original package to a temporary folder if necessary.");
            using var game=Read(executable);
            if(game.EntryPoint==null) throw new InvalidDataException("Choose a managed game executable with an entry point.");
            if(game.MainModule.AssemblyReferences.Any(r=>r.Name is "FNA" or "XnaToFna")) throw new InvalidDataException("This executable already references FNA or XnaToFna. Start from an original XNA copy.");
            if(!game.MainModule.AssemblyReferences.Any(r=>r.Name.StartsWith("Microsoft.Xna.Framework",StringComparison.Ordinal)))
                throw new InvalidDataException("Choose an original managed XNA executable. This file has no XNA assembly references (it may already be converted).");
            if(File.Exists(Path.ChangeExtension(executable,".runtimeconfig.json"))) throw new InvalidDataException("Modern .NET builds should use XnaAssemblyRedirector for their legacy libraries.");
            details.Add("Original managed XNA executable found: "+game.Name.FullName);
            using var fna=Read(Path.Combine(runtime,"FNA.dll"));
            string framework=fna.CustomAttributes.FirstOrDefault(a=>a.AttributeType.FullName=="System.Runtime.Versioning.TargetFrameworkAttribute")?.ConstructorArguments.FirstOrDefault().Value as string ?? "No target attribute; checking framework references";
            details.Add("FNA target: "+framework);
            if(framework.StartsWith(".NETCoreApp",StringComparison.Ordinal) || fna.MainModule.AssemblyReferences.Any(r=>r.Name=="System.Private.CoreLib" || r.Name=="System.Runtime"))
                throw new InvalidDataException("Our modern .NET FNA build cannot be loaded by this .NET Framework converter. Choose a matching legacy FNA runtime; no automatic runtime replacement is performed.");
            if(!fna.MainModule.Types.Any(t=>t.FullName=="Microsoft.Xna.Framework.Game")) throw new InvalidDataException("The selected DLL is not an FNA runtime.");
            if(!File.Exists(Path.Combine(runtime,"SDL2.dll"))) throw new InvalidDataException("A complete legacy runtime folder with SDL2.dll is required. SDL3-only bundles are not supported by this converter.");
            using(var native=File.OpenRead(Path.Combine(runtime,"SDL2.dll")))
            using(var pe=new PEReader(native))
            {
                bool x86=(game.MainModule.Attributes&(ModuleAttributes.Required32Bit|ModuleAttributes.Preferred32Bit))!=0;
                bool gameX64=game.MainModule.Architecture==TargetArchitecture.AMD64 || !x86&&Environment.Is64BitOperatingSystem;
                bool nativeX86=pe.PEHeaders.CoffHeader.Machine==Machine.I386;
                bool nativeX64=pe.PEHeaders.CoffHeader.Machine==Machine.Amd64;
                if(!nativeX86&&!nativeX64 || gameX64&&!nativeX64 || !gameX64&&!nativeX86) throw new InvalidDataException("The SDL2 native architecture does not match the original executable. Choose the matching x86/x64 legacy runtime.");
                details.Add("SDL2 architecture: "+(nativeX86?"x86":"x64")+"; original managed architecture is preserved.");
            }
            if(game.MainModule.AssemblyReferences.Any(r=>r.Name is "Microsoft.Xna.Framework.Net" or "Microsoft.Xna.Framework.GamerServices" or "Microsoft.Xna.Framework.Xdk") && !File.Exists(Path.Combine(runtime,"FNA.NetStub.dll"))) throw new InvalidDataException("This game also needs a matching legacy FNA.NetStub.dll.");
            details.Add("Legacy SDL2 runtime found. API compatibility will also be checked by the converter and output validation.");
            details.Add("Content is copied unchanged. Use the audio/video/shader repair tools separately when required.");
        }
        catch(Exception ex) {valid=false;details.Add(ex.GetBaseException().Message);}
        return new(valid,details.ToArray());
    }
    static IEnumerable<string> Files(string root)
    {
        if((File.GetAttributes(root)&FileAttributes.ReparsePoint)!=0) throw new IOException("Linked folders are not supported.");
        foreach(var file in Directory.EnumerateFiles(root))
        { if((File.GetAttributes(file)&FileAttributes.ReparsePoint)!=0) throw new IOException("Linked files are not supported."); yield return file; }
        foreach(var dir in Directory.EnumerateDirectories(root)) foreach(var file in Files(dir)) yield return file;
    }
    public static async Task<string> ConvertAsync(string executable,string runtime,string games,Action<string> log,CancellationToken token)
    {
        executable=Path.GetFullPath(executable);runtime=Path.GetFullPath(runtime);games=Path.TrimEndingDirectorySeparator(Path.GetFullPath(games));
        string source=Path.GetDirectoryName(executable)!;
        if(games.Equals(source,StringComparison.OrdinalIgnoreCase)||games.StartsWith(source+Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase)) throw new IOException("The output library must be outside the original game folder.");
        var check=Check(executable,runtime);if(!check.Compatible) throw new InvalidDataException(string.Join(Environment.NewLine,check.Details));
        Directory.CreateDirectory(games);
        string name=Path.GetFileNameWithoutExtension(executable)+" - FNA "+DateTime.Now.ToString("yyyyMMdd-HHmmss")+"-"+Guid.NewGuid().ToString("N")[..6];
        string output=Path.Combine(games,name), stage=Path.Combine(games,".xnatofna-"+Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(stage);var entries=new List<object>();bool success=false;
        try
        {
            log("Copying the original game to an isolated folder…");
            foreach(string file in Files(source))
            {
                token.ThrowIfCancellationRequested();string relative=Path.GetRelativePath(source,file),dest=Path.Combine(stage,relative);
                Directory.CreateDirectory(Path.GetDirectoryName(dest)!);File.Copy(file,dest,false);
                entries.Add(new {Path=relative,Sha256=Hash(file)});
                if(entries.Count%250==0)log($"Copied and inventoried {entries.Count} files…");
            }
            string backup=stage+".backup";Directory.CreateDirectory(backup);
            foreach(string file in Files(stage).Where(f=>f.EndsWith(".dll",StringComparison.OrdinalIgnoreCase)||f.EndsWith(".exe",StringComparison.OrdinalIgnoreCase)).ToArray())
            {string dest=Path.Combine(backup,Path.GetRelativePath(stage,file));Directory.CreateDirectory(Path.GetDirectoryName(dest)!);File.Copy(file,dest);}
            // The pinned upstream tool and its matching managed dependencies are kept together.
            using(var resource=typeof(XnaBinaryConversion).Assembly.GetManifestResourceStream("IndieGames.BuildManager.XnaToFna.zip") ?? throw new IOException("Bundled converter is missing."))
            using(var zip=new ZipArchive(resource))
            foreach(var item in zip.Entries)
            {
                if(item.FullName.EndsWith('/')) continue;
                string target=PortableGames.ResolveRelative(stage,item.FullName);
                Directory.CreateDirectory(Path.GetDirectoryName(target)!);
                if(File.Exists(target) && !item.Name.StartsWith("FNA.dll",StringComparison.OrdinalIgnoreCase)) throw new IOException("The original folder already contains converter files: "+item.FullName);
                item.ExtractToFile(target,true);
            }
            // Copy only runtime binaries/configuration, never scripts or launchers.
            foreach(string file in Directory.EnumerateFiles(runtime).Where(f=>new[]{"FNA.dll","FNA.dll.config","FNA.NetStub.dll","SDL2.dll","SDL2_image.dll","FNA3D.dll","FAudio.dll","libtheorafile.dll","mojoshader.dll","libmojoshader.dll","soft_oal.dll","libvorbisfile.dll","libvorbis.dll","libogg.dll","gamecontrollerdb.txt"}.Contains(Path.GetFileName(f),StringComparer.OrdinalIgnoreCase)))
            { token.ThrowIfCancellationRequested();if(Path.GetFileName(file).StartsWith("Mono.",StringComparison.OrdinalIgnoreCase)||Path.GetFileName(file).StartsWith("MonoMod",StringComparison.OrdinalIgnoreCase)||Path.GetFileName(file).StartsWith("XnaToFna",StringComparison.OrdinalIgnoreCase))continue;File.Copy(file,Path.Combine(stage,Path.GetFileName(file)),true); }
            var start=new ProcessStartInfo(Path.Combine(stage,"XnaToFna.exe")){WorkingDirectory=stage,UseShellExecute=false,CreateNoWindow=true,RedirectStandardOutput=true,RedirectStandardError=true};
            // Explicitly preserve locks and assets; do not silently rewrite audio/video/XNB data.
            start.ArgumentList.Add("--skip-content");start.ArgumentList.Add("--keep-locks");
            start.ArgumentList.Add("--keep-mixed-deps");start.ArgumentList.Add("--platform=Keep");
            using(var original=Read(executable)) if(original.EntryPoint.Parameters.Count==0) start.ArgumentList.Add("--skip-entrypoint");
            log("Running upstream XnaToFna in the copied folder…");
            string stdout="",stderr="";int exit=-1;
            using(var process=Process.Start(start) ?? throw new IOException("Could not start converter."))
            {
                var outTask=process.StandardOutput.ReadToEndAsync();var errTask=process.StandardError.ReadToEndAsync();
                try {await process.WaitForExitAsync(token);}catch(OperationCanceledException){try{process.Kill(true);}catch{} await process.WaitForExitAsync();throw;}
                stdout=await outTask;stderr=await errTask;exit=process.ExitCode;
            }
            File.WriteAllText(Path.Combine(stage,"conversion.log"),stdout+Environment.NewLine+stderr);log(stdout);if(stderr.Length>0)log(stderr);
            if(exit!=0)throw new IOException("Converter failed with exit code "+exit+". See conversion.log.");
            string result=Path.Combine(stage,Path.GetFileName(executable));using(var converted=Read(result))
            {
                if(converted.MainModule.AssemblyReferences.Any(r=>r.Name.StartsWith("Microsoft.Xna.Framework",StringComparison.Ordinal)))throw new IOException("Conversion left XNA assembly references in the executable.");
                if(!converted.MainModule.AssemblyReferences.Any(r=>r.Name=="FNA"))throw new IOException("The output does not reference FNA; conversion was not successful.");
            }
            PortableGames.WriteManifest(stage,new(){Name=Path.GetFileNameWithoutExtension(executable)+" (FNA conversion)",EntryPoint=Path.GetFileName(executable),Kind="XNA conversion"});
            success=true;
        }
        catch(Exception ex){log("Conversion did not complete: "+ex.Message);File.WriteAllText(Path.Combine(stage,"conversion-error.txt"),ex.ToString());throw;}
        finally
        {
            if(Directory.Exists(stage+".backup")) Directory.Move(stage+".backup",Path.Combine(stage,".xnatofna-backup"));
            File.WriteAllText(Path.Combine(stage,"conversion-report.json"),JsonSerializer.Serialize(new{Success=success,Tool=ToolVersion,Source=source,Runtime=runtime,Check=check,OriginalFiles=entries,Note="Static conversion validation only; launch testing is still required. Original files untouched; pre-conversion binaries in .xnatofna-backup."},Json));
            // Preserve incomplete copies and reports for diagnosis; never present them as installed games.
            if(success) Directory.Move(stage,output);else log("Incomplete copy and report: "+stage);
        }
        log("Converted copy: "+output);return output;
    }
}
