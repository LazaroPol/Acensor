using System;
using NUnit.Framework;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Vitals
{
    [TestFixture]
    public class ResourcePoolTests
    {
        [Test]
        public void StartsFullOrEmpty()
        {
            Assert.That(new ResourcePool(new Stat(50f)).Current, Is.EqualTo(50f));
            Assert.That(new ResourcePool(new Stat(50f), startFull: false).IsEmpty, Is.True);
        }

        [Test]
        public void TrySpend_FailsWithoutChangingWhenInsufficient()
        {
            var pool = new ResourcePool(new Stat(10f));
            Assert.That(pool.TrySpend(4f), Is.True);
            Assert.That(pool.TrySpend(7f), Is.False);
            Assert.That(pool.Current, Is.EqualTo(6f));
        }

        [Test]
        public void DrainAndRestore_ClampAndReturnActualAmount()
        {
            var pool = new ResourcePool(new Stat(10f));
            Assert.That(pool.Drain(15f), Is.EqualTo(10f));
            Assert.That(pool.IsEmpty, Is.True);
            Assert.That(pool.Restore(4f), Is.EqualTo(4f));
            Assert.That(pool.Restore(20f), Is.EqualTo(6f));
            Assert.That(pool.IsFull, Is.True);
        }

        [Test]
        public void Depleted_FiresOnceWhenReachingZero()
        {
            var pool = new ResourcePool(new Stat(10f));
            int depleted = 0;
            pool.Depleted += _ => depleted++;
            pool.Drain(6f);
            pool.Drain(6f);
            pool.Drain(1f);
            Assert.That(depleted, Is.EqualTo(1));
        }

        [Test]
        public void Changed_FiresOnlyWhenValueChanges()
        {
            var pool = new ResourcePool(new Stat(10f));
            int changes = 0;
            pool.Changed += _ => changes++;
            pool.Restore(5f);
            pool.Drain(2f);
            Assert.That(changes, Is.EqualTo(1));
        }

        [Test]
        public void LoweringMax_ClampsCurrent_RaisingDoesNotRefill()
        {
            var max = new Stat(100f);
            var pool = new ResourcePool(max);
            var debuff = StatModifier.Flat(-40f);
            max.AddModifier(debuff);
            Assert.That(pool.Current, Is.EqualTo(60f));
            max.RemoveModifier(debuff);
            Assert.That(pool.Max, Is.EqualTo(100f));
            Assert.That(pool.Current, Is.EqualTo(60f));
            Assert.That(pool.Fraction, Is.EqualTo(0.6f).Within(1e-5f));
        }

        [Test]
        public void FillAndEmpty()
        {
            var pool = new ResourcePool(new Stat(8f), startFull: false);
            pool.Fill();
            Assert.That(pool.Current, Is.EqualTo(8f));
            pool.Empty();
            Assert.That(pool.Current, Is.EqualTo(0f));
        }

        [Test]
        public void InvalidArguments_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => new ResourcePool(null));
            var pool = new ResourcePool(new Stat(5f));
            Assert.Throws<ArgumentOutOfRangeException>(() => pool.TrySpend(-1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => pool.Restore(-1f));
        }
    }
}
