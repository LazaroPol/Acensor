using System;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Datos serializables de un <see cref="KnowledgeDefinition"/>.</summary>
    [Serializable]
    public sealed class KnowledgeData
    {
        public string id;
        public KnowledgeType type;
        public Rarity rarity;
        public ProfessionId profession;
        public int minLevel = 1;
        public string[] teaches = new string[0];
        public string collectionId;
        public int volume;
    }
}
