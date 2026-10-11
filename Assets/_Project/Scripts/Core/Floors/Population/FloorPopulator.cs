using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;

namespace Specimen.Core.Floors.Population
{
    /// <summary>
    /// Reparte loot y enemigos por las salas de un piso según su anillo: más lejos = mejor loot y peores
    /// enemigos. La entrada queda vacía y el Hueco de Emergencia solo tiene guardianes.
    /// </summary>
    public sealed class FloorPopulator
    {
        private const uint PopulationSalt = 0x9091A7u;
        private readonly LootTable _loot;
        private readonly RingPopulationRules _rules;

        public FloorPopulator(LootTable loot, RingPopulationRules rules)
        {
            _loot = Guard.NotNull(loot, nameof(loot));
            _rules = Guard.NotNull(rules, nameof(rules));
        }

        /// <summary>Puebla el piso de forma reproducible a partir de la semilla de su trazado.</summary>
        public FloorPopulation Populate(FloorLayout layout, bool hasCombat = true)
        {
            Guard.NotNull(layout, nameof(layout));
            return Populate(layout, hasCombat, new Pcg32Random(layout.Seed).Fork(PopulationSalt));
        }

        public FloorPopulation Populate(FloorLayout layout, bool hasCombat, IRandom random)
        {
            Guard.NotNull(layout, nameof(layout));
            Guard.NotNull(random, nameof(random));
            var rooms = new List<RoomPopulation>(layout.Rooms.Count);
            foreach (var room in layout.Rooms) rooms.Add(PopulateRoom(room, hasCombat, random));
            return new FloorPopulation(rooms);
        }

        private RoomPopulation PopulateRoom(FloorRoom room, bool hasCombat, IRandom random)
        {
            if (room.Kind == RoomKind.Entrance)
                return new RoomPopulation(room, Array.Empty<LootDrop>(), 0, 0);
            var quota = _rules.For(room.Ring);
            var loot = room.Kind == RoomKind.Chamber && _loot.HasLootIn(room.Ring)
                ? _loot.Roll(room.Ring, random.Range(quota.MinLoot, quota.MaxLoot + 1), random)
                : Array.Empty<LootDrop>();
            int enemies = hasCombat ? random.Range(quota.MinEnemies, quota.MaxEnemies + 1) : 0;
            return new RoomPopulation(room, loot, enemies, enemies > 0 ? quota.Threat : 0);
        }
    }
}
