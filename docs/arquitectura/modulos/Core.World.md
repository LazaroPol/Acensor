<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.World`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/World`
Tests: `Assets/_Project/Tests/EditMode/Core/World`

**Depende de:** [Core.Common](Core.Common.md), [Core.Gathering](Core.Gathering.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md), [Runtime.Sandbox.World](Runtime.Sandbox.World.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `HouseBlueprint.cs` | class `HouseBlueprint` | Despiece de una casa del poblado con las piezas modulares del pack medieval (módulos de 2 m): suelo de madera, muros con puerta centrada en  |
| `HousePiece.cs` | struct `HousePiece` | Pieza modular de una casa en coordenadas locales (centro de la casa en el origen, fachada hacia +Z). |
| `HousePieceKind.cs` | enum `HousePieceKind` |  |
| `HouseSpot.cs` | class `HouseSpot` | Casa del poblado: centro, giro (la puerta mira a +Z local), planta en metros (múltiplos de 2: el módulo de muro del pack mide 2 m) y estilo  |
| `HouseStyle.cs` | enum `HouseStyle` | Acabado de los muros de una casa del pueblo medieval. |
| `MapProjection.cs` | class `MapProjection` | Convierte coordenadas del poblado (m) a coordenadas normalizadas 0–1 del mapa (0,0 = esquina inferior izquierda). |
| `MapZone.cs` | class `MapZone` | Zona con nombre del mapa (rectángulo en el plano XZ) y lo que se hace allí. |
| `VillageLayout.Nature.cs` | class `VillageLayout` | Árboles, hierbas, rocas y vetas colocados de forma determinista (semilla fija) lejos de caminos y casas. |
| `VillageLayout.Paths.cs` | class `VillageLayout` | Caminos empedrados (baldosas de 2 m): Armería → plaza y de la plaza al bosque, la cantera y el campamento. |
| `VillageLayout.cs` | class `VillageLayout` | Mapa del poblado medieval que rodea la Armería (sala de 22 × 18 m en el origen, puerta al sur): plaza con casas, caminos, bosque de ceniza a |
| `WorldSpot.cs` | struct `WorldSpot` | Algo colocado en el mundo: qué es (id), dónde (m, plano XZ) y hacia dónde mira (grados, 0 = +Z). |
