[CmdletBinding()]
param(
    [string]$ArchiveRoot = 'D:\indie-game-archive',
    [string]$Report = 'D:\indie-game-archive\XnaContentReaderAudit-v3.csv',
    [switch]$NoBuilt
)
$ErrorActionPreference = 'Stop'
$project = Join-Path $PSScriptRoot 'XnaContentReaderRepair.csproj'
$dll = Join-Path $PSScriptRoot 'bin\Release\net9.0\XnaContentReaderRepair.dll'
if (-not (Test-Path -LiteralPath $dll)) {
    & dotnet build $project -c Release
    if ($LASTEXITCODE -ne 0) { throw "Build failed: $LASTEXITCODE" }
}
$argsList = @($dll, '--audit', '--root', $ArchiveRoot, '--report', $Report)
if ($NoBuilt) { $argsList += '--no-built' }
& dotnet @argsList
if ($LASTEXITCODE -ne 0) { throw "Audit failed: $LASTEXITCODE" }
