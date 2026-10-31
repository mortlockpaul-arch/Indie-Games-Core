function Restore-GameProject {
    param(
        [Parameter(Mandatory)]
        [string]$ProjectPath,

        [string]$ProjectFile
    )

    if (-not (Test-Path $ProjectPath -PathType Container)) {
        Write-Host "Project folder not found: $ProjectPath" -ForegroundColor Red
        return
    }

    # Find the project file if not specified.
    if (-not $ProjectFile) {
        $projects = @(Get-ChildItem $ProjectPath -Filter *.csproj -File)

        if ($projects.Count -ne 1) {
            Write-Host "Specify ProjectFile (found $($projects.Count) projects)." -ForegroundColor Yellow
            return
        }

        $ProjectFile = $projects[0].FullName
    }
    else {
        $ProjectFile = Join-Path $ProjectPath $ProjectFile
    }

    # Remove old build and restore files.
    foreach ($folder in @("obj", "bin")) {
        $target = Join-Path $ProjectPath $folder

        if (Test-Path $target) {
            Remove-Item $target -Recurse -Force
            Write-Host "Removed: $target"
        }
    }

    # Restore NuGet packages.
    Write-Host "Restoring: $ProjectFile" -ForegroundColor Cyan
    dotnet restore $ProjectFile
}

function Repair-ProjectReferences {
    param(
        [Parameter(Mandatory)]
        [string]$RootPath,

        [Parameter(Mandatory)]
        [string]$OldPath,

        [Parameter(Mandatory)]
        [string]$NewPath
    )

    if (-not (Test-Path $RootPath -PathType Container)) {
        Write-Host "Folder not found: $RootPath" -ForegroundColor Red
        return
    }

    $changed = 0

    # Find and update project files recursively.
    Get-ChildItem $RootPath -Recurse -Filter *.csproj -File |
        ForEach-Object {
            $path = $_.FullName
            $text = Get-Content $path -Raw

            if ($text.Contains($OldPath)) {
                $text = $text.Replace($OldPath, $NewPath)

                Set-Content -Path $path -Value $text -Encoding UTF8

                $changed++
                Write-Host "Fixed: $path" -ForegroundColor Green
            }
        }

    Write-Host "Updated $changed project(s)." -ForegroundColor Cyan
}

function something7
{
    # Get project folder and assets file.
    $folder = Split-Path $project -Parent
    $assets = Join-Path $folder "obj\project.assets.json"

    # Check restored target frameworks.
    if (Test-Path $assets)
    {
        $json = Get-Content $assets -Raw | ConvertFrom-Json

        Write-Host "Restored targets:" -ForegroundColor Cyan
        $json.targets.PSObject.Properties.Name
    }
    else
    {
        Write-Host "Assets file not found: $assets" -ForegroundColor Red
    }

    dotnet restore $project -p:TargetFramework=net9.0-windows --force
}

function something6
{
    $root = ".\indie-game-archive\Head Shot 2 (World) (XBLIG)"

    Get-ChildItem -LiteralPath $root -Recurse -Filter "*.cs" |
            ForEach-Object {
                $path = $_.FullName
                $content = [System.IO.File]::ReadAllText($path)

                $updated = $content.Replace(
                        "Guide.BeginShowStorageDeviceSelector(",
                        "StorageDevice.BeginShowSelector("
                ).Replace(
                        "Guide.EndShowStorageDeviceSelector(",
                        "StorageDevice.EndShowSelector("
                )

                if ($updated -ne $content)
                {

                    # Add Storage namespace if missing
                    if ($updated -notmatch 'using Microsoft\.Xna\.Framework\.Storage;')
                    {
                        $updated = "using Microsoft.Xna.Framework.Storage;`r`n" + $updated
                    }

                    [System.IO.File]::WriteAllText($path, $updated)

                    Write-Host "Fixed: $path" -ForegroundColor Green
                }
            }
}

function something5
{
    $root = ".\indie-game-archive\Head Shot 2 (World) (XBLIG)"

    Get-ChildItem -LiteralPath $root -Recurse -Filter "*.cs" |
            ForEach-Object {
                $path = $_.FullName
                $content = [System.IO.File]::ReadAllText($path)

                $pattern = '(\b[A-Za-z_]\w*)\.OpenContainer\(\s*("(?:[^"\\]|\\.)*"|[A-Za-z_]\w*)\s*\)'

                $updated = [regex]::Replace(
                        $content,
                        $pattern,
                        {
                            param($match)

                            $device = $match.Groups[1].Value
                            $name = $match.Groups[2].Value

                            return "$device.EndOpenContainer($device.BeginOpenContainer($name, null, null))"
                        }
                )

                if ($updated -ne $content)
                {
                    [System.IO.File]::WriteAllText($path, $updated)

                    Write-Host "Fixed: $path" -ForegroundColor Green
                }
            }
}

