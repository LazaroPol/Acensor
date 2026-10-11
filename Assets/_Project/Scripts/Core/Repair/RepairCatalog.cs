using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Repair
{
    /// <summary>Catálogo en memoria con una regla por categoría.</summary>
    public sealed class RepairCatalog : IRepairCatalog
    {
        private readonly Dictionary<ItemCategory, RepairRule> _rules = new Dictionary<ItemCategory, RepairRule>();

        public RepairCatalog(IEnumerable<RepairRule> rules)
        {
            foreach (var rule in Guard.NotNull(rules, nameof(rules)))
            {
                Guard.NotNull(rule, nameof(rules));
                if (_rules.ContainsKey(rule.Category))
                    throw new ArgumentException($"Regla duplicada para {rule.Category}.", nameof(rules));
                _rules.Add(rule.Category, rule);
            }
        }

        public bool TryGet(ItemCategory category, out RepairRule rule) => _rules.TryGetValue(category, out rule);
    }
}
