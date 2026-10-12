using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>
    /// Catálogo de recetas de oficio: ids únicos, consultas por estación y por oficio y resolución
    /// de variantes mejoradas (GDD §17.2). Valida que todo <c>Improves</c> exista y no forme ciclos.
    /// </summary>
    public sealed partial class RecipeCatalog
    {
        private readonly Dictionary<string, RecipeDefinition> _byId;
        private readonly ILookup<string, RecipeDefinition> _improvers;

        public IReadOnlyList<RecipeDefinition> All { get; }

        public RecipeCatalog(IEnumerable<RecipeDefinition> recipes)
        {
            All = Guard.NotNull(recipes, nameof(recipes)).ToList().AsReadOnly();
            if (All.Any(r => r == null)) throw new ArgumentException("Receta nula.", nameof(recipes));
            _byId = new Dictionary<string, RecipeDefinition>();
            foreach (var recipe in All)
            {
                if (_byId.ContainsKey(recipe.Id))
                    throw new ArgumentException($"Id de receta repetido: {recipe.Id}.", nameof(recipes));
                _byId.Add(recipe.Id, recipe);
            }
            _improvers = All.Where(r => r.IsVariant).ToLookup(r => r.Improves);
            ValidateImproves();
        }

        public static RecipeCatalog FromLegacy(IEnumerable<Recipe> recipes) =>
            new RecipeCatalog(Guard.NotNull(recipes, nameof(recipes)).Select(RecipeDefinition.FromLegacy));

        public int Count => All.Count;

        public bool Contains(string id) => id != null && _byId.ContainsKey(id);

        public bool TryGet(string id, out RecipeDefinition recipe)
        {
            recipe = null;
            return id != null && _byId.TryGetValue(id, out recipe);
        }

        public RecipeDefinition Get(string id) =>
            TryGet(id, out var recipe) ? recipe : throw new KeyNotFoundException($"Receta desconocida: {id}.");

        public IReadOnlyList<RecipeDefinition> ForStation(StationType station) =>
            All.Where(r => r.Station == station).ToList().AsReadOnly();

        public IReadOnlyList<RecipeDefinition> ForProfession(ProfessionId profession) =>
            All.Where(r => r.Profession == profession).ToList().AsReadOnly();

        /// <summary>Variantes que mejoran directamente la receta indicada.</summary>
        public IReadOnlyList<RecipeDefinition> ImprovementsOf(string id) =>
            _improvers[id ?? string.Empty].ToList().AsReadOnly();
    }
}
