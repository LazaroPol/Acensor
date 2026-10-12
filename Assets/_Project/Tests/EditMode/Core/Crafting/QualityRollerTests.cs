using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class QualityRollerTests
    {
        private static QualityRoller Roller(ulong seed) => new QualityRoller(QualityRules.Default, new Pcg32Random(seed));

        private static Quality[] Rolls(ulong seed, QualityRollInput input, int n = 200)
        {
            var roller = Roller(seed);
            return Enumerable.Range(0, n).Select(_ => roller.Roll(input)).ToArray();
        }

        [Test]
        public void SameSeedGivesSameSequence()
        {
            var input = new QualityRollInput(8, 2f, 2, 0, 6);
            Assert.That(Rolls(42, input, 50), Is.EqualTo(Rolls(42, input, 50)));
        }

        [Test]
        public void NoviceSmithGetsCrudeOrCommon()
        {
            // 1 + 3 + 2 = 6, aleatorio [-2, +3] → 4..9
            var all = Rolls(7, new QualityRollInput(1, 1f, 1, 0, 3)).Distinct().ToArray();
            Assert.That(all, Is.SubsetOf(new[] { Quality.Crude, Quality.Common }));
            Assert.That(all, Has.Length.EqualTo(2));
        }

        [Test]
        public void MasterReachesLegendaryWhenCapsAllow()
        {
            var max = ProfessionMilestones.Default.For(20).MaxQuality;
            // 20 + 12 + 6 + 3 = 41 → mínimo 39 ≥ 37
            var all = Rolls(3, new QualityRollInput(20, 4f, 3, 3, max)).Distinct();
            Assert.That(all, Is.EquivalentTo(new[] { Quality.Legendary }));
        }

        [Test]
        public void MilestoneCapLimitsQuality()
        {
            var max = ProfessionMilestones.Default.For(1).MaxQuality;
            var all = Rolls(5, new QualityRollInput(1, 4f, 3, 20, max)).Distinct();
            Assert.That(all, Is.EquivalentTo(new[] { Quality.Good }));
        }

        [Test]
        public void RecipeCapLimitsQuality()
        {
            var all = Rolls(9, new QualityRollInput(20, 4f, 3, 3, 6, Quality.Fine)).Distinct();
            Assert.That(all, Is.EquivalentTo(new[] { Quality.Fine }));
        }
    }
}
