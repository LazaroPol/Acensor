using System;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Repair
{
    /// <summary>
    /// Repara equipo gastando materiales del inventario. Si no hay material suficiente para
    /// repararlo del todo, usa lo que haya y lo repara en parte.
    /// </summary>
    public sealed class RepairService
    {
        private readonly IRepairCatalog _catalog;

        public RepairService(IRepairCatalog catalog)
        {
            _catalog = Guard.NotNull(catalog, nameof(catalog));
        }

        public RepairReport TryRepair(ItemInstance item, IInventory inventory)
        {
            Guard.NotNull(item, nameof(item));
            Guard.NotNull(inventory, nameof(inventory));
            var durability = item.Durability;
            if (durability.IsIndestructible) return new RepairReport(RepairResult.NotRepairable);
            var missing = durability.Max - durability.Current;
            if (missing == 0) return new RepairReport(RepairResult.NothingToRepair);
            if (!_catalog.TryGet(item.Definition.Category, out var rule)) return new RepairReport(RepairResult.NoRule);

            var available = inventory.CountOf(rule.Material.Id);
            if (available == 0) return new RepairReport(RepairResult.MissingMaterials);

            var units = Math.Min(rule.UnitsFor(missing), available);
            var used = inventory.Remove(rule.Material.Id, units);
            var restored = durability.Repair(used * rule.PointsPerUnit);
            var full = durability.Current == durability.Max;
            return new RepairReport(full ? RepairResult.Repaired : RepairResult.PartiallyRepaired, used, restored);
        }
    }
}
