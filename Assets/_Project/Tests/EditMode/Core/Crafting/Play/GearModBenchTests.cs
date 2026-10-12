using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Mods;

namespace Specimen.Tests.Core.Crafting.Play
{
    /// <summary>OF-16: poner y quitar mods en un arma y ver sus stats base → efectivo.</summary>
    [TestFixture]
    public class GearModBenchTests
    {
        [Test]
        public void PonerUnModCambiaLosStatsYQuitarloLoDevuelveALaBolsa()
        {
            var play = new PlayFixtures();
            var palo = play.GoodPalo();
            var piedra = play.Mod(DefaultMods.PiedraAtada);
            play.Give(piedra.Item);
            Assert.Contains(piedra, play.Mods.Installable(palo).ToList());
            Assert.AreEqual(ModInstallResult.Installed, play.Mods.Install(palo, piedra.Id));
            Assert.AreEqual(0, play.Profile.Bag.CountOf(piedra.Id));
            var view = play.Mods.Stats(palo);
            Assert.Greater(view.Delta(GearStat.DañoContundente), 4.9f);
            Assert.Greater(view.Delta(GearStat.Peso), 1.4f);
            Assert.AreEqual(ModUninstallResult.Returned, play.Mods.Uninstall(palo, piedra.Id));
            Assert.AreEqual(1, play.Profile.Bag.CountOf(piedra.Id));
            Assert.AreEqual(0f, play.Mods.Stats(palo).Delta(GearStat.DañoContundente), 1e-4f);
        }

        [Test]
        public void SinModEnLaBolsaNoSePuedeInstalar()
        {
            var play = new PlayFixtures();
            var palo = play.GoodPalo();
            Assert.IsEmpty(play.Mods.Installable(palo));
            Assert.AreEqual(ModInstallResult.ModNotOwned, play.Mods.Install(palo, DefaultMods.PiedraAtada));
            Assert.AreEqual(ModInstallResult.ModNotOwned, play.Mods.Install(palo, "mod.inexistente"));
        }

        [Test]
        public void LosModsSeRecuerdanPorInstancia()
        {
            var play = new PlayFixtures();
            var uno = play.GoodPalo();
            var otro = play.GoodPalo();
            play.Mods.Restore(uno, new[] { DefaultMods.PiedraAtada, "mod.retirado" });
            Assert.AreEqual(1, play.Mods.ModsOf(uno).Count);
            Assert.IsEmpty(play.Mods.ModsOf(otro));
        }
    }
}
