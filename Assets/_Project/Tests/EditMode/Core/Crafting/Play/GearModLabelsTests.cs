using NUnit.Framework;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Play;

namespace Specimen.Tests.Core.Crafting.Play
{
    /// <summary>OF-16: la ventana de mods enseña base → efectivo y si mejora o empeora.</summary>
    [TestFixture]
    public class GearModLabelsTests
    {
        [Test]
        public void LaLineaMuestraBaseEfectivoYDiferencia()
        {
            var play = new PlayFixtures();
            var palo = play.GoodPalo();
            play.Mods.Restore(palo, new[] { DefaultMods.PiedraAtada });
            var view = play.Mods.Stats(palo);
            StringAssert.Contains("→", GearModLabels.Line(view, GearStat.DañoContundente));
            StringAssert.Contains("(+", GearModLabels.Line(view, GearStat.DañoContundente));
            Assert.AreEqual(1, GearModLabels.Trend(view, GearStat.DañoContundente));
            Assert.AreEqual(-1, GearModLabels.Trend(view, GearStat.Peso), "Más peso es peor.");
            Assert.AreEqual(0, GearModLabels.Trend(view, GearStat.Armadura));
            Assert.AreEqual("Armadura: 0", GearModLabels.Line(view, GearStat.Armadura));
        }

        [Test]
        public void CadaResultadoTieneSuMensaje()
        {
            foreach (ModInstallResult r in System.Enum.GetValues(typeof(ModInstallResult)))
                Assert.IsNotEmpty(GearModLabels.Result(r));
            Assert.AreEqual("Mod retirado a la bolsa.", GearModLabels.Result(ModUninstallResult.Returned));
            Assert.AreEqual("Daño perforante", GearModLabels.Stat(GearStat.DañoPerforante));
        }
    }
}
