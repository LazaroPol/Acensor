<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Iteration.Readiness`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Iteration/Readiness`
Tests: `Assets/_Project/Tests/EditMode/Core/Iteration/Readiness`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Iteration](Core.Iteration.md), [Core.Iteration.States](Core.Iteration.States.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `HasteBonusEarned.cs` | struct `HasteBonusEarned` | Todo el grupo estuvo listo antes de tiempo: se gana Prisa para el loot de este piso. |
| `IHasteBonusPolicy.cs` | interface `IHasteBonusPolicy` | Calcula el bonus Prisa (fracción extra de loot del piso) según el tiempo de Calma ahorrado. |
| `LinearHasteBonus.cs` | class `LinearHasteBonus` | Bonus proporcional al tiempo ahorrado: ahorrar toda la Calma da . |
| `ReadyCheck.cs` | class `ReadyCheck` | Registro de qué jugadores han marcado "Listo" en la Calma. |
