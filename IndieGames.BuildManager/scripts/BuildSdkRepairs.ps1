# Shared SDK repair function library. Use fix_sdk1005.ps1 for command-line actions.


$ErrorActionPreference = "Stop"

function Repair-NetSdk1005 {
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive",
        [string]$ProjectName = "",
        [string]$Framework = "net9.0-windows"
    )

    if ($ProjectName) {
        $folders = @(Get-Item -LiteralPath (Join-Path $Root $ProjectName) -ErrorAction Stop)
    }
    else {
        $folders = @(Get-ChildItem -LiteralPath $Root -Directory)
    }

    foreach ($folder in $folders) {
        $projects = @(Get-ChildItem -LiteralPath $folder.FullName -Filter "*.csproj" -File)

        foreach ($project in $projects) {
            # Only process projects targeting the requested framework
            [xml]$xml = Get-Content -LiteralPath $project.FullName
            $targets = @(
                $xml.Project.PropertyGroup.TargetFramework
                $xml.Project.PropertyGroup.TargetFrameworks
            ) -join ";"

            if ($targets -notmatch [regex]::Escape($Framework)) {
                continue
            }

            Write-Host "`nRepairing: $($project.Name)" -ForegroundColor Cyan

            foreach ($name in @("obj", "bin")) {
                $path = Join-Path $project.DirectoryName $name

                if (Test-Path -LiteralPath $path) {
                    Write-Host "Removing $path" -ForegroundColor Yellow
                    Remove-Item -LiteralPath $path -Recurse -Force
                }
            }

            Write-Host "Restoring $Framework..." -ForegroundColor Green

            & dotnet restore $project.FullName `
                -p:TargetFramework=$Framework `
                --force

            if ($LASTEXITCODE -ne 0) {
                Write-Warning "Restore failed: $($project.Name)"
                continue
            }

            Write-Host "Restored: $($project.Name)" -ForegroundColor Green
        }
    }
}

function Get-MSBuildPath {
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"

    if (Test-Path -LiteralPath $vswhere) {
        $msbuild = & $vswhere `
            -latest `
            -products '*' `
            -requires Microsoft.Component.MSBuild `
            -find 'MSBuild\Current\Bin\amd64\MSBuild.exe' |
            Select-Object -First 1

        if ($msbuild -and (Test-Path -LiteralPath $msbuild)) {
            return $msbuild
        }
    }

    # Fall back to the standard Visual Studio installation paths
    foreach ($edition in @("Community", "Professional", "Enterprise", "BuildTools")) {
        $path = "C:\Program Files\Microsoft Visual Studio\18\$edition\MSBuild\Current\Bin\amd64\MSBuild.exe"

        if (Test-Path -LiteralPath $path) {
            return $path
        }
    }

    # Fall back to MSBuild already available in PATH
    $command = Get-Command msbuild.exe -ErrorAction SilentlyContinue

    if ($command) {
        return $command.Source
    }

    throw "MSBuild.exe not found. Install Visual Studio Build Tools or the MSBuild component."
}

