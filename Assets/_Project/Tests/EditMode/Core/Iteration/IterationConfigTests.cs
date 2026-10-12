using System;
using NUnit.Framework;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationConfigTests
    {
        private static IterationConfig Make(int floors = 5, float calm = 180, float incursion = 420,
                                            int waves = 5, int players = 4, float[] warnings = null) =>
            new IterationConfig(floors, calm, incursion, waves, players, warnings ?? new[] { 60f, 15f });

        [Test]
        public void ValidValues_AreExposed()
        {
            var c = Make(floors: 3, calm: 10, incursion: 20, waves: 2, players: 2, warnings: new[] { 5f });
            Assert.That((c.FloorCount, c.CalmSeconds, c.IncursionSeconds, c.SiegeWaves, c.MaxPlayers),
                Is.EqualTo((3, 10f, 20f, 2, 2)));
        }

        [Test]
        public void FloorsDuration_IsCalmPlusIncursionPerFloor() =>
            Assert.That(Make(floors: 5, calm: 180, incursion: 420).FloorsDurationSeconds, Is.EqualTo(3000f));

        [TestCase(0, 180, 420, 5, 4)]
        [TestCase(5, 0, 420, 5, 4)]
        [TestCase(5, 180, -1, 5, 4)]
        [TestCase(5, 180, 420, 0, 4)]
        [TestCase(5, 180, 420, 5, 0)]
        [TestCase(5, 180, 420, 5, 7)]
        public void InvalidValues_AreRejected(int floors, float calm, float incursion, int waves, int players) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => Make(floors, calm, incursion, waves, players));

        [Test]
        public void Warnings_AreSortedDescending_AndDeduplicated() =>
            Assert.That(Make(warnings: new[] { 15f, 60f, 15f, 30f }).DoorWarnings, Is.EqualTo(new[] { 60f, 30f, 15f }));

        [TestCase(0f)]
        [TestCase(420f)]
        [TestCase(500f)]
        public void Warnings_OutsideIncursion_AreRejected(float warning) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => Make(incursion: 420, warnings: new[] { warning }));

        [Test]
        public void Warnings_CanBeEmpty() => Assert.That(Make(warnings: new float[0]).DoorWarnings, Is.Empty);

        [Test]
        public void Warnings_AreReadOnly() =>
            Assert.Throws<NotSupportedException>(() => ((System.Collections.Generic.IList<float>)Make().DoorWarnings).Add(1f));
    }
}
