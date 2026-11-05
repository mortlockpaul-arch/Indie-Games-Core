# Indie Games Build Manager (WPF, .NET 10)

A Windows desktop application for discovering, diagnosing, repairing and rebuilding recovered XNA / Xbox Live Indie Game projects. Current release: **1.1.3**.

The separately distributed [Python frontend](https://pmortlock.itch.io/xenia-game-manager) handles extraction and ILSpy decompilation and launches Build Manager. Both use the same recovered project folders. See the [repository overview and setup guide](../README.md) for the complete workflow.

## Run

Open `Indie-Games.slnx` in Visual Studio. Build Manager is the solution's default startup project.
If Visual Studio has already saved a different startup selection, right-click **IndieGames.BuildManager** and choose **Set as Startup Project** once.

From the repository root:

```powershell
dotnet run --project .\IndieGames.BuildManager\IndieGames.BuildManager.csproj
```
Requires Windows and the SDK selected by `global.json` (currently 10.0.401, with compatible feature-band patches allowed). A runtime alone is insufficient for source builds. Initialize Git submodules before building.

## Current capabilities
- Recursively discovers .csproj files in the selected archive folder.
- Shows declared framework, PlatformTarget, runtime, and build status.
- Inspects evaluated MSBuild properties for a selected project.
- Builds, rebuilds, cleans, and restores selected projects, sequentially.
- Lets you independently set `Platform` (MSBuild project/solution configuration) and `PlatformTarget` (compiler target).
- Allows optional target framework/runtime overrides, and an optional full Visual Studio MSBuild.exe path.
- Captures output and supports cancellation.
- Repairs supported references/content, including known shaders, and launches built games for testing.
- Configures separate source archive, playable game, build cache and torrent folders.
- Loads the global catalogue automatically and submits packages for owner approval.
- Starts/resumes saved torrent transfers and downloads or seeds selected packages; verification and installation are separate actions.
- Provides resizable output/transfer panels and monitor-aware window placement.

## Important
- The default platform values are editable; do not assume x64 is valid for every legacy project.
- A project's declared XML properties can differ from evaluated MSBuild properties. Use **Inspect properties** for diagnosis.
- Project-specific `Directory.Build.props`, conditions, and solution configuration mappings may override or conflict with your choices.
- For legacy .NET Framework projects, use Visual Studio MSBuild and the relevant targeting packs.
- Repairs and conversions can modify project files. Review changes and retain backups; some recovered projects still need manual source or content fixes.

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

## Global project catalogue (1.1.0)

Build Manager automatically reads the approved catalogue from [Indie-Games-Catalogue](https://github.com/mortlockpaul-arch/Indie-Games-Catalogue). No catalogue file needs to be opened, saved or exported. The catalogue refreshes when opened, every five minutes while visible, and with **Refresh catalogue**. A fully verified copy is cached for offline browsing; a failed or malformed download does not replace it.

### Publish a project

1. Choose **Connect GitHub** and complete the browser/terminal sign-in. The app installs GitHub CLI if needed; GitHub CLI manages the credential. Browsing and downloading need no account.
2. Choose **Publish project**, select source files, a playable build, or both, and review the preview. Publish only material you have permission to share. Builds, backups, logs and version-control metadata are excluded from source packages; directory links are not followed.
3. Choose **Publish for approval**. Build Manager packages and seeds the selected files, then submits metadata to a GitHub pull request in the dedicated catalogue repository. External contributors use their own fork; nobody receives a shared write token.
4. The catalogue owner reviews and merges accepted requests. GitHub Actions validates and publishes approved entries; recipients then see them automatically. **My submissions** opens the contributor's requests; **Review submissions** opens the owner's review queue. There is no automatic merge.
5. Keep Build Manager running to seed the package. Changing files creates a new snapshot; unchanged files reuse a valid existing package. Repeated submission reuses its request. A rejected request stays closed; modify the package to create a new snapshot.

Existing local shares remain visible as **Local · not submitted** and can be migrated with **Publish selected**. Failed submissions retain packages and seeding state so they can be retried. **Awaiting approval** packages are local to the contributor, not part of the global catalogue. Accepted entries show **Approved** once the published catalogue contains them. The initial catalogue is empty until the owner accepts a submission.

### Download and install

Select an approved package and choose **Start selected**, then **Verify And Install** once complete. Torrent pieces and ZIP SHA-256 are checked before extraction. Paths, sizes and contents are validated; existing destination folders are never overwritten. Source goes to the configured archive; playable builds go to the games folder. Installation never runs code automatically. **Run Installed Game** launches a selected playable package. Source may need the usual shared-framework configuration before building.

**Pause** and **Stop** preserve cached data for resume. Closing the catalogue hides it while transfers continue; **Stop Torrents** or exiting Build Manager stops the client. **Remove Selected** hides entries on this PC and stops their transfers while retaining files. It never removes global entries. **Show hidden packages** restores the local view. Retiring a public entry requires an owner-reviewed change in the catalogue repository.

Packages, legacy `my-catalogue.json` transfer metadata, publication status, verified global cache and resume data stay in the configured torrent cache. Local bookkeeping remains automatic. Changing storage folders does not move existing data. GitHub hosts metadata, not the ZIP payload: a reachable seeder is still needed. TCP/UDP port 6881 is the default; optional router forwarding is off by default. Approval is moderation, not a guarantee of compatibility or executable safety.

### Integration checks

- `dotnet run --project IndieGames.BuildManager/Tests/GlobalCatalogue/GlobalCatalogue.Tests.csproj -c Release`: verified online/offline cache, invalid-download preservation, cancellation, contributor fork/PR flow, owner review path, pending/approved separation, idempotent retry, partial failure recovery and authorization-header handling. Uses synthetic metadata and a fake GitHub API by default.
- `dotnet run --project IndieGames.BuildManager/Tests/TorrentSharing/TorrentSharing.Tests.csproj -c Release`: real loopback transfers, pause/resume, package validation, extraction and cleanup boundaries. Never shares archive game content.
- `dotnet run --project IndieGames.BuildManager/Tests/UiSmoke/UiSmoke.Tests.csproj -c Release`: automatic catalogue UI, publication action, share previews and window layout. Uses an injected local catalogue and no GitHub access.

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

**Both** creates two independent catalogue entries labelled **Source** and **Game**. Ctrl/Shift-select either or both rows and choose **Start selected**. When complete, **Verify and install** routes Source to the archive and Game to the games folder. Existing destinations are refused. Game packages do not require a `.csproj`. Select a Game entry and click **Run installed game**, or use **Open games folder**. Installing never automatically executes a game. Older catalogues without a package kind load as Source.

Run `./Tests/TorrentSharing/BuildLayout.Integration.ps1` to check two identically named synthetic game projects sharing one dependency. It verifies central intermediate files, direct game outputs, Content/native post-build copies, executable launch, and absence of source `bin`/`obj`. Pass `-ArtifactRoot <new-folder>` to keep the fixtures in a chosen location.

## Known shader repairs (1.0.4)

Select game rows, open the XNB Effect Conversion section, and choose **Repair known shaders (selected games)**. This action is independent of the extractor-based conversion button and does not require XnbExtractor.

Supported shader fingerprints are the Maximinus `RoundLine`, billiards `spotlight`, `Blur` (`GaussianBlur` technique), and standard 36-byte billboard `ParticleEffect` assets found in 21 Ball Snooker LIVE. RoundLine's FNA vertex/index buffer counts are repaired as part of the operation. Only recognized originals or previously managed shader assets are replaced. Existing custom HLSL is preserved and reported as a conflict; unrelated binaries with the same filename are reported as unsupported.

The action installs editable `.fx` files, Windows XNB assets, `build/BuildKnownShaders.ps1`, a manifest, and an incremental `BuildKnownShaders` project target. It migrates the earlier RoundLine-only target. The Windows SDK's `fxc.exe` is detected automatically. Rebuild the game after repair to deploy its updated content. New edits to the managed HLSL rebuild on subsequent game builds.

Before committing, shaders are compiled in staging. Changes are backed up under each game's `shader-repair-backups` folder, which is excluded from the game build; failed commits roll back completed writes. Repeat runs skip unchanged repairs, including when the compiler is unavailable.

Tests:
- `dotnet run --project Tests/KnownShaders/KnownShaders.Tests.csproj`: repair integration tests, backups, unknown/custom shader protection, idempotency, missing compiler, cancellation, rollback, and path validation. Requires the Windows SDK. Uses bundled original fixtures.
- `dotnet run --project Tests/KnownShadersGpu/KnownShadersGpu.csproj -- <game-Content-folder> <preview.png>`: offscreen FNA/Vulkan tests for all four effects. Requires a built FNA.Core and matching native libraries; the default renderer source root is the local Snooker LIVE port and can be overridden with the `RoundLineSourceRoot` MSBuild property.

Shader implementations are desktop reconstructions of the original contracts, not recovered Xbox HLSL. Full gameplay and other shader fingerprints are outside these component tests.
## Portable build and deployment

Build Manager includes its PowerShell scripts and embedded build help in this repository. No separate Python frontend checkout is required to build it. To deliberately use another script checkout, pass `-p:SharedScriptsDirectory=<folder>`.

Publish a self-contained Windows x64 build:

```powershell
dotnet publish IndieGames.BuildManager/IndieGames.BuildManager.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o artifacts/build-manager
```

Deploy the complete publish folder to the Python frontend's `src/assets/tools/build_manager` folder. The manager itself includes the .NET runtime; building game projects still requires their SDKs and targeting packs. Close Build Manager before replacing the deployed files.

## One-click torrents (1.1.3)

Choose **Start / resume torrents** in the main window to resume saved downloads and seed local packages. Transfers appear in the bottom Torrents tab without opening the catalogue. If no transfers are saved, the catalogue opens so you can select a package and choose **Start selected**. This same action automatically seeds complete cached packages or downloads missing pieces. Downloads are remembered for restart; verification and installation remain separate. No new catalogue projects are automatically downloaded, and publishing still requires approval. **Stop torrents** stops all transfers while retaining their cached files and saved metadata.
