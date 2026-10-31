param(
    [Parameter(Mandatory)]
    [string]$Path
)

$bytes = [System.IO.File]::ReadAllBytes($Path)

if ([System.Text.Encoding]::ASCII.GetString($bytes, 0, 4) -ne "KBDS") {
    throw "Not a valid XSB sound bank."
}

$text = [System.Text.Encoding]::ASCII.GetString($bytes)

[regex]::Matches($text, '[\x20-\x7E]{3,}') |
    ForEach-Object { $_.Value } |
    Sort-Object -Unique