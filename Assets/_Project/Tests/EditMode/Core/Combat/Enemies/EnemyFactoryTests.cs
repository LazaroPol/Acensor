using System;
using NUnit.Framework;
using Specimen.Core.Combat;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Tests.Core.Combat.Enemies
{
    public class EnemyFactoryTests
    {
        private static EnemyArchetype Ignis() => new EnemyArchetype(
            "ignis", new EnemyStats(50f, 8f, 10f), DamageType.Fire,
            new Resistances().Set(DamageType.Fire, 0.5f));

        [Test]
        public void Archetype_RejectsEmptyId()
            => Assert.Throws<ArgumentException>(() =>
                new EnemyArchetype(" ", new EnemyStats(1f, 1f, 0f), DamageType.Physical, Resistances.None));

        [Test]
        public void Factory_RejectsNullScaling()
            => Assert.Throws<ArgumentNullException>(() => new EnemyFactory(null));

        [Test]
        public void Create_ScalesStats_AndKeepsResistances()
        {
            var factory = new EnemyFactory(new LinearDepthScaling(DepthScalingConfig.Default));
            var deep = factory.Create(Ignis(), new EnemyDepth(6, 2));

            Assert.That(deep.Stats.MaxHealth, Is.EqualTo(50f * 1.6f * 1.15f).Within(1e-3));
            Assert.That(deep.Defense.Armor, Is.EqualTo(deep.Stats.Armor));
            Assert.That(deep.Defense.Resistances.Of(DamageType.Fire), Is.EqualTo(0.5f));
            Assert.That(deep.Depth.Ring, Is.EqualTo(2));
        }

        [Test]
        public void Attack_UsesScaledDamageAndArchetypeType()
        {
            var factory = new EnemyFactory(new LinearDepthScaling(DepthScalingConfig.Default));
            var enemy = factory.Create(Ignis(), new EnemyDepth(1, 1));
            var hit = enemy.Attack();

            Assert.That(hit.Amount, Is.EqualTo(8f).Within(1e-4));
            Assert.That(hit.Type, Is.EqualTo(DamageType.Fire));
        }

        [Test]
        public void DeeperEnemy_IsTougherThanShallowOne()
        {
            var factory = new EnemyFactory(new LinearDepthScaling(DepthScalingConfig.Default));
            var shallow = factory.Create(Ignis(), new EnemyDepth(2, 1));
            var deep = factory.Create(Ignis(), new EnemyDepth(2, 3));
            Assert.That(deep.Stats.MaxHealth, Is.GreaterThan(shallow.Stats.MaxHealth));
            Assert.That(deep.Stats.Damage, Is.GreaterThan(shallow.Stats.Damage));
        }
    }
}