function something4
{
    $root = ".\indie-game-archive\Head Shot 2 (World) (XBLIG)"

    Get-ChildItem -LiteralPath $root -Recurse -Filter "*.cs" |
            ForEach-Object {
                $path = $_.FullName
                $content = [System.IO.File]::ReadAllText($path)

                $pattern = '(?m)^\s*string path = Path\.Combine\(storageContainer\.Path,\s*File_Name \+ "\.sav"\);\s*\r?\n\s*filestream = File\.Open\(path,\s*FileMode\.Create\);'

                $replacement = @'
                                filestream = storageContainer.CreateFile(File_Name + ".sav");
'@

                $updated = [regex]::Replace(
                        $content,
                        $pattern,
                        "`r`n$replacement"
                )

                if ($updated -ne $content)
                {
                    Copy-Item -LiteralPath $path -Destination "$path.bak" -Force

                    [System.IO.File]::WriteAllText($path, $updated)

                    Write-Host "Fixed: $path" -ForegroundColor Green
                }
            }
}

function something3
{
    $root = ".\indie-game-archive\Head Shot 2 (World) (XBLIG)"

    Get-ChildItem -LiteralPath $root -Recurse -Filter "*.cs" |
            ForEach-Object {
                $path = $_.FullName
                $content = [System.IO.File]::ReadAllText($path)

                $updated = [regex]::Replace(
                        $content,
                        '\bFileStream\s+filestream\b',
                        'Stream filestream'
                )

                if ($updated -ne $content)
                {
                    Copy-Item -LiteralPath $path -Destination "$path.bak" -Force
                    [System.IO.File]::WriteAllText($path, $updated)

                    Write-Host "Fixed: $path" -ForegroundColor Green
                }
            }
}

function something2
{
    $root = ".\indie-game-archive\Head Shot 2 (World) (XBLIG)"

    Get-ChildItem -LiteralPath $root -Recurse -Filter "*.cs" |
            ForEach-Object {
                $path = $_.FullName
                $content = [System.IO.File]::ReadAllText($path)

                $updated = $content

                # Remove the unsupported container.Path assignment
                $updated = $updated.Replace(
                        'string path = Path.Combine(container.Path, File_Name + ".sav");',
                        ''
                )

                # Replace file existence check
                $updated = $updated.Replace(
                        'File.Exists(path)',
                        'container.FileExists(File_Name + ".sav")'
                )

                # Replace file creation
                $updated = $updated.Replace(
                        'File.Open(path, FileMode.CreateNew)',
                        'container.CreateFile(File_Name + ".sav")'
                )

                # Replace file loading
                $updated = $updated.Replace(
                        'File.Open(path, FileMode.Open)',
                        'container.OpenFile(File_Name + ".sav", FileMode.Open)'
                )

                if ($updated -ne $content)
                {
                    Copy-Item -LiteralPath $path -Destination "$path.bak" -Force

                    [System.IO.File]::WriteAllText($path, $updated)

                    Write-Host "Fixed: $path" -ForegroundColor Green
                }
            }
}

function something1
{
    $root = ".\indie-game-archive\Head Shot 2 (World) (XBLIG)"

    Get-ChildItem -LiteralPath $root -Recurse -Filter "*.cs" |
            ForEach-Object {
                $path = $_.FullName
                $content = [System.IO.File]::ReadAllText($path)

                $updated = $content.Replace(
                        "Color.TransparentWhite",
                        "new Color(255, 255, 255, 0)"
                )

                if ($updated -ne $content)
                {
                    Copy-Item -LiteralPath $path -Destination "$path.bak" -Force

                    [System.IO.File]::WriteAllText($path, $updated)

                    Write-Host "Fixed: $path" -ForegroundColor Green
                }
            }
}

