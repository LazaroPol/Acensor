using NUnit.Framework;
using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Elevator;
using Specimen.Core.Hubs;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Hubs
{
    public class ElevatorExitTests
    {
        private sealed class NoLoader : ISceneLoader { public void Load(SceneId scene) { } }

        private IterationRun _run;
        private ElevatorPresenter _elevator;

        [SetUp]
        public void SetUp()
        {
            var services = GameComposition.Build(new NoLoader());
            _run = services.Resolve<IterationRun>();
            var armory = new ArmoryPresenter(services.Resolve<ClassKitArmory>(), services.Resolve<IClassKitCatalog>(),
                _run, GameComposition.LocalPlayerId);
            _elevator = new ElevatorPresenter(services.Resolve<ElevatorPlatform>(), _run, GameComposition.LocalPlayerId);
            _run.Start();
            armory.Choose(armory.Entries()[0].Id);
            armory.Leave();
            _run.Tick(0.1f);
        }

        [Test]
        public void CanExit_FalseDuringCalm()
        {
            Assert.That(_elevator.CanExit, Is.False);
        }

        [Test]
        public void CanExit_TrueInIncursionUntilThePlayerLeaves()
        {
            _elevator.ToggleReady();
            _run.Tick(1f);
            Assert.That(_run.Phase, Is.EqualTo(IterationPhase.Incursion));
            Assert.That(_elevator.CanExit, Is.True);
            _run.LeaveElevator(GameComposition.LocalPlayerId);
            Assert.That(_elevator.CanExit, Is.False);
        }
    }
}
