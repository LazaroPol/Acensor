using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Randomness;

namespace Specimen.Core.Floors.Remnants
{
    /// <summary>
    /// Siembra restos de clones anteriores y Fragmentos de memoria en las salas de saqueo de un piso. Cuanto
    /// más profundo el anillo, más probable. Nunca repite un fragmento ni siembra uno ya recogido.
    /// </summary>
    public sealed class RemnantPlacer
    {
        private const uint RemnantSalt = 0x4E3A1Cu;
        private static readonly RemnantKind[] Kinds = { RemnantKind.BrokenGear, RemnantKind.Diary, RemnantKind.Corpse };
        private readonly RemnantRules _rules;

        public RemnantPlacer(RemnantRules rules) => _rules = Guard.NotNull(rules, nameof(rules));

        /// <summary>Siembra el piso de forma reproducible a partir de la semilla de su trazado.</summary>
        public FloorRemnants Place(FloorLayout layout, MemoryFragmentLog log)
        {
            Guard.NotNull(layout, nameof(layout));
            return Place(layout, log, new Pcg32Random(layout.Seed).Fork(RemnantSalt));
        }

        public FloorRemnants Place(FloorLayout layout, MemoryFragmentLog log, IRandom random)
        {
            Guard.NotNull(layout, nameof(layout));
            Guard.NotNull(log, nameof(log));
            Guard.NotNull(random, nameof(random));
            var pool = new List<int>(log.Missing());
            var remnants = new List<Remnant>();
            var fragments = new List<FragmentPlacement>();
            foreach (var room in layout.Rooms)
            {
                if (room.Kind != RoomKind.Chamber) continue;
                var odds = _rules.For(room.Ring);
                if (random.Chance(odds.RemnantChance)) remnants.Add(new Remnant(room.Id, random.Pick(Kinds)));
                if (pool.Count > 0 && random.Chance(odds.FragmentChance))
                    fragments.Add(new FragmentPlacement(room.Id, Take(pool, random)));
            }
            return new FloorRemnants(remnants, fragments);
        }

        private static int Take(List<int> pool, IRandom random)
        {
            int index = random.Range(0, pool.Count);
            int id = pool[index];
            pool.RemoveAt(index);
            return id;
        }
    }
}