function Find-XnaBackends {
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive\Sexy Flight (World) (XBLIG)"
    )

    if (-not (Test-Path -LiteralPath $Root -PathType Container)) {
        Write-Warning "Game folder not found: $Root"
        return
    }

    $patterns = @(
        "NetworkSession"
        "NetworkGamer"
        "SignedInGamer"
        "GamerServicesDispatcher"
        "Guide.ShowSignIn"
        "SendData"
        "ReceiveData"
        "NetworkSessionType"
        "AvailableNetworkSession"
        "LocalNetworkGamer"
    )

    Get-ChildItem -LiteralPath $Root -Recurse -Filter "*.cs" -File |
        Select-String -Pattern $patterns -SimpleMatch |
        Select-Object Path, LineNumber, Line
}


function Get-XnaNetworkReport {
    [CmdletBinding()]
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive",
        [string]$OutputDirectory = "C:\source\Indie-Games\reports",
        [switch]$IncludeDefinitions
    )

    if (-not (Test-Path -LiteralPath $Root -PathType Container)) {
        throw "Archive folder not found: $Root"
    }

    New-Item -ItemType Directory -Force -Path $OutputDirectory |
        Out-Null

    $patterns = [ordered]@{
        "NetworkSession.Create" = "LAN/Internet session hosting"
        "NetworkSession.Find" = "LAN discovery or matchmaking"
        "NetworkSession.Join" = "Connect to a multiplayer session"
        "NetworkSession.Update" = "Session event processing"
        "NetworkSession.StartGame" = "Synchronize game start"
        "NetworkSession.EndGame" = "Synchronize game end"
        "NetworkSession" = "Session lifecycle and multiplayer"
        "AvailableNetworkSession" = "Discovered multiplayer sessions"
        "NetworkSessionType" = "Session transport selection"
        "LocalNetworkGamer" = "Local player networking"
        "NetworkGamer" = "Remote/local player management"
        "SendData" = "Network packet transmission"
        "ReceiveData" = "Network packet reception"
        "SignedInGamer" = "Local player identity"
        "GamerServicesDispatcher" = "Legacy gamer services update loop"
        "GamerServicesComponent" = "Gamer services initialization"
        "Guide.ShowSignIn" = "Sign-in user interface"
        "Guide.BeginShowMessageBox" = "Gamer services message box"
        "Guide.ShowMessageBox" = "Gamer services message box"
        "Guide.BeginShowKeyboardInput" = "Keyboard input dialog"
        "Guide.BeginShowStorageDeviceSelector" = "Storage selection"
        "GamerPrivileges" = "Player permissions"
        "FriendCollection" = "Friends list"
        "InviteAccepted" = "Session invitation handling"
        "Steamworks" = "Steam platform integration"
        "SteamMatchmaking" = "Steam lobby/matchmaking"
        "SteamNetworking" = "Steam networking transport"
        "FNA.NetStub" = "FNA legacy networking stubs"
        "FNA.Steamworks" = "FNA Steamworks integration"
    }

    $gameSummary = [System.Collections.Generic.List[object]]::new()
    $apiDetails = [System.Collections.Generic.List[object]]::new()

    $games = @(
        Get-ChildItem -LiteralPath $Root -Directory |
            Sort-Object Name
    )

    $index = 0

    foreach ($game in $games) {
        $index++

        Write-Progress `
            -Activity "Scanning XBLIG networking" `
            -Status $game.Name `
            -PercentComplete (($index / [Math]::Max($games.Count, 1)) * 100)

            $files = @(
                Get-ChildItem -LiteralPath $game.FullName -Recurse -File |
                    Where-Object {
                        (
                            $_.Extension -in @(".cs", ".csproj", ".config")
                        ) -and (
                            $_.FullName -notmatch '[\\/](bin|obj|\.git)[\\/]'
                        )
                    }
            )

        $found = [System.Collections.Generic.HashSet[string]]::new(
            [StringComparer]::OrdinalIgnoreCase
        )

        $references = [System.Collections.Generic.HashSet[string]]::new(
            [StringComparer]::OrdinalIgnoreCase
        )

        foreach ($file in $files) {
            $isSource = $file.Extension -eq ".cs"

            try {
                $lines = [System.IO.File]::ReadAllLines($file.FullName)
            }
            catch {
                Write-Warning "Cannot read $($file.FullName): $_"
                continue
            }

            for ($i = 0; $i -lt $lines.Length; $i++) {
                $line = $lines[$i].Trim()

                if (-not $line) { continue }

                if (-not $IncludeDefinitions -and $isSource) {
                    if ($line -match '^(//|\*|/\*|using\s|namespace\s)') {
                        continue
                    }
                }

                foreach ($api in $patterns.Keys) {
                    if (-not $line.Contains($api)) {
                        continue
                    }

                    # Avoid duplicate generic matches when a
                    # more specific API was detected.
                    if ($api -eq "NetworkSession" -and
                        $line -match 'NetworkSession\.(Create|Find|Join|Update|StartGame|EndGame)') {
                        continue
                    }

                    if ($api -eq "NetworkGamer" -and
                        $line.Contains("LocalNetworkGamer")) {
                        continue
                    }

                    if ($api -eq "Steamworks" -and
                        $line.Contains("FNA.Steamworks")) {
                        continue
                    }

                    if ($isSource) {
                        [void]$found.Add($api)
                    }
                    else {
                        [void]$references.Add($api)
                    }

                    $apiDetails.Add([PSCustomObject]@{
                        Game        = $game.Name
                        API         = $api
                        Description = $patterns[$api]
                        File        = $file.FullName.Substring(
                            $game.FullName.Length
                        ).TrimStart('\')
                        LineNumber  = $i + 1
                        SourceType  = $file.Extension
                        Code        = $line
                    })
                }
            }
        }

        $networkApis = @(
            $found | Where-Object {
                $_ -match 'NetworkSession|NetworkGamer|SendData|ReceiveData|AvailableNetworkSession'
            }
        )

        $identityApis = @(
            $found | Where-Object {
                $_ -match 'SignedInGamer|GamerServices|Guide\.|GamerPrivileges|FriendCollection'
            }
        )

        $steamApis = @(
            $found | Where-Object {
                $_ -match 'Steamworks|SteamMatchmaking|SteamNetworking'
            }
        )

        $backend = "None detected"
        $resolution = "No networking changes indicated by scan"
        $options = "Keep existing FNA configuration"

        if ($networkApis.Count -gt 0) {
            $backend = "XNA Networking"
            $resolution = "Implement compatible session and packet networking"
            $options = "FNA.Network (LAN); FNA.Network (Internet); Steamworks adapter"
        }
        elseif ($identityApis.Count -gt 0) {
            $backend = "XNA Gamer Services"
            $resolution = "Implement local identity and gamer services UI"
            $options = "Local profile backend; FNA.NetStub with additional implementations"
        }

        if ($steamApis.Count -gt 0) {
            $backend = "Steamworks"
            $resolution = "Verify Steam API initialization and compatibility"
            $options = "FNA.Steamworks; Steamworks.NET adapter"
        }

        $hasNetStub = $references.Contains("FNA.NetStub") -or
                      $found.Contains("FNA.NetStub")

        $hasSteamworks = $references.Contains("FNA.Steamworks") -or
                         $found.Contains("FNA.Steamworks")

        $gameSummary.Add([PSCustomObject]@{
            Game             = $game.Name
            Backend          = $backend
            API_Count        = $found.Count
            NetworkAPI_Count = $networkApis.Count
            IdentityAPI_Count = $identityApis.Count
            SteamAPI_Count   = $steamApis.Count
            UsesNetStub      = $hasNetStub
            UsesSteamworks   = $hasSteamworks
            DetectedAPIs     = (($found | Sort-Object) -join "; ")
            References       = (($references | Sort-Object) -join "; ")
            Resolution       = $resolution
            Options          = $options
        })
    }

    Write-Progress -Activity "Scanning XBLIG networking" -Completed

    $summaryPath = Join-Path $OutputDirectory "xna-network-summary.csv"
    $detailsPath = Join-Path $OutputDirectory "xna-network-api-details.csv"

    $gameSummary |
        Export-Csv -LiteralPath $summaryPath -NoTypeInformation -Encoding UTF8

    $apiDetails |
        Export-Csv -LiteralPath $detailsPath -NoTypeInformation -Encoding UTF8

    Write-Host ""
    Write-Host "Network scan complete" -ForegroundColor Green
    Write-Host "Games scanned: $($games.Count)"
    Write-Host "API matches: $($apiDetails.Count)"
    Write-Host "Summary: $summaryPath"
    Write-Host "Details: $detailsPath"

    $gameSummary |
        Sort-Object NetworkAPI_Count, IdentityAPI_Count -Descending |
        Format-Table Game, Backend, API_Count, Resolution -AutoSize

    return $gameSummary
}

function Repair-XnaSignedInGamerAvatar {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Root,

        [switch]$NoBackup
    )

    if (-not (Test-Path -LiteralPath $Root -PathType Container)) {
        Write-Warning "Folder not found: $Root"
        return
    }

    $total = 0

    $pattern = @'
if\s*\(\s*(?<gamer>\w+)\s*!=\s*null\s*&&\s*\k<gamer>\.Avatar\s*!=\s*null\s*\)\s*\{\s*return\s+\k<gamer>\.Avatar\s*;\s*\}
'@.Trim()

    Get-ChildItem -LiteralPath $Root -Recurse -Filter "*.cs" -File |
        ForEach-Object {

            $file = $_.FullName
            $content = [System.IO.File]::ReadAllText($file)

            $matches = [regex]::Matches($content, $pattern)

            if ($matches.Count -eq 0) {
                return
            }

            $updated = [regex]::Replace(
                $content,
                $pattern,
                ""
            )

            if ($updated -ne $content) {

                if (-not $NoBackup) {
                    Copy-Item `
                        -LiteralPath $file `
                        -Destination "$file.bak" `
                        -Force
                }

                [System.IO.File]::WriteAllText(
                    $file,
                    $updated
                )

                $total += $matches.Count

                Write-Host "Fixed: $file" -ForegroundColor Green
            }
        }

    Write-Host "`nSignedInGamer.Avatar fixes: $total" `
        -ForegroundColor Cyan

    return $total
}

function Repair-CSharpAlias {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory)]
        [string]$Root,

        [Parameter(Mandatory)]
        [string]$TypeName,

        [Parameter(Mandatory)]
        [string]$TargetType,

        [switch]$Recurse
    )

    if (-not (Test-Path -LiteralPath $Root)) {
        throw "Path not found: $Root"
    }

    $aliasLine = "using $TypeName = $TargetType;"

    $files = if (Test-Path -LiteralPath $Root -PathType Leaf) {
        @(Get-Item -LiteralPath $Root)
    }
    else {
        @(
            Get-ChildItem -LiteralPath $Root `
                -Filter "*.cs" `
                -File `
                -Recurse:$Recurse |
                Where-Object {
                    $_.FullName -notmatch '[\\/](bin|obj)[\\/]'
                }
        )
    }

    $fixed = 0

    foreach ($file in $files) {
        $text = [System.IO.File]::ReadAllText($file.FullName)

        $escapedType = [regex]::Escape($TypeName)

        # Skip files with an existing alias.
        if ($text -match "(?m)^\s*using\s+$escapedType\s*=") {
            continue
        }

        # Only process files referencing the ambiguous type.
        if ($text -notmatch "\b$escapedType\b") {
            continue
        }

        if ($PSCmdlet.ShouldProcess($file.FullName, "Add $aliasLine")) {

            $backup = "$($file.FullName).alias.bak"

            if (-not (Test-Path -LiteralPath $backup)) {
                Copy-Item -LiteralPath $file.FullName -Destination $backup
            }

            $updated = [regex]::new(
                '(?m)^(?=\s*using\s+)',
                [System.Text.RegularExpressions.RegexOptions]::Multiline
            ).Replace($text, "$aliasLine`r`n", 1)

            if ($updated -eq $text) {
                Write-Warning "No using directive found: $($file.FullName)"
                continue
            }

            [System.IO.File]::WriteAllText(
                $file.FullName,
                $updated,
                [System.Text.UTF8Encoding]::new($false)
            )

            Write-Host "Fixed: $($file.FullName)" -ForegroundColor Green
            $fixed++
        }
    }

    Write-Host "Files fixed: $fixed" -ForegroundColor Cyan
}

