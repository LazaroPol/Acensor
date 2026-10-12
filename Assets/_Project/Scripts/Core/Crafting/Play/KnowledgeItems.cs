using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Play
{
    /// <summary>
    /// Objeto de bolsa de cada conocimiento (GDD §17.3): mismo id que el conocimiento, una unidad por hueco.
    /// Se cachean para que la bolsa reconozca siempre la misma definición.
    /// </summary>
    public static class KnowledgeItems
    {
        public const float Weight = 0.1f;
        private static readonly Dictionary<string, ItemDefinition> Cache = new Dictionary<string, ItemDefinition>();

        public static ItemDefinition For(KnowledgeDefinition knowledge)
        {
            Guard.NotNull(knowledge, nameof(knowledge));
            lock (Cache)
            {
                if (Cache.TryGetValue(knowledge.Id, out var item)) return item;
                item = new ItemDefinition(knowledge.Id, NameOf(knowledge), ItemCategory.Consumable,
                    knowledge.Rarity, weight: Weight, iconKey: "conocimiento");
                Cache.Add(knowledge.Id, item);
                return item;
            }
        }

        public static string NameOf(KnowledgeDefinition knowledge)
        {
            string trade = StationLabels.Profession(knowledge.Profession);
            return knowledge.Type switch
            {
                KnowledgeType.Scroll => $"Pergamino de {trade}",
                KnowledgeType.Blueprint => $"Plano de {trade}",
                KnowledgeType.Treatise => $"Tratado de {trade}",
                _ => $"Libro de {trade}, vol. {knowledge.Volume}"
            };
        }
    }
}
