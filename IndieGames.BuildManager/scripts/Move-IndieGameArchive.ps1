#requires -Version 5.1
<#
.SYNOPSIS
  Safely copies and verifies the Indie-Games game archive outside Git repositories.
.DESCRIPTION
  Dry-run by default. With -Execute, copies to a staging folder, SHA-256 verifies
  each file, then renames staging to the destination. NEVER deletes the source.
  Never edits Git metadata, solution files, or application settings.
.EXAMPLE
  .\Move-IndieGameArchive.ps1 -Destination 'D:\indie-game-archive'
.EXAMPLE
  .\Move-IndieGameArchive.ps1 -Destination 'D:\indie-game-archive' -Execute
#>
[CmdletBinding()]
param(
    [string]$Source = 'C:\source\Indie-Games\indie-game-archive',
    [string]$Destination = 'D:\indie-game-archive',
    [switch]$Execute,
    [switch]$SkipFreeSpaceCheck
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-FullPath([string]$Path) { [IO.Path]::GetFullPath($Path).TrimEnd('\','/') }
function Assert-NoReparsePoints([string]$Root) {
    $rootItem = Get-Item -LiteralPath $Root -Force
    if (($rootItem.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) {
        throw "Reparse point not supported: $Root"
    }
    Get-ChildItem -LiteralPath $Root -Force -Recurse -ErrorAction Stop | ForEach-Object {
        if (($_.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) {
            throw "Reparse point not supported: $($_.FullName)"
        }
    }
}
function Get-RelativePath([string]$Root, [string]$FullName) {
    return $FullName.Substring($Root.Length).TrimStart('\','/')
}

$Source = Get-FullPath $Source
$Destination = Get-FullPath $Destination
$staging = "$Destination.migration-staging"
if (-not (Test-Path -LiteralPath $Source -PathType Container)) { throw "Source missing: $Source" }
if ($Source.Equals($Destination, [StringComparison]::OrdinalIgnoreCase)) { throw 'Source and destination are identical.' }
if ($Destination.StartsWith($Source + '\', [StringComparison]::OrdinalIgnoreCase) -or
    $Source.StartsWith($Destination + '\', [StringComparison]::OrdinalIgnoreCase)) {
    throw 'Source and destination cannot contain one another.'
}
if ($staging.StartsWith($Source + '\', [StringComparison]::OrdinalIgnoreCase)) { throw 'Staging path cannot be inside source.' }
if (Test-Path -LiteralPath $Destination) { throw "Destination already exists: $Destination. Choose an empty new destination; no overwrites are allowed." }
if (Test-Path -LiteralPath $staging) { throw "Staging directory already exists: $staging. Inspect it before retrying; this script will not overwrite it." }
Assert-NoReparsePoints $Source

$files = @(Get-ChildItem -LiteralPath $Source -File -Force -Recurse -ErrorAction Stop)
$directories = @(Get-ChildItem -LiteralPath $Source -Directory -Force -Recurse -ErrorAction Stop)
$bytes = [long]0
foreach ($file in $files) { $bytes += $file.Length }
Write-Host "Source:       $Source"
Write-Host "Destination:  $Destination"
Write-Host "Files:        $($files.Count)"
Write-Host "Directories:  $($directories.Count)"
Write-Host ("Data size:    {0:N2} GiB" -f ($bytes / 1GB))
Write-Host 'Source is NEVER deleted by this script.' -ForegroundColor Yellow

if (-not $SkipFreeSpaceCheck) {
    $destRoot = [IO.Path]::GetPathRoot($Destination)
    if (-not (Test-Path -LiteralPath $destRoot)) { throw "Destination volume unavailable: $destRoot" }
    $drive = New-Object IO.DriveInfo($destRoot)
    $reserve = [long][Math]::Max(1GB, [Math]::Ceiling($bytes * 0.05))
    if ($drive.AvailableFreeSpace -lt ($bytes + $reserve)) {
        throw ("Insufficient space: {0:N2} GiB free, need at least {1:N2} GiB (including reserve)." -f ($drive.AvailableFreeSpace/1GB), (($bytes+$reserve)/1GB))
    }
}
if (-not $Execute) {
    Write-Host 'DRY RUN ONLY. Add -Execute to copy and verify.' -ForegroundColor Cyan
    return
}

$parent = Split-Path -Parent $Destination
if (-not (Test-Path -LiteralPath $parent)) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
New-Item -ItemType Directory -Path $staging -ErrorAction Stop | Out-Null
$copied = 0
try {
    foreach ($dir in $directories) {
        $rel = Get-RelativePath $Source $dir.FullName
        New-Item -ItemType Directory -Path (Join-Path $staging $rel) -Force | Out-Null
    }
    foreach ($file in $files) {
        $rel = Get-RelativePath $Source $file.FullName
        $target = Join-Path $staging $rel
        $targetParent = Split-Path -Parent $target
        if (-not (Test-Path -LiteralPath $targetParent)) { New-Item -ItemType Directory -Path $targetParent -Force | Out-Null }
        [IO.File]::Copy($file.FullName, $target, $false)
        [IO.File]::SetLastWriteTimeUtc($target, $file.LastWriteTimeUtc)
        $copied++
        if (($copied % 250) -eq 0) { Write-Progress -Activity 'Copying game archive' -Status "$copied / $($files.Count) files" -PercentComplete (100 * $copied / [Math]::Max(1, $files.Count)) }
    }
    Write-Progress -Activity 'Copying game archive' -Completed
    Write-Host 'Copy complete. Verifying SHA-256 of every file...' -ForegroundColor Cyan
    $verified = 0
    foreach ($file in $files) {
        $rel = Get-RelativePath $Source $file.FullName
        $target = Join-Path $staging $rel
        if (-not (Test-Path -LiteralPath $target -PathType Leaf)) { throw "Missing copied file: $rel" }
        if ((Get-Item -LiteralPath $target).Length -ne $file.Length) { throw "Size mismatch: $rel" }
        $originalHash = (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash
        $copiedHash = (Get-FileHash -LiteralPath $target -Algorithm SHA256).Hash
        if ($originalHash -ne $copiedHash) { throw "SHA-256 mismatch: $rel" }
        $verified++
        if (($verified % 100) -eq 0) { Write-Progress -Activity 'Verifying game archive' -Status "$verified / $($files.Count) files" -PercentComplete (100 * $verified / [Math]::Max(1, $files.Count)) }
    }
    Write-Progress -Activity 'Verifying game archive' -Completed
    if (@(Get-ChildItem -LiteralPath $staging -File -Force -Recurse).Count -ne $files.Count) { throw 'Copied file count mismatch.' }
    # Rename only after complete verification; no existing destination is overwritten.
    [IO.Directory]::Move($staging, $Destination)
    Write-Host "SUCCESS: $verified files SHA-256 verified at $Destination" -ForegroundColor Green
    Write-Host "Original remains untouched: $Source" -ForegroundColor Green
    Write-Host 'Next: update Build Manager / Xenia Manager archive paths manually, then test games.'
    Write-Host 'Do not remove the original until backups and application paths are independently verified.' -ForegroundColor Yellow
}
catch {
    Write-Error "Migration did not complete: $($_.Exception.Message). Partial data may remain at $staging. Original source was not deleted."
    throw
}
