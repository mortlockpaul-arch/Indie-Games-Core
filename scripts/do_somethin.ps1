$folder = "C:\source\Indie-Games\indie-game-archive\Alien Jelly (World) (XBLIG)"
$project = Join-Path $folder "Alien Jelly (World) (XBLIG).csproj"
$assets = Join-Path $folder "obj\project.assets.json"

Write-Host "`n=== Project files ===" -ForegroundColor Cyan
Get-ChildItem $folder -Filter *.csproj |
    Select-Object Name

Write-Host "`n=== Effective MSBuild properties ===" -ForegroundColor Cyan
dotnet msbuild $project `
    -getProperty:TargetFramework,TargetFrameworks,ProjectAssetsFile,MSBuildProjectExtensionsPath

Write-Host "`n=== Restored frameworks ===" -ForegroundColor Cyan
if (Test-Path $assets) {
    $json = Get-Content $assets -Raw | ConvertFrom-Json
    $json.targets.PSObject.Properties.Name
}
else {
    Write-Host "Assets file missing!" -ForegroundColor Red
}

Write-Host "`n=== Restore diagnostics ===" -ForegroundColor Cyan
dotnet restore $project --force -v:minimal