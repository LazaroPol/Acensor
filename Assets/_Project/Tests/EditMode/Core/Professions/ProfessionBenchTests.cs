using NUnit.Framework;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Professions
{
    [TestFixture]
    public class ProfessionBenchTests
    {
        private ProfessionBench _bench;

        [SetUp]
        public void SetUp() => _bench = new ProfessionBench(SynergyCatalog.CreateDefault());

        [Test]
        public void PlayerGetsChosenProfession()
        {
            Assert.That(_bench.TryChoose(1, ProfessionId.Herrero), Is.True);
            Assert.That(_bench.TryGetProfession(1, out var id), Is.True);
            Assert.That(id, Is.EqualTo(ProfessionId.Herrero));
        }

        [Test]
        public void ChoosingAgainReplacesProfession()
        {
            _bench.TryChoose(1, ProfessionId.Herrero);
            _bench.TryChoose(1, ProfessionId.Sastre);
            _bench.TryGetProfession(1, out var id);
            Assert.That(id, Is.EqualTo(ProfessionId.Sastre));
            Assert.That(_bench.ChosenCount, Is.EqualTo(1));
        }

        [Test]
        public void DuplicatesAreAllowed()
        {
            Assert.That(_bench.TryChoose(1, ProfessionId.Manitas), Is.True);
            Assert.That(_bench.TryChoose(2, ProfessionId.Manitas), Is.True);
        }

        [Test]
        public void LockFailsUntilEveryoneChose()
        {
            _bench.TryChoose(1, ProfessionId.Quimico);
            Assert.That(_bench.TryLock(new[] { 1, 2 }), Is.False);
            _bench.TryChoose(2, ProfessionId.Cocinero);
            Assert.That(_bench.TryLock(new[] { 1, 2 }), Is.True);
        }

        [Test]
        public void LockedBenchRejectsChanges()
        {
            _bench.TryChoose(1, ProfessionId.Quimico);
            _bench.TryLock(new[] { 1 });
            Assert.That(_bench.TryChoose(1, ProfessionId.Herrero), Is.False);
            Assert.That(_bench.Release(1), Is.False);
        }

        [Test]
        public void ActiveSynergiesFollowGroup()
        {
            _bench.TryChoose(1, ProfessionId.Quimico);
            _bench.TryChoose(2, ProfessionId.Cocinero);
            Assert.That(_bench.ActiveSynergies(), Has.Count.EqualTo(1));
            _bench.Release(2);
            Assert.That(_bench.ActiveSynergies(), Is.Empty);
            Assert.That(_bench.GroupHas(ProfessionId.Cocinero), Is.False);
        }

        [Test]
        public void ResetClearsAndUnlocks()
        {
            _bench.TryChoose(1, ProfessionId.Escultor);
            _bench.TryLock(new[] { 1 });
            _bench.Reset();
            Assert.That(_bench.IsLocked, Is.False);
            Assert.That(_bench.ChosenCount, Is.Zero);
        }
    }
}
