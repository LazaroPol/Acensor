<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Talents`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Talents`
Tests: `Assets/_Project/Tests/EditMode/Core/Talents`

**Depende de:** [Core.Classes](Core.Classes.md), [Core.Common](Core.Common.md), [Core.Stats](Core.Stats.md)
**Lo usan:** [Core.Progression](Core.Progression.md), [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `KitTalentTrees.Casters.cs` | class `KitTalentTrees` | Árboles de Oráculo, Druida de Ceniza, Duelista y Arcanista. |
| `KitTalentTrees.cs` | class `KitTalentTrees` | Árbol de talentos de cada kit: tres ramas de 5 filas (defensa, ofensiva y especialidad). |
| `TalentAllocation.cs` | class `TalentAllocation` | Puntos gastados en un árbol. |
| `TalentBonuses.cs` | class `TalentBonuses` | Aplica los talentos a la hoja de stats (modificadores con fuente propia) y expone las mejoras de hechizos. |
| `TalentBranch.cs` | class `TalentBranch` | Rama de un árbol de talentos (p. |
| `TalentBuilder.cs` | class `TalentBuilder` | Atajos para escribir árboles: cada rama son 5 talentos, uno por fila (tier 0 a 4). |
| `TalentDefinition.cs` | class `TalentDefinition` | Talento de un árbol: fila (tier 0-4), rangos máximos y efecto por rango. |
| `TalentEffectKind.cs` | enum `TalentEffectKind` | Qué mejora un talento por cada rango. |
| `TalentText.cs` | class `TalentText` | Texto del tooltip de un talento para un rango dado. |
| `TalentTree.cs` | class `TalentTree` | Árbol de talentos de un kit: tres ramas independientes. |
