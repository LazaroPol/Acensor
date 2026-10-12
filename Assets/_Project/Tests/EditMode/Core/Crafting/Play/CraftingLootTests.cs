using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Play;
using Specimen.Core.Items;
using Specimen.Core.Loot;
using Specimen.Core.Randomness;

namespace Specimen.Tests.Core.Crafting.Play
{
    /// <summary>OF-16: conocimientos, piezas y mods aparecen en el botín del poblado.</summary>
    [TestFixture]
    public class CraftingLootTests
    {
        private static readonly CraftingLoot Loot = new CraftingLoot(PlayFixtures.Catalog);

        [Test]
        public void LaTablaTieneConocimientosPiezasYMods()
        {
            var ids = Loot.Table.Entries.Select(e => e.Item.Id).ToList();
            Assert.Contains(PlayFixtures.Scroll().Id, ids);
            Assert.Contains("pieza.crisol", ids);
            Assert.IsTrue(ids.Any(id => id.StartsWith("mod.")));
            Assert.IsTrue(Loot.Table.HasLootIn(DepthRings.Min));
        }

        [Test]
        public void LosConocimientosAltosSoloSalenEnAnillosProfundos()
        {
            var high = PlayFixtures.Highest();
            var entry = Loot.Table.Entries.Single(e => e.Item.Id == high.Id);
            Assert.IsFalse(entry.AvailableIn(DepthRings.Min));
            Assert.IsTrue(entry.AvailableIn(DepthRings.Max));
        }

        [Test]
        public void ConProbabilidadUnoSiempreSueltaAlgoYConCeroNada()
        {
            var random = new Pcg32Random(11);
            for (int i = 0; i < 20; i++) Assert.AreEqual(1, Loot.Roll(DepthRings.Min, random, 1f).Count);
            Assert.IsEmpty(Loot.Roll(DepthRings.Min, random, 0f));
        }

        [Test]
        public void ElBotinDeOficioSeRecogeEnLaBolsa()
        {
            var drop = Loot.Roll(DepthRings.Min, new Pcg32Random(2), 1f).Single();
            var bag = new Inventory(4);
            Assert.AreEqual(drop.Quantity, bag.Add(ItemInstance.CreateNew(drop.Item), drop.Quantity));
        }
    }
}
