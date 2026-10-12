<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Stats`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Stats`
Tests: `Assets/_Project/Tests/EditMode/Core/Stats`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Classes](Core.Classes.md), [Core.Dna](Core.Dna.md), [Core.Elevator](Core.Elevator.md), [Core.Vitals](Core.Vitals.md), [Runtime.Player](Runtime.Player.md), [Runtime.Prototype](Runtime.Prototype.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `IStatFormula.cs` | interface `IStatFormula` | Estrategia que combina el valor base con los modificadores activos. |
| `ModifierKind.cs` | enum `ModifierKind` | Cómo se aplica un modificador: Flat suma al valor base, PercentAdd se suma con los demás porcentajes (+10% y +20% = +30%) y Multiplier multi |
| `StandardStatFormula.cs` | class `StandardStatFormula` | (base + Σ Flat) × (1 + Σ PercentAdd) × Π Multiplier, sin bajar de 0. |
| `Stat.cs` | class `Stat` | Atributo con valor base y modificadores; cachea el valor final hasta que cambia algo. |
| `StatModifier.cs` | class `StatModifier` | Modificador inmutable de un atributo. |
| `StatSheet.cs` | class `StatSheet` | Conjunto de atributos de un personaje, indexado por . |
| `StatType.cs` | enum `StatType` | Atributos del personaje sobre los que actúan los modificadores. |
