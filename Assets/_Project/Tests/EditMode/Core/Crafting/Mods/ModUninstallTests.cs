using NUnit.Framework;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Items;
using static Specimen.Tests.Core.Crafting.Mods.ModFixtures;

namespace Specimen.Tests.Core.Crafting.Mods
{
    [TestFixture]
    public class ModUninstallTests
    {
        private static ModdedItem WithMod(ModDefinition mod, Inventory bag)
        {
            var palo = Modded(Palo, Quality.Good);
            bag.Add(ItemInstance.CreateNew(mod.Item));
            Installer().Install(palo, mod, bag);
            return palo;
        }

        [Test]
        public void Retirar_DevuelveALaBolsa()
        {
            var bag = new Inventory(2);
            var palo = WithMod(DefaultMods.Esquirlas, bag);
            Assert.That(Installer().Uninstall(palo, DefaultMods.EsquirlasMetal, bag),
                Is.EqualTo(ModUninstallResult.Returned));
            Assert.That(palo.Mods, Is.Empty);
            Assert.That(bag.CountOf(DefaultMods.EsquirlasMetal), Is.EqualTo(1));
        }

        [Test]
        public void BolsaLlena_FallaSinCambios()
        {
            var bag = new Inventory(1);
            var palo = WithMod(DefaultMods.Esquirlas, bag);
            bag.Add(ItemInstance.CreateNew(Peto));
            Assert.That(Installer().Uninstall(palo, DefaultMods.EsquirlasMetal, bag),
                Is.EqualTo(ModUninstallResult.BagFull));
            Assert.That(palo.Mods, Has.Member(DefaultMods.Esquirlas));
            Assert.That(bag.CountOf(DefaultMods.EsquirlasMetal), Is.Zero);
        }

        [Test]
        public void NoRetirable_SeDestruye()
        {
            var bag = new Inventory(2);
            var palo = WithMod(DefaultMods.Resina, bag);
            Assert.That(Installer().Uninstall(palo, DefaultMods.ResinaIgnea, bag),
                Is.EqualTo(ModUninstallResult.Destroyed));
            Assert.That(palo.Mods, Is.Empty);
            Assert.That(bag.CountOf(DefaultMods.ResinaIgnea), Is.Zero);
        }

        [Test]
        public void ModNoInstalado_NoHaceNada() =>
            Assert.That(Installer().Uninstall(Modded(Palo, Quality.Good), DefaultMods.Clavo, new Inventory(1)),
                Is.EqualTo(ModUninstallResult.NotInstalled));
    }
}
