<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Runtime.Crafting.Authoring`

Ensamblado: `Specimen.Runtime` · Carpeta: `Assets/_Project/Scripts/Runtime/Crafting/Authoring`
Tests: (sin carpeta propia)

**Depende de:** [Core.Crafting](Core.Crafting.md), [Core.Crafting.Authoring](Core.Crafting.Authoring.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Crafting.Knowledge](Core.Crafting.Knowledge.md), [Core.Crafting.Mods](Core.Crafting.Mods.md), [Core.Crafting.Stations](Core.Crafting.Stations.md), [Core.Items](Core.Items.md), [Core.Professions](Core.Professions.md)
**Lo usan:** nadie

| Fichero | Tipo | Resumen |
|---|---|---|
| `CraftingAsset.cs` | class `CraftingAsset` | Base de los assets de oficios: guarda los datos serializables del núcleo ( ) y deja la conversión a los mappers de Specimen.Core.Crafting.Au |
| `CraftingDatabase.cs` | class `CraftingDatabase` | Punto de entrada único de los datos de oficios en Unity: lista todos los assets y los compila a un del núcleo (las recetas resuelven sus íte |
| `ItemDefinitionAsset.cs` | class `ItemDefinitionAsset` | Objeto o material editable; el icono es el sprite que usará la UI (su clave, iconKey ). |
| `KnowledgeAsset.cs` | class `KnowledgeAsset` |  |
| `ModDefinitionAsset.cs` | class `ModDefinitionAsset` |  |
| `ProfessionCurveAsset.cs` | class `ProfessionCurveAsset` |  |
| `QualityRulesAsset.cs` | class `QualityRulesAsset` |  |
| `RecipeDefinitionAsset.cs` | class `RecipeDefinitionAsset` | Receta editable; entradas y salidas van por id de ítem (las resuelve la base de datos). |
| `StationPartAsset.cs` | class `StationPartAsset` |  |
