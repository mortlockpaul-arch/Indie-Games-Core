
. (Join-Path $PSScriptRoot 'Repair-EmbeddedResources.ps1')

function Repair-XnaStorageSave {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        # Preserve File.Open/Create migrations as well as constructor-based migrations.
        # Converts save paths only when the path variable is immediately used for writing.
        $pattern = '(?m)^[ \t]*(?:string|var)\s+(?<path>\w+)\s*=\s*Path\.Combine\(\s*(?<container>\w+)\.Path\s*,\s*(?<filename>[^;\r\n]+?)\s*\);[ \t]*\r?\n(?<indent>[ \t]*)(?<prefix>(?:(?:using\s+)?(?:FileStream|Stream)\s+\w+\s*=\s*|\w+\s*=\s*))File\.(?:Open|Create)\(\s*\k<path>\s*(?:,\s*FileMode\.(?:Create|CreateNew|OpenOrCreate))?\s*\)'
        $s = [regex]::Replace($s, $pattern, {
            param($m)
            $container = $m.Groups['container'].Value
            $filename = $m.Groups['filename'].Value.Trim()
            $indent = $m.Groups['indent'].Value
            $prefix = [regex]::Replace($m.Groups['prefix'].Value, '\bFileStream\b', 'Stream')
            return "${indent}${prefix}${container}.CreateFile($filename)"
        })

        $pattern = '(?m)^(?<indent>[ \t]*)(?:string|var)\s+(?<path>\w+)\s*=\s*Path\.Combine\(\s*(?<container>\w+)\.Path\s*,\s*(?<file>"[^"\r\n]+")\s*\);\s*\r?\n(?<open>[ \t]*using\s+FileStream\s+\w+\s*=\s*new\s+FileStream\(\s*\k<path>\s*,\s*FileMode\.Create\s*\);)'
        [regex]::Replace($s, $pattern, {
            param($m)
            $open = [regex]::Replace($m.Groups['open'].Value, 'FileStream', 'Stream')
            $open = [regex]::Replace($open, 'new\s+Stream\([^;]+\)', $m.Groups['container'].Value + '.CreateFile(' + $m.Groups['file'].Value + ')')
            return $open
        })
    }
}

