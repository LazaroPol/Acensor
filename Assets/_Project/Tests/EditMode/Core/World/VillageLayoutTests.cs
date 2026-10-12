using System.Linq;
using NUnit.Framework;
using Specimen.Core.Bestiary;
using Specimen.Core.Gathering;
using Specimen.Core.World;

namespace Specimen.Tests.Core.World
{
    [TestFixture]
    public class VillageLayoutTests
    {
        private readonly VillageLayout _village = new VillageLayout();

        [Test]
        public void PathStartsAtTheArmoryGateAndReachesThePlaza()
        {
            Assert.That(_village.IsOnPath(VillageLayout.GateX, -1f, 0f), Is.True);
            Assert.That(_village.IsOnPath(VillageLayout.PlazaX, VillageLayout.PlazaZ, 0f), Is.True);
            Assert.That(_village.IsOnPath(VillageLayout.GateX, -34f, 0f), Is.True, "camino a la cantera");
        }

        [Test]
        public void HousesDoNotBlockPathsNorEachOther()
        {
            foreach (var house in _village.Houses)
            {
                Assert.That(_village.IsOnPath(house.X, house.Z, 0f), Is.False, house.Id);
                foreach (var other in _village.Houses.Where(h => h != house))
                    Assert.That(new WorldSpot("", house.X, house.Z).DistanceTo(other.X, other.Z),
                        Is.GreaterThan(house.Radius), $"{house.Id}/{other.Id}");
            }
        }

        [Test]
        public void EveryCreatureOfTheBestiaryHasACamp()
            => Assert.That(_village.Camps.Select(c => c.Id), Is.EquivalentTo(DefaultBestiary.All().Select(e => e.Id)));

        [Test]
        public void NatureIsDeterministicSpacedAndInsideTheMap()
        {
            var again = new VillageLayout().Nodes;
            Assert.That(_village.Nodes.Count, Is.EqualTo(again.Count));
            Assert.That(_village.Nodes.Count(n => n.kind == GatherKind.VetaHierro), Is.GreaterThanOrEqualTo(4));
            foreach (var (_, spot) in _village.Nodes)
            {
                Assert.That(spot.X, Is.InRange(VillageLayout.MinX, VillageLayout.MaxX));
                Assert.That(spot.Z, Is.InRange(VillageLayout.MinZ, VillageLayout.MaxZ));
                Assert.That(_village.IsOnPath(spot.X, spot.Z, 1f), Is.False);
            }
        }

        [Test]
        public void ZonesNameThePlaces()
        {
            Assert.That(_village.ZoneAt(11f, 5f).Name, Is.EqualTo("Armería"));
            Assert.That(_village.ZoneAt(VillageLayout.PlazaX, VillageLayout.PlazaZ).Name, Is.EqualTo("Plaza del Poblado"));
            Assert.That(_village.ZoneAt(-100f, 0f), Is.Null);
            foreach (var camp in _village.Camps) Assert.That(_village.ZoneAt(camp.X, camp.Z), Is.Not.Null, camp.Id);
        }

        [Test]
        public void HouseTemplatesFollowTheModule()
        {
            Assert.That(_village.Houses[0].Template, Is.EqualTo("Casa_4x6_Yeso"));
            Assert.Throws<System.ArgumentException>(() => new HouseSpot("x", 0, 0, 0, 5, 4, HouseStyle.Yeso));
        }
    }
}
