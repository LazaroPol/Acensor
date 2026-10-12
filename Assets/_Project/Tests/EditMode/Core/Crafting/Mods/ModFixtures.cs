using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Crafting.Mods
{
    internal static class ModFixtures
    {
        public static readonly ItemDefinition Palo = new ItemDefinition("palo_madera", "Palo de madera",
            ItemCategory.Weapon, Rarity.Common, 50, tags: MaterialTags.Madera);
        public static readonly ItemDefinition Peto = new ItemDefinition("peto", "Peto",
            ItemCategory.Armor, Rarity.Common, 40);

        public static readonly GearStatSheet PaloStats = GearStatSheet.Empty
            .With(GearStat.DañoContundente, 6f).With(GearStat.Velocidad, 1f).With(GearStat.Peso, 1f);

        public static ModdedItem Modded(ItemDefinition def, Quality quality)
        {
            var rules = QualityRules.Default;
            var instance = quality == Quality.None
                ? ItemInstance.CreateNew(def)
                : ItemInstance.CreateNew(def, rules.Grade(quality));
            return new ModdedItem(instance, rules);
        }

        public static Inventory BagWith(int capacity, params ModDefinition[] mods)
        {
            var bag = new Inventory(capacity);
            foreach (var m in mods) bag.Add(ItemInstance.CreateNew(m.Item));
            return bag;
        }

        public static ModInstaller Installer() => new ModInstaller(new ModInstallValidator(new DefaultGearTags()));
    }
}