function Invoke-XnaSourceFix {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [switch]$NoBackup,

        [Parameter(Mandatory)]
        [scriptblock]$Transform
    )

    if (-not (Test-Path -LiteralPath $Root)) {
        throw "Directory not found: $Root"
    }

    $files = @(
        Get-ChildItem -LiteralPath $Root -Recurse -Filter "*.cs" -File |
            Where-Object {
                $_.FullName -notmatch '\\(bin|obj|\.git)\\'
            }
    )

    $changed = 0

    foreach ($file in $files) {
        $path = $file.FullName
        $original = [System.IO.File]::ReadAllText($path)
        $updated = & $Transform $original

        if ($null -eq $updated) {
            Write-Warning "Transform returned null: $path"
            continue
        }

        if ($updated -ceq $original) {
            continue
        }

        if ($PSCmdlet.ShouldProcess($path, "Apply XNA source fix")) {

            if (-not $NoBackup) {
                Copy-Item -LiteralPath $path `
                    -Destination "$path.bak" -Force
            }

            # Preserve the original encoding where possible.
            $bytes = [System.IO.File]::ReadAllBytes($path)
            $encoding = [System.Text.Encoding]::UTF8

            if ($bytes.Length -ge 2) {
                if ($bytes[0] -eq 0xFF -and $bytes[1] -eq 0xFE) {
                    $encoding = [System.Text.Encoding]::Unicode
                }
                elseif ($bytes[0] -eq 0xFE -and $bytes[1] -eq 0xFF) {
                    $encoding = [System.Text.Encoding]::BigEndianUnicode
                }
                elseif ($bytes.Length -ge 3 -and
                        $bytes[0] -eq 0xEF -and
                        $bytes[1] -eq 0xBB -and
                        $bytes[2] -eq 0xBF) {
                    $encoding = [System.Text.UTF8Encoding]::new($true)
                }
            }

            [System.IO.File]::WriteAllText(
                $path,
                $updated,
                $encoding
            )

            Write-Host "Fixed: $path" -ForegroundColor Green
            $changed++
        }
    }

    Write-Host "Files scanned: $($files.Count)" -ForegroundColor Cyan
    Write-Host "Files modified: $changed" -ForegroundColor Green
}

function Repair-XnaStorageSelector {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,
        [switch]$NoBackup
    )

    Invoke-XnaSourceFix `
        -Root $Root `
        -NoBackup:$NoBackup `
        -WhatIf:$WhatIfPreference `
        -Transform {
            param($s)

            $s = $s.Replace(
                'Guide.BeginShowStorageDeviceSelector',
                'StorageDevice.BeginShowSelector'
            )

            $s = $s.Replace(
                'Guide.EndShowStorageDeviceSelector',
                'StorageDevice.EndShowSelector'
            )

            return $s
        }
}

function Repair-XnaColors {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [switch]$NoBackup
    )

    Invoke-XnaSourceFix `
        -Root $Root `
        -NoBackup:$NoBackup `
        -WhatIf:$WhatIfPreference `
        -Transform {
            param($s)

            # Missing XNA color constants
            $replacements = [ordered]@{
                'Color.TransparentWhite' = 'new Color(255, 255, 255, 0)'
                'Color.TransparentBlack' = 'new Color(0, 0, 0, 0)'
            }

            foreach ($old in $replacements.Keys) {
                $s = $s.Replace($old, $replacements[$old])
            }

            # Convert:
            # new Color(Color.Black, alpha)
            # new Color(Color.White, alpha)
            #
            # Alpha may be a variable, cast or expression.
            # Handle simple arguments only.

            $pattern = 'new\s+Color\s*\(\s*Color\.(?<color>Black|White)\s*,\s*(?<alpha>[^()]+|\([^()]*\)[^()]*)\)'

            $s = [regex]::Replace(
                $s,
                $pattern,
                {
                    param($match)

                    $color = $match.Groups['color'].Value
                    $alpha = $match.Groups['alpha'].Value.Trim()

                    if ($color -eq 'Black') {
                        return "new Color(0, 0, 0, $alpha)"
                    }

                    return "new Color(255, 255, 255, $alpha)"
                }
            )

            return $s
        }
}

function Repair-XnaOpenContainer {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [switch]$NoBackup
    )

    Invoke-XnaSourceFix `
        -Root $Root `
        -NoBackup:$NoBackup `
        -WhatIf:$WhatIfPreference `
        -Transform {
            param($s)

            $pattern = '\b(?<device>[A-Za-z_]\w*)\.OpenContainer\(\s*(?<name>"(?:[^"\\]|\\.)*"|[A-Za-z_]\w*)\s*\)'

            [regex]::Replace($s, $pattern, {
                param($match)

                $device = $match.Groups['device'].Value
                $name = $match.Groups['name'].Value

                return "$device.EndOpenContainer($device.BeginOpenContainer($name, null, null))"
            })
        }
}

function Repair-XnaResolveTexture2D {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [switch]$NoBackup
    )

    Invoke-XnaSourceFix `
        -Root $Root `
        -NoBackup:$NoBackup `
        -WhatIf:$WhatIfPreference `
        -Transform {
            param($s)

            # Replace old XNA type
            $s = [regex]::Replace(
                $s,
                '\bResolveTexture2D\b',
                'RenderTarget2D'
            )

            # Convert simple legacy constructors:
            # new RenderTarget2D(device, width, height, levels, format)
            $pattern = 'new\s+RenderTarget2D\s*\(\s*([^,()]+)\s*,\s*([^,()]+)\s*,\s*([^,()]+)\s*,\s*1\s*,\s*([^,()]+)\s*\)'

            $s = [regex]::Replace(
                $s,
                $pattern,
                {
                    param($m)

                    $device = $m.Groups[1].Value.Trim()
                    $width  = $m.Groups[2].Value.Trim()
                    $height = $m.Groups[3].Value.Trim()
                    $format = $m.Groups[4].Value.Trim()

                    return "new RenderTarget2D($device, $width, $height, false, $format, DepthFormat.None)"
                }
            )

            return $s
        }
}

function Repair-XnaStorageLoad {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        # Preserve File.Open/Create migrations as well as constructor-based migrations.
        # Rewrite only a declaration followed immediately by a known read operation.
        $pattern = '(?m)^[ \t]*(?:string|var)\s+(?<path>\w+)\s*=\s*Path\.Combine\(\s*(?<container>\w+)\.Path\s*,\s*(?<filename>[^;\r\n]+?)\s*\);[ \t]*\r?\n(?<indent>[ \t]*)(?<prefix>(?:(?:using\s+)?(?:FileStream|Stream)\s+\w+\s*=\s*|\w+\s*=\s*))File\.Open\(\s*\k<path>\s*,\s*FileMode\.Open\s*\)'
        $s = [regex]::Replace($s, $pattern, {
            param($m)
            return $m.Groups['indent'].Value + ([regex]::Replace($m.Groups['prefix'].Value, '\bFileStream\b', 'Stream')) + $m.Groups['container'].Value + '.OpenFile(' + $m.Groups['filename'].Value.Trim() + ', FileMode.Open)'
        })

        # Supports an if (!File.Exists(path)) { return; } between declaration
        # and stream creation, as used by The Impossible Game.
        $pattern = '(?ms)^(?<indent>[ \t]*)(?:string|var)\s+(?<path>\w+)\s*=\s*Path\.Combine\(\s*(?<container>\w+)\.Path\s*,\s*(?<file>"[^"\r\n]+")\s*\);[ \t]*\r?\n(?<guard>[ \t]*if\s*\(\s*!File\.Exists\(\s*\k<path>\s*\)\s*\)\s*\{\s*return\s*;\s*\}\s*)(?<open>[ \t]*using\s+FileStream\s+\w+\s*=\s*new\s+FileStream\(\s*\k<path>\s*,\s*FileMode\.Open\s*\);)'
        [regex]::Replace($s, $pattern, {
            param($m)
            $guard = [regex]::Replace($m.Groups['guard'].Value, 'File\.Exists\(\s*' + [regex]::Escape($m.Groups['path'].Value) + '\s*\)', $m.Groups['container'].Value + '.FileExists(' + $m.Groups['file'].Value + ')')
            $open = $m.Groups['open'].Value.Replace('FileStream', 'Stream')
            $open = [regex]::Replace($open, 'new\s+Stream\([^;]+\)', $m.Groups['container'].Value + '.OpenFile(' + $m.Groups['file'].Value + ', FileMode.Open)')
            return $m.Groups['indent'].Value + $guard + $open
        })
    }
}

function Repair-XnaStoragePaths {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        # A direct Path.Combine(container.Path, name) inside a File call is unambiguous.
        $file = 'Path\.Combine\(\s*(?<container>\w+)\.Path\s*,\s*(?<name>[^()\r\n]+?)\s*\)'
        $s = [regex]::Replace($s, 'File\.Exists\(\s*' + $file + '\s*\)', {
            param($m)
            return $m.Groups['container'].Value + '.FileExists(' + $m.Groups['name'].Value.Trim() + ')'
        })
        $s = [regex]::Replace($s, 'File\.Open\(\s*' + $file + '\s*,\s*FileMode\.Open\s*\)', {
            param($m)
            return $m.Groups['container'].Value + '.OpenFile(' + $m.Groups['name'].Value.Trim() + ', FileMode.Open)'
        })
        $s = [regex]::Replace($s, 'File\.Open\(\s*' + $file + '\s*,\s*FileMode\.Create\s*\)', {
            param($m)
            return $m.Groups['container'].Value + '.CreateFile(' + $m.Groups['name'].Value.Trim() + ')'
        })
        return $s
    }
}

function Find-XnaLegacyStorageCalls {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$Root)
    Get-ChildItem -LiteralPath $Root -Recurse -File -Filter '*.cs' |
        Where-Object { $_.FullName -notmatch '\\(bin|obj|\.git)\\' } |
        Select-String -Pattern '\.OpenContainer\s*\(', '\.Path\b', 'File\.(?:Open|Exists|Create)\s*\(', 'BeginShowStorageDeviceSelector', 'EndShowStorageDeviceSelector' |
        Select-Object Path, LineNumber, Line
}

function Repair-XnaFileStream {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        [regex]::Replace($s, '\bFileStream\s+filestream\b', 'Stream filestream')
    }
}

function Repair-XnaTransparentWhite {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        $s.Replace('Color.TransparentWhite', 'new Color(255, 255, 255, 0)')
    }
}

function Repair-XnaSignedInGamerAvatar {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        $pattern = 'if\s*\(\s*(?<gamer>\w+)\s*!=\s*null\s*&&\s*\k<gamer>\.Avatar\s*!=\s*null\s*\)\s*\{\s*return\s+\k<gamer>\.Avatar\s*;\s*\}'
        [regex]::Replace($s, $pattern, '')
    }
}

function Repair-XnaCompatibility {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    foreach ($fix in @('Repair-XnaStorageSelector','Repair-XnaOpenContainer','Repair-XnaStorageSave',
                       'Repair-XnaStorageLoad','Repair-XnaStoragePaths','Repair-XnaFileStream','Repair-XnaColors','Repair-XnaResolveTexture2D','Repair-XnaSpriteBatch',"Repair-XnaBloomComponent",
                       'Repair-XnaSignedInGamerAvatar','Repair-EmbeddedResources')) {
        Write-Host "Running $fix" -ForegroundColor Cyan
        & $fix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference
    }
}

function Resolve-XnaStartupProject {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$GameFolder,
        [string]$ProjectFile,
        [ValidateSet('Debug','Release')][string]$Configuration = 'Debug',
        [ValidateSet('x86','x64','AnyCPU')][string]$Platform = 'x64'
    )

    $folder = (Resolve-Path -LiteralPath $GameFolder -ErrorAction Stop).Path
    if ($ProjectFile) {
        $candidate = if ([IO.Path]::IsPathRooted($ProjectFile)) { $ProjectFile } else { Join-Path $folder $ProjectFile }
        $projects = @(Get-Item -LiteralPath $candidate -ErrorAction Stop)
    } else {
        $projects = @(Get-ChildItem -LiteralPath $folder -Recurse -Filter '*.csproj' -File |
            Where-Object { $_.FullName -notmatch '\\(bin|obj|\.git)\\' })
    }
    $launchable = @()
    foreach ($project in $projects) {
        $output = & dotnet msbuild $project.FullName "-p:Configuration=$Configuration" "-p:Platform=$Platform" '-getProperty:OutputType,TargetFramework,AssemblyName' 2>&1
        if ($LASTEXITCODE -ne 0) {
            Write-Warning "Could not evaluate $($project.FullName): $($output -join ' ')"
            continue
        }
        try { $props = ($output -join "`n" | ConvertFrom-Json -ErrorAction Stop).Properties }
        catch { Write-Warning "Unexpected MSBuild property output: $($project.FullName)"; continue }
        if ($props.OutputType -in @('Exe','WinExe')) {
            $launchable += [pscustomobject]@{
                Path = $project.FullName
                OutputType = $props.OutputType
                AssemblyName = $props.AssemblyName
                TargetFramework = $props.TargetFramework
            }
        }
    }
    if ($launchable.Count -eq 0) { throw "No executable (Exe/WinExe) project found under $folder. Do not run library DLLs." }
    if ($startupProjects.Count -gt 1) {

        $folderName = Split-Path $GameFolder -Leaf

        # Prefer a project named exactly like the game folder.
        $matches = @(
            $startupProjects | Where-Object {
                [IO.Path]::GetFileNameWithoutExtension($_) -eq $folderName
            }
        )

        if ($matches.Count -eq 1) {
            Write-Host "Automatically selected startup project:" -ForegroundColor Green
            Write-Host "  $($matches[0])"

            return $matches[0]
        }

        throw "Multiple startup projects found. Specify -ProjectFile explicitly:`n$($startupProjects -join "`n")"
    }
    return $launchable[0]
}

function Repair-XnaGraphicsContent {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [switch]$NoBackup
    )

    Invoke-XnaSourceFix `
        -Root $Root `
        -NoBackup:$NoBackup `
        -WhatIf:$WhatIfPreference `
        -Transform {
    param($s)

    # Convert old XNA graphics lifecycle overrides.
    $s = [regex]::Replace(
        $s,
        '\bLoadGraphicsContent\s*\(\s*bool\s+\w+\s*\)',
        'LoadContent()'
    )

    $s = [regex]::Replace(
        $s,
        '\bUnloadGraphicsContent\s*\(\s*bool\s+\w+\s*\)',
        'UnloadContent()'
    )

    # Convert base calls.
    $s = [regex]::Replace(
        $s,
        '\bbase\.LoadGraphicsContent\s*\([^)]*\)',
        'base.LoadContent()'
    )

    $s = [regex]::Replace(
        $s,
        '\bbase\.UnloadGraphicsContent\s*\([^)]*\)',
        'base.UnloadContent()'
    )

    return $s
}
}

function Repair-XnaSpriteBatch {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [switch]$NoBackup
    )

    Invoke-XnaSourceFix `
        -Root $Root `
        -NoBackup:$NoBackup `
        -WhatIf:$WhatIfPreference `
        -Transform {
            param($s)

            # Convert single-BlendState calls, including custom batch variable names.
            # Anchor to a statement line to avoid changing examples in line comments
            # or ordinary strings. Already migrated calls do not match.
            $s = [regex]::Replace(
                $s,
                '(?m)^(?<prefix>[ \t]*(?:(?:this|base)\s*\.\s*)?@?[A-Za-z_][A-Za-z_0-9]*\s*\.\s*Begin\s*\(\s*)(?<blend>(?:(?:global::)?Microsoft\.Xna\.Framework\.Graphics\.)?BlendState\s*\.\s*(?:AlphaBlend|Additive|Opaque|NonPremultiplied))(?<suffix>\s*\)\s*;)',
                '${prefix}Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, ${blend}${suffix}'
            )

            # Convert the old XNA 3.x SpriteBatch.Begin overload.
            $pattern = 'spriteBatch\.Begin\s*\(\s*SpriteBlendMode\.AlphaBlend\s*,\s*SpriteSortMode\.Immediate\s*,\s*SaveStateMode\.None\s*\)\s*;'

            $replacement = 'spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);'

            $s = [regex]::Replace(
                $s,
                $pattern,
                $replacement
            )

            # Remove old point-filter assignments only when
            # immediately following the converted Begin call.
            $pattern = '(?m)(?<begin>^[ \t]*spriteBatch\.Begin\(SpriteSortMode\.Immediate, BlendState\.AlphaBlend, SamplerState\.PointClamp, DepthStencilState\.None, RasterizerState\.CullNone\);)(?:[ \t]*\r?\n[ \t]*Global\.GraphicsDevice\.SamplerStates\[0\]\.(?:MagFilter|MipFilter|MinFilter)\s*=\s*TextureFilter\.Point;){3}'

            $s = [regex]::Replace(
                $s,
                $pattern,
                '${begin}'
            )

            return $s
        }
}

function Invoke-XnaGame {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$GameFolder,
        [string]$ProjectFile,
        [ValidateSet('Debug','Release')][string]$Configuration = 'Debug',
        [ValidateSet('x86','x64','AnyCPU')][string]$Platform = 'x64',
        [switch]$NoBuild
    )
    $game = Resolve-XnaStartupProject -GameFolder $GameFolder -ProjectFile $ProjectFile -Configuration $Configuration -Platform $Platform
    $project = $game.Path
    Write-Host "Startup project: $project" -ForegroundColor Cyan
    if (-not $NoBuild) {
        & dotnet build $project '-c' $Configuration "-p:Platform=$Platform"
        if ($LASTEXITCODE -ne 0) { throw "Game build failed: $project" }
    }
    $output = & dotnet msbuild $project "-p:Configuration=$Configuration" "-p:Platform=$Platform" '-getProperty:TargetPath,OutputType' 2>&1
    if ($LASTEXITCODE -ne 0) { throw "Failed to resolve game output: $($output -join ' ')" }
    $props = ($output -join "`n" | ConvertFrom-Json -ErrorAction Stop).Properties
    if ($props.OutputType -notin @('Exe','WinExe')) { throw "Refusing to launch a library: $project" }
    $target = $props.TargetPath
    if (-not (Test-Path -LiteralPath $target -PathType Leaf)) { throw "Game output not found: $target" }
    $workingDir = Split-Path -Parent $target
    Write-Host "Running game: $target" -ForegroundColor Cyan
    Push-Location $workingDir
    try {
        if ([IO.Path]::GetExtension($target) -ieq '.dll') { & dotnet $target }
        else { & $target }
        $code = $LASTEXITCODE
    } finally { Pop-Location }
    Write-Host "Game exit code: $code"
    if ($code -ne 0) { throw "Game exited with code $code" }
}

function Repair-XnaBloomComponent {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [switch]$NoBackup
    )

    Invoke-XnaSourceFix `
        -Root $Root `
        -NoBackup:$NoBackup `
        -WhatIf:$WhatIfPreference `
        -Transform {

            param($s)

            # Only modify the XNA BloomComponent implementation.
            if (
                $s -notmatch 'class\s+BloomComponent\s*:' -or
                $s -notmatch 'BloomExtract' -or
                $s -notmatch 'GaussianBlur'
            ) {
                return $s
            }

            # 1. ResolveTexture2D -> RenderTarget2D
            $s = [regex]::Replace(
                $s,
                '\bResolveTexture2D\b',
                'RenderTarget2D'
            )

            # 2. Remove obsolete back-buffer resolve.
            # Scene capture must occur before bloom Draw().
            $s = [regex]::Replace(
                $s,
                '(?m)^[ \t]*GraphicsDevice\.ResolveBackBuffer\s*\(\s*resolveTarget\s*\)\s*;[ \t]*\r?\n',
                ''
            )

            # 3. RenderTarget2D already inherits Texture2D.
            $s = [regex]::Replace(
                $s,
                '\b(renderTarget1|renderTarget2|resolveTarget)\.GetTexture\s*\(\s*\)',
                '$1'
            )

            # 4. Convert old SetRenderTarget overload.
            $s = [regex]::Replace(
                $s,
                'GraphicsDevice\.SetRenderTarget\s*\(\s*0\s*,\s*([^)]+)\)',
                'GraphicsDevice.SetRenderTarget($1)'
            )

            # 5. Convert legacy SpriteBatch.Begin.
            $s = [regex]::Replace(
                $s,
                'spriteBatch\.Begin\s*\(\s*SpriteBlendMode\.None\s*,\s*SpriteSortMode\.Immediate\s*,\s*SaveStateMode\.None\s*\)\s*;',
                'spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, showBuffer >= currentBuffer ? effect : null);'
            )

            # 6. Remove obsolete effect lifecycle calls.
            $s = [regex]::Replace(
                $s,
                '(?m)^[ \t]*effect\.Begin\s*\(\s*\)\s*;[ \t]*\r?\n',
                ''
            )

            $s = [regex]::Replace(
                $s,
                '(?m)^[ \t]*effect\.CurrentTechnique\.Passes\[0\]\.(?:Begin|End)\s*\(\s*\)\s*;[ \t]*\r?\n',
                ''
            )

            $s = [regex]::Replace(
                $s,
                '(?m)^[ \t]*effect\.End\s*\(\s*\)\s*;[ \t]*\r?\n',
                ''
            )

            # 7. Remove empty legacy effect conditionals.
            $s = [regex]::Replace(
                $s,
                'if\s*\(\s*showBuffer\s*>=\s*currentBuffer\s*\)\s*\{\s*\}',
                ''
            )

            # 8. Insert scene capture methods.
            # These must be called by the game's main drawing code.
            if ($s -notmatch 'public\s+void\s+BeginDraw\s*\(') {

                $captureMethods = @'

    public void BeginDraw()
    {
        GraphicsDevice.SetRenderTarget(resolveTarget);
    }

    public void EndDraw()
    {
        GraphicsDevice.SetRenderTarget(null);
    }

'@

                $s = [regex]::Replace(
                    $s,
                    '(?=\s*public override void Draw\s*\(\s*GameTime gameTime\s*\))',
                    [System.Text.RegularExpressions.MatchEvaluator] {
                        param($m)
                        return $captureMethods
                    },
                    [System.Text.RegularExpressions.RegexOptions]::None,
                    [TimeSpan]::FromSeconds(2)
                )
            }

            return $s
        }
}

function Repair-XnaNativeDlls {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [string]$OutputFolder,

        [ValidateSet("x86", "x64")]
        [string]$Architecture = "x64",

        [string]$Root = "C:\source\Indie-Games"
    )

    $sources = @{
        "SDL3.dll"  = "$Root\SDL3\lib\$Architecture\SDL3.dll"
        "FNA3D.dll" = "$Root\FNA.Contents\$Architecture\FNA3D.dll"
        "FAudio.dll" = "$Root\FNA.Contents\$Architecture\FAudio.dll"
    }

    if (-not (Test-Path -LiteralPath $OutputFolder -PathType Container)) {
        throw "Output directory does not exist: $OutputFolder"
    }

    foreach ($name in $sources.Keys) {
        $source = $sources[$name]
        $destination = Join-Path $OutputFolder $name

        if (-not (Test-Path -LiteralPath $source)) {
            Write-Warning "Native source missing: $source"
            continue
        }

        $sourceArch = Get-DllArchitecture $source

        if ($sourceArch -ne $Architecture) {
            throw "Wrong architecture for ${name}: $sourceArch (expected $Architecture)"
        }

        if (Test-Path -LiteralPath $destination) {
            $backup = "$destination.bak"
            Copy-Item -LiteralPath $destination -Destination $backup -Force
        }

        Copy-Item -LiteralPath $source -Destination $destination -Force

        $installedArch = Get-DllArchitecture $destination

        if ($installedArch -ne $Architecture) {
            throw "Architecture verification failed for $destination"
        }

        Write-Host "$name installed ($installedArch)" -ForegroundColor Green
    }
}

