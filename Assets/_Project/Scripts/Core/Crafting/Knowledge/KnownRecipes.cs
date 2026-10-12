using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Core.Crafting.Knowledge
{
    /// <summary>
    /// Lo aprendido por el jugador (GDD §17.3): conocimientos leídos, recetas enseñadas y volúmenes de
    /// cada libro. Responde a las <see cref="UnlockCondition"/> y expone su contenido para guardarlo (OF-13).
    /// </summary>
    public sealed class KnownRecipes : IRecipeKnowledge
    {
        private readonly HashSet<string> _read = new HashSet<string>(StringComparer.Ordinal);
        private readonly HashSet<string> _recipes = new HashSet<string>(StringComparer.Ordinal);
        private readonly Dictionary<string, SortedSet<int>> _volumes =
            new Dictionary<string, SortedSet<int>>(StringComparer.Ordinal);

        /// <summary>Ids de conocimientos leídos (para guardar), en orden estable.</summary>
        public IReadOnlyList<string> ReadKnowledge => _read.OrderBy(x => x, StringComparer.Ordinal).ToList();

        /// <summary>Ids de recetas aprendidas (para guardar), en orden estable.</summary>
        public IReadOnlyList<string> LearnedRecipes =>
            _recipes.OrderBy(x => x, StringComparer.Ordinal).ToList();

        public bool Knows(string knowledgeId) =>
            knowledgeId != null && (_read.Contains(knowledgeId) || _recipes.Contains(knowledgeId));

        public bool KnowsRecipe(string recipeId) => recipeId != null && _recipes.Contains(recipeId);

        public bool HasCompleted(string collectionId) =>
            VolumesRead(collectionId) >= KnowledgeDefinition.BookVolumes;

        public int VolumesRead(string collectionId) =>
            collectionId != null && _volumes.TryGetValue(collectionId, out var v) ? v.Count : 0;

        /// <summary>True si la condición de desbloqueo se cumple con lo aprendido.</summary>
        public bool Satisfies(UnlockCondition condition) =>
            Guard.NotNull(condition, nameof(condition)).IsMet(this);

        /// <summary>Registra un conocimiento leído; false si ya se había leído.</summary>
        public bool Register(KnowledgeDefinition knowledge)
        {
            Guard.NotNull(knowledge, nameof(knowledge));
            if (!_read.Add(knowledge.Id)) return false;
            foreach (var recipe in knowledge.Teaches) _recipes.Add(recipe);
            if (knowledge.IsVolume) AddVolume(knowledge.CollectionId, knowledge.Volume);
            return true;
        }

        /// <summary>Restaura una receta conocida desde el guardado.</summary>
        public void RestoreRecipe(string recipeId) => _recipes.Add(RequireId(recipeId));

        /// <summary>Restaura un conocimiento leído desde el guardado (volumen incluido).</summary>
        public void RestoreRead(KnowledgeDefinition knowledge) => Register(knowledge);

        private void AddVolume(string collectionId, int volume)
        {
            if (!_volumes.TryGetValue(collectionId, out var set))
                _volumes[collectionId] = set = new SortedSet<int>();
            set.Add(volume);
        }

        private static string RequireId(string id) =>
            string.IsNullOrWhiteSpace(id) ? throw new ArgumentException("Id vacío.", nameof(id)) : id;
    }
}
