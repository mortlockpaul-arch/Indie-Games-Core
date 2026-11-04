[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [string]$ArchiveRoot = 'D:\indie-game-archive',
    [string]$FixerScript = (Join-Path $PSScriptRoot 'Repair-XnaAssemblyReferences.ps1'),
    [string]$FnaProject = 'C:\source\Indie-Games-Core\FNA\FNA.csproj',
    [string]$ReportPath = (Join-Path $PSScriptRoot 'XnaReferenceScan.csv'),
    [switch]$NoBackup,
    [switch]$SkipDllAudit
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
if (-not (Test-Path -LiteralPath $ArchiveRoot -PathType Container)) { throw "Archive root missing: $ArchiveRoot" }
if (-not (Test-Path -LiteralPath $FixerScript -PathType Leaf)) { throw "Fixer script missing: $FixerScript" }
if (-not (Test-Path -LiteralPath $FnaProject -PathType Leaf)) { throw "FNA project missing: $FnaProject" }
. $FixerScript
$folders = @(Get-ChildItem -LiteralPath $ArchiveRoot -Directory | Where-Object { $_.Name -notmatch '^(?:bin|obj|\.git|backup|backups)$' } | Sort-Object Name)
$rows = [System.Collections.Generic.List[object]]::new()
$i = 0
foreach ($folder in $folders) {
    $i++
    Write-Progress -Activity 'Scanning XBLIG game folders' -Status $folder.Name -PercentComplete ([int](100*$i/[Math]::Max(1,$folders.Count)))
    $projects = @(Get-ChildItem -LiteralPath $folder.FullName -Recurse -File -Filter '*.csproj' -ErrorAction SilentlyContinue | Where-Object { $_.FullName -notmatch '[\\/](bin|obj|backup|backups)[\\/]' })
    if ($projects.Count -eq 0) {
        $rows.Add([pscustomobject]@{Game=$folder.Name;Projects=0;Status='NoProjects';Updated=0;Details=''})
        continue
    }
    try {
        # Collect host output through Information stream (Write-Host); warnings are preserved separately.
        $messages = @(Repair-XnaAssemblyReferences -Root $folder.FullName -FnaProject $FnaProject -NoBackup:$NoBackup -SkipDllAudit:$SkipDllAudit -WhatIf:$WhatIfPreference 6>&1 3>&1)
        $lines = @($messages | ForEach-Object { [string]$_ })
        $updated = @($lines | Where-Object { $_ -match '^Updated:' }).Count
        $warnings = @($lines | Where-Object { $_ -match '^(Failed:|Legacy XNA DLL dependency:)' })
        $status = if ($warnings.Count -gt 0) { 'ReviewWarnings' } elseif ($WhatIfPreference) { 'Preview' } elseif ($updated -gt 0) { 'Updated' } else { 'Unchanged' }
        $rows.Add([pscustomobject]@{Game=$folder.Name;Projects=$projects.Count;Status=$status;Updated=$updated;Details=($warnings -join ' | ')})
        Write-Host "[$i/$($folders.Count)] $($folder.Name): $status ($updated updated)"
    }
    catch {
        $rows.Add([pscustomobject]@{Game=$folder.Name;Projects=$projects.Count;Status='Error';Updated=0;Details=$_.Exception.Message})
        Write-Warning "Failed game $($folder.Name): $($_.Exception.Message)"
    }
}
Write-Progress -Activity 'Scanning XBLIG game folders' -Completed
$reportDirectory = Split-Path -Parent $ReportPath
if ($reportDirectory -and -not (Test-Path -LiteralPath $reportDirectory)) { New-Item -ItemType Directory -Path $reportDirectory -Force | Out-Null }
$rows | Export-Csv -LiteralPath $ReportPath -NoTypeInformation -Encoding UTF8
Write-Host "Games scanned: $($folders.Count); projects found: $(($projectsCount = ($rows | Measure-Object -Property Projects -Sum).Sum)); updated: $(($rows | Measure-Object -Property Updated -Sum).Sum)"
Write-Host "CSV report: $ReportPath"
