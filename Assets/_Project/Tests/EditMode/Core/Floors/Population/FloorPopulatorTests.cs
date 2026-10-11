using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors;
using Specimen.Core.Floors.Population;
using Specimen.Tests.Core.Loot;

namespace Specimen.Tests.Core.Floors.Population
{
    public class FloorPopulatorTests
    {
        private static readonly RingCorridorGenerator Generator = new RingCorridorGenerator(RingLayoutConfig.Default);
        private static readonly FloorPopulator Populator =
            new FloorPopulator(LootFixtures.Table(), RingPopulationRules.Default);

        private static string Signature(FloorPopulation p) => string.Join(";", p.Rooms.Select(r =>
            $"{r.Room}:{r.Enemies}:{string.Join(",", r.Loot.Select(d => d.Item.Id + "x" + d.Quantity))}"));

        [Test]
        public void SameLayout_ProducesSamePopulation()
        {
            Assert.That(Signature(Populator.Populate(Generator.Generate(11))),
                Is.EqualTo(Signature(Populator.Populate(Generator.Generate(11)))));
        }

        [Test]
        public void DifferentSeeds_VaryThePopulation()
        {
            var distinct = Enumerable.Range(1, 10)
                .Select(s => Signature(Populator.Populate(Generator.Generate((ulong)s)))).Distinct();
            Assert.That(distinct.Count(), Is.GreaterThan(5));
        }

        [Test]
        public void EveryRoom_RespectsItsRingQuota([Range(1, 20)] int seed)
        {
            var population = Populator.Populate(Generator.Generate((ulong)seed));
            foreach (var r in population.Rooms.Where(r => r.Room.Kind == RoomKind.Chamber))
            {
                var quota = RingPopulationRules.Default.For(r.Room.Ring);
                Assert.That(r.Loot.Count, Is.InRange(quota.MinLoot, quota.MaxLoot));
                Assert.That(r.Enemies, Is.InRange(quota.MinEnemies, quota.MaxEnemies));
                Assert.That(r.Threat, Is.EqualTo(r.Enemies > 0 ? quota.Threat : 0));
            }
        }

        [Test]
        public void Entrance_IsEmpty_AndExitHasGuardsButNoLoot([Range(1, 10)] int seed)
        {
            var population = Populator.Populate(Generator.Generate((ulong)seed));
            Assert.That(population.ForRoom(0).IsEmpty, Is.True);
            foreach (var exit in population.Rooms.Where(r => r.Room.Kind == RoomKind.EmergencyExit))
            {
                Assert.That(exit.Loot, Is.Empty);
                Assert.That(exit.Enemies, Is.GreaterThanOrEqualTo(RingPopulationRules.Default.For(exit.Room.Ring).MinEnemies));
            }
        }

        [Test]
        public void PeacefulFloor_HasNoEnemies_ButKeepsLoot()
        {
            var population = Populator.Populate(Generator.Generate(5), hasCombat: false);
            Assert.That(population.TotalEnemies, Is.Zero);
            Assert.That(population.Rooms.All(r => r.Threat == 0), Is.True);
            Assert.That(population.Rooms.Sum(r => r.Loot.Count), Is.GreaterThan(0));
        }

        [Test]
        public void DeepRelic_OnlyAppearsInDeepestRing()
        {
            var drops = Enumerable.Range(1, 30).SelectMany(s => Populator.Populate(Generator.Generate((ulong)s)).Rooms)
                .Where(r => r.Loot.Any(d => d.Item == LootFixtures.Relic)).ToList();
            Assert.That(drops, Is.Not.Empty);
            Assert.That(drops.All(r => r.Room.Ring == 3), Is.True);
        }

        [Test]
        public void DeeperRings_HaveMoreEnemiesOnAverage()
        {
            var rooms = Enumerable.Range(1, 30).SelectMany(s => Populator.Populate(Generator.Generate((ulong)s)).Rooms).ToList();
            double Avg(int ring) => rooms.Where(r => r.Room.Ring == ring).Average(r => r.Enemies);
            Assert.That(Avg(3), Is.GreaterThan(Avg(2)));
            Assert.That(Avg(2), Is.GreaterThan(Avg(1)));
        }

        [Test]
        public void Constructor_AndPopulate_RejectNulls()
        {
            Assert.Throws<ArgumentNullException>(() => new FloorPopulator(null, RingPopulationRules.Default));
            Assert.Throws<ArgumentNullException>(() => new FloorPopulator(LootFixtures.Table(), null));
            Assert.Throws<ArgumentNullException>(() => Populator.Populate(null));
        }
    }
}
