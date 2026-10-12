using System.Collections.Generic;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting
{
    /// <summary>
    /// Estado de trabajo del planificador: copia de las existencias por ítem (sin tocar el inventario)
    /// y las líneas del plan, fusionadas por ítem en orden de primera aparición.
    /// </summary>
    internal sealed class PlanBuilder
    {
        private readonly Dictionary<string, int> _stock = new Dictionary<string, int>();
        private readonly Dictionary<string, ItemDefinition> _defs = new Dictionary<string, ItemDefinition>();
        private readonly List<string> _order = new List<string>();
        private readonly Dictionary<string, int> _planned = new Dictionary<string, int>();

        public PlanBuilder(IInventory inventory)
        {
            foreach (var stack in inventory.Stacks)
            {
                if (stack.Definition.IsDegradable) continue;
                _defs[stack.Id] = stack.Definition;
                _stock[stack.Id] = Available(stack.Id) + stack.Quantity;
            }
        }

        public int Available(string itemId) => _stock.TryGetValue(itemId, out int n) ? n : 0;

        public ItemDefinition Definition(string itemId) => _defs.TryGetValue(itemId, out var d) ? d : null;

        /// <summary>Ítems con existencias que acepta el ingrediente: menor tier, luego menos unidades, luego id.</summary>
        public List<ItemDefinition> CandidatesFor(IngredientSpec spec)
        {
            var list = new List<ItemDefinition>();
            foreach (var def in _defs.Values)
                if (Available(def.Id) > 0 && spec.Accepts(def)) list.Add(def);
            list.Sort((a, b) =>
            {
                int byTier = a.Tier.CompareTo(b.Tier);
                if (byTier != 0) return byTier;
                int byCount = Available(a.Id).CompareTo(Available(b.Id));
                return byCount != 0 ? byCount : string.CompareOrdinal(a.Id, b.Id);
            });
            return list;
        }

        /// <summary>Reserva hasta <paramref name="wanted"/> unidades; devuelve las reservadas.</summary>
        public int Take(ItemDefinition def, int wanted)
        {
            int taken = System.Math.Min(wanted, Available(def.Id));
            if (taken <= 0) return 0;
            _stock[def.Id] -= taken;
            if (!_planned.ContainsKey(def.Id)) { _order.Add(def.Id); _planned[def.Id] = 0; }
            _planned[def.Id] += taken;
            return taken;
        }

        public ConsumptionPlan Build()
        {
            var entries = new List<PlannedConsumption>(_order.Count);
            foreach (var id in _order) entries.Add(new PlannedConsumption(_defs[id], _planned[id]));
            return new ConsumptionPlan(entries, true);
        }
    }
}
