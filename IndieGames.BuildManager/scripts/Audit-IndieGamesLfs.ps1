#requires -Version 5.1
<#
.SYNOPSIS
  Read-only audit of Git LFS and game assets in the Indie-Games repository.
.DESCRIPTION
  Creates a file inventory, LFS summary, migration candidates, and per-game
  manifests. Does not change Git history, .gitattributes, or working files.
  All classifications are proposals and should be reviewed before migration.
.EXAMPLE
  .\Audit-IndieGamesLfs.ps1 -DryRun
.EXAMPLE
  .\Audit-IndieGamesLfs.ps1 -RepoRoot 'C:\source\Indie-Games' -ReportDir 'C:\temp\indie-lfs-audit'
#>
[CmdletBinding()]
param(
    [string]$RepoRoot = 'C:\source\Indie-Games',
    [string]$ArchiveRelativePath = 'indie-game-archive',
    [string]$ReportDir,
    [switch]$DryRun,
    [switch]$IncludeUntracked
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Invoke-GitLines {
    param([string[]]$GitArgs)
    $result = @(& git -C $RepoRoot @GitArgs 2>&1)
    if ($LASTEXITCODE -ne 0) { throw "git $($GitArgs -join ' ') failed: $($result -join [Environment]::NewLine)" }
    return $result
}
function Get-Category {
    param([string]$RelativePath)
    $p = $RelativePath.Replace('\','/')
    $ext = [IO.Path]::GetExtension($p).ToLowerInvariant()
    $file = [IO.Path]::GetFileName($p).ToLowerInvariant()
    $parts = $p -split '/'
    $insideArchive = $p.StartsWith($script:ArchivePrefix + '/', [StringComparison]::OrdinalIgnoreCase)
    if (-not $insideArchive) { return 'OutsideArchive_Review' }
    if ($parts -contains 'obj' -or $parts -contains 'bin' -or $parts -contains '.vs') { return 'Generated_Review' }
    if ($file -eq '.gitattributes' -or $file -eq '.gitignore' -or $file -eq '.gitmodules') { return 'KeepSource' }
    $sourceExtensions = @('.cs','.csproj','.sln','.slnx','.fs','.fsproj','.vb','.vbproj',
        '.props','.targets','.ps1','.psm1','.psd1','.py','.cmd','.bat','.sh',
        '.json','.jsonc','.xml','.xaml','.config','.editorconfig','.ruleset',
        '.md','.markdown','.txt','.yml','.yaml','.toml','.ini','.resx',
        '.s','.h','.hpp','.c','.cpp','.rc','.nuspec','.sql')
    if ($sourceExtensions -contains $ext) { return 'KeepSource' }
    $assetExtensions = @('.xnb','.xsb','.xwb','.xgs','.wav','.wma','.mp3','.ogg','.flac',
        '.png','.jpg','.jpeg','.bmp','.gif','.dds','.tga','.tif','.tiff','.webp','.ico',
        '.fbx','.obj','.mtl','.dae','.3ds','.blend','.gltf','.glb',
        '.fxb','.mgfxo','.spritefont','.ttf','.otf','.mp4','.wmv','.avi','.mov',
        '.zip','.7z','.rar','.iso','.xiso','.stfs','.pak','.dat','.bin','.content',
        '.xex','.xbe','.dll','.exe','.pdb','.mdb')
    if ($assetExtensions -contains $ext) { return 'MoveAsset' }
    if ($parts -contains 'Content' -or $parts -contains 'Assets' -or $parts -contains 'Resources' -or $parts -contains 'Textures' -or $parts -contains 'Audio') {
        return 'ReviewContentFolder'
    }
    return 'Unknown_Review'
}
function Get-GameName {
    param([string]$RelativePath)
    $p = $RelativePath.Replace('\','/')
    if (-not $p.StartsWith($script:ArchivePrefix + '/', [StringComparison]::OrdinalIgnoreCase)) { return $null }
    $rest = $p.Substring($script:ArchivePrefix.Length + 1)
    if ($rest -notmatch '/') { return $null }
    return ($rest -split '/',2)[0]
}
function Get-Sha256 {
    param([string]$Path)
    try { return (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToLowerInvariant() }
    catch { return $null }
}

$RepoRoot = [IO.Path]::GetFullPath($RepoRoot)
$script:ArchivePrefix = $ArchiveRelativePath.Replace('\','/').Trim('/')
if (-not (Test-Path -LiteralPath $RepoRoot -PathType Container)) { throw "Repository folder not found: $RepoRoot" }
if (-not (Get-Command git -ErrorAction SilentlyContinue)) { throw 'git executable not found in PATH' }
$gitTop = ((Invoke-GitLines -GitArgs @('rev-parse','--show-toplevel')) | Select-Object -First 1).Trim()
if ([IO.Path]::GetFullPath($gitTop).TrimEnd('\','/') -ne $RepoRoot.TrimEnd('\','/')) {
    throw "RepoRoot must be the Git repository root; detected: $gitTop"
}
if (-not $ReportDir) {
    $ReportDir = Join-Path ([IO.Path]::GetTempPath()) ('IndieGames-LfsAudit-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
}
$ReportDir = [IO.Path]::GetFullPath($ReportDir)
New-Item -ItemType Directory -Force -Path $ReportDir | Out-Null
Write-Host "Repository: $RepoRoot" -ForegroundColor Cyan
Write-Host "Reports:    $ReportDir" -ForegroundColor Cyan
Write-Host 'Mode:       READ ONLY (DryRun is an explicit confirmation; both modes audit only)' -ForegroundColor Yellow

# Use NUL-separated Git output to safely handle spaces, Unicode and unusual filenames.
# PowerShell 5.1 decodes native stdout through its text pipeline; use a temporary file
# and UTF-8 decoding to preserve paths. The temp file is outside the repository.
function Get-GitPaths {
    param([string[]]$Arguments)
    $tmp = [IO.Path]::GetTempFileName()
    try {
        $proc = Start-Process -FilePath (Get-Command git).Source -ArgumentList (@('-C',('"' + $RepoRoot + '"')) + $Arguments) -Wait -PassThru -NoNewWindow -RedirectStandardOutput $tmp
        if ($proc.ExitCode -ne 0) { throw "Git path enumeration failed (exit $($proc.ExitCode)): $($Arguments -join ' ')" }
        $bytes = [IO.File]::ReadAllBytes($tmp)
        $content = [Text.Encoding]::UTF8.GetString($bytes)
        return @($content.Split([char]0) | Where-Object { $_.Length -gt 0 })
    } finally { Remove-Item -LiteralPath $tmp -Force -ErrorAction SilentlyContinue }
}
$tracked = @(Get-GitPaths @('ls-files','-z','--cached'))
$lfsAvailable = $false
try { $null = Invoke-GitLines -GitArgs @('lfs','version'); $lfsAvailable = $true } catch { Write-Warning 'Git LFS command unavailable.' }
$lfsPaths = @()
if ($lfsAvailable) {
    try {
        # git-lfs ls-files does not accept -z. Use line-separated names.
        # core.quotePath=false avoids Git's C-style quoting for Unicode paths.
        $lfsOutput = @(Invoke-GitLines -GitArgs @('-c','core.quotePath=false','lfs','ls-files','--name-only'))
        $lfsPaths = @($lfsOutput | ForEach-Object { ([string]$_).TrimEnd("`r") } | Where-Object { $_.Length -gt 0 })
    } catch {
        throw "Cannot enumerate LFS files; audit stopped to avoid incorrect migration recommendations: $_"
    }
} else { Write-Warning 'git-lfs not found; LFS inventory will be incomplete.' }
$lfsSet = New-Object 'System.Collections.Generic.HashSet[string]' ([StringComparer]::Ordinal)
foreach ($p in $lfsPaths) { [void]$lfsSet.Add($p) }
$trackedSet = New-Object 'System.Collections.Generic.HashSet[string]' ([StringComparer]::Ordinal)
foreach ($p in $tracked) { [void]$trackedSet.Add($p) }
$all = New-Object 'System.Collections.Generic.HashSet[string]' ([StringComparer]::Ordinal)
foreach ($p in $tracked) { [void]$all.Add($p) }
if ($IncludeUntracked) {
    foreach ($p in @(Get-GitPaths @('ls-files','--others','--exclude-standard','-z'))) { [void]$all.Add($p) }
}
$rows = New-Object 'System.Collections.Generic.List[object]'
$index = 0
foreach ($p in $all) {
    $index++
    $local = Join-Path $RepoRoot ($p.Replace('/',[IO.Path]::DirectorySeparatorChar))
    $exists = Test-Path -LiteralPath $local -PathType Leaf
    $bytes = $null
    if ($exists) { $bytes = (Get-Item -LiteralPath $local).Length }
    $category = Get-Category $p
    $isLfs = $lfsSet.Contains($p)
    $action = switch ($category) {
        'KeepSource' { if ($isLfs) { 'ConvertLfsToGit' } else { 'KeepInGit' }; break }
        'MoveAsset' { 'ExternalArchiveCandidate'; break }
        'Generated_Review' { 'ReviewGeneratedFiles'; break }
        default { 'ManualReview' }
    }
    $rows.Add([pscustomobject]@{
        Path = $p; Game = Get-GameName $p; Category = $category
        ProposedAction = $action; IsLfs = $isLfs
        IsTracked = $trackedSet.Contains($p); ExistsLocally = $exists
        LocalBytes = $bytes; LocalMiB = if ($null -ne $bytes) { [math]::Round($bytes / 1MB, 3) } else { $null }
    })
    if ($index % 5000 -eq 0) { Write-Host "Inventoried $index files..." }
}
$inventory = @($rows | Sort-Object Path)
$inventory | Export-Csv -LiteralPath (Join-Path $ReportDir 'file-inventory.csv') -NoTypeInformation -Encoding UTF8
@($inventory | Where-Object IsLfs) | Export-Csv -LiteralPath (Join-Path $ReportDir 'lfs-files.csv') -NoTypeInformation -Encoding UTF8
@($inventory | Where-Object { $_.ProposedAction -eq 'ExternalArchiveCandidate' }) | Export-Csv -LiteralPath (Join-Path $ReportDir 'archive-candidates.csv') -NoTypeInformation -Encoding UTF8
@($inventory | Where-Object { $_.ProposedAction -eq 'ConvertLfsToGit' }) | Export-Csv -LiteralPath (Join-Path $ReportDir 'lfs-source-to-convert.csv') -NoTypeInformation -Encoding UTF8
@($inventory | Where-Object { $_.ProposedAction -like '*Review*' }) | Export-Csv -LiteralPath (Join-Path $ReportDir 'manual-review.csv') -NoTypeInformation -Encoding UTF8

$manifests = @($inventory | Where-Object { $_.Game -and $_.ProposedAction -eq 'ExternalArchiveCandidate' } | Group-Object Game | ForEach-Object {
    $group = $_
    [pscustomobject]@{
        game = $group.Name
        provider = 'internet_archive'
        identifier = $null
        download_url = $null
        assets = @($group.Group | ForEach-Object {
            [pscustomobject]@{ path = $_.Path; size_bytes = $_.LocalBytes; sha256 = $null }
        })
    }
})
$manifests | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath (Join-Path $ReportDir 'game-asset-manifests.json') -Encoding UTF8
$summary = [pscustomobject]@{
    repository = $RepoRoot
    created_utc = (Get-Date).ToUniversalTime().ToString('o')
    dry_run = [bool]$DryRun
    read_only = $true
    tracked_file_count = $tracked.Count
    lfs_file_count = $lfsPaths.Count
    inventory_count = $inventory.Count
    external_archive_candidate_count = @($inventory | Where-Object ProposedAction -eq 'ExternalArchiveCandidate').Count
    lfs_source_conversion_count = @($inventory | Where-Object ProposedAction -eq 'ConvertLfsToGit').Count
    manual_review_count = @($inventory | Where-Object { $_.ProposedAction -like '*Review*' }).Count
    game_count_with_candidates = $manifests.Count
    candidate_local_bytes = [long](($inventory | Where-Object ProposedAction -eq 'ExternalArchiveCandidate' | Measure-Object LocalBytes -Sum).Sum)
    notes = @(
        'LocalBytes measures working-tree files, not historical Git LFS storage.',
        'Missing local files and Git LFS pointer-only files require review before uploading.',
        'Manifest hashes are placeholders; calculate from validated assets before publication.',
        'Extensions and paths are heuristic; review all classifications.',
        'No Git history, LFS configuration, tracked files, or remote storage were modified.'
    )
}
$summary | ConvertTo-Json -Depth 5 | Set-Content -LiteralPath (Join-Path $ReportDir 'summary.json') -Encoding UTF8
Write-Host ''
$summary | Format-List | Out-Host
Write-Host "Audit complete: $ReportDir" -ForegroundColor Green
