[CmdletBinding(SupportsShouldProcess=$true)]
param(
    [Parameter(Mandatory)][string]$GameRoot,
    [Parameter(Mandatory)][string]$TargetDirectory,
    [string]$SourceDirectory = '',
    [string]$Redirector = 'C:\source\Indie-Games-Core\tools\XnaAssemblyRedirector\bin\Release\net9.0\XnaAssemblyRedirector.dll',
    [string]$Fna = 'C:\source\Indie-Games-Core\FNA\bin\x64\Debug\net8.0\FNA.dll',
    [string]$NetStub = 'C:\source\Indie-Games-Core\FNA.NetStub\bin\x64\Debug\net8.0-windows\FNA.NetStub.dll',
    [switch]$FailOnIncompatible
)
$ErrorActionPreference = 'Stop'
# Discover original assemblies from the GAME ROOT first, then the extracted package.
# Never use bin/obj or previous redirector outputs as original sources.
$GameRoot = [IO.Path]::GetFullPath($GameRoot)
$TargetDirectory = [IO.Path]::GetFullPath($TargetDirectory)
if (-not $SourceDirectory) { $SourceDirectory = $GameRoot }
$SourceDirectory = [IO.Path]::GetFullPath($SourceDirectory)
foreach ($p in @($GameRoot,$TargetDirectory,$SourceDirectory,$Redirector,$Fna,$NetStub)) {
    if (-not (Test-Path -LiteralPath $p)) { throw "Missing required path: $p" }
}
$excluded = @('FNA.dll','FNA.NetStub.dll','Microsoft.Xna.Framework.dll')
$cache = Join-Path $GameRoot 'obj\XnaRedirector\cache'
$log = Join-Path $GameRoot 'obj\XnaRedirector\discovery-report.csv'
$results = [System.Collections.Generic.List[object]]::new()
$errors = 0
$skipFolders = @('bin','obj','.git','XnaRedirect-Test','XnaRedirect-Patched','XnaRedirect-Patched-v3','XnaRedirect-Patched-v4','XnaRedirector')
$sourceRoot = $SourceDirectory.TrimEnd('\','/')
$sourceFiles = @(Get-ChildItem -LiteralPath $SourceDirectory -Recurse -File -Filter '*.dll' -ErrorAction Stop | Where-Object {
    $relative = $_.FullName.Substring($sourceRoot.Length).TrimStart('\','/')
    $parts = $relative -split '[\\/]'
    $blocked = @($parts | Where-Object { $_ -in $skipFolders -or $_ -like 'XnaRedirect-*' })
    $blocked.Count -eq 0 -and $_.Name -notin $excluded -and
    $_.Name -notlike 'System.*' -and $_.Name -notlike 'Microsoft.*'
})
# Read the PE CLR data-directory entry; native DLLs have no CLR header.
function Test-ManagedAssembly([string]$Path) {
    $stream = [IO.File]::OpenRead($Path)
    $reader = [IO.BinaryReader]::new($stream)
    try {
        if ($stream.Length -lt 256 -or $reader.ReadUInt16() -ne 0x5A4D) { return $false }
        $stream.Position = 0x3C
        $pe = $reader.ReadInt32()
        if ($pe -lt 0 -or ($pe + 24) -gt $stream.Length) { return $false }
        $stream.Position = $pe
        if ($reader.ReadUInt32() -ne 0x00004550) { return $false }
        $stream.Position = $pe + 20
        $optionalSize = [int]$reader.ReadUInt16()
        $optionalStart = $pe + 24
        if ($optionalSize -lt 224 -or ($optionalStart + $optionalSize) -gt $stream.Length) { return $false }
        $stream.Position = $optionalStart
        $magic = $reader.ReadUInt16()
        $directoryOffset = if ($magic -eq 0x10B) { 96 } elseif ($magic -eq 0x20B) { 112 } else { return $false }
        if ($optionalSize -lt ($directoryOffset + 15 * 8)) { return $false }
        $stream.Position = $optionalStart + $directoryOffset + (14 * 8)
        $rva = $reader.ReadUInt32()
        $size = $reader.ReadUInt32()
        return $rva -ne 0 -and $size -ne 0
    }
    catch { return $false }
    finally { $reader.Dispose(); $stream.Dispose() }
}

# Rank root DLLs ahead of extracted package copies. If the preferred rank has
# multiple candidates, skip as ambiguous rather than guessing.
$sources = @($sourceFiles | Group-Object Name)
Write-Host "XNA discovery root: $SourceDirectory"
Write-Host "Candidate DLL names: $($sources.Count)"
foreach ($group in $sources) {
    $name = $group.Name
    $dest = Join-Path $TargetDirectory $name
    if (-not (Test-Path -LiteralPath $dest -PathType Leaf)) { continue }
    $ranked = @($group.Group | ForEach-Object {
        $relative = $_.FullName.Substring($sourceRoot.Length).TrimStart('\','/')
        $parts = $relative -split '[\\/]'
        $rank = if ($parts.Count -eq 1) { 0 } elseif ($parts[0] -eq '584E07D1') { 1 } else { 2 }
        [pscustomobject]@{ File = $_; Rank = $rank }
    } | Sort-Object Rank)
    $bestRank = $ranked[0].Rank
    $best = @($ranked | Where-Object { $_.Rank -eq $bestRank })
    if ($best.Count -ne 1) {
        Write-Warning "Ambiguous original DLL: $name ($($best.Count) equally preferred copies); skipped"
        $results.Add([pscustomobject]@{Dll=$name;Status='Ambiguous';Detail='Multiple equally preferred source DLLs'})
        $errors++; continue
    }
    $source = $best[0].File.FullName
    if (-not (Test-ManagedAssembly $source)) {
        Write-Host "Skipping native/non-.NET DLL: $name"
        continue
    }
    Write-Host "Source for $name : $source"
    # Native tool failures should be reported as individual scan failures, not
    # converted to terminating PowerShell errors by ErrorActionPreference=Stop.
    $previousPreference = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    try { $scanOutput = @(& dotnet $Redirector --mode scan --input $source 2>&1) ; $scanExit = $LASTEXITCODE }
    finally { $ErrorActionPreference = $previousPreference }
    if ($scanExit -ne 0) {
        Write-Warning "Cannot inspect $name : $($scanOutput -join ' ')"
        $results.Add([pscustomobject]@{Dll=$name;Status='ScanFailed';Detail=($scanOutput -join ' ')})
        $errors++; continue
    }
    $referenceLine = @($scanOutput | Where-Object { $_ -match 'XNA references:\s*(\d+)' } | Select-Object -First 1)
    if ($referenceLine.Count -eq 0) { $errors++; Write-Warning "No XNA reference count for $name"; continue }
    $count = [int]([regex]::Match([string]$referenceLine[0], 'XNA references:\s*(\d+)').Groups[1].Value)
    if ($count -eq 0) { continue }
    Write-Host "XNA dependency detected: $name ($count references)" -ForegroundColor Cyan
    if (-not $PSCmdlet.ShouldProcess($dest, "Verify, patch and install $name")) {
        $results.Add([pscustomobject]@{Dll=$name;Status='WouldPatch';Detail="$count XNA references"}); continue
    }
    # Use an isolated patch folder because the redirector scans every DLL in its input directory.
    # PowerShell 5.1 does not support HashData/ToHexString; use Get-FileHash for compatibility.
    $key = (Get-FileHash -LiteralPath $source -Algorithm SHA256).Hash.Substring(0,12)
    $output = Join-Path $cache $key
    New-Item -ItemType Directory -Path $output -Force | Out-Null
    $patched = Join-Path $output $name
    $ErrorActionPreference = 'Continue'
    try { $patchLog = @(& dotnet $Redirector --mode patch --input $source --output $output --fna $Fna --netstub $NetStub 2>&1); $patchExit = $LASTEXITCODE }
    finally { $ErrorActionPreference = $previousPreference }
    if ($patchExit -ne 0 -or -not (Test-Path -LiteralPath $patched -PathType Leaf)) {
        Write-Warning "Incompatible or unpatchable $name : $($patchLog -join ' ')"
        $results.Add([pscustomobject]@{Dll=$name;Status='Incompatible';Detail=($patchLog -join ' ')})
        $errors++; continue
    }
    $ErrorActionPreference = 'Continue'
    try { $postScan = @(& dotnet $Redirector --mode scan --input $patched 2>&1); $postExit = $LASTEXITCODE }
    finally { $ErrorActionPreference = $previousPreference }
    if ($postExit -ne 0 -or -not (($postScan -join "`n") -match 'XNA references:\s*0')) {
        Write-Warning "Post-patch scan failed for $name"
        $results.Add([pscustomobject]@{Dll=$name;Status='PostScanFailed';Detail=($postScan -join ' ')})
        $errors++; continue
    }
    $backup = "$dest.before-xna-redirect.bak"
    if (-not (Test-Path -LiteralPath $backup)) { Copy-Item -LiteralPath $dest -Destination $backup }
    Copy-Item -LiteralPath $patched -Destination $dest -Force
    Write-Host "Installed patched: $dest" -ForegroundColor Green
    $results.Add([pscustomobject]@{Dll=$name;Status='Patched';Detail="$count XNA references redirected"})
}
if ($results.Count -gt 0) {
    New-Item -ItemType Directory -Path (Split-Path -Parent $log) -Force | Out-Null
    $results | Export-Csv -LiteralPath $log -NoTypeInformation -Encoding UTF8
    $results | Format-Table -AutoSize
    Write-Host "Report: $log"
} else { Write-Host 'No matching unpatched XNA-dependent DLLs found.' }
if ($errors -gt 0 -and $FailOnIncompatible) { throw "$errors DLL(s) could not be redirected; see report" }
