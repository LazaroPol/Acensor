using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Elevator;
using Specimen.Core.Iteration;
using Specimen.Core.Items;
using Specimen.Core.Life;
using Specimen.Core.Vitals;
using static Specimen.Tests.Core.Elevator.ElevatorFixtures;

namespace Specimen.Tests.Core.Elevator
{
    [TestFixture]
    public class BunkTests
    {
        private sealed class FixedLevels : IModuleLevels
        {
            public int Bunk;
            public int LevelOf(ModuleType type) => type == ModuleType.Bunk ? Bunk : 0;
        }

        private sealed class FieldCost : IReviveCost
        {
            public bool TryPay(ReviveMethod method) => true;
        }

        private static readonly BunkConfig Config =
            new BunkConfig(new[] { new RecipeIngredient(Wood, 4), new RecipeIngredient(Stone, 2) }, 2);

        private FixedLevels _levels;
        private Inventory _stock;
        private CharacterVitals _vitals;
        private CharacterLife _life;
        private Bunk _bunk;

        private void Setup(int level, int wood, int stone, IReviveCost field = null)
        {
            _levels = new FixedLevels { Bunk = level };
            _stock = Stocked(wood, stone);
            _bunk = new Bunk(_levels, _stock, Config, field);
            _vitals = Specimen.Tests.Core.Life.LifeTestKit.Vitals();
            _life = new CharacterLife(_vitals, ReviveRules.Default, _bunk);
            _vitals.Health.Drain(1000f);
        }

        [Test]
        public void WithoutBunk_ReturnsNoBunk_AndKeepsMaterials()
        {
            Setup(0, 10, 10);
            Assert.That(_bunk.Revive(_life, IterationPhase.Calm), Is.EqualTo(ReviveResult.NoBunk));
            Assert.That(_stock.CountOf(Wood.Id), Is.EqualTo(10));
            Assert.That(_life.IsSpectre, Is.True);
        }

        [Test]
        public void OutsideCalm_ReturnsWrongPhase_AndKeepsMaterials()
        {
            Setup(1, 10, 10);
            Assert.That(_bunk.Revive(_life, IterationPhase.Incursion), Is.EqualTo(ReviveResult.WrongPhase));
            Assert.That(_stock.CountOf(Wood.Id), Is.EqualTo(10));
        }

        [Test]
        public void MissingMaterials_ReturnsCannotPay_AndConsumesNothing()
        {
            Setup(1, 10, 1);
            Assert.That(_bunk.Revive(_life, IterationPhase.Calm), Is.EqualTo(ReviveResult.CannotPay));
            Assert.That(_stock.CountOf(Wood.Id), Is.EqualTo(10));
            Assert.That(_stock.CountOf(Stone.Id), Is.EqualTo(1));
        }

        [Test]
        public void InCalm_RevivesWithFullHealth_AndChargesLevelCost()
        {
            Setup(1, 10, 10);
            Assert.That(_bunk.Revive(_life, IterationPhase.Calm), Is.EqualTo(ReviveResult.Revived));
            Assert.That(_life.IsSpectre, Is.False);
            Assert.That(_vitals.Health.IsFull, Is.True);
            Assert.That(_stock.CountOf(Wood.Id), Is.EqualTo(6));
            Assert.That(_stock.CountOf(Stone.Id), Is.EqualTo(8));
        }

        [Test]
        public void HigherLevel_ReducesCost_ButNeverBelowOne()
        {
            Setup(3, 10, 10);
            _bunk.Revive(_life, IterationPhase.Calm);
            Assert.That(_stock.CountOf(Wood.Id), Is.EqualTo(9));
            Assert.That(_stock.CountOf(Stone.Id), Is.EqualTo(9));
        }

        [Test]
        public void AliveCharacter_ReturnsNotSpectre()
        {
            Setup(1, 10, 10);
            _bunk.Revive(_life, IterationPhase.Calm);
            Assert.That(_bunk.Revive(_life, IterationPhase.Calm), Is.EqualTo(ReviveResult.NotSpectre));
        }

        [Test]
        public void FieldRevive_DelegatesToFieldCost_OrFailsWithoutIt()
        {
            Setup(1, 10, 10);
            Assert.That(_life.TryRevive(ReviveMethod.FieldItem, IterationPhase.Incursion),
                Is.EqualTo(ReviveResult.CannotPay));
            Setup(1, 10, 10, new FieldCost());
            Assert.That(_life.TryRevive(ReviveMethod.FieldItem, IterationPhase.Incursion),
                Is.EqualTo(ReviveResult.Revived));
            Assert.That(_stock.CountOf(Wood.Id), Is.EqualTo(10));
        }

        [Test]
        public void Config_RejectsEmptyOrDuplicatedCost()
        {
            Assert.That(() => new BunkConfig(new RecipeIngredient[0]), Throws.ArgumentException);
            Assert.That(() => new BunkConfig(new[] { new RecipeIngredient(Wood), new RecipeIngredient(Wood) }),
                Throws.ArgumentException);
        }
    }
}
