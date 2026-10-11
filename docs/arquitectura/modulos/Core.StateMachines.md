<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.StateMachines`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/StateMachines`
Tests: `Assets/_Project/Tests/EditMode/Core/StateMachines`

**Depende de:** [Core.Common](Core.Common.md), [Core.Time](Core.Time.md)
**Lo usan:** [Core.Iteration](Core.Iteration.md), [Core.Iteration.States](Core.Iteration.States.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `IState.cs` | interface `IState` | Estado de una máquina de estados. |
| `IState.cs` | class `StateBase` | Base con implementaciones vacías para que cada estado solo sobrescriba lo que necesita. |
| `StateMachine.cs` | class `StateMachine` | Máquina de estados genérica con transiciones condicionales. |
| `Transition.cs` | struct `Transition` | Transición condicional hacia un estado destino. |
