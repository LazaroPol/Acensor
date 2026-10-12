using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Bestiary;
using Specimen.Core.Professions;
using Specimen.Core.Sandbox;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Sandbox
{
    [TestFixture]
    public class SandboxLayoutTests
    {
        private readonly SandboxLayout _layout = new SandboxLayout();

        private IEnumerable<SandboxSpot> AllSpots()
            => _layout.Enemies().Concat(_layout.Benches()).Concat(_layout.Decor()).Append(_layout.PlayerSpawn);

        [Test]
        public void RoomHasNoPlayerPlaceholderButHasADoor()
        {
            Assert.That(_layout.Room().Any(p => p.Id == VisualId.Jugador), Is.False);
            Assert.That(_layout.Room().Count(p => p.Id == VisualId.Puerta), Is.EqualTo(0), "el portón sustituye a la puerta");
            Assert.That(_layout.Room().Count(p => p.Z == 0f && p.YawDegrees == 0f && p.Id == VisualId.Muro), Is.EqualTo(SandboxLayout.Width - 3));
            Assert.That(_layout.Gate.X, Is.EqualTo(SandboxLayout.Width / 2f + 0.5f));
        }

        [Test]
        public void EveryCreatureAndProfessionIsPlaced()
        {
            Assert.That(_layout.Enemies(), Is.Empty, "el bestiario vive fuera, en el poblado");
            Assert.That(_layout.Benches().Select(s => s.Id),
                Is.EquivalentTo(Enum.GetNames(typeof(ProfessionId))));
        }

        [Test]
        public void SpotsStayInsideTheWalls()
        {
            foreach (var spot in AllSpots())
            {
                Assert.That(spot.X, Is.InRange(1f, SandboxLayout.Width - 1f), spot.Id);
                Assert.That(spot.Z, Is.InRange(1f, SandboxLayout.Depth - 1f), spot.Id);
            }
        }

        [Test]
        public void SpotsDoNotOverlap()
        {
            var spots = AllSpots().ToList();
            for (int i = 0; i < spots.Count; i++)
                for (int j = i + 1; j < spots.Count; j++)
                {
                    float dx = spots[i].X - spots[j].X, dz = spots[i].Z - spots[j].Z;
                    Assert.That(Math.Sqrt(dx * dx + dz * dz), Is.GreaterThan(1.5), $"{spots[i].Id} / {spots[j].Id}");
                }
        }
    }
}
