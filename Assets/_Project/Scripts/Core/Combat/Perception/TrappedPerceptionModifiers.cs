using System;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Core.Combat.Perception
{
    /// <summary>Aplica los modificadores de Atrapado (GDD §3.2) a los objetivos atrapados; neutros para el resto.</summary>
    public sealed class TrappedPerceptionModifiers : IPerceptionModifiers
    {
        public static readonly TrappedPerceptionModifiers Instance = new TrappedPerceptionModifiers();

        public float VisionMultiplier(PerceivedTarget target) =>
            Checked(target).IsTrapped ? TrappedModifiers.EnemyVisionMultiplier : 1f;

        public float SpeedMultiplier(PerceivedTarget target) =>
            Checked(target).IsTrapped ? TrappedModifiers.EnemySpeedMultiplier : 1f;

        private static PerceivedTarget Checked(PerceivedTarget target) =>
            target ?? throw new ArgumentNullException(nameof(target));
    }
}
