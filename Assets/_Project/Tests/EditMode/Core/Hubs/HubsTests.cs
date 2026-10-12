using System.Linq;
using NUnit.Framework;
using Specimen.Core.Armory;
using Specimen.Core.Classes;
using Specimen.Core.Composition;
using Specimen.Core.Elevator;
using Specimen.Core.Hubs;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Hubs
{
    public class HubsTests
    {
        private sealed class NoLoader : ISceneLoader { public void Load(SceneId scene) { } }

        private ServiceContainer _services;
        private IterationRun _run;
        private ArmoryPresenter _armory;
        private ElevatorPresenter _elevator;

        [SetUp]
        public void SetUp()
        {
            _services = GameComposition.Build(new NoLoader());
            _run = _services.Resolve<IterationRun>();
            _armory = new ArmoryPresenter(_services.Resolve<ClassKitArmory>(), _services.Resolve<IClassKitCatalog>(),
                _run, GameComposition.LocalPlayerId);
            _elevator = new ElevatorPresenter(_services.Resolve<ElevatorPlatform>(), _run, GameComposition.LocalPlayerId);
            _run.Start();
        }

        [Test]
        public void Choose_AssignsKitAndSwitchingReleasesThePrevious()
        {
            var kits = _armory.Entries().Select(e => e.Id).ToList();
            Assert.That(_armory.Choose(kits[0]), Is.True);
            Assert.That(_armory.Entries()[0].Availability, Is.EqualTo(KitAvailability.Mine));
            Assert.That(_armory.Choose(kits[1]), Is.True);
            Assert.That(_armory.Entries()[0].Availability, Is.EqualTo(KitAvailability.Free));
            Assert.That(_armory.Entries()[1].Availability, Is.EqualTo(KitAvailability.Mine));
        }

        [Test]
        public void Leave_RequiresKitAndMovesToCalm()
        {
            Assert.That(_armory.Leave(), Is.False);
            Assert.That(_armory.Message, Is.EqualTo("Coge un kit antes de salir."));
            _armory.Choose(_armory.Entries()[0].Id);
            Assert.That(_armory.CanLeave, Is.True);
            Assert.That(_armory.Leave(), Is.True);
            _run.Tick(0.1f);
            Assert.That(_run.Phase, Is.EqualTo(IterationPhase.Calm));
        }

        [Test]
        public void Elevator_ReadyOnlyInCalm_AndToggles()
        {
            Assert.That(_elevator.ToggleReady(), Is.False);
            _armory.Choose(_armory.Entries()[0].Id);
            _armory.Leave();
            _run.Tick(0.1f);
            Assert.That(_elevator.IsCalm, Is.True);
            Assert.That(_elevator.ToggleReady(), Is.True);
            Assert.That(_elevator.IsReady, Is.True);
            Assert.That(_elevator.ToggleReady(), Is.False);
        }

        [Test]
        public void Elevator_ListsFourEmptySlots()
        {
            var labels = _elevator.SlotLabels();
            Assert.That(labels.Count, Is.EqualTo(4));
            Assert.That(labels[0], Is.EqualTo("Ranura 1: vacía"));
        }
    }
}
