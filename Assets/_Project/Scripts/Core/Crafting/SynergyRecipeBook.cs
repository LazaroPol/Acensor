using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>Recetario de sinergias en memoria; los ids son únicos.</summary>
    public sealed class SynergyRecipeBook : ISynergyRecipeBook
    {
        private readonly Dictionary<string, SynergyRecipe> _byId = new Dictionary<string, SynergyRecipe>();

        public SynergyRecipeBook(IEnumerable<SynergyRecipe> recipes)
        {
            foreach (var recipe in Guard.NotNull(recipes, nameof(recipes)))
            {
                Guard.NotNull(recipe, nameof(recipes));
                if (_byId.ContainsKey(recipe.Id))
                    throw new ArgumentException($"Receta duplicada: {recipe.Id}.", nameof(recipes));
                _byId.Add(recipe.Id, recipe);
            }
        }

        public int Count => _byId.Count;

        public bool TryGet(string recipeId, out SynergyRecipe recipe)
        {
            recipe = null;
            return recipeId != null && _byId.TryGetValue(recipeId, out recipe);
        }

        public IReadOnlyList<SynergyRecipe> UnlockedFor(IEnumerable<ProfessionId> group)
        {
            var present = Guard.NotNull(group, nameof(group)).ToList();
            return _byId.Values.Where(r => r.IsUnlockedBy(present)).ToList().AsReadOnly();
        }
    }
}
