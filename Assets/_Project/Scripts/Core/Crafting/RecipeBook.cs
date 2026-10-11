using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting
{
    /// <summary>Recetario en memoria; los ids de receta son únicos.</summary>
    public sealed class RecipeBook : IRecipeBook
    {
        private readonly Dictionary<string, Recipe> _byId = new Dictionary<string, Recipe>();

        public RecipeBook(IEnumerable<Recipe> recipes)
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

        public bool TryGet(string recipeId, out Recipe recipe)
        {
            recipe = null;
            return recipeId != null && _byId.TryGetValue(recipeId, out recipe);
        }

        public IReadOnlyList<Recipe> For(ProfessionId profession) =>
            _byId.Values.Where(r => r.Profession == profession).ToList().AsReadOnly();
    }
}
