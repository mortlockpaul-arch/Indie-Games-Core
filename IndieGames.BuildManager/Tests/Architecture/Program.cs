using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using IndieGames.BuildManager.Services;

if (args.Length > 0)
{
    var report = ArchitectureDiagnostics.Inspect(args[0], args.Length > 1 ? args[1] : "x64", "");
    foreach (var issue in report) Console.WriteLine($"[{issue.Severity}] {issue.Code}: {issue.Detail}");
    return report.Any(i => i.Severity == "Error") ? 1 : 0;
}

string root = Path.Combine(Path.GetTempPath(), "ArchitectureChecks-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
try
{
    string game = Path.Combine(root, "Game.dll");
    void Write(string name, Machine machine, CorFlags flags)
    {
        var metadata = new MetadataBuilder();
        metadata.AddModule(0, metadata.GetOrAddString(name), metadata.GetOrAddGuid(Guid.NewGuid()), default, default);
        metadata.AddAssembly(metadata.GetOrAddString(Path.GetFileNameWithoutExtension(name)), new Version(1, 0),
            default, default, (AssemblyFlags)0, (AssemblyHashAlgorithm)0);
        var pe = new ManagedPEBuilder(new PEHeaderBuilder(machine: machine,
                imageCharacteristics: Characteristics.ExecutableImage | Characteristics.Dll),
            new MetadataRootBuilder(metadata), new System.Reflection.Metadata.BlobBuilder(), flags: flags);
        var blob = new System.Reflection.Metadata.BlobBuilder();
        pe.Serialize(blob);
        File.WriteAllBytes(Path.Combine(root, name), blob.ToArray());
    }
    void Expect(string code, bool present, string target = "x64", string rid = "")
    {
        var issues = ArchitectureDiagnostics.Inspect(game, target, rid);
        if (issues.Any(i => i.Code == code) != present)
            throw new Exception($"Expected {code} present={present}: {string.Join("; ", issues.Select(i => i.Detail))}");
    }

    Expect("ARCH_OUTPUT_MISSING", true);
    Write("Game.dll", Machine.Amd64, CorFlags.ILOnly);
    Write("Game.exe", Machine.Amd64, CorFlags.ILOnly);
    Write("Portable.dll", Machine.I386, CorFlags.ILOnly);
    Expect("ARCH_OK", true);
    Expect("ARCH_MISMATCH", false);
    Write("Portable.dll", Machine.I386, CorFlags.ILOnly | CorFlags.Prefers32Bit);
    Expect("ARCH_MISMATCH", false);
    Write("FNA.dll", Machine.I386, CorFlags.ILOnly | CorFlags.Requires32Bit);
    Expect("ARCH_MISMATCH", true);
    File.Delete(Path.Combine(root, "FNA.dll"));
    Write("Native.dll", Machine.I386, (CorFlags)0);
    Expect("ARCH_MISMATCH", true);
    Write("Native.dll", Machine.Amd64, (CorFlags)0);
    Expect("ARCH_OK", true);
    Expect("ARCH_SETTINGS", true, "x86", "win-x64");
    Expect("ARCH_STALE_OUTPUT", true, "x86");
    Write("Game.exe", Machine.I386, CorFlags.ILOnly | CorFlags.Requires32Bit);
    Expect("ARCH_MISMATCH", true);
    File.Delete(Path.Combine(root, "Game.exe"));
    File.Delete(Path.Combine(root, "Native.dll"));
    Write("Game.dll", Machine.Arm64, CorFlags.ILOnly);
    Expect("ARCH_OK", true, "arm64", "win-arm64");
    File.WriteAllText(Path.Combine(root, "Broken.dll"), "invalid binary");
    Expect("ARCH_UNREADABLE", true, "arm64");
    using var cancellation = new CancellationTokenSource();
    cancellation.Cancel();
    try { ArchitectureDiagnostics.Inspect(game, "arm64", "", cancellation.Token); throw new Exception("Cancellation ignored"); }
    catch (OperationCanceledException) { }
    string projectPath = Path.Combine(root, "Example.csproj");
    string original = "<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net10.0</TargetFramework><Platforms>x86;x64</Platforms><PlatformTarget>x86</PlatformTarget><RuntimeIdentifier>win-x86</RuntimeIdentifier></PropertyGroup><PropertyGroup Condition=\"'$(Configuration)' == 'Release'\"><PlatformTarget>x86</PlatformTarget></PropertyGroup></Project>";
    File.WriteAllText(projectPath, original);
    var saved = ProjectArchitectureUpdater.Apply(projectPath, "x64");
    if (saved.PlatformTarget != "x64" || saved.RuntimeIdentifier != "win-x64") throw new Exception("Architecture settings were not saved consistently");
    if (File.ReadAllText(projectPath + ".before-architecture.bak") != original) throw new Exception("Original project backup was not preserved");
    var scanned = ProjectScanner.Scan(root).Single();
    if (scanned.PlatformTarget != "x64" || scanned.RuntimeIdentifier != "win-x64") throw new Exception("Rescan did not show saved architecture");
    ProjectArchitectureUpdater.Apply(projectPath, "x86");
    var document = System.Xml.Linq.XDocument.Load(projectPath);
    if (document.Descendants().Count(e => (string?)e.Attribute("Label") == ProjectArchitectureUpdater.Label) != 1) throw new Exception("Repeated saves duplicate settings");
    foreach (string name in new[] { "Platform", "PlatformTarget", "Platforms", "Prefer32Bit", "RuntimeIdentifier" })
        if (document.Descendants(name).Count() != 1) throw new Exception("Duplicate architecture setting: " + name);
    if (File.ReadAllText(projectPath + ".before-architecture.bak") != original) throw new Exception("Repeated save overwrote original backup");
    ProjectArchitectureUpdater.Apply(projectPath, "AnyCPU");
    if (ProjectScanner.Scan(root).Single().PlatformTarget != "AnyCPU") throw new Exception("AnyCPU save failed");
    try { ProjectArchitectureUpdater.Apply(projectPath, "invalid"); throw new Exception("Invalid architecture accepted"); }
    catch (ArgumentException) { }
    Console.WriteLine("PASS explicit architecture saves, backup preservation, runtime alignment and rescan consistency.");
    Console.WriteLine("PASS: x86/x64/ARM64, AnyCPU DLLs, native mismatches, stale output, RID conflicts, missing/corrupt output and cancellation.");
    return 0;
}
finally { Directory.Delete(root, recursive: true); }
