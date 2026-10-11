using System;
using NUnit.Framework;
using Specimen.Core.Iteration.Readiness;

namespace Specimen.Tests.Core.Iteration.Readiness
{
    public class ReadyCheckTests
    {
        [Test]
        public void AllReady_OnlyWhenEveryPlayerMarked()
        {
            var r = new ReadyCheck(new[] { 1, 2, 3 });
            r.MarkReady(1); r.MarkReady(2);
            Assert.That(r.AllReady, Is.False);
            r.MarkReady(3);
            Assert.That((r.AllReady, r.ReadyCount), Is.EqualTo((true, 3)));
        }

        [Test]
        public void MarkingTwice_CountsOnce_AndUnmarkWorks()
        {
            var r = new ReadyCheck(new[] { 1, 2 });
            r.MarkReady(1); r.MarkReady(1);
            Assert.That(r.ReadyCount, Is.EqualTo(1));
            r.Unmark(1);
            Assert.That(r.IsReady(1), Is.False);
        }

        [Test]
        public void Reset_ClearsEveryone()
        {
            var r = new ReadyCheck(new[] { 1 });
            r.MarkReady(1);
            r.Reset();
            Assert.That(r.AllReady, Is.False);
        }

        [Test]
        public void UnknownPlayers_AndEmptyGroups_AreRejected()
        {
            Assert.Throws<ArgumentException>(() => new ReadyCheck(new int[0]));
            var r = new ReadyCheck(new[] { 1 });
            Assert.Throws<ArgumentException>(() => r.MarkReady(9));
            Assert.Throws<ArgumentException>(() => r.Unmark(9));
        }
    }
}
