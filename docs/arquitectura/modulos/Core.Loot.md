<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Loot`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Loot`
Tests: `Assets/_Project/Tests/EditMode/Core/Loot`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** [Core.Floors](Core.Floors.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DepthRings.cs` | class `DepthRings` | Anillos de profundidad de un piso (1 = cerca del ascensor, 3 = el más profundo). |
| `IRingWeighting.cs` | interface `IRingWeighting` | Estrategia que ajusta el peso de una rareza según el anillo de profundidad. |
| `LootDrop.cs` | struct `LootDrop` | Resultado de una tirada: un objeto y cuántas unidades han caído. |
| `LootEntry.cs` | class `LootEntry` | Fila de una tabla de loot: qué objeto, con qué peso base, desde qué anillo aparece y cuántas unidades caen (solo los apilables pueden caer e |
| `LootTable.cs` | class `LootTable` | Tabla de loot ponderada por anillo de profundidad. |
| `RarityRingWeighting.cs` | class `RarityRingWeighting` | Ponderación por defecto: en el anillo 1 se respetan los pesos base; cada anillo más profundo suma bonusPerStep × nivel de rareza al multipli |
