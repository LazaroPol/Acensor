<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Combat`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Combat`
Tests: `Assets/_Project/Tests/EditMode/Core/Combat`

**Depende de:** [Core.Common](Core.Common.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** [Core.Bestiary](Core.Bestiary.md), [Runtime.Combat](Runtime.Combat.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ArmorMitigation.cs` | class `ArmorMitigation` | Armadura con rendimientos decrecientes: reducción = armor / (armor + K). |
| `CritRules.cs` | class `CritRules` | Probabilidad de crítico en [0, 1] y multiplicador >= 1 del atacante. |
| `DamageCalculator.cs` | class `DamageCalculator` | Resuelve un golpe: crítico (con IRandom inyectado) → armadura (solo físico) → resistencia. |
| `DamageResult.cs` | class `DamageResult` | Resultado de resolver un golpe: daño bruto, final tras mitigación y si fue crítico. |
| `DamageType.cs` | enum `DamageType` | Naturaleza del daño. |
| `DefenseProfile.cs` | class `DefenseProfile` | Defensas del objetivo: armadura plana y resistencias por tipo. |
| `Hit.cs` | class `Hit` | Golpe bruto antes de mitigación: cantidad, tipo y si puede ser crítico. |
| `Resistances.cs` | class `Resistances` | Resistencia por tipo de daño, acotada a [Min, Max]. |
