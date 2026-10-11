using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Elevator;
using static Specimen.Tests.Core.Elevator.ElevatorFixtures;

namespace Specimen.Tests.Core.Elevator
{
    [TestFixture]
    public class ModuleBlueprintTests
    {
        [Test]
        public void RejectsEmptyNullOrRepeatedCost()
        {
            Assert.Throws<ArgumentNullException>(() => new ModuleBlueprint(ModuleType.Loom, null));
            Assert.Throws<ArgumentException>(() => new ModuleBlueprint(ModuleType.Loom, new RecipeIngredient[0]));
            Assert.Throws<ArgumentException>(() => new ModuleBlueprint(ModuleType.Loom, new RecipeIngredient[] { null }));
            Assert.Throws<ArgumentException>(() => new ModuleBlueprint(ModuleType.Loom,
                new[] { new RecipeIngredient(Wood, 1), new RecipeIngredient(Wood, 2) }));
        }

        [Test]
        public void AffordableOnlyWithEveryMaterial()
        {
            var plan = BonfirePlan();
            Assert.That(plan.IsAffordable(Stocked(3, 2)), Is.True);
            Assert.That(plan.IsAffordable(Stocked(3, 1)), Is.False);
            Assert.That(plan.IsAffordable(Stocked(0, 5)), Is.False);
        }

        [Test]
        public void CatalogFindsByTypeAndRejectsDuplicates()
        {
            Assert.That(Catalog().TryGet(ModuleType.Forge, out var forge), Is.True);
            Assert.That(forge.Type, Is.EqualTo(ModuleType.Forge));
            Assert.That(Catalog().TryGet(ModuleType.Bunk, out _), Is.False);
            Assert.Throws<ArgumentException>(() => new BlueprintCatalog(new[] { ForgePlan(), ForgePlan() }));
            Assert.Throws<ArgumentException>(() => new BlueprintCatalog(new ModuleBlueprint[] { null }));
            Assert.Throws<ArgumentNullException>(() => new BlueprintCatalog(null));
        }
    }
}
