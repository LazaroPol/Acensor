using System;
using NUnit.Framework;
using Specimen.Core.Floors.Remnants;

namespace Specimen.Tests.Core.Floors.Remnants
{
    public class MemoryFragmentLogTests
    {
        [Test]
        public void Collect_RegistersOnce_AndTracksMissing()
        {
            var log = new MemoryFragmentLog(3);
            Assert.That(log.Collect(1), Is.True);
            Assert.That(log.Collect(1), Is.False);
            Assert.That(log.Has(1), Is.True);
            Assert.That(log.Count, Is.EqualTo(1));
            Assert.That(log.Missing(), Is.EqualTo(new[] { 0, 2 }));
        }

        [Test]
        public void IsComplete_WhenEveryFragmentIsCollected()
        {
            var log = new MemoryFragmentLog(2);
            log.Collect(0);
            Assert.That(log.IsComplete, Is.False);
            log.Collect(1);
            Assert.That(log.IsComplete, Is.True);
            Assert.That(log.Missing(), Is.Empty);
        }

        [Test]
        public void RejectsInvalidCatalogAndIds()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MemoryFragmentLog(0));
            var log = new MemoryFragmentLog(2);
            Assert.Throws<ArgumentOutOfRangeException>(() => log.Collect(2));
            Assert.Throws<ArgumentOutOfRangeException>(() => log.Has(-1));
        }
    }
}
