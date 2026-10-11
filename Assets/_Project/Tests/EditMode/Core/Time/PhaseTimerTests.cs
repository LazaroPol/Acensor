using System;
using NUnit.Framework;
using Specimen.Core.Time;

namespace Specimen.Tests.Core.Time
{
    public class PhaseTimerTests
    {
        [Test]
        public void StartsStopped_WithFullTime()
        {
            var t = new PhaseTimer(10f);
            Assert.That((t.IsRunning, t.IsExpired, t.Remaining, t.Elapsed), Is.EqualTo((false, false, 10f, 0f)));
        }

        [Test]
        public void DoesNotCount_UntilStarted_OrWhilePaused()
        {
            var t = new PhaseTimer(10f);
            t.Tick(3f);
            t.Start(); t.Tick(2f);
            t.Pause(); t.Tick(5f);
            Assert.That(t.Remaining, Is.EqualTo(8f));
        }

        [Test]
        public void Expires_Once_AndClampsAtZero()
        {
            var t = new PhaseTimer(5f);
            int expired = 0;
            t.Expired += () => expired++;
            t.Start();
            t.Tick(7f);
            t.Tick(1f);
            Assert.That((t.Remaining, t.IsExpired, t.IsRunning, expired), Is.EqualTo((0f, true, false, 1)));
        }

        [Test]
        public void CannotRestart_AfterExpiry()
        {
            var t = new PhaseTimer(1f);
            t.Start(); t.Tick(1f);
            Assert.Throws<InvalidOperationException>(t.Start);
        }

        [Test]
        public void ShortenTo_ReducesButNeverExtends()
        {
            var t = new PhaseTimer(100f);
            t.Start();
            t.ShortenTo(150f);
            Assert.That(t.Remaining, Is.EqualTo(100f));
            t.ShortenTo(20f);
            Assert.That(t.Remaining, Is.EqualTo(20f));
        }

        [Test]
        public void ShortenToZero_ExpiresImmediately_WhenRunning()
        {
            var t = new PhaseTimer(30f);
            bool expired = false;
            t.Expired += () => expired = true;
            t.Start();
            t.ShortenTo(0f);
            Assert.That(expired, Is.True);
        }

        [Test]
        public void InvalidArguments_AreRejected()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PhaseTimer(0f));
            var t = new PhaseTimer(5f);
            Assert.Throws<ArgumentOutOfRangeException>(() => t.Tick(-1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => t.ShortenTo(-1f));
        }
    }
}
