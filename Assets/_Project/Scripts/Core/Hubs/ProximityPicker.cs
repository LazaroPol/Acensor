using System.Collections.Generic;

namespace Specimen.Core.Hubs
{
    /// <summary>Elige el punto de interés más cercano al jugador dentro de un alcance (-1 si no hay ninguno).</summary>
    public static class ProximityPicker
    {
        public static int Nearest(IReadOnlyList<(float X, float Z)> points, float x, float z, float range)
        {
            int best = -1;
            float bestSq = range * range;
            for (int i = 0; i < points.Count; i++)
            {
                float dx = points[i].X - x, dz = points[i].Z - z;
                float sq = dx * dx + dz * dz;
                if (sq > bestSq) continue;
                bestSq = sq;
                best = i;
            }
            return best;
        }
    }
}
