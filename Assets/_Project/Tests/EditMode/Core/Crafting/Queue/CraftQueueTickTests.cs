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
    public class CraftQueueTickTests
    {
        private Workstation _forge;
        private FixedRoller _roller;
        private CraftQueue _queue;
        private ProfessionSkill _smith;

        [SetUp]
        public void SetUp()
        {
            _forge = new Workstation(StationType.Fragua);
            _roller = new FixedRoller();
            _queue = new CraftQueue(_forge, _roller);
            _smith = new ProfessionSkill(ProfessionId.Herrero);
            Put(_forge.Input, Ore, 6);
        }

        [Test]
        public void SinCombustibleSePausaSinPerderNada()
        {
            _queue.Enqueue(Forge(), _smith);
            _queue.Tick(50f);
            Assert.AreEqual(CraftQueueState.NoFuel, _queue.State);
            Assert.AreEqual(0f, _queue.Jobs[0].Elapsed);
            Put(_forge.Fuel, Wood, 1);
            _queue.Tick(10f);
            Assert.AreEqual(1, _forge.Output.CountOf(Sword.Id));
        }

        [Test]
        public void CadaUnidadArdeSuTiempoYElFuelleAhorra()
        {
            Put(_forge.Fuel, Wood, 3);
            _queue.Enqueue(Forge(40f), _smith);
            _queue.Tick(40f);
            Assert.AreEqual(1, _forge.Fuel.CountOf(Wood.Id));
            _forge.PlacePart(new StationPart(new StationPartDefinition("fuelle", StationType.Fragua, fuelSaving: 0.25f)));
            _queue.Enqueue(Forge(60f), _smith);
            _queue.Tick(60f);
            Assert.AreEqual(0, _forge.Fuel.CountOf(Wood.Id));
            Assert.AreEqual(2, _forge.Output.CountOf(Sword.Id));
        }

        [Test]
        public void ConLaSalidaLlenaEsperaYNoRepiteLaTirada()
        {
            Put(_forge.Fuel, Wood, 1);
            for (int i = 0; i < Workstation.OutputSlots; i++) _forge.Output.Add(ItemInstance.CreateNew(Sword));
            _queue.Enqueue(Forge(5f), _smith);
            _queue.Tick(10f);
            Assert.AreEqual(CraftQueueState.OutputFull, _queue.State);
            Assert.AreEqual(1, _queue.Jobs.Count);
            _queue.Tick(1f);
            _forge.Output.Remove(Sword.Id, 1);
            _queue.Tick(0f);
            Assert.AreEqual(0, _queue.Jobs.Count);
            Assert.AreEqual(1, _roller.Calls);
            Assert.AreEqual(25f, _queue.BurnLeft, 1e-3f);
        }

        [Test]
        public void AlTerminarUsaLosDatosDeEncolarYDaXpAQuienEncolo()
        {
            Put(_forge.Fuel, Wood, 1);
            _queue.Enqueue(Forge(xp: 25), _smith);
            _smith.Gain(_smith.Curve.XpToNext(1));
            CraftJob done = null;
            _queue.Completed += j => done = j;
            _queue.Tick(10f);
            Assert.AreEqual(1, _roller.Last.ProfessionLevel);
            Assert.AreEqual(Quality.Good, _forge.Output.Stacks[0].Item.Quality);
            Assert.AreSame(_smith, done.Crafter);
            Assert.AreEqual(2, _smith.Level);
            Assert.AreEqual(25, _smith.Xp);
            Assert.AreEqual(CraftQueueState.Idle, _queue.State);
        }

        [Test]
        public void LaColaEncadenaTrabajosEnUnMismoAvance()
        {
            Put(_forge.Fuel, Wood, 1);
            _queue.Enqueue(Forge(), _smith);
            _queue.Enqueue(Forge(), _smith);
            _queue.Tick(15f);
            Assert.AreEqual(1, _forge.Output.CountOf(Sword.Id));
            Assert.AreEqual(5f, _queue.Jobs[0].Elapsed, 1e-3f);
            Assert.AreEqual(CraftQueueState.Working, _queue.State);
        }
    }
}
