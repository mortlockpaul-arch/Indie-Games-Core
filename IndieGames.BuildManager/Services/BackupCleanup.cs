using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace IndieGames.BuildManager.Services;
public sealed record BackupCleanupEntry(string Root,string Path,bool Directory,long Bytes,int Files,string Fingerprint);
public static class BackupCleanup
{
    static readonly HashSet<string> KnownFolders=new(StringComparer.OrdinalIgnoreCase){".backups",".backup",".xnatofna-backup","shader-repair-backups","font-repair-backups","effect-recovery-backups","audio-conversion-backups","video-conversion-backups"};
    static bool BackupFile(string name)=>name.EndsWith(".bak",StringComparison.OrdinalIgnoreCase)||name.Contains(".bak.",StringComparison.OrdinalIgnoreCase)||name.Contains(".before-",StringComparison.OrdinalIgnoreCase)||name.EndsWith(".cs.orig",StringComparison.OrdinalIgnoreCase)||name.EndsWith(".csproj.orig",StringComparison.OrdinalIgnoreCase);
    static string Full(string path)=>System.IO.Path.TrimEndingDirectorySeparator(System.IO.Path.GetFullPath(path));
    static void Validate(string root,string path)
    {
        root=Full(root);path=Full(path);
        if(root==System.IO.Path.GetPathRoot(root)||!path.StartsWith(root+System.IO.Path.DirectorySeparatorChar,StringComparison.OrdinalIgnoreCase))throw new IOException("Backup cleanup target is outside the selected archive.");
        for(string? p=path;p!=null;p=System.IO.Path.GetDirectoryName(p))if((File.GetAttributes(p)&FileAttributes.ReparsePoint)!=0)throw new IOException("Backup cleanup refuses linked paths.");
    }
    static BackupCleanupEntry Inspect(string root,string path,bool directory,CancellationToken token)
    {
        long bytes=0;int files=0;var metadata=new StringBuilder();
        void Walk(string current){token.ThrowIfCancellationRequested();Validate(root,current);var info=new FileInfo(current);if(!System.IO.Directory.Exists(current)){bytes+=info.Length;files++;metadata.Append(current).Append('|').Append(info.Length).Append('|').Append(info.LastWriteTimeUtc.Ticks).AppendLine();return;}metadata.Append(current).AppendLine();foreach(string child in System.IO.Directory.EnumerateFileSystemEntries(current).Order(StringComparer.OrdinalIgnoreCase))Walk(child);}
        Walk(path);return new(root,path,directory,bytes,files,Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(metadata.ToString()))));
    }
    static void ValidateRoot(string root)
    {
        for(string? p=root;p!=null;p=System.IO.Path.GetDirectoryName(p))
            if((File.GetAttributes(p)&FileAttributes.ReparsePoint)!=0)
                throw new IOException("Backup cleanup refuses linked archive paths.");
    }
    public static IReadOnlyList<BackupCleanupEntry> Preview(string root,CancellationToken token=default)
    {
        root=Full(root);if(root==System.IO.Path.GetPathRoot(root))throw new IOException("Choose a dedicated source archive, not a drive root.");
        if(!System.IO.Directory.Exists(root))throw new DirectoryNotFoundException(root);
        ValidateRoot(root);
        var result=new List<BackupCleanupEntry>();
        void Walk(string folder){token.ThrowIfCancellationRequested();if((File.GetAttributes(folder)&FileAttributes.ReparsePoint)!=0)return;
            foreach(var file in System.IO.Directory.EnumerateFiles(folder))if(BackupFile(System.IO.Path.GetFileName(file)) && (File.GetAttributes(file)&FileAttributes.ReparsePoint)==0)result.Add(Inspect(root,file,false,token));
            bool project=System.IO.Directory.EnumerateFiles(folder,"*.csproj").Any();
            foreach(var child in System.IO.Directory.EnumerateDirectories(folder)){
                token.ThrowIfCancellationRequested();if((File.GetAttributes(child)&FileAttributes.ReparsePoint)!=0)continue;string name=System.IO.Path.GetFileName(child);
                if(KnownFolders.Contains(name)||project&&(name.Equals("backup",StringComparison.OrdinalIgnoreCase)||name.Equals("backups",StringComparison.OrdinalIgnoreCase)))result.Add(Inspect(root,child,true,token));
                else if(!new[]{"bin","obj",".git",".vs",".idea","node_modules","XnaRedirect-Test","XnaRedirect-Patched","Content"}.Contains(name,StringComparer.OrdinalIgnoreCase)&&!name.StartsWith("584E",StringComparison.OrdinalIgnoreCase))Walk(child);
            }
        }
        Walk(root);return result;
    }
    public static void Delete(BackupCleanupEntry entry, Action<long>? fileDeleted = null)
    {
        ValidateRoot(Full(entry.Root));
        Validate(entry.Root,entry.Path);
        if(!BackupFile(System.IO.Path.GetFileName(entry.Path))&&!KnownFolders.Contains(System.IO.Path.GetFileName(entry.Path))&&!(entry.Directory&&new[]{"backup","backups"}.Contains(System.IO.Path.GetFileName(entry.Path),StringComparer.OrdinalIgnoreCase)))throw new IOException("Not a recognized backup.");
        var current=Inspect(entry.Root,entry.Path,entry.Directory,CancellationToken.None);
        if(current.Fingerprint!=entry.Fingerprint)throw new IOException("Backup changed since preview; scan again: "+entry.Path);
        var files = new List<(string Path, long Bytes, DateTime Modified)>();
        var directories = new List<string>();
        void Collect(string path)
        {
            Validate(entry.Root,path);
            if(System.IO.Directory.Exists(path)) {
                foreach(var child in System.IO.Directory.EnumerateFileSystemEntries(path))Collect(child);
                directories.Add(path);
            } else {
                var info=new FileInfo(path);files.Add((path,info.Length,info.LastWriteTimeUtc));
            }
        }
        Collect(entry.Path);
        if(Inspect(entry.Root,entry.Path,entry.Directory,CancellationToken.None).Fingerprint!=entry.Fingerprint)
            throw new IOException("Backup changed since preview; scan again: "+entry.Path);
        foreach(var file in files) {
            Validate(entry.Root,file.Path);
            var info=new FileInfo(file.Path);
            if(info.Length!=file.Bytes || info.LastWriteTimeUtc!=file.Modified)throw new IOException("Backup changed since preview: "+file.Path);
            File.Delete(file.Path);
            fileDeleted?.Invoke(file.Bytes);
        }
        foreach(var directory in directories) {
            Validate(entry.Root,directory);
            System.IO.Directory.Delete(directory,recursive:false);
        }
    }
}