function Repair-SharedProjectAssets {
    param(
        [Parameter(Mandatory = $true, Position = 0)]
        [string]$Game,

        [string]$Root = "C:\source\Indie-Games\indie-game-archive",

        [switch]$Restore,

        [switch]$Clean,
        [switch]$FixRuntimeIdentifiers
    )

    # Accept either a full path or a game folder name
    if ([IO.Path]::IsPathRooted($Game)) {
        $folder = $Game
    }
    else {
        $folder = Join-Path $Root $Game
    }

    if (-not (Test-Path -LiteralPath $folder -PathType Container)) {
        Write-Error "Game folder not found: $folder"
        return
    }

    $folder = (Resolve-Path -LiteralPath $folder).Path
    $projects = @(Get-ChildItem -LiteralPath $folder -Filter "*.csproj" -File)

    if ($projects.Count -eq 0) {
        Write-Warning "No .csproj files found in $folder"
        return
    }

    Write-Host "`nGame: $folder" -ForegroundColor Cyan
    Write-Host "Projects found: $($projects.Count)" -ForegroundColor Yellow

    $propsPath = Join-Path $folder "Directory.Build.props"

    if (Test-Path -LiteralPath $propsPath) {
        Write-Warning "Directory.Build.props already exists: $propsPath"
        Write-Warning "Existing file preserved. Verify it configures unique obj paths."
    }
    else {
        # Preserve properties inherited from the nearest parent props file
        $props = @'
<Project>
  <PropertyGroup>
    <BaseIntermediateOutputPath>obj\$(MSBuildProjectName)\</BaseIntermediateOutputPath>
    <MSBuildProjectExtensionsPath>$(BaseIntermediateOutputPath)</MSBuildProjectExtensionsPath>
  </PropertyGroup>
</Project>
'@

        $parent = Split-Path $folder -Parent
        $parentProps = $null

        while ($parent) {
            $candidate = Join-Path $parent "Directory.Build.props"

            if (Test-Path -LiteralPath $candidate) {
                $parentProps = $candidate
                break
            }

            $next = Split-Path $parent -Parent
            if ($next -eq $parent) { break }
            $parent = $next
        }

        if ($parentProps) {
            $relative = [IO.Path]::GetRelativePath($folder, $parentProps)
            $relative = $relative.Replace("/", "\")

            $import = "  <Import Project=`"$relative`" />"
            $props = $props.Replace("<Project>", "<Project>`r`n$import")
        }

        Set-Content -LiteralPath $propsPath -Value $props -Encoding UTF8
        Write-Host "Created: $propsPath" -ForegroundColor Green
    }

    if ($FixRuntimeIdentifiers) {
    foreach ($project in $projects) {
        Write-Host "Checking runtime identifiers: $($project.Name)" -ForegroundColor Cyan

        $xml = New-Object System.Xml.XmlDocument
        $xml.PreserveWhitespace = $true
        $xml.Load($project.FullName)

        $rootNode = $xml.DocumentElement
        $ns = $rootNode.NamespaceURI

        $group = @(
            $rootNode.ChildNodes | Where-Object {
                $_ -is [System.Xml.XmlElement] -and
                $_.LocalName -eq "PropertyGroup" -and
                -not $_.HasAttribute("Condition")
            }
        ) | Select-Object -First 1

        if (-not $group) {
            $group = $xml.CreateElement(
                "",
                "PropertyGroup",
                $rootNode.NamespaceURI
            )
            [void]$rootNode.PrependChild($group)
        }

        $runtimeNodes = @(
            $group.ChildNodes | Where-Object {
                $_ -is [System.Xml.XmlElement] -and
                $_.LocalName -eq "RuntimeIdentifiers" -and
                -not $_.HasAttribute("Condition")
            }
        )

        if ($runtimeNodes.Count -gt 0) {
            $runtimeNodes[0].InnerText = "win-x86;win-x64"
        }
        else {
            $node = $xml.CreateElement(
                "",
                "RuntimeIdentifiers",
                $xml.DocumentElement.NamespaceURI
            )

            $node.InnerText = "win-x86;win-x64"
            [void]$group.AppendChild($node)
        }

        Save-FormattedXml -Xml $xml -Path $project.FullName

        Write-Host "Configured win-x86;win-x64: $($project.Name)" -ForegroundColor Green
    }
}

    if ($Clean) {
        $obj = Join-Path $folder "obj"

        if (Test-Path -LiteralPath $obj) {
            Write-Host "Removing: $obj" -ForegroundColor Yellow
            Remove-Item -LiteralPath $obj -Recurse -Force
        }
    }

    if ($Restore) {
        foreach ($project in $projects) {
            Write-Host "`nRestoring: $($project.Name)" -ForegroundColor Cyan

            & dotnet restore $project.FullName `
                --force `
                -p:RuntimeIdentifiers="win-x86;win-x64"

            if ($LASTEXITCODE -eq 0) {
                Write-Host "SUCCESS: $($project.Name)" -ForegroundColor Green
            }
            else {
                Write-Warning "FAILED: $($project.Name)"
            }
        }
    }

    Write-Host "`nFinished: $($projects.Count) projects checked." -ForegroundColor Green
}

function Repair-SDK1047 {
    [CmdletBinding()]
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive",
        [string]$Game,
        [switch]$All,
        [string]$Framework = "net9.0-windows",
        [ValidateSet("x86", "x64", "Both")]
        [string]$Platform = "x64",
        [switch]$Clean
    )

    $ErrorActionPreference = "Stop"

    if ($All) {
        $folders = @(
            Get-ChildItem -LiteralPath $Root -Directory
        )
    }
    elseif ($Game) {
        $folder = if ([IO.Path]::IsPathRooted($Game)) {
            $Game
        }
        else {
            Join-Path $Root $Game
        }

        if (-not (Test-Path -LiteralPath $folder -PathType Container)) {
            throw "Game folder not found: $folder"
        }

        $folders = @(Get-Item -LiteralPath $folder)
    }
    else {
        throw "Specify -Game or -All"
    }

    $platforms = if ($Platform -eq "Both") {
        @("x86", "x64")
    }
    else {
        @($Platform)
    }

    foreach ($folder in $folders) {
        Write-Host "`nGame: $($folder.Name)" -ForegroundColor Cyan

        $projects = @(
            Get-ChildItem -LiteralPath $folder.FullName `
                -Filter "*.csproj" -File |
                Where-Object {
                    $_.BaseName -eq $folder.Name
                }
        )

        if ($projects.Count -eq 0) {
            continue
        }

        foreach ($project in $projects) {
            Write-Host "`nProject: $($project.Name)" -ForegroundColor Yellow

            # Load project XML while preserving whitespace.
            $xml = New-Object System.Xml.XmlDocument
            $xml.PreserveWhitespace = $false
            $xml.Load($project.FullName)
            $rootNode = $xml.DocumentElement
            $ns = $rootNode.NamespaceURI

            $frameworkNodes = @(
                $xml.SelectNodes(
                    "//*[local-name()='TargetFramework' or local-name()='TargetFrameworks']"
                )
            )

            $frameworks = @(
                $frameworkNodes |
                    ForEach-Object { $_.InnerText -split ";" }
            )

            if ($Framework -notin $frameworks) {
                Write-Warning "Skipping: target framework not found."
                continue
            }

            # Find an unconditional property group.
            $propertyGroup = $null

            foreach ($node in $rootNode.ChildNodes) {
                if (
                    $node -is [System.Xml.XmlElement] -and
                    $node.LocalName -eq "PropertyGroup" -and
                    -not $node.HasAttribute("Condition")
                ) {
                    $propertyGroup = $node
                    break
                }
            }

            if (-not $propertyGroup) {
                $propertyGroup = $xml.CreateElement(
                    "PropertyGroup",
                    $ns
                )

                [void]$rootNode.PrependChild($propertyGroup)
            }

            # Configure restore targets for both architectures.
            $runtimeNode = $propertyGroup.SelectSingleNode(
                "*[local-name()='RuntimeIdentifiers']"
            )

            if (-not $runtimeNode) {
                $runtimeNode = $xml.CreateElement(
                    "RuntimeIdentifiers",
                    $ns
                )

                [void]$propertyGroup.AppendChild($runtimeNode)
            }

            if ($runtimeNode.InnerText -ne "win-x86;win-x64") {
                $runtimeNode.InnerText = "win-x86;win-x64"
                Save-FormattedXml -Xml $xml -Path $project.FullName

                Write-Host "Configured RuntimeIdentifiers: win-x86;win-x64" `
                    -ForegroundColor Green
            }

            if ($Clean) {
                # Remove only this project's intermediate files
                # when a project-specific obj folder exists.
                $projectObj = Join-Path $folder.FullName (
                    "obj\$($project.BaseName)"
                )

                if (Test-Path -LiteralPath $projectObj) {
                    Write-Host "Removing: $projectObj" -ForegroundColor Yellow

                    Remove-Item -LiteralPath $projectObj `
                        -Recurse -Force
                }
            }

            foreach ($buildPlatform in $platforms) {
                $runtime = if ($buildPlatform -eq "x86") {
                    "win-x86"
                }
                else {
                    "win-x64"
                }

                Write-Host "`nRestoring $buildPlatform ($runtime)..." `
                    -ForegroundColor Cyan

                & dotnet restore $project.FullName `
                    --force `
                    "-p:Platform=$buildPlatform"

                if ($LASTEXITCODE -ne 0) {
                    Write-Warning "Restore failed: $($project.Name) [$buildPlatform]"
                    continue
                }

                Write-Host "Building $buildPlatform..." -ForegroundColor Cyan

                & dotnet build $project.FullName `
                    -c Debug `
                    --no-restore `
                    "-p:Platform=$buildPlatform"

                if ($LASTEXITCODE -ne 0) {
                    Write-Warning "Build failed: $($project.Name) [$buildPlatform]"
                    continue
                }

                # Query the evaluated assets path.
                $assetsPath = (
                    & dotnet msbuild $project.FullName `
                        "-p:Configuration=Debug" `
                        "-p:Platform=$buildPlatform" `
                        -getProperty:ProjectAssetsFile
                ) | Select-Object -Last 1

                $assetsPath = "$assetsPath".Trim()

                if (-not [IO.Path]::IsPathRooted($assetsPath)) {
                    $assetsPath = Join-Path $folder.FullName $assetsPath
                }

                if (-not (Test-Path -LiteralPath $assetsPath)) {
                    Write-Warning "Assets file missing: $assetsPath"
                    continue
                }

                $json = Get-Content -LiteralPath $assetsPath -Raw |
                    ConvertFrom-Json

                $targets = @(
                    $json.targets.PSObject.Properties.Name
                )

                $expectedTarget = "$Framework/$runtime"

                if ($expectedTarget -in $targets) {
                    Write-Host "SUCCESS: $($project.Name)" -ForegroundColor Green
                    Write-Host "Verified: $expectedTarget"
                    Write-Host "Assets: $assetsPath"
                }
                else {
                    Write-Warning "Missing target: $expectedTarget"

                    Write-Host "Available targets:"
                    $targets | ForEach-Object {
                        Write-Host "  $_"
                    }
                }
            }
        }
    }

    Write-Host "`nSDK1047 repair finished." -ForegroundColor Cyan
}

function Rename-DllProjects
{
    param(
        [Parameter(Mandatory = $true)]
        [string]$Game,

        [string]$Root = "C:\source\Indie-Games\indie-game-archive",

        [string]$MainProject = "",

        [switch]$Restore
    )

    $folder = if ( [IO.Path]::IsPathRooted($Game))
    {
        $Game
    }
    else
    {
        Join-Path $Root $Game
    }

    if (-not (Test-Path -LiteralPath $folder -PathType Container))
    {
        Write-Warning "Game folder not found: $folder"
        return
    }

    $gameName = Split-Path $folder -Leaf

    if (-not $MainProject)
    {
        $MainProject = "$gameName.csproj"
    }
    elseif (-not $MainProject.EndsWith(
            ".csproj",
            [StringComparison]::OrdinalIgnoreCase
    ))
    {
        $MainProject += ".csproj"
    }

    $mainPath = Join-Path $folder $MainProject

    if (-not (Test-Path -LiteralPath $mainPath -PathType Leaf))
    {
        Write-Warning "Main project not found: $mainPath"
        Write-Warning "Skipping DLL project renaming to protect the main project."
        return
    }

    Write-Host "Main project: $MainProject" -ForegroundColor Cyan

    if ($Restore)
    {
        $projects = @(
            Get-ChildItem -LiteralPath $folder `
                -Filter "*.csproj.disabled" -File
        )
    }
    else
    {
        $projects = @(
        Get-ChildItem -LiteralPath $folder `
                -Filter "*.csproj" -File |
                Where-Object {
                    $_.Name -ine $MainProject
                }
        )
    }

    foreach ($project in $projects)
    {

        if ($Restore)
        {
            $newName = $project.Name -replace '\.disabled$', ''
        }
        else
        {
            $newName = "$($project.Name).disabled"
        }

        $destination = Join-Path $folder $newName

        if (Test-Path -LiteralPath $destination)
        {
            Write-Warning "Already exists: $destination"
            continue
        }

        Rename-Item `
            -LiteralPath $project.FullName `
            -NewName $newName

        Write-Host "Renamed: $( $project.Name ) -> $newName" `
            -ForegroundColor Green
    }
}

function Save-FormattedXml {
    param(
        [Parameter(Mandatory)]
        [System.Xml.XmlDocument]$Xml,

        [Parameter(Mandatory)]
        [string]$Path
    )

    $settings = New-Object System.Xml.XmlWriterSettings
    $settings.Indent = $true
    $settings.IndentChars = "`t"
    $settings.NewLineChars = "`r`n"
    $settings.NewLineHandling = [System.Xml.NewLineHandling]::Replace
    $settings.OmitXmlDeclaration = $false
    $settings.Encoding = New-Object System.Text.UTF8Encoding($false)

    # Rebuild whitespace so existing formatting does not interfere.
    $Xml.PreserveWhitespace = $false

    $writer = [System.Xml.XmlWriter]::Create($Path, $settings)

    try {
        $Xml.Save($writer)
    }
    finally {
        $writer.Dispose()
    }
}

function Format-XbligProjects {
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive",

        [string]$Game,

        [switch]$All,

        [switch]$Recurse,

        [switch]$NoBackup
    )

    $ErrorActionPreference = "Stop"

    # Determine which folder to process.
    if ($Game) {
        $folder = if ([IO.Path]::IsPathRooted($Game)) {
            $Game
        }
        else {
            Join-Path $Root $Game
        }

        if (-not (Test-Path -LiteralPath $folder -PathType Container)) {
            throw "Game folder not found: $folder"
        }

        $folder = (Resolve-Path -LiteralPath $folder).Path
    }
    elseif ($All) {
        $folder = (Resolve-Path -LiteralPath $Root).Path
    }
    else {
        throw "Specify -Game or -All"
    }

    # Find project files.
    $searchParams = @{
        LiteralPath = $folder
        Filter      = "*.csproj"
        File        = $true
        Recurse     = $true
    }

    $projects = @(
        Get-ChildItem @searchParams |
            Where-Object {
                $_.FullName -notmatch '\\(obj|bin|\.git)\\'
            }
    )

    Write-Host "`nFound $($projects.Count) project(s)." -ForegroundColor Cyan

    $formatted = 0
    $unchanged = 0
    $failed = 0

    foreach ($project in $projects) {
        try {
            $path = $project.FullName

            # Read original content.
            $original = [IO.File]::ReadAllText($path)

            # Load XML without preserving existing indentation.
            $xml = New-Object System.Xml.XmlDocument
            $xml.PreserveWhitespace = $false
            $xml.LoadXml($original)

            if ($xml.DocumentElement.LocalName -ne "Project") {
                Write-Warning "Skipping non-project XML: $path"
                continue
            }

            # Remember whether the original had an XML declaration.
            $hasDeclaration = $xml.FirstChild -is [System.Xml.XmlDeclaration]

            # Remove the declaration to prevent duplicate WriteStartDocument calls.
            if ($hasDeclaration) {
                [void]$xml.RemoveChild($xml.FirstChild)
            }

            # Configure XML formatting.
            $settings = New-Object System.Xml.XmlWriterSettings
            $settings.Indent = $true
            $settings.IndentChars = "`t"
            $settings.NewLineChars = "`r`n"
            $settings.NewLineHandling = [System.Xml.NewLineHandling]::Replace
            $settings.OmitXmlDeclaration = $true
            $settings.Encoding = New-Object System.Text.UTF8Encoding($false)

            # Write formatted XML to a UTF-8 memory stream.
            $stream = New-Object System.IO.MemoryStream

            try {
                $writer = [System.Xml.XmlWriter]::Create($stream, $settings)

                try {
                    $xml.Save($writer)
                }
                finally {
                    $writer.Dispose()
                }

                $formattedText = [System.Text.Encoding]::UTF8.GetString(
                    $stream.ToArray()
                )
            }
            finally {
                $stream.Dispose()
            }

            # Restore the original declaration if one existed.
            if ($hasDeclaration) {
                $formattedText = '<?xml version="1.0" encoding="utf-8"?>' +
                                 "`r`n" +
                                 $formattedText
            }

            # Ensure one trailing newline.
            $formattedText = $formattedText.TrimEnd() + "`r`n"

            if ($original -ceq $formattedText) {
                $unchanged++
                continue
            }

            if ($PSCmdlet.ShouldProcess($path, "Format XML")) {
                if (-not $NoBackup) {
                    Copy-Item -LiteralPath $path `
                        -Destination "$path.bak" -Force
                }

                $utf8 = New-Object System.Text.UTF8Encoding($false)

                [IO.File]::WriteAllText(
                    $path,
                    $formattedText,
                    $utf8
                )

                $formatted++

                Write-Host "Formatted: $($project.Name)" `
                    -ForegroundColor Green
            }
        }
        catch {
            $failed++
            Write-Warning "Failed: $($project.FullName): $_"
        }
    }

    Write-Host "`nFormatting complete." -ForegroundColor Cyan
    Write-Host "Formatted: $formatted"
    Write-Host "Unchanged: $unchanged"
    Write-Host "Failed: $failed"
}

function Repair-SDK1032 {
    [CmdletBinding()]
    param(
        [string]$Root = "C:\source\Indie-Games\indie-game-archive",
        [string]$Game,
        [switch]$All,
        [ValidateSet("x86", "x64", "Both")]
        [string]$Platform = "x64",
        [string]$Framework = "net9.0-windows",
        [switch]$Restore,
        [switch]$Build
    )

    $ErrorActionPreference = "Stop"

    if ($All) {
        $folders = @(Get-ChildItem -LiteralPath $Root -Directory)
    }
    elseif ($Game) {
        $path = if ([IO.Path]::IsPathRooted($Game)) {
            $Game
        }
        else {
            Join-Path $Root $Game
        }

        if (-not (Test-Path -LiteralPath $path -PathType Container)) {
            throw "Game folder not found: $path"
        }

        $folders = @(Get-Item -LiteralPath $path)
    }
    else {
        throw "Specify -Game or -All"
    }

    $platforms = if ($Platform -eq "Both") {
        @("x86", "x64")
    }
    else {
        @($Platform)
    }

    foreach ($folder in $folders) {
        $projects = @(
            Get-ChildItem -LiteralPath $folder.FullName `
                -Filter "*.csproj" -File |
                Where-Object { $_.BaseName -eq $folder.Name }
        )

        foreach ($project in $projects) {
            Write-Host "`nProject: $($project.Name)" -ForegroundColor Cyan

            $xml = New-Object System.Xml.XmlDocument
            $xml.PreserveWhitespace = $true
            $xml.Load($project.FullName)

            $rootNode = $xml.DocumentElement
            $ns = $rootNode.NamespaceURI

            # Preserve the original project before editing.
            $backup = "$($project.FullName).sdk1032.bak"

            if (-not (Test-Path -LiteralPath $backup)) {
                Copy-Item -LiteralPath $project.FullName -Destination $backup
            }

            # Remove conflicting architecture properties from
            # unconditional groups and platform-specific groups.
            # Leave unrelated conditional configurations untouched.
            foreach ($group in @($rootNode.ChildNodes)) {
                if (
                    $group -isnot [System.Xml.XmlElement] -or
                    $group.LocalName -ne "PropertyGroup"
                ) {
                    continue
                }

                $condition = $group.GetAttribute("Condition")

                $isPlatformGroup = (
                    -not $condition -or
                    $condition -match "Platform"
                )

                if (-not $isPlatformGroup) {
                    continue
                }

                foreach ($child in @($group.ChildNodes)) {
                    if (
                        $child -is [System.Xml.XmlElement] -and
                        $child.LocalName -in @(
                            "PlatformTarget",
                            "RuntimeIdentifier"
                        )
                    ) {
                        [void]$group.RemoveChild($child)
                    }
                }
            }

            # Append explicit architecture mappings.
            foreach ($arch in @("x86", "x64")) {
                $rid = "win-$arch"

                $group = $xml.CreateElement("PropertyGroup", $ns)
                $group.SetAttribute(
                    "Condition",
                    "'`$(Platform)' == '$arch'"
                )

                $target = $xml.CreateElement("PlatformTarget", $ns)
                $target.InnerText = $arch
                [void]$group.AppendChild($target)

                $runtime = $xml.CreateElement("RuntimeIdentifier", $ns)
                $runtime.InnerText = $rid
                [void]$group.AppendChild($runtime)

                [void]$rootNode.AppendChild($group)
            }

            # Ensure both runtime identifiers are available.
            $runtimeIdentifiers = $xml.SelectSingleNode(
                "//*[local-name()='RuntimeIdentifiers' and not(@Condition)]"
            )

            if (-not $runtimeIdentifiers) {
                $group = $xml.CreateElement("PropertyGroup", $ns)
                $runtimeIdentifiers = $xml.CreateElement(
                    "RuntimeIdentifiers", $ns
                )
                [void]$group.AppendChild($runtimeIdentifiers)
                [void]$rootNode.AppendChild($group)
            }

            $runtimeIdentifiers.InnerText = "win-x86;win-x64"

            $xml.Save($project.FullName)

            Write-Host "Updated architecture settings." -ForegroundColor Green
            Write-Host "Backup: $backup"

            foreach ($arch in $platforms) {
                $rid = "win-$arch"

                $properties = @(
                    "-p:TargetFramework=$Framework"
                    "-p:Platform=$arch"
                    "-p:PlatformTarget=$arch"
                    "-p:RuntimeIdentifier=$rid"
                    "-p:Configuration=Debug"
                )

                if ($Restore -or $Build) {
                    Write-Host "Restoring $arch..." -ForegroundColor Yellow

                    & dotnet restore $project.FullName @properties

                    if ($LASTEXITCODE -ne 0) {
                        Write-Warning "Restore failed [$arch]"
                        continue
                    }
                }

                if ($Build) {
                    Write-Host "Building $arch..." -ForegroundColor Yellow

                    & dotnet build $project.FullName `
                        --no-restore @properties

                    if ($LASTEXITCODE -ne 0) {
                        Write-Warning "Build failed [$arch]"
                        continue
                    }
                }

                Write-Host "Configured: $arch / $rid" -ForegroundColor Green
            }
        }
    }

    Write-Host "`nSDK1032 repair finished." -ForegroundColor Cyan
}

#$IndieGamesRoot = "C:\source\Indie-Games"
#$Folder = "Decay - The Mare - Episode 1 (World) (XBLIG)"
#$Game = Join-Path $IndieGamesRoot "indie-game-archive\$Folder"
#Rename-DllProjects -Game $Folder

function Repair-LegacyProjectPlatforms {
    param(
        [string]$Root = "C:\source\Indie-Games",

        [string[]]$ProjectNames = @(
            "FNA.Steamworks.csproj",
            "Steamworks.NET.csproj"
        )
    )

    foreach ($projectName in $ProjectNames) {
        $projects = @(
            Get-ChildItem -Path $Root -Filter $projectName -Recurse -File
        )

        foreach ($project in $projects) {
            Write-Host "Repairing: $($project.FullName)" -ForegroundColor Cyan

            [xml]$xml = Get-Content -LiteralPath $project.FullName

            $namespace = $xml.DocumentElement.NamespaceURI

            foreach ($configuration in @("Debug", "Release")) {
                foreach ($platform in @("x86", "x64")) {

                    $condition = "'`$(Configuration)|`$(Platform)' == '$configuration|$platform'"

                    $existing = @($xml.Project.PropertyGroup) |
                        Where-Object { $_.Condition -eq $condition }

                    if ($existing.Count -gt 0) {
                        Write-Host "Already configured: $configuration|$platform"
                        continue
                    }

                    $group = $xml.CreateElement("PropertyGroup", $namespace)
                    $group.SetAttribute("Condition", $condition)

                    $output = $xml.CreateElement("OutputPath", $namespace)
                    $output.InnerText = "bin\$platform\$configuration\"
                    [void]$group.AppendChild($output)

                    $intermediate = $xml.CreateElement("IntermediateOutputPath", $namespace)
                    $intermediate.InnerText = "obj\$platform\$configuration\"
                    [void]$group.AppendChild($intermediate)

                    $target = $xml.CreateElement("PlatformTarget", $namespace)
                    $target.InnerText = "AnyCPU"
                    [void]$group.AppendChild($target)

                    [void]$xml.DocumentElement.AppendChild($group)

                    Write-Host "Added: $configuration|$platform" -ForegroundColor Green
                }
            }

            Copy-Item -LiteralPath $project.FullName `
                -Destination "$($project.FullName).bak" -Force

            $xml.Save($project.FullName)
        }
    }
}
