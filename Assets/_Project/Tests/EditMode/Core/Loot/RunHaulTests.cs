using NUnit.Framework;
using Specimen.Core.Loot;

namespace Specimen.Tests.Core.Loot
{
    public class RunHaulTests
    {
        [Test]
        public void Add_Accumulates_AndClearResets()
        {
            var haul = new RunHaul();
            haul.Add(2);
            haul.Add(3);
            Assert.That(haul.Items, Is.EqualTo(5));
            Assert.That(() => haul.Add(-1), Throws.InstanceOf<System.ArgumentOutOfRangeException>());
            haul.Clear();
            Assert.That(haul.Items, Is.EqualTo(0));
        }
    }
}
