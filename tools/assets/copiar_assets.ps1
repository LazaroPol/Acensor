# Copia los packs de arte de terceros (carpeta Assets_pack) a Assets/_Project/Art/ThirdParty.
# Esa carpeta está en .gitignore: cada persona del equipo la rellena en su copia local.
# Uso (PowerShell, desde la raíz del repo):
#   powershell -ExecutionPolicy Bypass -File tools/assets/copiar_assets.ps1 -Origen "C:\ruta\Assets_pack"
param([Parameter(Mandatory = $true)][string]$Origen)
$ErrorActionPreference = "Stop"
$Destino = Join-Path $PSScriptRoot "..\..\Assets\_Project\Art\ThirdParty"
$Paquetes = Join-Path $PSScriptRoot "..\..\ThirdPartyPackages"

function Copiar($desde, $hacia, $filtro = "*") {
    $ruta = Join-Path $Origen $desde
    if (-not (Test-Path -LiteralPath $ruta)) { Write-Warning "No encontrado: $desde"; return }
    $dst = Join-Path $Destino $hacia
    New-Item -ItemType Directory -Force -Path $dst | Out-Null
    Get-ChildItem -LiteralPath $ruta -Filter $filtro -File | Copy-Item -Destination $dst -Force
}

$ubc = "Universal Base Characters[Standard]\Universal Base Characters[Standard]"
Copiar "$ubc\Base Characters\Unity" "Q_Personajes" "*.fbx"
Copiar "$ubc\Base Characters\Textures" "Q_Personajes\Textures" "*.png"
Copiar "$ubc\Hairstyles\Rigged to Head Bone\FBX (Unity)" "Q_Personajes\Pelo" "*.fbx"
Copiar "$ubc\Hairstyles\Textures" "Q_Personajes\Pelo\Textures" "*.png"
$out = "Modular Character Outfits - Fantasy[Standard]\Modular Character Outfits - Fantasy[Standard]"
Copiar "$out\Exports\FBX (Unity)\Outfits" "Q_Atuendos" "*.fbx"
Copiar "$out\Exports\FBX (Unity)\Modular Parts" "Q_Atuendos\Partes" "*.fbx"
foreach ($t in "Base", "Peasant", "Ranger") { Copiar "$out\Textures\$t" "Q_Atuendos\Textures" "*.png" }
Copiar "Universal Animation Library[Standard]\Universal Animation Library[Standard]\Unity" "Q_Animaciones" "UAL1_Standard.fbx"
Copiar "Universal Animation Library 2[Standard]\Universal Animation Library 2[Standard]\Unity" "Q_Animaciones" "UAL2_Standard.fbx"
$bes = "Bestiary - Dungeon Monsters Kit[Standard]\Bestiary - Dungeon Monsters Kit[Standard]"
Copiar "$bes\Exports\FBX (Unity)" "Q_Bestiario" "*.fbx"
Copiar "$bes\Textures" "Q_Bestiario\Textures" "*.png"
$vil = "Medieval Village MegaKit[Standard]\Medieval Village MegaKit[Standard]"
Copiar "$vil\FBX" "Q_Pueblo" "*.fbx"
Copiar "$vil\Textures" "Q_Pueblo\Textures" "*.png"
Copiar "$vil\Textures\Normals Godot-Unity" "Q_Pueblo\Textures" "*.png"
Copiar "KayKit_RPGToolsBits_1.0_FREE\Assets\fbx(unity)" "KayKit_Herramientas"
$armas = Join-Path $Origen "RPGWeapons_Free\Free\FBX"
if (Test-Path -LiteralPath $armas) {
    Get-ChildItem -LiteralPath $armas -Directory | ForEach-Object { Copiar "RPGWeapons_Free\Free\FBX\$($_.Name)" "RPGWeapons\$($_.Name)" }
}
New-Item -ItemType Directory -Force -Path $Paquetes | Out-Null
Get-ChildItem -LiteralPath $Origen -Filter "*.unitypackage" -File | Copy-Item -Destination $Paquetes -Force
Write-Host "Listo. Abre Unity y usa el menú SPECIMEN > Arte."
