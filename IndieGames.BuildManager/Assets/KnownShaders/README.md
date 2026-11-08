# Known Shader Repairs

3D Pixel Art Studio's `InstancedModel` effect is also fingerprinted and recovered with the same decompiler. Its per-instance colour, four transform rows, lighting defaults, and world/view/projection parameter contract are retained. The desktop variant uses standard interpolation for SDL_GPU compatibility; original Xbox visual parity has not been verified.

Repairs match the original XNB SHA-256 before installing a desktop reconstruction. A filename alone is never sufficient. Existing custom HLSL is preserved. Originals are saved under the game's `shader-repair-backups` folder.

The generated `build/KnownShaders.json` and `BuildKnownShaders.ps1` drive an incremental `BuildKnownShaders` MSBuild target. This compiles `.fx` with the Windows SDK `fxc.exe` (`fx_2_0` effect container, shader functions use their declared profile), then writes an FNA-compatible Windows EffectReader XNB. Recompilation requires the matching HLSL; it does not decompile arbitrary Xbox bytecode.

## Adamant Ants Bloom

The three templates reconstruct the classic bloom contract found in the game's compiled payload metadata and `AdamantAntsXBOX/BloomComponent.cs`:

- BloomExtract: `BloomThreshold`, texture slot 0, technique `BloomExtract`.
- BloomCombine: `BloomIntensity`, `BaseIntensity`, `BloomSaturation`, `BaseSaturation`; bloom in slot 0, original scene in slot 1; technique `BloomCombine`.
- GaussianBlur: 15 `SampleOffsets` vectors and 15 `SampleWeights` floats, texture slot 0; technique `GaussianBlur`. The game supplies normalized weights and UV offsets, which are applied without additional scaling.

These three bloom templates are functional replacements. FNA GPU render/readback tests cover extraction, combination, saturation/intensity, flat-field brightness and impulse offsets. Custom effects are supported by the recovered templates described below.

The general bloom algorithm is described in [Microsoft's DirectXTK documentation](https://github.com/microsoft/DirectXTK/wiki/Writing-custom-shaders). The templates retain the legacy parameter layout and classic saturation coefficients. They do not copy the altered blur in newer NeonShooter samples.

## Recovered Custom Effects

All 25 Dead.Kings effects and the remaining 11 Adamant Ants effects now have fingerprinted templates recovered through HlslDecompiler, revision db004b9094e9d7ef1564ad1815e738d2273b2325. The shared CLI and attributed source are in Tools/XnaEffectDecompiler. Xbox's big-endian effect layout, original defaults, texture bindings, techniques and passes are retained; shader calculations come from microcode translation. Xbox effect state mappings were checked using Microsoft XNA compiler probes. Unknown state mappings fail explicitly.

All 39 desktop effects compile and load through FNA ContentManager, with matching parameter types, array lengths, techniques and passes. Distortion and WaterReflection7 passed GPU readback checks against the recovered formula. Full gameplay and original Xbox visual parity have not been verified.

Ten recovered Dead.Kings sources contain centroid qualifiers (eight triggered SPIR-V load failures). The FNA SDL_GPU variants use standard interpolation, with original source retained in the analysis archive. Multisampled edge colours can differ. The standalone decompiler supports --preserve-centroid for compatible backends.

There are 38 distinct extracted payloads: PureReflection and WaterReflection3 are identical but need separate asset names in the registry. Dead.Kings' ParticleEffect is distinct from the earlier supported ParticleEffect; each uses its own fingerprint and template. Unknown assets remain unchanged.
