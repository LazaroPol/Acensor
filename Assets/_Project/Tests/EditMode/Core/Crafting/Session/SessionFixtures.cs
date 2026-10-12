using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Items;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Crafting.Session
{
    /// <summary>Estaciones montadas con el catálogo de oficios por defecto, como en el Campo de pruebas.</summary>
    internal static class SessionFixtures
    {
        public const string Hacha = "herrero.hacha_mano";
        public const string LingoteHierro = "herrero.lingote";
        public const string Crisol = "pieza.crisol";

        public static readonly ICraftingCatalog Catalog = new CraftingCatalog(DefaultCraftingData.Create());

        public static StationSession Forge(out CraftingProfile profile, int bagSlots = 12)
        {
            profile = new CraftingProfile(new Inventory(bagSlots), curve: Catalog.Curve);
            return new StationSessionFactory(Catalog, profile, new Pcg32Random(3)).Create(StationType.Fragua);
        }

        public static void Give(IInventory bag, string id, int quantity) =>
            bag.Add(ItemInstance.CreateNew(ContentItems.Get(id)), quantity);

        /// <summary>Lo justo para el hacha de mano: 2 lingotes de cobre y 1 de cualquier madera.</summary>
        public static void GiveAxeMaterials(IInventory bag)
        {
            Give(bag, "lingote_cobre", 2);
            Give(bag, "madera", 1);
        }
    }
}
