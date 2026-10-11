using System;
using System.Collections.Generic;

namespace Specimen.Core.Combat
{
    /// <summary>
    /// Resistencia por tipo de daño, acotada a [Min, Max]. Positiva reduce, negativa amplifica
    /// (vulnerabilidad). Un tipo sin entrada vale 0.
    /// </summary>
    public sealed class Resistances
    {
        public const float Min = -1f;
        public const float Max = 0.9f;

        private readonly Dictionary<DamageType, float> _values = new Dictionary<DamageType, float>();

        public static Resistances None => new Resistances();

        public float Of(DamageType type) => _values.TryGetValue(type, out var v) ? v : 0f;

        public Resistances Set(DamageType type, float value)
        {
            if (float.IsNaN(value)) throw new ArgumentOutOfRangeException(nameof(value), "No puede ser NaN.");
            _values[type] = Math.Clamp(value, Min, Max);
            return this;
        }

        /// <summary>Multiplicador de daño resultante: 1 - resistencia.</summary>
        public float MultiplierFor(DamageType type) => 1f - Of(type);
    }
}
