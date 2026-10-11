using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Professions;
using Specimen.Core.Randomness;

namespace Specimen.Core.Floors.Paths
{
    /// <summary>
    /// Sella algunos pasillos del piso como caminos alternativos. Solo sella pasillos redundantes, así que el
    /// piso sigue entero alcanzable sin ninguna profesión: ninguna es obligatoria (GDD §7).
    /// </summary>
    public sealed class AlternativePathPlanner
    {
        private const uint PathsSalt = 0xA17E5u;
        private static readonly ProfessionId[] NoProfessions = new ProfessionId[0];

        public AlternativePathPlanner(int maxSealedDoors)
        {
            if (maxSealedDoors < 0) throw new ArgumentOutOfRangeException(nameof(maxSealedDoors));
            MaxSealedDoors = maxSealedDoors;
        }

        public int MaxSealedDoors { get; }

        /// <summary>Planifica de forma reproducible a partir de la semilla del trazado.</summary>
        public FloorPaths Plan(FloorLayout layout)
        {
            Guard.NotNull(layout, nameof(layout));
            return Plan(layout, new Pcg32Random(layout.Seed).Fork(PathsSalt));
        }

        public FloorPaths Plan(FloorLayout layout, IRandom random)
        {
            Guard.NotNull(layout, nameof(layout));
            Guard.NotNull(random, nameof(random));
            var candidates = new List<Corridor>(layout.Corridors);
            var sealedCorridors = new List<Corridor>();
            while (candidates.Count > 0 && sealedCorridors.Count < MaxSealedDoors)
            {
                int i = random.Range(0, candidates.Count);
                var corridor = candidates[i];
                candidates.RemoveAt(i);
                sealedCorridors.Add(corridor);
                if (!KeepsFloorReachable(layout, sealedCorridors)) sealedCorridors.RemoveAt(sealedCorridors.Count - 1);
            }
            return new FloorPaths(sealedCorridors);
        }

        private static bool KeepsFloorReachable(FloorLayout layout, IReadOnlyList<Corridor> sealedCorridors) =>
            new FloorPaths(sealedCorridors).ReachableBy(layout, NoProfessions).Count == layout.Rooms.Count;
    }
}
