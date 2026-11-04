#Requires -Version 5.1
[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
param(
    [string]$ArchiveRoot = 'D:\indie-game-archive',
    [string]$CoreRoot = 'C:\source\Indie-Games-Core',
    [string]$ReportPath,
    [switch]$IncludeValidReferences,
    [switch]$UseCoreProperty
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $ArchiveRoot -PathType Container)) { throw "Archive root missing: $ArchiveRoot" }
if (-not (Test-Path -LiteralPath $CoreRoot -PathType Container)) { throw "Core root missing: $CoreRoot" }
$ArchiveRoot = (Resolve-Path -LiteralPath $ArchiveRoot).Path
$CoreRoot = (Resolve-Path -LiteralPath $CoreRoot).Path
if (-not $ReportPath) { $ReportPath = Join-Path $ArchiveRoot 'ProjectReference-Repair-Report.csv' }

# Known shared projects. Only these names are eligible for automatic repair.
$known = @{
    'FNA.csproj'                = @('FNA\FNA.csproj')
    'FNA.NetStub.csproj'        = @('FNA.NetStub\FNA.NetStub.csproj')
    'FNA.Contents.csproj'       = @('FNA.Contents\FNA.Contents.csproj')
    'FNA.AvatarCompat.csproj'   = @('FNA.AvatarCompat\FNA.AvatarCompat.csproj')
    'FNA.Steamworks.csproj'     = @('FNA.Steamworks\FNA.Steamworks.csproj')
    'FNA.Steamworks.net9.csproj'= @('FNA.Steamworks\FNA.Steamworks.net9.csproj')
    'Steamworks.NET.csproj'     = @('FNA.Steamworks\Steamworks.NET\Standalone\Steamworks.NET.csproj')
    'Steamworks.NET.net9.csproj'= @('FNA.Steamworks\Steamworks.NET\Standalone\Steamworks.NET.net9.csproj')
}

function Get-ReferenceName([string]$include) {
    # MSBuild expressions may appear in directory segments; basename remains identifiable.
    return [System.IO.Path]::GetFileName(($include -replace '/', '\'))
}
function Resolve-ExistingReference([string]$projectDir, [string]$include) {
    if ($include -match '\$\(|\%\(') { return $false } # Cannot safely evaluate MSBuild properties here
    try {
        $candidate = if ([IO.Path]::IsPathRooted($include)) { $include } else { Join-Path $projectDir $include }
        return Test-Path -LiteralPath $candidate -PathType Leaf
    } catch { return $false }
}
function Get-Target([string]$name) {
    foreach ($relative in $known[$name]) {
        $candidate = Join-Path $CoreRoot $relative
        if (Test-Path -LiteralPath $candidate -PathType Leaf) { return $candidate }
    }
    return $null
}

$projects = @(Get-ChildItem -LiteralPath $ArchiveRoot -Filter '*.csproj' -File -Recurse -ErrorAction Stop |
    Where-Object { $_.FullName -notmatch '[\\/](?:obj|bin)[\\/]' })
$report = [System.Collections.Generic.List[object]]::new()
$changedProjects = 0
$changedRefs = 0
foreach ($project in $projects) {
    try {
        $doc = [System.Xml.XmlDocument]::new()
        $doc.PreserveWhitespace = $true
        $doc.Load($project.FullName)
        $nodes = @($doc.SelectNodes('//*[local-name()="ProjectReference"]'))
        $updates = [System.Collections.Generic.List[object]]::new()
        foreach ($node in $nodes) {
            $include = [string]$node.GetAttribute('Include')
            if (-not $include) { continue }
            $name = Get-ReferenceName $include
            if (-not $known.ContainsKey($name)) { continue }
            $isValid = Resolve-ExistingReference $project.DirectoryName $include
            if ($isValid -and -not $IncludeValidReferences) { continue }
            $target = Get-Target $name
            $status = if (-not $target) { 'MissingCoreProject' } elseif ($isValid) { 'AlreadyValid' } elseif ($include -match '\$\(') { 'PropertyBasedReference' } else { 'BrokenReference' }
            if ($status -eq 'AlreadyValid') { continue }
            # Do not overwrite an MSBuild-property-based reference automatically: it may resolve correctly.
            if ($status -eq 'PropertyBasedReference') {
                $report.Add([pscustomobject]@{ Project=$project.FullName; Reference=$include; Replacement=$target; Status='ReviewPropertyReference'; Backup='' })
                continue
            }
            if (-not $target) {
                $report.Add([pscustomobject]@{ Project=$project.FullName; Reference=$include; Replacement=''; Status=$status; Backup='' })
                continue
            }
            $replacement = if ($UseCoreProperty) {
                '$(IndieGamesCoreRoot)\' + $target.Substring($CoreRoot.TrimEnd('\').Length).TrimStart('\')
            } else { $target }
            $updates.Add([pscustomobject]@{ Node=$node; Old=$include; New=$replacement })
        }
        if ($updates.Count -eq 0) { continue }
        if ($PSCmdlet.ShouldProcess($project.FullName, "Repair $($updates.Count) shared ProjectReference(s)")) {
            $backup = $project.FullName + '.references.bak'
            if (-not (Test-Path -LiteralPath $backup)) { Copy-Item -LiteralPath $project.FullName -Destination $backup -ErrorAction Stop }
            foreach ($update in $updates) { $update.Node.SetAttribute('Include', $update.New) }
            # Preserve whitespace and UTF-8 encoding; XML serializer may normalize some XML formatting.
            $settings = [System.Xml.XmlWriterSettings]::new()
            $settings.Indent = $false
            $settings.Encoding = [System.Text.UTF8Encoding]::new($false)
            $settings.NewLineHandling = [System.Xml.NewLineHandling]::None
            $writer = [System.Xml.XmlWriter]::Create($project.FullName, $settings)
            try { $doc.Save($writer) } finally { $writer.Dispose() }
            $changedProjects++
            $changedRefs += $updates.Count
            $status = 'Fixed'
        } else {
            $backup = ''
            $status = 'WouldFix'
        }
        foreach ($update in $updates) {
            $report.Add([pscustomobject]@{ Project=$project.FullName; Reference=$update.Old; Replacement=$update.New; Status=$status; Backup=$backup })
        }
    } catch {
        $report.Add([pscustomobject]@{ Project=$project.FullName; Reference=''; Replacement=''; Status='Error: ' + $_.Exception.Message; Backup='' })
    }
}
$report | Export-Csv -LiteralPath $ReportPath -NoTypeInformation -Encoding UTF8
Write-Host "Projects scanned: $($projects.Count)"
Write-Host "Projects changed: $changedProjects"
Write-Host "References changed: $changedRefs"
Write-Host "References needing review: $( @($report | Where-Object { $_.Status -match 'Missing|Review|Error' }).Count )"
Write-Host "Report: $ReportPath"
if ($WhatIfPreference) { Write-Host 'DRY RUN ONLY. Re-run without -WhatIf to apply fixes.' -ForegroundColor Yellow }
