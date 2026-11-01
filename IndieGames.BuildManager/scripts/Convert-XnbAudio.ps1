#requires -Version 5.1
[CmdletBinding(SupportsShouldProcess)]
param(
    [Parameter(Mandatory)][string]$ContentDirectory,
    [Parameter(Mandatory)][string]$ExtractorExe,
    [Parameter(Mandatory)][string]$FfmpegExe,
    [switch]$Install
)
$ErrorActionPreference = 'Stop'
foreach ($path in @($ContentDirectory, $ExtractorExe, $FfmpegExe)) {
    if (-not (Test-Path -LiteralPath $path)) { throw "Not found: $path" }
}
$ContentDirectory = (Get-Item -LiteralPath $ContentDirectory).FullName
$work = Join-Path (Split-Path -Parent $ContentDirectory) ('audio-conversion\' + [guid]::NewGuid().ToString('N'))
$results = @()

function Write-PcmXnb([string]$Wave, [string]$Output, [int]$LoopStart, [int]$LoopLength) {
    $reader = [IO.BinaryReader]::new([IO.File]::OpenRead($Wave))
    try {
        if ([Text.Encoding]::ASCII.GetString($reader.ReadBytes(4)) -ne 'RIFF') { throw 'Not RIFF audio' }
        $null = $reader.ReadUInt32()
        if ([Text.Encoding]::ASCII.GetString($reader.ReadBytes(4)) -ne 'WAVE') { throw 'Not WAVE audio' }
        $format = $null; $data = $null
        while ($reader.BaseStream.Position + 8 -le $reader.BaseStream.Length) {
            $id = [Text.Encoding]::ASCII.GetString($reader.ReadBytes(4))
            $size = $reader.ReadUInt32()
            if ($size -gt $reader.BaseStream.Length - $reader.BaseStream.Position) { throw 'Truncated WAV chunk' }
            $chunk = $reader.ReadBytes([int]$size)
            if ($id -eq 'fmt ') { $format = $chunk }
            if ($id -eq 'data') { $data = $chunk }
            if ($size % 2) { $null = $reader.ReadByte() }
        }
    } finally { $reader.Dispose() }
    if ($null -eq $format -or $null -eq $data -or $format.Length -lt 16 -or [BitConverter]::ToUInt16($format, 0) -ne 1) { throw 'Expected PCM WAV' }
    $rate = [BitConverter]::ToUInt32($format, 4)
    $align = [BitConverter]::ToUInt16($format, 12)
    $samples = [int]($data.Length / $align)
    if ($LoopStart -lt 0 -or $LoopLength -lt 0 -or [long]$LoopStart + $LoopLength -gt $samples) { throw 'Loop metadata exceeds decoded samples' }
    $stream = [IO.MemoryStream]::new()
    $writer = [IO.BinaryWriter]::new($stream)
    try {
        $writer.Write([byte[]]@(88,78,66,119,5,0))
        $writer.Write([int]0)
        $writer.Write([byte]1)
        $writer.Write('Microsoft.Xna.Framework.Content.SoundEffectReader, Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553')
        $writer.Write([int]0)
        $writer.Write([byte]0)
        $writer.Write([byte]1)
        $writer.Write([int]16)
        $writer.Write([byte[]]$format[0..15])
        $writer.Write([int]$data.Length)
        $writer.Write([byte[]]$data)
        $writer.Write([int]$LoopStart)
        $writer.Write([int]$LoopLength)
        $writer.Write([int][Math]::Round(1000.0 * $samples / $rate))
        $writer.Flush()
        $length = $stream.Length
        $stream.Position = 6
        $writer.Write([int]$length)
        $writer.Flush()
        [IO.File]::WriteAllBytes($Output, $stream.ToArray())
    } finally { $writer.Dispose(); $stream.Dispose() }
}

$files = @(foreach ($folder in @('Songs', 'Sounds')) {
    $directory = Join-Path $ContentDirectory $folder
    if (Test-Path -LiteralPath $directory) { Get-ChildItem -LiteralPath $directory -Filter '*.xnb' -File -Recurse }
})
# Root-level XNA audio assets are common too. Only select known audio reader
# names from uncompressed headers; do not send unrelated textures/fonts here.
$files += @(Get-ChildItem -LiteralPath $ContentDirectory -Filter '*.xnb' -File | Where-Object {
    $bytes = [IO.File]::ReadAllBytes($_.FullName)
    $bytes.Length -ge 10 -and ($bytes[5] -band 0xC0) -eq 0 -and
        [Text.Encoding]::UTF8.GetString($bytes) -match 'Microsoft\.Xna\.Framework\.Content\.(Song|SoundEffect)Reader'
})
foreach ($file in $files) {
    $relative = $file.FullName.Substring($ContentDirectory.Length).TrimStart('\','/')
    if (-not $PSCmdlet.ShouldProcess($file.FullName, $(if ($Install) { 'Convert audio and install with backups' } else { 'Convert audio to staging directory' }))) { continue }
    try {
        $directory = Join-Path $work ([IO.Path]::GetDirectoryName($relative))
        New-Item -ItemType Directory -Force -Path $directory | Out-Null
        $log = Join-Path $directory ($file.BaseName + '.log')
        # Extractor can report success despite an asset failure: require actual outputs below.
        & $ExtractorExe --input $file.FullName --output $directory --parser --extract --convert-to-windows *> $log
        if ($LASTEXITCODE -ne 0) { throw "Extractor failed; see $log" }
        $xnb = Join-Path $directory $file.Name
        $original = [IO.File]::ReadAllBytes($file.FullName)
        if ($original.Length -lt 22 -or [Text.Encoding]::ASCII.GetString($original,0,3) -ne 'XNB') { throw 'Invalid source XNB' }
        $outputs = @($xnb)
        if ($relative -match '^Sounds[\\/]' -or [Text.Encoding]::UTF8.GetString($original) -match 'Microsoft\.Xna\.Framework\.Content\.SoundEffectReader') {
            $wave = Join-Path $directory ($file.BaseName + '.wav')
            if (-not (Test-Path -LiteralPath $wave)) { throw "Extractor did not export audio; see $log" }
            $pcm = Join-Path $directory ($file.BaseName + '.pcm.wav')
            & $FfmpegExe -nostdin -hide_banner -loglevel error -i $wave -vn -c:a pcm_s16le -y $pcm 2>> $log
            if ($LASTEXITCODE -ne 0) { throw "PCM decoding failed; see $log" }
            if (($original[5] -band 0xC0) -ne 0) { throw 'Compressed sound loop metadata requires extractor support; source preserved' }
            $loopStart = [BitConverter]::ToInt32($original, $original.Length - 12)
            $loopLength = [BitConverter]::ToInt32($original, $original.Length - 8)
            Write-PcmXnb $pcm $xnb $loopStart $loopLength
        } else {
            if (-not (Test-Path -LiteralPath $xnb)) { throw "Extractor did not produce a Windows XNB; see $log" }
            $bytes = [IO.File]::ReadAllBytes($xnb)
            $reader = [IO.BinaryReader]::new([IO.MemoryStream]::new($bytes))
            try {
                $reader.BaseStream.Position = 10
                if ($reader.ReadByte() -ne 1) { throw 'Unexpected song reader table' }
                $null = $reader.ReadString(); $null = $reader.ReadInt32()
                if ($reader.ReadByte() -ne 0 -or $reader.ReadByte() -ne 1) { throw 'Unexpected song object table' }
                $reference = $reader.ReadString()
            } finally { $reader.Dispose() }
            $audio = Join-Path $file.DirectoryName $reference
            if (-not (Test-Path -LiteralPath $audio)) { throw "Referenced song audio missing: $audio" }
            $ogg = Join-Path $directory ($file.BaseName + '.ogg')
            & $FfmpegExe -nostdin -hide_banner -loglevel error -i $audio -vn -c:a libvorbis -q:a 5 -y $ogg 2>> $log
            if ($LASTEXITCODE -ne 0) { throw "Ogg encoding failed; see $log" }
            # Song duration is a trailing LE Int32 even in Xbox XNB 3–5.
            if ($original[4] -ge 3 -and $original[4] -le 5 -and ($original[5] -band 0xC0) -eq 0) {
                [Array]::Copy($original,$original.Length-4,$bytes,$bytes.Length-4,4)
                [IO.File]::WriteAllBytes($xnb,$bytes)
            }
            # Match the referenced filename, which need not match the asset name.
            $oggName = [IO.Path]::GetFileNameWithoutExtension($reference) + '.ogg'
            if ([IO.Path]::GetFileName($ogg) -cne $oggName) {
                $renamed = Join-Path $directory $oggName
                Move-Item -LiteralPath $ogg -Destination $renamed
                $ogg = $renamed
            }
            if ($reference -match '[\\/]') { throw 'Song references in subdirectories require manual deployment; source preserved' }
            $outputs += $ogg
        }
        $header = [IO.File]::ReadAllBytes($xnb)
        if ($header[3] -ne 119 -or $header[4] -ne 5 -or [BitConverter]::ToInt32($header,6) -ne $header.Length) { throw 'Invalid converted Windows XNB' }
        if ($Install) {
            foreach ($output in $outputs) {
                $destination = Join-Path $file.DirectoryName ([IO.Path]::GetFileName($output))
                if (Test-Path -LiteralPath $destination) {
                    $backup = $destination + '.before-audio-conversion.bak'
                    if (-not (Test-Path -LiteralPath $backup)) { Copy-Item -LiteralPath $destination -Destination $backup }
                }
                Copy-Item -LiteralPath $output -Destination $destination -Force
            }
        }
        Write-Host "Converted: $relative"
        $results += [pscustomobject]@{ Asset=$relative; Status='Converted'; Error='' }
    } catch {
        Write-Warning "${relative}: $_"
        $results += [pscustomobject]@{ Asset=$relative; Status='Failed'; Error=$_.Exception.Message }
    }
}
if ($results.Count) {
    $results | Export-Csv -LiteralPath (Join-Path $work 'report.csv') -NoTypeInformation
    Write-Host "Report: $work\report.csv"
}
if (@($results | Where-Object Status -eq 'Failed').Count) { exit 1 }
Write-Host "Converted $($results.Count) audio assets. Rebuild the game to copy installed content to bin."
