[CmdletBinding(SupportsShouldProcess=$true)]
param(
    [string]$Root = 'D:\indie-game-archive\This Is Hard (World) (XBLIG)\Content',
    [string]$Project = (Join-Path $PSScriptRoot 'XnaContentReaderRepair.csproj'),
    [string]$Output,
    [string]$From = 'Renderer',
    [string]$To = 'This Is Hard'
)
$ErrorActionPreference = 'Stop'
if (-not (Test-Path -LiteralPath $Root -PathType Container)) { throw "Input folder not found: $Root" }
if (-not (Test-Path -LiteralPath $Project -PathType Leaf)) { throw "Repair project not found: $Project" }
$arguments = @('run', '--project', $Project, '--', '--root', $Root, '--from', $From, '--to', $To)
if ($PSCmdlet.ShouldProcess($Root, 'Create patched XNB copies')) {
    if ([string]::IsNullOrWhiteSpace($Output)) { throw 'Specify -Output for patched copies.' }
    $arguments += @('--apply', '--output', $Output)
} else { $arguments += '--whatif' }
& dotnet @arguments
if ($LASTEXITCODE -notin @(0,3)) { throw "Repair tool failed with exit code $LASTEXITCODE" }
if ($LASTEXITCODE -eq 3) { Write-Warning 'Some unsupported compressed XNB files were skipped (for example LZ4). Other matching files may have been patched.' }
