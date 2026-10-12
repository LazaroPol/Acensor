using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Queue;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using Specimen.Core.Randomness;
using static Specimen.Tests.Core.Crafting.Session.SessionFixtures;

namespace Specimen.Tests.Core.Crafting.Session
{
    /// <summary>OF-15: piezas, combustible, salida y mejora de la estación desde su ventana.</summary>
    [TestFixture]
    public class StationSessionUpkeepTests
    {
        [Test]
        public void RetirarUnaPiezaLaDevuelveALaBolsaConSuDesgaste()
        {
            var forge = Forge(out var profile);
            Give(profile.Bag, Crisol, 1);
            forge.PlacePart(Crisol);
            forge.Station.Parts.Find(Crisol).Wear();

            Assert.IsTrue(forge.RemovePart(Crisol));
            Assert.AreEqual(1, profile.Bag.CountOf(Crisol));
            Assert.IsFalse(forge.RemovePart(Crisol));
            forge.PlacePart(Crisol);
            var part = forge.Station.Parts.Find(Crisol);
            Assert.AreEqual(part.Definition.MaxDurability - 1, part.Durability);
        }

        [Test]
        public void NoColocaPiezasQueNoLlevaNiDeOtraEstacion()
        {
            var forge = Forge(out var profile);
            Assert.IsNull(forge.PlacePart(Crisol));
            Give(profile.Bag, "pieza.olla_hierro", 1);
            Assert.IsEmpty(forge.PartsInBag());
            Assert.AreEqual(PartPlaceResult.Incompatible, forge.PlacePart("pieza.olla_hierro"));
        }

        [Test]
        public void SoloEchaCombustibleQueLaEstacionPuedeQuemar()
        {
            var forge = Forge(out var profile);
            Give(profile.Bag, "lingote_cobre", 2);
            Give(profile.Bag, "madera", 3);

            Assert.AreEqual(0, forge.AddFuel("lingote_cobre"));
            Assert.AreEqual(3, forge.AddFuel("madera", 5));
            Assert.AreEqual(0, profile.Bag.CountOf("madera"));
            var profile2 = new CraftingProfile(profile.Bag);
            var tailor = new StationSessionFactory(Catalog, profile2, new Pcg32Random(1)).Create(StationType.TallerSastre);
            Assert.IsEmpty(tailor.FuelsInBag());
        }

        [Test]
        public void LaMejoraGastaPiedraYMetalYAlargaLaCola()
        {
            var forge = Forge(out var profile, bagSlots: 24);
            Assert.AreEqual(UpgradeResult.MissingMaterials, forge.CanUpgrade());
            Give(profile.Bag, "piedra", 20);
            Give(profile.Bag, "lingote-hierro", 10);

            Assert.AreEqual(UpgradeResult.Upgraded, forge.Upgrade());
            Assert.AreEqual(2, forge.Station.Level);
            Assert.AreEqual(4, forge.Station.QueueSize);
            Assert.AreEqual(0, profile.Bag.CountOf("piedra"));
            Assert.AreEqual(3, forge.NextUpgrade.TargetLevel);
        }

        [Test]
        public void ConLaBolsaLlenaLaSalidaSeQuedaEnLaEstacion()
        {
            var forge = Forge(out var profile, bagSlots: 1);
            forge.Station.Output.Add(ItemInstance.CreateNew(Catalog.Recipes.Get(Hacha).MainOutput.Item));
            Give(profile.Bag, "piedra", 1);

            Assert.AreEqual(0, forge.CollectOutput());
            Assert.AreEqual(1, forge.Station.Output.UsedSlots);
        }

        [Test]
        public void AvisaDeLaSubidaDeNivelDeOficio()
        {
            var forge = Forge(out var profile);
            var smith = profile.Skill(ProfessionId.Herrero);
            smith.Gain(smith.Curve.XpToNext(1) - 5);
            GiveAxeMaterials(profile.Bag);
            Give(profile.Bag, "lena", 1);
            forge.AddFuel("lena");
            CraftCompletion done = null;
            forge.Completed += c => done = c;

            forge.Craft(Catalog.Recipes.Get(Hacha));
            forge.Tick(10f);

            Assert.AreEqual(1, done.LevelsGained);
            Assert.AreEqual(2, done.Level);
            Assert.AreEqual(CraftQueueState.Idle, forge.Queue.State);
        }

        [Test]
        public void TextosVisiblesDeOficioEstacionYEstado()
        {
            Assert.AreEqual("Alquimista", StationLabels.Profession(ProfessionId.Quimico));
            Assert.AreEqual("Hogar de cocina", StationLabels.Station(StationType.HogarCocina));
            Assert.AreEqual("En pausa: sin combustible", StationLabels.State(CraftQueueState.NoFuel));
            Assert.AreEqual("Encolado", StationLabels.Result(StationCraftResult.Queued));
        }
    }
}
