<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Abilities`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Abilities`
Tests: `Assets/_Project/Tests/EditMode/Core/Abilities`

**Depende de:** [Core.Classes](Core.Classes.md), [Core.Common](Core.Common.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** [Core.Combat.Attacks](Core.Combat.Attacks.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `AbilityCooldown.cs` | class `AbilityCooldown` | Enfriamiento de una habilidad: cuenta atrás que avanza con . |
| `AbilityDefinition.cs` | class `AbilityDefinition` | Definición inmutable de una habilidad: coste de recurso de clase y de stamina, y enfriamiento. |
| `AbilityResult.cs` | enum `AbilityResult` | Resultado de intentar activar una habilidad. |
| `AbilitySlot.cs` | class `AbilitySlot` | Habilidad equipada: comprueba enfriamiento, cobra el coste e inicia el enfriamiento. |
| `DefaultClassAbilities.cs` | class `DefaultClassAbilities` | Habilidad de firma de cada kit (GDD §4). |
| `IAbilityCostPayer.cs` | interface `IAbilityCostPayer` | Cobra el coste de una habilidad; si no alcanza, no cobra nada. |
| `PoolCostPayer.cs` | class `PoolCostPayer` | Cobra de los pools de recurso de clase y stamina de forma atómica (o ambos o ninguno). |
