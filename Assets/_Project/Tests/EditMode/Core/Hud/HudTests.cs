using NUnit.Framework;
using Specimen.Core.Events;
using Specimen.Core.Hud;
using Specimen.Core.Iteration;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Hud
{
    public class HudTests
    {
        private EventBus _bus;
        private HudNotices _notices;
        private IterationRun _run;
        private CharacterVitals _vitals;
        private GameHudPresenter _hud;

        [SetUp]
        public void SetUp()
        {
            _bus = new EventBus();
            _notices = new HudNotices(_bus);
            _run = new IterationRun(IterationConfigBuilder.Default, _bus, new[] { 1 });
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 50f);
            sheet.Define(StatType.MaxStamina, 80f);
            _vitals = new CharacterVitals(sheet);
            _hud = new GameHudPresenter(_run, _vitals, _notices);
        }

        [Test]
        public void Bars_ReflectVitalsAndFraction()
        {
            _vitals.Health.Drain(25f);
            Assert.That(_hud.Health.Label, Is.EqualTo("Vida"));
            Assert.That(_hud.Health.Fraction, Is.EqualTo(0.75f).Within(1e-4));
            Assert.That(_hud.ClassResource.Max, Is.EqualTo(50f));
            Assert.That(_hud.Stamina.Fraction, Is.EqualTo(1f));
        }

        [Test]
        public void PhaseAndTimer_FollowTheIteration()
        {
            _run.Start();
            Assert.That(_hud.PhaseText, Is.EqualTo("Armería"));
            _run.LeaveArmory();
            _run.Tick(0.1f);
            Assert.That(_hud.PhaseText, Is.EqualTo("Calma"));
            Assert.That(_hud.TimerText, Does.Match(@"^\d\d:\d\d$"));
            Assert.That(_hud.FloorText, Is.EqualTo("Piso 1"));
        }

        [Test]
        public void Notices_AreRaisedFromEventsAndExpire()
        {
            _bus.Publish(new DoorsClosingWarning(1, 14.2f));
            var active = _notices.Active();
            Assert.That(active.Count, Is.EqualTo(1));
            Assert.That(active[0].Text, Is.EqualTo("¡Las puertas se cierran en 15 s!"));
            Assert.That(active[0].Severity, Is.EqualTo(NoticeSeverity.Danger));
            _notices.Tick(HudNotices.DefaultLifetime + 0.1f);
            Assert.That(_notices.Active(), Is.Empty);
        }

        [Test]
        public void Notices_MostSevereFirst_ThenMostRecent()
        {
            _notices.Add("a", NoticeSeverity.Info);
            _notices.Add("b", NoticeSeverity.Danger);
            _notices.Add("c", NoticeSeverity.Info);
            var texts = new System.Collections.Generic.List<string>();
            foreach (var n in _notices.Active()) texts.Add(n.Text);
            Assert.That(texts, Is.EqualTo(new[] { "b", "c", "a" }));
        }

        [Test]
        public void Dispose_StopsListening()
        {
            _notices.Dispose();
            _bus.Publish(new FloorReached(2));
            Assert.That(_notices.Active(), Is.Empty);
        }
    }
}
