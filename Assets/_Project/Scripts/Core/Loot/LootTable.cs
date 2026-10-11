using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Randomness;

namespace Specimen.Core.Loot
{
    /// <summary>
    /// Tabla de loot ponderada por anillo de profundidad. El peso efectivo de cada fila es
    /// <c>peso base × multiplicador(rareza, anillo)</c>; las filas cuyo anillo mínimo no se alcanza no cuentan.
    /// </summary>
    public sealed class LootTable
    {
        private readonly List<LootEntry> _entries;
        private readonly IRingWeighting _weighting;

        public IReadOnlyList<LootEntry> Entries => _entries;

        public LootTable(IEnumerable<LootEntry> entries, IRingWeighting weighting)
        {
            _entries = new List<LootEntry>(Guard.NotNull(entries, nameof(entries)));
            if (_entries.Count == 0) throw new ArgumentException("La tabla necesita al menos una fila.", nameof(entries));
            if (_entries.Contains(null)) throw new ArgumentException("La tabla contiene filas nulas.", nameof(entries));
            if (!HasLootIn(DepthRings.Max))
                throw new ArgumentException("Ninguna fila está disponible en ningún anillo.", nameof(entries));
            _weighting = Guard.NotNull(weighting, nameof(weighting));
        }

        public bool HasLootIn(int ring)
        {
            DepthRings.Validate(ring, nameof(ring));
            return _entries.Exists(e => e.AvailableIn(ring));
        }

        /// <summary>Pesos efectivos en el anillo, alineados con <see cref="Entries"/> (0 = no disponible).</summary>
        public IReadOnlyList<float> WeightsFor(int ring)
        {
            DepthRings.Validate(ring, nameof(ring));
            var weights = new float[_entries.Count];
            for (int i = 0; i < _entries.Count; i++)
            {
                var entry = _entries[i];
                weights[i] = entry.AvailableIn(ring)
                    ? entry.BaseWeight * Guard.NonNegative(_weighting.Multiplier(entry.Item.Rarity, ring), "multiplier")
                    : 0f;
            }
            return weights;
        }

        /// <summary>Probabilidad (0-1) de que salga la fila <paramref name="index"/> en el anillo.</summary>
        public float ChanceOf(int index, int ring)
        {
            var weights = WeightsFor(ring);
            float total = 0f;
            foreach (var w in weights) total += w;
            return total <= 0f ? 0f : weights[index] / total;
        }

        /// <summary>Una tirada en el anillo dado.</summary>
        public LootDrop Roll(int ring, IRandom random)
        {
            Guard.NotNull(random, nameof(random));
            if (!HasLootIn(ring)) throw new InvalidOperationException($"No hay loot disponible en el anillo {ring}.");
            var entry = _entries[random.WeightedIndex(WeightsFor(ring))];
            int quantity = random.Range(entry.MinQuantity, entry.MaxQuantity + 1);
            return new LootDrop(entry.Item, quantity);
        }

        /// <summary>Varias tiradas independientes (p. ej. un cofre con N premios).</summary>
        public IReadOnlyList<LootDrop> Roll(int ring, int count, IRandom random)
        {
            Guard.NonNegative(count, nameof(count));
            var drops = new List<LootDrop>(count);
            for (int i = 0; i < count; i++) drops.Add(Roll(ring, random));
            return drops;
        }
    }
}
