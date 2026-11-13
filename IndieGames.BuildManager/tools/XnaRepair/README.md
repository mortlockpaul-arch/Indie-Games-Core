# Centralized XNA repair scripts

Canonical location: `C:\source\Indie-Games-Core\tools\XnaRepair`.

These scripts are **not installed automatically**. Copy the directory there, then configure Build Manager to resolve this canonical location for development. Published builds still need a single bundled `scripts` folder.

The original `repair_xna3.py` CLI remains unchanged for Build Manager compatibility (`--convert-xna3`, `--cron-bloom`, `--preview`). `run_repair.py` adds independently selectable Python repairs without modifying the original script.

```powershell
py -3 tools\XnaRepair\run_repair.py --list
py -3 tools\XnaRepair\run_repair.py "D:\indie-game-archive\ABC Fun (World) (XBLIG)" --fix fix_xna_sprite_blend_mode
py -3 tools\XnaRepair\run_repair.py "D:\indie-game-archive\ABC Fun (World) (XBLIG)" --fix fix_xna_sprite_blend_mode --apply
```

Preview is the default. Apply backs up changed source files into the game `.backups` directory before replacing them. The Python source still contains some broad regex transformations; review diffs and build after applying. This package does not automatically change Build Manager C# routing or remove the published copies. The PowerShell script is retained as a separate backend; overlapping Python and PowerShell repairs should not both run automatically.

`repair_ambiguities.py` is a compiler-diagnostic repair and remains a separate entry point because it builds the selected project and uses compiler locations.

**Why `.py`?** They are Python-based source transformers launched by Build Manager through `py.exe`; `.ps1` files are PowerShell-based repair functions. Both are legitimate script types. `bin`, `obj`, `artifacts`, and `publish` copies are build outputs; do not edit those copies.
