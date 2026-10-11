<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Floors.Population`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Floors/Population`
Tests: `Assets/_Project/Tests/EditMode/Core/Floors/Population`

**Depende de:** [Core.Common](Core.Common.md), [Core.Loot](Core.Loot.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `FloorPopulation.cs` | class `FloorPopulation` | Población completa de un piso, alineada con las salas del trazado (índice = id de sala). |
| `FloorPopulator.cs` | class `FloorPopulator` | Reparte loot y enemigos por las salas de un piso según su anillo: más lejos = mejor loot y peores enemigos. |
| `RingPopulationRules.cs` | class `RingPopulationRules` | Cuotas de población por anillo de profundidad (1-3). |
| `RingQuota.cs` | class `RingQuota` | Cuánto se puebla una sala de un anillo: tiradas de loot, enemigos y nivel de amenaza. |
| `RoomPopulation.cs` | class `RoomPopulation` | Contenido de una sala: loot que se puede saquear y enemigos que la guardan. |
