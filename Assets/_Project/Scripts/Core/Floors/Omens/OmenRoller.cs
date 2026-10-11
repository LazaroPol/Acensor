using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Floors.Types;
using Specimen.Core.Randomness;

namespace Specimen.Core.Floors.Omens
{
    /// <summary>Tira el Presagio de un piso de forma reproducible a partir de la semilla de su plan.</summary>
    public sealed class OmenRoller
    {
        private const uint OmenSalt = 0x0A11E0u;
        private readonly FloorModifierCatalog _catalog;
        private readonly int _maxModifiers;

        public OmenRoller(FloorModifierCatalog catalog, int maxModifiers = 2)
        {
            _catalog = Guard.NotNull(catalog, nameof(catalog));
            if (maxModifiers < 0) throw new ArgumentOutOfRangeException(nameof(maxModifiers));
            _maxModifiers = maxModifiers;
        }

        public Omen Roll(FloorPlan plan)
        {
            Guard.NotNull(plan, nameof(plan));
            return Roll(plan.Type, new Pcg32Random(plan.Seed).Fork(OmenSalt));
        }

        public Omen Roll(IFloorType type, IRandom random)
        {
            Guard.NotNull(random, nameof(random));
            var pool = new List<FloorModifier>(_catalog.CompatibleWith(type));
            var count = random.Range(0, Math.Min(_maxModifiers, pool.Count) + 1);
            var chosen = new List<FloorModifier>(count);
            for (var i = 0; i < count; i++)
            {
                var index = random.Range(0, pool.Count);
                chosen.Add(pool[index]);
                pool.RemoveAt(index);
            }
            return new Omen(type, chosen);
        }
    }
}
