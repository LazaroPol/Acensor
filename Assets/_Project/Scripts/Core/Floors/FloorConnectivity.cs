using System.Collections.Generic;

namespace Specimen.Core.Floors
{
    /// <summary>Consultas de alcanzabilidad sobre un trazado (BFS desde la entrada).</summary>
    public static class FloorConnectivity
    {
        public static ISet<int> ReachableFromEntrance(FloorLayout layout)
        {
            var seen = new HashSet<int> { layout.Entrance.Id };
            var queue = new Queue<int>();
            queue.Enqueue(layout.Entrance.Id);
            while (queue.Count > 0)
            {
                foreach (var next in layout.NeighborsOf(queue.Dequeue()))
                    if (seen.Add(next)) queue.Enqueue(next);
            }
            return seen;
        }

        public static bool IsFullyConnected(FloorLayout layout) =>
            ReachableFromEntrance(layout).Count == layout.Rooms.Count;
    }
}
