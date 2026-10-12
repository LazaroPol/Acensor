using System.Linq;
using NUnit.Framework;
using Specimen.Core.Visuals;
using Specimen.Core.Visuals.Packs;

namespace Specimen.Tests.Core.Visuals
{
    [TestFixture]
    public class EnvironmentKitTests
    {
        [TestCase(VisualId.Suelo)]
        [TestCase(VisualId.Muro)]
        [TestCase(VisualId.Puerta)]
        [TestCase(VisualId.Loot)]
        public void LevelPiecesHaveAVillageModel(VisualId id)
            => Assert.That(EnvironmentKit.ModelFor(id), Is.Not.Null.And.Not.Empty);

        [TestCase(VisualId.Jugador)]
        [TestCase(VisualId.Enemigo)]
        [TestCase(VisualId.Ascensor)]
        public void CharactersAndElevatorAreNotVillagePieces(VisualId id)
            => Assert.That(EnvironmentKit.ModelFor(id), Is.Null);

        [Test]
        public void DoorFillsAWholeWallCell()
        {
            Assert.That(EnvironmentKit.FitBoxOf(VisualId.Puerta), Is.EqualTo(VisualId.Muro));
            Assert.That(EnvironmentKit.FitBoxOf(VisualId.Suelo), Is.EqualTo(VisualId.Suelo));
        }

        [Test]
        public void FittedMatchesPiecesAndDecorIsUnique()
        {
            Assert.That(EnvironmentKit.Fitted.Count(), Is.EqualTo(4));
            Assert.That(EnvironmentKit.Decor.Distinct().Count(), Is.EqualTo(EnvironmentKit.Decor.Count));
        }
    }
}
