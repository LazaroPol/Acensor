using System.Collections.Generic;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>Catálogo de recetas consultable por id o por profesión.</summary>
    public interface IRecipeBook
    {
        int Count { get; }

        bool TryGet(string recipeId, out Recipe recipe);

        /// <summary>Recetas que puede fabricar la profesión indicada.</summary>
        IReadOnlyList<Recipe> For(ProfessionId profession);
    }
}
