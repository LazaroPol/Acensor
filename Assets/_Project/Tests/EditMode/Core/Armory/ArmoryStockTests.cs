using System;
using NUnit.Framework;
using Specimen.Core.Armory;

namespace Specimen.Tests.Core.Armory
{
    [TestFixture]
    public class ArmoryStockTests
    {
        private ArmoryStock<string> _stock;

        [SetUp]
        public void SetUp()
        {
            _stock = new ArmoryStock<string>();
            _stock.Define("escudo", 2);
        }

        [Test]
        public void UndefinedItemHasNoStock()
        {
            Assert.That(_stock.Remaining("lanza"), Is.EqualTo(0));
            Assert.That(_stock.TryTake("lanza"), Is.False);
        }

        [Test]
        public void TakeUntilEmptyThenFails()
        {
            Assert.That(_stock.TryTake("escudo"), Is.True);
            Assert.That(_stock.TryTake("escudo"), Is.True);
            Assert.That(_stock.TryTake("escudo"), Is.False);
            Assert.That(_stock.Remaining("escudo"), Is.EqualTo(0));
        }

        [Test]
        public void ReturnFreesUnitButNotAboveCapacity()
        {
            _stock.TryTake("escudo");
            Assert.That(_stock.Return("escudo"), Is.True);
            Assert.That(_stock.Return("escudo"), Is.False);
            Assert.That(_stock.Remaining("escudo"), Is.EqualTo(2));
        }

        [Test]
        public void RestockRefillsEverything()
        {
            _stock.TryTake("escudo");
            _stock.TryTake("escudo");
            _stock.Restock();
            Assert.That(_stock.Remaining("escudo"), Is.EqualTo(_stock.Capacity("escudo")));
        }

        [Test]
        public void StockChangedReportsRemaining()
        {
            var last = -1;
            _stock.StockChanged += (_, remaining) => last = remaining;
            _stock.TryTake("escudo");
            Assert.That(last, Is.EqualTo(1));
        }

        [Test]
        public void NegativeCapacityThrows()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _stock.Define("x", -1));
        }
    }
}