function Remove-ReferenceProjects {
    param(
        [string]$Solution = "C:\source\Indie-Games\Indie-Games.sln",
        [string]$FolderName = "Reference-Projects"
    )

    if (-not (Test-Path -LiteralPath $Solution)) {
        throw "Solution not found: $Solution"
    }

    $backup = "$Solution.$(Get-Date -Format 'yyyyMMdd-HHmmss').bak"
    Copy-Item -LiteralPath $Solution -Destination $backup

    $lines = [System.Collections.Generic.List[string]]::new()
    $lines.AddRange([string[]](Get-Content -LiteralPath $Solution))

    $projectPattern = '^Project\("\{(?<type>[^}]+)\}"\)\s*=\s*"(?<name>[^"]+)",\s*"(?<path>[^"]+)",\s*"\{(?<guid>[^}]+)\}"'

    $projects = @{}
    $nested = @{}

    foreach ($line in $lines) {
        if ($line -match $projectPattern) {
            $guid = $Matches.guid.ToUpperInvariant()

            $projects[$guid] = @{
                Name = $Matches.name
                Type = $Matches.type.ToUpperInvariant()
                Path = $Matches.path
            }
        }
    }

    $inNested = $false

    foreach ($line in $lines) {
        if ($line -match '^\s*GlobalSection\(NestedProjects\)') {
            $inNested = $true
            continue
        }

        if ($inNested -and $line -match '^\s*EndGlobalSection') {
            $inNested = $false
            continue
        }

        if ($inNested -and
            $line -match '^\s*\{([^}]+)\}\s*=\s*\{([^}]+)\}') {

            $nested[$Matches[1].ToUpperInvariant()] =
                $Matches[2].ToUpperInvariant()
        }
    }

    $folderGuid = @(
        $projects.Keys | Where-Object {
            $projects[$_].Name -eq $FolderName -and
            $projects[$_].Type -eq "66A26720-8FB5-11D2-AA7E-00C04F688DDE"
        }
    ) | Select-Object -First 1

    if (-not $folderGuid) {
        Write-Warning "Solution folder not found: $FolderName"
        return
    }

    $remove = [System.Collections.Generic.HashSet[string]]::new(
        [StringComparer]::OrdinalIgnoreCase
    )

    [void]$remove.Add($folderGuid)

    # Include every descendant of the target solution folder.
    do {
        $changed = $false

        foreach ($child in @($nested.Keys)) {
            if ($remove.Contains($nested[$child]) -and
                -not $remove.Contains($child)) {

                [void]$remove.Add($child)
                $changed = $true
            }
        }
    } while ($changed)

    $output = [System.Collections.Generic.List[string]]::new()
    $skipProject = $false
    $inNested = $false

    foreach ($line in $lines) {

        if ($line -match $projectPattern) {
            $guid = $Matches.guid.ToUpperInvariant()
            $skipProject = $remove.Contains($guid)

            if ($skipProject) {
                Write-Host "Removing: $($projects[$guid].Name)" -ForegroundColor Yellow
                continue
            }
        }

        if ($skipProject) {
            if ($line -match '^EndProject\s*$') {
                $skipProject = $false
            }
            continue
        }

        if ($line -match '^\s*GlobalSection\(NestedProjects\)') {
            $inNested = $true
        }
        elseif ($line -match '^\s*EndGlobalSection') {
            $inNested = $false
        }

        if ($inNested -and
            $line -match '^\s*\{([^}]+)\}\s*=\s*\{([^}]+)\}') {

            if ($remove.Contains($Matches[1]) -or
                $remove.Contains($Matches[2])) {
                continue
            }
        }

        # Remove configuration entries for removed projects.
        if ($line -match '^\s*\{([^}]+)\}\.' -and
            $remove.Contains($Matches[1])) {
            continue
        }

        $output.Add($line)
    }

    # Preserve the original file encoding where practical.
    $encoding = [System.Text.UTF8Encoding]::new($true)
    [System.IO.File]::WriteAllLines($Solution, $output, $encoding)

    Write-Host ""
    Write-Host "Removed $($remove.Count) solution entries." -ForegroundColor Green
    Write-Host "Backup: $backup"
}

