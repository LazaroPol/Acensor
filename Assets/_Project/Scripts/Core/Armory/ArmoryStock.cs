using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Armory
{
    /// <summary>
    /// Stock compartido por servidor/iteración: cada tipo tiene una capacidad fija
    /// (p. ej. 10 escudos pesados). Los tipos no definidos no tienen stock.
    /// </summary>
    public sealed class ArmoryStock<TItem> : IArmoryStock<TItem>
    {
        private readonly Dictionary<TItem, int> _capacity = new Dictionary<TItem, int>();
        private readonly Dictionary<TItem, int> _taken = new Dictionary<TItem, int>();

        public event Action<TItem, int> StockChanged;

        public void Define(TItem item, int capacity)
        {
            Guard.NonNegative(capacity, nameof(capacity));
            _capacity[item] = capacity;
            _taken[item] = 0;
            StockChanged?.Invoke(item, capacity);
        }

        public int Capacity(TItem item) => _capacity.TryGetValue(item, out var c) ? c : 0;

        public int Remaining(TItem item) => Capacity(item) - Taken(item);

        public bool TryTake(TItem item)
        {
            if (Remaining(item) <= 0) return false;
            _taken[item]++;
            StockChanged?.Invoke(item, Remaining(item));
            return true;
        }

        public bool Return(TItem item)
        {
            if (Taken(item) <= 0) return false;
            _taken[item]--;
            StockChanged?.Invoke(item, Remaining(item));
            return true;
        }

        public void Restock()
        {
            foreach (var item in new List<TItem>(_taken.Keys))
            {
                if (_taken[item] == 0) continue;
                _taken[item] = 0;
                StockChanged?.Invoke(item, Capacity(item));
            }
        }

        private int Taken(TItem item) => _taken.TryGetValue(item, out var t) ? t : 0;
    }
}
