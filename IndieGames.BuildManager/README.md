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
The override does not edit project files; BuildManager passes matching build properties to restore as well.

## Startup Diagnostics Injection
Select one or more projects, then click **Inject Startup Diagnostics**. Adds runtime, process architecture, OS, executable and working directory logging to a conventional `static void Main` or `static int Main`. Backs up the source file with `.startup-diagnostics.bak`, skips multiple/absent Main methods and repeat injections. Review generated changes; this is a conservative text-based injector, not a full C# syntax parser. Console output still requires a visible/attached console or debugger output capture.

## Project torrent catalogue and sharing

Open **Project torrent catalogue / sharing…** from the main window. Select a game row first to prefill its archive folder for sharing.

### Share a project

1. Expand **Create a share**, choose the game's folder, and click **Preview files**.
2. Review the source/Content file list. Build outputs (`bin`/`obj`), version-control metadata, backups, logs, conversion staging and extracted `584E*` package folders are excluded; directory links are not followed.
3. Optionally add tracker URLs. Click **Create torrent and seed** to create an immutable ZIP snapshot and begin seeding it.
4. Click **Export catalogue…**. Share the JSON file with recipients or host it at an HTTP/HTTPS URL; recipients load it in the catalogue window. The file embeds torrent metadata, so no separate `.torrent` hosting is necessary.
5. Keep the catalogue window open while seeding. Editing source after snapshot creation does not change the shared package; create a new share to publish another version.

### Download and install

1. Open the shared catalogue file or paste its URL and click **Load**.
2. Select a project and choose **Download / resume**. Progress, connected peers and transfer rates are displayed. Downloads also upload verified pieces while running.
3. Choose **Verify and install** after completion. Torrent pieces and the ZIP's SHA-256 are checked before extraction; entry paths and declared sizes are validated. Existing project folders are never overwritten.
4. The installed project appears in the main project list. Downloading does not run or build it. Source project references remain as published and may need the usual local FNA/core configuration before building.

**Pause** suspends a transfer; **Stop** stops it while preserving cached files for resume. **Cancel operation** cancels packaging/catalogue loading/installation; use Pause or Stop for transfers already running. Closing the catalogue or application stops the client and saves resume data. Reopen the catalogue and choose Download / resume or Seed selected to restart a transfer; transfers do not automatically restart on application launch.

Snapshots, the local `my-catalogue.json`, downloads and resume data live in the configured torrent-cache folder (default `D:\indie-torrent-cache`). Existing `.torrent-sharing` caches are not moved automatically; choose that old folder in storage settings to keep using one. Keep the ZIP snapshots to continue seeding. Changing the install destination does not move this cache.

Internet peers use DHT and optional trackers; a catalogue only lists projects and does not host their payloads. At least one reachable seeder is needed. The default listening port is TCP/UDP 6881; change it before the first transfer. Optional UPnP/NAT-PMP port forwarding is off by default; manual forwarding may be needed for internet seeding. The basic implementation does not include a hosted catalogue service, accounts, torrent signing, or automatic source updates. Obtain catalogues from sources you trust; hashes verify integrity, not the publisher's identity.

### Integration checks

Run `dotnet run --project Tests/TorrentSharing/TorrentSharing.Tests.csproj` from this folder. The test creates synthetic source/content and transfers it between two loopback-only clients (DHT, trackers, local discovery and router port mapping disabled). It checks snapshot exclusions, catalogue round-trip, actual transfer, pause/resume, stopped-transfer resume, verified extraction, refusal to overwrite existing projects, checksum failures, ZIP traversal rejection and cancellation. It prints its temporary artifact directory; no archive game content is shared.


## Storage folders and playable game sharing

Open **Storage folders / cleanup…** to choose and save four separate roots:

| Files | Default folder |
|---|---|
| Source projects and original Content | `D:\indie-game-archive` |
| Playable Windows builds | `D:\indie-games` |
| Intermediate files and shared dependency build outputs | `D:\indie-build-cache` |
| Torrent snapshots, downloads and resume data | `D:\indie-torrent-cache` |

Settings are saved to `%LOCALAPPDATA%\IndieGames.BuildManager\storage.json`. Roots cannot overlap. Saving settings does not move or delete existing files.

BuildManager build/restore/clean/rebuild actions inject the shipped `BuildManager.Layout.props` and `.targets`; project source and existing archive imports remain in place. Intermediate files go into cache `obj`, referenced project outputs into cache `bin`, and the selected executable project builds directly into its game folder. Its Content, managed DLLs and native dependencies follow the normal build copy targets. Project folder identities include a path hash to avoid collisions between identically named projects. The game folder holds the current selected build; use a separate Games root if you want to retain another configuration/architecture. **Run Game** uses this location, with a fallback to existing project `bin` output. Visual Studio builds retain their existing output layout. Custom projects that override MSBuild hooks or output properties may need adjustment; select one framework for multitargeted games.

**Preview build cache cleanup** lists cache `bin`/`obj` folders and sizes. **Preview archive bin/obj cleanup** lists existing outputs next to `.csproj` files. Only pressing **Delete previewed folders** and confirming deletes them. Cleanup rejects linked paths and paths outside the selected root. Source, Content and playable games are not included. Close games/build tools before cleaning; a later restore/build recreates required intermediate files. Torrent snapshots are retained for continued seeding and download resume; manage that separate folder after stopping transfers.

In the torrent window choose **Source**, **Playable game**, or **Both** before **Preview files**. The source folder is the original archive project; the game folder is its built Windows output (prefilled for the selected project). Build the game first. A playable package includes executable, Content, dependencies, and runtime metadata, while excluding source, PDBs, development runtime configuration, backups and generated build folders. Its entry point must have matching `.runtimeconfig.json` and `.deps.json`; framework-dependent builds still require the matching .NET runtime on the recipient's PC.

**Both** creates two independent catalogue entries labelled **Source** and **Game**. Ctrl/Shift-select either or both rows and choose **Download / resume**. When complete, **Verify and install** routes Source to the archive and Game to the games folder. Existing destinations are refused. Game packages do not require a `.csproj`. Select a Game entry and click **Run installed game**, or use **Open games folder**. Installing never automatically executes a game. Older catalogues without a package kind load as Source.

Run `./Tests/TorrentSharing/BuildLayout.Integration.ps1` to check two identically named synthetic game projects sharing one dependency. It verifies central intermediate files, direct game outputs, Content/native post-build copies, executable launch, and absence of source `bin`/`obj`. Pass `-ArtifactRoot <new-folder>` to keep the fixtures in a chosen location.
