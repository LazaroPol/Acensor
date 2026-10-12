using Specimen.Core.Common;

namespace Specimen.Core.Movement
{
    /// <summary>Parámetros de la esquiva: impulso breve en una dirección que cuesta Aguante y tiene reutilización.</summary>
    public sealed class DodgeConfig
    {
        public DodgeConfig(float speed = 12f, float durationSeconds = 0.25f, float staminaCost = 25f,
            float cooldownSeconds = 0.6f)
        {
            Speed = Guard.Positive(speed, nameof(speed));
            DurationSeconds = Guard.Positive(durationSeconds, nameof(durationSeconds));
            StaminaCost = Guard.NonNegative(staminaCost, nameof(staminaCost));
            CooldownSeconds = Guard.NonNegative(cooldownSeconds, nameof(cooldownSeconds));
        }

        public float Speed { get; }
        public float DurationSeconds { get; }
        public float StaminaCost { get; }
        public float CooldownSeconds { get; }
    }
}
