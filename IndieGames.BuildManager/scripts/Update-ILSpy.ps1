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
    if (Test-Path $To) {
        if (!$Force) { throw "Destination exists: $To (use -Force)" }
        Remove-Item $To -Recurse -Force
    }
    New-Item -ItemType Directory -Path $To -Force | Out-Null
    Copy-Item (Join-Path $From '*') $To -Recurse -Force
    Write-Host "Deployed $To" -ForegroundColor Green
}
$tools = Join-Path $ManagerRoot 'src\assets\tools'
Deploy (Find-Output $gui 'ILSpy.exe') (Join-Path $tools 'ilspy')
Deploy (Find-Output $cli 'ilspycmd.dll') (Join-Path $tools 'ilspycmd')
Write-Host 'ILSpy deployment complete.' -ForegroundColor Green