function Get-DllArchitecture {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [string]$Path
    )

    $resolvedPath = (Resolve-Path -LiteralPath $Path -ErrorAction Stop).Path

    $stream = [System.IO.File]::OpenRead($resolvedPath)
    $reader = [System.IO.BinaryReader]::new($stream)

    try {
        # Verify DOS header
        if ($reader.ReadUInt16() -ne 0x5A4D) {
            throw "Not a valid PE file: $resolvedPath"
        }

        # Read PE header offset
        $stream.Seek(0x3C, [System.IO.SeekOrigin]::Begin) | Out-Null
        $peOffset = $reader.ReadInt32()

        # Verify PE signature
        $stream.Seek($peOffset, [System.IO.SeekOrigin]::Begin) | Out-Null

        if ($reader.ReadUInt32() -ne 0x00004550) {
            throw "Invalid PE signature: $resolvedPath"
        }

        # Read machine architecture
        $machine = $reader.ReadUInt16()

        switch ($machine) {
            0x014C { return "x86" }
            0x8664 { return "x64" }
            0xAA64 { return "ARM64" }
            0x01C4 { return "ARM" }
            default {
                return "Unknown (0x{0:X4})" -f $machine
            }
        }
    }
    finally {
        $reader.Dispose()
        $stream.Dispose()
    }
}

