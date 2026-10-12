using NUnit.Framework;
using Specimen.Core.Composition;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Composition
{
    public class DemoConfigTests
    {
        private sealed class NoLoader : ISceneLoader { public void Load(SceneId scene) { } }

        [Test]
        public void Demo_HasASingleFloorThatLeadsStraightToTheSiege()
        {
            var run = GameComposition.Build(new NoLoader()).Resolve<IterationRun>();
            run.Start();
            run.LeaveArmory();
            run.Tick(0.1f);
            run.MarkReady(GameComposition.LocalPlayerId);
            run.Tick(0.1f);
            Assert.That(run.Phase, Is.EqualTo(IterationPhase.Incursion));
            Assert.That(run.Floor, Is.EqualTo(1));
            run.Tick(GameComposition.DemoConfig.IncursionSeconds + 1f);
            for (int i = 0; i < 3; i++) run.Tick(0.1f);
            Assert.That(run.Phase, Is.EqualTo(IterationPhase.Siege));
        }

        [Test]
        public void Demo_TrappedPlayerCanAbandonThroughTheShaft()
        {
            var run = GameComposition.Build(new NoLoader()).Resolve<IterationRun>();
            run.Start();
            run.LeaveArmory();
            run.Tick(0.1f);
            run.MarkReady(GameComposition.LocalPlayerId);
            run.Tick(0.1f);
            run.LeaveElevator(GameComposition.LocalPlayerId);
            run.Tick(GameComposition.DemoConfig.IncursionSeconds + 1f);
            run.Tick(0.1f);
            Assert.That(run.Location(GameComposition.LocalPlayerId), Is.EqualTo(Specimen.Core.Iteration.Presence.PlayerLocation.Trapped));
            run.Abandon();
            run.Tick(0.1f);
            Assert.That(run.Phase, Is.EqualTo(IterationPhase.Ended));
            Assert.That(run.Outcome, Is.EqualTo(Specimen.Core.Iteration.Ending.IterationOutcome.Abandoned));
        }
    }
}
