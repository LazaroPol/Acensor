<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Hubs`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Hubs`
Tests: (sin carpeta propia)

**Depende de:** [Core.Armory](Core.Armory.md), [Core.Classes](Core.Classes.md), [Core.Composition](Core.Composition.md), [Core.Elevator](Core.Elevator.md), [Core.Events](Core.Events.md), [Core.Hubs](Core.Hubs.md), [Core.Hud](Core.Hud.md), [Core.Iteration](Core.Iteration.md), [Core.Stats](Core.Stats.md), [Core.Visuals](Core.Visuals.md), [Runtime.Bootstrap](Runtime.Bootstrap.md), [Runtime.Incursion](Runtime.Incursion.md), [Runtime.Player](Runtime.Player.md), [Runtime.Prototype](Runtime.Prototype.md), [Runtime.Visuals](Runtime.Visuals.md)
**Lo usan:** [Runtime.Bootstrap](Runtime.Bootstrap.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ArmoryRoom.cs` | class `ArmoryRoom` | Armería caminable: un pedestal por kit (E lo coge) y el Ascensor al fondo (E sale con kit en mano). |
| `ArmoryScreen.cs` | class `ArmoryScreen` | Pantalla de la Armería (IMGUI): un botón por kit y «Salir al Ascensor». |
| `ElevatorRoom.cs` | class `ElevatorRoom` | Ascensor-Refugio caminable: pedestales de ranuras de módulo (E muestra la ranura), el Laboratorio como última estación y las puertas del Asc |
| `ElevatorScreen.cs` | class `ElevatorScreen` | Pantalla del Ascensor (IMGUI): ranuras de módulos, «listo» y acceso a Armería y Laboratorio. |
| `HubBootstrap.cs` | class `HubBootstrap` | Monta la pantalla de las escenas Armería y Ascensor con los servicios del contenedor y avanza la iteración. |
| `HubRoom.cs` | class `HubRoom` | Sala 3D de un hub: monta la geometría de , al jugador y resuelve la interacción por cercanía (E) con las estaciones y el Ascensor. |
