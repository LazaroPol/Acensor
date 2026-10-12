using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Conversión de las recetas antiguas (<see cref="Recipe"/>) al formato de oficios.</summary>
    public sealed partial class RecipeDefinition
    {
        /// <summary>
        /// Receta antigua → estación principal del oficio, nivel 1, siempre conocida e instantánea
        /// (sin XP propia), conservando ingredientes, salida y calidad máxima.
        /// </summary>
        public static RecipeDefinition FromLegacy(Recipe recipe)
        {
            Guard.NotNull(recipe, nameof(recipe));
            return new RecipeDefinition(recipe.Id, recipe.Profession, StationTypes.MainOf(recipe.Profession),
                recipe.Ingredients.Select(IngredientSpec.From),
                new[] { new RecipeOutput(recipe.Output, recipe.OutputQuantity) },
                qualityCap: recipe.QualityCap);
        }
    }
}