$root = "C:\source\Indie-Games\FNA.Steamworks"

$project = Get-ChildItem "$root\Steamworks.NET\Standalone" `
    -Filter "Steamworks.NET.csproj" |
    Select-Object -First 1

if (-not $project) {
    throw "Steamworks.NET.csproj not found"
}
$project = "C:\source\Indie-Games\FNA.Steamworks\Steamworks.NET\Standalone\Steamworks.NET.csproj"
dotnet build $project -p:Configuration=Windows -p:Platform=x64 -p:OutputPath="bin\x64\Windows\"

function Repair-FarseerHashSet {
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive\Baby Maker Extreme 2 (World) (XBLIG)"
    )

    $file = Join-Path $Root "FarseerPhysics\Dynamics\World.cs"

    if (-not (Test-Path -LiteralPath $file)) {
        throw "File not found: $file"
    }

    $text = [System.IO.File]::ReadAllText($file)
    $original = $text

    # Remove any invalid HashSet aliases.
    $text = [regex]::Replace(
        $text,
        '(?m)^[ \t]*using\s+HashSet\s*=\s*[^;]+;[ \t]*\r?\n?',
        ''
    )

    # Replace only unqualified HashSet<T> usages.
    $text = [regex]::Replace(
        $text,
        '(?<![\w.])HashSet(?=\s*<)',
        'FarseerPhysics.Common.HashSet'
    )

    if ($text -ne $original) {
        Copy-Item $file "$file.bak" -Force

        [System.IO.File]::WriteAllText(
            $file,
            $text,
            [System.Text.UTF8Encoding]::new($false)
        )

        Write-Host "Fixed: $file" -ForegroundColor Green
    }
    else {
        Write-Host "No changes needed."
    }
}

$file = "C:\source\Indie-Games\indie-game-archive\Baby Maker Extreme 2 (World) (XBLIG)\FarseerPhysics\Factories\LinkFactory.cs"

$text = [System.IO.File]::ReadAllText($file)

if ($text -notmatch '(?m)^\s*using\s+Path\s*=') {

    $updated = [regex]::Replace(
        $text,
        '(?m)^(?=using\s+)',
        "using Path = FarseerPhysics.Common.Path;`r`n",
        1
    )

    if ($updated -eq $text) {
        throw "No top-level using directive found in $file"
    }

    Copy-Item -LiteralPath $file -Destination "$file.alias.bak" -Force

    [System.IO.File]::WriteAllText(
        $file,
        $updated,
        [System.Text.UTF8Encoding]::new($false)
    )

    Write-Host "Fixed: $file" -ForegroundColor Green
}
else {
    Write-Host "Path alias already exists."
}

function Repair-DuplicateGlassBreaker {
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive\Baby Maker Extreme 2 (World) (XBLIG)"
    )

    $project = Join-Path $Root "Baby Maker Extreme 2 (World) (XBLIG).csproj"

    if (-not (Test-Path $project)) {
        throw "Project not found: $project"
    }

    [xml]$xml = Get-Content $project -Raw

    # SDK-style projects automatically include *.cs files.
    # Explicitly exclude the duplicate dotted-namespace folder.
    $itemGroup = $xml.CreateElement("ItemGroup")
    $compile = $xml.CreateElement("Compile")

    $compile.SetAttribute(
        "Remove",
        "PlayObjects.Props\GlassBreaker.cs"
    )

    [void]$itemGroup.AppendChild($compile)
    [void]$xml.Project.AppendChild($itemGroup)

    Copy-Item $project "$project.duplicate.bak" -Force

    $xml.Save($project)

    Write-Host "Excluded duplicate GlassBreaker.cs" -ForegroundColor Green
}

function Repair-FNADuplicateReferences {
    param(
        [Parameter(Mandatory = $true)]
        [string]$GamePath,

        [string]$SolutionRoot = "C:\source\Indie-Games",

        [switch]$WhatIf
    )

    # Put the existing fixer code here
}

$ErrorActionPreference = "Stop"

# Default configuration
if (-not $SolutionRoot) {
    $SolutionRoot = "C:\source\Indie-Games"
}

if (-not $GamePath) {
    $GamePath = Join-Path $SolutionRoot `
        "indie-game-archive\Zombie Estate (World) (XBLIG)"
}

