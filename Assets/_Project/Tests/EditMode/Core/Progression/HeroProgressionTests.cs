using System.Linq;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Progression;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Progression
{
    [TestFixture]
    public class HeroProgressionTests
    {
        private static ClassKit Kit => ClassKitCatalog.CreateDefault().All[0];

        private static HeroProgression AtLevel(int level) => new HeroProgression(new CharacterProgress(level));

        [Test]
        public void TalentsAreKeptPerKit()
        {
            var hero = AtLevel(4);
            var first = hero.TalentsFor(ClassKitId.Bastion).Tree.Branches[0].Talents[0];
            Assert.That(hero.TrySpend(ClassKitId.Bastion, first.Id), Is.True);
            Assert.That(hero.AvailablePoints(ClassKitId.Bastion), Is.EqualTo(2));
            Assert.That(hero.AvailablePoints(ClassKitId.Vigia), Is.EqualTo(3));
            Assert.That(hero.TalentsFor(ClassKitId.Bastion), Is.SameAs(hero.TalentsFor(ClassKitId.Bastion)));
        }

        [Test]
        public void CannotSpendMorePointsThanLevelGives()
        {
            var hero = AtLevel(2);
            var talent = hero.TalentsFor(ClassKitId.Bastion).Tree.Branches[0].Talents.First(t => t.MaxRank > 1);
            Assert.That(hero.TrySpend(ClassKitId.Bastion, talent.Id), Is.True);
            Assert.That(hero.CanSpend(ClassKitId.Bastion, talent.Id), Is.False);
        }

        [Test]
        public void ApplyIsIdempotentAndGrowsWithLevel()
        {
            var sheet = Kit.CreateStatSheet();
            float baseHealth = sheet.ValueOf(StatType.MaxHealth);
            var hero = AtLevel(5);
            hero.Apply(Kit.Id, sheet);
            float leveled = sheet.ValueOf(StatType.MaxHealth);
            hero.Apply(Kit.Id, sheet);
            Assert.That(leveled, Is.GreaterThan(baseHealth));
            Assert.That(sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(leveled).Within(1e-3));
        }

        [Test]
        public void SpellSlotNeedsLevelResourceAndCooldown()
        {
            var vitals = new CharacterVitals(Kit.CreateStatSheet());
            var spell = KitSpellbooks.For(Kit.Id).All[0];
            var slot = new SpellSlot(spell);
            Assert.That(slot.TryCast(spell.Level - 1, vitals.ClassResource), Is.EqualTo(SpellCastResult.NotLearned));
            Assert.That(slot.TryCast(spell.Level, vitals.ClassResource, 0.5f), Is.EqualTo(SpellCastResult.Cast));
            Assert.That(slot.LastCooldown, Is.EqualTo(spell.Cooldown * 0.5f).Within(1e-4));
            Assert.That(slot.TryCast(spell.Level, vitals.ClassResource), Is.EqualTo(SpellCastResult.OnCooldown));
            slot.Tick(100f);
            vitals.ClassResource.Empty();
            Assert.That(slot.TryCast(spell.Level, vitals.ClassResource), Is.EqualTo(SpellCastResult.NoResource));
        }

        [Test]
        public void TimedBonusKeepsTheStrongestAndExpires()
        {
            var bonus = new TimedBonus();
            bonus.Begin(0.3f, 5f);
            bonus.Begin(0.1f, 10f);
            Assert.That(bonus.Current, Is.EqualTo(0.3f));
            bonus.Tick(6f);
            Assert.That(bonus.Current, Is.EqualTo(0f));
        }
    }
}
