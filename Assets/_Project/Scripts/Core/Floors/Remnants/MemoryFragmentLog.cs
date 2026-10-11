using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Floors.Remnants
{
    /// <summary>
    /// Fragmentos de memoria recogidos de un catálogo de ids [0, Total). Persisten entre pisos y sirven,
    /// p. ej., para desempatar votaciones de Ecos.
    /// </summary>
    public sealed class MemoryFragmentLog
    {
        private readonly HashSet<int> _collected = new HashSet<int>();

        public MemoryFragmentLog(int total) => Total = Guard.Positive(total, nameof(total));

        public int Total { get; }
        public int Count => _collected.Count;
        public bool IsComplete => Count == Total;

        public bool Has(int fragmentId) => _collected.Contains(Validate(fragmentId));

        /// <summary>Registra el fragmento; devuelve false si ya estaba recogido.</summary>
        public bool Collect(int fragmentId) => _collected.Add(Validate(fragmentId));

        /// <summary>Ids aún por recoger, en orden ascendente.</summary>
        public IReadOnlyList<int> Missing() => Enumerable.Range(0, Total).Where(id => !_collected.Contains(id)).ToList();

        private int Validate(int fragmentId)
        {
            if (fragmentId < 0 || fragmentId >= Total)
                throw new ArgumentOutOfRangeException(nameof(fragmentId), fragmentId, $"El fragmento debe estar en [0, {Total}).");
            return fragmentId;
        }
    }
}
