using NUnit.Framework;
using Specimen.Core.Iteration;
using Specimen.Core.Life;

namespace Specimen.Tests.Core.Life
{
    [TestFixture]
    public class PartyLifeTests
    {
        [Test]
        public void AllFallen_RaisedOnlyWhenLastPlayerDies()
        {
            var party = new PartyLife();
            var a = LifeTestKit.Vitals();
            var b = LifeTestKit.Vitals();
            party.Add(1, LifeTestKit.Life(a, new FakeReviveCost()));
            party.Add(2, LifeTestKit.Life(b, new FakeReviveCost()));
            int wipes = 0;
            party.AllFallen += () => wipes++;
            a.Health.Drain(1000f);
            Assert.That(wipes, Is.EqualTo(0));
            Assert.That(party.Spectres, Is.EqualTo(new[] { 1 }));
            b.Health.Drain(1000f);
            Assert.That(wipes, Is.EqualTo(1));
            Assert.That(party.AllSpectres, Is.True);
        }

        [Test]
        public void RevivedPlayer_LeavesSpectreList()
        {
            var party = new PartyLife();
            var vitals = LifeTestKit.Vitals();
            party.Add(7, LifeTestKit.Life(vitals, new FakeReviveCost()));
            vitals.Health.Drain(1000f);
            party.Of(7).TryRevive(ReviveMethod.ElevatorBunk, IterationPhase.Calm);
            Assert.That(party.Spectres, Is.Empty);
            Assert.That(party.AllSpectres, Is.False);
        }

        [Test]
        public void DuplicatePlayer_Throws_AndEmptyPartyIsNotWiped()
        {
            var party = new PartyLife();
            Assert.That(party.AllSpectres, Is.False);
            party.Add(1, LifeTestKit.Life());
            Assert.That(() => party.Add(1, LifeTestKit.Life()), Throws.ArgumentException);
        }
    }
}
