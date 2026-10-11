using NUnit.Framework;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationRunFlowTests : IterationRunFixture
    {
        [Test]
        public void Start_BeginsInArmory_OnFloorZero_WithoutTimer()
        {
            Run.Start();
            Assert.That((Run.Phase, Run.Floor, Run.PhaseRemaining), Is.EqualTo((IterationPhase.Armory, 0, (float?)null)));
        }

        [Test]
        public void Armory_WaitsForSignal_ThenCalmOnFirstFloor()
        {
            Run.Start();
            Advance(999f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Armory));
            Run.LeaveArmory();
            Run.Tick(0f);
            Assert.That((Run.Phase, Run.Floor, Run.PhaseRemaining), Is.EqualTo((IterationPhase.Calm, 1, (float?)Calm)));
        }

        [Test]
        public void Calm_ThenIncursion_WhenTimerExpires()
        {
            Run.Start(); Run.LeaveArmory(); Run.Tick(0f);
            Advance(Calm - 1f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Calm));
            Advance(1f);
            Assert.That((Run.Phase, Run.PhaseRemaining), Is.EqualTo((IterationPhase.Incursion, (float?)Incursion)));
        }

        [Test]
        public void AfterIncursion_AscendsToNextFloorCalm()
        {
            Run.Start(); Run.LeaveArmory(); Run.Tick(0f);
            PlayFloor();
            Assert.That((Run.Phase, Run.Floor), Is.EqualTo((IterationPhase.Calm, 2)));
        }

        [Test]
        public void AfterLastFloor_GoesToSiege_ThenExtraction_ThenEnded()
        {
            Run.Start(); Run.LeaveArmory(); Run.Tick(0f);
            PlayFloor();
            PlayFloor();
            Assert.That((Run.Phase, Run.Floor), Is.EqualTo((IterationPhase.Siege, 2)));

            Run.CompleteSiege(); Run.Tick(0f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Extraction));
            Run.CompleteExtraction(); Run.Tick(0f);
            Assert.That(Run.Phase, Is.EqualTo(IterationPhase.Ended));
        }
    }
}
