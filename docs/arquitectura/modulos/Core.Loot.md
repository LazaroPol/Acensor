<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Loot`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Loot`
Tests: `Assets/_Project/Tests/EditMode/Core/Loot`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** [Core.Combat.Enemies](Core.Combat.Enemies.md), [Core.Composition](Core.Composition.md), [Core.Crafting.Play](Core.Crafting.Play.md), [Core.Floors](Core.Floors.md), [Core.Floors.Population](Core.Floors.Population.md), [Core.Floors.Remnants](Core.Floors.Remnants.md), [Core.Gathering](Core.Gathering.md), [Core.Sandbox](Core.Sandbox.md), [Runtime.Extraction](Runtime.Extraction.md), [Runtime.Incursion](Runtime.Incursion.md), [Runtime.Sandbox](Runtime.Sandbox.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DepthRings.cs` | class `DepthRings` | Anillos de profundidad de un piso (1 = cerca del ascensor, 3 = el más profundo). |
| `IRingWeighting.cs` | interface `IRingWeighting` | Estrategia que ajusta el peso de una rareza según el anillo de profundidad. |
| `LootDrop.cs` | struct `LootDrop` | Resultado de una tirada: un objeto y cuántas unidades han caído. |
| `LootEntry.cs` | class `LootEntry` | Fila de una tabla de loot: qué objeto, con qué peso base, desde qué anillo aparece y cuántas unidades caen (solo los apilables pueden caer e |
| `LootTable.cs` | class `LootTable` | Tabla de loot ponderada por anillo de profundidad. |
| `RarityRingWeighting.cs` | class `RarityRingWeighting` | Ponderación por defecto: en el anillo 1 se respetan los pesos base; cada anillo más profundo suma bonusPerStep × nivel de rareza al multipli |
| `RunHaul.cs` | class `RunHaul` | Loot que el equipo ha subido al Ascensor en esta iteración; solo cuenta si llega al Osprey. |
