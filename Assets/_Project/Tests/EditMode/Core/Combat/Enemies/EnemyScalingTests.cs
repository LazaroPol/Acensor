using System;
using NUnit.Framework;
using Specimen.Core.Combat.Enemies;

namespace Specimen.Tests.Core.Combat.Enemies
{
    public class EnemyScalingTests
    {
        private static readonly EnemyStats Base = new EnemyStats(100f, 10f, 20f);
        private static readonly LinearDepthScaling Scaling =
            new LinearDepthScaling(new DepthScalingConfig(0.1f, 0.05f, 0.02f, 0.2f));

        [TestCase(0f, 1f, 0f)]
        [TestCase(1f, 0f, 0f)]
        [TestCase(1f, 1f, -1f)]
        public void Stats_RejectInvalidValues(float health, float damage, float armor)
            => Assert.Throws<ArgumentOutOfRangeException>(() => new EnemyStats(health, damage, armor));

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 4)]
        public void Depth_RejectsOutOfRange(int floor, int ring)
            => Assert.Throws<ArgumentOutOfRangeException>(() => new EnemyDepth(floor, ring));

        [Test]
        public void Config_RejectsNegativeRates()
            => Assert.Throws<ArgumentOutOfRangeException>(() => new DepthScalingConfig(-0.1f, 0f, 0f, 0f));

        [Test]
        public void FirstFloorNearestRing_KeepsBaseStats()
        {
            var s = Scaling.Scale(Base, new EnemyDepth(1, 1));
            Assert.That(s.MaxHealth, Is.EqualTo(100f).Within(1e-4));
            Assert.That(s.Damage, Is.EqualTo(10f).Within(1e-4));
            Assert.That(s.Armor, Is.EqualTo(20f).Within(1e-4));
        }

        [Test]
        public void FloorAndRing_ApplyTheirRates()
        {
            var s = Scaling.Scale(Base, new EnemyDepth(11, 3));
            Assert.That(s.MaxHealth, Is.EqualTo(100f * 2f * 1.4f).Within(1e-3));
            Assert.That(s.Damage, Is.EqualTo(10f * 1.5f * 1.4f).Within(1e-3));
            Assert.That(s.Armor, Is.EqualTo(20f * 1.2f).Within(1e-3), "La armadura no escala por anillo.");
        }

        [Test]
        public void Stats_GrowMonotonicallyWithFloorAndRing()
        {
            for (int floor = 1; floor < 20; floor++)
                for (int ring = 1; ring <= 3; ring++)
                {
                    var here = Scaling.Scale(Base, new EnemyDepth(floor, ring));
                    var below = Scaling.Scale(Base, new EnemyDepth(floor + 1, ring));
                    Assert.That(below.MaxHealth, Is.GreaterThan(here.MaxHealth));
                    Assert.That(below.Damage, Is.GreaterThan(here.Damage));
                    Assert.That(below.Armor, Is.GreaterThan(here.Armor));
                    if (ring < 3)
                        Assert.That(Scaling.Scale(Base, new EnemyDepth(floor, ring + 1)).MaxHealth,
                            Is.GreaterThan(here.MaxHealth));
                }
        }

        [Test]
        public void Scaling_RejectsNulls()
        {
            Assert.Throws<ArgumentNullException>(() => new LinearDepthScaling(null));
            Assert.Throws<ArgumentNullException>(() => Scaling.Scale(null, new EnemyDepth(1, 1)));
            Assert.Throws<ArgumentNullException>(() => Scaling.Scale(Base, null));
        }
    }
}
