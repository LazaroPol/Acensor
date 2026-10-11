using System;
using Specimen.Core.Common;

namespace Specimen.Core.Movement
{
    /// <summary>
    /// Cámara en tercera persona cenital-oblicua (GDD: estilo Risk of Rain 2 / V Rising): se coloca a
    /// <see cref="Distance"/> del objetivo, inclinada <see cref="PitchDegrees"/> y mirando hacia
    /// <see cref="YawDegrees"/>; lo sigue con suavizado exponencial independiente del framerate.
    /// </summary>
    public sealed class ThirdPersonRig
    {
        public ThirdPersonRig(float distance = 12f, float pitchDegrees = 55f, float yawDegrees = 0f,
            float followSharpness = 10f)
        {
            Distance = Guard.Positive(distance, nameof(distance));
            if (pitchDegrees <= 0f || pitchDegrees > 90f)
                throw new ArgumentOutOfRangeException(nameof(pitchDegrees), "La inclinación va de (0, 90] grados.");
            PitchDegrees = pitchDegrees;
            YawDegrees = yawDegrees;
            FollowSharpness = Guard.NonNegative(followSharpness, nameof(followSharpness));
        }

        public float Distance { get; }
        public float PitchDegrees { get; }
        public float YawDegrees { get; }
        public float FollowSharpness { get; }

        /// <summary>Posición de la cámara relativa al objetivo.</summary>
        public CameraPoint Offset()
        {
            double pitch = PitchDegrees * Math.PI / 180.0;
            float horizontal = (float)(Distance * Math.Cos(pitch));
            var back = PlanarVector.FromYaw(YawDegrees).Scale(-horizontal);
            return new CameraPoint(back.X, (float)(Distance * Math.Sin(pitch)), back.Z);
        }

        public CameraPoint Desired(CameraPoint target) => target.Plus(Offset());

        /// <summary>Avanza desde <paramref name="current"/> hacia la posición deseada; sin suavizado salta.</summary>
        public CameraPoint Follow(CameraPoint current, CameraPoint target, float deltaSeconds)
        {
            Guard.NonNegative(deltaSeconds, nameof(deltaSeconds));
            var desired = Desired(target);
            if (FollowSharpness <= 0f) return desired;
            float t = 1f - (float)Math.Exp(-FollowSharpness * deltaSeconds);
            return current.Lerp(desired, t);
        }
    }
}
