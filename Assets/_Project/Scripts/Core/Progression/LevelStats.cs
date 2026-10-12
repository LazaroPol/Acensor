using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Progression
{
    /// <summary>Crecimiento de stats por nivel: +8 % de vida y +6 % de daño y recurso por nivel por encima del 1.</summary>
    public static class LevelStats
    {
        public const float HealthPerLevel = 0.08f;
        public const float DamagePerLevel = 0.06f;
        public const float ResourcePerLevel = 0.06f;

        private static readonly object Source = new object();

        public static void Apply(StatSheet sheet, int level)
        {
            Guard.NotNull(sheet, nameof(sheet));
            int steps = level - 1;
            foreach (var type in new[] { StatType.MaxHealth, StatType.Damage, StatType.MaxClassResource })
                if (sheet.Has(type)) sheet.Get(type).RemoveAllFromSource(Source);
            if (steps <= 0) return;
            Add(sheet, StatType.MaxHealth, HealthPerLevel * steps);
            Add(sheet, StatType.Damage, DamagePerLevel * steps);
            Add(sheet, StatType.MaxClassResource, ResourcePerLevel * steps);
        }

        private static void Add(StatSheet sheet, StatType type, float fraction)
        {
            if (sheet.Has(type)) sheet.AddModifier(type, StatModifier.Percent(fraction, Source));
        }
    }
}
