<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Professions](Core.Professions.md)
**Lo usan:** [Core.Elevator](Core.Elevator.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `CraftResult.cs` | enum `CraftResult` | Resultado de un intento de fabricación. |
| `CraftTransaction.cs` | class `CraftTransaction` | Consume ingredientes y añade el resultado de forma atómica: si el resultado no cabe (ni con el hueco que liberan los ingredientes) el invent |
| `Crafter.cs` | class `Crafter` | Fabrica recetas sobre un inventario. |
| `IRecipeBook.cs` | interface `IRecipeBook` | Catálogo de recetas consultable por id o por profesión. |
| `ISynergyRecipeBook.cs` | interface `ISynergyRecipeBook` | Catálogo de recetas de sinergia consultable por id o por grupo. |
| `Recipe.cs` | class `Recipe` | Receta inmutable de una profesión: ingredientes → resultado (GDD §7). |
| `RecipeBook.cs` | class `RecipeBook` | Recetario en memoria; los ids de receta son únicos. |
| `RecipeIngredient.cs` | class `RecipeIngredient` | Ingrediente de una receta: un material y cuántas unidades consume. |
| `SynergyCrafter.cs` | class `SynergyCrafter` | Fabrica recetas de sinergia: exige que el grupo tenga ambas profesiones y que quien fabrica tenga una de ellas. |
| `SynergyRecipe.cs` | class `SynergyRecipe` | Receta de sinergia (GDD §7): solo se desbloquea si el grupo tiene las dos profesiones de la sinergia, y solo puede fabricarla un jugador con |
| `SynergyRecipeBook.cs` | class `SynergyRecipeBook` | Recetario de sinergias en memoria; los ids son únicos. |
