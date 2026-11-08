# Restore RESX assets referenced by generated ResourceManager wrappers.
function Repair-EmbeddedResources {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    $ErrorActionPreference = 'Stop'
    foreach ($project in Get-ChildItem -LiteralPath $Root -Recurse -File -Filter '*.csproj' |
        Where-Object { $_.FullName -notmatch '[\\/](bin|obj|\.git)[\\/]' }) {
        $xml = [xml]::new(); $xml.PreserveWhitespace = $true; $xml.Load($project.FullName)
        $before = $xml.OuterXml
        $ns = $xml.DocumentElement.NamespaceURI
        $files = @(Get-ChildItem -LiteralPath $project.DirectoryName -Recurse -File |
            Where-Object { $_.FullName -notmatch '[\\/](bin|obj|\.git)[\\/]' })
        $bases = @($files | Where-Object Extension -eq '.cs' | ForEach-Object {
            $source = [IO.File]::ReadAllText($_.FullName)
            foreach ($match in [regex]::Matches($source, 'new\s+(?:System\.Resources\.)?ResourceManager\s*\(\s*"([^"]+)"')) {
                $match.Groups[1].Value
            }
        } | Sort-Object -Unique)
        $defaultProperties = @($xml.SelectNodes("//*[local-name()='EnableDefaultEmbeddedResourceItems' or local-name()='EnableDefaultItems']"))
        if (@($defaultProperties | Where-Object { $_.HasAttribute('Condition') -or $_.ParentNode.HasAttribute('Condition') }).Count) {
            Write-Warning "Resources unchanged (conditional default-item settings): $($project.FullName)"
            continue
        }
        $defaultResources = -not @($defaultProperties |
            Where-Object { $_.InnerText.Trim() -eq 'false' }).Count
        foreach ($resx in $files | Where-Object Extension -eq '.resx') {
            $stem = $resx.BaseName
            $culture = [IO.Path]::GetExtension($stem).TrimStart('.')
            $resourceCulture = ''
            if ($culture -and $culture -in [Globalization.CultureInfo]::GetCultures([Globalization.CultureTypes]::AllCultures).Name) {
                $resourceCulture = '.' + $culture
                $stem = [IO.Path]::GetFileNameWithoutExtension($stem)
            }
            $relative = $resx.FullName.Substring($project.DirectoryName.Length + 1)
            $qualified = [IO.Path]::Combine([IO.Path]::GetDirectoryName($relative), $stem).Replace('\', '.').Replace('/', '.')
            $candidates = @($bases | Where-Object { $_ -eq $stem -or $_ -eq $qualified -or $_.EndsWith('.' + $qualified, [StringComparison]::Ordinal) })
            if ($candidates.Count -ne 1) {
                Write-Warning "Resource unchanged (no unique ResourceManager name): $($resx.FullName)"
                continue
            }
            $logicalName = $candidates[0] + $resourceCulture + '.resources'
            $items = @($xml.SelectNodes("//*[local-name()='EmbeddedResource']"))
            $exact = @($items | Where-Object {
                $_.GetAttribute('Include').Replace('/', '\') -eq $relative -or
                $_.GetAttribute('Update').Replace('/', '\') -eq $relative
            })
            if ($exact.Count -gt 1 -or @($exact | Where-Object {
                $_.HasAttribute('Condition') -or $_.ParentNode.HasAttribute('Condition') -or
                ($_.SelectSingleNode("*[local-name()='LogicalName']") -and $_.SelectSingleNode("*[local-name()='LogicalName']").InnerText -ne $logicalName)
            }).Count) {
                Write-Warning "Resource unchanged (custom or conditional metadata): $relative"
                continue
            }
            # Respect an existing wildcard provider; use Update to avoid duplicate items.
            $provided = $defaultResources -or @($items | Where-Object {
                $pattern = $_.GetAttribute('Include').Replace('/', '\')
                $pattern -and $relative -like $pattern
            }).Count -gt 0
            if ($exact.Count) {
                $item = $exact[0]
                if ($item.HasAttribute('Update') -and -not $provided) {
                    $item.RemoveAttribute('Update'); $item.SetAttribute('Include', $relative)
                }
            } else {
                $group = $xml.CreateElement('ItemGroup', $ns)
                $group.SetAttribute('Label', 'BuildManagerResourceRepair')
                $item = $xml.CreateElement('EmbeddedResource', $ns)
                $attribute = if ($provided) { 'Update' } else { 'Include' }
                $item.SetAttribute($attribute, $relative)
                [void]$group.AppendChild($item); [void]$xml.DocumentElement.AppendChild($group)
            }
            $logical = $item.SelectSingleNode("*[local-name()='LogicalName']")
            if (-not $logical) { $logical = $xml.CreateElement('LogicalName', $ns); [void]$item.AppendChild($logical) }
            $logical.InnerText = $logicalName
        }
        if ($xml.OuterXml -eq $before) { Write-Host "Resources already configured: $($project.Name)"; continue }
        if ($PSCmdlet.ShouldProcess($project.FullName, 'Restore embedded resource names and inclusion')) {
            $backup = $project.FullName + '.before-resource-repair.bak'
            if (-not $NoBackup -and -not (Test-Path -LiteralPath $backup)) { Copy-Item -LiteralPath $project.FullName -Destination $backup }
            $xml.Save($project.FullName)
            Write-Host "Repaired embedded resources: $($project.FullName)"
        }
    }
}
