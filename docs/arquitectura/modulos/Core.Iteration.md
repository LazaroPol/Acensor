<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Iteration`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Iteration`
Tests: `Assets/_Project/Tests/EditMode/Core/Iteration`

**Depende de:** [Core.Common](Core.Common.md), [Core.Events](Core.Events.md), [Core.Iteration.Ending](Core.Iteration.Ending.md), [Core.Iteration.Presence](Core.Iteration.Presence.md), [Core.Iteration.Readiness](Core.Iteration.Readiness.md), [Core.Iteration.States](Core.Iteration.States.md), [Core.Iteration.Voting](Core.Iteration.Voting.md), [Core.StateMachines](Core.StateMachines.md), [Core.Time](Core.Time.md)
**Lo usan:** [Core.Life](Core.Life.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `IterationConfig.cs` | class `IterationConfig` | Parámetros inmutables de una iteración (una partida): pisos, duración de cada fase y avisos. |
| `IterationConfigBuilder.cs` | class `IterationConfigBuilder` | Constructor fluido con los valores por defecto del GDD (§3.1): Acto de 5 pisos, Calma 3 min, Incursión 7 min, avisos a 60 s y 15 s, asedio d |
| `IterationContext.cs` | class `IterationContext` | Estado compartido entre los estados de la iteración. |
| `IterationEvents.cs` | struct `IterationPhaseChanged` | La iteración ha cambiado de fase. |
| `IterationEvents.cs` | struct `FloorReached` | El ascensor ha llegado a un piso nuevo (empieza su Calma). |
| `IterationEvents.cs` | struct `DoorsClosingWarning` | Quedan segundos para que se cierren las puertas. |
| `IterationMachineFactory.cs` | class `IterationMachineFactory` | Define los estados y transiciones del bucle de iteración en un único sitio. |
| `IterationPhase.cs` | enum `IterationPhase` | Fases de una iteración (GDD §3.1). |
| `IterationRun.cs` | class `IterationRun` | Orquesta una iteración: Armería → (Calma → Incursión → Ascenso) × pisos → Asedio → Extracción → Fin. |
| `IterationRun.Ending.cs` | class `IterationRun` | Condiciones de fin: victoria por extracción, grupo eliminado o abandono. |
| `IterationRun.Presence.cs` | class `IterationRun` | Entradas y salidas del ascensor, y rescate por el Hueco de Emergencia. |
