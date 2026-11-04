[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
param(
    [string]$CoreRoot = 'C:\source\Indie-Games-Core',
    [string]$SolutionName = 'Indie-Games.slnx',
    [switch]$Execute
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$solution = Join-Path $CoreRoot $SolutionName
if (-not (Test-Path -LiteralPath $solution -PathType Leaf)) { throw "Solution missing: $solution" }
$xml = New-Object System.Xml.XmlDocument
$xml.PreserveWhitespace = $true
$xml.Load($solution)
$removed = New-Object System.Collections.Generic.List[string]
$updated = New-Object System.Collections.Generic.List[string]
$missing = New-Object System.Collections.Generic.List[string]
$nodes = @($xml.SelectNodes('//Project | //File'))
foreach ($node in $nodes) {
    $old = $node.GetAttribute('Path')
    if ([string]::IsNullOrWhiteSpace($old)) { continue }
    $normalized = $old.Replace('\','/')
    $isArchive = $normalized -match '(?i)(^|/)indie-game-archive/' -or $normalized -match '(?i)^D:/downloads/indie-game-downloads/'
    if ($isArchive) {
        $removed.Add($old)
        [void]$node.ParentNode.RemoveChild($node)
        continue
    }
    $marker = '/source/Indie-Games/'
    $index = $normalized.IndexOf($marker, [StringComparison]::OrdinalIgnoreCase)
    if ($index -ge 0) {
        $relative = $normalized.Substring($index + $marker.Length)
        $candidate = Join-Path $CoreRoot ($relative.Replace('/', [IO.Path]::DirectorySeparatorChar))
        $node.SetAttribute('Path', $relative)
        $updated.Add("$old => $relative")
        if (-not (Test-Path -LiteralPath $candidate)) { $missing.Add($relative) }
    } elseif (-not [IO.Path]::IsPathRooted($old)) {
        $candidate = [IO.Path]::GetFullPath((Join-Path $CoreRoot $old))
        if (-not (Test-Path -LiteralPath $candidate)) { $missing.Add($old) }
    }
}
# Remove empty solution folders, preserving any folders with children.
foreach ($folder in @($xml.SelectNodes('//Folder'))) {
    if ($folder.ParentNode -and -not $folder.HasChildNodes) { [void]$folder.ParentNode.RemoveChild($folder) }
}
Write-Host "Solution: $solution"
Write-Host "Updated project/file paths: $($updated.Count)"
$updated | ForEach-Object { Write-Host "  UPDATE $_" }
Write-Host "Removed archive entries: $($removed.Count)"
$removed | ForEach-Object { Write-Host "  REMOVE $_" }
Write-Host "Missing paths after migration: $($missing.Count)"
$missing | ForEach-Object { Write-Warning "MISSING: $_" }
if (-not $Execute) { Write-Host 'PREVIEW ONLY. Add -Execute to back up and write the solution.'; return }
if ($PSCmdlet.ShouldProcess($solution, 'Back up and migrate solution XML')) {
    $backup = "$solution.backup.$(Get-Date -Format 'yyyyMMdd-HHmmss')"
    Copy-Item -LiteralPath $solution -Destination $backup -ErrorAction Stop
    $xml.Save($solution)
    Write-Host "Updated: $solution"
    Write-Host "Backup:  $backup"
    Write-Host 'No game projects, targets, or csproj files were modified.'
}
