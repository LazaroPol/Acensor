using Specimen.Core.Common;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Parámetros de la Hoguera por nivel: vida y stamina regeneradas por segundo y segundos de descanso
    /// para recuperar un punto de fatiga (a nivel N se tarda 1/N de ese tiempo).
    /// </summary>
    public sealed class BonfireConfig
    {
        public static readonly BonfireConfig Default = new BonfireConfig(2f, 10f, 20f);

        public float HealthPerSecondPerLevel { get; }
        public float StaminaPerSecondPerLevel { get; }
        public float SecondsPerFatiguePoint { get; }

        public BonfireConfig(float healthPerSecondPerLevel, float staminaPerSecondPerLevel,
            float secondsPerFatiguePoint)
        {
            HealthPerSecondPerLevel = Guard.NonNegative(healthPerSecondPerLevel, nameof(healthPerSecondPerLevel));
            StaminaPerSecondPerLevel = Guard.NonNegative(staminaPerSecondPerLevel, nameof(staminaPerSecondPerLevel));
            SecondsPerFatiguePoint = Guard.Positive(secondsPerFatiguePoint, nameof(secondsPerFatiguePoint));
        }

        public float FatigueIntervalAt(int level) => SecondsPerFatiguePoint / level;
    }
}