# Validate paths
if (-not (Test-Path -LiteralPath $SolutionRoot -PathType Container)) {
    throw "Solution root not found: $SolutionRoot"
}

if (-not (Test-Path -LiteralPath $GamePath -PathType Container)) {
    throw "Game folder not found: $GamePath"
}

$SolutionRoot = (Resolve-Path -LiteralPath $SolutionRoot).Path
$GamePath = (Resolve-Path -LiteralPath $GamePath).Path

Write-Host "Solution root: $SolutionRoot"
Write-Host "Game path: $GamePath"
$avatarProject = Join-Path $SolutionRoot "FNA.AvatarCompat\FNA.AvatarCompat.csproj"

function Save-ModifiedFile {
    param(
        [string]$Path,
        [string]$Content
    )

    if ($WhatIf) {
        Write-Host "[WhatIf] Would modify: $Path" -ForegroundColor Yellow
        return
    }

    Copy-Item -LiteralPath $Path -Destination "$Path$backupSuffix"
    [System.IO.File]::WriteAllText(
        $Path,
        $Content,
        [System.Text.UTF8Encoding]::new($false)
    )

    Write-Host "Updated: $Path" -ForegroundColor Green
}

Write-Host "`nProcessing: $gameName" -ForegroundColor Cyan

# -------------------------------------------------
# 1. Remove FNA.NetStub references from .csproj files
# -------------------------------------------------

