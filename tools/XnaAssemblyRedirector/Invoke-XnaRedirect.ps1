[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$InputFolder,
    [Parameter(Mandatory)][string]$OutputFolder,
    [Parameter(Mandatory)][string]$ToolDll,
    [Parameter(Mandatory)][string]$FnaDll,
    [string]$StubDll = '',
    [switch]$WhatIf
)
$ErrorActionPreference = 'Stop'
if (!(Test-Path -LiteralPath $InputFolder)) { Write-Host "XNA redirect: no DLL folder: $InputFolder"; return }
if (!(Test-Path -LiteralPath $ToolDll)) { throw "Missing redirector: $ToolDll" }
if (!(Test-Path -LiteralPath $FnaDll)) { throw "Missing FNA assembly: $FnaDll" }
New-Item -ItemType Directory -Path $OutputFolder -Force | Out-Null
$files = @(Get-ChildItem -LiteralPath $InputFolder -Filter '*.dll' -File)
foreach ($file in $files) {
    $argsList = @($ToolDll, '--mode', $(if ($WhatIf) { 'verify' } else { 'patch' }), '--input', $file.FullName, '--fna', $FnaDll, '--output', $OutputFolder)
    if ($StubDll -and (Test-Path -LiteralPath $StubDll)) { $argsList += @('--stub', $StubDll) }
    & dotnet @argsList
    if ($LASTEXITCODE -notin @(0,2)) { throw "Redirector failed for $($file.Name): $LASTEXITCODE" }
}
Write-Host "XNA redirect: scanned $($files.Count) DLL(s), patched files under $OutputFolder"
