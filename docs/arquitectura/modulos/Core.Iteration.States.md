<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Iteration.States`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Iteration/States`
Tests: (sin carpeta propia)

**Depende de:** [Core.Iteration.Ending](Core.Iteration.Ending.md), [Core.Iteration.Presence](Core.Iteration.Presence.md), [Core.Iteration.Readiness](Core.Iteration.Readiness.md), [Core.StateMachines](Core.StateMachines.md), [Core.Time](Core.Time.md)
**Lo usan:** [Core.Iteration](Core.Iteration.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CalmState.cs` | class `CalmState` | Calma: el ascensor llega a un piso nuevo y las puertas están cerradas. |
| `EndedState.cs` | class `EndedState` | Fin: fija el resultado (victoria si no había otro), resuelve destinos y lo publica. |
| `IncursionState.cs` | class `IncursionState` | Incursión: puertas abiertas; avisa antes de que se cierren. |
| `SignalState.cs` | class `SignalState` | Fase sin lógica propia: avanza cuando se cumple su condición de salida. |
| `TimedPhaseState.cs` | class `TimedPhaseState` | Estado con cuenta atrás propia: crea el temporizador al entrar y lo hace avanzar. |
