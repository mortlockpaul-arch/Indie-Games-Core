param([string]$ArtifactRoot = (Join-Path ([System.IO.Path]::GetTempPath()) ("BuildManagerLayout-" + [guid]::NewGuid().ToString('N'))))
$ErrorActionPreference = 'Stop'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$layoutRoot = [System.IO.Path]::GetFullPath($ArtifactRoot)
$managerRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..\..'))
if (Test-Path -LiteralPath $layoutRoot) { throw 'Choose a new artifact directory; tests do not overwrite existing files.' }
New-Item -ItemType Directory -Path "$layoutRoot\source\Shared", "$layoutRoot\source\one", "$layoutRoot\source\two" | Out-Null
'<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><TargetFramework>net10.0</TargetFramework></PropertyGroup></Project>' | Set-Content -LiteralPath "$layoutRoot\source\Shared\Shared.csproj"
'public class SharedValue { public static string Value => "central build passed"; }' | Set-Content -LiteralPath "$layoutRoot\source\Shared\Shared.cs"
$xml = @'
<Project Sdk="Microsoft.NET.Sdk">
 <PropertyGroup><TargetFramework>net10.0</TargetFramework><OutputType>Exe</OutputType><OutputPath>bin\custom</OutputPath></PropertyGroup>
 <ItemGroup><ProjectReference Include="..\Shared\Shared.csproj"/><Content Include="Content\**\*" CopyToOutputDirectory="PreserveNewest"/></ItemGroup>
 <Target Name="SyntheticNativeCopy" AfterTargets="Build"><WriteLinesToFile File="$(TargetDir)native-fixture.dll" Lines="fixture" Overwrite="true"/></Target>
</Project>
'@
foreach ($name in @('one', 'two')) {
    $project = "$layoutRoot\source\$name\Game.csproj"
    $xml | Set-Content -LiteralPath $project
    'System.Console.WriteLine(SharedValue.Value);' | Set-Content -LiteralPath "$layoutRoot\source\$name\Program.cs"
    New-Item -ItemType Directory -Path "$layoutRoot\source\$name\Content" | Out-Null
    'content fixture' | Set-Content -LiteralPath "$layoutRoot\source\$name\Content\demo.txt"
    $arguments = @('build', $project, '--disable-build-servers', '-m:1', '-v:q', '-p:Configuration=Release', '-p:Platform=x64',
        "-p:CustomAfterDirectoryBuildProps=$managerRoot\scripts\BuildManager.Layout.props",
        "-p:CustomBeforeMicrosoftCommonTargets=$managerRoot\scripts\BuildManager.Layout.targets",
        "-p:BuildManagerCache=$layoutRoot\cache", "-p:BuildManagerMainProject=$project", "-p:BuildManagerGameOutput=$layoutRoot\games\$name")
    & dotnet @arguments
    if ($LASTEXITCODE -ne 0) { throw "Build failed: $project" }
    foreach ($file in @('Game.exe', 'Game.deps.json', 'Game.runtimeconfig.json', 'Shared.dll', 'Content\demo.txt', 'native-fixture.dll')) {
        if (!(Test-Path -LiteralPath "$layoutRoot\games\$name\$file")) { throw "Missing playable output: $file" }
    }
    $result = & "$layoutRoot\games\$name\Game.exe"
    if ($LASTEXITCODE -ne 0 -or $result -ne 'central build passed') { throw 'Playable output did not execute correctly.' }
}
$localOutput = Get-ChildItem -LiteralPath "$layoutRoot\source" -Recurse -Directory | Where-Object { $_.Name -in @('bin','obj') }
if ($localOutput) { throw 'Build created output in source.' }
$shared = @(Get-ChildItem -LiteralPath "$layoutRoot\cache\bin" -Directory | Where-Object Name -like 'Shared-*')
$gameObjects = @(Get-ChildItem -LiteralPath "$layoutRoot\cache\obj" -Directory | Where-Object Name -like 'Game-*')
if ($shared.Count -ne 1 -or $gameObjects.Count -ne 2) { throw 'Dependencies were duplicated or same-name project caches collided.' }
Write-Output 'PASS two same-name games, shared dependency cache, overridden output path, Content/native post-build copies, execution and no source bin/obj'
Write-Output "TEST ROOT $layoutRoot"
