using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Enemigo concreto ya escalado a su profundidad, listo para el combate.</summary>
    public sealed class Enemy
    {
        public Enemy(EnemyArchetype archetype, EnemyDepth depth, EnemyStats stats)
        {
            Archetype = Guard.NotNull(archetype, nameof(archetype));
            Depth = Guard.NotNull(depth, nameof(depth));
            Stats = Guard.NotNull(stats, nameof(stats));
            Defense = new DefenseProfile(stats.Armor, archetype.Resistances);
        }

        public EnemyArchetype Archetype { get; }
        public EnemyDepth Depth { get; }
        public EnemyStats Stats { get; }
        public DefenseProfile Defense { get; }

        /// <summary>Golpe base del enemigo (puede ser crítico) con su tipo de daño.</summary>
        public Hit Attack() => new Hit(Stats.Damage, Archetype.AttackType);
    }
}
