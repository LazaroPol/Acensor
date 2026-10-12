<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->

# `Specimen.Core.Crafting`

Ensamblado: `Specimen.Core` · Carpeta: `Assets/_Project/Scripts/Core/Crafting`
Tests: `Assets/_Project/Tests/EditMode/Core/Crafting`

**Depende de:** [Core.Common](Core.Common.md), [Core.Items](Core.Items.md), [Core.Items.Catalog](Core.Items.Catalog.md), [Core.Professions](Core.Professions.md), [Core.Randomness](Core.Randomness.md)
**Lo usan:** [Core.Elevator](Core.Elevator.md), [Runtime.Sandbox](Runtime.Sandbox.md), [Runtime.Sandbox.Ui](Runtime.Sandbox.Ui.md)

| Fichero | Tipo | Resumen |
|---|---|---|
| `ConsumptionPlan.cs` | class `ConsumptionPlan` | Resultado del planificador: qué se gastaría, sin haber tocado el inventario. |
| `CraftResult.cs` | enum `CraftResult` | Resultado de un intento de fabricación. |
| `CraftTransaction.cs` | class `CraftTransaction` | Consume ingredientes y añade el resultado de forma atómica: si el resultado no cabe (ni con el hueco que liberan los ingredientes) el invent |
| `Crafter.cs` | class `Crafter` | Fabrica recetas sobre un inventario. |
| `DefaultRecipes.cs` | class `DefaultRecipes` | Recetas de cada profesión con los materiales del poblado (recolección) y del bestiario (botín). |
| `IQualityRoller.cs` | interface `IQualityRoller` | Estrategia que decide la calidad de un objeto fabricado. |
| `IRecipeBook.cs` | interface `IRecipeBook` | Catálogo de recetas consultable por id o por profesión. |
| `ISynergyRecipeBook.cs` | interface `ISynergyRecipeBook` | Catálogo de recetas de sinergia consultable por id o por grupo. |
| `IngredientChoice.cs` | class `IngredientChoice` | Elección del jugador (desde la UI) de un ítem concreto para un ingrediente por etiqueta: el planificador lo gasta antes que el orden por def |
| `IngredientSpec.cs` | class `IngredientSpec` | Ingrediente de receta por objeto concreto ( ) o por etiqueta con tier mínimo ( ), p. |
| `PlanBuilder.cs` | class `PlanBuilder` | Estado de trabajo del planificador: copia de las existencias por ítem (sin tocar el inventario) y las líneas del plan, fusionadas por ítem e |
| `PlannedConsumption.cs` | class `PlannedConsumption` | Una línea del plan: cuántas unidades de un ítem se van a gastar. |
| `QualityRollInput.cs` | class `QualityRollInput` | Entradas de una tirada de calidad: palancas del jugador y topes (hito y receta). |
| `QualityRoller.cs` | class `QualityRoller` | Tirada de calidad del GDD §17.1: puntos → grado, limitado por hito y receta. |
| `QualityRules.cs` | class `QualityRules` | Constantes de calidad (datos): grados con umbrales, pesos de la puntuación y rango aleatorio. |
| `QualityWeights.cs` | class `QualityWeights` | Pesos de la puntuación de calidad y rango aleatorio inclusivo (GDD §17.1). |
| `Recipe.cs` | class `Recipe` | Receta inmutable de una profesión: ingredientes → resultado (GDD §7). |
| `RecipeBook.cs` | class `RecipeBook` | Recetario en memoria; los ids de receta son únicos. |
| `RecipeIngredient.cs` | class `RecipeIngredient` | Ingrediente de una receta: un material y cuántas unidades consume. |
| `SynergyCrafter.cs` | class `SynergyCrafter` | Fabrica recetas de sinergia: exige que el grupo tenga ambas profesiones y que quien fabrica tenga una de ellas. |
| `SynergyRecipe.cs` | class `SynergyRecipe` | Receta de sinergia (GDD §7): solo se desbloquea si el grupo tiene las dos profesiones de la sinergia, y solo puede fabricarla un jugador con |
| `SynergyRecipeBook.cs` | class `SynergyRecipeBook` | Recetario de sinergias en memoria; los ids son únicos. |
| `TagConsumptionPlanner.cs` | class `TagConsumptionPlanner` | Planifica qué pilas gastar para unos ingredientes por objeto o por etiqueta sin tocar el inventario. |
