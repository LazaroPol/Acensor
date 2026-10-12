using System;
using NUnit.Framework;
using Specimen.Core.Echoes.Secrets;

namespace Specimen.Tests.Core.Echoes.Secrets
{
    public class SecretEchoLedgerTests
    {
        private static SecretDecision D(int voter, string id = "traicion", int option = 1, int reveal = 4)
            => new SecretDecision(voter, id, 2, option, reveal);

        [Test]
        public void Decision_RejectsInvalidValues()
        {
            Assert.Throws<ArgumentException>(() => new SecretDecision(1, " ", 2, 0, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => new SecretDecision(1, "x", 0, 0, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => new SecretDecision(1, "x", 2, -1, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => new SecretDecision(1, "x", 2, 0, 2));
        }

        [Test]
        public void Record_RejectsNullAndSecondDecisionOfSameVoter()
        {
            var l = new SecretEchoLedger();
            Assert.Throws<ArgumentNullException>(() => l.Record(null));
            l.Record(D(1));
            Assert.Throws<InvalidOperationException>(() => l.Record(D(1, option: 0)));
            Assert.DoesNotThrow(() => l.Record(D(2)));
            Assert.DoesNotThrow(() => l.Record(D(1, "otro")));
            Assert.That(l.Count, Is.EqualTo(3));
        }

        [Test]
        public void Owner_AlwaysSeesOwnChoice_OthersOnlyAfterReveal()
        {
            var l = new SecretEchoLedger();
            l.Record(D(1, option: 1, reveal: 4));
            Assert.That(l.ChoiceVisibleTo(1, 1, "traicion", 2), Is.EqualTo(1));
            Assert.That(l.ChoiceVisibleTo(2, 1, "traicion", 3), Is.Null);
            Assert.That(l.ChoiceVisibleTo(2, 1, "traicion", 4), Is.EqualTo(1));
        }

        [Test]
        public void UnknownDecision_IsNotVisible()
            => Assert.That(new SecretEchoLedger().ChoiceVisibleTo(1, 2, "x", 9), Is.Null);

        [Test]
        public void RevealedAt_And_RevealedOn_FilterByFloor()
        {
            var l = new SecretEchoLedger();
            l.Record(D(1, reveal: 4));
            l.Record(D(2, reveal: 5));
            Assert.That(l.RevealedAt(3), Is.Empty);
            Assert.That(l.RevealedAt(4), Has.Count.EqualTo(1));
            Assert.That(l.RevealedAt(6), Has.Count.EqualTo(2));
            Assert.That(l.RevealedOn(5), Has.Count.EqualTo(1));
            Assert.That(l.RevealedOn(5)[0].Voter, Is.EqualTo(2));
        }

        [Test]
        public void Clear_ForgetsEverything()
        {
            var l = new SecretEchoLedger();
            l.Record(D(1));
            l.Clear();
            Assert.That(l.Count, Is.EqualTo(0));
            Assert.DoesNotThrow(() => l.Record(D(1)));
        }
    }
}
