using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Mods;

namespace Specimen.Tests.Core.Crafting.Mods
{
    /// <summary>Catálogo de mods de §17.5 (OF-11).</summary>
    [TestFixture]
    public class DefaultModsCatalogTests
    {
        [Test]
        public void HasAtLeast15ModsWithUniquePrefixedIds()
        {
            var all = DefaultMods.All;
            Assert.That(all.Count, Is.GreaterThanOrEqualTo(15));
            Assert.That(all.All(m => m != null && m.Id.StartsWith("mod.")), Is.True);
            Assert.That(all.Select(m => m.Id).Distinct().Count(), Is.EqualTo(all.Count));
            Assert.That(all.All(m => m.Modifiers.Count > 0), Is.True);
        }

        [Test]
        public void EverySlotTypeHasAMod()
        {
            foreach (ModSlotType slot in Enum.GetValues(typeof(ModSlotType)))
                Assert.That(DefaultMods.All.Any(m => m.SlotType == slot), Is.True, slot.ToString());
        }

        [Test]
        public void OnlyTheResinIsDestroyedOnRemoval()
            => CollectionAssert.AreEqual(new[] { DefaultMods.ResinaIgnea },
                DefaultMods.All.Where(m => !m.Removable).Select(m => m.Id));

        [Test]
        public void ArmorModsFitArmorOnly()
        {
            Assert.That(DefaultMods.PlacasCosidas.FitsTags(GearTags.Armadura), Is.True);
            Assert.That(DefaultMods.PlacasCosidas.FitsTags(GearTags.Arma), Is.False);
            Assert.That(DefaultMods.PuntaHueso.FitsTags(GearTags.Flecha), Is.True);
        }

        [Test]
        public void GemBoostsAllAndCostsDurability()
        {
            var gem = DefaultMods.GemaTallada;
            Assert.That(gem.Modifiers.Count(m => m.Value > 0f), Is.EqualTo(5));
            Assert.That(gem.Modifiers.Single(m => m.Stat == GearStat.Durabilidad).Value, Is.EqualTo(-0.1f));
        }
    }
}
