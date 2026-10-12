using System;
using Specimen.Core.Common;
using Specimen.Core.Randomness;

namespace Specimen.Core.Equipment
{
    /// <summary>
    /// Daño y velocidad fijos de un arma, como en WoW: cada golpe hace entre <see cref="Min"/> y <see cref="Max"/>
    /// más la aportación del poder de ataque, una vez cada <see cref="Speed"/> segundos.
    /// </summary>
    public sealed class WeaponSpec
    {
        /// <summary>Poder de ataque (stat Daño) que suma 1 de daño por segundo de velocidad del arma.</summary>
        public const float PowerPerDamage = 3.5f;

        public const float MeleeRange = 3f;
        public const float RangedRange = 25f;

        public WeaponSpec(WeaponKind kind, float min, float max, float speed)
        {
            Kind = kind;
            Min = Guard.NonNegative(min, nameof(min));
            Max = Math.Max(Min, max);
            Speed = Guard.Positive(speed, nameof(speed));
        }

        public static WeaponSpec Unarmed => new WeaponSpec(WeaponKind.Daga, 1f, 2f, 2f);

        public WeaponKind Kind { get; }
        public float Min { get; }
        public float Max { get; }
        public float Speed { get; }
        public bool IsRanged => Kind == WeaponKind.Varita;
        public bool IsTwoHanded => Kind == WeaponKind.Lanza || Kind == WeaponKind.Mandoble || Kind == WeaponKind.Baston;
        public float Range => IsRanged ? RangedRange : MeleeRange;
        public float Dps => (Min + Max) / 2f / Speed;

        /// <summary>Bono plano por golpe del poder de ataque (escala con la velocidad: armas lentas pegan más fuerte).</summary>
        public float PowerBonus(float attackPower) => Math.Max(0f, attackPower) * Speed / PowerPerDamage;

        public float Roll(float attackPower, IRandom random)
        {
            Guard.NotNull(random, nameof(random));
            return random.Range(Min, Max) + PowerBonus(attackPower);
        }

        public float MinHit(float attackPower) => Min + PowerBonus(attackPower);

        public float MaxHit(float attackPower) => Max + PowerBonus(attackPower);

        public static string KindName(WeaponKind kind) => kind switch
        {
            WeaponKind.Baston => "Bastón",
            _ => kind.ToString()
        };
    }
}
