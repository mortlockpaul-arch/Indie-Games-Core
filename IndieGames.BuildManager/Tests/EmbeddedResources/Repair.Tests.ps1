$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot '..\..\scripts\Repair-EmbeddedResources.ps1')
$root = Join-Path ([IO.Path]::GetTempPath()) ('resource-repair-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $root | Out-Null
foreach ($mode in @('disabled', 'default', 'wildcard', 'namespaced')) {
    $folder = Join-Path $root $mode
    New-Item -ItemType Directory -Path $folder | Out-Null
    $defaults = if ($mode -eq 'default') { '' } else { '<EnableDefaultEmbeddedResourceItems>false</EnableDefaultEmbeddedResourceItems>' }
    $items = if ($mode -eq 'wildcard') { '<ItemGroup><EmbeddedResource Include="Example.Strings*.resx"/></ItemGroup>' } else { '' }
    $namespace = if ($mode -eq 'namespaced') { ' xmlns="http://schemas.microsoft.com/developer/msbuild/2003"' } else { '' }
    $project = Join-Path $folder 'Game.csproj'
    [IO.File]::WriteAllText($project, "<Project Sdk=`"Microsoft.NET.Sdk`"$namespace><PropertyGroup><TargetFramework>net10.0</TargetFramework><AssemblyName>ResourceTest.$mode</AssemblyName>$defaults</PropertyGroup>$items</Project>")
    [IO.File]::WriteAllText((Join-Path $folder 'Strings.cs'), 'namespace Example { public class Strings { public static System.Resources.ResourceManager Manager = new System.Resources.ResourceManager("Example.Strings", typeof(Strings).Assembly); } }')
    foreach ($culture in @('', '.de')) {
        $value = if ($culture) { 'Hallo' } else { 'Hello' }
        [IO.File]::WriteAllText((Join-Path $folder "Example.Strings$culture.resx"), "<root><data name=`"Greeting`" xml:space=`"preserve`"><value>$value</value></data></root>")
    }
    $before = [IO.File]::ReadAllText($project)
    Repair-EmbeddedResources -Root $folder -WhatIf
    if ([IO.File]::ReadAllText($project) -cne $before) { throw 'Preview changed a project' }
    Repair-EmbeddedResources -Root $folder
    if ([IO.File]::ReadAllText($project + '.before-resource-repair.bak') -cne $before) { throw 'Original backup mismatch' }
    $after = [IO.File]::ReadAllText($project)
    Repair-EmbeddedResources -Root $folder
    if ([IO.File]::ReadAllText($project) -cne $after) { throw 'Repair is not idempotent' }
    & dotnet build $project --nologo --verbosity quiet
    if ($LASTEXITCODE -ne 0) { throw "Resource build failed: $mode" }
    $assembly = [Reflection.Assembly]::LoadFrom((Join-Path $folder "bin\Debug\net10.0\ResourceTest.$mode.dll"))
    $manager = [Resources.ResourceManager]::new('Example.Strings', $assembly)
    if ($manager.GetString('Greeting', [Globalization.CultureInfo]::InvariantCulture) -ne 'Hello') { throw 'Neutral resource missing' }
    if ($manager.GetString('Greeting', [Globalization.CultureInfo]::GetCultureInfo('de')) -ne 'Hallo') { throw 'Localized resource missing' }
    Write-Host "PASS: $mode inclusion, resource names, localization, backup, preview and repeat run"
}
# Keep fixtures for diagnosis; the process holds loaded resource assemblies open on Windows.
Write-Host "Resource repair fixtures: $root"
