namespace Specimen.Core.Movement
{
    /// <summary>Punto 3D en metros (convenio de Unity: Y arriba).</summary>
    public readonly struct CameraPoint
    {
        public CameraPoint(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public CameraPoint Plus(CameraPoint other) => new CameraPoint(X + other.X, Y + other.Y, Z + other.Z);

        public CameraPoint Lerp(CameraPoint to, float t) =>
            new CameraPoint(X + (to.X - X) * t, Y + (to.Y - Y) * t, Z + (to.Z - Z) * t);
    }
}
