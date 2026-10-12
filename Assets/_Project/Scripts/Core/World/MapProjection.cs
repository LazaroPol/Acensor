namespace Specimen.Core.World
{
    /// <summary>Convierte coordenadas del poblado (m) a coordenadas normalizadas 0–1 del mapa (0,0 = esquina inferior izquierda).</summary>
    public static class MapProjection
    {
        public const float Width = VillageLayout.MaxX - VillageLayout.MinX;
        public const float Depth = VillageLayout.MaxZ - VillageLayout.MinZ;

        public static float U(float x) => Clamp01((x - VillageLayout.MinX) / Width);

        public static float V(float z) => Clamp01((z - VillageLayout.MinZ) / Depth);

        private static float Clamp01(float value) => value < 0f ? 0f : value > 1f ? 1f : value;
    }
}
