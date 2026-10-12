using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Knowledge;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Convierte <see cref="KnowledgeData"/> ⇄ <see cref="KnowledgeDefinition"/>.</summary>
    public static class KnowledgeAssetMapper
    {
        public static KnowledgeDefinition ToDefinition(KnowledgeData data)
        {
            Guard.NotNull(data, nameof(data));
            var collection = string.IsNullOrWhiteSpace(data.collectionId) ? null : data.collectionId;
            return new KnowledgeDefinition(data.id, data.type, data.rarity, data.profession, data.minLevel,
                data.teaches, collection, data.volume);
        }

        public static KnowledgeData FromDefinition(KnowledgeDefinition knowledge)
        {
            Guard.NotNull(knowledge, nameof(knowledge));
            return new KnowledgeData
            {
                id = knowledge.Id, type = knowledge.Type, rarity = knowledge.Rarity,
                profession = knowledge.Profession, minLevel = knowledge.MinLevel,
                teaches = knowledge.Teaches.ToArray(), collectionId = knowledge.CollectionId,
                volume = knowledge.Volume
            };
        }
    }
}
