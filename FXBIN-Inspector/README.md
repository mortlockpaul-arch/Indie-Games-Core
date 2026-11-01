# FXBIN Inspector

Read-only .NET 9 console application for inspecting recovered Xbox 360 XNB EffectReader `.fxbin` files.

## Build

```powershell
dotnet publish .\FXBIN-Inspector.csproj -c Release -r win-x64 --self-contained false
```

## Run

```powershell
.\bin\Release\net9.0\win-x64\publish\FxbinInspector.exe 'D:\temp\xnb-effect-test\MurkyWater.fxbin' --json 'D:\temp\murkywater-report.json'

.\bin\Release\net9.0\win-x64\publish\FxbinInspector.exe 'D:\temp\xnb-effect-test' --recursive --json 'D:\temp\fxbin-report.json'
```

The tool reports file length, first 16 bytes, signature hints, SHA-256, printable ASCII strings with offsets, and candidate shader profiles and names. Candidate names are *heuristics*, not confirmed parsed parameters or techniques. The legacy Direct3D effect signature `BC F0 0B CF` is recognized, but internal effect structures are not decoded. This tool does not convert Xbox 360 shader bytecode to FNA-compatible bytecode.
