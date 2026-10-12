using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Condición para <b>conocer</b> una receta (GDD §17.2). Estrategias combinables con «Y».</summary>
    public abstract class UnlockCondition
    {
        public abstract bool IsMet(IRecipeKnowledge knowledge);

        public static UnlockCondition AlwaysKnown { get; } = new AlwaysKnownCondition();

        public static UnlockCondition KnowledgeRequired(string knowledgeId) =>
            new KnowledgeRequiredCondition(knowledgeId);

        public static UnlockCondition CollectionComplete(string collectionId) =>
            new CollectionCompleteCondition(collectionId);

        public static UnlockCondition All(params UnlockCondition[] conditions) =>
            new AllOfCondition(conditions);

        /// <summary>Combina esta condición con otra («Y»).</summary>
        public UnlockCondition And(UnlockCondition other) => All(this, other);

        internal static string RequireId(string id, string paramName)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", paramName);
            return id;
        }

        internal static IReadOnlyList<UnlockCondition> RequireAll(IEnumerable<UnlockCondition> conditions)
        {
            var list = Guard.NotNull(conditions, nameof(conditions)).ToList();
            if (list.Count == 0 || list.Any(c => c == null))
                throw new ArgumentException("Faltan condiciones válidas.", nameof(conditions));
            return list.AsReadOnly();
        }
    }
}
