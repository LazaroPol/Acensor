using NUnit.Framework;
using Specimen.Core.Iteration;
using Specimen.Core.Life;

namespace Specimen.Tests.Core.Life
{
    [TestFixture]
    public class CharacterLifeTests
    {
        [Test]
        public void EmptyHealth_TurnsIntoSpectre_AndRaisesDiedOnce()
        {
            var vitals = LifeTestKit.Vitals();
            var life = LifeTestKit.Life(vitals, new FakeReviveCost());
            int deaths = 0;
            life.Died += _ => deaths++;
            vitals.Health.Drain(1000f);
            vitals.Health.Drain(10f);
            Assert.That(life.State, Is.EqualTo(LifeState.Spectre));
            Assert.That(deaths, Is.EqualTo(1));
        }

        [Test]
        public void Spectre_CannotFight_ButActivatesMechanismsAndMarksEnemies()
        {
            var vitals = LifeTestKit.Vitals();
            var life = LifeTestKit.Life(vitals, new FakeReviveCost());
            Assert.That(life.CanFight && !life.CanMarkEnemies, Is.True);
            vitals.Health.Drain(1000f);
            Assert.That(life.CanFight, Is.False);
            Assert.That(life.CanActivateMechanisms && life.CanMarkEnemies, Is.True);
        }

        [Test]
        public void ElevatorRevive_OnlyDuringCalm()
        {
            var vitals = LifeTestKit.Vitals();
            var cost = new FakeReviveCost();
            var life = LifeTestKit.Life(vitals, cost);
            vitals.Health.Drain(1000f);
            Assert.That(life.TryRevive(ReviveMethod.ElevatorBunk, IterationPhase.Incursion), Is.EqualTo(ReviveResult.WrongPhase));
            Assert.That(cost.Paid, Is.Empty);
            Assert.That(life.TryRevive(ReviveMethod.ElevatorBunk, IterationPhase.Calm), Is.EqualTo(ReviveResult.Revived));
            Assert.That(life.State, Is.EqualTo(LifeState.Alive));
            Assert.That(vitals.Health.IsFull && vitals.Stamina.IsFull && vitals.ClassResource.IsFull, Is.True);
        }

        [Test]
        public void FieldRevive_WorksInAnyPhase_WithPartialHealth()
        {
            var vitals = LifeTestKit.Vitals();
            var life = LifeTestKit.Life(vitals, new FakeReviveCost());
            ReviveMethod? used = null;
            life.Revived += (_, m) => used = m;
            vitals.Health.Drain(1000f);
            Assert.That(life.TryRevive(ReviveMethod.FieldItem, IterationPhase.Incursion), Is.EqualTo(ReviveResult.Revived));
            Assert.That(vitals.Health.Current, Is.EqualTo(35f).Within(0.001f));
            Assert.That(used, Is.EqualTo(ReviveMethod.FieldItem));
        }

        [Test]
        public void Revive_FailsWhenAliveOrUnpaid()
        {
            var vitals = LifeTestKit.Vitals();
            var cost = new FakeReviveCost { CanPay = false };
            var life = LifeTestKit.Life(vitals, cost);
            Assert.That(life.TryRevive(ReviveMethod.FieldItem, IterationPhase.Calm), Is.EqualTo(ReviveResult.NotSpectre));
            vitals.Health.Drain(1000f);
            Assert.That(life.TryRevive(ReviveMethod.FieldItem, IterationPhase.Calm), Is.EqualTo(ReviveResult.CannotPay));
            Assert.That(life.IsSpectre, Is.True);
        }

        [Test]
        public void RevivedCharacter_CanDieAgain()
        {
            var vitals = LifeTestKit.Vitals();
            var life = LifeTestKit.Life(vitals, new FakeReviveCost());
            int deaths = 0;
            life.Died += _ => deaths++;
            vitals.Health.Drain(1000f);
            life.TryRevive(ReviveMethod.FieldItem, IterationPhase.Ascent);
            vitals.Health.Drain(1000f);
            Assert.That(deaths, Is.EqualTo(2));
        }

        [Test]
        public void Rules_RejectFractionsOutOfRange()
        {
            Assert.That(() => new ReviveRules(0f, 0.5f), Throws.InstanceOf<System.ArgumentOutOfRangeException>());
            Assert.That(() => new ReviveRules(1f, 1.5f), Throws.InstanceOf<System.ArgumentOutOfRangeException>());
        }
    }
}
