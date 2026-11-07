#Requires -Version 5.1
[CmdletBinding(SupportsShouldProcess=$true, ConfirmImpact='High')]
param(
 [string]$Repository='mortlockpaul-arch/Indie-Games-Catalogue',
 [switch]$Apply,
 [switch]$CloseObsolete,
 [string]$Numbers='',
 [int]$Limit=200,
 [string]$ReportPath=''
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
function GhJson([string[]]$Argv) {
 $output = & gh @Argv 2>&1
 if ($LASTEXITCODE -ne 0) { throw "gh $($Argv -join ' ') failed: $($output -join ' ')" }
 $raw=($output | Out-String).Trim()
 if (-not $raw) { return $null }
 $parsed = ConvertFrom-Json -InputObject $raw
 # Windows PowerShell 5.1 can return a JSON array as one nested object.
 # Emit each array element separately so callers receive individual PR/check rows.
 if ($parsed -is [System.Array]) {
  foreach ($item in $parsed) { Write-Output $item }
 } else { Write-Output $parsed }
}
function Key([string]$title) { return (($title -replace '^Catalogue submission:\s*','').Trim().ToLowerInvariant() -replace '\s+',' ') }
function Check([int]$n) {
 $c=@(GhJson @('pr','checks',"$n",'--repo',$Repository,'--json','name,state,bucket'))
 if ($c.Count -eq 0 -or ($c.Count -eq 1 -and $null -eq $c[0])) { return 'NO_CHECKS' }
 if (@($c | Where-Object { $_.state -in @('FAILURE','ERROR','CANCELLED','TIMED_OUT','ACTION_REQUIRED','STARTUP_FAILURE') }).Count) { return 'FAILED' }
 if (@($c | Where-Object { $_.state -notin @('SUCCESS','SKIPPED','NEUTRAL') }).Count) { return 'PENDING' }
 if (@($c | Where-Object { $_.name -eq 'validate' -and $_.state -eq 'SUCCESS' }).Count -eq 0) { return 'VALIDATE_NOT_CONFIRMED' }
 return 'PASSED'
}
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) { throw 'GitHub CLI gh not found.' }
$me=GhJson @('api','user','--jq','{login:.login}')
$repo=GhJson @('api',"repos/$Repository",'--jq','{permissions:.permissions,default_branch:.default_branch}')
if (-not $repo.permissions.admin) { throw "Account $($me.login) lacks repository admin permissions." }
$open=@(GhJson @('pr','list','--repo',$Repository,'--state','open','--limit',"$Limit",'--json','number,title,author,isDraft,baseRefName,mergeable,createdAt'))
$selected=@()
if($Numbers.Trim()) { $selected=@($Numbers.Split(',') | ForEach-Object { [int]$_.Trim() }) }
$rows=@()
foreach($p in ($open | Sort-Object number)) {
 if($selected.Count -and $p.number -notin $selected) { continue }
 $k=Key $p.title; $status='SKIP'; $reason=''
 if($p.title -notmatch '^Catalogue submission:\s*') { $reason='Not a catalogue submission' }
 elseif($p.author.login -ne $me.login) { $reason='Other author: requires normal review' }
 elseif($p.baseRefName -ne $repo.default_branch) { $reason='Wrong base branch' }
 elseif($p.isDraft) { $reason='Draft' }
 elseif($p.mergeable -eq 'CONFLICTING') { $reason='Merge conflict; manual resolution required' }
 elseif((Check ([int]$p.number)) -ne 'PASSED') { $reason='Checks not passing (or GitHub check data unavailable)' }
 else { $status='READY'; $reason='Submission validate passed' }
 $rows += [pscustomobject]@{Number=[int]$p.number;Status=$status;Author=$p.author.login;Title=$p.title;Reason=$reason;GameKey=$k}
}
Write-Host "Diagnostic: open=$($open.Count); ready=$(@($rows | Where-Object Status -eq 'READY').Count)"
$rows | Format-Table Number,Status,Author,Title,Reason -AutoSize -Wrap
if($ReportPath) {
 $parent=Split-Path -Parent $ReportPath
 if($parent -and -not (Test-Path -LiteralPath $parent)) { New-Item -ItemType Directory -Force -Path $parent | Out-Null }
 $rows | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath $ReportPath -Encoding UTF8
}
if(-not $Apply) { Write-Host 'Preview only. Use -Apply to merge. Each version and package is reviewed independently.'; return }
$mergedNow=@{}
foreach($r in @($rows | Where-Object Status -eq 'READY')) {
 if(-not $PSCmdlet.ShouldProcess("PR #$($r.Number)",'Squash merge with admin bypass')) { continue }
 try {
  $current=GhJson @('pr','view',"$($r.Number)",'--repo',$Repository,'--json','state,mergeable,title,author,baseRefName')
  if($current.state -ne 'OPEN' -or $current.mergeable -eq 'CONFLICTING' -or $current.author.login -ne $me.login -or $current.baseRefName -ne $repo.default_branch -or (Key $current.title) -ne $r.GameKey) { Write-Warning "PR #$($r.Number) changed; skipped"; continue }
  if((Check $r.Number) -ne 'PASSED') { Write-Warning "PR #$($r.Number) checks no longer pass"; continue }
  & gh pr merge "$($r.Number)" --repo $Repository --squash --admin
  if($LASTEXITCODE -ne 0) { Write-Warning "Merge failed #$($r.Number)"; continue }
  $mergedNow[$r.GameKey]=$true
  Write-Host "Merged #$($r.Number)" -ForegroundColor Green
 } catch { Write-Warning "Merge #$($r.Number): $($_.Exception.Message)" }
}
if($CloseObsolete) {
 Write-Warning 'Submissions are no longer closed by game title. Different versions and Source/Game snapshots may share a title; review duplicate snapshot hashes manually.'
}
Write-Host 'Finished. Review warnings and the GitHub pull request list.'
