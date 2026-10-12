<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Movement`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Movement`
Tests: `Assets/_Project/Tests/EditMode/Core/Movement`

**Depende de:** [Core.Common](Core.Common.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** [Core.Abilities.Mobility](Core.Abilities.Mobility.md), [Core.Combat.Attacks](Core.Combat.Attacks.md), [Core.Combat.Perception](Core.Combat.Perception.md), [Runtime.Player](Runtime.Player.md), [Runtime.Prototype](Runtime.Prototype.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CameraPoint.cs` | struct `CameraPoint` | Punto 3D en metros (convenio de Unity: Y arriba). |
| `CameraZoom.cs` | class `CameraZoom` | Distancia de la cámara al objetivo, acotada entre un mínimo y un máximo, ajustable con la rueda. |
| `DodgeConfig.cs` | class `DodgeConfig` | Parámetros de la esquiva: impulso breve en una dirección que cuesta Aguante y tiene reutilización. |
| `DodgeController.cs` | class `DodgeController` | Esquiva: al iniciarla gasta Aguante y fija la dirección; durante devuelve la velocidad del impulso y después espera el enfriamiento. |
| `MovementConfig.cs` | class `MovementConfig` | Parámetros del movimiento del jugador (GDD 16, PG-03): velocidad al andar, multiplicador de sprint, Aguante por segundo que cuesta esprintar |
| `PlanarMover.cs` | class `PlanarMover` | Convierte la entrada del jugador (stick/WASD) en velocidad plana relativa al giro de la cámara, aplicando el sprint. |
| `PlanarVector.cs` | struct `PlanarVector` | Vector en el plano del suelo (X, Z): dirección de entrada o velocidad en m/s. |
| `SprintController.cs` | class `SprintController` | Decide si el jugador esprinta en cada tick y cobra el Aguante (stamina) del núcleo. |
| `ThirdPersonRig.cs` | class `ThirdPersonRig` | Cámara en tercera persona cenital-oblicua (GDD: estilo Risk of Rain 2 / V Rising): se coloca a del objetivo, inclinada y mirando hacia ; lo  |
