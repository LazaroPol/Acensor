using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Authoring;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Reglas de calidad", fileName = "ReglasCalidad")]
    public sealed class QualityRulesAsset : CraftingAsset<QualityData>
    {
        public QualityRules ToRules() => QualityAssetMapper.ToRules(Data);
    }
}
