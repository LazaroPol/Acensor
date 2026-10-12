using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Definitions
{
    [TestFixture]
    public class StationTypeTests
    {
        [Test]
        public void SieteEstacionesConValoresEstables()
        {
            Assert.AreEqual(7, Enum.GetValues(typeof(StationType)).Length);
            Assert.AreEqual(1, (int)StationType.Fragua);
            Assert.AreEqual(2, (int)StationType.TallerSastre);
            Assert.AreEqual(3, (int)StationType.HogarCocina);
            Assert.AreEqual(4, (int)StationType.MesaAlquimia);
            Assert.AreEqual(5, (int)StationType.TallerIngenios);
            Assert.AreEqual(6, (int)StationType.BancoCarpintero);
            Assert.AreEqual(7, (int)StationType.BancoCantero);
        }

        [Test]
        public void CadaOficioTieneUnaEstacionPrincipalDistinta()
        {
            var stations = Enum.GetValues(typeof(ProfessionId)).Cast<ProfessionId>()
                .Select(StationTypes.MainOf).ToList();
            Assert.AreEqual(7, stations.Distinct().Count());
            Assert.AreEqual(StationType.Fragua, StationTypes.MainOf(ProfessionId.Herrero));
            Assert.AreEqual(StationType.BancoCantero, StationTypes.MainOf(ProfessionId.Escultor));
        }

        [Test]
        public void IsDefinedRechazaValoresDesconocidos()
        {
            Assert.IsTrue(StationTypes.IsDefined(StationType.MesaAlquimia));
            Assert.IsFalse(StationTypes.IsDefined((StationType)0));
            Assert.IsFalse(StationTypes.IsDefined((StationType)8));
        }
    }
}
