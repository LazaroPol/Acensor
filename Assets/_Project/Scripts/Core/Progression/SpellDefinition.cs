using System;
using Specimen.Core.Combat;
using Specimen.Core.Common;

namespace Specimen.Core.Progression
{
    /// <summary>
    /// Hechizo o técnica que un kit aprende al subir de nivel. <see cref="Power"/> es multiplicador de daño
    /// (golpe, proyectil, área), vida curada, reducción de daño (escudo), metros (impulso) o % de daño (furia).
    /// </summary>
    public sealed class SpellDefinition
    {
        public SpellDefinition(string id, string name, string description, int level, SpellKind kind, float power,
            float cost, float cooldown, DamageType type = DamageType.Physical, float radius = 0f, float duration = 0f)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            if (level < 1 || level > LevelCurve.MaxLevel) throw new ArgumentOutOfRangeException(nameof(level));
            Id = id;
            Name = Guard.NotNull(name, nameof(name));
            Description = Guard.NotNull(description, nameof(description));
            Level = level;
            Kind = kind;
            Power = Guard.Positive(power, nameof(power));
            Cost = Guard.NonNegative(cost, nameof(cost));
            Cooldown = Guard.NonNegative(cooldown, nameof(cooldown));
            Type = type;
            Radius = Guard.NonNegative(radius, nameof(radius));
            Duration = Guard.NonNegative(duration, nameof(duration));
        }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Level { get; }
        public SpellKind Kind { get; }
        public float Power { get; }
        public float Cost { get; }
        public float Cooldown { get; }
        public DamageType Type { get; }
        public float Radius { get; }
        public float Duration { get; }

        public bool DealsDamage => Kind == SpellKind.Golpe || Kind == SpellKind.Proyectil || Kind == SpellKind.Area;
    }
}
