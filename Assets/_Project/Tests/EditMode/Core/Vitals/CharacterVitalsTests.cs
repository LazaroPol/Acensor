using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Vitals
{
    [TestFixture]
    public class CharacterVitalsTests
    {
        private static StatSheet Sheet()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.Define(StatType.MaxClassResource, 50f);
            sheet.Define(StatType.MaxStamina, 80f);
            return sheet;
        }

        [Test]
        public void PoolsUseMaxFromStatSheet()
        {
            var vitals = new CharacterVitals(Sheet());
            Assert.That(vitals.Health.Max, Is.EqualTo(100f));
            Assert.That(vitals.ClassResource.Max, Is.EqualTo(50f));
            Assert.That(vitals.Stamina.Max, Is.EqualTo(80f));
            Assert.That(vitals.Get(ResourceKind.Stamina), Is.SameAs(vitals.Stamina));
        }

        [Test]
        public void StatModifiers_ChangePoolMax()
        {
            var sheet = Sheet();
            var vitals = new CharacterVitals(sheet);
            sheet.AddModifier(StatType.MaxStamina, StatModifier.Flat(-30f));
            Assert.That(vitals.Stamina.Max, Is.EqualTo(50f));
            Assert.That(vitals.Stamina.Current, Is.EqualTo(50f));
        }

        [Test]
        public void IsAlive_FollowsHealth_AndRefillAllRestores()
        {
            var vitals = new CharacterVitals(Sheet());
            vitals.Health.Drain(200f);
            vitals.Stamina.Drain(10f);
            Assert.That(vitals.IsAlive, Is.False);
            vitals.RefillAll();
            Assert.That(vitals.IsAlive, Is.True);
            Assert.That(vitals.Stamina.IsFull, Is.True);
        }

        [Test]
        public void MissingStat_Throws()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            Assert.Throws<KeyNotFoundException>(() => new CharacterVitals(sheet));
        }

        [Test]
        public void MaxStatFor_MapsEachKind()
        {
            Assert.That(CharacterVitals.MaxStatFor(ResourceKind.Health), Is.EqualTo(StatType.MaxHealth));
            Assert.That(CharacterVitals.MaxStatFor(ResourceKind.ClassResource), Is.EqualTo(StatType.MaxClassResource));
            Assert.That(CharacterVitals.MaxStatFor(ResourceKind.Stamina), Is.EqualTo(StatType.MaxStamina));
        }
    }
}
