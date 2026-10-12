using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;
using static Specimen.Tests.Core.Crafting.Stations.StationFixtures;

namespace Specimen.Tests.Core.Crafting.Stations
{
    [TestFixture]
    public class WorkstationTests
    {
        [Test]
        public void NuevaEstacionNivel1ConRanuras()
        {
            var forge = new Workstation(StationType.Fragua);
            Assert.AreEqual(1, forge.Level);
            Assert.AreEqual(Workstation.BaseQueueSize, forge.QueueSize);
            Assert.AreEqual(6, forge.Input.Capacity);
            Assert.AreEqual(6, forge.Output.Capacity);
            Assert.AreEqual(2, forge.Fuel.Capacity);
            Assert.AreEqual(3, forge.Parts.Capacity);
        }

        [Test]
        public void SoloLasEstacionesDeFuegoTienenCombustible()
        {
            Assert.IsTrue(new Workstation(StationType.HogarCocina).HasFuelSlots);
            Assert.IsTrue(new Workstation(StationType.MesaAlquimia).HasFuelSlots);
            Assert.IsFalse(new Workstation(StationType.TallerSastre).HasFuelSlots);
            Assert.IsNull(new Workstation(StationType.BancoCantero).Fuel);
        }

        [Test]
        public void MejorarConsumeMaterialesYSubeNivel()
        {
            var forge = new Workstation(StationType.Fragua);
            var bag = Stocked(10, ore: 7, coal: 2);
            Assert.AreEqual(UpgradeResult.Upgraded, forge.Upgrade(ForgeTo2(), bag));
            Assert.AreEqual(2, forge.Level);
            Assert.AreEqual(4, forge.QueueSize);
            Assert.AreEqual(2, forge.QualityBonus);
            Assert.AreEqual(2, bag.CountOf(Ore.Id));
            Assert.AreEqual(UpgradeResult.Upgraded, forge.Upgrade(ForgeTo3(), bag));
            Assert.AreEqual(3, forge.Level);
            Assert.IsTrue(forge.IsMaxLevel);
            Assert.AreEqual(0, bag.CountOf(Coal.Id));
        }

        [Test]
        public void SinMaterialesNoMejoraNiGasta()
        {
            var forge = new Workstation(StationType.Fragua);
            var bag = Stocked(10, ore: 4, coal: 0);
            Assert.AreEqual(UpgradeResult.MissingMaterials, forge.Upgrade(ForgeTo2(), bag));
            Assert.AreEqual(1, forge.Level);
            Assert.AreEqual(4, bag.CountOf(Ore.Id));
        }

        [Test]
        public void MejoraDeOtraEstacionONivelSaltadoSeRechaza()
        {
            var forge = new Workstation(StationType.Fragua);
            var bag = Stocked(10, ore: 9, coal: 9);
            Assert.AreEqual(UpgradeResult.WrongLevel, forge.Upgrade(ForgeTo3(), bag));
            Assert.AreEqual(UpgradeResult.WrongStation, new Workstation(StationType.HogarCocina).Upgrade(ForgeTo2(), bag));
            forge.Upgrade(ForgeTo2(), bag);
            Assert.AreEqual(UpgradeResult.WrongLevel, forge.Upgrade(ForgeTo2(), bag));
            Assert.AreEqual(9, bag.CountOf(Coal.Id));
        }

        [Test]
        public void MejoraValidaSusDatos()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() =>
                new StationUpgrade(StationType.Fragua, 1, new[] { Specimen.Core.Crafting.IngredientSpec.ForItem(Ore) }, 2));
            Assert.Throws<System.ArgumentException>(() =>
                new StationUpgrade(StationType.Fragua, 2, new Specimen.Core.Crafting.IngredientSpec[0], 4));
        }
    }
}
