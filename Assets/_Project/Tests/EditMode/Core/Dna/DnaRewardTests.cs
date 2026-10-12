using NUnit.Framework;
using Specimen.Core.Dna;
using Specimen.Core.Iteration.Ending;

namespace Specimen.Tests.Core.Dna
{
    public class DnaRewardTests
    {
        [TestCase(IterationOutcome.Victory, 1, 0, 13)]
        [TestCase(IterationOutcome.Victory, 1, 4, 21)]
        [TestCase(IterationOutcome.Victory, 0, 0, 10)]
        [TestCase(IterationOutcome.Wiped, 2, 9, 4)]
        [TestCase(IterationOutcome.Abandoned, 1, 3, 2)]
        [TestCase(IterationOutcome.Abandoned, -3, -1, 0)]
        public void Points_FollowOutcomeFloorsAndLoot(IterationOutcome outcome, int floors, int loot, int expected)
        {
            Assert.That(DnaReward.For(outcome, floors, loot), Is.EqualTo(expected));
        }
    }
}
