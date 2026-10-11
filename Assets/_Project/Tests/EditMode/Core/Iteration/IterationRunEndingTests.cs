using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Iteration;
using Specimen.Core.Iteration.Ending;
using Specimen.Core.Iteration.Presence;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationRunEndingTests : IterationRunFixture
    {
        [Test]
        public void CompletingExtraction_IsVictory_WithFatesPublished()
        {
            EnterFirstCalm();
            PlayFloor(); PlayFloor();
            Run.CompleteSiege(); Run.Tick(0f);
            Run.CompleteExtraction(); Run.Tick(0f);

            Assert.That(Run.Outcome, Is.EqualTo(IterationOutcome.Victory));
            var ended = Events.OfType<IterationEnded>().Single();
            Assert.That((ended.Outcome, ended.Floor), Is.EqualTo((IterationOutcome.Victory, 2)));
            Assert.That(ended.Fates.Values, Is.All.EqualTo(PlayerFate.Extracted));
        }

        [Test]
        public void EliminatingEveryone_WipesTheGroup_FromAnyPhase()
        {
            EnterFirstCalm();
            Advance(Calm);
            Run.EliminatePlayer(1);
            Run.Tick(0f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Incursion), "Queda un jugador");

            Run.EliminatePlayer(2);
            Run.Tick(0f);
            Assert.That((Run.Phase, Run.Outcome), Is.EqualTo((IterationPhase.Ended, (IterationOutcome?)IterationOutcome.Wiped)));
        }

        [Test]
        public void Abandon_EndsWithAbandonedFates()
        {
            EnterFirstCalm();
            Run.EliminatePlayer(2);
            Run.Abandon();
            Run.Tick(0f);
            Assert.That(Run.Outcome, Is.EqualTo(IterationOutcome.Abandoned));
            Assert.That((Run.Fates[1], Run.Fates[2]), Is.EqualTo((PlayerFate.Abandoned, PlayerFate.Eliminated)));
        }

        [Test]
        public void EliminatedPlayers_DoNotBlockReadyCheck()
        {
            EnterFirstCalm();
            Run.EliminatePlayer(1);
            Run.MarkReady(2);
            Run.Tick(0f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Incursion));
        }

        [Test]
        public void TrappedOnRooftop_CanStillBeRescuedDuringSiege()
        {
            EnterFirstCalm();
            PlayFloor();
            Advance(Calm);
            Run.LeaveElevator(1);
            Advance(Incursion); Run.Tick(0f);
            Assert.That((Run.Phase, Run.Location(1)), Is.EqualTo((IterationPhase.Siege, PlayerLocation.Trapped)));
            Run.ReachEmergencyShaft(1);
            Assert.That(Run.Location(1), Is.EqualTo(PlayerLocation.InElevator));
        }

        [Test]
        public void Outcome_IsNullWhilePlaying_AndSignalsFailAfterEnd()
        {
            EnterFirstCalm();
            Assert.That(Run.Outcome, Is.Null);
            Run.Abandon(); Run.Tick(0f);
            Assert.Throws<InvalidOperationException>(Run.Abandon);
            Assert.Throws<InvalidOperationException>(() => Run.EliminatePlayer(1));
        }
    }
}
