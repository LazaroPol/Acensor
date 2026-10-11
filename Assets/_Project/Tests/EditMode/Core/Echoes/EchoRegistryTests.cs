using System;
using NUnit.Framework;
using Specimen.Core.Echoes;

namespace Specimen.Tests.Core.Echoes
{
    public class EchoRegistryTests
    {
        [Test]
        public void Decision_RejectsInvalidValues()
        {
            Assert.Throws<ArgumentException>(() => new EchoDecision(" ", 1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EchoDecision("x", 0, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EchoDecision("x", 1, -1));
        }

        [Test]
        public void Empty_RegistryKnowsNothing()
        {
            var r = new EchoRegistry();
            Assert.That(r.Count, Is.EqualTo(0));
            Assert.That(r.IsDecided("prisionero"), Is.False);
            Assert.That(r.IsDecided(null), Is.False);
            Assert.That(r.ChosenOption("prisionero"), Is.Null);
        }

        [Test]
        public void Record_StoresDecision_InOrder()
        {
            var r = new EchoRegistry();
            r.Record(new EchoDecision("prisionero", 2, 1));
            r.Record(new EchoDecision("sello", 3, 0));
            Assert.That(r.ChosenOption("prisionero"), Is.EqualTo(1));
            Assert.That(r.Chose("sello", 0), Is.True);
            Assert.That(r.Chose("sello", 1), Is.False);
            Assert.That(r.Decisions[0].EchoId, Is.EqualTo("prisionero"));
            Assert.That(r.Count, Is.EqualTo(2));
        }

        [Test]
        public void Record_RejectsNullAndDuplicates()
        {
            var r = new EchoRegistry();
            Assert.Throws<ArgumentNullException>(() => r.Record(null));
            r.Record(new EchoDecision("sello", 3, 0));
            Assert.Throws<InvalidOperationException>(() => r.Record(new EchoDecision("sello", 4, 1)));
            Assert.That(r.ChosenOption("sello"), Is.EqualTo(0));
        }

        [Test]
        public void DecidedOnFloor_FiltersByFloor()
        {
            var r = new EchoRegistry();
            r.Record(new EchoDecision("a", 2, 0));
            r.Record(new EchoDecision("b", 3, 0));
            r.Record(new EchoDecision("c", 2, 1));
            Assert.That(r.DecidedOnFloor(2), Has.Count.EqualTo(2));
            Assert.That(r.DecidedOnFloor(4), Is.Empty);
        }

        [Test]
        public void Clear_ForgetsEverything()
        {
            var r = new EchoRegistry();
            r.Record(new EchoDecision("a", 2, 0));
            r.Clear();
            Assert.That(r.Count, Is.EqualTo(0));
            Assert.That(r.IsDecided("a"), Is.False);
            Assert.DoesNotThrow(() => r.Record(new EchoDecision("a", 2, 1)));
        }
    }
}
