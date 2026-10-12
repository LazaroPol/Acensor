using Specimen.Core.Items;

namespace Specimen.Tests.Core.Crafting
{
    internal static class TagFixtures
    {
        public static readonly ItemDefinition Pine = Wood("pino", 1);
        public static readonly ItemDefinition Birch = Wood("abedul", 1);
        public static readonly ItemDefinition Oak = Wood("roble", 3);
        public static readonly ItemDefinition Iron = new ItemDefinition("lingote_hierro", "Lingote de hierro",
            ItemCategory.Material, Rarity.Common, 0, 20, 2, 1f, MaterialTags.Metal);
        public static readonly ItemDefinition Output =
            new ItemDefinition("mango", "Mango", ItemCategory.Material, Rarity.Common, 0, 10);

        private static ItemDefinition Wood(string id, int tier) => new ItemDefinition(id, id,
            ItemCategory.Material, Rarity.Common, 0, 20, tier, 0.5f, MaterialTags.Madera | MaterialTags.Combustible);

        public static Inventory With(int capacity, params (ItemDefinition def, int qty)[] items)
        {
            var inv = new Inventory(capacity);
            foreach (var (def, qty) in items) inv.Add(ItemInstance.CreateNew(def), qty);
            return inv;
        }
    }
}
