[CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='High')]
param(
    [string]$RepoRoot = 'C:\source\Indie-Games',
    [switch]$Execute,
    [string]$ReportPath
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$archive = Join-Path $RepoRoot 'indie-game-archive'
if (-not (Test-Path -LiteralPath $archive -PathType Container)) { throw "Archive not found: $archive" }
if (-not $ReportPath) { $ReportPath = Join-Path ([IO.Path]::GetTempPath()) ('XBLIG-Cleanup-' + (Get-Date -Format 'yyyyMMdd-HHmmss') + '.csv') }
function Test-StfsPackage([IO.FileInfo]$File) {
    if ($File.Length -lt 4) { return $false }
    $stream = $null
    try {
        $stream = [IO.File]::Open($File.FullName, [IO.FileMode]::Open, [IO.FileAccess]::Read, [IO.FileShare]::ReadWrite)
        $header = New-Object byte[] 4
        if ($stream.Read($header,0,4) -ne 4) { return $false }
        $magic = [Text.Encoding]::ASCII.GetString($header)
        return ($magic -eq 'CON ' -or $magic -eq 'LIVE' -or $magic -eq 'PIRS')
    } catch { return $false }
    finally { if ($null -ne $stream) { $stream.Dispose() } }
}
$targets = @(Get-ChildItem -LiteralPath $archive -Directory -Recurse -Force -ErrorAction Stop | Where-Object { $_.Name -eq '00000002' -and $_.Parent.Name -eq '584E07D2' -and -not ($_.Attributes -band [IO.FileAttributes]::ReparsePoint) })
$rows = New-Object 'System.Collections.Generic.List[object]'
[long]$potential = 0
[long]$deleted = 0
foreach ($folder in $targets) {
    $files = @(Get-ChildItem -LiteralPath $folder.FullName -File -Recurse -Force | Where-Object { -not ($_.Attributes -band [IO.FileAttributes]::ReparsePoint) })
    $packages = @($files | Where-Object { Test-StfsPackage $_ })
    $game = $folder.Parent.Parent.Name
    if ($packages.Count -eq 0) {
        Write-Warning "SKIPPED (no verified STFS package): $($folder.FullName)"
        $rows.Add([pscustomobject]@{Game=$game;Path=$folder.FullName;Bytes=0;Action='SKIPPED - no verified STFS package'})
        continue
    }
    $packagePaths = @{}; foreach ($p in $packages) { $packagePaths[$p.FullName] = $true }
    $candidates = @($files | Where-Object { -not $packagePaths.ContainsKey($_.FullName) })
    [long]$folderBytes = 0
    $unknownPackages = @($candidates | Where-Object { $_.Name -match '^[0-9A-Fa-f]{40}$' })
    if ($unknownPackages.Count -gt 0) {
        Write-Warning "SKIPPED (unverified hex-named possible package): $($folder.FullName)"
        $rows.Add([pscustomobject]@{Game=$game;Path=$folder.FullName;Bytes=0;Action='SKIPPED - unverified possible package'})
        continue
    }
    foreach ($file in $candidates) {
        $folderBytes += [long]$file.Length
        $potential += $file.Length
        $action = 'DRY RUN'
        if ($Execute) {
            if ($PSCmdlet.ShouldProcess($file.FullName, 'Delete non-package file')) {
                try { Remove-Item -LiteralPath $file.FullName -Force -ErrorAction Stop; $deleted += $file.Length; $action = 'DELETED' }
                catch { $action = 'ERROR: ' + $_.Exception.Message; Write-Warning "$($file.FullName): $($_.Exception.Message)" }
            } else { $action = 'NOT CONFIRMED' }
        }
        $rows.Add([pscustomobject]@{Game=$game;Path=$file.FullName;Bytes=$file.Length;Action=$action})
    }
    Write-Host ("{0}: {1} verified package(s), {2} candidate(s), {3:N2} MB" -f $game,$packages.Count,$candidates.Count,($folderBytes / 1MB))
}
$reportDir = Split-Path -Parent $ReportPath
if ($reportDir -and -not (Test-Path -LiteralPath $reportDir)) { New-Item -ItemType Directory -Path $reportDir -Force | Out-Null }
$rows | Export-Csv -LiteralPath $ReportPath -NoTypeInformation -Encoding UTF8
Write-Host ("Folders scanned: {0}; Potential savings: {1:N2} MB; Deleted: {2:N2} MB" -f $targets.Count,($potential/1MB),($deleted/1MB))
Write-Host "CSV report: $ReportPath"
if (-not $Execute) { Write-Host 'Dry-run only. Use -Execute to delete, then confirm the PowerShell prompt.' }
