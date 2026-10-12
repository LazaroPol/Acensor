using Specimen.Core.Common;
using Specimen.Core.Vitals;

namespace Specimen.Core.Movement
{
    /// <summary>
    /// Esquiva: al iniciarla gasta Aguante y fija la dirección; durante <see cref="DodgeConfig.DurationSeconds"/>
    /// devuelve la velocidad del impulso y después espera el enfriamiento. No arranca sin Aguante suficiente.
    /// </summary>
    public sealed class DodgeController
    {
        private readonly DodgeConfig _config;
        private readonly ResourcePool _stamina;
        private PlanarVector _direction;
        private float _remaining;
        private float _cooldown;

        public DodgeController(DodgeConfig config, ResourcePool stamina)
        {
            _config = Guard.NotNull(config, nameof(config));
            _stamina = Guard.NotNull(stamina, nameof(stamina));
        }

        public bool IsDodging => _remaining > 0f;
        public bool IsReady => !IsDodging && _cooldown <= 0f;

        /// <summary>Inicia la esquiva hacia <paramref name="direction"/> (sin dirección: falla sin gastar).</summary>
        public bool TryStart(PlanarVector direction)
        {
            if (!IsReady || direction.Magnitude <= MovementConfig.DeadZone) return false;
            if (!_stamina.TrySpend(_config.StaminaCost)) return false;
            _direction = direction.Scale(1f / direction.Magnitude);
            _remaining = _config.DurationSeconds;
            return true;
        }

        /// <summary>Avanza el tiempo y devuelve la velocidad del impulso (cero si no se esquiva).</summary>
        public PlanarVector Step(float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            if (!IsDodging)
            {
                _cooldown = System.Math.Max(0f, _cooldown - deltaSeconds);
                return PlanarVector.Zero;
            }
            _remaining -= deltaSeconds;
            if (_remaining <= 0f)
            {
                _remaining = 0f;
                _cooldown = _config.CooldownSeconds;
            }
            return _direction.Scale(_config.Speed);
        }
    }
}
