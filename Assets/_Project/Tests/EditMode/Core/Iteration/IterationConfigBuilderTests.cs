using NUnit.Framework;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Iteration
{
    public class IterationConfigBuilderTests
    {
        [Test]
        public void Default_MatchesGameDesign()
        {
            var c = IterationConfigBuilder.Default;
            Assert.That(c.FloorCount, Is.EqualTo(5));
            Assert.That(c.CalmSeconds, Is.EqualTo(180f));
            Assert.That(c.IncursionSeconds, Is.EqualTo(420f));
            Assert.That(c.SiegeWaves, Is.EqualTo(5));
            Assert.That(c.MaxPlayers, Is.EqualTo(6));
            Assert.That(c.DoorWarnings, Is.EqualTo(new[] { 60f, 15f }));
        }

        [Test]
        public void FluentOverrides_AreApplied()
        {
            var c = new IterationConfigBuilder()
                .WithFloors(2).WithCalm(30).WithIncursion(90).WithSiegeWaves(3).WithMaxPlayers(1)
                .WithDoorWarnings(10)
                .Build();
            Assert.That((c.FloorCount, c.CalmSeconds, c.IncursionSeconds, c.SiegeWaves, c.MaxPlayers),
                Is.EqualTo((2, 30f, 90f, 3, 1)));
            Assert.That(c.DoorWarnings, Is.EqualTo(new[] { 10f }));
        }

        [Test]
        public void EarlyCloseGrace_DefaultsTo15_AndMustFitInIncursion()
        {
            Assert.That(IterationConfigBuilder.Default.EarlyCloseGraceSeconds, Is.EqualTo(15f));
            Assert.That(new IterationConfigBuilder().WithEarlyCloseGrace(30).Build().EarlyCloseGraceSeconds, Is.EqualTo(30f));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new IterationConfigBuilder().WithEarlyCloseGrace(0).Build());
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                new IterationConfigBuilder().WithIncursion(100).WithDoorWarnings(10).WithEarlyCloseGrace(100).Build());
        }

        [Test]
        public void Build_ValidatesThroughConfig() =>
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new IterationConfigBuilder().WithIncursion(30).Build());
    }
}
