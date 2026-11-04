# Shared FNA SunBurn source

Decompiled with ILSpyCmd 11.2.0.10052 from the Zombie game's original SunBurn 2.0.16.5 DLL. This project now targets .NET 9 Windows and references FNA/FNA.NetStub. The archive uses Shared/SunBurn.targets for conditional project selection.

The shared x64 library builds; the Zombie game still has other legacy dependency errors and is not runtime-validated. Embedded Xbox 360 effect resources are preserved and may need conversion. Original signing keys are unavailable, so unsigned dependent copies need assembly-identity redirection. Original game DLLs are unchanged.

The full instructions are in XBLIG REBUILDER's Help, Build Manager's Shared SunBurn help tab, and src/docs/build-and-repair-guide.md in xenia-game-manager. UseSharedSunBurn=false disables automatic source substitution.
