# Add to IndieGames.BuildManager\scripts\XnaRepairFunctions.ps1
# Adjust path if the fixer lives elsewhere.
. (Join-Path $PSScriptRoot 'Repair-XnaGamerPicture.ps1')

# The Build Manager's fixer registry/checkbox list must also include:
# Repair-XnaGamerPicture
# Then the existing dispatcher can call:
# & Repair-XnaGamerPicture -Root $gameFolder -WhatIf:$whatIf
