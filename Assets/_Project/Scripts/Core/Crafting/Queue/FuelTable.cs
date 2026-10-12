using System;
using System.Collections.Generic;
using Specimen.Core.Crafting.Definitions;

namespace Specimen.Core.Crafting.Queue
{
    /// <summary>Combustibles (datos): segundos que arde cada unidad y, si procede, única estación donde arde (GDD §17.5).</summary>
    public sealed partial class FuelTable
    {
        public const float Lena = 30f;
        public const float Carbon = 90f;
        public const float Hulla = 180f;
        public const float AceiteLampara = 60f;
        /// <summary>Cualquier madera tier 1 que no sea leña también arde, la mitad.</summary>
        public const float MaderaTier1 = 15f;

        private readonly Dictionary<string, float> _burn = new Dictionary<string, float>();
        private readonly Dictionary<string, StationType> _onlyIn = new Dictionary<string, StationType>();

        public FuelTable(IEnumerable<KeyValuePair<string, float>> entries,
            IEnumerable<KeyValuePair<string, StationType>> onlyIn = null)
        {
            if (entries == null) throw new ArgumentNullException(nameof(entries));
            foreach (var e in entries)
            {
                if (string.IsNullOrWhiteSpace(e.Key)) throw new ArgumentException("Id de combustible vacío.");
                if (e.Value <= 0f) throw new ArgumentOutOfRangeException(nameof(entries), e.Value, e.Key);
                _burn.Add(e.Key, e.Value);
            }
            foreach (var r in onlyIn ?? Array.Empty<KeyValuePair<string, StationType>>())
            {
                if (!_burn.ContainsKey(r.Key)) throw new ArgumentException($"Restricción sin combustible: {r.Key}");
                _onlyIn.Add(r.Key, r.Value);
            }
        }

        public IReadOnlyCollection<string> Ids => _burn.Keys;

        public bool IsFuel(string itemId) => itemId != null && _burn.ContainsKey(itemId);

        /// <summary>True si es combustible y la estación lo admite (el aceite de lámpara, solo en alquimia).</summary>
        public bool CanBurnIn(string itemId, StationType station)
            => IsFuel(itemId) && (!_onlyIn.TryGetValue(itemId, out var only) || only == station);

        /// <summary>Segundos que arde una unidad; 0 si no es combustible.</summary>
        public float BurnSeconds(string itemId) => IsFuel(itemId) ? _burn[itemId] : 0f;
    }
}
