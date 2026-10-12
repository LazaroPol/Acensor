using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;
using static Specimen.Tests.Core.Crafting.Queue.QueueFixtures;

namespace Specimen.Tests.Core.Crafting.Queue
{
    [TestFixture]
    public class CraftTimingTests
    {
        [Test]
        public void AplicaBonusDeOficioYDePiezas()
        {
            Assert.AreEqual(72f, CraftTiming.Duration(100f, 0.1f, 0.2f), 1e-3f);
        }

        [Test]
        public void ComoMinimoUnSegundo()
        {
            Assert.AreEqual(1f, CraftTiming.Duration(0f, 0f, 0f));
            Assert.AreEqual(1f, CraftTiming.Duration(1.5f, 0.3f, 0.5f));
        }

        [Test]
        public void ElFuelleAlargaCadaUnidadDeCombustible()
        {
            Assert.AreEqual(40f, CraftTiming.BurnTime(30f, 0.25f), 1e-3f);
        }

        [Test]
        public void ElTrabajoUsaElHitoDeQuienEncolaYLasPiezas()
        {
            var bench = new Workstation(StationType.BancoCantero);
            bench.PlacePart(new StationPart(new StationPartDefinition("cincel", StationType.BancoCantero,
                timeBonus: 0.5f)));
            var mason = new ProfessionSkill(ProfessionId.Herrero);
            while (mason.Level < 5) mason.Gain(10);
            Put(bench.Input, Ore, 3);
            var queue = new CraftQueue(bench, new FixedRoller());
            queue.Enqueue(Forge(100f, station: StationType.BancoCantero), mason);
            Assert.AreEqual(45f, queue.Jobs[0].Duration, 1e-3f);
        }
    }
}
