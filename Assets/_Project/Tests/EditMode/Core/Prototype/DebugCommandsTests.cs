using NUnit.Framework;
using Specimen.Core.Prototype;
using Specimen.Core.Stats;
using Specimen.Core.Time;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Prototype
{
    [TestFixture]
    public class DebugCommandsTests
    {
        private CharacterVitals _vitals;
        private PhaseTimer _timer;
        private DebugCommands _commands;

        [SetUp]
        public void SetUp()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 25f);
            sheet.Define(StatType.MaxClassResource, 10f);
            sheet.Define(StatType.MaxStamina, 10f);
            _vitals = new CharacterVitals(sheet);
            _timer = new PhaseTimer(60f);
            _commands = new DebugCommands(_vitals, _timer, damage: 10f, heal: 5f, timeStep: 30f);
        }

        [Test]
        public void HurtDrainsFixedAmountWithoutGoingBelowZero()
        {
            Assert.That(_commands.Hurt(), Is.EqualTo(10f));
            _commands.Hurt();
            Assert.That(_commands.Hurt(), Is.EqualTo(5f));
            Assert.That(_vitals.IsAlive, Is.False);
        }

        [Test]
        public void CureRestoresFixedAmountWithoutExceedingMax()
        {
            _vitals.Health.Drain(7f);
            Assert.That(_commands.Cure(), Is.EqualTo(5f));
            Assert.That(_commands.Cure(), Is.EqualTo(2f));
            Assert.That(_vitals.Health.IsFull, Is.True);
        }

        [Test]
        public void AdvanceTimeShortensPhaseAndExpiresIt()
        {
            bool expired = false;
            _timer.Expired += () => expired = true;
            _timer.Start();
            _commands.AdvanceTime();
            Assert.That(_timer.Remaining, Is.EqualTo(30f));
            _commands.AdvanceTime();
            Assert.That(_timer.IsExpired, Is.True);
            Assert.That(expired, Is.True);
        }

        [Test]
        public void RejectsNonPositiveAmounts()
        {
            Assert.That(() => new DebugCommands(_vitals, _timer, damage: 0f), Throws.TypeOf<System.ArgumentOutOfRangeException>());
            Assert.That(() => new DebugCommands(_vitals, _timer, heal: -1f), Throws.TypeOf<System.ArgumentOutOfRangeException>());
            Assert.That(() => new DebugCommands(_vitals, _timer, timeStep: 0f), Throws.TypeOf<System.ArgumentOutOfRangeException>());
        }
    }
}
