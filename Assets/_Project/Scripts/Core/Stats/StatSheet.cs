using System;
using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Stats
{
    /// <summary>Conjunto de atributos de un personaje, indexado por <see cref="StatType"/>.</summary>
    public sealed class StatSheet
    {
        private readonly Dictionary<StatType, Stat> _stats = new Dictionary<StatType, Stat>();
        private readonly IStatFormula _formula;

        public StatSheet(IStatFormula formula = null)
        {
            _formula = formula ?? StandardStatFormula.Instance;
        }

        public IEnumerable<StatType> Types => _stats.Keys;

        public Stat Define(StatType type, float baseValue)
        {
            if (_stats.ContainsKey(type))
                throw new InvalidOperationException($"El atributo {type} ya está definido.");
            var stat = new Stat(baseValue, _formula);
            _stats.Add(type, stat);
            return stat;
        }

        public bool Has(StatType type) => _stats.ContainsKey(type);

        public Stat Get(StatType type)
            => _stats.TryGetValue(type, out var s) ? s : throw new KeyNotFoundException($"Atributo {type} no definido.");

        public float ValueOf(StatType type) => Get(type).Value;

        public void AddModifier(StatType type, StatModifier modifier) => Get(type).AddModifier(modifier);

        public int RemoveAllFromSource(object source)
        {
            Guard.NotNull(source, nameof(source));
            int total = 0;
            foreach (var stat in _stats.Values) total += stat.RemoveAllFromSource(source);
            return total;
        }
    }
}
