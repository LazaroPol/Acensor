using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Randomness;

namespace Specimen.Core.Floors
{
    /// <summary>
    /// Genera pasillos por anillos de profundidad: cada sala cuelga de una del anillo anterior
    /// (conectividad garantizada), con pasillos laterales y de retorno opcionales.
    /// El Hueco de Emergencia se coloca en el anillo más profundo.
    /// </summary>
    public sealed class RingCorridorGenerator : IFloorGenerator
    {
        private readonly RingLayoutConfig _config;
        private readonly Func<ulong, IRandom> _randomFactory;

        public RingCorridorGenerator(RingLayoutConfig config, Func<ulong, IRandom> randomFactory = null)
        {
            _config = Guard.NotNull(config, nameof(config));
            _randomFactory = randomFactory ?? (s => new Pcg32Random(s));
        }

        public FloorLayout Generate(ulong seed)
        {
            var rng = _randomFactory(seed);
            var rings = BuildRings();
            var corridors = new CorridorSet();
            for (int r = 1; r < rings.Count; r++)
                LinkRing(rng, rings[r - 1], rings[r], corridors);
            return new FloorLayout(seed, BuildRooms(rng, rings), corridors.Items);
        }

        private List<List<int>> BuildRings()
        {
            var rings = new List<List<int>> { new List<int> { 0 } };
            int next = 1;
            foreach (var count in _config.RoomsPerRing)
            {
                var ring = new List<int>(count);
                for (int i = 0; i < count; i++) ring.Add(next++);
                rings.Add(ring);
            }
            return rings;
        }

        private void LinkRing(IRandom rng, List<int> previous, List<int> current, CorridorSet corridors)
        {
            foreach (var room in current)
            {
                var parent = rng.Pick(previous);
                corridors.Add(room, parent);
                if (previous.Count > 1 && rng.Chance(_config.BackLinkChance))
                {
                    var other = rng.Pick(previous);
                    if (other != parent) corridors.Add(room, other);
                }
            }
            for (int i = 0; i + 1 < current.Count; i++)
                if (rng.Chance(_config.LateralChance)) corridors.Add(current[i], current[i + 1]);
        }

        private static List<FloorRoom> BuildRooms(IRandom rng, List<List<int>> rings)
        {
            var deepest = rings[rings.Count - 1];
            var exit = rng.Pick(deepest);
            var rooms = new List<FloorRoom> { new FloorRoom(0, 0, RoomKind.Entrance) };
            for (int r = 1; r < rings.Count; r++)
                foreach (var id in rings[r])
                    rooms.Add(new FloorRoom(id, r, id == exit ? RoomKind.EmergencyExit : RoomKind.Chamber));
            return rooms;
        }

        private sealed class CorridorSet
        {
            private readonly HashSet<Corridor> _seen = new HashSet<Corridor>();
            public List<Corridor> Items { get; } = new List<Corridor>();

            public void Add(int a, int b)
            {
                var c = new Corridor(a, b);
                if (_seen.Add(c)) Items.Add(c);
            }
        }
    }
}
