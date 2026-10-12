using System;
using Specimen.Core.Movement;

namespace Specimen.Core.Combat.Attacks
{
    /// <summary>Geometría del golpe cuerpo a cuerpo: un sector circular delante del atacante.</summary>
    public static class MeleeArc
    {
        /// <param name="facingYaw">Grados; 0 = +Z, 90 = +X (convención de PlanarVector).</param>
        /// <param name="targetRadius">Radio del objetivo: amplía el alcance para no exigir tocar su centro.</param>
        public static bool Contains(PlanarVector origin, float facingYaw, PlanarVector target, float range,
            float arcDegrees, float targetRadius = 0f)
        {
            var offset = new PlanarVector(target.X - origin.X, target.Z - origin.Z);
            float distance = offset.Magnitude;
            if (distance > range + targetRadius) return false;
            if (distance < 1e-4f) return true;
            return Math.Abs(DeltaAngle(facingYaw, offset.YawDegrees)) <= arcDegrees / 2f;
        }

        public static float DeltaAngle(float from, float to)
        {
            float delta = (to - from) % 360f;
            if (delta > 180f) delta -= 360f;
            if (delta < -180f) delta += 360f;
            return delta;
        }
    }
}
