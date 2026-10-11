<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Floors`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Floors`
Tests: `Assets/_Project/Tests/EditMode/Core/Floors`

**Depende de:** [Core.Common](Core.Common.md), [Core.Loot](Core.Loot.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `Corridor.cs` | struct `Corridor` | Pasillo no dirigido entre dos salas (A &lt; B). |
| `FloorConnectivity.cs` | class `FloorConnectivity` | Consultas de alcanzabilidad sobre un trazado (BFS desde la entrada). |
| `FloorLayout.cs` | class `FloorLayout` | Trazado inmutable de un piso: salas, pasillos y semilla que lo generó. |
| `FloorRoom.cs` | struct `FloorRoom` | Sala del piso. |
| `IFloorGenerator.cs` | interface `IFloorGenerator` | Genera el trazado de un piso de forma reproducible a partir de una semilla. |
| `RingCorridorGenerator.cs` | class `RingCorridorGenerator` | Genera pasillos por anillos de profundidad: cada sala cuelga de una del anillo anterior (conectividad garantizada), con pasillos laterales y |
| `RingLayoutConfig.cs` | class `RingLayoutConfig` | Parámetros del generador por anillos: salas por anillo y pasillos extra. |
| `RoomKind.cs` | enum `RoomKind` | Función de una sala dentro del trazado de un piso. |
