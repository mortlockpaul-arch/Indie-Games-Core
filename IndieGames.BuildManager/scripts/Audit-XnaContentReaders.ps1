[CmdletBinding()]
param(
    [string]$ArchiveRoot = 'D:\indie-game-archive',
    [string]$Report = (Join-Path $PWD 'XnaContentReaderAudit.csv')
)
$ErrorActionPreference = 'Stop'
function Read-7BitInt([System.IO.BinaryReader]$Reader) {
    $value = 0
    for ($i = 0; $i -lt 5; $i++) {
        $b = [int]$Reader.ReadByte()
        if ($i -eq 4 -and ($b -band 0xF0)) { throw 'Invalid 7-bit integer' }
        $value = $value -bor (($b -band 0x7f) -shl (7 * $i))
        if (($b -band 0x80) -eq 0) { return $value }
    }
    throw 'Invalid 7-bit integer'
}
function Read-XnbString([System.IO.BinaryReader]$Reader) {
    $length = Read-7BitInt $Reader
    if ($length -lt 0 -or $length -gt 1048576 -or $length -gt ($Reader.BaseStream.Length - $Reader.BaseStream.Position)) { throw 'Invalid reader string length' }
    $bytes = $Reader.ReadBytes($length)
    return ([System.Text.UTF8Encoding]::new($false, $true)).GetString($bytes)
}
$rows = [System.Collections.Generic.List[object]]::new()
$games = Get-ChildItem -LiteralPath $ArchiveRoot -Directory
foreach ($game in $games) {
    $projects = @(Get-ChildItem -LiteralPath $game.FullName -Filter '*.csproj' -File -Recurse | Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' })
    $assemblyNames = @()
    foreach ($project in $projects) {
        try {
            [xml]$xml = Get-Content -LiteralPath $project.FullName -Raw
            $assembly = [string]$xml.Project.PropertyGroup.AssemblyName | Select-Object -First 1
            if (-not $assembly) { $assembly = [System.IO.Path]::GetFileNameWithoutExtension($project.Name) }
            $assemblyNames += $assembly
        } catch { Write-Warning "Cannot parse project $($project.FullName): $_" }
    }
    $readers = @{}
    foreach ($source in (Get-ChildItem -LiteralPath $game.FullName -Filter '*.cs' -File -Recurse | Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' })) {
        $content = [System.IO.File]::ReadAllText($source.FullName)
        $ns = [regex]::Match($content, '(?m)^\s*namespace\s+([\w.]+)\s*[;{]')
        if (-not $ns.Success) { continue }
        foreach ($match in [regex]::Matches($content, '\bclass\s+(\w+)\s*:\s*(?:[\w.]+\.)?ContentTypeReader\s*<')) {
            $type = $ns.Groups[1].Value + '.' + $match.Groups[1].Value
            $readers[$type] = $source.FullName
        }
    }
    foreach ($file in (Get-ChildItem -LiteralPath $game.FullName -Filter '*.xnb' -File -Recurse | Where-Object { $_.FullName -notmatch '[\\/](obj|bin|Content-Patched|XnaRedirect-Test)[\\/]' })) {
        $stream = $null; $br = $null
        try {
            $stream = [System.IO.File]::OpenRead($file.FullName)
            $br = [System.IO.BinaryReader]::new($stream, [System.Text.Encoding]::UTF8, $true)
            if ($stream.Length -lt 10 -or $br.ReadByte() -ne 88 -or $br.ReadByte() -ne 78 -or $br.ReadByte() -ne 66) { continue }
            $null = $br.ReadByte(); $null = $br.ReadByte()
            $flags = $br.ReadByte()
            $declared = $br.ReadInt32()
            if (($flags -band 0xC0) -ne 0) {
                $rows.Add([pscustomobject]@{ Game=$game.Name; XNB=$file.FullName; Reader=''; ReferencedAssembly=''; GameAssembly=($assemblyNames -join ';'); Status='COMPRESSED_NOT_INSPECTED'; Detail='Requires XNB decompression' })
                continue
            }
            if ($declared -ne $stream.Length) { throw "Header size $declared differs from file length $($stream.Length)" }
            $count = Read-7BitInt $br
            if ($count -gt 4096) { throw 'Invalid reader count' }
            for ($i=0; $i -lt $count; $i++) {
                $identity = Read-XnbString $br
                $null = $br.ReadInt32()
                $parts = $identity -split '\s*,\s*', 3
                if ($parts.Count -lt 2) { continue }
                $type = $parts[0]; $refAssembly = $parts[1]
                if ($readers.ContainsKey($type)) {
                    $status = if ($assemblyNames -contains $refAssembly) { 'LIKELY_OK' } else { 'POSSIBLE_MISMATCH' }
                    $detail = $readers[$type]
                } else {
                    # A reader absent from source may legitimately live in a referenced DLL.
                    $status = 'READER_NOT_IN_SOURCE'; $detail = 'Check referenced DLLs before treating as broken'
                }
                if ($type -match '^(Microsoft|System)\.') { continue }
                $rows.Add([pscustomobject]@{ Game=$game.Name; XNB=$file.FullName; Reader=$type; ReferencedAssembly=$refAssembly; GameAssembly=($assemblyNames -join ';'); Status=$status; Detail=$detail })
            }
        } catch {
            $rows.Add([pscustomobject]@{ Game=$game.Name; XNB=$file.FullName; Reader=''; ReferencedAssembly=''; GameAssembly=($assemblyNames -join ';'); Status='PARSE_ERROR'; Detail=$_.Exception.Message })
        } finally {
            if ($br) { $br.Dispose() }
            if ($stream) { $stream.Dispose() }
        }
    }
}
$rows | Export-Csv -LiteralPath $Report -NoTypeInformation -Encoding UTF8
$rows | Group-Object Status | Sort-Object Name | Select-Object Name,Count | Format-Table -AutoSize
Write-Host "Report: $Report" -ForegroundColor Cyan
$rows | Where-Object Status -eq 'POSSIBLE_MISMATCH' | Select-Object Game,Reader,ReferencedAssembly,GameAssembly -Unique | Format-Table -AutoSize