$projects = Get-ChildItem -LiteralPath $GamePath -Recurse -Filter "*.csproj" -File |
    Where-Object {
        $_.FullName -notmatch '\\(bin|obj)\\'
    }

foreach ($project in $projects) {

    [xml]$xml = Get-Content -LiteralPath $project.FullName -Raw
    $modified = $false

    $references = @(
        $xml.SelectNodes(
            "//*[local-name()='ProjectReference' or local-name()='Reference']"
        )
    )

    foreach ($reference in $references) {

        $include = $reference.GetAttribute("Include")
        $hintPath = $reference.SelectSingleNode(
            "*[local-name()='HintPath']"
        )

        $isNetStub = (
            $include -match 'FNA\.NetStub' -or
            ($hintPath -and $hintPath.InnerText -match 'FNA\.NetStub')
        )

        if ($isNetStub) {
            Write-Host "Removing reference: $include"
            [void]$reference.ParentNode.RemoveChild($reference)
            $modified = $true
        }
    }

    # Add AvatarCompat only to the main game project
    if ($project.FullName -ieq $mainProject) {

        $existing = @(
            $xml.SelectNodes(
                "//*[local-name()='ProjectReference']"
            )
        ) | Where-Object {
            $_.GetAttribute("Include") -match 'FNA\.AvatarCompat'
        }

        if (-not $existing) {

            $relativePath = [System.IO.Path]::GetRelativePath(
                (Split-Path $project.FullName -Parent),
                $avatarProject
            )

            $namespace = $xml.DocumentElement.NamespaceURI
            $group = $xml.CreateElement("ItemGroup", $namespace)
            $reference = $xml.CreateElement("ProjectReference", $namespace)

            $reference.SetAttribute("Include", $relativePath)

            [void]$group.AppendChild($reference)
            [void]$xml.DocumentElement.AppendChild($group)

            Write-Host "Adding AvatarCompat: $relativePath"
            $modified = $true
        }
    }

    if ($modified) {
        if ($WhatIf) {
            Write-Host "[WhatIf] Would update: $($project.FullName)" -ForegroundColor Yellow
        }
        else {
            Copy-Item -LiteralPath $project.FullName `
                -Destination "$($project.FullName)$backupSuffix"

            $xml.Save($project.FullName)

            Write-Host "Updated: $($project.FullName)" -ForegroundColor Green
        }
    }
}

# -------------------------------------------------
# 2. Remove obsolete NetStub aliases from C# files
# -------------------------------------------------

$sourceFiles = Get-ChildItem -LiteralPath $GamePath -Recurse -Filter "*.cs" -File |
    Where-Object {
        $_.FullName -notmatch '\\(bin|obj)\\'
    }

foreach ($source in $sourceFiles) {

    $content = [System.IO.File]::ReadAllText($source.FullName)
    $original = $content

    $content = [regex]::Replace(
        $content,
        '(?m)^[ \t]*extern[ \t]+alias[ \t]+NetStub[ \t]*;[ \t]*\r?\n',
        ''
    )

    $content = $content -replace '\bNetStub::', ''

    if ($content -cne $original) {
        Save-ModifiedFile -Path $source.FullName -Content $content
    }
}

Write-Host "`nFinished processing $gameName" -ForegroundColor Green



#Repair-DuplicateGlassBreaker
Repair-FarseerHashSet
#Repair-XnaSignedInGamerAvatar -Root ".\indie-game-archive\This Is Hard (World) (XBLIG)"
#Restore-GameProject -ProjectPath "C:\source\Indie-Games\indie-game-archive\Avatar Farm Online (World) (XBLIG)" -ProjectFile "Avatar Farm Online (World) (XBLIG).csproj"
#Repair-ProjectReferences -RootPath "C:\source\Indie-Games" -OldPath "D:\downloads\FNA.Contents" -NewPath "C:\source\Indie-Games\FNA.Contents"
# Set project path.
$project = "C:\source\Indie-Games\indie-game-archive\Avatar Farm Online (World) (XBLIG)\Avatar Farm Online (World) (XBLIG).csproj"
