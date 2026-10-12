namespace Specimen.Core.World
{
    /// <summary>Algo colocado en el mundo: qué es (id), dónde (m, plano XZ) y hacia dónde mira (grados, 0 = +Z).</summary>
    public readonly struct WorldSpot
    {
        public WorldSpot(string id, float x, float z, float yaw = 0f)
        {
            Id = id;
            X = x;
            Z = z;
            Yaw = yaw;
        }

        public string Id { get; }
        public float X { get; }
        public float Z { get; }
        public float Yaw { get; }

        public float DistanceTo(float x, float z)
        {
            float dx = X - x, dz = Z - z;
            return (float)System.Math.Sqrt(dx * dx + dz * dz);
        }
    }
}
