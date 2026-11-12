# Dot-source this file to register Repair-XnaLegacyGraphics in the Build Manager.
function Repair-XnaLegacyGraphics {
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Medium')]
    param(
        [Parameter(Mandatory = $true)]
        [string]$Root,
        [switch]$NoBackup
    )

    Set-StrictMode -Version Latest
    $ErrorActionPreference = 'Stop'
    $rootPath = (Resolve-Path -LiteralPath $Root -ErrorAction Stop).ProviderPath
    $files = @(Get-ChildItem -LiteralPath $rootPath -Filter '*.cs' -File -Recurse -ErrorAction Stop |
        Where-Object { $_.FullName -notmatch '[\\/](?:bin|obj|XnaRedirect-Patched|XnaRedirect-Test)[\\/]' })
    $scanned = 0; $matched = 0; $modified = 0
    Write-Host "[Repair-XnaLegacyGraphics] Root=$rootPath WhatIf=$([bool]$WhatIfPreference) NoBackup=$([bool]$NoBackup)" -ForegroundColor Cyan

    foreach ($file in $files) {
        $scanned++
        $reader = [System.IO.StreamReader]::new($file.FullName, $true)
        try { $original = $reader.ReadToEnd(); $encoding = $reader.CurrentEncoding }
        finally { $reader.Dispose() }
        $updated = $original
        $changes = [System.Collections.Generic.List[string]]::new()

        # XNA 3.x VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements)
        $pattern = 'new\s+VertexDeclaration\s*\(\s*(?:this\.)?GraphicsDevice\s*,\s*VertexPositionColor\.VertexElements\s*\)'
        if ([regex]::IsMatch($updated, $pattern)) {
            $updated = [regex]::Replace($updated, $pattern, 'VertexPositionColor.VertexDeclaration')
            $changes.Add('VertexPositionColor.VertexDeclaration')
        }

        # XNA 3.x BasicEffect(GraphicsDevice, null). Do not rewrite non-null effect pools.
        $pattern = 'new\s+BasicEffect\s*\(\s*((?:this\.)?GraphicsDevice)\s*,\s*null\s*\)'
        if ([regex]::IsMatch($updated, $pattern)) {
            $updated = [regex]::Replace($updated, $pattern, 'new BasicEffect($1)')
            $changes.Add('BasicEffect(GraphicsDevice)')
        }

        # Only replace the precise legacy SourceAlpha/One blend sequence.
        $additive = 'device\.BlendState\s*=\s*BlendState\.AlphaBlend\s*;\s*device\.RenderState\.SourceBlend\s*=\s*Blend\.SourceAlpha\s*;\s*device\.RenderState\.DestinationBlend\s*=\s*Blend\.One\s*;'
        if ([regex]::IsMatch($updated, $additive)) {
            $replacement = @'
device.BlendState = new BlendState
            {
                ColorSourceBlend = Blend.SourceAlpha,
                ColorDestinationBlend = Blend.One,
                AlphaSourceBlend = Blend.SourceAlpha,
                AlphaDestinationBlend = Blend.One
            };
'@
            $updated = [regex]::Replace($updated, $additive, [System.Text.RegularExpressions.MatchEvaluator]{ param($m) $replacement })
            $changes.Add('SourceAlpha/One blend state')
        }

        # Only replace the precise legacy SourceAlpha/InverseSourceAlpha sequence.
        $normal = 'device\.BlendState\s*=\s*BlendState\.AlphaBlend\s*;\s*device\.RenderState\.SourceBlend\s*=\s*Blend\.SourceAlpha\s*;\s*device\.RenderState\.DestinationBlend\s*=\s*Blend\.InverseSourceAlpha\s*;'
        if ([regex]::IsMatch($updated, $normal)) {
            $updated = [regex]::Replace($updated, $normal, 'device.BlendState = BlendState.NonPremultiplied;')
            $changes.Add('SourceAlpha/InverseSourceAlpha blend state')
        }

        if ($updated -ceq $original) { continue }
        $matched++
        Write-Host "  MATCH: $($file.FullName) [$($changes -join ', ')]"
        if ($PSCmdlet.ShouldProcess($file.FullName, 'Apply FNA legacy graphics compatibility fixes')) {
            if (-not $NoBackup) {
                $backup = "$($file.FullName).legacygraphics.bak"
                if (-not (Test-Path -LiteralPath $backup)) {
                    Copy-Item -LiteralPath $file.FullName -Destination $backup -ErrorAction Stop
                }
            }
            [System.IO.File]::WriteAllText($file.FullName, $updated, $encoding)
            $modified++
            Write-Host "  FIXED: $($file.FullName)" -ForegroundColor Green
        }
    }
    Write-Host "[Repair-XnaLegacyGraphics] Files scanned: $scanned; matched: $matched; modified: $modified" -ForegroundColor Cyan
}
