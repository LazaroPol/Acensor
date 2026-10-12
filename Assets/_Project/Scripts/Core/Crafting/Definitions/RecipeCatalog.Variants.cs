using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Validación y resolución de variantes mejoradas del <see cref="RecipeCatalog"/>.</summary>
    public sealed partial class RecipeCatalog
    {
        /// <summary>
        /// Variante que la estación muestra en lugar de <paramref name="id"/>: sigue la cadena de
        /// mejoras conocidas por el jugador (la primera por id si hay varias). Sin mejoras conocidas
        /// devuelve la propia receta.
        /// </summary>
        public RecipeDefinition ResolveVariant(string id, IRecipeKnowledge knowledge)
        {
            Guard.NotNull(knowledge, nameof(knowledge));
            var current = Get(id);
            while (true)
            {
                var next = _improvers[current.Id].Where(r => r.IsKnownBy(knowledge))
                    .OrderBy(r => r.Id, StringComparer.Ordinal).FirstOrDefault();
                if (next == null) return current;
                current = next;
            }
        }

        private void ValidateImproves()
        {
            foreach (var recipe in All.Where(r => r.IsVariant))
            {
                if (!_byId.ContainsKey(recipe.Improves))
                    throw new ArgumentException($"{recipe.Id} mejora una receta inexistente: {recipe.Improves}.");
            }
            foreach (var recipe in All.Where(r => r.IsVariant))
            {
                var visited = new HashSet<string> { recipe.Id };
                for (var cur = recipe; cur.IsVariant; cur = _byId[cur.Improves])
                    if (!visited.Add(cur.Improves))
                        throw new ArgumentException($"Ciclo de variantes en {recipe.Id}.");
            }
        }
    }
}
