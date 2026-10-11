using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Professions;

namespace Specimen.Core.Floors.Paths
{
    /// <summary>Pasillos sellados de un piso: atajos que solo cruza un grupo con la profesión adecuada.</summary>
    public sealed class FloorPaths
    {
        private readonly HashSet<Corridor> _sealed;

        public FloorPaths(IReadOnlyList<Corridor> sealedCorridors)
        {
            SealedCorridors = Guard.NotNull(sealedCorridors, nameof(sealedCorridors));
            _sealed = new HashSet<Corridor>(sealedCorridors);
        }

        public static FloorPaths None { get; } = new FloorPaths(new Corridor[0]);

        public IReadOnlyList<Corridor> SealedCorridors { get; }

        public bool IsSealed(Corridor corridor) => _sealed.Contains(corridor);

        /// <summary>Salas alcanzables desde la entrada por un grupo con esas profesiones.</summary>
        public ISet<int> ReachableBy(FloorLayout layout, IEnumerable<ProfessionId> party)
        {
            Guard.NotNull(layout, nameof(layout));
            bool canOpen = SealedDoorRules.CanOpen(party);
            var seen = new HashSet<int> { layout.Entrance.Id };
            var queue = new Queue<int>();
            queue.Enqueue(layout.Entrance.Id);
            while (queue.Count > 0)
            {
                int room = queue.Dequeue();
                foreach (var next in layout.NeighborsOf(room))
                {
                    if (!canOpen && IsSealed(new Corridor(room, next))) continue;
                    if (seen.Add(next)) queue.Enqueue(next);
                }
            }
            return seen;
        }
    }
}
