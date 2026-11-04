# XNA Effect Decompiler

Uses [AndresTraks/HlslDecompiler](https://github.com/AndresTraks/HlslDecompiler), pinned to revision `db004b9094e9d7ef1564ad1815e738d2273b2325`, under its MIT license. Local adaptations read Xbox's big-endian effect container, translate its state assignments and provide a virtual scratch constant for intermediate HLSL recovery. The desktop compiler remains responsible for validating shader-model register limits.

```powershell
dotnet build XnaEffectDecompiler.csproj
dotnet run --project XnaEffectDecompiler.csproj -- "input.xnb" "output.fx"
```

Inputs can be Xbox effect XNBs or extracted Xbox effect containers. The tool decodes XNB LZX compression using the existing FNA decoder, recovers variables/defaults, techniques, passes, texture bindings and shader calculations, then writes HLSL and metadata. It does not change the input or install the result. Existing output requires `--force`.

Build FNA.Contents for x64 first. The tool references its existing FNA.dll; alternatively pass `-p:FnaAssembly=<path-to-FNA.dll>`. FNA source is not modified.

Instruction-level output is used to avoid incorrect high-level matrix reductions. Local fixes preserve zero-increment hardware loop trip counts and vector-valued vertex FOG inputs. Recovered code still needs compilation and visual testing. The known shadow-mask repair uses an equivalent minimal world/view/projection shader because the expanded generated variant triggered a native failure when drawn.

The default output replaces centroid interpolation with standard interpolation because the current FNA SDL_GPU/SPIR-V backend rejects centroid. The original is also saved as `.original.fx`. This can change multisampled edge colours. Use `--preserve-centroid` for a backend that supports it.

Build Manager installs only separately tested, fingerprinted templates. Generic output must still be compiled and tested; successful decompilation does not prove correct visuals or a working game. Unknown effect state assignments fail explicitly. Current state mappings were checked with compiler probes from Microsoft's public XNA Game Studio 3.1. Those Microsoft binaries are not included or redistributed here.

For desktop FNA effects, compile `.fx` using the Windows SDK fxc compiler (`/T fx_2_0 /O3`) and package the compiled effect in a Windows EffectReader XNB. Build Manager's shader repair installs this wrapper, original backups and an incremental rebuild hook automatically for recognized assets.
