using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Iteration;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationRunTrappedTests : IterationRunFixture
    {
        private void TrapPlayerOneOnFirstFloor()
        {
            EnterFirstCalm();
            Advance(Calm);
            Run.LeaveElevator(1);
            Advance(Incursion);
            Run.Tick(0f);
        }

        [Test]
        public void PlayerOutsideWhenDoorsClose_IsTrapped_AndEventPublished()
        {
            TrapPlayerOneOnFirstFloor();
            Assert.That((Run.Floor, Run.Location(1), Run.Location(2)),
                Is.EqualTo((2, PlayerLocation.Trapped, PlayerLocation.InElevator)));
            var e = Events.OfType<PlayersTrapped>().Single();
            Assert.That((e.Floor, e.PlayerIds), Is.EqualTo((1, new[] { 1 })));
        }

        [Test]
        public void PlayerWhoReturnsInTime_IsNotTrapped()
        {
            EnterFirstCalm();
            Advance(Calm);
            Run.LeaveElevator(1);
            Run.ReturnToElevator(1);
            Advance(Incursion);
            Assert.That(Events.OfType<PlayersTrapped>(), Is.Empty);
        }

        [Test]
        public void TrappedPlayers_DoNotBlockReadyCheck()
        {
            TrapPlayerOneOnFirstFloor();
            Run.MarkReady(2);
            Run.Tick(0f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Incursion));
        }

        [Test]
        public void ReachingEmergencyShaft_RescuesAndPublishes()
        {
            TrapPlayerOneOnFirstFloor();
            Run.ReachEmergencyShaft(1);
            Assert.That(Run.Location(1), Is.EqualTo(PlayerLocation.InElevator));
            var e = Events.OfType<PlayerRescued>().Single();
            Assert.That((e.PlayerId, e.Floor), Is.EqualTo((1, 2)));
        }

        [Test]
        public void InvalidPresenceActions_AreRejected()
        {
            EnterFirstCalm();
            Assert.Throws<InvalidOperationException>(() => Run.LeaveElevator(1), "Puertas cerradas en la Calma");
            Assert.Throws<InvalidOperationException>(() => Run.ReachEmergencyShaft(1), "No está atrapado");
        }

        [Test]
        public void TrappedModifiers_MatchDesign() =>
            Assert.That((TrappedModifiers.EnemySpeedMultiplier, TrappedModifiers.EnemyVisionMultiplier), Is.EqualTo((1.5f, 2f)));
    }
}
