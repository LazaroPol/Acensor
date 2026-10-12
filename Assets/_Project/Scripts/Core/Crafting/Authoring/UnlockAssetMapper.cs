using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>
    /// Aplana una <see cref="UnlockCondition"/> en dos listas (conocimientos y colecciones, todas exigidas)
    /// y la reconstruye. Vacío = <see cref="UnlockCondition.AlwaysKnown"/>.
    /// </summary>
    public static class UnlockAssetMapper
    {
        public static UnlockCondition ToCondition(IEnumerable<string> knowledge, IEnumerable<string> collections)
        {
            var parts = (knowledge ?? Enumerable.Empty<string>()).Select(UnlockCondition.KnowledgeRequired)
                .Concat((collections ?? Enumerable.Empty<string>()).Select(UnlockCondition.CollectionComplete))
                .ToArray();
            if (parts.Length == 0) return UnlockCondition.AlwaysKnown;
            return parts.Length == 1 ? parts[0] : UnlockCondition.All(parts);
        }

        public static (string[] knowledge, string[] collections) Flatten(UnlockCondition condition)
        {
            var knowledge = new List<string>();
            var collections = new List<string>();
            Walk(condition, knowledge, collections);
            return (knowledge.ToArray(), collections.ToArray());
        }

        private static void Walk(UnlockCondition condition, List<string> knowledge, List<string> collections)
        {
            switch (condition)
            {
                case null:
                case AlwaysKnownCondition _:
                    return;
                case KnowledgeRequiredCondition k: knowledge.Add(k.KnowledgeId); return;
                case CollectionCompleteCondition c: collections.Add(c.CollectionId); return;
                case AllOfCondition all:
                    foreach (var inner in all.Conditions) Walk(inner, knowledge, collections);
                    return;
                default:
                    throw new NotSupportedException($"Condición sin mapper: {condition.GetType().Name}");
            }
        }
    }
}
