<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Hubs`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Hubs`
Tests: `Assets/_Project/Tests/EditMode/Core/Hubs`

**Depende de:** [Core.Armory](Core.Armory.md), [Core.Classes](Core.Classes.md), [Core.Common](Core.Common.md), [Core.Elevator](Core.Elevator.md), [Core.Iteration](Core.Iteration.md), [Core.Iteration.Presence](Core.Iteration.Presence.md), [Core.Prototype](Core.Prototype.md), [Core.Visuals](Core.Visuals.md)
**Lo usan:** [Runtime.Hubs](Runtime.Hubs.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ArmoryPresenter.cs` | class `ArmoryPresenter` | Pantalla de la Armería (Piso 0): el jugador local elige un kit de clase con el stock limitado del y, con kit en mano, sale hacia el Ascensor |
| `ElevatorPresenter.cs` | class `ElevatorPresenter` | Pantalla del Ascensor-Refugio: ranuras de módulos y el «listo» del jugador local durante la Calma. |
| `HubRoomLayout.cs` | class `HubRoomLayout` | Sala de un hub (Armería o Ascensor) por la que se camina: suelo y muros greybox, una fila de «estaciones» (pedestales de Loot) a lo largo de |
| `KitEntry.cs` | enum `KitAvailability` |  |
| `KitEntry.cs` | struct `KitEntry` | Kit de clase tal como se muestra en las estanterías de la Armería. |
| `ProximityPicker.cs` | class `ProximityPicker` | Elige el punto de interés más cercano al jugador dentro de un alcance (-1 si no hay ninguno). |
