[CmdletBinding()]
param([Parameter(Mandatory)][string]$ProjectRoot, [string]$IntermediateDirectory='', [string]$FxcPath='')
$ErrorActionPreference='Stop'
$ProjectRoot=[System.IO.Path]::GetFullPath($ProjectRoot).TrimEnd('\','/')
function Project-Path([string]$relative) {
    if ([System.IO.Path]::IsPathRooted($relative)) { throw 'Shader paths must be relative to the game project.' }
    $path=[System.IO.Path]::GetFullPath((Join-Path $ProjectRoot $relative))
    if (!$path.StartsWith($ProjectRoot+[System.IO.Path]::DirectorySeparatorChar,[System.StringComparison]::OrdinalIgnoreCase)) { throw 'Shader path escapes the game project.' }
    return $path
}
if (!$IntermediateDirectory) { $IntermediateDirectory=Join-Path $ProjectRoot 'obj\KnownShaders' }
if (!$FxcPath) {
    $command=Get-Command fxc.exe -ErrorAction SilentlyContinue
    if ($command) { $FxcPath=$command.Source }
    else {
        $sdkRoot=Join-Path ${env:ProgramFiles(x86)} 'Windows Kits\10\bin'
        if (Test-Path -LiteralPath $sdkRoot) {
            $FxcPath=Get-ChildItem -LiteralPath $sdkRoot -Directory | Where-Object {$_.Name -match '^\d+\.\d+\.\d+\.\d+$'} |
                Sort-Object {[version]$_.Name} -Descending | ForEach-Object {Join-Path $_.FullName 'x64\fxc.exe'} |
                Where-Object {Test-Path -LiteralPath $_} | Select-Object -First 1
        }
    }
}
if (!$FxcPath -or !(Test-Path -LiteralPath $FxcPath)) { throw 'Install the Windows SDK shader compiler (fxc.exe), or pass -FxcPath.' }
$manifest=Get-Content -LiteralPath (Join-Path $ProjectRoot 'build\KnownShaders.json') -Raw | ConvertFrom-Json
if ($manifest.Schema -ne 1) { throw 'Unsupported known-shader manifest version.' }
[System.IO.Directory]::CreateDirectory($IntermediateDirectory)|Out-Null
foreach ($effect in $manifest.Effects) {
    $source=Project-Path $effect.Source
    $asset=Project-Path $effect.Asset
    $compiled=Join-Path $IntermediateDirectory ([System.IO.Path]::GetFileNameWithoutExtension($source)+'.fxb')
    & $FxcPath /nologo /T fx_2_0 /O3 /Fo $compiled $source
    if ($LASTEXITCODE -ne 0) { throw ('Shader compilation failed: '+$source) }
    $bytes=[System.IO.File]::ReadAllBytes($compiled)
    $payload=[System.IO.MemoryStream]::new();$writer=[System.IO.BinaryWriter]::new($payload)
    $writer.Write([byte]1);$writer.Write('Microsoft.Xna.Framework.Content.EffectReader, FNA');$writer.Write([int]0)
    $writer.Write([byte]0);$writer.Write([byte]1);$writer.Write([int]$bytes.Length);$writer.Write($bytes);$writer.Flush()
    $data=$payload.ToArray();$writer.Dispose()
    $stream=[System.IO.MemoryStream]::new();$writer=[System.IO.BinaryWriter]::new($stream)
    $writer.Write([System.Text.Encoding]::ASCII.GetBytes('XNBw'));$writer.Write([byte]5);$writer.Write([byte]0)
    $writer.Write([int]($data.Length+10));$writer.Write($data);$writer.Flush()
    $xnb=$stream.ToArray();$writer.Dispose()
    [System.IO.File]::WriteAllBytes($asset,$xnb)
    Write-Output ('Built Windows shader: '+$asset)
}
