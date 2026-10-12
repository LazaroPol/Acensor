using NUnit.Framework;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.Mods.ModFixtures;

namespace Specimen.Tests.Core.Crafting.Mods
{
    [TestFixture]
    public class ModInstallerTests
    {
        [TestCase(Quality.None, 0)]
        [TestCase(Quality.Common, 1)]
        [TestCase(Quality.Good, 2)]
        [TestCase(Quality.Masterwork, 3)]
        public void Huecos_SalenDeLaCalidad(Quality quality, int slots) =>
            Assert.That(Modded(Palo, quality).SlotCount, Is.EqualTo(slots));

        [Test]
        public void SinCalidad_NoAdmiteMods()
        {
            var bag = BagWith(4, DefaultMods.Esquirlas);
            var result = Installer().Install(Modded(Palo, Quality.None), DefaultMods.Esquirlas, bag);
            Assert.That(result, Is.EqualTo(ModInstallResult.NoFreeSlot));
            Assert.That(bag.CountOf(DefaultMods.EsquirlasMetal), Is.EqualTo(1));
        }

        [Test]
        public void Instalar_SacaElModDeLaBolsa()
        {
            var palo = Modded(Palo, Quality.Common);
            var bag = BagWith(4, DefaultMods.Esquirlas);
            Assert.That(Installer().Install(palo, DefaultMods.Esquirlas, bag), Is.EqualTo(ModInstallResult.Installed));
            Assert.That(palo.Mods, Has.Member(DefaultMods.Esquirlas));
            Assert.That(bag.CountOf(DefaultMods.EsquirlasMetal), Is.Zero);
        }

        [Test]
        public void SinHuecoLibre_FallaSinCambios()
        {
            var palo = Modded(Palo, Quality.Common);
            var bag = BagWith(4, DefaultMods.Esquirlas, DefaultMods.Tiras);
            Installer().Install(palo, DefaultMods.Esquirlas, bag);
            Assert.That(Installer().Install(palo, DefaultMods.Tiras, bag), Is.EqualTo(ModInstallResult.NoFreeSlot));
            Assert.That(bag.CountOf(DefaultMods.TirasCuero), Is.EqualTo(1));
            Assert.That(palo.Mods.Count, Is.EqualTo(1));
        }

        [Test]
        public void TipoDeHuecoRepetido_SeRechaza()
        {
            var palo = Modded(Palo, Quality.Good);
            var bag = BagWith(4, DefaultMods.Esquirlas, DefaultMods.ClavoMod);
            Installer().Install(palo, DefaultMods.Esquirlas, bag);
            Assert.That(Installer().Install(palo, DefaultMods.ClavoMod, bag),
                Is.EqualTo(ModInstallResult.SlotTypeTaken));
        }

        [Test]
        public void Accesorio_AdmiteDos()
        {
            var palo = Modded(Palo, Quality.Masterwork);
            var bag = BagWith(4, DefaultMods.Resina, DefaultMods.Resina, DefaultMods.Resina);
            Assert.That(Installer().Install(palo, DefaultMods.Resina, bag), Is.EqualTo(ModInstallResult.Installed));
            Assert.That(Installer().Install(palo, DefaultMods.Resina, bag), Is.EqualTo(ModInstallResult.Installed));
            Assert.That(Installer().Install(palo, DefaultMods.Resina, bag), Is.EqualTo(ModInstallResult.SlotTypeTaken));
        }

        [Test]
        public void TagsIncompatibles_SeRechaza() =>
            Assert.That(Installer().Install(Modded(Peto, Quality.Good), DefaultMods.Esquirlas,
                BagWith(2, DefaultMods.Esquirlas)), Is.EqualTo(ModInstallResult.Incompatible));

        [Test]
        public void TagsExtraPorId_HacenCompatible()
        {
            var tags = new DefaultGearTags(new System.Collections.Generic.Dictionary<string, GearTags>
                { ["peto"] = GearTags.Arma });
            Assert.That(tags.TagsOf(Peto), Is.EqualTo(GearTags.Armadura | GearTags.Arma));
            Assert.That(tags.TagsOf(Palo), Is.EqualTo(GearTags.Arma | GearTags.Madera));
        }

        [Test]
        public void RequisitosNoCumplidos_SeRechaza()
        {
            var exigente = new ModDefinition("mod.x", "X", ModSlotType.Filo, GearTags.Arma,
                new GearModifier[0], new ModRequirements(Quality.Good, ProfessionId.Herrero, 5));
            var levels = new ProfessionLevels();
            Assert.That(Installer().Install(Modded(Palo, Quality.Common), exigente, BagWith(2, exigente), levels),
                Is.EqualTo(ModInstallResult.RequirementsNotMet));
            Assert.That(Installer().Install(Modded(Palo, Quality.Good), exigente, BagWith(2, exigente), levels),
                Is.EqualTo(ModInstallResult.RequirementsNotMet));
            Assert.That(new ModRequirements(Quality.Good).IsMetBy(Quality.Fine, null), Is.True);
        }

        [Test]
        public void ObjetoRoto_SeRechaza()
        {
            var palo = Modded(Palo, Quality.Common);
            palo.Item.Durability.Wear(palo.Item.Durability.Max);
            Assert.That(Installer().Install(palo, DefaultMods.Esquirlas, BagWith(2, DefaultMods.Esquirlas)),
                Is.EqualTo(ModInstallResult.ItemBroken));
        }

        [Test]
        public void ModQueNoEstaEnLaBolsa_SeRechaza() =>
            Assert.That(Installer().Install(Modded(Palo, Quality.Common), DefaultMods.Esquirlas, BagWith(2)),
                Is.EqualTo(ModInstallResult.ModNotOwned));
    }
}
