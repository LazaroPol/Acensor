using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Convierte <see cref="QualityData"/> ⇄ <see cref="QualityRules"/>.</summary>
    public static class QualityAssetMapper
    {
        public static QualityRules ToRules(QualityData data)
        {
            Guard.NotNull(data, nameof(data));
            var weights = new QualityWeights(data.perLevel, data.perTier, data.perStationLevel,
                data.randomMin, data.randomMax);
            var grades = (data.grades ?? new GradeData[0]).Select(g => new QualityGrade(g.quality, g.name,
                g.minScore, g.modSlots, g.statMultiplier, g.durabilityMultiplier));
            return new QualityRules(grades.ToList(), weights);
        }

        public static QualityData FromRules(QualityRules rules)
        {
            Guard.NotNull(rules, nameof(rules));
            var w = rules.Weights;
            return new QualityData
            {
                perLevel = w.PerLevel, perTier = w.PerTier, perStationLevel = w.PerStationLevel,
                randomMin = w.RandomMin, randomMax = w.RandomMax,
                grades = rules.Grades.Select(g => new GradeData
                {
                    quality = g.Quality, name = g.Name, minScore = g.MinScore, modSlots = g.ModSlots,
                    statMultiplier = g.StatMultiplier, durabilityMultiplier = g.DurabilityMultiplier
                }).ToArray()
            };
        }
    }
}
