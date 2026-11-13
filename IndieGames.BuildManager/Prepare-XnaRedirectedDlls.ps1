[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$Project,
    [Parameter(Mandatory)][string]$CoreRoot,
    [ValidateSet('x86','x64','AnyCPU')][string]$Architecture = 'x64'
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
if (!(Test-Path -LiteralPath $Project -PathType Leaf)) { throw "Project missing: $Project" }
$game = Split-Path -Parent (Resolve-Path -LiteralPath $Project).Path
$redirector = Join-Path $CoreRoot 'tools\XnaAssemblyRedirector\bin\Release\net9.0\XnaAssemblyRedirector.dll'
if (!(Test-Path -LiteralPath $redirector)) { throw "Build XnaAssemblyRedirector first: $redirector" }

# Prefer the architecture-specific FNA assemblies. Never use an arbitrary assembly from the game's output.
function Find-ManagedDll([string]$name, [string]$subproject) {
    $base = Join-Path $CoreRoot $subproject
    $candidates = @(Get-ChildItem -LiteralPath $base -Filter $name -File -Recurse -ErrorAction Stop |
        Where-Object { $_.FullName -match '[\\/]bin[\\/]' -and $_.FullName -notmatch '[\\/](obj|publish)[\\/]' })
    $ordered = @($candidates | Sort-Object -Property @{Expression={
        if ($_.FullName -match "[\\/]$Architecture[\\/]") { 0 }
        elseif ($_.FullName -match '[\\/](x86|x64)[\\/]') { 2 }
        else { 1 }
    }}, @{Expression={if ($_.FullName -match '[\\/]Release[\\/]') {0} else {1}}}, FullName)
    if ($ordered.Count -eq 0) { throw "Cannot find $name under $base" }
    return $ordered[0].FullName
}
$fna = Find-ManagedDll 'FNA.dll' 'FNA'
$netstub = Find-ManagedDll 'FNA.NetStub.dll' 'FNA.NetStub'
Write-Host "Pre-build FNA: $fna"
Write-Host "Pre-build NetStub: $netstub"

# Only scan known original binary folders; do not scan existing patched copies or bin/obj.
$inputs = @((Join-Path $game '584E07D1'), (Join-Path $game '584E07D2')) |
    Where-Object { Test-Path -LiteralPath $_ -PathType Container }
if ($inputs.Count -eq 0) {
    Write-Host 'No original 584E07D1/584E07D2 folders; pre-build redirection skipped.'
    exit 0
}
$patched = Join-Path $game 'XnaRedirect-Patched'
New-Item -ItemType Directory -Path $patched -Force | Out-Null

foreach ($inputDir in $inputs) {
    $temp = Join-Path $game ('.xna-redirect-stage-' + [guid]::NewGuid().ToString('N'))
    try {
        New-Item -ItemType Directory -Path $temp -Force | Out-Null
        Write-Host "=== Verifying original DLLs: $inputDir ==="
        & dotnet $redirector --mode verify --input $inputDir --fna $fna --netstub $netstub
        if ($LASTEXITCODE -ne 0) { throw "Redirector verify failed for $inputDir" }
        Write-Host "=== Applying compatible redirects: $inputDir ==="
        & dotnet $redirector --mode apply --input $inputDir --output $temp --fna $fna --netstub $netstub
        if ($LASTEXITCODE -ne 0) { throw "Redirector apply failed for $inputDir" }
        foreach ($dll in @(Get-ChildItem -LiteralPath $temp -Filter '*.dll' -File -Recurse)) {
            $destination = Join-Path $patched $dll.Name
            Copy-Item -LiteralPath $dll.FullName -Destination $destination -Force
            Write-Host "Prepared: $destination"
        }
    }
    finally { if (Test-Path -LiteralPath $temp) { Remove-Item -LiteralPath $temp -Recurse -Force } }
}

# Only replace existing matching project references. Do not add arbitrary unused DLLs.
[xml]$xml = Get-Content -LiteralPath $Project -Raw
$changed = 0
foreach ($reference in @($xml.SelectNodes("//*[local-name()='Reference']"))) {
    $include = $reference.GetAttribute('Include')
    $simple = ($include -split ',')[0].Trim()
    $dll = Join-Path $patched ($simple + '.dll')
    if (!(Test-Path -LiteralPath $dll)) { continue }
    $hint = @($reference.ChildNodes | Where-Object { $_.LocalName -eq 'HintPath' }) | Select-Object -First 1
    if ($null -eq $hint) {
        $hint = $xml.CreateElement('HintPath', $xml.DocumentElement.NamespaceURI)
        [void]$reference.AppendChild($hint)
    }
    if ($hint.InnerText -ne $dll) { $hint.InnerText = $dll; $changed++ }
}

# Prioritize HintPath without altering global Build Manager or unrelated game projects.
$targets = Join-Path $game 'PatchedAssemblyReferences.targets'
$targetsContent = @'
<Project>
  <PropertyGroup>
    <AssemblySearchPaths>{HintPathFromItem};{CandidateAssemblyFiles};{TargetFrameworkDirectory};{RawFileName}</AssemblySearchPaths>
  </PropertyGroup>
</Project>
'@
$import = $xml.SelectSingleNode("/*[local-name()='Project']/*[local-name()='Import'][@Project='PatchedAssemblyReferences.targets']")
$hasPatched = @(Get-ChildItem -LiteralPath $patched -Filter '*.dll' -File).Count -gt 0
if ($hasPatched -and $null -eq $import) {
    $import = $xml.CreateElement('Import', $xml.DocumentElement.NamespaceURI)
    $import.SetAttribute('Project', 'PatchedAssemblyReferences.targets')
    [void]$xml.DocumentElement.AppendChild($import)
    $changed++
}
if ($hasPatched) { Set-Content -LiteralPath $targets -Value $targetsContent -Encoding UTF8 }
if ($changed -gt 0) {
    $backup = "$Project.prebuild-$(Get-Date -Format yyyyMMdd-HHmmss).bak"
    Copy-Item -LiteralPath $Project -Destination $backup
    $xml.Save($Project)
    Write-Host "Updated $changed project reference/import setting(s). Backup: $backup"
} else { Write-Host 'Project references already prepared; no project edits needed.' }
Write-Host 'Pre-build XNA preparation complete.'
