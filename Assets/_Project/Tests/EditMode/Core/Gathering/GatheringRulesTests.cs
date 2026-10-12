using System;
using NUnit.Framework;
using Specimen.Core.Gathering;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Gathering
{
    [TestFixture]
    public class GatheringRulesTests
    {
        [TestCase(GatherKind.Arbol, ItemIds.Madera)]
        [TestCase(GatherKind.Roca, ItemIds.Piedra)]
        [TestCase(GatherKind.VetaHierro, ItemIds.MineralHierro)]
        [TestCase(GatherKind.Hierba, ItemIds.Hierba)]
        public void EachNodeGivesItsMaterial(GatherKind kind, string item)
        {
            var random = new Pcg32Random(3UL);
            for (int i = 0; i < 20; i++)
            {
                var drops = GatheringRules.Gather(kind, random);
                Assert.That(drops[0].Item.Id, Is.EqualTo(item));
                Assert.That(drops[0].Quantity, Is.InRange(1, 3));
            }
        }

        [Test]
        public void EveryKindHasAName()
        {
            foreach (GatherKind kind in Enum.GetValues(typeof(GatherKind)))
                Assert.That(GatheringRules.NameOf(kind), Is.Not.Empty);
        }
    }
}
