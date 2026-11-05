# Indie-Games-Core

A Windows workspace for recovering, repairing and rebuilding managed XNA / Xbox Live Indie Game projects. It contains **Indie Games Build Manager**, shared framework projects and supporting conversion tools.

[Get the packaged frontend on itch.io](https://pmortlock.itch.io/xenia-game-manager) · [Build Manager guide](IndieGames.BuildManager/README.md) · [Global catalogue](https://github.com/mortlockpaul-arch/Indie-Games-Catalogue)

## How the pieces fit together

| Component | Role |
| --- | --- |
| Python frontend, distributed on itch.io | Manages extraction and ILSpy decompilation, prepares recovered projects and opens Build Manager. |
| This repository | Supplies the C# Build Manager source, shared FNA/MonoGame-related projects and repair/conversion tooling. |
| Build Manager | Discovers projects, diagnoses build configuration, repairs supported references/content, builds Windows outputs and launches games for testing. |

The frontend and Build Manager use the same recovered project folders. The Python frontend is distributed separately; it is not contained in this repository. This is an independent toolchain, not an official Xenia release.

The workflow is **extract → decompile → repair/convert → rebuild → test**. Decompilation recovers C# from managed assemblies; compatibility varies and some games still need manual source or content fixes.

## Build Manager 1.1.3

- Restore, build, rebuild and clean selected projects with captured output and cancellation.
- Inspect evaluated MSBuild properties and diagnose framework/configuration mismatches.
- Apply supported reference and content repairs, including known shader repairs.
- Browse an automatically refreshed global catalogue with owner-reviewed submissions.
- Start or resume saved torrent transfers from the main window; **Start selected** downloads or seeds a catalogue package.
- Preserve download metadata and partial transfers across restarts.
- Resize output/transfer panels and open the window on the launcher's monitor.

Starting a transfer does not install it. Choose **Verify And Install** after completion. GitHub hosts catalogue metadata; a reachable torrent seeder is still required.

## Build and run from source

Use Windows and Git. Install the SDK selected by [global.json](global.json): currently **10.0.401**, with `latestPatch` roll-forward within its feature band. A compatible 10.0.4xx SDK can satisfy this pin. A .NET runtime alone cannot build the application.

```powershell
git clone --recurse-submodules https://github.com/mortlockpaul-arch/Indie-Games-Core.git
cd Indie-Games-Core
dotnet --list-sdks
dotnet run --project .\IndieGames.BuildManager\IndieGames.BuildManager.csproj
```

For a Release build:

```powershell
dotnet build .\IndieGames.BuildManager\IndieGames.BuildManager.csproj -c Release
```

Build the manager project first. Recovered game projects may require additional targeting packs, legacy Visual Studio MSBuild, framework dependencies or repairs. A self-contained packaged manager includes its runtime, but building games still requires the appropriate SDK and tools.

Alternatively, open `Indie-Games.slnx` in Visual Studio with .NET 10 support and select **IndieGames.BuildManager** as the startup project.

Update an existing clone from its repository folder:

```powershell
git pull --recurse-submodules
git submodule update --init --recursive
```

Keep local work committed or backed up before updating. Downloading a source ZIP does not initialize Git submodules.

## First use

1. Open **Storage folders / cleanup…** and choose separate source archive, playable games, build cache and torrent cache folders. Change the default `D:` paths if needed. Saving settings does not move files.
2. Point the project list at the recovered source archive and scan for projects. Use the same source archive in the Python frontend.
3. Select a game, inspect its framework/platform requirements and run **Detect Configuration Issues**. Review appropriate repairs, then restore/build.
4. Review build output and use **Run Game** to test a successful build. Repeat fixes as needed.

See the [detailed manager guide](IndieGames.BuildManager/README.md) for build overrides, content conversion, diagnostics, storage and portable deployment.

## Global catalogue and contributions

The approved catalogue loads automatically from [Indie-Games-Catalogue](https://github.com/mortlockpaul-arch/Indie-Games-Catalogue). Users do not need to exchange catalogue files; a verified cache supports offline browsing.

To add a project, choose **Connect GitHub**, then **Publish project**. Review the package preview and choose **Publish for approval**. Build Manager creates a catalogue pull request; external contributors use their own fork. The catalogue owner reviews submissions before merging. Accepted entries appear automatically for other users.

Keep Build Manager running to seed the package. Approval does not host its payload or guarantee that a recovered project builds. Browsing and downloading require no GitHub account; submission requires sign-in.

For tool bugs, open an issue here with the manager version, relevant build error and reproduction steps. Share only files you have permission to distribute. Third-party projects and game assets retain their own licensing requirements.