function Get-Dll-References
{
    param(
        [string]$Root = 'D:\indie-game-archive\$1 Zombie Game, The (World) (XBLIG)\584E07D1'
    )

    $ErrorActionPreference = 'Stop'

    Write-Host "Scanning: $Root" -ForegroundColor Cyan

    if (-not (Test-Path -LiteralPath $Root))
    {
        throw "Folder not found: $Root"
    }

    $dlls = @(Get-ChildItem -LiteralPath $Root -Filter '*.dll' -File)

    Write-Host "Found $( $dlls.Count ) DLLs" -ForegroundColor Yellow

    foreach ($dll in $dlls)
    {
        Write-Host "`nChecking: $( $dll.Name )" -ForegroundColor Cyan

        try
        {
            $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName(
                    $dll.FullName
            )

            $assembly = [System.Reflection.Assembly]::LoadFrom($dll.FullName)

            $references = @($assembly.GetReferencedAssemblies() |
                    Where-Object {
                        $_.Name -like 'Microsoft.Xna.Framework*'
                    })

            if ($references.Count -eq 0)
            {
                Write-Host "  No XNA references"
                continue
            }

            foreach ($reference in $references)
            {
                Write-Host "  XNA: $( $reference.FullName )" -ForegroundColor Yellow
            }
        }
        catch
        {
            Write-Warning "$( $dll.Name ): $( $_.Exception.Message )"
        }
    }

    Write-Host "`nScan complete." -ForegroundColor Green
}

#
#.\Repair-AllXnaAssemblyReferences.ps1 `
#    -ArchiveRoot 'D:\indie-game-archive' `
#


#
#.\New-IndieGamesCoreRepo `
#    -Execute -InitializeGit
#
#.\Update-IndieGamesCoreSolution.ps1 -Execute
