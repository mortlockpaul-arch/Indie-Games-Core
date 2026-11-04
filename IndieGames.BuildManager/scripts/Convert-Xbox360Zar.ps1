#Requires -Version 5.1
[CmdletBinding()]
param(
    [string]$RomRoot = 'D:\roms\xbox360',
    [string]$OutputRoot = '',
    [string]$ToolPath = '',
    [string]$ReportRoot = '',
    [switch]$Execute,
    [switch]$Cleanup,
    [switch]$SkipDownload,
    [switch]$SkipDeepVerify,
    [ValidateRange(1,10)][int]$VerifyAttempts = 2
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'

function Format-Bytes([double]$Bytes) {
    if ($Bytes -ge 1TB) { return ('{0:N2} TB' -f ($Bytes / 1TB)) }
    if ($Bytes -ge 1GB) { return ('{0:N2} GB' -f ($Bytes / 1GB)) }
    if ($Bytes -ge 1MB) { return ('{0:N2} MB' -f ($Bytes / 1MB)) }
    if ($Bytes -ge 1KB) { return ('{0:N2} KB' -f ($Bytes / 1KB)) }
    return ('{0:N0} B' -f $Bytes)
}
function Get-FreeBytes([string]$Path) {
    $p = [IO.Path]::GetFullPath($Path)
    $root = [IO.Path]::GetPathRoot($p)
    try { return ([IO.DriveInfo]::new($root)).AvailableFreeSpace } catch { return $null }
}
function Invoke-External([string]$Executable, [string[]]$Arguments) {
    # Native stdout must not enter the PowerShell success pipeline: otherwise
    # Invoke-Verification returns an array instead of its result object.
    & $Executable @Arguments | ForEach-Object { Write-Host $_ }
    $code = $LASTEXITCODE
    if ($code -ne 0) { throw "Command failed (exit $code): $Executable $($Arguments -join ' ')" }
}
function Install-Xverter([string]$Destination) {
    $release = Invoke-RestMethod -Uri 'https://api.github.com/repos/blank-query/xverter/releases/latest' -Headers @{ 'User-Agent' = 'IndieGames-ZAR-Converter' }
    $assets = @($release.assets | Where-Object { $_.name -eq 'xverter.exe' })
    if ($assets.Count -ne 1) { throw 'No uniquely named xverter.exe release asset. Install manually and pass -ToolPath.' }
    $uri = [string]$assets[0].browser_download_url
    if (-not $uri.StartsWith('https://github.com/blank-query/xverter/releases/download/', [StringComparison]::OrdinalIgnoreCase)) { throw "Unexpected download URL: $uri" }
    $folder = Split-Path -Parent $Destination
    if (-not (Test-Path -LiteralPath $folder)) { New-Item -ItemType Directory -Path $folder -Force | Out-Null }
    $temp = "$Destination.download"
    try {
        Invoke-WebRequest -Uri $uri -OutFile $temp -UseBasicParsing
        if ((Get-Item -LiteralPath $temp).Length -lt 100KB) { throw 'Downloaded executable is unexpectedly small.' }
        Move-Item -LiteralPath $temp -Destination $Destination -Force
    } finally { if (Test-Path -LiteralPath $temp) { Remove-Item -LiteralPath $temp -Force } }
    Write-Host "Downloaded xVerter $($release.tag_name) to $Destination" -ForegroundColor Green
    Write-Warning 'Downloaded executable is third-party software. Review its publisher and release before running.'
}
function Invoke-Verification([string]$Executable, [string]$IsoPath, [bool]$Deep, [int]$Attempts) {
    $lastError = ''
    for ($attempt = 1; $attempt -le $Attempts; $attempt++) {
        # xverter uses the OS temp directory for extraction during deep verification.
        # Isolate each invocation to prevent collision with concurrent jobs.
        $scratch = Join-Path ([IO.Path]::GetTempPath()) ('IndieGames-xverter-' + [guid]::NewGuid().ToString('N'))
        New-Item -ItemType Directory -Path $scratch -Force | Out-Null
        $oldTemp = $env:TEMP
        $oldTmp = $env:TMP
        try {
            $env:TEMP = $scratch
            $env:TMP = $scratch
            $arguments = @('verify', $IsoPath)
            if ($Deep) { $arguments += '--deep' }
            $arguments += '--no-lookup'
            Write-Host "Verifying ($(if ($Deep) { 'deep' } else { 'standard' })) attempt $attempt/$Attempts : $IsoPath"
            Invoke-External $Executable $arguments
            return [pscustomobject]@{ Passed = $true; Error = ''; Attempts = $attempt }
        } catch {
            $lastError = $_.Exception.Message
            Write-Warning "Verification attempt $attempt/$Attempts failed: $lastError"
        } finally {
            $env:TEMP = $oldTemp
            $env:TMP = $oldTmp
            try { Remove-Item -LiteralPath $scratch -Recurse -Force -ErrorAction Stop } catch { Write-Warning "Could not remove temporary folder: $scratch ($($_.Exception.Message))" }
        }
    }
    return [pscustomobject]@{ Passed = $false; Error = $lastError; Attempts = $Attempts }
}

$romFull = [IO.Path]::GetFullPath($RomRoot).TrimEnd('\')
if (-not (Test-Path -LiteralPath $romFull -PathType Container)) { throw "ROM directory not found: $romFull" }
if (-not $OutputRoot) { $OutputRoot = $romFull }
$outFull = [IO.Path]::GetFullPath($OutputRoot).TrimEnd('\')
if ($outFull -ne $romFull -and $outFull.StartsWith($romFull + '\', [StringComparison]::OrdinalIgnoreCase)) { throw 'OutputRoot cannot be inside RomRoot.' }
if (-not $ReportRoot) { $ReportRoot = Join-Path $env:TEMP ('Xbox360-ZAR-Report-' + (Get-Date -Format 'yyyyMMdd-HHmmss')) }
if (-not $ToolPath) { $ToolPath = Join-Path $env:LOCALAPPDATA 'IndieGames\Tools\xverter.exe' }
$toolFull = [IO.Path]::GetFullPath($ToolPath)
$files = @(Get-ChildItem -LiteralPath $romFull -Filter '*.zar' -File -Recurse | Sort-Object FullName)
$rows = New-Object 'System.Collections.Generic.List[object]'
[long]$inputTotal = 0
[long]$outputTotal = 0
[long]$deletedTotal = 0
[int]$success = 0
[int]$failed = 0
[int]$skipped = 0
$freeBefore = Get-FreeBytes $outFull
Write-Host "Mode: $(if ($Execute) { 'EXECUTE' } else { 'DRY RUN' })"
Write-Host "Input: $romFull"
Write-Host "Output: $outFull"
Write-Host "Found $($files.Count) ZAR file(s)."
if ($Cleanup -and -not $Execute) { Write-Warning 'Cleanup is disabled in dry-run mode.' }
if ($Execute -and -not (Test-Path -LiteralPath $toolFull -PathType Leaf)) {
    if ($SkipDownload) { throw "Tool missing: $toolFull" }
    Install-Xverter $toolFull
}
foreach ($file in $files) {
    $relative = $file.FullName.Substring($romFull.Length).TrimStart('\')
    $target = Join-Path $outFull ([IO.Path]::ChangeExtension($relative, '.iso'))
    $partial = [IO.Path]::ChangeExtension($target, '.partial.iso')
    # Compatibility with the original script's .iso.partial.iso filename.
    $legacyPartial = "$target.partial.iso"
    $status = 'Planned'
    $conversionStatus = 'Not run'
    $standardStatus = 'Not run'
    $deepStatus = 'Not run'
    $errorText = ''
    [long]$isoBytes = 0
    [long]$deletedBytes = 0
    $inputTotal += $file.Length
    if (Test-Path -LiteralPath $target -PathType Leaf) {
        $status = 'Skipped: destination exists'
        $errorText = 'Existing ISO was not verified by this run.'
        $skipped++
    } elseif ($Execute) {
        try {
            $parent = Split-Path -Parent $target
            if (-not (Test-Path -LiteralPath $parent)) { New-Item -ItemType Directory -Path $parent -Force | Out-Null }
            if (Test-Path -LiteralPath $partial -PathType Leaf) {
                $conversionStatus = 'Resumed existing partial (not independently conversion-verified)'
                Write-Host "Resuming verification: $partial" -ForegroundColor Cyan
            } elseif (Test-Path -LiteralPath $legacyPartial -PathType Leaf) {
                $partial = $legacyPartial
                $conversionStatus = 'Resumed legacy partial (not independently conversion-verified)'
                Write-Host "Resuming legacy partial: $partial" -ForegroundColor Cyan
            } else {
                Write-Host "Converting: $relative" -ForegroundColor Cyan
                Invoke-External $toolFull @('convert', $file.FullName, '-o', $partial)
                $conversionStatus = 'Completed'
            }
            if (-not (Test-Path -LiteralPath $partial -PathType Leaf)) { throw 'No partial ISO exists after conversion.' }
            $isoBytes = (Get-Item -LiteralPath $partial).Length
            if ($isoBytes -le 0) { throw 'Partial ISO is empty.' }
            $standard = Invoke-Verification $toolFull $partial $false $VerifyAttempts
            if (-not $standard.Passed) { $standardStatus = 'Failed'; throw "Standard verification failed: $($standard.Error)" }
            $standardStatus = 'Passed'
            if (-not $SkipDeepVerify) {
                $deep = Invoke-Verification $toolFull $partial $true $VerifyAttempts
                if (-not $deep.Passed) { $deepStatus = 'Failed'; throw "Deep verification failed: $($deep.Error)" }
                $deepStatus = 'Passed'
            } else { $deepStatus = 'Skipped by request' }
            Move-Item -LiteralPath $partial -Destination $target -ErrorAction Stop
            $status = if ($SkipDeepVerify) { 'Converted, standard verified (deep skipped)' } else { 'Converted and deep verified' }
            $success++
            $outputTotal += $isoBytes
            if ($Cleanup) {
                Remove-Item -LiteralPath $file.FullName -Force -ErrorAction Stop
                $deletedBytes = $file.Length
                $deletedTotal += $deletedBytes
                $status += ', source deleted'
            }
        } catch {
            $errorText = $_.Exception.Message
            $status = if ($conversionStatus -eq 'Not run') { 'Conversion failed' } elseif ($standardStatus -eq 'Failed') { 'Standard verification failed' } elseif ($deepStatus -eq 'Failed') { 'Deep verification failed' } else { 'Processing failed' }
            $failed++
            Write-Warning "$relative : $errorText"
        }
    }
    $rows.Add([pscustomobject]@{
        Source = $file.FullName
        Destination = $target
        PartialDestination = $partial
        SourceBytes = [long]$file.Length
        IsoBytes = $isoBytes
        DeletedSourceBytes = $deletedBytes
        Conversion = $conversionStatus
        StandardVerification = $standardStatus
        DeepVerification = $deepStatus
        Status = $status
        Error = $errorText
    })
}
if (-not (Test-Path -LiteralPath $ReportRoot)) { New-Item -ItemType Directory -Path $ReportRoot -Force | Out-Null }
$csvPath = Join-Path $ReportRoot 'conversion-report.csv'
$jsonPath = Join-Path $ReportRoot 'summary.json'
$rows | Export-Csv -LiteralPath $csvPath -NoTypeInformation -Encoding UTF8
$freeAfter = Get-FreeBytes $outFull
$summary = [ordered]@{
    Timestamp = (Get-Date).ToString('o')
    Mode = $(if ($Execute) { 'Execute' } else { 'DryRun' })
    CleanupEnabled = [bool]$Cleanup
    DeepVerificationEnabled = -not [bool]$SkipDeepVerify
    VerificationAttempts = $VerifyAttempts
    RomRoot = $romFull
    OutputRoot = $outFull
    ZarCount = $files.Count
    SuccessfulConversions = $success
    FailedConversions = $failed
    SkippedExisting = $skipped
    InputZarBytes = $inputTotal
    CreatedIsoBytes = $outputTotal
    DeletedZarBytes = $deletedTotal
    OutputDriveFreeBeforeBytes = $freeBefore
    OutputDriveFreeAfterBytes = $freeAfter
    CsvReport = $csvPath
}
$summary | ConvertTo-Json -Depth 5 | Set-Content -LiteralPath $jsonPath -Encoding UTF8
Write-Host ''
Write-Host "ZAR files scanned: $($files.Count) | Verified: $success | Failed: $failed | Skipped: $skipped"
Write-Host "Total ZAR size scanned: $(Format-Bytes $inputTotal)"
Write-Host "ISO bytes finalized: $(Format-Bytes $outputTotal)"
Write-Host "Source bytes deleted: $(Format-Bytes $deletedTotal)"
if ($null -ne $freeBefore) { Write-Host "Output drive free before: $(Format-Bytes $freeBefore)" }
if ($null -ne $freeAfter) { Write-Host "Output drive free after: $(Format-Bytes $freeAfter)" }
Write-Host "CSV: $csvPath"
Write-Host "JSON: $jsonPath"
if ($failed -gt 0) { exit 2 }
