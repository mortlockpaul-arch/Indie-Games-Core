# XNB Reader Audit v3 (read-only)

This bundle extends XnaContentReaderRepair with a `--audit` mode. It scans game directories, decompresses Xbox 360 LZX XNBs using the supplied XnbExtractor LZX decoder, inspects the XNB reader table, and indexes managed DLL type definitions using `System.Reflection.Metadata` (no Mono.Cecil NuGet dependency). It does not modify XNBs or DLLs.

## Install
Extract into `C:\source\Indie-Games-Core\tools\XnaContentReaderRepair`, replacing the earlier tool files after backing them up. The original repair command is retained.

## Run
```powershell
$tool = 'C:\source\Indie-Games-Core\tools\XnaContentReaderRepair'
dotnet build "$tool\XnaContentReaderRepair.csproj" -c Release
& "$tool\Audit-XnaContentReaders.ps1" -ArchiveRoot 'D:\indie-game-archive' -Report 'D:\indie-game-archive\XnaContentReaderAudit-v3.csv'
```

Statuses:
- `VALID_EXTERNAL_READER`: reader type exists in a managed DLL with matching assembly name and version (if XNB specifies version). This does not guarantee runtime compatibility.
- `ASSEMBLY_MISMATCH`: reader type exists, but in a differently named DLL. Candidate only; no automatic rewrite.
- `ASSEMBLY_VERSION_MISMATCH`: type and assembly name found but XNB's embedded version differs.
- `MISSING_READER`: no matching type found in scanned managed DLLs. Decompiled source files are not analyzed.
- `UNSUPPORTED_COMPRESSION`: LZ4 XNBs, which this decoder does not support.
- `PARSE_ERROR`: malformed, corrupt, or unhandled XNB; may include LZX decompression failures.

Audit indexes DLLs under game folders including `bin` outputs, excluding `obj`. Assembly metadata is inspected without executing DLL code. Reader names in the `Microsoft.*` and `System.*` namespaces are ignored as framework readers.

**Limitations:** 48k compressed assets may take substantial time. LZX decompression currently buffers each asset; a 512 MiB per-asset decompressed-size guard is applied. Nested/generic assembly-qualified reader names and forwarded types are not fully resolved. If a game uses reader classes that are only present in source code and not built DLLs, it will be classified `MISSING_READER`. Some game folders contain duplicate original/deployed DLLs: the report chooses a representative provider path, not the actual runtime resolution path. The CSV may be large. Validate all `ASSEMBLY_MISMATCH` rows before patching.

LZX decoder source comes from the user's XnbExtractor project, with its existing licensing notices.
