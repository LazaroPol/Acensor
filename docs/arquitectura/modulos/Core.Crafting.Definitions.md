<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting.Definitions`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting/Definitions`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting/Definitions`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Professions](Core.Professions.md)
**Lo usan:** [Core.Crafting.Knowledge](Core.Crafting.Knowledge.md), [Core.Crafting.Queue](Core.Crafting.Queue.md), [Core.Crafting.Stations](Core.Crafting.Stations.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `AllOfCondition.cs` | class `AllOfCondition` | Combinación «Y»: se cumple si se cumplen todas las condiciones. |
| `AlwaysKnownCondition.cs` | class `AlwaysKnownCondition` | Receta de partida del oficio: siempre conocida. |
| `CollectionCompleteCondition.cs` | class `CollectionCompleteCondition` | Todos los volúmenes de un libro de oficio leídos. |
| `IRecipeKnowledge.cs` | interface `IRecipeKnowledge` | Lo que el jugador sabe: conocimientos leídos y colecciones completas (OF-07). |
| `KnowledgeRequiredCondition.cs` | class `KnowledgeRequiredCondition` | Hay que haber leído un pergamino, plano o tratado concreto. |
| `RecipeCatalog.Variants.cs` | class `RecipeCatalog` | Validación y resolución de variantes mejoradas del . |
| `RecipeCatalog.cs` | class `RecipeCatalog` | Catálogo de recetas de oficio: ids únicos, consultas por estación y por oficio y resolución de variantes mejoradas (GDD §17.2). |
| `RecipeDefinition.Legacy.cs` | class `RecipeDefinition` | Conversión de las recetas antiguas ( ) al formato de oficios. |
| `RecipeDefinition.Validation.cs` | class `RecipeDefinition` | Validación temprana de : los datos malos fallan al crearla. |
| `RecipeDefinition.cs` | class `RecipeDefinition` | Receta de oficio inmutable (GDD §17.2): entradas, salidas, estación y nivel, piezas exigidas, oficio y nivel, tiempo, XP, condición de desbl |
| `RecipeOutput.cs` | class `RecipeOutput` | Salida de receta: ítem + cantidad. |
| `StationType.cs` | enum `StationType` | Las 7 estaciones de trabajo (GDD §17). |
| `StationType.cs` | class `StationTypes` | Estación principal de cada oficio (tabla de GDD §17). |
| `UnlockCondition.cs` | class `UnlockCondition` | Condición para conocer una receta (GDD §17.2). |
