<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Professions`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Professions`
Tests: `Assets/_Project/Tests/EditMode/Core/Professions`

**Depende de:** [Core.Common](Core.Common.md)
**Lo usan:** [Core.Crafting](Core.Crafting.md), [Core.Crafting.Content](Core.Crafting.Content.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Crafting.Knowledge](Core.Crafting.Knowledge.md), [Core.Crafting.Mods](Core.Crafting.Mods.md), [Core.Crafting.Queue](Core.Crafting.Queue.md), [Core.Crafting.Stations](Core.Crafting.Stations.md), [Core.Floors.Paths](Core.Floors.Paths.md), [Core.Progression](Core.Progression.md), [Core.Sandbox](Core.Sandbox.md), [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md), [Runtime.Visuals](Runtime.Visuals.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `DefaultProfessionSynergies.cs` | class `DefaultProfessionSynergies` | Las 7 sinergias del GDD §7. |
| `ISynergyCatalog.cs` | interface `ISynergyCatalog` | Resuelve qué sinergias desbloquea un conjunto de profesiones. |
| `LearnedProfessions.cs` | class `LearnedProfessions` | Profesiones aprendidas por el personaje (como en WoW, como mucho dos a la vez). |
| `ProfessionBench.cs` | class `ProfessionBench` | Banco de trabajo de la Armería: cada jugador elige exactamente una profesión (GDD §4). |
| `ProfessionCurve.cs` | class `ProfessionCurve` | Curva de XP de un oficio (GDD §17.1): tabla «nivel → XP para el siguiente». |
| `ProfessionFurniture.cs` | class `ProfessionFurniture` | Banco de trabajo de cada profesión: una base y herramientas del pack KayKit RPG Tools Bits encima. |
| `ProfessionId.cs` | enum `ProfessionId` | Las 7 profesiones del banco de trabajo de la Armería (GDD §4, §7). |
| `ProfessionLevels.cs` | class `ProfessionLevels` | Un por cada : el nivel existe para todos los oficios, aprendidos o no, para no perder progreso al cambiar de oficio. |
| `ProfessionMilestone.cs` | class `ProfessionMilestone` | Lo que abre un nivel de oficio: tier de material, bonus de tiempo y de calidad máxima. |
| `ProfessionMilestones.cs` | class `ProfessionMilestones` | Tabla de hitos de oficio (datos). |
| `ProfessionSkill.cs` | class `ProfessionSkill` | Nivel y XP de un personaje en un oficio. |
| `ProfessionSynergy.cs` | class `ProfessionSynergy` | Receta disponible solo si el grupo tiene ambas profesiones (GDD §7). |
| `SynergyCatalog.cs` | class `SynergyCatalog` |  |
