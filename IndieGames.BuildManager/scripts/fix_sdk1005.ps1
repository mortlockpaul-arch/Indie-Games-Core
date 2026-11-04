# Compatibility entry point used by the Python app and existing command lines.
# Shared SDK repair implementations live in BuildSdkRepairs.ps1.
[CmdletBinding()]
param(
    [string]$Root = 'D:\indie-game-archive',
    [string]$Folder,
    [ValidateSet('Rename', 'NetSdk1005', 'SharedAssets', 'SDK1047')]
    [string]$Action = 'NetSdk1005',
    [switch]$Restore,
    [switch]$Clean,
    [switch]$FixRuntimeIdentifiers,
    [string]$MainProject,
    [string]$Framework = 'net9.0-windows',
    [string]$Runtime = 'win-x64',
    [ValidateSet('x86', 'x64', 'Both')][string]$Platform = 'x64'
)
. (Join-Path $PSScriptRoot 'BuildSdkRepairs.ps1')
if ($MyInvocation.InvocationName -ne '.') {
    if ([string]::IsNullOrWhiteSpace($Folder)) { throw 'Specify the game folder with -Folder.' }
    switch ($Action) {
        'Rename' { Rename-DllProjects -Game $Folder -Root $Root -MainProject $MainProject -Restore:$Restore }
        'NetSdk1005' { Repair-NetSdk1005 -ProjectName $Folder -Root $Root -Framework $Framework }
        'SharedAssets' { Repair-SharedProjectAssets -Game $Folder -Root $Root -Clean:$Clean -Restore:$Restore -FixRuntimeIdentifiers:$FixRuntimeIdentifiers }
        'SDK1047' {
            if (-not $PSBoundParameters.ContainsKey('Platform') -and $Runtime -match '-(x86|x64)$') { $Platform = $Matches[1] }
            Repair-SDK1047 -Game $Folder -Root $Root -Framework $Framework -Platform $Platform -Clean:$Clean
        }
    }
}
