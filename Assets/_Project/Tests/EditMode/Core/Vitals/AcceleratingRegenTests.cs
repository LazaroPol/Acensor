using NUnit.Framework;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Vitals
{
    public class AcceleratingRegenTests
    {
        private ResourcePool _mana;
        private AcceleratingRegen _regen;

        [SetUp]
        public void SetUp()
        {
            _mana = new ResourcePool(new Stat(100f));
            _mana.TrySpend(90f);
            _regen = new AcceleratingRegen(_mana, 1f, 2f, 2f, 5f);
        }

        [Test]
        public void BeforeQuietWindow_RegensAtBaseRate()
        {
            _regen.Tick(1f);
            Assert.That(_regen.Multiplier, Is.EqualTo(1f));
            Assert.That(_mana.Current, Is.EqualTo(11f).Within(0.001f));
        }

        [Test]
        public void AfterQuietWindow_RateRampsUpToMax()
        {
            _regen.Tick(2f);
            Assert.That(_regen.Multiplier, Is.EqualTo(1f));
            _regen.Tick(1f);
            Assert.That(_regen.Multiplier, Is.EqualTo(3f).Within(0.001f));
            _regen.Tick(5f);
            Assert.That(_regen.Multiplier, Is.EqualTo(5f));
        }

        [Test]
        public void Spending_ResetsToBaseRate()
        {
            _regen.Tick(5f);
            Assert.That(_regen.Multiplier, Is.EqualTo(5f));
            _mana.TrySpend(5f);
            Assert.That(_regen.Multiplier, Is.EqualTo(1f));
        }

        [Test]
        public void FullPool_DoesNotOverflow()
        {
            _mana.Fill();
            _regen.Tick(100f);
            Assert.That(_mana.Current, Is.EqualTo(100f));
        }
    }
}
