using System;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Loot
{
    /// <summary>
    /// Fila de una tabla de loot: qué objeto, con qué peso base, desde qué anillo aparece
    /// y cuántas unidades caen (solo los apilables pueden caer en grupo).
    /// </summary>
    public sealed class LootEntry
    {
        public ItemDefinition Item { get; }
        public float BaseWeight { get; }
        public int MinRing { get; }
        public int MinQuantity { get; }
        public int MaxQuantity { get; }

        public LootEntry(ItemDefinition item, float baseWeight, int minRing = DepthRings.Min,
            int minQuantity = 1, int maxQuantity = 1)
        {
            Item = Guard.NotNull(item, nameof(item));
            BaseWeight = Guard.Positive(baseWeight, nameof(baseWeight));
            MinRing = DepthRings.Validate(minRing, nameof(minRing));
            MinQuantity = Guard.Positive(minQuantity, nameof(minQuantity));
            MaxQuantity = Guard.Positive(maxQuantity, nameof(maxQuantity));
            if (MaxQuantity < MinQuantity)
                throw new ArgumentException("La cantidad máxima no puede ser menor que la mínima.", nameof(maxQuantity));
            if (MaxQuantity > item.MaxStack)
                throw new ArgumentException("La cantidad supera el máximo de la pila.", nameof(maxQuantity));
        }

        public bool AvailableIn(int ring) => ring >= MinRing;
    }
}
