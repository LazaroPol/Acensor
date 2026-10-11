using System;

namespace Specimen.Core.Stats
{
    /// <summary>Modificador inmutable de un atributo. <see cref="Source"/> permite retirarlo en bloque.</summary>
    public sealed class StatModifier
    {
        public ModifierKind Kind { get; }
        public float Value { get; }
        public object Source { get; }

        public StatModifier(ModifierKind kind, float value, object source = null)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
                throw new ArgumentOutOfRangeException(nameof(value), value, "Debe ser un número finito.");
            if (kind == ModifierKind.Multiplier && value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Un multiplicador debe ser >= 0.");
            Kind = kind;
            Value = value;
            Source = source;
        }

        public static StatModifier Flat(float value, object source = null)
            => new StatModifier(ModifierKind.Flat, value, source);

        public static StatModifier Percent(float fraction, object source = null)
            => new StatModifier(ModifierKind.PercentAdd, fraction, source);

        public static StatModifier Multiply(float factor, object source = null)
            => new StatModifier(ModifierKind.Multiplier, factor, source);
    }
}
