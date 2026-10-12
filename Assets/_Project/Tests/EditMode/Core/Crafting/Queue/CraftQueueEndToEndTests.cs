using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using Specimen.Core.Randomness;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;
using static Specimen.Tests.Core.Crafting.Queue.QueueFixtures;

namespace Specimen.Tests.Core.Crafting.Queue
{
    /// <summary>Criterio de fase OF: fabricar una receta en una estación consumiendo materiales por etiqueta.</summary>
    [TestFixture]
    public class CraftQueueEndToEndTests
    {
        [Test]
        public void FabricaEnLaFraguaConMetalPorEtiquetaYCombustible()
        {
            var crisol = new StationPartDefinition("crisol", StationType.Fragua, maxDurability: 3, qualityBonus: 1);
            var recipe = new RecipeDefinition("espada_metal", ProfessionId.Herrero, StationType.Fragua,
                new[] { IngredientSpec.ForTag(MaterialTags.Metal, 2, minTier: 1) },
                new[] { new RecipeOutput(Sword) }, requiredParts: new[] { "crisol" }, craftSeconds: 20f, xp: 30);
            var forge = new Workstation(StationType.Fragua);
            forge.PlacePart(new StationPart(crisol));
            Put(forge.Fuel, Wood, 1);
            var bag = new Inventory(8);
            Put(bag, Iron, 1);
            Put(bag, Steel, 4);
            var smith = new ProfessionSkill(ProfessionId.Herrero);
            var queue = new CraftQueue(forge, new QualityRoller(QualityRules.Default, new Pcg32Random(7)));

            Assert.AreEqual(EnqueueResult.Queued, queue.Enqueue(recipe, smith, bag));
            Assert.AreEqual(0, bag.CountOf(Iron.Id));
            Assert.AreEqual(3, bag.CountOf(Steel.Id));
            Assert.AreEqual(2f, queue.Jobs[0].QualityInput.AverageTier, 1e-3f);
            Assert.AreEqual(1, queue.Jobs[0].QualityInput.PartsBonus);

            queue.Tick(19f);
            Assert.AreEqual(0, forge.Output.CountOf(Sword.Id));
            queue.Tick(1f);

            Assert.AreEqual(1, forge.Output.CountOf(Sword.Id));
            Assert.AreNotEqual(Quality.None, forge.Output.Stacks[0].Item.Quality);
            Assert.AreEqual(30, smith.Xp);
            Assert.AreEqual(2, forge.Parts.Find("crisol").Durability);
            Assert.AreEqual(0, forge.Fuel.CountOf(Wood.Id));
            Assert.AreEqual(CraftQueueState.Idle, queue.State);
        }
    }
}
