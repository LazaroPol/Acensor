<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Authoring`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Authoring`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Authoring`

**Depende de:** [Core.Common](Core.Common.md), [Core.Crafting.Content](Core.Crafting.Content.md), [Core.Crafting.Definitions](Core.Crafting.Definitions.md), [Core.Crafting.Knowledge](Core.Crafting.Knowledge.md), [Core.Crafting.Mods](Core.Crafting.Mods.md), [Core.Crafting.Stations](Core.Crafting.Stations.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Professions](Core.Professions.md)
**Lo usan:** [Core.Crafting.Play](Core.Crafting.Play.md), [Core.Crafting.Session](Core.Crafting.Session.md), [Runtime.Crafting.Authoring](Runtime.Crafting.Authoring.md), [Runtime.Sandbox](Runtime.Sandbox.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CraftingCatalog.cs` | class `CraftingCatalog` | compilado desde un . |
| `CraftingDataSet.cs` | class `CraftingDataSet` | Todos los datos de oficios en forma serializable: lo que guarda la CraftingDatabase de Unity y lo que genera desde el catálogo C#. |
| `CraftingDataValidator.References.cs` | class `CraftingDataValidator` |  |
| `CraftingDataValidator.cs` | class `CraftingDataValidator` | Valida un antes de usarlo: ids vacíos o repetidos, referencias rotas (ítems, piezas, recetas, conocimientos) y curvas/umbrales no crecientes |
| `CurveAssetMapper.cs` | class `CurveAssetMapper` | Convierte ⇄ + . |
| `CurveData.cs` | class `CurveData` | Datos serializables de + . |
| `CurveData.cs` | class `MilestoneData` |  |
| `DataIssue.cs` | class `DataIssue` | Problema encontrado al validar los datos de oficios (tipo de asset, id y motivo). |
| `DefaultCraftingData.cs` | class `DefaultCraftingData` | Vuelca el catálogo C# por defecto (OF-11/OF-12) a un : es lo que usa el menú «Generar assets por defecto» para no teclear las recetas a mano |
| `ICraftingCatalog.cs` | interface `ICraftingCatalog` | Punto de entrada único a los datos de oficios ya convertidos al núcleo. |
| `ItemAssetMapper.cs` | class `ItemAssetMapper` | Convierte ⇄ . |
| `ItemData.cs` | class `ItemData` | Datos serializables de un (campos públicos: Unity los guarda en el ScriptableObject sin conocer el núcleo). |
| `KnowledgeAssetMapper.cs` | class `KnowledgeAssetMapper` | Convierte ⇄ . |
| `KnowledgeData.cs` | class `KnowledgeData` | Datos serializables de un . |
| `ModAssetMapper.cs` | class `ModAssetMapper` | Convierte ⇄ . |
| `ModData.cs` | class `ModData` | Datos serializables de un (Unity no guarda nulables: ). |
| `ModData.cs` | class `ModifierData` |  |
| `QualityAssetMapper.cs` | class `QualityAssetMapper` | Convierte ⇄ . |
| `QualityData.cs` | class `QualityData` | Datos serializables de : pesos de la fórmula y los seis grados. |
| `QualityData.cs` | class `GradeData` |  |
| `RecipeAssetMapper.cs` | class `RecipeAssetMapper` | Convierte ⇄ ; los ítems se resuelven por id. |
| `RecipeData.cs` | class `RecipeData` | Datos serializables de un ; los ítems van por id. |
| `RecipeData.cs` | class `IngredientData` | Entrada de receta: por ítem ( ) o, si está vacío, por etiqueta. |
| `RecipeData.cs` | class `OutputData` |  |
| `StationPartAssetMapper.cs` | class `StationPartAssetMapper` | Convierte ⇄ (definición + ítem). |
| `StationPartData.cs` | class `StationPartData` | Datos serializables de una pieza de estación ( ). |
| `UnlockAssetMapper.cs` | class `UnlockAssetMapper` | Aplana una en dos listas (conocimientos y colecciones, todas exigidas) y la reconstruye. |
