using Specimen.Core.Common;

namespace Specimen.Core.Movement
{
    /// <summary>
    /// Parámetros del movimiento del jugador (GDD 16, PG-03): velocidad al andar, multiplicador de sprint,
    /// Aguante por segundo que cuesta esprintar y Aguante mínimo para empezar a esprintar.
    /// </summary>
    public sealed class MovementConfig
    {
        public MovementConfig(float walkSpeed = 4f, float sprintMultiplier = 1.6f,
            float sprintStaminaPerSecond = 20f, float minStaminaToSprint = 10f)
        {
            WalkSpeed = Guard.Positive(walkSpeed, nameof(walkSpeed));
            SprintMultiplier = Guard.Positive(sprintMultiplier, nameof(sprintMultiplier));
            SprintStaminaPerSecond = Guard.NonNegative(sprintStaminaPerSecond, nameof(sprintStaminaPerSecond));
            MinStaminaToSprint = Guard.NonNegative(minStaminaToSprint, nameof(minStaminaToSprint));
        }

        /// <summary>Por debajo de este módulo la entrada cuenta como «quieto».</summary>
        public const float DeadZone = 0.01f;

        public float WalkSpeed { get; }
        public float SprintMultiplier { get; }
        public float SprintStaminaPerSecond { get; }
        public float MinStaminaToSprint { get; }
    }
}
