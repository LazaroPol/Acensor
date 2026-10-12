using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Knowledge;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Conocimiento", fileName = "Conocimiento")]
    public sealed class KnowledgeAsset : CraftingAsset<KnowledgeData>
    {
        public KnowledgeDefinition ToDefinition() => KnowledgeAssetMapper.ToDefinition(Data);
    }
}
