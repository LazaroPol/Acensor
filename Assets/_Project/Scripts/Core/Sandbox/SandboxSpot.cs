namespace Specimen.Core.Sandbox
{
    /// <summary>Punto del Campo de pruebas: qué va (id), dónde (m) y hacia dónde mira (grados, 0 = +Z).</summary>
    public readonly struct SandboxSpot
    {
        public SandboxSpot(string id, float x, float z, float yaw = 0f)
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
    }
}
