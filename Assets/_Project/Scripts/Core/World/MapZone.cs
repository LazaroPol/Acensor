namespace Specimen.Core.World
{
    /// <summary>Zona con nombre del mapa (rectángulo en el plano XZ) y lo que se hace allí.</summary>
    public sealed class MapZone
    {
        public MapZone(string name, string hint, float minX, float minZ, float maxX, float maxZ)
        {
            Name = name;
            Hint = hint;
            MinX = minX;
            MinZ = minZ;
            MaxX = maxX;
            MaxZ = maxZ;
        }

        public string Name { get; }
        public string Hint { get; }
        public float MinX { get; }
        public float MinZ { get; }
        public float MaxX { get; }
        public float MaxZ { get; }
        public float CenterX => (MinX + MaxX) / 2f;
        public float CenterZ => (MinZ + MaxZ) / 2f;

        public bool Contains(float x, float z) => x >= MinX && x <= MaxX && z >= MinZ && z <= MaxZ;
    }
}
