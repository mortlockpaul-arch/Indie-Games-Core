#Requires -Version 5.1
[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
param(
    [string]$RepoRoot = 'C:\source\Indie-Games',
    [switch]$Execute,
    [string]$ReportPath,
    [switch]$IncludeMetadata
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Format-Size([long]$Bytes) {
    if ($Bytes -ge 1GB) { return ('{0:N2} GB' -f ($Bytes / 1GB)) }
    if ($Bytes -ge 1MB) { return ('{0:N2} MB' -f ($Bytes / 1MB)) }
    if ($Bytes -ge 1KB) { return ('{0:N2} KB' -f ($Bytes / 1KB)) }
    return "$Bytes B"
}

function Test-StfsPackage([string]$Path) {
    $stream = $null
    try {
        $stream = [System.IO.File]::Open($Path, [System.IO.FileMode]::Open, [System.IO.FileAccess]::Read, [System.IO.FileShare]::ReadWrite)
        if ($stream.Length -lt 4) { return $false }
        $bytes = New-Object byte[] 4
        if ($stream.Read($bytes, 0, 4) -ne 4) { return $false }
        $magic = [System.Text.Encoding]::ASCII.GetString($bytes)
        return ($magic -eq 'CON ' -or $magic -eq 'LIVE' -or $magic -eq 'PIRS')
    } catch {
        Write-Warning "Could not inspect package header: $Path : $_"
        return $false
    } finally {
        if ($null -ne $stream) { $stream.Dispose() }
    }
}

function Get-TreeSize([string]$Path) {
    [long]$total = 0
    foreach ($file in [System.IO.Directory]::EnumerateFiles($Path, '*', [System.IO.SearchOption]::AllDirectories)) {
        $total += (New-Object System.IO.FileInfo($file)).Length
    }
    return $total
}

$archiveRoot = Join-Path $RepoRoot 'indie-game-archive'
if (-not (Test-Path -LiteralPath $archiveRoot -PathType Container)) { throw "Archive root not found: $archiveRoot" }
if (-not $ReportPath) {
    $ReportPath = Join-Path ([System.IO.Path]::GetTempPath()) ('Xblig584E07D2-Cleanup-{0}.csv' -f (Get-Date -Format 'yyyyMMdd-HHmmss'))
}
$reportParent = Split-Path -Parent $ReportPath
if ($reportParent -and -not (Test-Path -LiteralPath $reportParent)) { New-Item -ItemType Directory -Path $reportParent -Force | Out-Null }

$rows = New-Object 'System.Collections.Generic.List[object]'
[long]$potentialBytes = 0
[long]$deletedBytes = 0
[int]$protectedPackages = 0
[int]$foldersScanned = 0
[int]$errors = 0

Write-Host ('Mode: ' + $(if ($Execute) { 'EXECUTE (deletions enabled)' } else { 'DRY RUN (no deletions)' })) -ForegroundColor Cyan
Write-Host "Scanning: $archiveRoot"
Write-Host "Metadata files: $(if ($IncludeMetadata) { 'eligible for deletion' } else { 'preserved' })"

# Only scan real directories under game roots, never follow directory reparse points.
$games = @(Get-ChildItem -LiteralPath $archiveRoot -Directory -Force -ErrorAction Stop | Where-Object { -not ($_.Attributes -band [System.IO.FileAttributes]::ReparsePoint) })
foreach ($game in $games) {
    $contentRoot = Join-Path $game.FullName '584E07D2'
    if (-not (Test-Path -LiteralPath $contentRoot -PathType Container)) { continue }
    if ((Get-Item -LiteralPath $contentRoot -Force).Attributes -band [System.IO.FileAttributes]::ReparsePoint) {
        Write-Warning "Skipping reparse point: $contentRoot"
        continue
    }
    # Standard Xbox 360 content folder, and any additional subfolders under 584E07D2.
    $candidateRoots = @(Get-ChildItem -LiteralPath $contentRoot -Directory -Force | Where-Object { -not ($_.Attributes -band [System.IO.FileAttributes]::ReparsePoint) })
    # Include loose files directly inside 584E07D2 as well.
    $candidateRoots = @($contentRoot) + @($candidateRoots | ForEach-Object { $_.FullName })
    foreach ($folder in $candidateRoots) {
        $folderPath = if ($folder -is [string]) { $folder } else { $folder.FullName }
        $foldersScanned++
        $items = @(Get-ChildItem -LiteralPath $folderPath -Force -ErrorAction Continue)
        foreach ($item in $items) {
            # Child folders are evaluated as a unit. Never delete a folder containing an STFS package.
            if ($item.Attributes -band [System.IO.FileAttributes]::ReparsePoint) { continue }
            if ($item.PSIsContainer) {
                # At the top-level, recurse by visiting child folders separately.
                if ($folderPath -eq $contentRoot) { continue }
                $packageFound = $false
                try {
                    $links = @(Get-ChildItem -LiteralPath $item.FullName -Recurse -Force -Attributes ReparsePoint -ErrorAction Stop)
                    if ($links.Count -gt 0) { Write-Warning "Skipping folder containing links/junctions: $($item.FullName)"; continue }
                    foreach ($nestedFile in [System.IO.Directory]::EnumerateFiles($item.FullName, '*', [System.IO.SearchOption]::AllDirectories)) {
                        if (Test-StfsPackage $nestedFile) { $packageFound = $true; break }
                    }
                    if ($packageFound) {
                        Write-Warning "Preserving folder containing an STFS package: $($item.FullName)"
                        continue
                    }
                    [long]$size = Get-TreeSize $item.FullName
                } catch {
                    $errors++
                    Write-Warning "Skipping folder with unreadable content: $($item.FullName) : $_"
                    continue
                }
                $kind = 'ExtractedFolder'
            } else {
                if (Test-StfsPackage $item.FullName) { $protectedPackages++; continue }
                if (-not $IncludeMetadata -and $item.Name -in @('GameInfo.bin','GameInfo.xml','DashboardIcon.png','TitleImage.png')) { continue }
                [long]$size = $item.Length
                $kind = 'NonPackageFile'
            }
            $potentialBytes += $size
            $status = 'WouldDelete'
            if ($Execute) {
                try {
                    if ($PSCmdlet.ShouldProcess($item.FullName, 'Remove extracted XBLIG content')) {
                        Remove-Item -LiteralPath $item.FullName -Recurse -Force -ErrorAction Stop -Confirm:$false
                        $deletedBytes += $size
                        $status = 'Deleted'
                    } else { $status = 'Skipped' }
                } catch {
                    $errors++
                    $status = 'Error'
                    Write-Warning "Could not delete $($item.FullName): $_"
                }
            }
            $rows.Add([pscustomobject]@{
                Game = $game.Name
                Path = $item.FullName
                Kind = $kind
                SizeBytes = $size
                Size = Format-Size $size
                Status = $status
            })
        }
    }
}

$rows | Export-Csv -LiteralPath $ReportPath -NoTypeInformation -Encoding UTF8
$grouped = @($rows | Group-Object Game | ForEach-Object {
    [pscustomobject]@{ Game = $_.Name; FilesAndFolders = $_.Count; PotentialBytes = [long](($_.Group | Measure-Object SizeBytes -Sum).Sum) }
} | Sort-Object PotentialBytes -Descending)
Write-Host ''
Write-Host 'Potential savings per game:' -ForegroundColor Cyan
$grouped | Select-Object Game, FilesAndFolders, @{Name='PotentialSavings';Expression={Format-Size $_.PotentialBytes}} | Format-Table -AutoSize | Out-Host
Write-Host "Games scanned: $($games.Count)"
Write-Host "Content directories scanned: $foldersScanned"
Write-Host "Protected STFS packages: $protectedPackages"
Write-Host "Potential space savings: $(Format-Size $potentialBytes)"
Write-Host "Actually deleted: $(Format-Size $deletedBytes)"
Write-Host "Errors: $errors"
Write-Host "CSV report: $ReportPath"
if (-not $Execute) { Write-Host 'No files were deleted. Add -Execute to enable deletion with confirmation.' -ForegroundColor Yellow }
