using Specimen.Core.Common;

namespace Specimen.Core.Life
{
    /// <summary>Fracción de vida con la que vuelve un Espectro según cómo se le reviva.</summary>
    public sealed class ReviveRules
    {
        public static readonly ReviveRules Default = new ReviveRules(1f, 0.35f);

        public ReviveRules(float elevatorHealthFraction, float fieldHealthFraction)
        {
            ElevatorHealthFraction = Fraction(elevatorHealthFraction, nameof(elevatorHealthFraction));
            FieldHealthFraction = Fraction(fieldHealthFraction, nameof(fieldHealthFraction));
        }

        public float ElevatorHealthFraction { get; }
        public float FieldHealthFraction { get; }

        public float HealthFractionFor(ReviveMethod method)
            => method == ReviveMethod.ElevatorBunk ? ElevatorHealthFraction : FieldHealthFraction;

        private static float Fraction(float value, string name)
        {
            Guard.Positive(value, name);
            return value > 1f ? throw new System.ArgumentOutOfRangeException(name, value, "Debe ser <= 1.") : value;
        }
    }
}
