using Specimen.Core.Common;

namespace Specimen.Core.Abilities.Mobility
{
    /// <summary>Parámetros de los rasgos de movilidad (GDD §4). Valores provisionales, a ajustar en balance.</summary>
    public sealed class MobilityConfig
    {
        public MobilityConfig(float dashDistance = 5f, float dashStaminaCost = 20f, float dashCooldown = 1.5f,
            int extraAirJumps = 1, float glideFallMultiplier = 0.25f, float climbStaminaPerSecond = 8f,
            float teleportRange = 6f, float teleportResourceCost = 20f, float teleportCooldown = 3f)
        {
            DashDistance = Guard.Positive(dashDistance, nameof(dashDistance));
            DashStaminaCost = Guard.NonNegative(dashStaminaCost, nameof(dashStaminaCost));
            DashCooldown = Guard.NonNegative(dashCooldown, nameof(dashCooldown));
            ExtraAirJumps = Guard.NonNegative(extraAirJumps, nameof(extraAirJumps));
            GlideFallMultiplier = Guard.Positive(glideFallMultiplier, nameof(glideFallMultiplier));
            ClimbStaminaPerSecond = Guard.NonNegative(climbStaminaPerSecond, nameof(climbStaminaPerSecond));
            TeleportRange = Guard.Positive(teleportRange, nameof(teleportRange));
            TeleportResourceCost = Guard.NonNegative(teleportResourceCost, nameof(teleportResourceCost));
            TeleportCooldown = Guard.NonNegative(teleportCooldown, nameof(teleportCooldown));
        }

        public float DashDistance { get; }
        public float DashStaminaCost { get; }
        public float DashCooldown { get; }
        public int ExtraAirJumps { get; }
        public float GlideFallMultiplier { get; }
        public float ClimbStaminaPerSecond { get; }
        public float TeleportRange { get; }
        public float TeleportResourceCost { get; }
        public float TeleportCooldown { get; }
    }
}
