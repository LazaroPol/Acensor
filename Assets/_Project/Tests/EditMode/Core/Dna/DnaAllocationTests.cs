using NUnit.Framework;
using Specimen.Core.Dna;

namespace Specimen.Tests.Core.Dna
{
    [TestFixture]
    public class DnaAllocationTests
    {
        private DnaAllocation _dna;

        [SetUp]
        public void SetUp() => _dna = new DnaAllocation(DnaTree.CreateDefault(), TierLock.CreateDefault());

        [Test]
        public void UnlocksTierOneNodeSpendingPoints()
        {
            _dna.AddPoints(3);
            Assert.That(_dna.TryUnlock("Potencia-T1-a"), Is.EqualTo(UnlockResult.Ok));
            Assert.That(_dna.FreePoints, Is.EqualTo(2));
            Assert.That(_dna.Invested(DnaBranch.Potencia), Is.EqualTo(1));
            Assert.That(_dna.IsUnlocked("Potencia-T1-a"), Is.True);
        }

        [Test]
        public void RejectsUnknownAndDuplicate()
        {
            _dna.AddPoints(5);
            Assert.That(_dna.TryUnlock("nope"), Is.EqualTo(UnlockResult.UnknownNode));
            _dna.TryUnlock("Agilidad-T1-a");
            Assert.That(_dna.TryUnlock("Agilidad-T1-a"), Is.EqualTo(UnlockResult.AlreadyUnlocked));
        }

        [Test]
        public void RejectsWhenNotEnoughPoints()
        {
            Assert.That(_dna.TryUnlock("Vitalidad-T1-a"), Is.EqualTo(UnlockResult.NotEnoughPoints));
            Assert.That(_dna.FreePoints, Is.EqualTo(0));
        }

        [Test]
        public void TierLockBlocksUntilBranchThresholdReached()
        {
            _dna.AddPoints(100);
            Assert.That(_dna.TryUnlock("Resistencia-T2-a"), Is.EqualTo(UnlockResult.TierLocked));
            _dna.TryUnlock("Resistencia-T1-a");
            _dna.TryUnlock("Resistencia-T1-b");
            _dna.TryUnlock("Resistencia-T2-a"); // bloqueado: 2 < 5
            Assert.That(_dna.IsUnlocked("Resistencia-T2-a"), Is.False);
        }

        [Test]
        public void ThresholdCountsOnlyThatBranch()
        {
            _dna.AddPoints(100);
            _dna.TryUnlock("Agilidad-T1-a");
            _dna.TryUnlock("Agilidad-T1-b");
            _dna.TryUnlock("Agilidad-T2-a"); // bloqueado en Agilidad (2)
            _dna.TryUnlock("Potencia-T1-a");
            Assert.That(_dna.TryUnlock("Potencia-T2-a"), Is.EqualTo(UnlockResult.TierLocked));
        }

        [Test]
        public void TierTwoOpensWhenCustomThresholdReached()
        {
            var tl = new TierLock(0, 2);
            var dna = new DnaAllocation(DnaTree.CreateDefault(), tl);
            dna.AddPoints(10);
            dna.TryUnlock("Cognicion-T1-a");
            dna.TryUnlock("Cognicion-T1-b");
            Assert.That(dna.TryUnlock("Cognicion-T2-a"), Is.EqualTo(UnlockResult.Ok));
            Assert.That(dna.FreePoints, Is.EqualTo(10 - 1 - 1 - 2));
        }

        [Test]
        public void NegativePointsThrow()
            => Assert.Throws<System.ArgumentOutOfRangeException>(() => _dna.AddPoints(-1));
    }
}
