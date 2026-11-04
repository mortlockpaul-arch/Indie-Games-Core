#requires -Version 5.1
[CmdletBinding(SupportsShouldProcess=$true)]
param(
    [Parameter(Mandatory=$true)][string]$InputPath,
    [string]$OutputDirectory = '',
    [string]$ExtractorExe = '',
    [string[]]$ExtractorArguments = @(
        '--input', '{input}',
        '--output', '{output}',
        '--loader',
        '--parser',
        '--extract'
    ),
    [string]$SourceRoot = '',
    [string]$CompilerExe = '',
    [string[]]$CompilerArguments = @('{source}','{output}'),
    [switch]$Compile,
    [switch]$IncludeAllXnb,
    [switch]$Overwrite,
    [switch]$Recurse,
    [switch]$FailOnUnresolved
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Expand-Arguments([string[]]$Template, [hashtable]$Tokens) {
    $result = @()
    foreach ($arg in $Template) {
        $value = $arg
        foreach ($key in $Tokens.Keys) { $value = $value.Replace('{' + $key + '}', [string]$Tokens[$key]) }
        $result += $value
    }
    return ,$result
}
function Read-XnbHeader([string]$Path) {
    $stream = [System.IO.File]::OpenRead($Path)
    try {
        $bytes = New-Object byte[] 14
        $count = $stream.Read($bytes, 0, 14)
        if ($count -lt 10 -or [System.Text.Encoding]::ASCII.GetString($bytes,0,3) -ne 'XNB') { throw 'Not an XNB file' }
        $flags = [int]$bytes[5]
        $compressed = (($flags -band 0x80) -ne 0) -or (($flags -band 0x40) -ne 0)
        $declared = [BitConverter]::ToUInt32($bytes,6)
        $uncompressed = $null
        if ($compressed) {
            if ($count -lt 14) { throw 'Truncated compressed XNB header' }
            $uncompressed = [BitConverter]::ToUInt32($bytes,10)
        }
        [pscustomobject]@{
            Platform = [char]$bytes[3]; Version = [int]$bytes[4]; Flags = ('0x{0:X2}' -f $flags)
            Compressed = $compressed; Compression = if ($flags -band 0x80) {'LZX'} elseif ($flags -band 0x40) {'LZ4'} else {'None'}
            DeclaredSize = $declared; ActualSize = $stream.Length; DecompressedSize = $uncompressed
            HeaderValid = ($declared -eq $stream.Length)
        }
    } finally { $stream.Dispose() }
}
function Invoke-External([string]$Executable, [string[]]$Arguments) {
    Write-Host ('> "{0}" {1}' -f $Executable, ($Arguments -join ' ')) -ForegroundColor Cyan
    $global:LASTEXITCODE = 0
    & $Executable @Arguments
    $exitCode = $LASTEXITCODE
    if ($null -ne $exitCode -and $exitCode -ne 0) { throw "Command failed with exit code $exitCode" }
}
function Find-Source([string]$Xnb, [string]$Root) {
    $sameFolder = [System.IO.Path]::ChangeExtension($Xnb, '.fx')
    if (Test-Path -LiteralPath $sameFolder -PathType Leaf) { return $sameFolder }
    if (-not $Root -or -not (Test-Path -LiteralPath $Root -PathType Container)) { return $null }
    $name = [System.IO.Path]::GetFileNameWithoutExtension($Xnb) + '.fx'
    $matches = @(Get-ChildItem -LiteralPath $Root -Filter $name -File -Recurse -ErrorAction SilentlyContinue)
    if ($matches.Count -eq 1) { return $matches[0].FullName }
    return $null
}
function Test-EffectCandidate([string]$Path, [string]$Source, [bool]$AllXnb) {
    if ($AllXnb -or $Source) { return $true }
    # Path includes the input root, so a root named Shaders is recognized too.
    return ($Path -match '(?i)[\\/](?:effects?|shaders?)[\\/]')
}

$inputItem = Get-Item -LiteralPath $InputPath -ErrorAction Stop
if (-not $inputItem.PSIsContainer -and $inputItem.Extension -ine '.xnb') { throw 'Input file must be an .xnb file' }
if ($ExtractorExe -and -not (Test-Path -LiteralPath $ExtractorExe -PathType Leaf)) { throw "Extractor executable not found: $ExtractorExe" }
if ($Compile -and $CompilerExe -and -not (Test-Path -LiteralPath $CompilerExe -PathType Leaf)) { throw "Compiler executable not found: $CompilerExe" }
if (-not $OutputDirectory) {
    $base = if ($inputItem.PSIsContainer) { $inputItem.FullName } else { $inputItem.DirectoryName }
    $OutputDirectory = Join-Path $base 'effect-recovery'
}
$OutputDirectory = [System.IO.Path]::GetFullPath($OutputDirectory)
if ($inputItem.PSIsContainer) {
    $root = $inputItem.FullName.TrimEnd('\','/')
    $files = @(Get-ChildItem -LiteralPath $inputItem.FullName -Filter '*.xnb' -File -Recurse:$Recurse | Where-Object {
        -not $_.FullName.StartsWith(($OutputDirectory.TrimEnd('\','/') + '\'), [System.StringComparison]::OrdinalIgnoreCase)
    })
} else {
    $files = @($inputItem)
    $root = $inputItem.DirectoryName.TrimEnd('\','/')
}
if (-not (Test-Path -LiteralPath $OutputDirectory)) { New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null }
$results = [System.Collections.Generic.List[object]]::new()
foreach ($file in $files) {
    $status = 'Inspected'; $errorMessage = ''; $source = $null; $compiled = $null; $extracted = $null
    $header = $null; $candidate = $false; $extractFileCount = 0
    try {
        $header = Read-XnbHeader $file.FullName
        $relative = $file.FullName.Substring($root.Length).TrimStart('\','/')
        $relativeDir = Split-Path -Parent $relative
        $destDir = if ($relativeDir) { Join-Path $OutputDirectory $relativeDir } else { $OutputDirectory }
        if (-not (Test-Path -LiteralPath $destDir)) { New-Item -ItemType Directory -Path $destDir -Force | Out-Null }
        $baseName = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
        $metadata = Join-Path $destDir ($baseName + '.xnb-info.json')
        $header | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath $metadata -Encoding UTF8
        $source = Find-Source $file.FullName $SourceRoot
        if ($null -eq $source) { $source = Find-Source $file.FullName $file.DirectoryName }
        $candidate = Test-EffectCandidate $file.FullName $source ([bool]$IncludeAllXnb)
        if (-not $candidate) {
            $status = 'Skipped (not identified as effect candidate)'
        } else {
            $status = if ($source) { 'Source found' } else { 'Candidate (source not found)' }
            if ($ExtractorExe) {
                $extractDir = Join-Path $destDir ($baseName + '.extracted')
                $tokens = @{input=$file.FullName; output=$extractDir; source=$(if ($source) { $source } else { '' }); name=$baseName}
                $argsList = Expand-Arguments $ExtractorArguments $tokens
                if ($PSCmdlet.ShouldProcess($file.FullName, 'Run configured XNB extractor')) {
                    if ((Test-Path -LiteralPath $extractDir) -and -not $Overwrite) {
                        $existing = @(Get-ChildItem -LiteralPath $extractDir -File -Recurse -ErrorAction SilentlyContinue)
                        if ($existing.Count -gt 0) {
                            $extractFileCount = $existing.Count
                            $extracted = $extractDir
                            $status = 'Existing extraction (skipped; use -Overwrite to rerun)'
                        }
                    }
                    if (-not $extracted) {
                        if ((Test-Path -LiteralPath $extractDir) -and $Overwrite) { Remove-Item -LiteralPath $extractDir -Recurse -Force }
                        New-Item -ItemType Directory -Path $extractDir -Force | Out-Null
                        Invoke-External $ExtractorExe $argsList
                        $produced = @(Get-ChildItem -LiteralPath $extractDir -File -Recurse -ErrorAction SilentlyContinue)
                        $extractFileCount = $produced.Count
                        if ($extractFileCount -eq 0) { throw 'Extractor returned successfully but produced no files' }
                        $extracted = $extractDir
                        $status = 'Extracted (shader compatibility unverified)'
                    }
                } else { $status = 'Extraction planned (WhatIf)' }
            } elseif (-not $Compile) { $status = 'Candidate (no extractor configured)' }
            if ($Compile) {
                if (-not $source) { $status = 'Unresolved: no matching .fx source' }
                elseif (-not $CompilerExe) { $status = 'Unresolved: compiler not configured' }
                else {
                    $compiled = Join-Path $destDir ($baseName + '.fxb')
                    if ((Test-Path -LiteralPath $compiled) -and -not $Overwrite) { $status = 'Existing .fxb (skipped)' }
                    else {
                        $tokens = @{input=$file.FullName; output=$compiled; source=$source; name=$baseName}
                        $argsList = Expand-Arguments $CompilerArguments $tokens
                        if ($PSCmdlet.ShouldProcess($compiled, 'Compile FX source with configured compiler')) {
                            Invoke-External $CompilerExe $argsList
                            if (-not (Test-Path -LiteralPath $compiled -PathType Leaf)) { throw 'Compiler completed without creating the requested output file' }
                            $status = 'Compiled (runtime validation still required)'
                        } else { $status = 'Compile planned (WhatIf)' }
                    }
                }
            }
        }
    } catch {
        $errorMessage = $_.Exception.Message
        $status = 'Error'
        Write-Warning ("{0}: {1}" -f $file.Name, $errorMessage)
    }
    $results.Add([pscustomobject]@{
        File=$file.FullName
        Platform=$(if ($header) { [string]$header.Platform } else { '' })
        Compression=$(if ($header) { $header.Compression } else { '' })
        HeaderValid=$(if ($header) { $header.HeaderValid } else { $false })
        EffectCandidate=$candidate
        Source=$source
        ExtractedTo=$extracted
        ExtractedFileCount=$extractFileCount
        Output=$compiled
        Status=$status
        Error=$errorMessage
    })
}
$csv = Join-Path $OutputDirectory 'effect-report.csv'
$json = Join-Path $OutputDirectory 'effect-report.json'
$results | Export-Csv -LiteralPath $csv -NoTypeInformation -Encoding UTF8
ConvertTo-Json -InputObject @($results.ToArray()) -Depth 5 | Set-Content -LiteralPath $json -Encoding UTF8
$results | Group-Object Status | Sort-Object Name | ForEach-Object { Write-Host ('{0}: {1}' -f $_.Name, $_.Count) }
Write-Host "Reports: $csv ; $json"
if ($FailOnUnresolved -and @($results | Where-Object { $_.Status -match 'Unresolved|Error|Candidate|Skipped|no extractor' }).Count -gt 0) { exit 2 }
