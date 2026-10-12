using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Tests.Core.Crafting.Queue
{
    /// <summary>Combustibles de §17.5 en la tabla por defecto (OF-11).</summary>
    [TestFixture]
    public class DefaultFuelTests
    {
        private static readonly FuelTable Fuels = FuelTable.Default;

        [TestCase(FuelIds.Lena, 30f), TestCase(FuelIds.CarbonVegetal, 90f)]
        [TestCase(FuelIds.Hulla, 180f), TestCase(FuelIds.AceiteLampara, 60f), TestCase(ItemIds.Madera, 30f)]
        public void BurnTimesMatchTheTable(string id, float seconds)
            => Assert.That(Fuels.BurnSeconds(id), Is.EqualTo(seconds));

        [Test]
        public void AnyTier1WoodBurnsHalf()
        {
            Assert.That(Fuels.BurnSeconds("rama_seca"), Is.EqualTo(FuelTable.MaderaTier1));
            Assert.That(Fuels.IsFuel("tablon_roble"), Is.False);
        }

        [Test]
        public void EveryFuelIsACatalogItem()
        {
            foreach (var id in Fuels.Ids) Assert.That(ItemCatalog.TryGet(id, out _), Is.True, id);
            Assert.That(ItemCatalog.Get(FuelIds.Hulla).HasTag(MaterialTags.Combustible), Is.True);
        }

        [Test]
        public void LampOilOnlyBurnsInTheAlchemyTable()
        {
            Assert.That(Fuels.CanBurnIn(FuelIds.AceiteLampara, StationType.MesaAlquimia), Is.True);
            Assert.That(Fuels.CanBurnIn(FuelIds.AceiteLampara, StationType.Fragua), Is.False);
            Assert.That(Fuels.CanBurnIn(FuelIds.Lena, StationType.Fragua), Is.True);
            Assert.That(Fuels.CanBurnIn("piedra", StationType.Fragua), Is.False);
        }

        [Test]
        public void RestrictionWithoutFuelFailsFast()
            => Assert.Throws<System.ArgumentException>(() => new FuelTable(
                new[] { new System.Collections.Generic.KeyValuePair<string, float>("a", 1f) },
                new[] { new System.Collections.Generic.KeyValuePair<string, StationType>("b", StationType.Fragua) }));
    }
}
