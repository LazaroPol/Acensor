using NUnit.Framework;
using Specimen.Core.Prototype;
using Specimen.Core.Stats;
using Specimen.Core.Time;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Prototype
{
    [TestFixture]
    public class DebugHudPresenterTests
    {
        private CharacterVitals _vitals;
        private PhaseTimer _timer;
        private DebugHudPresenter _hud;

        [SetUp]
        public void SetUp()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 50f);
            sheet.Define(StatType.MaxStamina, 80f);
            _vitals = new CharacterVitals(sheet);
            _timer = new PhaseTimer(300f);
            _hud = new DebugHudPresenter(_vitals, _timer, "Calma");
        }

        [Test]
        public void ShowsEachResourceAsCurrentOverMax()
        {
            _vitals.ClassResource.Drain(20f);
            _vitals.Stamina.Drain(30.6f);
            var lines = _hud.Lines();
            Assert.That(lines[0], Is.EqualTo("Vida 100/100"));
            Assert.That(lines[1], Is.EqualTo("Recurso 30/50"));
            Assert.That(lines[2], Is.EqualTo("Aguante 49/80"));
        }

        [Test]
        public void ShowsPhaseWithRemainingTime()
        {
            _timer.Start();
            _timer.Tick(59.5f);
            Assert.That(_hud.Lines()[3], Is.EqualTo("Fase Calma 04:01"));
        }

        [Test]
        public void MarksSpectreWhenHealthIsEmpty()
        {
            _vitals.Health.Empty();
            Assert.That(_hud.Lines()[0], Is.EqualTo("Vida 0/100 " + DebugHudPresenter.SpectreLabel));
        }

        [Test]
        public void MarksExpiredPhase()
        {
            _timer.Start();
            _timer.Tick(300f);
            Assert.That(_hud.Lines()[3], Is.EqualTo("Fase Calma " + DebugHudPresenter.ExpiredLabel));
        }

        [Test]
        public void TextJoinsLinesWithNewlines()
        {
            Assert.That(_hud.Text.Split('\n'), Has.Length.EqualTo(4));
            Assert.That(_hud.Text, Does.StartWith("Vida 100/100\nRecurso 50/50"));
        }

        [Test]
        public void RejectsMissingDependencies()
        {
            Assert.That(() => new DebugHudPresenter(null, _timer, "x"), Throws.ArgumentNullException);
            Assert.That(() => new DebugHudPresenter(_vitals, null, "x"), Throws.ArgumentNullException);
            Assert.That(() => new DebugHudPresenter(_vitals, _timer, null), Throws.ArgumentNullException);
        }
    }
}
