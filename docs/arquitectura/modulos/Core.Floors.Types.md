<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Floors.Types`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Floors/Types`
Tests: `Assets/_Project/Tests/EditMode/Core/Floors/Types`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Floors.Omens](Core.Floors.Omens.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CalmFloorTypes.cs` | class `PuzzleFloor` | Salas conectadas entre sí para repartir al equipo y comunicarse. |
| `CalmFloorTypes.cs` | class `DecisionFloor` | Bifurcación: cada sala del anillo final es una opción del Eco. |
| `CalmFloorTypes.cs` | class `SanctuaryFloor` | Piso seguro: mercader, forja especial y recuperar fatiga. |
| `CalmFloorTypes.cs` | class `ProfessionFloor` | Rutas que premian la variedad de kit de profesiones. |
| `CombatFloorTypes.cs` | class `LootFloor` | Piso base: exploración y riesgo/recompensa por anillos. |
| `CombatFloorTypes.cs` | class `BossFloor` | Antesala corta que desemboca en la arena del jefe. |
| `CombatFloorTypes.cs` | class `WavesFloor` | Arena amplia y muy conectada para defender posiciones. |
| `CombatFloorTypes.cs` | class `TrapMazeFloor` | Muchos anillos estrechos sin atajos laterales. |
| `CombatFloorTypes.cs` | class `HybridFloor` | Todo a la vez: el trazado más grande y enrevesado. |
| `FloorObjective.cs` | enum `FloorObjective` | Condición principal para superar un piso. |
| `FloorPlan.cs` | class `FloorPlan` | Piso planificado: su tipo y el trazado generado para él. |
| `FloorPlanner.cs` | class `FloorPlanner` | Genera el plan de un piso delegando el trazado en la estrategia de su tipo. |
| `FloorType.cs` | class `FloorType` | Base común de las estrategias de piso; por defecto genera por anillos. |
| `FloorTypeCatalog.cs` | class `FloorTypeCatalog` | Registro de estrategias de piso indexado por tipo. |
| `FloorTypeKind.cs` | enum `FloorTypeKind` | Tipos de piso del GDD (§8). |
| `IFloorType.cs` | interface `IFloorType` | Estrategia de un tipo de piso: qué pone a prueba y cómo se genera su trazado. |
