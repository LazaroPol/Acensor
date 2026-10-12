using System;
using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Distancias (m) y ritmo de la IA cuerpo a cuerpo: agro, abandono, alcance y enfriamiento.</summary>
    public sealed class EnemyBrainConfig
    {
        public EnemyBrainConfig(float aggroRange = 9f, float leashRange = 18f, float attackRange = 1.6f,
            float attackCooldown = 1.6f, float moveSpeed = 2.8f)
        {
            AggroRange = Guard.Positive(aggroRange, nameof(aggroRange));
            LeashRange = Guard.Positive(leashRange, nameof(leashRange));
            AttackRange = Guard.Positive(attackRange, nameof(attackRange));
            if (leashRange < aggroRange || attackRange > aggroRange)
                throw new ArgumentException("Debe cumplirse alcance ≤ agro ≤ abandono.");
            AttackCooldown = Guard.NonNegative(attackCooldown, nameof(attackCooldown));
            MoveSpeed = Guard.Positive(moveSpeed, nameof(moveSpeed));
        }

        public float AggroRange { get; }
        public float LeashRange { get; }
        public float AttackRange { get; }
        public float AttackCooldown { get; }
        public float MoveSpeed { get; }

        /// <summary>Configuración escalada al tamaño de la criatura (llega más lejos, igual de rápida).</summary>
        public EnemyBrainConfig ScaledBy(float scale)
            => new EnemyBrainConfig(Math.Max(AggroRange, AttackRange * scale), Math.Max(LeashRange, AttackRange * scale),
                AttackRange * Guard.Positive(scale, nameof(scale)), AttackCooldown, MoveSpeed);
    }
}
