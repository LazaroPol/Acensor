<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Time`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Time`
Tests: `Assets/_Project/Tests/EditMode/Core/Time`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Elevator](Core.Elevator.md), [Core.Hud](Core.Hud.md), [Core.Iteration](Core.Iteration.md), [Core.Iteration.States](Core.Iteration.States.md), [Core.Prototype](Core.Prototype.md), [Core.StateMachines](Core.StateMachines.md), [Core.Vitals](Core.Vitals.md), [Runtime.Prototype](Runtime.Prototype.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `IClock.cs` | interface `IClock` | Fuente de tiempo de juego en segundos. |
| `ITickable.cs` | interface `ITickable` | Sistema del dominio que avanza con el tiempo. |
| `ManualClock.cs` | class `ManualClock` | Reloj controlado manualmente: útil en tests y en simulaciones del servidor. |
| `PhaseTimer.cs` | class `PhaseTimer` | Cuenta atrás de una fase (Calma, Incursión…). |
