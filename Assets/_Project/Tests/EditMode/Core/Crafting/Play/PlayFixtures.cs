using System.Linq;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Play;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Items;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Crafting.Play
{
    /// <summary>Campo de pruebas en miniatura: catálogo por defecto, perfil, estaciones y banco de mods.</summary>
    internal sealed class PlayFixtures
    {
        public static readonly ICraftingCatalog Catalog = new CraftingCatalog(DefaultCraftingData.Create());

        /// <summary>El Palo de madera del catálogo de oficios (el que se guarda y se vuelve a cargar).</summary>
        public static readonly ItemDefinition Palo = Catalog.TryGetItem("palo_madera", out var palo)
            ? palo
            : new ItemDefinition("palo_madera", "Palo de madera", ItemCategory.Weapon, Rarity.Common, 50,
                tags: MaterialTags.Madera);

        public PlayFixtures(int bagSlots = 16)
        {
            Profile = new CraftingProfile(new Inventory(bagSlots), curve: Catalog.Curve);
            Stations = new StationSessionFactory(Catalog, Profile, new Pcg32Random(5));
            Mods = new GearModBench(Catalog.Mods, Catalog.Quality, Profile);
            Shelf = new KnowledgeShelf(Catalog.Knowledge, Profile);
        }

        public CraftingProfile Profile { get; }
        public StationSessionFactory Stations { get; }
        public GearModBench Mods { get; }
        public KnowledgeShelf Shelf { get; }

        /// <summary>El pergamino de menor nivel del catálogo.</summary>
        public static KnowledgeDefinition Scroll() =>
            Catalog.Knowledge.Where(k => k.Type == KnowledgeType.Scroll).OrderBy(k => k.MinLevel).First();

        /// <summary>Sube el oficio del pergamino justo a su nivel mínimo.</summary>
        public void ReadyFor(KnowledgeDefinition knowledge) =>
            Profile.Skill(knowledge.Profession).Restore(knowledge.MinLevel, 0);

        /// <summary>El conocimiento de mayor nivel del catálogo.</summary>
        public static KnowledgeDefinition Highest() => Catalog.Knowledge.OrderByDescending(k => k.MinLevel).First();

        public ItemInstance GoodPalo()
        {
            var palo = ItemInstance.CreateNew(Palo, Catalog.Quality.Grade(Quality.Good));
            Profile.Bag.Add(palo);
            return palo;
        }

        public void Give(ItemDefinition item, int quantity = 1) => Profile.Bag.Add(ItemInstance.CreateNew(item), quantity);

        public ModDefinition Mod(string id) => Catalog.Mods.First(m => m.Id == id);
    }
}
