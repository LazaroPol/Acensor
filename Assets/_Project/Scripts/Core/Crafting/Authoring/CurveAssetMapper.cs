using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Convierte <see cref="CurveData"/> ⇄ <see cref="ProfessionCurve"/> + <see cref="ProfessionMilestones"/>.</summary>
    public static class CurveAssetMapper
    {
        public static ProfessionCurve ToCurve(CurveData data) =>
            new ProfessionCurve(Guard.NotNull(data, nameof(data)).xpToNext ?? new int[0]);

        public static ProfessionMilestones ToMilestones(CurveData data) =>
            new ProfessionMilestones((Guard.NotNull(data, nameof(data)).milestones ?? new MilestoneData[0])
                .Select(m => new ProfessionMilestone(m.level, m.materialTier, m.timeBonus, m.maxQualityBonus)));

        public static CurveData FromCurve(ProfessionCurve curve, ProfessionMilestones milestones)
        {
            Guard.NotNull(curve, nameof(curve));
            Guard.NotNull(milestones, nameof(milestones));
            return new CurveData
            {
                xpToNext = Enumerable.Range(1, curve.MaxLevel - 1).Select(curve.XpToNext).ToArray(),
                milestones = milestones.Steps.Select(s => new MilestoneData
                {
                    level = s.Level, materialTier = s.MaterialTier, timeBonus = s.TimeBonus,
                    maxQualityBonus = s.MaxQualityBonus
                }).ToArray()
            };
        }
    }
}
