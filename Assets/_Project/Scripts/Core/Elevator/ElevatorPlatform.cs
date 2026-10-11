using System;
using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Elevator
{
    /// <summary>
    /// Plataforma del Ascensor-Refugio (GDD §6): ranuras donde se construyen módulos pagando
    /// materiales. Cada tipo de módulo se construye una sola vez y persiste hasta <see cref="Reset"/>.
    /// </summary>
    public sealed class ElevatorPlatform
    {
        private readonly ModuleSlot[] _slots;
        private readonly IBlueprintCatalog _catalog;

        public IReadOnlyList<ModuleSlot> Slots => _slots;

        /// <summary>Se lanza tras construir un módulo, con la ranura ocupada.</summary>
        public event Action<ModuleSlot> ModuleBuilt;

        public ElevatorPlatform(int slotCount, IBlueprintCatalog catalog)
        {
            Guard.Positive(slotCount, nameof(slotCount));
            _catalog = Guard.NotNull(catalog, nameof(catalog));
            _slots = Enumerable.Range(0, slotCount).Select(i => new ModuleSlot(i)).ToArray();
        }

        public bool Has(ModuleType type) => _slots.Any(s => s.Module == type);

        /// <summary>Construye el módulo en la ranura; si falla, el inventario no cambia.</summary>
        public BuildResult TryBuild(int slotIndex, ModuleType type, IInventory inventory)
        {
            Guard.NotNull(inventory, nameof(inventory));
            var check = Validate(slotIndex, type, inventory, out var blueprint);
            if (check != BuildResult.Built) return check;
            foreach (var c in blueprint.Cost) inventory.Remove(c.ItemId, c.Quantity);
            _slots[slotIndex].Place(type);
            ModuleBuilt?.Invoke(_slots[slotIndex]);
            return BuildResult.Built;
        }

        /// <summary>Vacía todas las ranuras (nueva iteración).</summary>
        public void Reset()
        {
            foreach (var s in _slots) s.Clear();
        }

        private BuildResult Validate(int slotIndex, ModuleType type, IInventory inventory,
            out ModuleBlueprint blueprint)
        {
            blueprint = null;
            if (slotIndex < 0 || slotIndex >= _slots.Length) return BuildResult.InvalidSlot;
            if (!_slots[slotIndex].IsEmpty) return BuildResult.SlotOccupied;
            if (Has(type)) return BuildResult.AlreadyBuilt;
            if (!_catalog.TryGet(type, out blueprint)) return BuildResult.UnknownBlueprint;
            return blueprint.IsAffordable(inventory) ? BuildResult.Built : BuildResult.MissingMaterials;
        }
    }
}
