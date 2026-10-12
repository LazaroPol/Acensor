using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Play;
using Specimen.Core.Crafting.Save;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Play
{
    /// <summary>OF-16: salir y volver a entrar conserva niveles, recetas, estaciones, piezas, cola e inventario.</summary>
    [TestFixture]
    public class CraftingSnapshotTests
    {
        private const string Hacha = "herrero.hacha_mano";

        [Test]
        public void GuardarYCargarConservaTodoElProgreso()
        {
            var before = new PlayFixtures();
            var forge = Prepare(before, out var palo, out var scroll);
            var loaded = RoundTrip(before);
            var after = new PlayFixtures();
            new CraftingRestorer(PlayFixtures.Catalog, after.Mods).Apply(loaded, after.Profile, after.Stations);

            Assert.AreEqual(before.Profile.LevelOf(ProfessionId.Herrero), after.Profile.LevelOf(ProfessionId.Herrero));
            Assert.AreEqual(before.Profile.LevelOf(scroll.Profession), after.Profile.LevelOf(scroll.Profession));
            Assert.AreEqual(before.Profile.Skill(ProfessionId.Herrero).Xp, after.Profile.Skill(ProfessionId.Herrero).Xp);
            Assert.IsTrue(after.Profile.Knowledge.KnowsRecipe(scroll.Teaches[0]));
            Assert.IsTrue(after.Profile.Knowledge.Knows(scroll.Id));
            var station = after.Stations.Create(StationType.Fragua);
            Assert.AreEqual(2, station.Station.Level);
            Assert.AreEqual(37, station.Station.Parts.Find("pieza.crisol").Durability);
            Assert.AreEqual(forge.Station.Fuel.CountOf("lena"), station.Station.Fuel.CountOf("lena"));
            Assert.AreEqual(1, station.Queue.Jobs.Count);
            Assert.AreEqual(forge.Queue.Jobs[0].Remaining, station.Queue.Jobs[0].Remaining, 0.01f);
            Assert.IsFalse(station.Queue.Cancel(station.Queue.Jobs[0]), "Sus materiales ya se pagaron.");

            var restored = after.Profile.Bag.Stacks.Single(s => s.Id == palo.Id).Item;
            Assert.AreEqual(Quality.Good, restored.Quality);
            Assert.AreEqual(palo.Durability.Current, restored.Durability.Current);
            Assert.AreEqual(DefaultMods.PiedraAtada, after.Mods.ModsOf(restored).Single().Id);
            Assert.AreEqual(3, after.Profile.Bag.CountOf("lingote_cobre"));
        }

        [Test]
        public void LaColaRestauradaTerminaYDaElObjeto()
        {
            var before = new PlayFixtures();
            Prepare(before, out _, out _);
            var after = new PlayFixtures();
            new CraftingRestorer(PlayFixtures.Catalog).Apply(RoundTrip(before), after.Profile, after.Stations);
            var forge = after.Stations.Create(StationType.Fragua);
            forge.Tick(120f);
            Assert.AreEqual(1, forge.Station.Output.CountOf("hacha_mano"));
        }

        [Test]
        public void LoQueElCatalogoYaNoConoceSeIgnora()
        {
            var data = new CraftingSaveData();
            data.Stations.Add(new StationEntry("EstacionRetirada", 2));
            data.Inventory.Add(InventoryEntry.Stack(CraftingSnapshot.Bag, 0, "objeto_retirado", 3));
            data.Jobs.Add(new JobEntry("Fragua", "receta.retirada", 5, ProfessionId.Herrero, 1));
            var play = new PlayFixtures();
            new CraftingRestorer(PlayFixtures.Catalog).Apply(data, play.Profile, play.Stations);
            Assert.AreEqual(0, play.Profile.Bag.UsedSlots);
            Assert.IsEmpty(play.Stations.Sessions);
        }

        private static StationSession Prepare(PlayFixtures play, out ItemInstance palo, out Specimen.Core.Crafting.Knowledge.KnowledgeDefinition scroll)
        {
            scroll = PlayFixtures.Scroll();
            play.ReadyFor(scroll);
            play.Profile.Skill(ProfessionId.Herrero).Restore(System.Math.Max(3, play.Profile.LevelOf(ProfessionId.Herrero)), 4);
            play.Give(KnowledgeItems.For(scroll));
            play.Shelf.Read(scroll.Id);
            palo = play.GoodPalo();
            palo.Durability.Wear(7);
            play.Give(play.Mod(DefaultMods.PiedraAtada).Item);
            play.Mods.Install(palo, DefaultMods.PiedraAtada);
            var forge = play.Stations.Create(StationType.Fragua);
            forge.RestoreLevel(2);
            forge.RestorePart("pieza.crisol", 37);
            play.Give(ContentItems.Get("lingote_cobre"), 5);
            play.Give(ContentItems.Get("madera"), 1);
            play.Give(ContentItems.Get("lena"), 4);
            forge.AddFuel("lena", 4);
            Assert.AreEqual(StationCraftResult.Queued, forge.Craft(PlayFixtures.Catalog.Recipes.Get(Hacha)));
            forge.Tick(1f);
            return forge;
        }

        private static CraftingSaveData RoundTrip(PlayFixtures play)
        {
            var save = new CraftingSave(new InMemoryCraftingSaveStore());
            save.Save(CraftingSnapshot.Capture(play.Profile, play.Stations.Sessions, play.Mods));
            return save.Load();
        }
    }
}
