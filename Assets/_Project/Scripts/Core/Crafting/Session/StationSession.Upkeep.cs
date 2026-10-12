using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Session
{
    /// <summary>Combustible y mejora de nivel de la estación en uso.</summary>
    public sealed partial class StationSession
    {
        /// <summary>Combustibles de la bolsa que esta estación puede quemar (vacío si no usa combustible).</summary>
        public IReadOnlyList<ItemDefinition> FuelsInBag() => !Station.HasFuelSlots
            ? new List<ItemDefinition>()
            : Profile.Bag.Stacks.Select(s => s.Definition).Where(d => _services.Fuel.CanBurnIn(d.Id, Station.Type))
                .GroupBy(d => d.Id).Select(g => g.First()).ToList();

        /// <summary>Pasa <paramref name="quantity"/> unidades (o las que quepan) de la bolsa al combustible.</summary>
        public int AddFuel(string itemId, int quantity = 1)
        {
            if (!Station.HasFuelSlots || !_services.Fuel.CanBurnIn(itemId, Station.Type)) return 0;
            var stack = Profile.Bag.Stacks.FirstOrDefault(s => s.Id == itemId);
            if (stack == null) return 0;
            int amount = System.Math.Min(quantity, Profile.Bag.CountOf(itemId));
            int moved = Station.Fuel.Add(ItemInstance.CreateNew(stack.Definition), amount);
            if (moved > 0) Profile.Bag.Remove(itemId, moved);
            return moved;
        }

        /// <summary>Siguiente mejora de la estación; null si ya está al máximo o no hay datos.</summary>
        public StationUpgrade NextUpgrade =>
            Station.IsMaxLevel ? null : _services.Upgrades.For(Station.Type, Station.Level + 1);

        public UpgradeResult CanUpgrade() =>
            NextUpgrade == null ? UpgradeResult.WrongLevel : Station.CanUpgrade(NextUpgrade, Materials);

        /// <summary>Mejora la estación gastando de la entrada y de la bolsa (todo o nada).</summary>
        public UpgradeResult Upgrade() =>
            NextUpgrade == null ? UpgradeResult.WrongLevel : Station.Upgrade(NextUpgrade, Materials);
    }
}
