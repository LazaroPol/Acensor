using System;
using NUnit.Framework;
using Specimen.Core.Lore;

namespace Specimen.Tests.Core.Lore
{
    [TestFixture]
    public class MemoryArchiveTests
    {
        private MemoryArchive _archive;

        [SetUp]
        public void SetUp() => _archive = new MemoryArchive(new[]
        {
            new LoreEntry("clon", "Eres un clon", "diario-1"),
            new LoreEntry("ilse", "La primera Specimen", "diario-2", "foto-ilse")
        });

        [Test]
        public void SingleFragmentUnlocksItsEntry()
        {
            var fresh = _archive.Collect("diario-1");
            Assert.That(fresh, Has.Count.EqualTo(1));
            Assert.That(_archive.IsUnlocked("clon"), Is.True);
        }

        [Test]
        public void EntryNeedsAllItsFragments()
        {
            Assert.That(_archive.Collect("diario-2"), Is.Empty);
            Assert.That(_archive.IsUnlocked("ilse"), Is.False);
            Assert.That(_archive.Collect("foto-ilse")[0].Id, Is.EqualTo("ilse"));
        }

        [Test]
        public void DuplicateFragmentUnlocksNothingNew()
        {
            _archive.Collect("diario-1");
            Assert.That(_archive.Collect("diario-1"), Is.Empty);
            Assert.That(_archive.FragmentCount, Is.EqualTo(1));
        }

        [Test]
        public void EntryUnlocksOnlyOnce()
        {
            _archive.Collect("diario-1");
            Assert.That(_archive.UnlockedEntries, Has.Count.EqualTo(1));
            _archive.Collect("otro");
            Assert.That(_archive.UnlockedEntries, Has.Count.EqualTo(1));
        }

        [Test]
        public void InvalidInputThrows()
        {
            Assert.Throws<ArgumentException>(() => _archive.Collect(" "));
            Assert.Throws<ArgumentException>(() => new LoreEntry("x", "t"));
            Assert.Throws<ArgumentException>(() => new MemoryArchive(new[]
                { new LoreEntry("a", "A", "f"), new LoreEntry("a", "B", "g") }));
        }
    }
}
