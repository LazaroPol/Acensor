using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>
    /// Ritmos de crecimiento (fracción por paso): por piso para vida, daño y armadura, y un bonus
    /// por anillo de profundidad que se aplica a vida y daño.
    /// </summary>
    public sealed class DepthScalingConfig
    {
        public DepthScalingConfig(float healthPerFloor, float damagePerFloor, float armorPerFloor, float perRing)
        {
            HealthPerFloor = Guard.NonNegative(healthPerFloor, nameof(healthPerFloor));
            DamagePerFloor = Guard.NonNegative(damagePerFloor, nameof(damagePerFloor));
            ArmorPerFloor = Guard.NonNegative(armorPerFloor, nameof(armorPerFloor));
            PerRing = Guard.NonNegative(perRing, nameof(perRing));
        }

        /// <summary>Valores de partida: +12 % vida, +8 % daño, +5 % armadura por piso; +15 % por anillo.</summary>
        public static DepthScalingConfig Default => new DepthScalingConfig(0.12f, 0.08f, 0.05f, 0.15f);

        public float HealthPerFloor { get; }
        public float DamagePerFloor { get; }
        public float ArmorPerFloor { get; }
        public float PerRing { get; }
    }
}
