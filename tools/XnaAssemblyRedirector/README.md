# XnaAssemblyRedirector (experimental)

A conservative Mono.Cecil-based **type-reference scope** patcher for legacy XNA DLLs.
It does not modify source DLLs. `scan` prints assembly references, `verify` checks that referenced XNA types exist in FNA/FNA.NetStub, and `patch` writes copies to an output directory.

## Install

```powershell
Copy-Item 'XnaAssemblyRedirector' 'C:\source\Indie-Games-Core\tools\XnaAssemblyRedirector' -Recurse
dotnet build 'C:\source\Indie-Games-Core\tools\XnaAssemblyRedirector\XnaAssemblyRedirector.csproj' -c Release
```

## Scan without loading game assemblies

```powershell
$tool = 'C:\source\Indie-Games-Core\tools\XnaAssemblyRedirector\bin\Release\net9.0\XnaAssemblyRedirector.dll'
$game = 'D:\indie-game-archive\$1 Zombie Game, The (World) (XBLIG)'
dotnet $tool --mode scan --input (Join-Path $game '584E07D1')
```

## Verify or patch (use the actual FNA.dll output path)

```powershell
$fna = 'C:\source\Indie-Games-Core\FNA\bin\x64\Debug\net8.0\FNA.dll'
$input = Join-Path $game '584E07D1'
$output = Join-Path $game 'obj\XnaRedirect'
dotnet $tool --mode verify --input $input --fna $fna
dotnet $tool --mode patch --input $input --fna $fna --output $output
```

Optional `--stub <path-to-FNA.NetStub.dll>` maps GamerServices/Net/Storage/Avatar references **only if the required types exist**.

## MSBuild integration (opt-in)

1. Copy `Directory.Build.XnaRedirect.targets` to `D:\indie-game-archive`.
2. Add `<Import Project="$(MSBuildThisFileDirectory)Directory.Build.XnaRedirect.targets" />` inside the **existing** `D:\indie-game-archive\Directory.Build.targets` `<Project>` element. Do not replace that file.
3. Edit `XnaRedirectFna`, `XnaRedirectStub` and `XnaRedirectRoot` in the imported file to point to actual builds.
4. Run `dotnet build <game.csproj> -p:EnableXnaDllRedirect=true -p:Platform=x64`.

The target executes before `ResolveAssemblyReferences`, patches DLLs in the game's `584E07D1`, then changes `Reference` HintPaths only when a patched copy exists. It intentionally does **not** patch `bin` or `obj` DLLs. Disable the feature by omitting the property.

## Limitations and safety

- **Experimental:** checks referenced type names, NOT method/field signatures, ABI, runtime compatibility, or IL validity. Passing verification does not guarantee the game compiles or runs.
- Some XNA types have no equivalent in FNA or FNA.NetStub; those DLLs are skipped and logged.
- Strong-named DLLs are skipped because rewriting would invalidate their signatures.
- Xbox 360 native/mixed-mode assemblies may be unreadable and are skipped.
- `scan`/`verify` are recommended first. Back up game projects and do not deploy patched binaries without testing.
- MSBuild target is intended for game projects using `<Reference HintPath="584E07D1\Some.dll" />`. Other reference mechanisms require separate integration.
- If a target fails with unresolved members or type identity mismatches, rebuild the original library against FNA when source is available.
