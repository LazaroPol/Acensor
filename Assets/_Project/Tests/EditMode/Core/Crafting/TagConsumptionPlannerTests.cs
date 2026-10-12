using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;
using static Specimen.Tests.Core.Crafting.TagFixtures;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class TagConsumptionPlannerTests
    {
        private static IngredientSpec Wood(int qty, int minTier = 0) =>
            IngredientSpec.ForTag(MaterialTags.Madera, qty, minTier);

        [Test]
        public void SpendsLowestTierFirst()
        {
            var inv = With(4, (Oak, 5), (Pine, 5));
            var plan = TagConsumptionPlanner.Plan(new[] { Wood(3) }, inv);
            Assert.That(plan.IsComplete, Is.True);
            Assert.That(plan.Entries.Count, Is.EqualTo(1));
            Assert.That(plan.Entries[0].Item, Is.SameAs(Pine));
            Assert.That(plan.Entries[0].Quantity, Is.EqualTo(3));
        }

        [Test]
        public void AtEqualTierSpendsSmallerStackFirstThenOthers()
        {
            var inv = With(4, (Pine, 5), (Birch, 2), (Oak, 5));
            var plan = TagConsumptionPlanner.Plan(new[] { Wood(8) }, inv);
            Assert.That(plan.Entries[0].Item, Is.SameAs(Birch));
            Assert.That(plan.Entries[0].Quantity, Is.EqualTo(2));
            Assert.That(plan.Entries[1].Item, Is.SameAs(Pine));
            Assert.That(plan.Entries[2].Item, Is.SameAs(Oak));
            Assert.That(plan.Entries[2].Quantity, Is.EqualTo(1));
        }

        [Test]
        public void RespectsMinimumTier()
        {
            var inv = With(4, (Pine, 9), (Oak, 2));
            Assert.That(TagConsumptionPlanner.Plan(new[] { Wood(3, 2) }, inv).IsComplete, Is.False);
            Assert.That(TagConsumptionPlanner.Plan(new[] { Wood(2, 2) }, inv).Entries[0].Item, Is.SameAs(Oak));
        }

        [Test]
        public void PinnedItemIsSpentFirst()
        {
            var inv = With(4, (Pine, 5), (Oak, 2));
            var plan = TagConsumptionPlanner.Plan(new[] { Wood(3) }, inv, new[] { new IngredientChoice(0, "roble") });
            Assert.That(plan.Entries[0].Item, Is.SameAs(Oak));
            Assert.That(plan.Entries[0].Quantity, Is.EqualTo(2));
            Assert.That(plan.Entries[1].Item, Is.SameAs(Pine));
            Assert.That(plan.Entries[1].Quantity, Is.EqualTo(1));
        }

        [Test]
        public void InvalidChoiceThrows()
        {
            var inv = With(4, (Pine, 5), (Iron, 2));
            var specs = new[] { IngredientSpec.ForItem(Iron), Wood(1) };
            Assert.Throws<ArgumentException>(() =>
                TagConsumptionPlanner.Plan(specs, inv, new[] { new IngredientChoice(0, "pino") }));
            Assert.Throws<ArgumentException>(() =>
                TagConsumptionPlanner.Plan(specs, inv, new[] { new IngredientChoice(1, "lingote_hierro") }));
        }

        [Test]
        public void ConcreteItemsAreReservedBeforeTags()
        {
            var inv = With(4, (Pine, 3), (Oak, 2));
            var specs = new[] { Wood(3), IngredientSpec.ForItem(Pine, 2) };
            var plan = TagConsumptionPlanner.Plan(specs, inv);
            Assert.That(plan.IsComplete, Is.True);
            Assert.That(plan.Entries[0].Item, Is.SameAs(Pine));
            Assert.That(plan.Entries[0].Quantity, Is.EqualTo(3));
            Assert.That(plan.Entries[1].Item, Is.SameAs(Oak));
        }

        [Test]
        public void AverageTierIsWeightedByWhatIsSpent()
        {
            var inv = With(4, (Pine, 3), (Oak, 5));
            var plan = TagConsumptionPlanner.Plan(new[] { Wood(4) }, inv);
            Assert.That(plan.AverageTier, Is.EqualTo((3 * 1 + 1 * 3) / 4f).Within(1e-5));
            Assert.That(ConsumptionPlan.Missing.AverageTier, Is.EqualTo(0f));
        }

        [Test]
        public void RejectsNullArguments()
        {
            var inv = With(1);
            Assert.Throws<ArgumentNullException>(() => TagConsumptionPlanner.Plan(null, inv));
            Assert.Throws<ArgumentNullException>(() => TagConsumptionPlanner.Plan(new[] { Wood(1) }, null));
            Assert.Throws<ArgumentException>(() => TagConsumptionPlanner.Plan(new IngredientSpec[] { null }, inv));
        }
    }
}
