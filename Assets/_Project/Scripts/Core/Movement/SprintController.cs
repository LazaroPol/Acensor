using Specimen.Core.Common;
using Specimen.Core.Vitals;

namespace Specimen.Core.Movement
{
    /// <summary>
    /// Decide si el jugador esprinta en cada tick y cobra el Aguante (stamina) del núcleo.
    /// Empezar exige <see cref="MovementConfig.MinStaminaToSprint"/>; seguir solo exige que quede algo.
    /// </summary>
    public sealed class SprintController
    {
        private readonly ResourcePool _stamina;
        private readonly MovementConfig _config;

        public SprintController(ResourcePool stamina, MovementConfig config)
        {
            _stamina = Guard.NotNull(stamina, nameof(stamina));
            _config = Guard.NotNull(config, nameof(config));
        }

        public bool IsSprinting { get; private set; }

        public bool Update(bool wantsSprint, bool isMoving, float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            IsSprinting = wantsSprint && isMoving && CanSprint();
            if (IsSprinting) _stamina.Drain(_config.SprintStaminaPerSecond * deltaSeconds);
            return IsSprinting;
        }

        private bool CanSprint()
        {
            if (_stamina.IsEmpty) return false;
            return IsSprinting || _stamina.Current >= _config.MinStaminaToSprint;
        }
    }
}
