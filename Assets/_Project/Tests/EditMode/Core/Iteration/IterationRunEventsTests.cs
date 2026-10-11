using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationRunEventsTests : IterationRunFixture
    {
        [Test]
        public void PublishesFloorReached_ForEachFloor()
        {
            Run.Start(); Run.LeaveArmory(); Run.Tick(0f);
            PlayFloor();
            Assert.That(Events.OfType<FloorReached>().Select(e => e.Floor), Is.EqualTo(new[] { 1, 2 }));
        }

        [Test]
        public void PublishesDoorWarnings_WithFloor_InOrder()
        {
            Run.Start(); Run.LeaveArmory(); Run.Tick(0f);
            Advance(Calm);
            Advance(Incursion);
            var warnings = Events.OfType<DoorsClosingWarning>().Select(w => (w.Floor, w.SecondsLeft));
            Assert.That(warnings, Is.EqualTo(new[] { (1, 20f), (1, 5f) }));
        }

        [Test]
        public void PublishesPhaseChanges_InOrder()
        {
            Run.Start(); Run.LeaveArmory(); Run.Tick(0f);
            Advance(Calm);
            var changes = Events.OfType<IterationPhaseChanged>().Select(c => (c.From, c.To, c.Floor));
            Assert.That(changes, Is.EqualTo(new[]
            {
                (IterationPhase.Armory, IterationPhase.Calm, 1),
                (IterationPhase.Calm, IterationPhase.Incursion, 1)
            }));
        }

        [Test]
        public void Signals_OutsideTheirPhase_AreRejected()
        {
            Assert.Throws<InvalidOperationException>(Run.LeaveArmory, "Antes de Start");
            Run.Start();
            Assert.Throws<InvalidOperationException>(Run.CompleteSiege);
            Assert.Throws<InvalidOperationException>(Run.CompleteExtraction);
            Run.LeaveArmory(); Run.Tick(0f);
            Assert.Throws<InvalidOperationException>(Run.LeaveArmory);
        }
    }
}
