using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Fabrica recetas de sinergia: exige que el grupo tenga ambas profesiones y que quien
    /// fabrica tenga una de ellas. Atómico como <see cref="Crafter"/>.
    /// </summary>
    public sealed class SynergyCrafter
    {
        private readonly ISynergyRecipeBook _book;

        public SynergyCrafter(ISynergyRecipeBook book)
        {
            _book = Guard.NotNull(book, nameof(book));
        }

        /// <summary>Comprueba receta, grupo, profesión e ingredientes sin tocar el inventario.</summary>
        public CraftResult Check(string recipeId, ProfessionId crafter, IEnumerable<ProfessionId> group,
            IInventory inventory)
        {
            Guard.NotNull(group, nameof(group));
            Guard.NotNull(inventory, nameof(inventory));
            if (!_book.TryGet(recipeId, out var recipe)) return CraftResult.UnknownRecipe;
            if (!recipe.IsUnlockedBy(group)) return CraftResult.SynergyLocked;
            if (!recipe.CanBeCraftedBy(crafter)) return CraftResult.WrongProfession;
            return recipe.HasIngredients(inventory) ? CraftResult.Crafted : CraftResult.MissingIngredients;
        }

        public CraftResult TryCraft(string recipeId, ProfessionId crafter, IEnumerable<ProfessionId> group,
            IInventory inventory)
        {
            var check = Check(recipeId, crafter, group, inventory);
            if (check != CraftResult.Crafted) return check;
            _book.TryGet(recipeId, out var recipe);
            return CraftTransaction.Apply(recipe.Ingredients, recipe.Output, recipe.OutputQuantity, inventory);
        }
    }
}
