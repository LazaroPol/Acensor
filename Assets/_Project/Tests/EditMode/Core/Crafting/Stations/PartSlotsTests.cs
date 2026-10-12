using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using static Specimen.Tests.Core.Crafting.Stations.StationFixtures;

namespace Specimen.Tests.Core.Crafting.Stations
{
    [TestFixture]
    public class PartSlotsTests
    {
        [Test]
        public void ColocarYRetirarPiezaCompatible()
        {
            var forge = new Workstation(StationType.Fragua);
            var crisol = new StationPart(Crisol);
            Assert.AreEqual(PartPlaceResult.Placed, forge.PlacePart(crisol));
            Assert.IsTrue(forge.Parts.HasWorking("crisol"));
            Assert.AreSame(crisol, forge.RemovePart("crisol"));
            Assert.IsFalse(forge.Parts.HasWorking("crisol"));
            Assert.IsNull(forge.RemovePart("crisol"));
        }

        [Test]
        public void PiezaDeOtraEstacionNoEntra()
        {
            var forge = new Workstation(StationType.Fragua);
            Assert.AreEqual(PartPlaceResult.Incompatible, forge.PlacePart(new StationPart(Olla)));
            Assert.AreEqual(0, forge.Parts.Parts.Count);
        }

        [Test]
        public void NoSeRepiteNiSePasaDeTresRanuras()
        {
            var slots = new PartSlots(StationType.Fragua);
            Assert.AreEqual(PartPlaceResult.Placed, slots.Place(new StationPart(Crisol)));
            Assert.AreEqual(PartPlaceResult.AlreadyPlaced, slots.Place(new StationPart(Crisol)));
            slots.Place(new StationPart(new StationPartDefinition("fuelle", StationType.Fragua)));
            slots.Place(new StationPart(new StationPartDefinition("yunque", StationType.Fragua)));
            Assert.AreEqual(PartPlaceResult.NoFreeSlot,
                slots.Place(new StationPart(new StationPartDefinition("tenazas", StationType.Fragua))));
        }

        [Test]
        public void PiezaRotaDejaDeContarHastaRepararla()
        {
            var forge = new Workstation(StationType.Fragua);
            var crisol = new StationPart(Crisol);
            forge.PlacePart(crisol);
            Assert.AreEqual(1, forge.QualityBonus);
            crisol.Wear();
            crisol.Wear();
            crisol.Wear();
            Assert.IsTrue(crisol.IsBroken);
            Assert.IsFalse(forge.Parts.HasWorking("crisol"));
            Assert.AreEqual(0, forge.QualityBonus);
            crisol.Repair();
            Assert.AreEqual(2, crisol.Durability);
            Assert.IsTrue(forge.Parts.HasWorking("crisol"));
        }

        [Test]
        public void DefinicionDePiezaValidaDatos()
        {
            Assert.Throws<System.ArgumentException>(() => new StationPartDefinition(" ", StationType.Fragua));
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                new StationPartDefinition("x", StationType.Fragua, timeBonus: 1f));
        }
    }
}
