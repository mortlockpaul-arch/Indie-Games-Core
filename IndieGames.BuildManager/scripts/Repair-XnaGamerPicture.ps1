# Repair-XnaGamerPicture.ps1
# Dot-source this file to register Repair-XnaGamerPicture in Build Manager.
# Or execute directly: .\Repair-XnaGamerPicture.ps1 -Root 'D:\indie-game-archive\This Is Hard (World) (XBLIG)' -WhatIf
[CmdletBinding(SupportsShouldProcess = $true)]
param([string]$Root, [switch]$NoBackup)

function Repair-XnaGamerPicture {
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)

    if (-not (Test-Path -LiteralPath $Root -PathType Container)) { throw "Folder not found: $Root" }
    $pattern = '(?m)^(?<indent>[ \t]*)pages\[pageName\]\s*=\s*new\s+SpritePage\(\s*profile\.GamerPicture\s*,\s*pageName\s*\);[ \t]*$'
    $scanned = 0; $matched = 0; $changed = 0
    foreach ($file in (Get-ChildItem -LiteralPath $Root -Recurse -File -Filter '*.cs' | Where-Object {
        $_.FullName -notmatch '[\\/](bin|obj|XnaRedirect-Patched|XnaRedirect-Test)[\\/]'
    })) {
        $scanned++
        $source = [System.IO.File]::ReadAllText($file.FullName)
        if (-not $source.Contains('profile.GamerPicture')) { continue }
        if ($source -notmatch '\bGraphicsDevice\s+device\s*;' -or $source -notmatch '\bContentManager\s+content\s*;') {
            Write-Warning "Unsupported context (device/content not found): $($file.FullName)"; continue
        }
        $hits = [regex]::Matches($source, $pattern)
        if ($hits.Count -ne 1) { Write-Warning "Expected one supported assignment, found $($hits.Count): $($file.FullName)"; continue }
        $matched++
        $hit = $hits[0]; $indent = $hit.Groups['indent'].Value
        $nl = if ($source.Contains("`r`n")) { "`r`n" } else { "`n" }
        $replacementLines = @(
            'Texture2D gamerPicture = null;',
            'try',
            '{',
            '    using (System.IO.Stream pictureStream = profile.GetGamerPicture())',
            '    {',
            '        if (pictureStream != null && pictureStream.CanRead)',
            '        {',
            '            gamerPicture = Texture2D.FromStream(device, pictureStream);',
            '        }',
            '    }',
            '}',
            'catch (System.Exception ex)',
            '{',
            '    System.Diagnostics.Debug.WriteLine("Gamer picture decode failed: " + ex.Message);',
            '}',
            'if (gamerPicture == null)',
            '{',
            '    gamerPicture = content.Load<Texture2D>("blackzone");',
            '}',
            'pages[pageName] = new SpritePage(gamerPicture, pageName);'
        )
        $replacement = ($replacementLines | ForEach-Object { $indent + $_ }) -join $nl
        $updated = $source.Substring(0, $hit.Index) + $replacement + $source.Substring($hit.Index + $hit.Length)
        if ($PSCmdlet.ShouldProcess($file.FullName, 'Convert GamerPicture property to GetGamerPicture stream')) {
            if (-not $NoBackup) {
                $backup = $file.FullName + '.gamerpicture.bak'
                if (-not (Test-Path -LiteralPath $backup)) { Copy-Item -LiteralPath $file.FullName -Destination $backup -ErrorAction Stop }
            }
            [System.IO.File]::WriteAllText($file.FullName, $updated, [System.Text.UTF8Encoding]::new($false))
            $changed++
            Write-Host "[FIXED] $($file.FullName)" -ForegroundColor Green
        }
    }
    Write-Host "GamerPicture: scanned=$scanned matched=$matched modified=$changed"
}

# Execute when invoked with -Root; only register function when dot-sourced without arguments.
if ($Root) { Repair-XnaGamerPicture -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference }
