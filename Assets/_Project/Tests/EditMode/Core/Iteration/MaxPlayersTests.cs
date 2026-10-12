using NUnit.Framework;
using Specimen.Core.Iteration;

namespace Specimen.Tests.Core.Iteration
{
    public class MaxPlayersTests
    {
        [Test]
        public void TeamOfSix_IsAllowed()
        {
            Assert.That(new IterationConfigBuilder().WithMaxPlayers(6).Build().MaxPlayers, Is.EqualTo(6));
            Assert.That(IterationConfigBuilder.Default.MaxPlayers, Is.EqualTo(6));
        }

        [Test]
        public void SevenPlayers_IsRejected()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new IterationConfigBuilder().WithMaxPlayers(7).Build());
        }
    }
}
