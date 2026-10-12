using System;
using NUnit.Framework;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Tests.Core.Combat.Enemies
{
    [TestFixture]
    public class EnemyBrainTests
    {
        private EnemyBrain _brain;

        [SetUp]
        public void SetUp() => _brain = new EnemyBrain(new EnemyBrainConfig(8f, 15f, 1.5f, 1f));

        [Test]
        public void WaitsUntilTargetEntersAggro()
        {
            Assert.That(_brain.Decide(10f, true, true, 0.1f), Is.EqualTo(EnemyAction.Esperar));
            Assert.That(_brain.Decide(7f, true, true, 0.1f), Is.EqualTo(EnemyAction.Perseguir));
        }

        [Test]
        public void KeepsChasingInsideLeashAndDropsOutside()
        {
            _brain.Decide(7f, true, true, 0.1f);
            Assert.That(_brain.Decide(12f, true, true, 0.1f), Is.EqualTo(EnemyAction.Perseguir));
            Assert.That(_brain.Decide(16f, true, true, 0.1f), Is.EqualTo(EnemyAction.Esperar));
            Assert.That(_brain.IsEngaged, Is.False);
        }

        [Test]
        public void AttacksInRangeRespectingCooldown()
        {
            Assert.That(_brain.Decide(1f, true, true, 0.1f), Is.EqualTo(EnemyAction.Atacar));
            Assert.That(_brain.Decide(1f, true, true, 0.5f), Is.EqualTo(EnemyAction.Esperar));
            Assert.That(_brain.Decide(1f, true, true, 0.6f), Is.EqualTo(EnemyAction.Atacar));
        }

        [Test]
        public void DeadSelfOrDeadTargetStopTheFight()
        {
            Assert.That(_brain.Decide(1f, false, true, 0.1f), Is.EqualTo(EnemyAction.Muerto));
            Assert.That(_brain.Decide(1f, true, false, 0.1f), Is.EqualTo(EnemyAction.Esperar));
        }

        [Test]
        public void ConfigRejectsInconsistentRanges()
        {
            Assert.Throws<ArgumentException>(() => new EnemyBrainConfig(8f, 5f));
            Assert.Throws<ArgumentException>(() => new EnemyBrainConfig(2f, 5f, 3f));
        }

        [Test]
        public void ScaledConfigReachesFurther()
        {
            var scaled = new EnemyBrainConfig(8f, 15f, 1.5f).ScaledBy(2f);
            Assert.That(scaled.AttackRange, Is.EqualTo(3f).Within(1e-5f));
            Assert.That(scaled.AggroRange, Is.GreaterThanOrEqualTo(scaled.AttackRange));
        }
    }
}
