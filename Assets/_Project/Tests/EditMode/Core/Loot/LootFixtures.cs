using Specimen.Core.Items;
using Specimen.Core.Loot;

namespace Specimen.Tests.Core.Loot
{
    internal static class LootFixtures
    {
        public static readonly ItemDefinition Scrap =
            new ItemDefinition("scrap", "Chatarra", ItemCategory.Material, Rarity.Common, maxStack: 20);
        public static readonly ItemDefinition Blade =
            new ItemDefinition("blade", "Hoja", ItemCategory.Weapon, Rarity.Rare, baseDurability: 50);
        public static readonly ItemDefinition Relic =
            new ItemDefinition("relic", "Reliquia", ItemCategory.Weapon, Rarity.Legendary, baseDurability: 80);

        /// <summary>Chatarra (peso 10, 2-5 uds), Hoja (peso 2), Reliquia (peso 1, solo anillo 3).</summary>
        public static LootTable Table(IRingWeighting weighting = null) => new LootTable(new[]
        {
            new LootEntry(Scrap, 10f, minQuantity: 2, maxQuantity: 5),
            new LootEntry(Blade, 2f),
            new LootEntry(Relic, 1f, minRing: 3)
        }, weighting ?? new RarityRingWeighting());
    }
}
