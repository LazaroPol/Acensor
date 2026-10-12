using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Professions;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Curva de oficio", fileName = "CurvaOficio")]
    public sealed class ProfessionCurveAsset : CraftingAsset<CurveData>
    {
        public ProfessionCurve ToCurve() => CurveAssetMapper.ToCurve(Data);
        public ProfessionMilestones ToMilestones() => CurveAssetMapper.ToMilestones(Data);
    }
}
