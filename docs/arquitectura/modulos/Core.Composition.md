<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Composition`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Composition`
Tests: `Assets/_Project/Tests/EditMode/Core/Composition`

**Depende de:** [Core.Armory](Core.Armory.md), [Core.Classes](Core.Classes.md), [Core.Common](Core.Common.md), [Core.Elevator](Core.Elevator.md), [Core.Events](Core.Events.md), [Core.Iteration](Core.Iteration.md), [Core.Loot](Core.Loot.md), [Core.Profile](Core.Profile.md)
**Lo usan:** [Core.Art](Core.Art.md), [Runtime.Atico](Runtime.Atico.md), [Runtime.Bootstrap](Runtime.Bootstrap.md), [Runtime.Extraction](Runtime.Extraction.md), [Runtime.Hubs](Runtime.Hubs.md), [Runtime.Incursion](Runtime.Incursion.md), [Runtime.Lab](Runtime.Lab.md), [Runtime.Player](Runtime.Player.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `GameComposition.cs` | class `GameComposition` | Raíz de composición del juego: registra los servicios compartidos por todas las escenas. |
| `ISceneLoader.cs` | interface `ISceneLoader` | Carga física de una escena. |
| `IServiceContainer.cs` | interface `IServiceRegistry` | Registra servicios por tipo (ISP: quien compone no resuelve y viceversa). |
| `IServiceContainer.cs` | interface `IServiceResolver` | Resuelve servicios ya registrados. |
| `SceneFlow.cs` | struct `SceneChanged` | Evento: la partida cambió de escena. |
| `SceneFlow.cs` | class `SceneFlow` | Flujo de escenas: solo permite las transiciones del bucle de juego (Boot → Ascensor ⇄ Armería/Laboratorio, Ascensor ⇄ Incursión/Ático) y avi |
| `SceneId.cs` | enum `SceneId` | Escenas del juego. |
| `ServiceContainer.cs` | class `ServiceContainer` | Contenedor mínimo de dependencias (sin reflexión). |
