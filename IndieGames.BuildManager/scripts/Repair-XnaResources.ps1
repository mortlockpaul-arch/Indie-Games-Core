#Requires -Version 5.1
[CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='Medium')]
param(
    [string]$Root = 'D:\indie-game-archive',
    [string]$Game,
    [string]$ReportPath,
    [switch]$NoBackup
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-ResourceBases([string]$folder) {
    $result = New-Object 'System.Collections.Generic.HashSet[string]' ([StringComparer]::OrdinalIgnoreCase)
    $rx = 'new\s+(?:global::)?(?:System\.Resources\.)?ResourceManager\s*\(\s*@?"(?<base>[^"\r\n]+)"'
    Get-ChildItem -LiteralPath $folder -Recurse -File -Filter '*.cs' -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -notmatch '[\\/](bin|obj)[\\/]' } |
        ForEach-Object {
            $content = [IO.File]::ReadAllText($_.FullName)
            foreach ($match in [regex]::Matches($content, $rx)) {
                [void]$result.Add($match.Groups['base'].Value)
            }
        }
    return $result
}
function Get-ResourceBase([string]$filename) {
    $stem = [IO.Path]::GetFileNameWithoutExtension($filename)
    # Remove only a terminal culture suffix, preserving dotted resource base names.
    if ($stem -match '^(?<base>.+)\.(?<culture>[a-z]{2,3}(?:-[A-Za-z0-9]{2,8})*)$') {
        try {
            $ci = [Globalization.CultureInfo]::GetCultureInfo($Matches['culture'])
            if ($ci.Name -ne '') { return $Matches['base'] }
        } catch {}
    }
    return $stem
}
function Add-ResourceMetadata([xml]$xml, [string]$include, [string]$logicalName) {
    $ns = $xml.DocumentElement.NamespaceURI
    $group = $xml.CreateElement('ItemGroup', $ns)
    $item = $xml.CreateElement('EmbeddedResource', $ns)
    $item.SetAttribute('Update', $include)
    $logical = $xml.CreateElement('LogicalName', $ns)
    $logical.InnerText = $logicalName
    [void]$item.AppendChild($logical)
    [void]$group.AppendChild($item)
    [void]$xml.DocumentElement.AppendChild($group)
}

$rootPath = (Resolve-Path -LiteralPath $Root).Path
$projects = @(Get-ChildItem -LiteralPath $rootPath -Recurse -File -Filter '*.csproj' |
    Where-Object { $_.FullName -notmatch '[\\/](bin|obj)[\\/]' -and
                   (!$Game -or $_.FullName.IndexOf($Game, [StringComparison]::OrdinalIgnoreCase) -ge 0) })
$rows = New-Object 'System.Collections.Generic.List[object]'
foreach ($project in $projects) {
    try {
        [xml]$xml = [IO.File]::ReadAllText($project.FullName)
        $projectFolder = $project.DirectoryName
        $bases = Get-ResourceBases $projectFolder
        $resxFiles = @(Get-ChildItem -LiteralPath $projectFolder -File -Filter '*.resx')
        $modified = $false
        foreach ($resx in $resxFiles) {
            $stem = [IO.Path]::GetFileNameWithoutExtension($resx.Name)
            $base = Get-ResourceBase $resx.Name
            if ($base -ne $stem) { continue } # culture-specific resources: preserve satellite handling
            if (!$bases.Contains($base)) { continue } # only repair confirmed ResourceManager bases
            $expected = "$base.resources"
            $items = @($xml.SelectNodes('//*[local-name()="EmbeddedResource"]'))
            $existing = $null
            foreach ($item in $items) {
                $path = $item.GetAttribute('Include')
                if (!$path) { $path = $item.GetAttribute('Update') }
                if ($path -and ($path.Replace('/', '\') -ieq $resx.Name)) { $existing = $item; break }
            }
            $current = $null
            if ($null -ne $existing) {
                $logicalNode = $existing.SelectSingleNode('*[local-name()="LogicalName"]')
                if ($null -ne $logicalNode) { $current = $logicalNode.InnerText }
            }
            if ($current -ceq $expected) { $status = 'AlreadyCorrect' }
            else {
                $status = 'WouldRepair'
                if ($PSCmdlet.ShouldProcess($project.FullName, "Set $($resx.Name) LogicalName=$expected")) {
                    if ($null -eq $existing) {
                        Add-ResourceMetadata $xml $resx.Name $expected
                    } else {
                        $logicalNode = $existing.SelectSingleNode('*[local-name()="LogicalName"]')
                        if ($null -eq $logicalNode) {
                            $logicalNode = $xml.CreateElement('LogicalName', $xml.DocumentElement.NamespaceURI)
                            [void]$existing.AppendChild($logicalNode)
                        }
                        $logicalNode.InnerText = $expected
                    }
                    $modified = $true
                    $status = 'Repaired'
                }
            }
            $rows.Add([pscustomobject]@{Project=$project.FullName; Resource=$resx.Name; Expected=$expected; Status=$status})
        }
        if ($modified) {
            if (!$NoBackup) {
                $backup = "$($project.FullName).resources.$(Get-Date -Format 'yyyyMMdd-HHmmss-fff').bak"
                Copy-Item -LiteralPath $project.FullName -Destination $backup -ErrorAction Stop
            }
            $settings = New-Object System.Xml.XmlWriterSettings
            $settings.Indent = $true
            $settings.OmitXmlDeclaration = ($xml.FirstChild.NodeType -ne [Xml.XmlNodeType]::XmlDeclaration)
            $writer = [Xml.XmlWriter]::Create($project.FullName, $settings)
            try { $xml.Save($writer) } finally { $writer.Dispose() }
        }
    } catch {
        Write-Warning "Failed $($project.FullName): $_"
        $rows.Add([pscustomobject]@{Project=$project.FullName; Resource=''; Expected=''; Status="Error: $_"})
    }
}
if (!$ReportPath) { $ReportPath = Join-Path $rootPath 'XnaResourceRepair-report.csv' }
if ($PSCmdlet.ShouldProcess($ReportPath, 'Write resource repair CSV report')) {
    if ($rows.Count) { $rows | Export-Csv -LiteralPath $ReportPath -NoTypeInformation -Encoding UTF8 }
    else { 'Project,Resource,Expected,Status' | Set-Content -LiteralPath $ReportPath -Encoding UTF8 }
}
$rows | Format-Table Resource,Expected,Status,Project -AutoSize
Write-Host "Projects scanned: $($projects.Count); resources matched: $($rows.Count)"
