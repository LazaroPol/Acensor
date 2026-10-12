using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Crafting.Stations;
using static Specimen.Tests.Core.Crafting.Session.SessionFixtures;

namespace Specimen.Tests.Core.Crafting.Session
{
    /// <summary>OF-15: craftear en una estación del Campo con materiales por etiqueta, cola y XP.</summary>
    [TestFixture]
    public class StationSessionTests
    {
        [Test]
        public void FabricaConsumiendoMaderaPorEtiquetaYDaXpAlTerminar()
        {
            var forge = Forge(out var profile);
            GiveAxeMaterials(profile.Bag);
            Give(profile.Bag, "lena", 1);
            AddAllFuel(forge);
            CraftCompletion done = null;
            forge.Completed += c => done = c;

            Assert.AreEqual(StationCraftResult.Queued, forge.Craft(Catalog.Recipes.Get(Hacha)));
            Assert.AreEqual(0, profile.Bag.CountOf("madera"));
            forge.Tick(60f);

            Assert.IsNotNull(done);
            Assert.AreEqual(10, done.Xp);
            Assert.AreEqual(10, profile.Skill(done.Profession).Xp);
            Assert.AreEqual(1, forge.CollectOutput());
            Assert.AreEqual(1, profile.Bag.CountOf("hacha_mano"));
        }

        [Test]
        public void SinMaterialesLaRecetaSaleEnGrisConSuMotivo()
        {
            var forge = Forge(out _);
            var axe = forge.Options().Single(o => o.Recipe.Id == Hacha);

            Assert.AreEqual(RecipeBlockReason.MissingMaterials, axe.Block);
            Assert.AreEqual("Faltan materiales", StationLabels.Block(axe));
            Assert.AreEqual(StationCraftResult.MissingMaterials, forge.Craft(axe.Recipe));
        }

        [Test]
        public void ColocarElCrisolHaceAparecerRecetasNuevas()
        {
            var forge = Forge(out var profile);
            Give(profile.Bag, Crisol, 1);
            Assert.IsFalse(forge.Options().Any(o => o.Recipe.Id == LingoteHierro));
            CollectionAssert.AreEqual(new[] { Crisol }, forge.PartsInBag().Select(p => p.Id));

            Assert.AreEqual(PartPlaceResult.Placed, forge.PlacePart(Crisol));

            var ingot = forge.Options().Single(o => o.Recipe.Id == LingoteHierro);
            Assert.AreEqual("Requiere Herrero 5", StationLabels.Block(ingot));
            Assert.AreEqual(0, profile.Bag.CountOf(Crisol));
            Assert.AreEqual(StationCraftResult.ProfessionLevel, forge.Craft(ingot.Recipe));
        }

        [Test]
        public void LaColaSePausaSinCombustibleYSigueAlEcharlo()
        {
            var forge = Forge(out var profile);
            GiveAxeMaterials(profile.Bag);
            forge.Craft(Catalog.Recipes.Get(Hacha));

            forge.Tick(1f);
            Assert.AreEqual(CraftQueueState.NoFuel, forge.Queue.State);
            Assert.AreEqual(0f, forge.Queue.Jobs[0].Progress);

            Give(profile.Bag, "lena", 1);
            Assert.AreEqual(1, forge.AddFuel("lena"));
            forge.Tick(2f);
            Assert.AreEqual(CraftQueueState.Working, forge.Queue.State);
            Assert.AreEqual(0.5f, forge.Queue.Jobs[0].Progress, 1e-3f);
        }

        [Test]
        public void SinHuecoEnLaColaNoEncola()
        {
            var forge = Forge(out var profile);
            for (int i = 0; i < 3; i++) GiveAxeMaterials(profile.Bag);
            var axe = Catalog.Recipes.Get(Hacha);
            forge.Craft(axe);
            forge.Craft(axe);

            Assert.AreEqual(StationCraftResult.QueueFull, forge.Craft(axe));
            Assert.AreEqual("La cola está llena", StationLabels.Result(StationCraftResult.QueueFull));
        }

        private static void AddAllFuel(StationSession forge)
        {
            foreach (var fuel in forge.FuelsInBag().Where(f => f.Id == "lena")) forge.AddFuel(fuel.Id, 99);
        }
    }
}
