using NUnit.Framework;
using Specimen.Core.Bestiary;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Sandbox;

namespace Specimen.Tests.Core.Sandbox
{
    [TestFixture]
    public class RespawnTests
    {
        [Test]
        public void FiresOnceWhenTheDelayElapses()
        {
            var timer = new RespawnTimer(2f);
            Assert.That(timer.Tick(5f), Is.False, "sin armar no hace nada");
            timer.Begin();
            Assert.That(timer.Tick(1.5f), Is.False);
            Assert.That(timer.Remaining, Is.EqualTo(0.5f).Within(1e-5f));
            Assert.That(timer.Tick(0.6f), Is.True);
            Assert.That(timer.Tick(1f), Is.False);
            Assert.That(timer.IsRunning, Is.False);
        }

        [Test]
        public void CancelStopsTheCountdown()
        {
            var timer = new RespawnTimer(1f);
            timer.Begin();
            timer.Cancel();
            Assert.That(timer.Tick(2f), Is.False);
        }

        [Test]
        public void BossTakesLongerToComeBack()
        {
            Assert.That(SandboxRespawn.DelayFor(DefaultBestiary.Find(ActOneBoss.Id)), Is.EqualTo(SandboxRespawn.Boss));
            Assert.That(SandboxRespawn.DelayFor(DefaultBestiary.Find("puglin")), Is.EqualTo(SandboxRespawn.Common));
            Assert.That(SandboxRespawn.Boss, Is.GreaterThan(SandboxRespawn.Common));
        }
    }
}
