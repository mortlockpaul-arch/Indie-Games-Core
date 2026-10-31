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

$project = "C:\source\Indie-Games\FNA.Contents\FNA.Contents.csproj"

dotnet clean $project -c Debug -p:Platform=x64

dotnet restore $project -p:Platform=x64

dotnet build $project -c Debug -p:Platform=x64

$fnaProject = "C:\source\Indie-Games\FNA\FNA.NetFramework.csproj"
$msbuild = Get-MSBuildPath

Write-Host "Using MSBuild: $msbuild" -ForegroundColor Cyan
& $msbuild $fnaProject `
    /restore `
    /t:Build `
    /p:Configuration=Debug `
    /p:Platform=x64 `
    /v:minimal