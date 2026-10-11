using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Iteration;
using Specimen.Core.Iteration.Voting;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationRunEarlyCloseTests : IterationRunFixture
    {
        private void EnterIncursion()
        {
            EnterFirstCalm();
            Advance(Calm);
        }

        [Test]
        public void Majority_ShortensIncursionToGrace_AndPublishes()
        {
            EnterIncursion();
            Run.VoteEarlyClose(1, true);
            Assert.That(Run.PhaseRemaining, Is.EqualTo(Incursion), "1 de 2 no es mayoría");
            Run.VoteEarlyClose(2, true);

            Assert.That(Run.PhaseRemaining, Is.EqualTo(15f));
            var approved = Events.OfType<EarlyCloseApproved>().Single();
            Assert.That((approved.Floor, approved.SecondsLeft), Is.EqualTo((1, 15f)));
        }

        [Test]
        public void ApprovedVote_DoesNotRetrigger_OnLaterVotes()
        {
            EnterIncursion();
            Run.VoteEarlyClose(1, true); Run.VoteEarlyClose(2, true);
            Run.Tick(10f);
            Run.VoteEarlyClose(2, false); Run.VoteEarlyClose(2, true);
            Assert.That(Run.PhaseRemaining, Is.EqualTo(5f));
            Assert.That(Events.OfType<EarlyCloseApproved>().Count(), Is.EqualTo(1));
        }

        [Test]
        public void VoteWhenLessThanGraceRemains_DoesNotExtendIncursion()
        {
            EnterIncursion();
            Run.Tick(Incursion - 3f);
            Run.VoteEarlyClose(1, true); Run.VoteEarlyClose(2, true);
            Assert.That(Run.PhaseRemaining, Is.EqualTo(3f));
        }

        [Test]
        public void Votes_ResetOnNextFloor()
        {
            EnterIncursion();
            Run.VoteEarlyClose(1, true);
            Advance(Incursion); Run.Tick(0f);
            Advance(Calm);
            Run.VoteEarlyClose(2, true);
            Assert.That((Run.Floor, Run.PhaseRemaining), Is.EqualTo((2, (float?)Incursion)));
        }

        [Test]
        public void Vote_OutsideIncursion_IsRejected()
        {
            EnterFirstCalm();
            Assert.Throws<InvalidOperationException>(() => Run.VoteEarlyClose(1, true));
        }
    }
}
