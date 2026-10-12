using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Talents
{
    /// <summary>Aplica los talentos a la hoja de stats (modificadores con fuente propia) y expone las mejoras de hechizos.</summary>
    public sealed class TalentBonuses
    {
        private static readonly object Source = new object();

        private TalentBonuses(float spellPower, float cooldownReduction)
        {
            SpellPower = spellPower;
            CooldownReduction = cooldownReduction;
        }

        public float SpellPower { get; }
        public float CooldownReduction { get; }

        public static TalentBonuses None => new TalentBonuses(0f, 0f);

        public static TalentBonuses Apply(TalentAllocation allocation, StatSheet sheet)
        {
            Guard.NotNull(allocation, nameof(allocation));
            Guard.NotNull(sheet, nameof(sheet));
            foreach (var type in sheet.Types.ToList()) sheet.Get(type).RemoveAllFromSource(Source);
            float power = 0f, cooldown = 0f;
            foreach (var (talent, rank) in allocation.Ranked())
            {
                float value = talent.PerRank * rank;
                switch (talent.Kind)
                {
                    case TalentEffectKind.StatPorcentaje:
                        if (sheet.Has(talent.Stat)) sheet.AddModifier(talent.Stat, StatModifier.Percent(value, Source));
                        break;
                    case TalentEffectKind.StatPlano:
                        if (sheet.Has(talent.Stat)) sheet.AddModifier(talent.Stat, StatModifier.Flat(value, Source));
                        break;
                    case TalentEffectKind.PoderHechizos: power += value; break;
                    default: cooldown += value; break;
                }
            }
            return new TalentBonuses(power, System.Math.Min(cooldown, 0.5f));
        }
    }
}
