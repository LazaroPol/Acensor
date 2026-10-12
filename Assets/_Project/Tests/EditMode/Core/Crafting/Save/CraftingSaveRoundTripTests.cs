using NUnit.Framework;
using Specimen.Core.Crafting.Save;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Crafting.Save
{
    [TestFixture]
    public class CraftingSaveRoundTripTests
    {
        private static CraftingSaveData RoundTrip()
            => CraftingSaveReader.Read(CraftingSaveWriter.Write(CraftingSaveFixtures.Full()));

        [Test]
        public void TextIsStableAfterRoundTrip()
        {
            var text = CraftingSaveWriter.Write(CraftingSaveFixtures.Full());
            Assert.That(CraftingSaveWriter.Write(CraftingSaveReader.Read(text)), Is.EqualTo(text));
            Assert.That(text, Does.StartWith("oficios v1\n"));
        }

        [Test]
        public void KeepsSkillsKnowledgeAndVolumes()
        {
            var d = RoundTrip();
            Assert.That(d.Skills[0].Profession, Is.EqualTo(ProfessionId.Herrero));
            Assert.That(d.Skills[0].Level, Is.EqualTo(7));
            Assert.That(d.Skills[0].Xp, Is.EqualTo(340));
            Assert.That(d.Skills[1].Profession, Is.EqualTo(ProfessionId.Sastre));
            Assert.That(d.KnownRecipes, Is.EqualTo(new[] { "herrero.espada_acero" }));
            Assert.That(d.ReadVolumes, Is.EqualTo(new[] { "libro_forjador.vol2" }));
        }

        [Test]
        public void KeepsStationsPartsSlotsAndQueueOrder()
        {
            var d = RoundTrip();
            Assert.That(d.Stations[0].Station, Is.EqualTo("fragua_aldea"));
            Assert.That(d.Stations[0].Level, Is.EqualTo(2));
            Assert.That(d.Parts[0].ItemId, Is.EqualTo("pieza.crisol"));
            Assert.That(d.Parts[0].Durability, Is.EqualTo(37));
            Assert.That(d.Slots[0].Kind, Is.EqualTo("fuel"));
            Assert.That(d.Slots[0].Count, Is.EqualTo(4));
            Assert.That(d.Jobs[0].RemainingSeconds, Is.EqualTo(12.5));
            Assert.That(d.Jobs[1].RecipeId, Is.EqualTo("herrero.clavos"));
            Assert.That(d.Jobs[1].RemainingSeconds, Is.EqualTo(0.1));
            Assert.That(d.Jobs[1].Level, Is.EqualTo(7));
        }

        [Test]
        public void KeepsInstancesWithQualityDurabilityAndMods()
        {
            var d = RoundTrip();
            Assert.That(d.Items[0].Quality, Is.EqualTo(Quality.Fine));
            Assert.That(d.Items[0].Durability, Is.EqualTo(120));
            Assert.That(d.Items[0].Mods, Is.EqualTo(new[] { "mod.tiras_cuero", "mod.clavo" }));
            Assert.That(d.Items[1].Mods, Is.Empty);
        }

        [Test]
        public void KeepsInventoriesWithStacksAndInstances()
        {
            var d = RoundTrip();
            Assert.That(d.Inventory[0].IsInstance, Is.True);
            Assert.That(d.Inventory[0].Ref, Is.EqualTo("ab12"));
            Assert.That(d.Inventory[1].IsInstance, Is.False);
            Assert.That(d.Inventory[1].Ref, Is.EqualTo("madera_pino"));
            Assert.That(d.Inventory[1].Count, Is.EqualTo(12));
            Assert.That(d.Inventory[2].Inventory, Is.EqualTo("cofre"));
        }

        [Test]
        public void ReadsGddSampleWithWindowsLineEndings()
        {
            var d = CraftingSaveReader.Read(CraftingSaveFixtures.Sample.Replace("\n", "\r\n"));
            Assert.That(d.Items[0].Mods, Is.EqualTo(new[] { "mod.a" }));
            Assert.That(d.Inventory[0].Ref, Is.EqualTo("ab12"));
        }

        [Test]
        public void EmptyDataRoundTrips()
            => Assert.That(CraftingSaveReader.Read(CraftingSaveWriter.Write(new CraftingSaveData())).Skills, Is.Empty);
    }
}
