using Specimen.Core.Common;

namespace Specimen.Core.Movement
{
    /// <summary>
    /// Convierte la entrada del jugador (stick/WASD) en velocidad plana relativa al giro de la cámara,
    /// aplicando el sprint. El motor de Unity solo mueve el CharacterController con el resultado.
    /// </summary>
    public sealed class PlanarMover
    {
        private readonly MovementConfig _config;
        private readonly SprintController _sprint;

        public PlanarMover(MovementConfig config, SprintController sprint)
        {
            _config = Guard.NotNull(config, nameof(config));
            _sprint = Guard.NotNull(sprint, nameof(sprint));
        }

        public bool IsSprinting => _sprint.IsSprinting;

        public PlanarVector Step(PlanarVector input, float cameraYawDegrees, bool wantsSprint, float deltaSeconds)
        {
            var direction = input.ClampMagnitude(1f);
            bool moving = direction.Magnitude > MovementConfig.DeadZone;
            bool sprinting = _sprint.Update(wantsSprint, moving, deltaSeconds);
            if (!moving) return PlanarVector.Zero;
            float speed = _config.WalkSpeed * (sprinting ? _config.SprintMultiplier : 1f);
            return direction.RotateByYaw(cameraYawDegrees).Scale(speed);
        }
    }
}
