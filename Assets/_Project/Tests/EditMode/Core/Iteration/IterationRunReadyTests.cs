using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Iteration;
using Specimen.Core.Iteration.Readiness;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationRunReadyTests : IterationRunFixture
    {
        [Test]
        public void AllReady_EndsCalmEarly_AndAwardsHaste()
        {
            EnterFirstCalm();
            Run.Tick(4f);
            Run.MarkReady(1); Run.MarkReady(2);
            Run.Tick(0f);

            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Incursion));
            var earned = Events.OfType<HasteBonusEarned>().Single();
            Assert.That((earned.Floor, earned.SavedSeconds), Is.EqualTo((1, 6f)));
            Assert.That(Run.HasteBonus(1), Is.EqualTo(0.25f * 6f / Calm).Within(1e-6));
        }

        [Test]
        public void PartialReady_DoesNotEndCalm_NorAwardHaste()
        {
            EnterFirstCalm();
            Run.MarkReady(1);
            Advance(Calm);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Incursion));
            Assert.That(Events.OfType<HasteBonusEarned>(), Is.Empty);
            Assert.That(Run.HasteBonus(1), Is.Zero);
        }

        [Test]
        public void ReadyFlags_ResetOnEveryNewFloor()
        {
            EnterFirstCalm();
            Run.MarkReady(1); Run.MarkReady(2); Run.Tick(0f);
            Advance(Incursion); Run.Tick(0f);
            Assert.That((Run.Phase, Run.Floor), Is.EqualTo((IterationPhase.Calm, 2)));
            Assert.That(Run.IsReady(1) || Run.IsReady(2), Is.False);
        }

        [Test]
        public void Unmark_PreventsEarlyStart()
        {
            EnterFirstCalm();
            Run.MarkReady(1); Run.MarkReady(2); Run.UnmarkReady(2);
            Run.Tick(0f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Calm));
        }

        [Test]
        public void Ready_OutsideCalm_IsRejected()
        {
            Run.Start();
            Assert.Throws<InvalidOperationException>(() => Run.MarkReady(1));
        }

        [Test]
        public void TooManyPlayers_ForConfig_IsRejected()
        {
            var config = new IterationConfigBuilder().WithMaxPlayers(1).Build();
            Assert.Throws<ArgumentException>(() => new IterationRun(config, Events, new[] { 1, 2 }));
        }
    }
}
