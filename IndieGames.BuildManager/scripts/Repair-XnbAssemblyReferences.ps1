#requires -Version 5.1
[CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='Medium')]
param(
    [Parameter(Mandatory=$true)][string]$Root,
    [string]$Game,
    [string]$ReportPath,
    [switch]$NoBackup
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

# XNB reader declarations are length-prefixed UTF-8 strings. Only uncompressed XNBs
# are edited; compressed XNBs require a dedicated XNB decompressor/recompressor.
function Read-7BitInt([System.IO.BinaryReader]$Reader) {
    [long]$value = 0
    for ($i = 0; $i -lt 5; $i++) {
        $b = [int]$Reader.ReadByte()
        $value = $value -bor (([long]($b -band 127)) -shl (7 * $i))
        if (($b -band 128) -eq 0) {
            if ($value -gt [int]::MaxValue) { throw '7-bit integer exceeds Int32' }
            return [int]$value
        }
    }
    throw 'Invalid 7-bit encoded integer'
}
function Write-7BitInt([System.IO.BinaryWriter]$Writer, [int]$Value) {
    [uint32]$v = [uint32]$Value
    while ($v -ge 128) {
        $Writer.Write([byte](($v -band 127) -bor 128))
        $v = $v -shr 7
    }
    $Writer.Write([byte]$v)
}
function Get-AssemblyName([string]$Project) {
    [xml]$xml = Get-Content -LiteralPath $Project -Raw
    $node = $xml.SelectSingleNode('//*[local-name()="AssemblyName"]')
    if ($null -ne $node -and -not [string]::IsNullOrWhiteSpace($node.InnerText) -and $node.InnerText -notmatch '\$\(') {
        return $node.InnerText.Trim()
    }
    return [IO.Path]::GetFileNameWithoutExtension($Project)
}
function Get-SourceTypes([string]$ProjectDir) {
    $types = @{}
    $files = @(Get-ChildItem -LiteralPath $ProjectDir -Recurse -File -Filter '*.cs' | Where-Object {
        $_.FullName -notmatch '[\\/](obj|bin|\.git)[\\/]'
    })
    foreach ($file in $files) {
        $content = Get-Content -LiteralPath $file.FullName -Raw
        # Conservative: recognize namespace + class in ordinary decompiled XBLIG source.
        $ns = [regex]::Match($content, '(?m)^\s*namespace\s+([\w.]+)\s*[;{]')
        if (-not $ns.Success) { continue }
        foreach ($m in [regex]::Matches($content, '\b(?:class|struct)\s+([A-Za-z_]\w*)\b')) {
            $full = $ns.Groups[1].Value + '.' + $m.Groups[1].Value
            if (-not $types.ContainsKey($full)) { $types[$full] = @() }
            $types[$full] = @($types[$full]) + $file.FullName
        }
    }
    return $types
}
function Get-ReaderTypeMatches([string]$ReaderName) {
    # Only handle unambiguous generic type references with a simple assembly name.
    # The entire reader string is otherwise preserved byte-for-byte when rewritten.
    $pattern = '(?<type>[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)+),\s*(?<assembly>[A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*)(?=\s*(?:\]|,|$))'
    return @([regex]::Matches($ReaderName, $pattern))
}
function Repair-XnbFile([string]$Path, [hashtable]$Types, [string]$AssemblyName, [string]$ProjectDir) {
    $record = [ordered]@{ File=$Path; Status=''; Changes=''; Detail='' }
    try {
        $bytes = [IO.File]::ReadAllBytes($Path)
        if ($bytes.Length -lt 11) { throw 'Too short for an XNB header' }
        if ([Text.Encoding]::ASCII.GetString($bytes,0,3) -cne 'XNB') { throw 'Not an XNB file' }
        $flags = [int]$bytes[5]
        if (($flags -band 0xC0) -ne 0) {
            $record.Status = 'SkippedCompressed'
            $record.Detail = 'XNB uses LZX/LZ4 compression; no unsafe byte edits performed'
            return [pscustomobject]$record
        }
        $size = [BitConverter]::ToInt32($bytes, 6)
        if ($size -ne $bytes.Length) { throw "Header length $size differs from actual $($bytes.Length)" }
        $ms = New-Object IO.MemoryStream(,$bytes)
        $br = New-Object IO.BinaryReader($ms, [Text.Encoding]::UTF8)
        try {
            $ms.Position = 10
            $count = Read-7BitInt $br
            if ($count -lt 0 -or $count -gt 10000) { throw 'Invalid reader count' }
            $replacements = @()
            $issues = @()
            for ($i=0; $i -lt $count; $i++) {
                $start = [int]$ms.Position
                $n = Read-7BitInt $br
                if ($n -lt 0 -or $n -gt ($ms.Length - $ms.Position)) { throw 'Invalid reader string length' }
                $strBytes = $br.ReadBytes($n)
                $readerName = (New-Object Text.UTF8Encoding($false, $true)).GetString($strBytes)
                $end = [int]$ms.Position
                $newName = $readerName
                foreach ($match in (Get-ReaderTypeMatches $readerName)) {
                    $type = $match.Groups['type'].Value
                    $oldAssembly = $match.Groups['assembly'].Value
                    if (-not $Types.ContainsKey($type) -or $oldAssembly -eq $AssemblyName) { continue }
                    # Avoid replacing framework/system names and ambiguous multi-file types.
                    if ($oldAssembly -match '^(Microsoft\.|System\.|mscorlib$|netstandard$|FNA$)' -or @($Types[$type]).Count -ne 1) { continue }
                    # Only change a reference if the named original assembly is not
                    # provided as a DLL within this project's source/output tree.
                    $originalDll = @(Get-ChildItem -LiteralPath $ProjectDir -Filter "$oldAssembly.dll" -Recurse -File -ErrorAction SilentlyContinue |
                        Where-Object { $_.FullName -notmatch '[\\/]obj[\\/]' })
                    if ($originalDll.Count -gt 0) {
                        $issues += "$type references $oldAssembly, but $oldAssembly.dll exists; manual review"
                        continue
                    }
                    $oldText = "$type, $oldAssembly"
                    $newText = "$type, $AssemblyName"
                    $newName = $newName.Replace($oldText, $newText)
                    if ($newName -ne $readerName) { $issues += "$oldText -> $newText" }
                }
                if ($newName -ne $readerName) {
                    $replacements += [pscustomobject]@{ Start=$start; End=$end; Value=$newName }
                }
                [void]$br.ReadInt32() # content reader version
            }
            if ($replacements.Count -eq 0) {
                $record.Status = if ($issues.Count) { 'NeedsReview' } else { 'NoChange' }
                $record.Detail = $issues -join '; '
                return [pscustomobject]$record
            }
            $out = New-Object IO.MemoryStream
            $bw = New-Object IO.BinaryWriter($out, [Text.Encoding]::UTF8)
            try {
                $cursor = 0
                foreach ($replacement in $replacements) {
                    $bw.Write($bytes, $cursor, ($replacement.Start - $cursor))
                    $newBytes = [Text.Encoding]::UTF8.GetBytes($replacement.Value)
                    Write-7BitInt $bw $newBytes.Length
                    $bw.Write($newBytes)
                    $cursor = $replacement.End
                }
                $bw.Write($bytes, $cursor, ($bytes.Length - $cursor))
                $bw.Flush()
                $result = $out.ToArray()
                [Array]::Copy([BitConverter]::GetBytes([int]$result.Length),0,$result,6,4)
                $record.Changes = $issues -join '; '
                if ($PSCmdlet.ShouldProcess($Path, "Rewrite $($replacements.Count) XNB reader name(s)")) {
                    $backup = "$Path.xnbref.bak"
                    if (-not $NoBackup) {
                        if (Test-Path -LiteralPath $backup) { throw "Backup already exists: $backup (refusing to overwrite)" }
                        [IO.File]::Copy($Path, $backup, $false)
                    }
                    $temp = "$Path.xnbref.tmp"
                    try {
                        [IO.File]::WriteAllBytes($temp, $result)
                        # Check header before committing; preserve the original on errors.
                        if ([BitConverter]::ToInt32([IO.File]::ReadAllBytes($temp),6) -ne $result.Length) { throw 'Output validation failed' }
                        [IO.File]::Copy($temp, $Path, $true)
                        $record.Status = 'Repaired'
                    } finally {
                        if (Test-Path -LiteralPath $temp) { Remove-Item -LiteralPath $temp -Force }
                    }
                } else { $record.Status = 'WhatIf' }
            } finally { $bw.Dispose(); $out.Dispose() }
        } finally { $br.Dispose(); $ms.Dispose() }
    } catch {
        $record.Status = 'Error'
        $record.Detail = $_.Exception.Message
    }
    return [pscustomobject]$record
}

$rootPath = (Resolve-Path -LiteralPath $Root).Path
$gameDirs = if ($Game) {
    $candidate = Join-Path $rootPath $Game
    if (-not (Test-Path -LiteralPath $candidate -PathType Container)) { throw "Game folder not found: $candidate" }
    @(Get-Item -LiteralPath $candidate)
} else {
    @(Get-ChildItem -LiteralPath $rootPath -Directory)
}
$results = New-Object 'System.Collections.Generic.List[object]'
foreach ($dir in $gameDirs) {
    $projects = @(Get-ChildItem -LiteralPath $dir.FullName -Filter '*.csproj' -File)
    if ($projects.Count -ne 1) {
        Write-Warning "Skipping $($dir.Name): expected exactly one root .csproj, found $($projects.Count)"
        continue
    }
    $project = $projects[0]
    $assembly = Get-AssemblyName $project.FullName
    $types = Get-SourceTypes $dir.FullName
    $xnbs = @(Get-ChildItem -LiteralPath $dir.FullName -Recurse -File -Filter '*.xnb' | Where-Object {
        $_.FullName -notmatch '[\\/](obj|bin)[\\/]'
    })
    Write-Host "[$($dir.Name)] Assembly=$assembly; source types=$($types.Count); XNB files=$($xnbs.Count)" -ForegroundColor Cyan
    foreach ($xnb in $xnbs) {
        $r = Repair-XnbFile $xnb.FullName $types $assembly $dir.FullName
        $results.Add($r)
        if ($r.Status -notin @('NoChange')) { Write-Host "  $($r.Status): $($r.File) $($r.Changes) $($r.Detail)" }
    }
}
if (-not $ReportPath) { $ReportPath = Join-Path $rootPath 'Repair-XnbAssemblyReferences-report.csv' }
# A dry run never creates a report file.
if (-not $WhatIfPreference) {
    $results | Export-Csv -LiteralPath $ReportPath -NoTypeInformation -Encoding UTF8
    Write-Host "Report: $ReportPath"
}
$results | Group-Object Status | Sort-Object Name | ForEach-Object { Write-Host ("{0}: {1}" -f $_.Name, $_.Count) }
