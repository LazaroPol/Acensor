using System;
using NUnit.Framework;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Items;
using static Specimen.Tests.Core.Crafting.Mods.ModFixtures;

namespace Specimen.Tests.Core.Crafting.Mods
{
    [TestFixture]
    public class ModdedStatsTests
    {
        private static ModdedItem PaloCon(Quality quality, params ModDefinition[] mods)
        {
            var palo = Modded(Palo, quality);
            var bag = BagWith(4, mods);
            foreach (var m in mods) Installer().Install(palo, m, bag);
            Assert.That(palo.Mods.Count, Is.EqualTo(mods.Length));
            return palo;
        }

        private static int Round(float v) => (int)Math.Round(v, MidpointRounding.AwayFromZero);

        [Test]
        public void PaloConEsquirlas_MasCortanteMenosDurabilidad()
        {
            var view = ModdedStats.Compute(PaloCon(Quality.Common, DefaultMods.Esquirlas), PaloStats);
            Assert.That(view.Effective[GearStat.DañoCortante], Is.EqualTo(4f));
            Assert.That(view.MaxDurability, Is.EqualTo(Round(Palo.MaxDurability * 0.8f)));
            Assert.That(view.Delta(GearStat.Durabilidad), Is.LessThan(0f));
        }

        [Test]
        public void PaloConPiedra_MasContundenteYPesoMenosVelocidad()
        {
            var view = ModdedStats.Compute(PaloCon(Quality.Common, DefaultMods.Piedra), PaloStats);
            Assert.That(view.Effective[GearStat.DañoContundente], Is.EqualTo(11f).Within(1e-4));
            Assert.That(view.Effective[GearStat.Peso], Is.EqualTo(2.5f).Within(1e-4));
            Assert.That(view.Effective[GearStat.Velocidad], Is.EqualTo(0.95f).Within(1e-4));
        }

        [Test]
        public void PaloConClavo_MasPerforante() =>
            Assert.That(ModdedStats.Compute(PaloCon(Quality.Common, DefaultMods.ClavoMod), PaloStats)
                .Effective[GearStat.DañoPerforante], Is.EqualTo(3f));

        [Test]
        public void Calidad_EscalaDañoPeroNoVelocidadNiPeso()
        {
            var view = ModdedStats.Compute(PaloCon(Quality.Good), PaloStats);
            Assert.That(view.Effective[GearStat.DañoContundente], Is.EqualTo(6.6f).Within(1e-4));
            Assert.That(view.Effective[GearStat.Velocidad], Is.EqualTo(1f));
            Assert.That(view.Effective[GearStat.Peso], Is.EqualTo(1f));
            Assert.That(view.Base[GearStat.DañoContundente], Is.EqualTo(6f));
        }

        [Test]
        public void PorcentajesDeDurabilidad_SeSuman()
        {
            var view = ModdedStats.Compute(PaloCon(Quality.Good, DefaultMods.Esquirlas, DefaultMods.Tiras), PaloStats);
            Assert.That(view.MaxDurability, Is.EqualTo(Round(Palo.MaxDurability * 1.15f * 0.9f)));
            Assert.That(view.Effective[GearStat.Velocidad], Is.EqualTo(1.08f).Within(1e-4));
        }

        [Test]
        public void DurabilidadMaxima_NuncaBajaDeUno()
        {
            var fragil = new ModDefinition("mod.fragil", "Frágil", ModSlotType.Filo, GearTags.Arma,
                new[] { GearModifier.Percent(GearStat.Durabilidad, -5f) });
            Assert.That(ModdedStats.Compute(PaloCon(Quality.Common, fragil), PaloStats).MaxDurability, Is.EqualTo(1));
        }

        [Test]
        public void QuitarElMod_RestauraLosStats()
        {
            var palo = PaloCon(Quality.Common, DefaultMods.Esquirlas);
            Installer().Uninstall(palo, DefaultMods.EsquirlasMetal, new Inventory(1));
            var view = ModdedStats.Compute(palo, PaloStats);
            Assert.That(view.Effective[GearStat.DañoCortante], Is.Zero);
            Assert.That(view.MaxDurability, Is.EqualTo(Palo.MaxDurability));
        }

        [Test]
        public void ModDefinition_SinEtiquetas_Lanza() =>
            Assert.Throws<ArgumentException>(() => new ModDefinition("m", "M", ModSlotType.Filo, GearTags.None,
                new GearModifier[0]));
    }
}
