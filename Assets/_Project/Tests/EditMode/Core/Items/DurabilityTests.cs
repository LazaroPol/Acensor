using System;
using NUnit.Framework;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class DurabilityTests
    {
        [Test]
        public void StartsFull()
        {
            var d = new Durability(10);
            Assert.That(d.Current, Is.EqualTo(10));
            Assert.That(d.Ratio, Is.EqualTo(1f));
            Assert.That(d.IsBroken, Is.False);
        }

        [Test]
        public void WearIsClampedAndBreaksAtZero()
        {
            var d = new Durability(5);
            var broken = 0;
            d.Broken += () => broken++;
            Assert.That(d.Wear(3), Is.EqualTo(3));
            Assert.That(d.Wear(10), Is.EqualTo(2));
            Assert.That(d.IsBroken, Is.True);
            Assert.That(d.Wear(1), Is.EqualTo(0));
            Assert.That(broken, Is.EqualTo(1));
        }

        [Test]
        public void RepairIsClampedToMaxAndUnbreaks()
        {
            var d = new Durability(8, 0);
            Assert.That(d.IsBroken, Is.True);
            Assert.That(d.Repair(5), Is.EqualTo(5));
            Assert.That(d.Repair(5), Is.EqualTo(3));
            Assert.That(d.Current, Is.EqualTo(8));
            Assert.That(d.IsBroken, Is.False);
        }

        [Test]
        public void ChangedOnlyFiresOnRealChange()
        {
            var d = new Durability(4);
            var calls = 0;
            d.Changed += _ => calls++;
            d.Repair(2);
            d.Wear(0);
            d.Wear(1);
            Assert.That(calls, Is.EqualTo(1));
        }

        [Test]
        public void ZeroMaxIsIndestructible()
        {
            var d = new Durability(0);
            Assert.That(d.IsIndestructible, Is.True);
            Assert.That(d.Wear(100), Is.EqualTo(0));
            Assert.That(d.IsBroken, Is.False);
            Assert.That(d.Ratio, Is.EqualTo(1f));
        }

        [Test]
        public void InvalidValuesThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Durability(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Durability(5, 6));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Durability(5).Wear(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Durability(5).Repair(-1));
        }
    }
}
