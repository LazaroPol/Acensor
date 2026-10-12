using NUnit.Framework;
using Specimen.Core.Lore;

namespace Specimen.Tests.Core.Lore
{
    [TestFixture]
    public class FragmentLedgerTests
    {
        [Test]
        public void CountsDistinctFragmentsPerPlayer()
        {
            var ledger = new FragmentLedger();
            Assert.That(ledger.Collect(1, "a"), Is.True);
            Assert.That(ledger.Collect(1, "a"), Is.False);
            ledger.Collect(1, "b");
            ledger.Collect(2, "a");
            Assert.That(ledger.FragmentsOf(1), Is.EqualTo(2));
            Assert.That(ledger.FragmentsOf(2), Is.EqualTo(1));
            Assert.That(ledger.FragmentsOf(3), Is.EqualTo(0));
        }
    }
}
