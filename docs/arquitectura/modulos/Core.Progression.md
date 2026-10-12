<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Progression`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Progression`
Tests: `Assets/_Project/Tests/EditMode/Core/Progression`

**Depende de:** [Core.Bestiary](Core.Bestiary.md), [Core.Classes](Core.Classes.md), [Core.Combat](Core.Combat.md), [Core.Common](Core.Common.md), [Core.Equipment](Core.Equipment.md), [Core.Professions](Core.Professions.md), [Core.Stats](Core.Stats.md), [Core.Talents](Core.Talents.md), [Core.Vitals](Core.Vitals.md)
**Lo usan:** [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md), [Runtime.Sandbox.World](Runtime.Sandbox.World.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CharacterProgress.cs` | class `CharacterProgress` | Nivel y experiencia del personaje. |
| `HeroProgression.cs` | class `HeroProgression` | Progresión del personaje: nivel y experiencia compartidos, un reparto de talentos por kit (cambiar de kit conserva los talentos de cada uno) |
| `KitSpellbooks.cs` | class `KitSpellbooks` | Hechizos y técnicas de cada kit (5 por kit, de nivel 2 a 14). |
| `LevelCurve.cs` | class `LevelCurve` | Curva de experiencia: XP para pasar de un nivel al siguiente (crece de forma suave) y nivel máximo. |
| `LevelDifficulty.cs` | enum `Difficulty` | Color de dificultad de una criatura según la diferencia de nivel, como el nombre del objetivo en WoW. |
| `LevelDifficulty.cs` | class `LevelDifficulty` |  |
| `LevelStats.cs` | class `LevelStats` | Crecimiento de stats por nivel: +8 % de vida y +6 % de daño y recurso por nivel por encima del 1. |
| `SpellCastResult.cs` | enum `SpellCastResult` |  |
| `SpellDefinition.cs` | class `SpellDefinition` | Hechizo o técnica que un kit aprende al subir de nivel. |
| `SpellKind.cs` | enum `SpellKind` | Tipo de efecto de un hechizo o técnica aprendida. |
| `SpellSlot.cs` | class `SpellSlot` | Hueco de la barra de acción para un hechizo: nivel requerido, coste del recurso y enfriamiento. |
| `Spellbook.cs` | class `Spellbook` | Libro de hechizos de un kit: todos sus hechizos ordenados por nivel y los ya aprendidos a un nivel dado. |
| `TimedBonus.cs` | class `TimedBonus` | Bonificación temporal (p. |
| `XpRewards.cs` | class `XpRewards` | Experiencia por derrotar criaturas (según su vida base) y por recolectar o fabricar. |
| `XpSource.cs` | enum `XpSource` | Origen de la experiencia (el HUD la pinta en azul claro si es de combate y en verde si es de profesión). |
