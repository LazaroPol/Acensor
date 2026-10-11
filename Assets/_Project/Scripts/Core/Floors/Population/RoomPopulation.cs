using System.Collections.Generic;
using Specimen.Core.Loot;

namespace Specimen.Core.Floors.Population
{
    /// <summary>Contenido de una sala: loot que se puede saquear y enemigos que la guardan.</summary>
    public sealed class RoomPopulation
    {
        public RoomPopulation(FloorRoom room, IReadOnlyList<LootDrop> loot, int enemies, int threat)
        {
            Room = room;
            Loot = loot;
            Enemies = enemies;
            Threat = threat;
        }

        public FloorRoom Room { get; }
        public IReadOnlyList<LootDrop> Loot { get; }
        public int Enemies { get; }
        /// <summary>Nivel de amenaza de los enemigos (0 si la sala no tiene).</summary>
        public int Threat { get; }
        public bool IsEmpty => Loot.Count == 0 && Enemies == 0;
    }
}
