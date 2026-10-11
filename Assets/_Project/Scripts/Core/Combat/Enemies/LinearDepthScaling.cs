using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>
    /// Escalado lineal: stat = base × (1 + ritmo × pasos de piso) × (1 + bonus × pasos de anillo).
    /// La armadura solo escala por piso (el anillo endurece por vida y daño).
    /// </summary>
    public sealed class LinearDepthScaling : IDepthScaling
    {
        private readonly DepthScalingConfig _config;

        public LinearDepthScaling(DepthScalingConfig config)
        {
            _config = Guard.NotNull(config, nameof(config));
        }

        public EnemyStats Scale(EnemyStats baseStats, EnemyDepth depth)
        {
            Guard.NotNull(baseStats, nameof(baseStats));
            Guard.NotNull(depth, nameof(depth));
            float ring = 1f + _config.PerRing * depth.RingSteps;
            int floors = depth.FloorSteps;
            return new EnemyStats(
                baseStats.MaxHealth * Growth(_config.HealthPerFloor, floors) * ring,
                baseStats.Damage * Growth(_config.DamagePerFloor, floors) * ring,
                baseStats.Armor * Growth(_config.ArmorPerFloor, floors));
        }

        private static float Growth(float rate, int steps) => 1f + rate * steps;
    }
}
