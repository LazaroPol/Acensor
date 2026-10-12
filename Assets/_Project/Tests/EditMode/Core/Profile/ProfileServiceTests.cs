using System;
using NUnit.Framework;
using Specimen.Core.Dna;
using Specimen.Core.Lore;
using Specimen.Core.Profile;

namespace Specimen.Tests.Core.Profile
{
    [TestFixture]
    public class ProfileServiceTests
    {
        private InMemoryProfileStore _store;
        private ProfileService _service;

        private static DnaAllocation NewDna() => new DnaAllocation(DnaTree.CreateDefault(), TierLock.CreateDefault());

        private static MemoryArchive NewArchive() => new MemoryArchive(new[] { new LoreEntry("clon", "Clon", "d1") });

        [SetUp]
        public void SetUp()
        {
            _store = new InMemoryProfileStore();
            _service = new ProfileService(_store);
        }

        [Test]
        public void SaveThenLoadRestoresEverything()
        {
            var dna = NewDna();
            dna.AddPoints(10);
            dna.TryUnlock("Potencia-T1-a");
            dna.TryUnlock("Potencia-T1-b");
            var archive = NewArchive();
            archive.Collect("d1");
            _service.Save(dna, archive);

            var dna2 = NewDna();
            var archive2 = NewArchive();
            Assert.That(_service.Load(dna2, archive2), Is.True);
            Assert.That(dna2.FreePoints, Is.EqualTo(8));
            Assert.That(dna2.IsUnlocked("Potencia-T1-b"), Is.True);
            Assert.That(dna2.Invested(DnaBranch.Potencia), Is.EqualTo(2));
            Assert.That(archive2.IsUnlocked("clon"), Is.True);
        }

        [Test]
        public void LoadWithoutSavedProfileReturnsFalse()
        {
            var dna = NewDna();
            dna.AddPoints(3);
            Assert.That(_service.Load(dna, NewArchive()), Is.False);
            Assert.That(dna.FreePoints, Is.EqualTo(3));
        }

        [Test]
        public void CorruptedTextThrowsFormatException()
        {
            _store.Text = "v9\npoints=1\n";
            Assert.Throws<ProfileFormatException>(() => _service.Load(NewDna(), NewArchive()));
            _store.Text = "v1\npoints=abc\n";
            Assert.Throws<ProfileFormatException>(() => _service.Load(NewDna(), NewArchive()));
            _store.Text = "v1\nnode=x\n";
            Assert.Throws<ProfileFormatException>(() => _service.Load(NewDna(), NewArchive()));
        }

        [Test]
        public void UnknownNodeIsRejectedAndKeepsState()
        {
            _store.Text = "v1\npoints=4\nnode=Fantasma-T1-a\n";
            var dna = NewDna();
            dna.AddPoints(1);
            Assert.Throws<ArgumentException>(() => _service.Load(dna, NewArchive()));
            Assert.That(dna.FreePoints, Is.EqualTo(1));
        }
    }
}
