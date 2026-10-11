using System;
using Specimen.Core.Common;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Plantilla de enemigo: identificador, estadísticas en piso 1 / anillo 1, tipo de daño y resistencias.</summary>
    public sealed class EnemyArchetype
    {
        public EnemyArchetype(string id, EnemyStats baseStats, DamageType attackType, Resistances resistances)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("El id no puede estar vacío.", nameof(id));
            Id = id;
            BaseStats = Guard.NotNull(baseStats, nameof(baseStats));
            AttackType = attackType;
            Resistances = Guard.NotNull(resistances, nameof(resistances));
        }

        public string Id { get; }
        public EnemyStats BaseStats { get; }
        public DamageType AttackType { get; }
        public Resistances Resistances { get; }
    }
}
