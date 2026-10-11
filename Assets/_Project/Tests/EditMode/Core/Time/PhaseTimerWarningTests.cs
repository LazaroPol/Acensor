using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Time;

namespace Specimen.Tests.Core.Time
{
    public class PhaseTimerWarningTests
    {
        private PhaseTimer _timer;
        private List<float> _warnings;

        [SetUp]
        public void SetUp()
        {
            _timer = new PhaseTimer(420f, new[] { 15f, 60f });
            _warnings = new List<float>();
            _timer.WarningReached += _warnings.Add;
            _timer.Start();
        }

        [Test]
        public void WarningFires_WhenRemainingReachesThreshold()
        {
            _timer.Tick(359f);
            Assert.That(_warnings, Is.Empty);
            _timer.Tick(1f);
            Assert.That(_warnings, Is.EqualTo(new[] { 60f }));
        }

        [Test]
        public void EachWarning_FiresOnlyOnce()
        {
            for (int i = 0; i < 420; i++) _timer.Tick(1f);
            Assert.That(_warnings, Is.EqualTo(new[] { 60f, 15f }));
        }

        [Test]
        public void BigTick_FiresSkippedWarningsInOrder_BeforeExpiry()
        {
            var order = new List<string>();
            _timer.WarningReached += w => order.Add("warn" + w);
            _timer.Expired += () => order.Add("expired");
            _timer.Tick(1000f);
            Assert.That(order, Is.EqualTo(new[] { "warn60", "warn15", "expired" }));
        }

        [Test]
        public void Shorten_FiresWarningsThatAreNowPassed()
        {
            _timer.ShortenTo(15f);
            Assert.That(_warnings, Is.EqualTo(new[] { 60f, 15f }));
        }

        [Test]
        public void ThresholdsOutsideDuration_AreIgnored()
        {
            var t = new PhaseTimer(30f, new[] { 0f, 45f, 10f, 10f });
            var fired = new List<float>();
            t.WarningReached += fired.Add;
            t.Start();
            t.Tick(30f);
            Assert.That(fired, Is.EqualTo(new[] { 10f }));
        }
    }
}
