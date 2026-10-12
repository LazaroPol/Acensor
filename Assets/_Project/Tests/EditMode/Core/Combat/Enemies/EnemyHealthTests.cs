using NUnit.Framework;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Tests.Core.Combat.Enemies
{
    [TestFixture]
    public class EnemyHealthTests
    {
        [Test]
        public void DiesOnceAndNeverGoesNegative()
        {
            var health = new EnemyHealth(20f);
            int deaths = 0;
            health.Died += () => deaths++;
            Assert.That(health.TakeDamage(15f), Is.False);
            Assert.That(health.TakeDamage(15f), Is.True);
            Assert.That(health.TakeDamage(15f), Is.False);
            Assert.That(health.Current, Is.EqualTo(0f));
            Assert.That(deaths, Is.EqualTo(1));
        }

        [Test]
        public void ReviveRestoresFullHealth()
        {
            var health = new EnemyHealth(10f);
            health.TakeDamage(10f);
            health.Revive();
            Assert.That(health.IsDead, Is.False);
            Assert.That(health.Fraction, Is.EqualTo(1f));
        }
    }
}
