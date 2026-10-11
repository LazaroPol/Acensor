<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Floors.Omens`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Floors/Omens`
Tests: `Assets/_Project/Tests/EditMode/Core/Floors/Omens`

**Depende de:** [Core.Common](Core.Common.md), [Core.Floors.Types](Core.Floors.Types.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `FloorModifier.cs` | class `FloorModifier` | Modificador de piso: nombre, pista que lo anuncia en el Presagio y si exige combate. |
| `FloorModifierCatalog.cs` | class `FloorModifierCatalog` | Registro de modificadores de piso indexado por tipo. |
| `FloorModifierKind.cs` | enum `FloorModifierKind` | Modificadores que puede anunciar el Presagio de un piso (GDD §8). |
| `Omen.cs` | class `Omen` | Presagio de un piso: anuncia su tipo y los modificadores que lo afectan. |
| `OmenRoller.cs` | class `OmenRoller` | Tira el Presagio de un piso de forma reproducible a partir de la semilla de su plan. |
