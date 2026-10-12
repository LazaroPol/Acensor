using System.Linq;
using NUnit.Framework;
using Specimen.Core.World;

namespace Specimen.Tests.Core.World
{
    [TestFixture]
    public class HouseBlueprintTests
    {
        [Test]
        public void EveryHouseHasFloorWallsDoorAndRoof()
        {
            foreach (var house in new VillageLayout().Houses)
            {
                var pieces = HouseBlueprint.For(house);
                int cells = house.Width / 2 * (house.Depth / 2);
                int perimeter = house.Width + house.Depth;
                Assert.That(pieces.Count(p => p.Kind == HousePieceKind.Suelo), Is.EqualTo(cells), house.Id);
                Assert.That(pieces.Count(p => p.Kind == HousePieceKind.Muro), Is.EqualTo(perimeter), house.Id);
                Assert.That(pieces.Count(p => p.Model.Contains("Door")), Is.EqualTo(1), house.Id);
                Assert.That(pieces.Single(p => p.Kind == HousePieceKind.Tejado).Model,
                    Is.EqualTo($"Roof_RoundTiles_{house.Width}x{house.Depth}"));
            }
        }

        [Test]
        public void DoorIsOnTheFacade()
        {
            var house = new VillageLayout().Houses[0];
            var door = HouseBlueprint.For(house).Single(p => p.Model.Contains("Door"));
            Assert.That(door.Z, Is.EqualTo(house.Depth / 2f));
            Assert.That(door.Model, Does.StartWith(house.Style == HouseStyle.Yeso ? "Wall_Plaster" : "Wall_UnevenBrick"));
        }

        [Test]
        public void MapProjectionCoversTheVillage()
        {
            Assert.That(MapProjection.U(VillageLayout.MinX), Is.EqualTo(0f));
            Assert.That(MapProjection.V(VillageLayout.MaxZ), Is.EqualTo(1f));
            Assert.That(MapProjection.U(1000f), Is.EqualTo(1f));
            Assert.That(MapProjection.U(VillageLayout.PlazaX), Is.InRange(0.5f, 0.6f));
        }
    }
}
