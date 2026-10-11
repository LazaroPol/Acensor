using System.Collections.Generic;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>Catálogo de recetas de sinergia consultable por id o por grupo.</summary>
    public interface ISynergyRecipeBook
    {
        int Count { get; }

        bool TryGet(string recipeId, out SynergyRecipe recipe);

        /// <summary>Recetas que desbloquea el conjunto de profesiones del grupo.</summary>
        IReadOnlyList<SynergyRecipe> UnlockedFor(IEnumerable<ProfessionId> group);
    }
}
