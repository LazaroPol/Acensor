using System;

namespace Specimen.Core.Combat.Perception
{
    /// <summary>Sentidos base de un enemigo: alcance y semiángulo de visión (grados), alcance auditivo y velocidad de persecución.</summary>
    public sealed class PerceptionConfig
    {
        public PerceptionConfig(float visionRange, float visionHalfAngle, float hearingRange, float chaseSpeed)
        {
            if (visionRange < 0f) throw new ArgumentOutOfRangeException(nameof(visionRange), visionRange, "Debe ser >= 0.");
            if (visionHalfAngle < 0f || visionHalfAngle > 180f)
                throw new ArgumentOutOfRangeException(nameof(visionHalfAngle), visionHalfAngle, "Debe estar en [0, 180].");
            if (hearingRange < 0f) throw new ArgumentOutOfRangeException(nameof(hearingRange), hearingRange, "Debe ser >= 0.");
            if (chaseSpeed < 0f) throw new ArgumentOutOfRangeException(nameof(chaseSpeed), chaseSpeed, "Debe ser >= 0.");
            VisionRange = visionRange;
            VisionHalfAngle = visionHalfAngle;
            HearingRange = hearingRange;
            ChaseSpeed = chaseSpeed;
        }

        public float VisionRange { get; }
        public float VisionHalfAngle { get; }
        public float HearingRange { get; }
        public float ChaseSpeed { get; }
    }
}
