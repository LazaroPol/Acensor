using System;
using NUnit.Framework;
using Specimen.Core.Time;

namespace Specimen.Tests.Core.Time
{
    public class ManualClockTests
    {
        [Test]
        public void StartsAtGivenTime() => Assert.That(new ManualClock(5).Now, Is.EqualTo(5d));

        [Test]
        public void AdvanceAndTickAccumulate()
        {
            var clock = new ManualClock();
            clock.Advance(1.5f);
            clock.Tick(0.5f);
            Assert.That(clock.Now, Is.EqualTo(2d).Within(1e-6));
        }

        [Test]
        public void CannotGoBackwards() => Assert.Throws<ArgumentOutOfRangeException>(() => new ManualClock().Advance(-1f));
    }
}
