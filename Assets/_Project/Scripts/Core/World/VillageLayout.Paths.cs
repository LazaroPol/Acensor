using System.Collections.Generic;

namespace Specimen.Core.World
{
    /// <summary>Caminos empedrados (baldosas de 2 m): Armería → plaza y de la plaza al bosque, la cantera y el campamento.</summary>
    public sealed partial class VillageLayout
    {
        public const float Tile = 2f;

        private List<WorldSpot> _paths;

        public IReadOnlyList<WorldSpot> Paths => _paths ??= BuildPaths();

        public bool IsOnPath(float x, float z, float margin)
        {
            foreach (var tile in Paths)
                if (System.Math.Abs(tile.X - x) <= Tile / 2f + margin && System.Math.Abs(tile.Z - z) <= Tile / 2f + margin)
                    return true;
            return false;
        }

        private static List<WorldSpot> BuildPaths()
        {
            var tiles = new List<WorldSpot>();
            var seen = new HashSet<(int, int)>();
            void Add(float x, float z)
            {
                if (seen.Add(((int)System.Math.Round(x * 10), (int)System.Math.Round(z * 10))))
                    tiles.Add(new WorldSpot("camino", x, z));
            }
            for (float z = -1f; z >= PlazaZ; z -= Tile) Add(GateX, z);
            for (float x = PlazaX - 4f; x <= PlazaX + 4f; x += Tile)
                for (float z = PlazaZ - 4f; z <= PlazaZ + 4f; z += Tile) Add(x, z);
            for (float x = PlazaX + 6f; x <= 34f; x += Tile) Add(x, PlazaZ);
            for (float x = PlazaX - 6f; x >= -10f; x -= Tile) Add(x, PlazaZ);
            for (float z = PlazaZ - 6f; z >= -36f; z -= Tile) Add(GateX, z);
            return tiles;
        }
    }
}
