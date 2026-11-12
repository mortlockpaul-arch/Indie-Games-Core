Shader packaging and deployment patch for IndieGames.BuildManager

Apply to C:\source\Indie-Games-Core\IndieGames.BuildManager\ after backing up your existing files:
  MainWindow.xaml
  MainWindow.xaml.cs
  Services\ShaderRecovery.cs
  Services\ShaderDeployment.cs (new)

Prerequisite: run Recover HLSL + Compile from the earlier patch first.
Select game(s), click Package / Deploy Recovered Shaders.
Without the Deploy checkbox, creates build\shader-recovery\packaged-xnb\... only.
With the checkbox, asks for confirmation, verifies the original XNB SHA-256 against recovery reports, backs up original Content XNBs to shader-repair-backups\recovered-*, and installs packaged XNBs.
If a write fails, restores assets already replaced during this operation.

IMPORTANT: Successful fxc compilation and packaging are not visual/runtime validation. Deploy only reviewed effects, test in-game, and retain backups. This does not add an automatic MSBuild deployment hook. Existing KnownShaderRepair stays unchanged.

Build on Windows:
  dotnet build "C:\source\Indie-Games-Core\IndieGames.BuildManager\IndieGames.BuildManager.csproj" -c Debug -p:Platform=x64

Not compiled/tested in the user's Windows environment.
