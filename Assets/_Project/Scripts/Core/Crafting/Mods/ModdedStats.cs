using System;
using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>
    /// Stats efectivos (GDD §17.4): efectivo = (base × multCalidad + Σ planos) × (1 + Σ %), nunca negativo.
    /// La calidad escala daños y armadura; velocidad y peso no. Durabilidad máxima =
    /// (baseRareza × multDurCalidad + Σ planos) × (1 + Σ %), mínimo 1 si el objeto se desgasta.
    /// </summary>
    public static class ModdedStats
    {
        public static ModdedStatsView Compute(ModdedItem item, GearStatSheet baseStats)
        {
            Guard.NotNull(item, nameof(item));
            Guard.NotNull(baseStats, nameof(baseStats));
            var effective = GearStatSheet.Empty;
            foreach (GearStat stat in Enum.GetValues(typeof(GearStat)))
            {
                if (stat == GearStat.Durabilidad) continue;
                var scaled = baseStats[stat] * QualityFactor(item, stat);
                effective = effective.With(stat, Apply(item, stat, scaled));
            }
            var durability = MaxDurability(item);
            effective = effective.With(GearStat.Durabilidad, durability);
            return new ModdedStatsView(baseStats.With(GearStat.Durabilidad, item.Item.Definition.MaxDurability),
                effective, durability);
        }

        private static int MaxDurability(ModdedItem item)
        {
            var baseMax = item.Item.Definition.MaxDurability;
            if (baseMax <= 0) return 0;
            var scaled = baseMax * (item.Grade?.DurabilityMultiplier ?? 1f);
            var value = (int)Math.Round(Apply(item, GearStat.Durabilidad, scaled), MidpointRounding.AwayFromZero);
            return Math.Max(1, value);
        }

        private static float Apply(ModdedItem item, GearStat stat, float scaledBase)
        {
            float flat = 0f, percent = 0f;
            foreach (var mod in item.Mods)
                foreach (var m in mod.Modifiers)
                {
                    if (m.Stat != stat) continue;
                    if (m.Kind == GearModifierKind.Flat) flat += m.Value;
                    else percent += m.Value;
                }
            return Math.Max(0f, (scaledBase + flat) * (1f + percent));
        }

        private static float QualityFactor(ModdedItem item, GearStat stat)
        {
            if (item.Grade == null) return 1f;
            return stat == GearStat.Velocidad || stat == GearStat.Peso ? 1f : item.Grade.StatMultiplier;
        }
    }
}
