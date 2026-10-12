using System;
using NUnit.Framework;
using Specimen.Core.Combat;
using Specimen.Core.Siege.Defenses;

namespace Specimen.Tests.Core.Siege.Defenses
{
    public class DefensesTests
    {
        [Test]
        public void Fortification_RejectsInvalidValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Fortification(0f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Fortification(10f).TakeDamage(-1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Fortification(10f).Repair(-1f));
        }

        [Test]
        public void Fortification_AbsorbsDamage_AndIsDestroyedAtZero()
        {
            var f = new Fortification(10f);
            Assert.That(f.TakeDamage(4f), Is.EqualTo(4f));
            Assert.That(f.Health, Is.EqualTo(6f));
            Assert.That(f.TakeDamage(100f), Is.EqualTo(6f));
            Assert.That(f.IsDestroyed, Is.True);
        }

        [Test]
        public void Fortification_RepairIsCapped_AndDestroyedOnesStayDestroyed()
        {
            var f = new Fortification(10f);
            f.TakeDamage(3f);
            Assert.That(f.Repair(10f), Is.EqualTo(3f));
            Assert.That(f.Health, Is.EqualTo(10f));
            f.TakeDamage(10f);
            Assert.That(f.Repair(5f), Is.EqualTo(0f));
            Assert.That(f.IsDestroyed, Is.True);
        }

        [Test]
        public void Turret_RejectsInvalidValues()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Turret(0f, 5f, 1f, 3));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Turret(1f, 0f, 1f, 3));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Turret(1f, 5f, 0f, 3));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Turret(1f, 5f, 1f, 0));
        }

        [Test]
        public void Turret_FiresInRange_ConsumingAmmo_AndCoolsDown()
        {
            var t = new Turret(7f, 10f, 2f, 3);
            var hit = t.TryFire(5f);
            Assert.That(hit.Amount, Is.EqualTo(7f));
            Assert.That(hit.Type, Is.EqualTo(DamageType.Physical));
            Assert.That(t.Ammo, Is.EqualTo(2));
            Assert.That(t.TryFire(5f), Is.Null);
            t.Tick(1f);
            Assert.That(t.IsReady, Is.False);
            t.Tick(1f);
            Assert.That(t.TryFire(5f), Is.Not.Null);
        }

        [Test]
        public void Turret_DoesNotFireOutOfRange_OrWithoutAmmo()
        {
            var t = new Turret(1f, 10f, 1f, 1);
            Assert.That(t.TryFire(10.5f), Is.Null);
            Assert.That(t.Ammo, Is.EqualTo(1));
            t.TryFire(1f);
            t.Tick(5f);
            Assert.That(t.IsReady, Is.False);
            Assert.That(t.TryFire(1f), Is.Null);
        }

        [Test]
        public void Turret_ReloadIsCapped()
        {
            var t = new Turret(1f, 10f, 1f, 5);
            t.TryFire(1f);
            Assert.That(t.Reload(10), Is.EqualTo(1));
            Assert.That(t.Ammo, Is.EqualTo(5));
            Assert.Throws<ArgumentOutOfRangeException>(() => t.Reload(-1));
        }
    }
}
