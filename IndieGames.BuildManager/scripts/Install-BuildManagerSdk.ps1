<#
.SYNOPSIS
Checks or installs the x64 .NET SDK required to build Indie Games Build Manager.
.EXAMPLE
powershell.exe -NoProfile -ExecutionPolicy Bypass -File ".\IndieGames.BuildManager\scripts\Install-BuildManagerSdk.ps1"
#>
[CmdletBinding()]
param([switch]$CheckOnly)

$ErrorActionPreference = 'Stop'

# The fallback is used when the script ships with a standalone Build Manager.
$required = [version]'10.0.401'
$repo = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
$globalJson = Join-Path $repo 'global.json'
if (Test-Path -LiteralPath $globalJson -PathType Leaf) {
    $required = [version](Get-Content -LiteralPath $globalJson -Raw | ConvertFrom-Json).sdk.version
}

$programFiles64 = if ($env:ProgramW6432) { $env:ProgramW6432 } else { $env:ProgramFiles }
$dotnet = Join-Path $programFiles64 'dotnet\dotnet.exe'

function Get-CompatibleSdk {
    if (-not (Test-Path -LiteralPath $dotnet -PathType Leaf)) { return }
    & $dotnet --list-sdks | ForEach-Object {
        if ($_ -match '^(\d+\.\d+\.\d+)\s+\[') {
            $installed = [version]$Matches[1]
            if ($installed.Major -eq $required.Major -and
                $installed.Minor -eq $required.Minor -and
                [Math]::Floor($installed.Build / 100) -eq [Math]::Floor($required.Build / 100) -and
                $installed.Build -ge $required.Build) { $installed }
        }
    }
}

$compatible = Get-CompatibleSdk | Sort-Object -Descending | Select-Object -First 1
if (-not $compatible) {
    if ($CheckOnly) { throw "Missing x64 .NET SDK $required or a later patch in the same SDK feature band." }
    $winget = Get-Command winget.exe -ErrorAction SilentlyContinue
    if (-not $winget) { throw 'Install App Installer from Microsoft Store to get winget, then run this script again.' }

    $package = "Microsoft.DotNet.SDK.$($required.Major)"
    Write-Host "Installing x64 .NET SDK $required..."
    & $winget.Source install --id $package --exact --version ($required.ToString()) --architecture x64 --source winget --accept-source-agreements --accept-package-agreements --silent
    if ($LASTEXITCODE -ne 0) { throw "WinGet returned $LASTEXITCODE while installing $package. Try again from an elevated PowerShell." }

    $compatible = Get-CompatibleSdk | Sort-Object -Descending | Select-Object -First 1
    if (-not $compatible) { throw "Setup finished, but a compatible x64 SDK could not be found at $dotnet." }
}

Write-Host "Ready: x64 .NET SDK $compatible ($dotnet)."
if (Test-Path -LiteralPath $globalJson -PathType Leaf) {
    Push-Location -LiteralPath $repo
    try {
        $resolved = & $dotnet --version
        if ($LASTEXITCODE -ne 0) { throw 'The repository could not resolve a .NET SDK.' }
        Write-Host "SDK selected by this checkout: $resolved"
    }
    finally { Pop-Location }
}

$framework = Join-Path ${env:ProgramFiles(x86)} 'Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll'
if (-not (Test-Path -LiteralPath $framework -PathType Leaf)) {
    Write-Warning 'Steamworks projects also need the .NET Framework 4.8 Developer Pack. Install it from https://dotnet.microsoft.com/download/dotnet-framework/net48.'
}
