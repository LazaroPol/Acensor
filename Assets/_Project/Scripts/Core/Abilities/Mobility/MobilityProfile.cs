using Specimen.Core.Classes;
using Specimen.Core.Common;

namespace Specimen.Core.Abilities.Mobility
{
    /// <summary>Capacidades de movilidad que concede un kit según sus rasgos de exploración.</summary>
    public sealed class MobilityProfile
    {
        private readonly MobilityConfig _config;

        public MobilityProfile(ExplorationTraits traits, MobilityConfig config)
        {
            Traits = traits;
            _config = Guard.NotNull(config, nameof(config));
        }

        public static MobilityProfile For(ClassKit kit, MobilityConfig config)
            => new MobilityProfile(Guard.NotNull(kit, nameof(kit)).Traits, config);

        public ExplorationTraits Traits { get; }
        public bool CanDash => Has(ExplorationTraits.Dash);
        public bool CanClimb => Has(ExplorationTraits.Climb);
        public bool CanGlide => Has(ExplorationTraits.Glide);
        public bool CanTeleport => Has(ExplorationTraits.ShortTeleport);
        public int AirJumps => Has(ExplorationTraits.DoubleJump) ? _config.ExtraAirJumps : 0;
        public float ClimbStaminaPerSecond => _config.ClimbStaminaPerSecond;

        /// <summary>Velocidad de caída resultante: se reduce solo si el kit planea y el jugador lo pide.</summary>
        public float FallSpeed(float baseFallSpeed, bool wantsGlide)
        {
            Guard.NonNegative(baseFallSpeed, nameof(baseFallSpeed));
            return CanGlide && wantsGlide ? baseFallSpeed * _config.GlideFallMultiplier : baseFallSpeed;
        }

        /// <summary>Habilidad de dash (coste de stamina), o null si el kit no la tiene.</summary>
        public AbilityDefinition DashAbility() => CanDash
            ? new AbilityDefinition("movilidad.dash", "Dash", 0f, _config.DashStaminaCost, _config.DashCooldown)
            : null;

        /// <summary>Habilidad de teletransporte corto (coste de recurso de clase), o null si el kit no la tiene.</summary>
        public AbilityDefinition TeleportAbility() => CanTeleport
            ? new AbilityDefinition("movilidad.teleport", "Teletransporte", _config.TeleportResourceCost, 0f,
                _config.TeleportCooldown)
            : null;

        public float DashDistance => _config.DashDistance;
        public float TeleportRange => _config.TeleportRange;

        private bool Has(ExplorationTraits trait) => (Traits & trait) == trait;
    }
}
