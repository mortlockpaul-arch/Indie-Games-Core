# Shader Demo Browser

An animated FNA browser for all 43 shader repair templates: 25 Dead.Kings effects, 14 Adamant Ants effects and four earlier shared repairs. This is separate from the DigitalRune Sample Tester. No runnable SunBurn sample suite was found; Sunburn-Console is a placeholder and is not presented as a working demo.

Launch from Build Manager → Repairs → Shader demos, or run ShaderDemoBrowser.exe. Type to filter, click a row or use arrow keys, press Tab / Next technique to cycle techniques, and Space / Pause to stop animation. The intensity slider affects parameters exposed by applicable effects.

Post-process demos use a moving synthetic image; mesh, instancing, particle and line effects use synthetic geometry and textures, including an identity animation atlas. Shadow/fog techniques can intentionally produce dark masks. These previews verify that the shader can draw; they do not establish original Xbox visual parity or that a complete game runs correctly.

Recovered shaders use instruction-level HLSL. High-level matrix reduction produced incorrect transforms in several effects and is not used. The shadow-mask shader uses the equivalent minimal world/view/projection program and opaque-black pixel result; expanded generated code triggered a native failure. Original recovered source is retained in the analysis archive. Centroid qualifiers are replaced by standard interpolation for FNA SDL_GPU compatibility, which can change multisampled edges.

Build with the Windows SDK shader compiler installed:

```powershell
dotnet publish ShaderDemoBrowser.csproj -c Release -o publish
```

The published folder needs the .NET 9 Desktop Runtime and includes FNA's native libraries and compiled shader assets. The compiler is needed only when building. Source shaders are embedded for the preview fixtures. FNA source is not changed. Engine code and licensing are independent of the game-specific recovered shader content; this package is for the user's local rebuild workflow.

`--smoke-all <folder>` renders all previews and writes images/results.json. `--only <effect>` limits that run. `--effect <effect> --capture <file.png>` captures the browser. Errors are shown explicitly instead of being silently replaced by another effect.
