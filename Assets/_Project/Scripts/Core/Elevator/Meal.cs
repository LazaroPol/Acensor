using System;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Plato cocinable en la Cocina (GDD §6-§7): otorga un modificador temporal a un atributo
    /// durante <see cref="BaseDurationSeconds"/> (antes de aplicar el nivel de la Cocina).
    /// </summary>
    public sealed class Meal
    {
        public string Id { get; }
        public StatType Stat { get; }
        public ModifierKind Kind { get; }
        public float Value { get; }
        public float BaseDurationSeconds { get; }

        public Meal(string id, StatType stat, ModifierKind kind, float value, float baseDurationSeconds)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("El plato necesita id.", nameof(id));
            Id = id;
            Stat = stat;
            Kind = kind;
            Value = value;
            BaseDurationSeconds = Guard.Positive(baseDurationSeconds, nameof(baseDurationSeconds));
        }

        /// <summary>Crea el modificador del plato marcado con la fuente indicada.</summary>
        public StatModifier CreateModifier(object source) => new StatModifier(Kind, Value, source);
    }
}
