param([string]$SourceDirectory,[string]$OutputDirectory)
$ErrorActionPreference='Stop'
$fxc=Get-ChildItem -LiteralPath (Join-Path ${env:ProgramFiles(x86)} 'Windows Kits\10\bin') -Directory |
 Where-Object {$_.Name -match '^\d+\.\d+\.\d+\.\d+$'} | Sort-Object {[version]$_.Name} -Descending |
 ForEach-Object {Join-Path $_.FullName 'x64\fxc.exe'} | Where-Object {Test-Path -LiteralPath $_} | Select-Object -First 1
if (!$fxc) {throw 'Install the Windows SDK shader compiler to build the browser.'}
[IO.Directory]::CreateDirectory($OutputDirectory)|Out-Null
foreach ($file in Get-ChildItem -LiteralPath $SourceDirectory -Filter '*.fx' -File) {
 $output=Join-Path $OutputDirectory ($file.BaseName+'.fxb')
 if (!(Test-Path -LiteralPath $output) -or $file.LastWriteTimeUtc -gt (Get-Item -LiteralPath $output).LastWriteTimeUtc) {
  & $fxc /nologo /T fx_2_0 /O3 /Fo $output $file.FullName
  if ($LASTEXITCODE -ne 0) {throw ('Could not compile '+$file.Name)}
 }
}
