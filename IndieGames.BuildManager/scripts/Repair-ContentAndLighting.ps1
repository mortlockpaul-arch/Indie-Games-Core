Set-StrictMode -Version Latest

function Repair-ContentLocaleAliases {
    [CmdletBinding(SupportsShouldProcess=$true)]
    param([Parameter(Mandatory)][string]$Root,[switch]$NoBackup)
    $content = Join-Path $Root 'Content'
    if (-not (Test-Path -LiteralPath $content -PathType Container)) { Write-Host '[SKIP] Content folder missing'; return }
    $count=0
    foreach ($file in (Get-ChildItem -LiteralPath $content -File -Recurse | Where-Object { $_.Name -match '(?i)_en-UK\.' })) {
        $dest = Join-Path $file.DirectoryName ($file.Name -replace '(?i)_en-UK\.', '_en-GB.')
        if (Test-Path -LiteralPath $dest) { continue }
        if ($PSCmdlet.ShouldProcess($dest, "Create en-GB alias from $($file.Name)")) {
            Copy-Item -LiteralPath $file.FullName -Destination $dest -ErrorAction Stop
            Write-Host "[CREATED] $dest"
        }
        $count++
    }
    Write-Host "[CONTENT LOCALE] $count alias(es) proposed or created; existing files unchanged."
}

function Repair-BasicEffectLightingMatrices {
    [CmdletBinding(SupportsShouldProcess=$true)]
    param([Parameter(Mandatory)][string]$Root,[switch]$NoBackup)
    $files = @(Get-ChildItem -LiteralPath $Root -Recurse -File -Filter 'GeometricPrimitive.cs' | Where-Object {
        $_.FullName -notmatch '[\\/](?:bin|obj|\.backups|XnaRedirect-Patched)[\\/]'
    })
    $helper = @'
    // FNA BasicEffect requires invertible World and View matrices for lighting.
    private static bool IsFinite(Matrix m)
    {
        return float.IsFinite(m.M11) && float.IsFinite(m.M12) && float.IsFinite(m.M13) && float.IsFinite(m.M14) &&
               float.IsFinite(m.M21) && float.IsFinite(m.M22) && float.IsFinite(m.M23) && float.IsFinite(m.M24) &&
               float.IsFinite(m.M31) && float.IsFinite(m.M32) && float.IsFinite(m.M33) && float.IsFinite(m.M34) &&
               float.IsFinite(m.M41) && float.IsFinite(m.M42) && float.IsFinite(m.M43) && float.IsFinite(m.M44);
    }

    private static bool CanInvert(Matrix m)
    {
        if (!IsFinite(m)) return false;
        float determinant = m.Determinant();
        return float.IsFinite(determinant) && Math.Abs(determinant) >= 1e-12f && IsFinite(Matrix.Invert(m));
    }

    private void ConfigureMatrices(Matrix world, Matrix view, Matrix projection)
    {
        if (!IsFinite(world) || !IsFinite(view) || !IsFinite(projection))
            throw new InvalidOperationException($"Non-finite primitive transform. World={world}; View={view}; Projection={projection}");
        basicEffect.World = world;
        basicEffect.View = view;
        basicEffect.Projection = projection;
        bool safe = CanInvert(world) && CanInvert(view);
        basicEffect.LightingEnabled = safe;
        if (!safe)
            System.Diagnostics.Debug.WriteLine($"[GeometricPrimitive] Disabled lighting for non-invertible matrix. World={world}; View={view}");
    }

'@
    $count=0
    foreach ($file in $files) {
        $source = [IO.File]::ReadAllText($file.FullName)
        if ($source.Contains('private void ConfigureMatrices(Matrix world, Matrix view, Matrix projection)')) { continue }
        # Only patch the known, narrow two-overload pattern; do not rewrite unrelated primitives.
        $assignment = 'basicEffect\.World\s*=\s*world\s*;\s*basicEffect\.View\s*=\s*view\s*;\s*basicEffect\.Projection\s*=\s*projection\s*;'
        $drawSignature = 'public\s+void\s+Draw\s*\(\s*Matrix\s+world\s*,\s*Matrix\s+view\s*,\s*Matrix\s+projection\s*,\s*Texture2D\s+texture\s*\)'
        $colorSignature = 'public\s+void\s+Draw\s*\(\s*Matrix\s+world\s*,\s*Matrix\s+view\s*,\s*Matrix\s+projection\s*,\s*Color\s+color\s*\)'
        if (-not [regex]::IsMatch($source,$drawSignature) -or -not [regex]::IsMatch($source,$colorSignature) -or
            ([regex]::Matches($source,$assignment)).Count -ne 2 -or
            ([regex]::Matches($source,'basicEffect\.LightingEnabled\s*=\s*true\s*;')).Count -ne 2) {
            Write-Warning "[SKIP] Unrecognised GeometricPrimitive structure: $($file.FullName)"; continue
        }
        $updated = [regex]::Replace($source, $assignment, 'ConfigureMatrices(world, view, projection);')
        $updated = [regex]::Replace($updated, '(?m)^\s*basicEffect\.LightingEnabled\s*=\s*true\s*;\s*\r?\n', '')
        $updated = ([regex]::new($drawSignature)).Replace($updated, [System.Text.RegularExpressions.MatchEvaluator]{ param($m) $helper + "    " + $m.Value }, 1)
        if ($updated -eq $source) { continue }
        if ($PSCmdlet.ShouldProcess($file.FullName, 'Apply safe BasicEffect matrix handling')) {
            if (-not $NoBackup) {
                $backup = $file.FullName + '.lighting.' + (Get-Date -Format 'yyyyMMdd-HHmmssfff') + '.bak'
                Copy-Item -LiteralPath $file.FullName -Destination $backup -ErrorAction Stop
                Write-Host "[BACKUP] $backup"
            }
            [IO.File]::WriteAllText($file.FullName, $updated, [Text.UTF8Encoding]::new($false))
            Write-Host "[PATCHED] $($file.FullName)"
        }
        $count++
    }
    Write-Host "[BASICEFFECT] $count file(s) proposed or patched."
}
