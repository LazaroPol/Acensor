using Specimen.Core.Common;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Mejora de nivel de la <see cref="Workstation"/>: atómica (o gasta todo o nada).</summary>
    public sealed partial class Workstation
    {
        public UpgradeResult CanUpgrade(StationUpgrade upgrade, IInventory materials)
        {
            Guard.NotNull(upgrade, nameof(upgrade));
            Guard.NotNull(materials, nameof(materials));
            if (upgrade.Station != Type) return UpgradeResult.WrongStation;
            if (upgrade.TargetLevel != Level + 1) return UpgradeResult.WrongLevel;
            return TagConsumptionPlanner.Plan(upgrade.Cost, materials).IsComplete
                ? UpgradeResult.Upgraded
                : UpgradeResult.MissingMaterials;
        }

        /// <summary>Gasta el coste de <paramref name="materials"/> y sube un nivel.</summary>
        public UpgradeResult Upgrade(StationUpgrade upgrade, IInventory materials)
        {
            var check = CanUpgrade(upgrade, materials);
            if (check != UpgradeResult.Upgraded) return check;
            foreach (var entry in TagConsumptionPlanner.Plan(upgrade.Cost, materials).Entries)
                materials.Remove(entry.Item.Id, entry.Quantity);
            Level = upgrade.TargetLevel;
            QueueSize = upgrade.QueueSize;
            LevelQualityBonus = upgrade.QualityBonus;
            return UpgradeResult.Upgraded;
        }
    

        /// <summary>Aplica una mejora sin coste (al cargar un guardado, GDD §17.7).</summary>
        public void Restore(StationUpgrade upgrade)
        {
            Guard.NotNull(upgrade, nameof(upgrade));
            if (upgrade.Station != Type || upgrade.TargetLevel <= Level) return;
            Level = upgrade.TargetLevel;
            QueueSize = upgrade.QueueSize;
            LevelQualityBonus = upgrade.QualityBonus;
        }
    }
}
