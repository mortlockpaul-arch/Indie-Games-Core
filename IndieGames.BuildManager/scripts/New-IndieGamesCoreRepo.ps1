#Requires -Version 5.1
[CmdletBinding(SupportsShouldProcess=$true)]
param(
    [string]$Source = 'C:\source\Indie-Games',
    [string]$Destination = 'C:\source\Indie-Games-Core-Fresh',
    [string]$Archive = 'D:\indie-game-archive',
    [switch]$Execute,
    [switch]$InitializeGit
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
function FullPath([string]$p) { return [IO.Path]::GetFullPath($p).TrimEnd('\') }
$Source = FullPath $Source
$Destination = FullPath $Destination
$Archive = FullPath $Archive
if (!(Test-Path -LiteralPath $Source -PathType Container)) { throw "Source not found: $Source" }
if (!(Test-Path -LiteralPath $Archive -PathType Container)) { throw "Migrated archive not found: $Archive" }
if ($Destination -eq $Source -or $Destination.StartsWith($Source + '\',[StringComparison]::OrdinalIgnoreCase) -or $Source.StartsWith($Destination + '\',[StringComparison]::OrdinalIgnoreCase)) { throw 'Source and destination must be separate non-nested folders.' }
if ($Destination -eq $Archive -or $Destination.StartsWith($Archive + '\',[StringComparison]::OrdinalIgnoreCase)) { throw 'Destination must not be inside the archive.' }
$submodules = @('FNA','monogame','RetroShelf','FNA.NetStub','FNA.Steamworks')
$excludedDirs = @('.git','indie-game-archive','bin','obj','.vs','.idea','.venv','__pycache__','.pytest_cache','node_modules','packages')
$excludedFiles = @('*.user','*.suo','*.pyc','*.pdb','*.log','*.tmp','*.cache','*.bak','*.torrent','*.7z','*.zip','*.iso','*.zar','*.xnb')
Write-Host "Source: $Source" -ForegroundColor Cyan
Write-Host "Destination: $Destination" -ForegroundColor Cyan
Write-Host "Verified archive location: $Archive" -ForegroundColor Cyan
Write-Host "Excluded submodules: $($submodules -join ', ')" -ForegroundColor Cyan
Write-Warning 'Original source, archive and Git history will not be removed or modified.'
Write-Warning 'Game projects are excluded. No Git LFS will be installed or configured.'
if (!$Execute) {
    Write-Host 'PREVIEW ONLY. Run with -Execute to copy. Add -InitializeGit to initialize Git after a successful copy.' -ForegroundColor Yellow
    exit 0
}
if (!(Test-Path -LiteralPath $Destination)) { New-Item -ItemType Directory -Path $Destination -Force | Out-Null }
$log = Join-Path $Destination 'migration-copy.log'
# Exclude by directory name AND full path; use argument arrays to preserve paths containing spaces.
$xd = @($excludedDirs + $submodules) | Select-Object -Unique
$xf = @($excludedFiles + '.gitattributes' + '.gitmodules')
$argsList = @($Source,$Destination,'/E','/COPY:DAT','/DCOPY:DAT','/R:1','/W:1','/XJ','/NP','/NFL','/NDL','/NJH','/NJS','/XD') + $xd + @('/XF') + $xf + @('/LOG:' + $log)
Write-Host 'Copying core files (safe to resume; no mirror/delete flags)...' -ForegroundColor Green
& robocopy.exe @argsList
$rc = $LASTEXITCODE
Write-Host "Robocopy exit code: $rc"
if ($rc -ge 8) {
    Write-Host "Copy log: $log" -ForegroundColor Red
    if (Test-Path -LiteralPath $log) { Get-Content -LiteralPath $log -Tail 30 }
    throw "Robocopy failed (exit code $rc). Source remains untouched."
}
# A fresh ignore file deliberately keeps archives and build products out of Git.
$ignore = @'
# External game archive / large binary distributions
indie-game-archive/
*.iso
*.zar
*.xnb
*.torrent
*.7z
*.7z.*
*.zip
# Build and IDE output
**/bin/
**/obj/
.vs/
.idea/
.venv/
**/__pycache__/
*.user
*.suo
*.pdb
*.log
*.tmp
# Submodules to re-add using verified URLs
/FNA/
/monogame/
/RetroShelf/
/FNA.NetStub/
/FNA.Steamworks/
'@
$ignorePath = Join-Path $Destination '.gitignore'
if (!(Test-Path -LiteralPath $ignorePath)) { Set-Content -LiteralPath $ignorePath -Value $ignore -Encoding UTF8 }
Write-Host "Copy finished. Log: $log" -ForegroundColor Green
if (!$InitializeGit) {
    Write-Host 'Git not initialized. Inspect destination; rerun with -Execute -InitializeGit when ready.' -ForegroundColor Yellow
    exit 0
}
$gitDir = Join-Path $Destination '.git'
if (Test-Path -LiteralPath $gitDir) { throw "Destination already contains .git: $gitDir. Refusing to alter it." }
if (!(Get-Command git.exe -ErrorAction SilentlyContinue)) { throw 'git.exe not found on PATH.' }
# Scan likely text files for LFS pointers before initializing.
$pointerFiles = New-Object 'System.Collections.Generic.List[string]'
Get-ChildItem -LiteralPath $Destination -File -Recurse -ErrorAction Stop | Where-Object { $_.Length -lt 1024 -and $_.Extension -in @('.cs','.csproj','.props','.targets','.sln','.slnx','.json','.xml','.txt','.md','.fx','.fxb') } | ForEach-Object {
    $first = Get-Content -LiteralPath $_.FullName -TotalCount 1 -ErrorAction SilentlyContinue
    if ($first -eq 'version https://git-lfs.github.com/spec/v1') { $pointerFiles.Add($_.FullName) }
}
if ($pointerFiles.Count -gt 0) {
    $pointerFiles | ForEach-Object { Write-Warning "LFS pointer: $_" }
    throw "Found $($pointerFiles.Count) LFS pointer files. Git initialization aborted."
}
Push-Location $Destination
try {
    & git.exe init -b main
    if ($LASTEXITCODE -ne 0) { throw 'git init failed.' }
    Write-Host 'New independent Git repository initialized. No commit or remote created.' -ForegroundColor Green
    & git.exe status --short
} finally { Pop-Location }
