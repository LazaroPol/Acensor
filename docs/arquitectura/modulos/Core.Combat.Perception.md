<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Combat.Perception`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Combat/Perception`
Tests: `Assets/_Project/Tests/EditMode/Core/Combat/Perception`

**Depende de:** [Core.Iteration.Presence](Core.Iteration.Presence.md), [Core.Movement](Core.Movement.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `EnemyPerception.cs` | class `EnemyPerception` | Combina visión y oído de un enemigo con los modificadores inyectados (p. |
| `IPerceptionModifiers.cs` | interface `IPerceptionModifiers` | Estrategia que ajusta los sentidos y la persecución del enemigo según el objetivo. |
| `Observer.cs` | struct `Observer` | Posición y orientación (yaw en grados, convenio de Unity) del enemigo que percibe. |
| `PerceivedTarget.cs` | class `PerceivedTarget` | Lo que un enemigo puede percibir de un jugador: posición, ruido que hace (0-1) y si está Atrapado. |
| `PerceptionConfig.cs` | class `PerceptionConfig` | Sentidos base de un enemigo: alcance y semiángulo de visión (grados), alcance auditivo y velocidad de persecución. |
| `Senses.cs` | enum `Awareness` | Cómo ha percibido un enemigo a un objetivo. |
| `Senses.cs` | interface `ISense` | Un sentido: decide si el observador detecta al objetivo con un alcance ya modificado. |
| `Senses.cs` | class `VisionSense` | Visión en cono: dentro del alcance y del semiángulo de la mirada. |
| `Senses.cs` | class `HearingSense` | Oído omnidireccional: el ruido del objetivo (0-1) escala el alcance auditivo. |
| `TrappedPerceptionModifiers.cs` | class `TrappedPerceptionModifiers` | Aplica los modificadores de Atrapado (GDD §3.2) a los objetivos atrapados; neutros para el resto. |
