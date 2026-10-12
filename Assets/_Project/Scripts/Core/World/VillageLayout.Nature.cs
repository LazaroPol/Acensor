using System.Collections.Generic;
using Specimen.Core.Gathering;
using Specimen.Core.Randomness;

namespace Specimen.Core.World
{
    /// <summary>Árboles, hierbas, rocas y vetas colocados de forma determinista (semilla fija) lejos de caminos y casas.</summary>
    public sealed partial class VillageLayout
    {
        public const ulong Seed = 20261012UL;
        public const float MinSpacing = 2.6f;

        private List<(GatherKind kind, WorldSpot spot)> _nodes;

        public IReadOnlyList<(GatherKind kind, WorldSpot spot)> Nodes => _nodes ??= BuildNodes();

        private List<(GatherKind, WorldSpot)> BuildNodes()
        {
            var random = new Pcg32Random(Seed);
            var result = new List<(GatherKind, WorldSpot)>();
            Scatter(result, random, GatherKind.Arbol, 34, -40f, -9f, -35f, 3f);
            Scatter(result, random, GatherKind.Hierba, 12, -38f, -10f, -32f, 0f);
            Scatter(result, random, GatherKind.Roca, 8, 1f, 23f, -56f, -38f);
            Scatter(result, random, GatherKind.VetaHierro, 6, 1f, 23f, -56f, -38f);
            Scatter(result, random, GatherKind.Arbol, 10, 26f, 56f, -2f, 18f);
            return result;
        }

        private void Scatter(List<(GatherKind, WorldSpot)> nodes, IRandom random, GatherKind kind, int count,
            float minX, float maxX, float minZ, float maxZ)
        {
            int placed = 0;
            for (int attempt = 0; attempt < count * 40 && placed < count; attempt++)
            {
                float x = random.Range(minX, maxX), z = random.Range(minZ, maxZ);
                if (!IsFree(nodes, x, z)) continue;
                nodes.Add((kind, new WorldSpot(kind.ToString(), x, z, random.Range(0f, 360f))));
                placed++;
            }
        }

        private bool IsFree(List<(GatherKind, WorldSpot)> nodes, float x, float z)
        {
            if (IsOnPath(x, z, 1.5f)) return false;
            foreach (var house in Houses) if (new WorldSpot("", house.X, house.Z).DistanceTo(x, z) < house.Radius + 2f) return false;
            foreach (var camp in Camps) if (camp.DistanceTo(x, z) < 3f) return false;
            foreach (var (_, spot) in nodes) if (spot.DistanceTo(x, z) < MinSpacing) return false;
            return true;
        }
    }
}
