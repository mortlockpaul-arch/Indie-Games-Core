[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
param(
    [string]$Root,
    [string]$FnaProject = 'C:\source\Indie-Games-Core\FNA\FNA.csproj',
    [switch]$NoBackup,
    [switch]$SkipDllAudit
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Repair-XnaAssemblyReferences {
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
    param(
        [Parameter(Mandatory = $true)][string]$Root,
        [string]$FnaProject = 'C:\source\Indie-Games-Core\FNA\FNA.csproj',
        [switch]$NoBackup,
        [switch]$SkipDllAudit
    )

    if (-not (Test-Path -LiteralPath $Root -PathType Container)) {
        throw "Game folder not found: $Root"
    }
    if (-not (Test-Path -LiteralPath $FnaProject -PathType Leaf)) {
        throw "FNA project not found: $FnaProject"
    }

    $legacy = @(
        'Microsoft.Xna.Framework',
        'Microsoft.Xna.Framework.Graphics',
        'Microsoft.Xna.Framework.Game',
        'Microsoft.Xna.Framework.Xact',
        'Microsoft.Xna.Framework.GamerServices',
        'Microsoft.Xna.Framework.Avatar',
        'Microsoft.Xna.Framework.Net',
        'Microsoft.Xna.Framework.Storage',
        'Microsoft.Xna.Framework.Video'
    )
    $projects = @(Get-ChildItem -LiteralPath $Root -Recurse -File -Filter '*.csproj' |
        Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' })
    $changed = 0
    $skipped = 0
    foreach ($project in $projects) {
        try {
            $xml = New-Object System.Xml.XmlDocument
            $xml.PreserveWhitespace = $true
            $xml.Load($project.FullName)
            $rootNode = $xml.DocumentElement
            if ($null -eq $rootNode -or $rootNode.LocalName -ne 'Project') {
                Write-Warning "Not an MSBuild project: $($project.FullName)"
                $skipped++
                continue
            }
            $ns = $rootNode.NamespaceURI
            $refs = @($xml.SelectNodes("//*[local-name()='Reference']"))
            $remove = @($refs | Where-Object {
                $include = $_.GetAttribute('Include')
                $name = ($include -split ',')[0].Trim()
                $legacy -contains $name
            })
            $fnaRefs = @($xml.SelectNodes("//*[local-name()='ProjectReference']") | Where-Object {
                $_.GetAttribute('Include') -match '(^|[\\/])FNA\.csproj$'
            })
            $directFnaDllRefs = @($refs | Where-Object { ($_.GetAttribute('Include') -split ',')[0].Trim() -eq 'FNA' })
            if ($remove.Count -eq 0 -and ($fnaRefs.Count -gt 0 -or $directFnaDllRefs.Count -gt 0)) {
                Write-Host "Already configured: $($project.Name)"
                continue
            }
            $target = $project.FullName
            if (-not $PSCmdlet.ShouldProcess($target, "Remove $($remove.Count) legacy XNA references and ensure FNA reference")) {
                continue
            }
            foreach ($ref in $remove) {
                [void]$ref.ParentNode.RemoveChild($ref)
            }
            if ($fnaRefs.Count -eq 0 -and $directFnaDllRefs.Count -eq 0) {
                # MSBuild accepts absolute project paths, including cross-drive references.
                $group = $xml.CreateElement('ItemGroup', $ns)
                $reference = $xml.CreateElement('ProjectReference', $ns)
                $reference.SetAttribute('Include', $FnaProject)
                [void]$group.AppendChild($reference)
                [void]$rootNode.AppendChild($group)
            }
            if (-not $NoBackup) {
                $backup = "$target.xna-references.bak"
                if (-not (Test-Path -LiteralPath $backup)) {
                    Copy-Item -LiteralPath $target -Destination $backup -ErrorAction Stop
                }
            }
            $xml.Save($target)
            $changed++
            Write-Host "Updated: $target (removed $($remove.Count) XNA reference(s))" -ForegroundColor Green
        }
        catch {
            $skipped++
            Write-Warning "Failed: $($project.FullName): $($_.Exception.Message)"
        }
    }

    if (-not $SkipDllAudit) {
        $dlls = @(Get-ChildItem -LiteralPath $Root -Recurse -File -Filter '*.dll' |
            Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' })
        foreach ($dll in $dlls) {
            try {
                $assembly = [System.Reflection.Assembly]::ReflectionOnlyLoadFrom($dll.FullName)
                $dependencies = @($assembly.GetReferencedAssemblies() | Where-Object { $legacy -contains $_.Name })
                if ($dependencies.Count -gt 0) {
                    Write-Warning "Legacy XNA DLL dependency: $($dll.FullName) -> $(($dependencies | ForEach-Object FullName) -join '; ')"
                }
            }
            catch {
                # ReflectionOnlyLoadFrom may be unavailable in PowerShell 7; use metadata via GetAssemblyName
                # only for file validation. Never claim that an uninspected DLL has no XNA dependencies.
                Write-Verbose "DLL dependency inspection unavailable: $($dll.FullName): $($_.Exception.Message)"
            }
        }
    }
    Write-Host "Projects scanned: $($projects.Count); updated: $changed; failed/skipped: $skipped"
}

if ($MyInvocation.InvocationName -ne '.') {
    if ([string]::IsNullOrWhiteSpace($Root)) { throw 'Specify -Root when executing this script.' }
    Repair-XnaAssemblyReferences -Root $Root -FnaProject $FnaProject -NoBackup:$NoBackup -SkipDllAudit:$SkipDllAudit -WhatIf:$WhatIfPreference
}
