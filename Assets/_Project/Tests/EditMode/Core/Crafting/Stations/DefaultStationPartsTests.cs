using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;

namespace Specimen.Tests.Core.Crafting.Stations
{
    /// <summary>Piezas de estación de §17.5 (OF-11).</summary>
    [TestFixture]
    public class DefaultStationPartsTests
    {
        [Test]
        public void HasAtLeast19PartsWithUniquePrefixedIds()
        {
            var all = DefaultStationParts.All;
            Assert.That(all.Count, Is.GreaterThanOrEqualTo(19));
            Assert.That(all.Select(p => p.Id).Distinct().Count(), Is.EqualTo(all.Count));
            Assert.That(all.All(p => p.Id.StartsWith(DefaultStationParts.Prefix)), Is.True);
            Assert.That(all.All(p => p.Item.Id == p.Id && !string.IsNullOrWhiteSpace(p.Enables)), Is.True);
        }

        [Test]
        public void EveryStationHasAtLeastTwoParts()
        {
            foreach (StationType station in Enum.GetValues(typeof(StationType)))
                Assert.That(DefaultStationParts.For(station).Count(), Is.GreaterThanOrEqualTo(2), station.ToString());
        }

        [Test]
        public void BonusesMatchTheTable()
        {
            Assert.That(DefaultStationParts.TryGet("pieza.fuelle_cuero", out var fuelle), Is.True);
            Assert.That(fuelle.Definition.TimeBonus, Is.EqualTo(0.2f));
            Assert.That(fuelle.Definition.FuelSaving, Is.EqualTo(0.25f));
            Assert.That(DefaultStationParts.TryGet("pieza.yunque_acero", out var yunque), Is.True);
            Assert.That(yunque.Definition.QualityBonus, Is.EqualTo(3));
            Assert.That(DefaultStationParts.TryGet("pieza.crisol", out var crisol), Is.True);
            Assert.That(crisol.HasBonus, Is.False);
            Assert.That(DefaultStationParts.TryGet("pieza.nada", out _), Is.False);
        }

        [Test]
        public void EntryRequiresNameAndEnables()
        {
            var def = new StationPartDefinition("pieza.x", StationType.Fragua);
            Assert.Throws<ArgumentException>(() => new StationPartEntry(def, " ", "algo", 1f));
            Assert.Throws<ArgumentException>(() => new StationPartEntry(def, "X", "", 1f));
        }
    }
}
