using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Tests.Core.Crafting.Definitions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;
using static Specimen.Tests.Core.Crafting.Stations.StationFixtures;

namespace Specimen.Tests.Core.Crafting.Stations
{
    [TestFixture]
    public class RecipeAvailabilityTests
    {
        private static readonly UnlockCondition NeedsTreatise = UnlockCondition.KnowledgeRequired("tratado.acero");

        private static RecipeCatalog Catalog() => new RecipeCatalog(new[]
        {
            Recipe("herrero.espada"),
            Recipe("herrero.lingote_acero", part: "crisol"),
            Recipe("herrero.hacha", stationLevel: 2),
            Recipe("herrero.maestra", professionLevel: 10),
            Recipe("herrero.grande", ore: 50),
            Recipe("cocinero.sopa", station: StationType.HogarCocina),
            new RecipeDefinition("herrero.secreta", Specimen.Core.Professions.ProfessionId.Herrero, StationType.Fragua,
                new[] { Specimen.Core.Crafting.IngredientSpec.ForItem(Ore) }, new[] { new RecipeOutput(Sword) },
                unlock: NeedsTreatise)
        });

        private static string[] Ids(Workstation forge, FakeKnowledge known, IInventory bag) =>
            new RecipeAvailability(Catalog()).For(forge, known, _ => 1, bag).Select(o => o.Recipe.Id).ToArray();

        [Test]
        public void SoloRecetasDeSuTipoConocidasYDeSuNivel()
        {
            var ids = Ids(new Workstation(StationType.Fragua), new FakeKnowledge(), Stocked(10, 9, 0));
            CollectionAssert.AreEquivalent(new[] { "herrero.espada", "herrero.maestra", "herrero.grande" }, ids);
        }

        [Test]
        public void ColocarUnCrisolHabilitaUnaRecetaNueva()
        {
            var forge = new Workstation(StationType.Fragua);
            var known = new FakeKnowledge();
            var bag = Stocked(10, 9, 0);
            CollectionAssert.DoesNotContain(Ids(forge, known, bag), "herrero.lingote_acero");
            forge.PlacePart(new StationPart(Crisol));
            CollectionAssert.Contains(Ids(forge, known, bag), "herrero.lingote_acero");
        }

        [Test]
        public void SubirNivelYConocerMuestranMas()
        {
            var forge = new Workstation(StationType.Fragua);
            var bag = Stocked(10, 9, 9);
            forge.Upgrade(ForgeTo2(), bag);
            var known = new FakeKnowledge();
            known.Known.Add("tratado.acero");
            var ids = Ids(forge, known, bag);
            CollectionAssert.Contains(ids, "herrero.hacha");
            CollectionAssert.Contains(ids, "herrero.secreta");
        }

        [Test]
        public void VisiblesNoFabricablesInformanDelMotivo()
        {
            var options = new RecipeAvailability(Catalog())
                .For(new Workstation(StationType.Fragua), new FakeKnowledge(), _ => 1, Stocked(10, 3, 0))
                .ToDictionary(o => o.Recipe.Id);
            Assert.IsTrue(options["herrero.espada"].IsCraftable);
            Assert.AreEqual(RecipeBlockReason.ProfessionLevel, options["herrero.maestra"].Block);
            Assert.AreEqual(RecipeBlockReason.MissingMaterials, options["herrero.grande"].Block);
        }
    }
}
