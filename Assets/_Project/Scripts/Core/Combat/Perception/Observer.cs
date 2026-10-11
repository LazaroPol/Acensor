using System;
using Specimen.Core.Movement;

namespace Specimen.Core.Combat.Perception
{
    /// <summary>Posición y orientación (yaw en grados, convenio de Unity) del enemigo que percibe.</summary>
    public readonly struct Observer
    {
        public Observer(PlanarVector position, float facingYaw)
        {
            Position = position;
            FacingYaw = facingYaw;
        }

        public PlanarVector Position { get; }
        public float FacingYaw { get; }

        /// <summary>Vector desde el observador hasta el punto dado.</summary>
        public PlanarVector ToPoint(PlanarVector point) => new PlanarVector(point.X - Position.X, point.Z - Position.Z);

        /// <summary>Ángulo (grados, 0-180) entre la mirada y el punto; 0 si coinciden en posición.</summary>
        public float AngleTo(PlanarVector point)
        {
            PlanarVector offset = ToPoint(point);
            float distance = offset.Magnitude;
            if (distance <= 0f) return 0f;
            PlanarVector forward = PlanarVector.FromYaw(FacingYaw);
            double cos = (forward.X * offset.X + forward.Z * offset.Z) / distance;
            cos = Math.Max(-1.0, Math.Min(1.0, cos));
            return (float)(Math.Acos(cos) * 180.0 / Math.PI);
        }
    }
}
