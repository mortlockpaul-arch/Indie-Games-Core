
function Repair-XnaStorageSave {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        $pattern = '(?m)^[ \t]*string\s+path\s*=\s*Path\.Combine\(storageContainer\.Path,\s*File_Name\s*\+\s*"\.sav"\);[ \t]*\r?\n[ \t]*filestream\s*=\s*File\.Open\(path,\s*FileMode\.Create\);'
        [regex]::Replace($s, $pattern, 'filestream = storageContainer.CreateFile(File_Name + ".sav");')
    }
}

function Repair-XnaStorageLoad {
    [CmdletBinding(SupportsShouldProcess)]
    param([Parameter(Mandatory)][string]$Root, [switch]$NoBackup)
    Invoke-XnaSourceFix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference -Transform {
        param($s)
        # Only rewrite when the exact original path declaration is present.
        $declaration = 'string path = Path.Combine(container.Path, File_Name + ".sav");'
        if (-not $s.Contains($declaration)) { return $s }
        $u = $s.Replace($declaration, '')
        $u = $u.Replace('File.Exists(path)', 'container.FileExists(File_Name + ".sav")')
        $u = $u.Replace('File.Open(path, FileMode.CreateNew)', 'container.CreateFile(File_Name + ".sav")')
        $u.Replace('File.Open(path, FileMode.Open)', 'container.OpenFile(File_Name + ".sav", FileMode.Open)')
    }
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
                       'Repair-XnaStorageLoad','Repair-XnaFileStream','Repair-XnaTransparentWhite',
                       'Repair-XnaSignedInGamerAvatar')) {
        Write-Host "Running $fix" -ForegroundColor Cyan
        & $fix -Root $Root -NoBackup:$NoBackup -WhatIf:$WhatIfPreference
    }
}

# Select a launchable game project, never a referenced class library.
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


function Get-DllArchitecture {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory, ValueFromPipeline, ValueFromPipelineByPropertyName)]
        [Alias("FullName")]
        [string]$Path
    )

    process {
        if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) {
            throw "File not found: $Path"
        }

        $stream = [System.IO.File]::OpenRead($Path)

        try {
            $reader = [System.IO.BinaryReader]::new($stream)

            # DOS header
            if ($reader.ReadUInt16() -ne 0x5A4D) {
                return "Not PE"
            }

            # PE header offset
            $stream.Position = 0x3C
            $peOffset = $reader.ReadInt32()

            if ($peOffset -lt 0 -or $peOffset -gt ($stream.Length - 24)) {
                return "Invalid PE"
            }

            $stream.Position = $peOffset

            if ($reader.ReadUInt32() -ne 0x00004550) {
                return "Invalid PE"
            }

            # IMAGE_FILE_HEADER.Machine
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
            $stream.Dispose()
        }
    }
}


$root = "C:\source\Indie-Games"

Get-ChildItem $root -Recurse -Filter "FNA3D.dll" -File |
    ForEach-Object {
        [PSCustomObject]@{
            Architecture = Get-DllArchitecture $_.FullName
            Path         = $_.FullName
        }
    } |
    Format-Table -AutoSize

$root = "C:\source\Indie-Games"

$source = "$root\FNA.Contents\x64\FNA3D.dll"

$destination = "$root\indie-game-archive\360 Mega Pack HD Remix\bin\x64\Debug\net9.0-windows\win-x64\FNA3D.dll"

if ((Get-DllArchitecture $source) -ne "x64") {
    throw "Source FNA3D.dll is not x64"
}

Copy-Item $destination "$destination.bak" -Force
Copy-Item $source $destination -Force

Write-Host "Installed: $(Get-DllArchitecture $destination)"

$root = "C:\source\Indie-Games"

$gameOutput = "$root\indie-game-archive\360 Mega Pack HD Remix\bin\x64\Debug\net9.0-windows\win-x64"

$nativeDlls = @(
    "SDL3.dll",
    "FNA3D.dll",
    "FAudio.dll",
    "Theorafile.dll"
)

foreach ($name in $nativeDlls) {
    $path = Join-Path $gameOutput $name

    if (Test-Path $path) {
        [PSCustomObject]@{
            DLL          = $name
            Architecture = Get-DllArchitecture $path
            Path         = $path
        }
    }
    else {
        Write-Warning "Missing: $name"
    }
}

$root = "C:\source\Indie-Games"
$output = "$root\indie-game-archive\360 Mega Pack HD Remix\bin\x64\Debug\net9.0-windows\win-x64"

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

$root = "C:\source\Indie-Games"

$output = "$root\indie-game-archive\360 Mega Pack HD Remix\bin\x64\Debug\net9.0-windows\win-x64"

$root = "C:\source\Indie-Games"

Get-ChildItem $root -Recurse -Filter "CSteamworks.dll" -File |
    ForEach-Object {
        [PSCustomObject]@{
            Architecture = Get-DllArchitecture $_.FullName
            Path         = $_.FullName
        }
    } | Format-Table -AutoSize