using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;
using static Specimen.Tests.Core.Crafting.TagFixtures;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class ConsumptionPlanTests
    {
        private static readonly IngredientSpec[] Specs =
            { IngredientSpec.ForTag(MaterialTags.Madera, 3), IngredientSpec.ForItem(Iron, 1) };

        [Test]
        public void PlanningDoesNotTouchInventory()
        {
            var inv = With(4, (Pine, 1), (Iron, 1));
            var plan = TagConsumptionPlanner.Plan(Specs, inv);
            Assert.That(plan.IsComplete, Is.False);
            Assert.That(plan.Apply(Output, 1, inv), Is.EqualTo(CraftResult.MissingIngredients));
            Assert.That(inv.CountOf("pino"), Is.EqualTo(1));
            Assert.That(inv.CountOf("lingote_hierro"), Is.EqualTo(1));
        }

        [Test]
        public void ApplySpendsPlanAndAddsOutput()
        {
            var inv = With(4, (Pine, 2), (Oak, 4), (Iron, 1));
            var result = TagConsumptionPlanner.Plan(Specs, inv).Apply(Output, 2, inv);
            Assert.That(result, Is.EqualTo(CraftResult.Crafted));
            Assert.That(inv.CountOf("pino"), Is.EqualTo(0));
            Assert.That(inv.CountOf("roble"), Is.EqualTo(3));
            Assert.That(inv.CountOf("lingote_hierro"), Is.EqualTo(0));
            Assert.That(inv.CountOf("mango"), Is.EqualTo(2));
        }

        [Test]
        public void ApplyIsAtomicWhenOutputDoesNotFit()
        {
            var inv = With(3, (Pine, 5), (Iron, 2), (Oak, 1));
            var result = TagConsumptionPlanner.Plan(Specs, inv).Apply(Output, 1, inv);
            Assert.That(result, Is.EqualTo(CraftResult.NoSpace));
            Assert.That(inv.CountOf("pino"), Is.EqualTo(5));
            Assert.That(inv.CountOf("lingote_hierro"), Is.EqualTo(2));
            Assert.That(inv.CountOf("mango"), Is.EqualTo(0));
        }

        [Test]
        public void StalePlanIsRejected()
        {
            var inv = With(4, (Pine, 3), (Iron, 1));
            var plan = TagConsumptionPlanner.Plan(Specs, inv);
            inv.Remove("pino", 1);
            Assert.That(plan.Apply(Output, 1, inv), Is.EqualTo(CraftResult.MissingIngredients));
            Assert.That(inv.CountOf("lingote_hierro"), Is.EqualTo(1));
        }
    }
}
