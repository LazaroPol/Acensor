using System;
using System.Collections.Generic;
using Specimen.Core.Items.Catalog;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>Combustibles (datos): segundos que arde cada unidad (GDD §17.3).</summary>
    public sealed class FuelTable
    {
        public const float Lena = 30f;
        public const float Carbon = 90f;
        public const float Hulla = 180f;

        private readonly Dictionary<string, float> _burn = new Dictionary<string, float>();

        public FuelTable(IEnumerable<KeyValuePair<string, float>> entries)
        {
            if (entries == null) throw new ArgumentNullException(nameof(entries));
            foreach (var e in entries)
            {
                if (string.IsNullOrWhiteSpace(e.Key)) throw new ArgumentException("Id de combustible vacío.");
                if (e.Value <= 0f) throw new ArgumentOutOfRangeException(nameof(entries), e.Value, e.Key);
                _burn.Add(e.Key, e.Value);
            }
        }

        /// <summary>Catálogo actual: la madera arde como leña. Carbón y hulla llegan con sus materiales.</summary>
        public static FuelTable Default { get; } = new FuelTable(new[]
        {
            new KeyValuePair<string, float>(ItemIds.Madera, Lena)
        });

        public bool IsFuel(string itemId) => itemId != null && _burn.ContainsKey(itemId);

        /// <summary>Segundos que arde una unidad; 0 si no es combustible.</summary>
        public float BurnSeconds(string itemId) => IsFuel(itemId) ? _burn[itemId] : 0f;
    }
}
