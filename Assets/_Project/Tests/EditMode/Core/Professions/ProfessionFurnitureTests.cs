using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Professions
{
    [TestFixture]
    public class ProfessionFurnitureTests
    {
        [Test]
        public void EveryProfessionHasAtLeastTwoDistinctTools()
        {
            foreach (ProfessionId profession in Enum.GetValues(typeof(ProfessionId)))
            {
                var tools = ProfessionFurniture.ToolsFor(profession);
                Assert.That(tools.Count, Is.GreaterThanOrEqualTo(2), profession.ToString());
                Assert.That(tools.Distinct().Count(), Is.EqualTo(tools.Count), profession.ToString());
            }
        }

        [Test]
        public void BlacksmithWorksOnAnAnvil()
            => Assert.That(ProfessionFurniture.ToolsFor(ProfessionId.Herrero), Does.Contain("anvil"));
    }
}
