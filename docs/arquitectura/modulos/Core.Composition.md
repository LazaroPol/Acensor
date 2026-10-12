<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Composition`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Composition`
Tests: `Assets/_Project/Tests/EditMode/Core/Composition`

**Depende de:** [Core.Common](Core.Common.md), [Core.Events](Core.Events.md)
**Lo usan:** [Runtime.Bootstrap](Runtime.Bootstrap.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `GameComposition.cs` | class `GameComposition` | Raíz de composición del juego: registra los servicios compartidos por todas las escenas. |
| `ISceneLoader.cs` | interface `ISceneLoader` | Carga física de una escena. |
| `IServiceContainer.cs` | interface `IServiceRegistry` | Registra servicios por tipo (ISP: quien compone no resuelve y viceversa). |
| `IServiceContainer.cs` | interface `IServiceResolver` | Resuelve servicios ya registrados. |
| `SceneFlow.cs` | struct `SceneChanged` | Evento: la partida cambió de escena. |
| `SceneFlow.cs` | class `SceneFlow` | Flujo de escenas: solo permite las transiciones del bucle de juego (Boot → Ascensor ⇄ Armería/Laboratorio, Ascensor ⇄ Incursión) y avisa por |
| `SceneId.cs` | enum `SceneId` | Escenas del juego. |
| `ServiceContainer.cs` | class `ServiceContainer` | Contenedor mínimo de dependencias (sin reflexión). |
