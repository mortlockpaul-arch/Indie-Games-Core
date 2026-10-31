# Indie Games Build Manager (WPF, .NET 10)

A starter desktop application for discovering game projects and diagnosing configuration/platform mismatches.

## Run
```powershell
cd C:\source\Indie-Games\IndieGames.BuildManager
dotnet run
```
Requires Windows and .NET 10 SDK.

## Current capabilities
- Recursively discovers .csproj files in the selected archive folder.
- Shows declared framework, PlatformTarget, runtime, and build status.
- Inspects evaluated MSBuild properties for a selected project.
- Builds, rebuilds, cleans, and restores selected projects, sequentially.
- Lets you independently set `Platform` (MSBuild project/solution configuration) and `PlatformTarget` (compiler target).
- Allows optional target framework/runtime overrides, and an optional full Visual Studio MSBuild.exe path.
- Captures output and supports cancellation.

## Important
- The default platform values are editable; do not assume x64 is valid for every legacy project.
- A project's declared XML properties can differ from evaluated MSBuild properties. Use **Inspect properties** for diagnosis.
- Project-specific `Directory.Build.props`, conditions, and solution configuration mappings may override or conflict with your choices.
- For legacy .NET Framework projects, use Visual Studio MSBuild and the relevant targeting packs.
- PowerShell fixer support is a placeholder: the current button runs the chosen script **without arguments**, because existing scripts have different parameter contracts. Do not run a fixer until its parameters are wired correctly.
- No project files are modified automatically by this application. Builds/restore and user-selected scripts can still change generated files or project files respectively.

## Next iteration
- Parse .sln/.slnx configuration mappings, evaluate all platform/configuration pairs.
- Add per-project saved build profiles.
- Wire fixers to their actual PowerShell parameter sets and implement dry-run/backups.
- Parse MSBuild error codes into a diagnostics grid.


## Detect Configuration Issues
Select projects, set Configuration/Platform and optional framework/RID, then click **Detect Configuration Issues**.
The scanner evaluates MSBuild properties and checks NuGet assets for missing framework/RID targets (NETSDK1005/NETSDK1047), unsupported declared platforms/configurations, and PlatformTarget differences.
**Read-only:** no project files are changed. This is a targeted preflight, not a full solution configuration matrix or guaranteed build diagnosis.

## DefineConstants
The Advanced Build panel has an optional `DefineConstants` field, e.g. `STEAMWORKS_WIN;TRACE`.
Leave it empty to use the project's evaluated symbols. When populated, it passes an explicit
MSBuild property override (semicolon-escaped as `%3B`) for the selected build.
This replaces existing evaluated symbols rather than appending them; include all required
symbols or add the new symbol to a suitable conditional project PropertyGroup instead.
The override does not edit project files and does not apply to Restore/Clean actions.

## Startup Diagnostics Injection
Select one or more projects, then click **Inject Startup Diagnostics**. Adds runtime, process architecture, OS, executable and working directory logging to a conventional `static void Main` or `static int Main`. Backs up the source file with `.startup-diagnostics.bak`, skips multiple/absent Main methods and repeat injections. Review generated changes; this is a conservative text-based injector, not a full C# syntax parser. Console output still requires a visible/attached console or debugger output capture.
