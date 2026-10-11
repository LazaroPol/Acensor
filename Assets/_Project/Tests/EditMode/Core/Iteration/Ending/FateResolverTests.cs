using NUnit.Framework;
using Specimen.Core.Iteration.Ending;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Tests.Core.Iteration.Ending
{
    public class FateResolverTests
    {
        [TestCase(IterationOutcome.Victory, PlayerLocation.InElevator, PlayerFate.Extracted)]
        [TestCase(IterationOutcome.Victory, PlayerLocation.Trapped, PlayerFate.LeftBehind)]
        [TestCase(IterationOutcome.Victory, PlayerLocation.InFloor, PlayerFate.LeftBehind)]
        [TestCase(IterationOutcome.Victory, PlayerLocation.Eliminated, PlayerFate.Eliminated)]
        [TestCase(IterationOutcome.Abandoned, PlayerLocation.InElevator, PlayerFate.Abandoned)]
        [TestCase(IterationOutcome.Abandoned, PlayerLocation.Eliminated, PlayerFate.Eliminated)]
        [TestCase(IterationOutcome.Wiped, PlayerLocation.Eliminated, PlayerFate.Eliminated)]
        public void FateOf_CombinesOutcomeAndLocation(IterationOutcome outcome, PlayerLocation loc, PlayerFate fate) =>
            Assert.That(FateResolver.FateOf(outcome, loc), Is.EqualTo(fate));

        [Test]
        public void Resolve_ReturnsAFateForEveryPlayer()
        {
            var presence = new PresenceTracker(new[] { 1, 2, 3 });
            presence.LeaveElevator(2);
            presence.TrapThoseOutside();
            presence.Eliminate(3);

            var fates = FateResolver.Resolve(IterationOutcome.Victory, presence);

            Assert.That(fates[1], Is.EqualTo(PlayerFate.Extracted));
            Assert.That(fates[2], Is.EqualTo(PlayerFate.LeftBehind));
            Assert.That(fates[3], Is.EqualTo(PlayerFate.Eliminated));
        }
    }
}
