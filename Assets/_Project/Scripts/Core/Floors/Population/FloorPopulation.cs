using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Floors.Population
{
    /// <summary>Población completa de un piso, alineada con las salas del trazado (índice = id de sala).</summary>
    public sealed class FloorPopulation
    {
        public FloorPopulation(IReadOnlyList<RoomPopulation> rooms) => Rooms = rooms;

        public IReadOnlyList<RoomPopulation> Rooms { get; }
        public RoomPopulation ForRoom(int roomId) => Rooms[roomId];
        public int TotalEnemies => Rooms.Sum(r => r.Enemies);
        public IEnumerable<RoomPopulation> InRing(int ring) => Rooms.Where(r => r.Room.Ring == ring);
    }
}
