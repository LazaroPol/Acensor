using NUnit.Framework;
using Specimen.Core.Prototype;

namespace Specimen.Tests.Core.Prototype
{
    [TestFixture]
    public class HudTimeTests
    {
        [TestCase(0f, "00:00")]
        [TestCase(0.2f, "00:01")]
        [TestCase(59f, "00:59")]
        [TestCase(60f, "01:00")]
        [TestCase(299.5f, "05:00")]
        [TestCase(-3f, "00:00")]
        public void FormatsAsMinutesAndSecondsRoundingUp(float seconds, string expected)
        {
            Assert.That(HudTime.Format(seconds), Is.EqualTo(expected));
        }
    }
}
