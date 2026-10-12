using NUnit.Framework;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Classes;
using Specimen.Core.Professions;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Professions
{
    [TestFixture]
    public class LearnedProfessionsTests
    {
        [Test]
        public void LearnsAtMostTwo()
        {
            var learned = new LearnedProfessions();
            Assert.That(learned.Learn(ProfessionId.Herrero), Is.True);
            Assert.That(learned.Learn(ProfessionId.Herrero), Is.False);
            Assert.That(learned.Learn(ProfessionId.Cocinero), Is.True);
            Assert.That(learned.Learn(ProfessionId.Quimico), Is.False);
            Assert.That(learned.Forget(ProfessionId.Herrero), Is.True);
            Assert.That(learned.Learn(ProfessionId.Quimico), Is.True);
            Assert.That(learned.Knows(ProfessionId.Quimico), Is.True);
        }

        [Test]
        public void ConsumingAPotionHealsAndSpendsOne()
        {
            var vitals = new CharacterVitals(ClassKitCatalog.CreateDefault().All[0].CreateStatSheet());
            vitals.Health.Drain(80f);
            var inventory = new Inventory(4);
            inventory.Add(ItemInstance.CreateNew(ItemCatalog.Get(ItemIds.PocionVida)), 2);
            float before = vitals.Health.Current;
            Assert.That(ConsumableUse.TryUse(inventory, ItemIds.PocionVida, vitals, out var effect), Is.True);
            Assert.That(vitals.Health.Current, Is.GreaterThan(before));
            Assert.That(inventory.CountOf(ItemIds.PocionVida), Is.EqualTo(1));
            Assert.That(effect.Description, Is.Not.Empty);
            Assert.That(ConsumableUse.TryUse(inventory, ItemIds.Madera, vitals, out _), Is.False);
        }
    }
}
