using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;
using static Specimen.Tests.Core.Crafting.Queue.QueueFixtures;

namespace Specimen.Tests.Core.Crafting.Queue
{
    [TestFixture]
    public class CraftQueueTests
    {
        private Workstation _bench;
        private CraftQueue _queue;
        private ProfessionSkill _smith;

        [SetUp]
        public void SetUp()
        {
            _bench = new Workstation(StationType.BancoCantero);
            _queue = new CraftQueue(_bench, new FixedRoller());
            _smith = new ProfessionSkill(ProfessionId.Herrero);
        }

        private static RecipeDefinition Stone(int ore = 3) => Forge(ore: ore, station: StationType.BancoCantero);

        [Test]
        public void EncolarRetiraLosMaterialesDeLaEntrada()
        {
            Put(_bench.Input, Ore, 5);
            Assert.AreEqual(EnqueueResult.Queued, _queue.Enqueue(Stone(), _smith));
            Assert.AreEqual(2, _bench.Input.CountOf(Ore.Id));
            Assert.AreEqual(1, _queue.Jobs.Count);
        }

        [Test]
        public void SiFaltanTiraDeLaBolsaDelJugador()
        {
            var bag = new Inventory(4);
            Put(_bench.Input, Ore, 1);
            Put(bag, Ore, 4);
            Assert.AreEqual(EnqueueResult.Queued, _queue.Enqueue(Stone(), _smith, bag));
            Assert.AreEqual(0, _bench.Input.CountOf(Ore.Id));
            Assert.AreEqual(2, bag.CountOf(Ore.Id));
        }

        [Test]
        public void EncolarPorEtiquetaRetiraElMetal()
        {
            var forge = new Workstation(StationType.Fragua);
            Put(forge.Input, Iron, 1);
            Put(forge.Input, Steel, 2);
            Assert.AreEqual(EnqueueResult.Queued, new CraftQueue(forge, new FixedRoller()).Enqueue(ByTag(), _smith));
            Assert.AreEqual(0, forge.Input.CountOf(Iron.Id));
            Assert.AreEqual(1, forge.Input.CountOf(Steel.Id));
        }

        [Test]
        public void SinMaterialesNoSeEncolaNiSeTocaNada()
        {
            Put(_bench.Input, Ore, 2);
            Assert.AreEqual(EnqueueResult.MissingMaterials, _queue.Enqueue(Stone(), _smith));
            Assert.AreEqual(2, _bench.Input.CountOf(Ore.Id));
            Assert.AreEqual(0, _queue.Jobs.Count);
        }

        [Test]
        public void RechazaOtraEstacionYColaLlena()
        {
            Put(_bench.Input, Ore, 10);
            Assert.AreEqual(EnqueueResult.WrongStation, _queue.Enqueue(Forge(), _smith));
            _queue.Enqueue(Stone(1), _smith);
            _queue.Enqueue(Stone(1), _smith);
            Assert.AreEqual(EnqueueResult.QueueFull, _queue.Enqueue(Stone(1), _smith));
            Assert.AreEqual(8, _bench.Input.CountOf(Ore.Id));
        }

        [Test]
        public void CancelarUnPendienteDevuelveLosMateriales()
        {
            var bag = new Inventory(4);
            Put(bag, Ore, 3);
            _queue.Enqueue(Stone(), _smith, bag);
            Assert.IsTrue(_queue.Cancel(_queue.Jobs[0]));
            Assert.AreEqual(3, _bench.Input.CountOf(Ore.Id) + bag.CountOf(Ore.Id));
            Assert.AreEqual(0, _queue.Jobs.Count);
            Assert.AreEqual(CraftQueueState.Idle, _queue.State);
        }

        [Test]
        public void NoSeCancelaUnTrabajoEmpezado()
        {
            Put(_bench.Input, Ore, 3);
            _queue.Enqueue(Stone(), _smith);
            _queue.Tick(1f);
            Assert.IsFalse(_queue.Cancel(_queue.Jobs[0]));
            Assert.AreEqual(0, _bench.Input.CountOf(Ore.Id));
        }
    }
}
