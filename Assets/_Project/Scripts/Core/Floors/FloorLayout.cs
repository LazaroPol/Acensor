using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Floors
{
    /// <summary>Trazado inmutable de un piso: salas, pasillos y semilla que lo generó.</summary>
    public sealed class FloorLayout
    {
        private readonly List<int>[] _neighbors;

        public FloorLayout(ulong seed, IReadOnlyList<FloorRoom> rooms, IReadOnlyList<Corridor> corridors)
        {
            Seed = seed;
            Rooms = rooms;
            Corridors = corridors;
            _neighbors = new List<int>[rooms.Count];
            for (int i = 0; i < rooms.Count; i++) _neighbors[i] = new List<int>();
            foreach (var c in corridors)
            {
                _neighbors[c.A].Add(c.B);
                _neighbors[c.B].Add(c.A);
            }
        }

        public ulong Seed { get; }
        public IReadOnlyList<FloorRoom> Rooms { get; }
        public IReadOnlyList<Corridor> Corridors { get; }

        public FloorRoom Entrance => Rooms[0];
        public int DeepestRing => Rooms.Max(r => r.Ring);

        public IReadOnlyList<int> NeighborsOf(int roomId) => _neighbors[roomId];

        public IEnumerable<FloorRoom> RoomsInRing(int ring) => Rooms.Where(r => r.Ring == ring);
    }
}
