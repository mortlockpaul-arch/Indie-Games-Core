#Requires -Version 5.1
[CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='High')]
param(
    [string]$RepoRoot = 'C:\source\Indie-Games',
    [string]$DestinationRoot = 'D:\downloads\indie-game-downloads',
    [switch]$DryRun,
    [switch]$Execute,
    [string]$ReportPath
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
if ($Execute -and $DryRun) { throw 'Choose either -Execute or -DryRun, not both.' }
$sourceRoot = Join-Path $RepoRoot 'indie-game-archive'
if (-not (Test-Path -LiteralPath $sourceRoot -PathType Container)) { throw "Archive not found: $sourceRoot" }
$sourceRoot = (Resolve-Path -LiteralPath $sourceRoot).ProviderPath
$destinationFull = [IO.Path]::GetFullPath($DestinationRoot).TrimEnd('\')
$sourceFull = [IO.Path]::GetFullPath($sourceRoot).TrimEnd('\')
if ($destinationFull.StartsWith($sourceFull + '\', [StringComparison]::OrdinalIgnoreCase) -or $sourceFull.StartsWith($destinationFull + '\', [StringComparison]::OrdinalIgnoreCase) -or $sourceFull -eq $destinationFull) {
    throw 'Source and destination directories must not contain each other.'
}
if (-not $ReportPath) { $ReportPath = Join-Path ([IO.Path]::GetTempPath()) ('IndieGame-Move-' + (Get-Date -Format 'yyyyMMdd-HHmmss') + '.csv') }
function Get-Inventory([string]$Folder) {
    [long]$bytes = 0
    [long]$count = 0
    $files = @(Get-ChildItem -LiteralPath $Folder -Recurse -File -Force -ErrorAction Stop)
    foreach ($file in $files) {
        if (($file.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) { throw "Reparse-point file found: $($file.FullName)" }
        $bytes += [long]$file.Length
        $count++
    }
    return [pscustomobject]@{ Bytes = $bytes; Files = $count }
}
function Test-Reparse([string]$Folder) {
    $root = Get-Item -LiteralPath $Folder -Force
    if (($root.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) { return $true }
    foreach ($entry in (Get-ChildItem -LiteralPath $Folder -Recurse -Force -ErrorAction Stop)) {
        if (($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) { return $true }
    }
    return $false
}
$rows = New-Object 'System.Collections.Generic.List[object]'
[long]$planned = 0
[long]$moved = 0
$games = @(Get-ChildItem -LiteralPath $sourceRoot -Directory -Force | Sort-Object Name)
foreach ($game in $games) {
    foreach ($folderName in @('584E07D1','584E07D2')) {
        $src = Join-Path $game.FullName $folderName
        if (-not (Test-Path -LiteralPath $src -PathType Container)) { continue }
        $dst = Join-Path (Join-Path $destinationFull $game.Name) $folderName
        $status = 'Planned'
        $errorMessage = ''
        [long]$bytes = 0
        [long]$fileCount = 0
        try {
            if (Test-Reparse $src) { throw 'Source contains a junction, symbolic link or reparse point.' }
            $info = Get-Inventory $src
            $bytes = $info.Bytes
            $fileCount = $info.Files
            if (Test-Path -LiteralPath $dst) { throw 'Destination already exists; refusing to merge or overwrite.' }
            if ($Execute) {
                if ($PSCmdlet.ShouldProcess($src, "Copy, verify and remove source after success: $dst")) {
                    $parent = Split-Path -Parent $dst
                    if (-not (Test-Path -LiteralPath $parent)) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
                    # Copy first; compare file sizes and SHA256 before deleting original.
                    Copy-Item -LiteralPath $src -Destination $dst -Recurse -Force -ErrorAction Stop
                    $copyInfo = Get-Inventory $dst
                    if ($copyInfo.Bytes -ne $bytes -or $copyInfo.Files -ne $fileCount) { throw 'Copy verification failed: byte or file count differs. Original preserved.' }
                    foreach ($original in (Get-ChildItem -LiteralPath $src -Recurse -File -Force)) {
                        $relative = $original.FullName.Substring($src.Length).TrimStart('\')
                        $copyPath = Join-Path $dst $relative
                        if (-not (Test-Path -LiteralPath $copyPath -PathType Leaf)) { throw "Missing copied file: $relative" }
                        $a = (Get-FileHash -LiteralPath $original.FullName -Algorithm SHA256).Hash
                        $b = (Get-FileHash -LiteralPath $copyPath -Algorithm SHA256).Hash
                        if ($a -ne $b) { throw "SHA256 mismatch: $relative" }
                    }
                    Remove-Item -LiteralPath $src -Recurse -Force -ErrorAction Stop
                    $status = 'MovedAndVerified'
                    $moved += $bytes
                } else { $status = 'Skipped' }
            } else { $planned += $bytes }
        } catch {
            $status = 'SkippedOrFailed'
            $errorMessage = $_.Exception.Message
            Write-Warning "$($game.Name) / $folderName : $errorMessage"
        }
        $rows.Add([pscustomobject]@{
            Game = $game.Name; Folder = $folderName; Source = $src; Destination = $dst
            Files = $fileCount; Bytes = $bytes; SizeMB = [math]::Round($bytes / 1MB, 2)
            Status = $status; Error = $errorMessage
        })
        Write-Host ('{0} | {1} | {2:N2} MB | {3}' -f $game.Name, $folderName, ($bytes / 1MB), $status)
    }
}
$reportDir = Split-Path -Parent ([IO.Path]::GetFullPath($ReportPath))
if (-not (Test-Path -LiteralPath $reportDir)) { New-Item -ItemType Directory -Path $reportDir -Force | Out-Null }
$rows | Export-Csv -LiteralPath $ReportPath -NoTypeInformation -Encoding UTF8
Write-Host ''
Write-Host ('Folders found: {0}' -f $rows.Count)
Write-Host ('Potential space freed from source: {0:N2} GB' -f ($planned / 1GB))
Write-Host ('Actually moved and verified: {0:N2} GB' -f ($moved / 1GB))
Write-Host "CSV report: $ReportPath"
if (-not $Execute) { Write-Host 'DRY RUN ONLY. Add -Execute to move files. Existing destinations are skipped.' -ForegroundColor Yellow }
