using System;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Stats;
using Specimen.Core.Talents;

namespace Specimen.Tests.Core.Talents
{
    [TestFixture]
    public class TalentTests
    {
        private static readonly TalentTree Tree = KitTalentTrees.For(ClassKitId.Bastion);

        [Test]
        public void EveryKitHasThreeBranchesOfFiveTiers()
        {
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
            {
                var tree = KitTalentTrees.For(kit);
                Assert.That(tree.Branches.Count, Is.EqualTo(3), kit.ToString());
                foreach (var branch in tree.Branches)
                    for (int tier = 0; tier < TalentDefinition.Tiers; tier++)
                        Assert.That(branch.InTier(tier), Is.Not.Empty, $"{kit} {branch.Name} fila {tier}");
            }
        }

        [Test]
        public void TiersNeedFivePointsInTheBranch()
        {
            var alloc = new TalentAllocation(Tree);
            string tier0 = Tree.Branches[0].Talents[0].Id, tier1 = Tree.Branches[0].Talents[1].Id;
            Assert.That(alloc.CanSpend(tier1, 10), Is.False);
            for (int i = 0; i < 5; i++) Assert.That(alloc.TrySpend(tier0, 10), Is.True);
            Assert.That(alloc.TrySpend(tier0, 10), Is.False, "rango máximo");
            Assert.That(alloc.TrySpend(tier1, 10), Is.True);
            Assert.That(alloc.PointsIn(0), Is.EqualTo(6));
        }

        [Test]
        public void CannotSpendMorePointsThanAvailable()
        {
            var alloc = new TalentAllocation(Tree);
            string id = Tree.Branches[1].Talents[0].Id;
            Assert.That(alloc.TrySpend(id, 1), Is.True);
            Assert.That(alloc.TrySpend(id, 1), Is.False);
            alloc.Reset();
            Assert.That(alloc.TotalSpent, Is.EqualTo(0));
        }

        [Test]
        public void BonusesModifyStatsAndCanBeReapplied()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.Armor, 100f);
            var alloc = new TalentAllocation(Tree);
            alloc.TrySpend(Tree.Branches[0].Talents[0].Id, 5);
            alloc.TrySpend(Tree.Branches[0].Talents[0].Id, 5);
            TalentBonuses.Apply(alloc, sheet);
            Assert.That(sheet.ValueOf(StatType.Armor), Is.EqualTo(108f).Within(0.01f));
            alloc.Reset();
            TalentBonuses.Apply(alloc, sheet);
            Assert.That(sheet.ValueOf(StatType.Armor), Is.EqualTo(100f).Within(0.01f));
        }

        [Test]
        public void SpellPowerAndHasteAreAggregated()
        {
            var alloc = new TalentAllocation(Tree);
            for (int i = 0; i < 5; i++) alloc.TrySpend(Tree.Branches[2].Talents[0].Id, 20);
            for (int i = 0; i < 5; i++) alloc.TrySpend(Tree.Branches[2].Talents[1].Id, 20);
            alloc.TrySpend(Tree.Branches[2].Talents[2].Id, 20);
            var bonuses = TalentBonuses.Apply(alloc, new StatSheet());
            Assert.That(bonuses.CooldownReduction, Is.EqualTo(0.05f).Within(1e-5f));
            Assert.That(Tree.Branches[0].Talents[0].Describe(2), Does.Contain("8 %"));
        }
    }
}
