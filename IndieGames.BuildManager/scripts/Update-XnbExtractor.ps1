[CmdletBinding()]
param(
    [string]$RepoRoot = 'C:\source\xnb-extractor',
    [string]$ManagerRoot = 'C:\PycharmProjects\xenia-game-manager',
    [string]$Configuration = 'Release',
    [ValidateSet('Build', 'Copy')][string]$Mode = 'Build',
    [string]$SourceFolder,
    [string]$Project,
    [string]$Runtime = 'win-x64',
    [switch]$SelfContained,
    [switch]$SingleFile,
    [switch]$SkipUpdate,
    [switch]$Clean,
    [switch]$Force
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

function Run([string]$Exe, [string[]]$Arguments) {
    Write-Host "> $Exe $($Arguments -join ' ')" -ForegroundColor Cyan
    & $Exe @Arguments
    if ($LASTEXITCODE -ne 0) { throw "$Exe failed ($LASTEXITCODE)" }
}

function Resolve-Project {
    if ($Project) {
        $candidate = if ([IO.Path]::IsPathRooted($Project)) { $Project } else { Join-Path $RepoRoot $Project }
        if (!(Test-Path -LiteralPath $candidate -PathType Leaf)) { throw "Project not found: $candidate" }
        return (Resolve-Path -LiteralPath $candidate).Path
    }
    $projects = @(Get-ChildItem -LiteralPath $RepoRoot -Filter '*.csproj' -Recurse -File |
        Where-Object { $_.FullName -notmatch '[\\/](bin|obj)[\\/]' -and $_.BaseName -match '(?i)xnb.?extractor' })
    if ($projects.Count -ne 1) {
        throw "Expected one XnbExtractor project; found $($projects.Count). Specify -Project explicitly."
    }
    return $projects[0].FullName
}

function Deploy-Folder([string]$From, [string]$To) {
    if (!(Test-Path -LiteralPath $From -PathType Container)) { throw "Source folder not found: $From" }
    $resolvedSource = (Resolve-Path -LiteralPath $From).Path.TrimEnd('\','/')
    $resolvedDestination = [IO.Path]::GetFullPath($To).TrimEnd('\','/')
    if ($resolvedSource -ieq $resolvedDestination) { throw 'Source and destination must be different.' }
    if (!(Get-ChildItem -LiteralPath $resolvedSource -File | Where-Object { $_.Name -match '(?i)^xnb.?extractor\.(exe|dll)$' })) {
        throw "No XnbExtractor executable/assembly found at source root: $resolvedSource"
    }
    if (Test-Path -LiteralPath $To) {
        if (!$Force) { throw "Destination exists: $To (use -Force)" }
    }
    $parent = Split-Path -Path $To -Parent
    New-Item -ItemType Directory -Path $parent -Force | Out-Null
    $stage = Join-Path $parent ('.xnb-extractor-stage-' + [guid]::NewGuid().ToString('N'))
    New-Item -ItemType Directory -Path $stage -Force | Out-Null
    try {
        Copy-Item -Path (Join-Path $resolvedSource '*') -Destination $stage -Recurse -Force
        # Keep manager-specific assets that are not present in a standard dotnet publish output.
        foreach ($assetFolder in @('tools', 'xna-references')) {
            $sourceAsset = Join-Path $RepoRoot $assetFolder
            $stagedAsset = Join-Path $stage $assetFolder
            if ($Mode -eq 'Build' -and (Test-Path -LiteralPath $sourceAsset -PathType Container) -and
                !(Test-Path -LiteralPath $stagedAsset)) {
                Copy-Item -LiteralPath $sourceAsset -Destination $stagedAsset -Recurse -Force
            }
        }
        if (Test-Path -LiteralPath $To) {
            foreach ($assetFolder in @('tools', 'xna-references')) {
                $existingAsset = Join-Path $To $assetFolder
                $stagedAsset = Join-Path $stage $assetFolder
                if ((Test-Path -LiteralPath $existingAsset -PathType Container) -and
                    !(Test-Path -LiteralPath $stagedAsset)) {
                    Copy-Item -LiteralPath $existingAsset -Destination $stagedAsset -Recurse -Force
                }
            }
            Remove-Item -LiteralPath $To -Recurse -Force
        }
        Move-Item -LiteralPath $stage -Destination $To
    }
    finally {
        if (Test-Path -LiteralPath $stage) { Remove-Item -LiteralPath $stage -Recurse -Force }
    }
    Write-Host "Deployed: $To" -ForegroundColor Green
}


$destination = Join-Path $ManagerRoot 'src\assets\tools\xnb-extractor'
if (!(Test-Path -LiteralPath $ManagerRoot -PathType Container)) {
    throw "Manager root not found: $ManagerRoot"
}

if ($Mode -eq 'Build') {
    if (!(Test-Path -LiteralPath $RepoRoot -PathType Container)) { throw "Repository not found: $RepoRoot" }
    if (!$SkipUpdate) {
        Run git @('-C', $RepoRoot, 'pull', '--ff-only')
        Run git @('-C', $RepoRoot, 'submodule', 'update', '--init', '--recursive')
    }
    $projectPath = Resolve-Project
    if ($Clean) { Run dotnet @('clean', $projectPath, '-c', $Configuration, '-r', $Runtime) }
    $publishDir = Join-Path ([IO.Path]::GetTempPath()) ("xnb-extractor-publish-" + [guid]::NewGuid().ToString('N'))
    try {
        $publishArgs = @('publish', $projectPath, '-c', $Configuration, '-r', $Runtime,
            '--self-contained', $SelfContained.ToString().ToLowerInvariant(), '-o', $publishDir)
        if ($SingleFile) {
            $publishArgs = @('publish', $projectPath, '-c', $Configuration, '-r', $Runtime,
                '--self-contained', 'true', '-o', $publishDir)
            $publishArgs += @( '-p:PublishSingleFile=true',
                '-p:IncludeNativeLibrariesForSelfExtract=true', '-p:DebugType=none',
                '-p:DebugSymbols=false')
        }
        Run dotnet $publishArgs
        $SourceFolder = $publishDir
        Deploy-Folder -From $SourceFolder -To $destination
    }
    finally {
        if (Test-Path -LiteralPath $publishDir) { Remove-Item -LiteralPath $publishDir -Recurse -Force }
    }
}
else {
    if (!$SourceFolder) { throw 'Copy mode requires -SourceFolder pointing to an existing publish/output folder.' }
    Deploy-Folder -From $SourceFolder -To $destination
}

Write-Host 'XNB Extractor deployment complete.' -ForegroundColor Green
