using NUnit.Framework;
using Specimen.Core.Dna;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Dna
{
    [TestFixture]
    public class DnaMutationApplierTests
    {
        private DnaTree _tree;
        private DnaAllocation _dna;
        private DnaMutationApplier _applier;
        private StatSheet _sheet;

        [SetUp]
        public void SetUp()
        {
            _tree = DnaTree.CreateDefault();
            _dna = new DnaAllocation(_tree, TierLock.CreateDefault());
            _applier = new DnaMutationApplier(_tree, new DefaultDnaEffects());
            _sheet = new StatSheet();
            _sheet.Define(StatType.Damage, 100f);
            _sheet.Define(StatType.Armor, 10f);
        }

        [Test]
        public void NoMutationsLeavesStatsUntouched()
        {
            Assert.That(_applier.Apply(_sheet, _dna), Is.EqualTo(0));
            Assert.That(_sheet.ValueOf(StatType.Damage), Is.EqualTo(100f));
        }

        [Test]
        public void UnlockedNodeBoostsItsStat()
        {
            _dna.AddPoints(1);
            _dna.TryUnlock("Potencia-T1-a");
            _applier.Apply(_sheet, _dna);
            Assert.That(_sheet.ValueOf(StatType.Damage), Is.EqualTo(102f).Within(0.001f));
            Assert.That(_sheet.ValueOf(StatType.Armor), Is.EqualTo(10f));
        }

        [Test]
        public void BranchesCombineWithoutClassRestriction()
        {
            _dna.AddPoints(2);
            _dna.TryUnlock("Potencia-T1-a");
            _dna.TryUnlock("Resistencia-T1-a");
            Assert.That(_applier.Apply(_sheet, _dna), Is.EqualTo(2));
            Assert.That(_sheet.ValueOf(StatType.Armor), Is.EqualTo(10.2f).Within(0.001f));
        }

        [Test]
        public void ApplyingTwiceDoesNotStack()
        {
            _dna.AddPoints(1);
            _dna.TryUnlock("Potencia-T1-a");
            _applier.Apply(_sheet, _dna);
            _applier.Apply(_sheet, _dna);
            Assert.That(_sheet.ValueOf(StatType.Damage), Is.EqualTo(102f).Within(0.001f));
        }

        [Test]
        public void UndefinedStatsAreSkipped()
        {
            _dna.AddPoints(1);
            _dna.TryUnlock("Agilidad-T1-a");
            Assert.That(_applier.Apply(_sheet, _dna), Is.EqualTo(0));
        }

        [Test]
        public void HigherTierGivesBiggerBonus()
        {
            var fx = new DefaultDnaEffects();
            AssertBonus(fx, "Potencia-T3-a", 0.06f);
        }

        private void AssertBonus(IDnaEffectCatalog fx, string id, float expected)
        {
            _tree.TryGet(id, out var node);
            Assert.That(fx.EffectsOf(node)[0].Percent, Is.EqualTo(expected).Within(0.0001f));
        }
    }
}
