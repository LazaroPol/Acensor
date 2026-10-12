<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Play`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Play`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Play`

**Depende de:** [Core.Common](Core.Common.md), [Core.Crafting.Authoring](Core.Crafting.Authoring.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Crafting.Knowledge](Core.Crafting.Knowledge.md), [Core.Crafting.Mods](Core.Crafting.Mods.md), [Core.Crafting.Save](Core.Crafting.Save.md), [Core.Crafting.Session](Core.Crafting.Session.md), [Core.Equipment](Core.Equipment.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Loot](Core.Loot.md), [Core.Professions](Core.Professions.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CraftingLoot.cs` | class `CraftingLoot` | Botín de oficios del poblado y de los pisos (GDD §17.7): conocimientos, piezas de estación y mods en una tabla por anillo. |
| `CraftingRestorer.cs` | class `CraftingRestorer` | Aplica un guardado de oficios al Campo de pruebas (GDD §17.7). |
| `CraftingSnapshot.cs` | class `CraftingSnapshot` | Foto del progreso de oficios del Campo de pruebas (GDD §17.7) para : oficios, recetas y lecturas, estaciones (nivel, piezas, ranuras y cola) |
| `GearBaseStats.cs` | class `GearBaseStats` | Stats base de un objeto para la ventana de mods (GDD §17.4): daño medio y velocidad del arma, armadura y peso. |
| `GearModBench.cs` | class `GearModBench` | Ventana de mods de un objeto (GDD §17.4, §17.7): recuerda los mods de cada instancia, los pone y quita moviéndolos entre la bolsa y el objet |
| `GearModLabels.cs` | class `GearModLabels` | Textos de la ventana de mods (GDD §17.4, §17.7): nombre de cada stat, línea base → efectivo y resultados. |
| `ItemResolver.cs` | class `ItemResolver` | Encuentra la definición de un id guardado: catálogo de oficios, conocimientos, mods, piezas de estación y, por último, el catálogo general d |
| `KnowledgeItems.cs` | class `KnowledgeItems` | Objeto de bolsa de cada conocimiento (GDD §17.3): mismo id que el conocimiento, una unidad por hueco. |
| `KnowledgeShelf.cs` | class `KnowledgeShelf` | Leer pergaminos, planos y tratados desde la bolsa (GDD §17.7, clic derecho): usa el oficio del jugador, registra las recetas y consume el ob |
