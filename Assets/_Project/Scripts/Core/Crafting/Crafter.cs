using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Fabrica recetas sobre un inventario. La operación es atómica: si el resultado no cabe
    /// (ni siquiera con el hueco que liberan los ingredientes) el inventario queda como estaba.
    /// </summary>
    public sealed class Crafter
    {
        private readonly IRecipeBook _book;

        public Crafter(IRecipeBook book)
        {
            _book = Guard.NotNull(book, nameof(book));
        }

        /// <summary>Comprueba receta, profesión e ingredientes sin tocar el inventario.</summary>
        public CraftResult Check(string recipeId, ProfessionId crafter, IInventory inventory)
        {
            Guard.NotNull(inventory, nameof(inventory));
            if (!_book.TryGet(recipeId, out var recipe)) return CraftResult.UnknownRecipe;
            if (recipe.Profession != crafter) return CraftResult.WrongProfession;
            return recipe.HasIngredients(inventory) ? CraftResult.Crafted : CraftResult.MissingIngredients;
        }

        public CraftResult TryCraft(string recipeId, ProfessionId crafter, IInventory inventory)
        {
            var check = Check(recipeId, crafter, inventory);
            if (check != CraftResult.Crafted) return check;
            _book.TryGet(recipeId, out var recipe);
            return CraftTransaction.Apply(recipe.Ingredients, recipe.Output, recipe.OutputQuantity, inventory);
        }
    }
}
