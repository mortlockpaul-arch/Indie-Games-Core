[CmdletBinding()]
param(
    [string]$RepoRoot = 'C:\source\ILSpy',
    [string]$ManagerRoot = 'C:\PycharmProjects\xenia-game-manager',
    [string]$Configuration = 'Release',
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
$gui = Join-Path $RepoRoot 'ILSpy\ILSpy.csproj'
$cli = Join-Path $RepoRoot 'ICSharpCode.ILSpyCmd\ICSharpCode.ILSpyCmd.csproj'
foreach ($project in @($gui,$cli)) {
    if (!(Test-Path -LiteralPath $project)) { throw "Missing project: $project" }
}
Set-Location -LiteralPath $RepoRoot
if (!$SkipUpdate) {
    Run git @('-C',$RepoRoot,'pull','--ff-only')
    Run git @('-C',$RepoRoot,'submodule','update','--init','--recursive')
}
foreach ($project in @($gui,$cli)) {
    if ($Clean) { Run dotnet @('clean',$project,'-c',$Configuration) }
    Run dotnet @('restore',$project)
    Run dotnet @('build',$project,'-c',$Configuration,'--no-restore')
}
function Find-Output([string]$Project,[string]$Name) {
    $bin = Join-Path (Split-Path $Project -Parent) 'bin'
    $found = @(Get-ChildItem $bin -File -Recurse -ErrorAction SilentlyContinue |
        Where-Object { $_.Name -ieq $Name -and $_.FullName -match "[\\/]$Configuration[\\/]" } |
        Sort-Object LastWriteTimeUtc -Descending)
    if ($found.Count -eq 0) { throw "Could not find $Name in $bin" }
    return $found[0].DirectoryName
}
function Deploy([string]$From,[string]$To) {
    $allowedRoot = [IO.Path]::GetFullPath((Join-Path $ManagerRoot 'src\assets\tools')).TrimEnd('\') + '\'
    $resolvedTo = [IO.Path]::GetFullPath($To)
    if (!$resolvedTo.StartsWith($allowedRoot,[StringComparison]::OrdinalIgnoreCase) -or
        [IO.Path]::GetFileName($resolvedTo) -notin @('ilspy','ilspycmd')) { throw 'Unexpected ILSpy deployment destination.' }
    if (Test-Path $To) {
        if (!$Force) { throw "Destination exists: $To (use -Force)" }
        Remove-Item $To -Recurse -Force
    }
    New-Item -ItemType Directory -Path $To -Force | Out-Null
    Copy-Item (Join-Path $From '*') $To -Recurse -Force
    Write-Host "Deployed $To" -ForegroundColor Green
}
$tools = Join-Path $ManagerRoot 'src\assets\tools'
$outputs = @{ ilspy = (Find-Output $gui 'ILSpy.exe'); ilspycmd = (Find-Output $cli 'ilspycmd.dll') }

# Keep the archive copies current: the app removes extracted tool folders after use.
$sevenZip = Join-Path $ManagerRoot 'src\assets\zip\7z.exe'
if (!(Test-Path -LiteralPath $sevenZip)) { $sevenZip = 'C:\Program Files\7-Zip\7z.exe' }
if (!(Test-Path -LiteralPath $sevenZip)) { throw '7-Zip is required to persist updated ILSpy tool archives.' }
$stage = Join-Path ([IO.Path]::GetTempPath()) ('ilspy-archives-' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $stage | Out-Null
foreach ($name in @('ilspy','ilspycmd')) {
    $archive = Join-Path $stage ($name + '.7z')
    Push-Location $outputs[$name]
    try {
        $arguments = @('a','-t7z','-mx=5', $archive, '.')
        if ($name -eq 'ilspy') { $arguments += '-v30m' }
        Run $sevenZip $arguments
    } finally { Pop-Location }
    $backup = Join-Path $stage ($name + '-previous')
    New-Item -ItemType Directory -Path $backup | Out-Null
    $toolsPath = [IO.Path]::GetFullPath($tools).TrimEnd('\') + '\'
    $oldFiles = @(Get-ChildItem -LiteralPath $tools -File | Where-Object { $_.Name -eq "$name.7z" -or $_.Name -match ('^' + $name + '\.7z\.\d+$') })
    foreach ($old in $oldFiles) {
        if (!$old.FullName.StartsWith($toolsPath,[StringComparison]::OrdinalIgnoreCase)) { throw 'Archive path outside tools folder.' }
        Move-Item -LiteralPath $old.FullName -Destination $backup
    }
    try {
        Get-ChildItem -LiteralPath $stage -File | Where-Object { $_.Name -like "$name.7z*" } | Copy-Item -Destination $tools
    } catch {
        Get-ChildItem -LiteralPath $backup -File | Copy-Item -Destination $tools -Force
        throw
    }
}
Write-Host "Archive backups: $stage"
Write-Host 'ILSpy deployment complete.' -ForegroundColor Green
