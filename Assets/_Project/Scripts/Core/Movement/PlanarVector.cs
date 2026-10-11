using System;

namespace Specimen.Core.Movement
{
    /// <summary>Vector en el plano del suelo (X, Z): dirección de entrada o velocidad en m/s.</summary>
    public readonly struct PlanarVector
    {
        public static readonly PlanarVector Zero = new PlanarVector(0f, 0f);

        public PlanarVector(float x, float z)
        {
            X = x;
            Z = z;
        }

        public float X { get; }
        public float Z { get; }
        public float Magnitude => (float)Math.Sqrt(X * X + Z * Z);

        /// <summary>Giro en Y (grados, convenio de Unity: 0 → +Z, 90 → +X) hacia el que apunta.</summary>
        public float YawDegrees => (float)(Math.Atan2(X, Z) * 180.0 / Math.PI);

        public PlanarVector Scale(float factor) => new PlanarVector(X * factor, Z * factor);

        public PlanarVector ClampMagnitude(float max)
        {
            float magnitude = Magnitude;
            return magnitude > max && magnitude > 0f ? Scale(max / magnitude) : this;
        }

        /// <summary>Gira el vector alrededor de Y como lo haría <c>Quaternion.Euler(0, yaw, 0)</c>.</summary>
        public PlanarVector RotateByYaw(float yawDegrees)
        {
            double radians = yawDegrees * Math.PI / 180.0;
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            return new PlanarVector(X * cos + Z * sin, -X * sin + Z * cos);
        }

        public static PlanarVector FromYaw(float yawDegrees) => new PlanarVector(0f, 1f).RotateByYaw(yawDegrees);
    }
}
