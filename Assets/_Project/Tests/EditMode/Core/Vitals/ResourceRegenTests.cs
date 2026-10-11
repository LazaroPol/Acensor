using NUnit.Framework;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Vitals
{
    [TestFixture]
    public class ResourceRegenTests
    {
        [Test]
        public void Regenerates_AtRatePerSecond_UpToMax()
        {
            var pool = new ResourcePool(new Stat(10f), startFull: false);
            var regen = new ResourceRegen(pool, perSecond: 4f);
            regen.Tick(1f);
            Assert.That(pool.Current, Is.EqualTo(4f));
            regen.Tick(5f);
            Assert.That(pool.Current, Is.EqualTo(10f));
        }

        [Test]
        public void WaitsDelayAfterSpending()
        {
            var pool = new ResourcePool(new Stat(10f));
            var regen = new ResourceRegen(pool, perSecond: 2f, delaySeconds: 1f);
            pool.Drain(6f);
            regen.Tick(0.5f);
            Assert.That(pool.Current, Is.EqualTo(4f));
            regen.Tick(1f);
            Assert.That(pool.Current, Is.EqualTo(5f).Within(1e-5f));
        }

        [Test]
        public void SpendingAgain_RestartsDelay()
        {
            var pool = new ResourcePool(new Stat(10f));
            var regen = new ResourceRegen(pool, perSecond: 2f, delaySeconds: 1f);
            pool.Drain(5f);
            regen.Tick(0.9f);
            pool.Drain(1f);
            regen.Tick(0.9f);
            Assert.That(pool.Current, Is.EqualTo(4f));
        }

        [Test]
        public void Paused_DoesNotRegenerate()
        {
            var pool = new ResourcePool(new Stat(10f), startFull: false);
            var regen = new ResourceRegen(pool, perSecond: 3f) { Paused = true };
            regen.Tick(2f);
            Assert.That(pool.Current, Is.EqualTo(0f));
        }
    }
}
