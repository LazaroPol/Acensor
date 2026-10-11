using System;
using Specimen.Core.Common;
using Specimen.Core.Randomness;

namespace Specimen.Core.Combat
{
    /// <summary>
    /// Resuelve un golpe: crítico (con IRandom inyectado) → armadura (solo físico) → resistencia.
    /// El crítico multiplica el daño bruto; el resultado nunca es negativo.
    /// </summary>
    public sealed class DamageCalculator
    {
        private readonly IRandom _random;
        private readonly ArmorMitigation _armor;

        public DamageCalculator(IRandom random, ArmorMitigation armor = null)
        {
            _random = Guard.NotNull(random, nameof(random));
            _armor = armor ?? new ArmorMitigation();
        }

        public DamageResult Resolve(Hit hit, CritRules crit, DefenseProfile target)
        {
            Guard.NotNull(hit, nameof(hit));
            Guard.NotNull(crit, nameof(crit));
            Guard.NotNull(target, nameof(target));

            bool isCrit = hit.CanCrit && crit.Chance > 0f && _random.Chance(crit.Chance);
            float raw = isCrit ? hit.Amount * crit.Multiplier : hit.Amount;
            float final = raw
                * _armor.MultiplierFor(hit.Type, target.Armor)
                * target.Resistances.MultiplierFor(hit.Type);
            return new DamageResult(raw, Math.Max(0f, final), isCrit, hit.Type);
        }
    }
}
