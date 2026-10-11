using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Crafting;
using Specimen.Core.Items;

namespace Specimen.Core.Elevator
{
    public sealed partial class ElevatorPlatform
    {
        /// <summary>Se lanza tras subir de nivel un módulo, con la ranura ya mejorada.</summary>
        public event Action<ModuleSlot> ModuleUpgraded;

        /// <summary>Sube un nivel el módulo de la ranura; si falla, el inventario no cambia.</summary>
        public UpgradeResult TryUpgrade(int slotIndex, IInventory inventory)
        {
            Guard.NotNull(inventory, nameof(inventory));
            var check = ValidateUpgrade(slotIndex, inventory, out var cost);
            if (check != UpgradeResult.Upgraded) return check;
            ModuleBlueprint.Pay(cost, inventory);
            _slots[slotIndex].LevelUp();
            ModuleUpgraded?.Invoke(_slots[slotIndex]);
            return UpgradeResult.Upgraded;
        }

        private UpgradeResult ValidateUpgrade(int slotIndex, IInventory inventory,
            out IReadOnlyList<RecipeIngredient> cost)
        {
            cost = null;
            if (slotIndex < 0 || slotIndex >= _slots.Length) return UpgradeResult.InvalidSlot;
            var slot = _slots[slotIndex];
            if (slot.IsEmpty) return UpgradeResult.EmptySlot;
            if (!_catalog.TryGet(slot.Module.Value, out var blueprint) ||
                !blueprint.TryGetUpgradeCost(slot.Level, out cost)) return UpgradeResult.MaxLevel;
            return ModuleBlueprint.CanPay(cost, inventory) ? UpgradeResult.Upgraded : UpgradeResult.MissingMaterials;
        }
    }
}
