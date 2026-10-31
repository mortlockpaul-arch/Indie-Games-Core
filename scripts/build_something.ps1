$folder = "C:\source\Indie-Games\indie-game-archive\Alien Jelly (World) (XBLIG)"
$project = Join-Path $folder "Alien Jelly (World) (XBLIG).csproj"

# Remove the old shared assets.
Remove-Item "$folder\obj" -Recurse -Force -ErrorAction SilentlyContinue

# Restore the game.
dotnet restore $project --force

# Verify the new assets location.
dotnet msbuild $project -getProperty:ProjectAssetsFile

# Verify the target framework.
dotnet msbuild $project -getProperty:TargetFramework