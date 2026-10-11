using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Stats;
using Specimen.Core.Vitals;

namespace Specimen.Tests.Core.Classes
{
    [TestFixture]
    public class ClassKitTests
    {
        private static Dictionary<StatType, float> Stats() => new Dictionary<StatType, float>
        {
            { StatType.MaxHealth, 120f }, { StatType.MaxClassResource, 60f }, { StatType.MaxStamina, 90f }
        };

        private static ClassKit Kit(ExplorationTraits traits = ExplorationTraits.DoubleJump | ExplorationTraits.Dash)
            => new ClassKit(ClassKitId.Duelista, "Duelista", ClassRole.Dps, ClassResourceType.Energy, traits, Stats());

        [Test]
        public void CreateStatSheetDefinesBaseValues()
        {
            var sheet = Kit().CreateStatSheet();
            Assert.That(sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(120f));
            Assert.That(sheet.Has(StatType.Damage), Is.False);
        }

        [Test]
        public void EachStatSheetIsIndependent()
        {
            var kit = Kit();
            var a = kit.CreateStatSheet();
            a.AddModifier(StatType.MaxHealth, new StatModifier(ModifierKind.Flat, 30f, this));
            Assert.That(kit.CreateStatSheet().ValueOf(StatType.MaxHealth), Is.EqualTo(120f));
        }

        [Test]
        public void SheetFeedsCharacterVitals()
        {
            var vitals = new CharacterVitals(Kit().CreateStatSheet());
            Assert.That(vitals.ClassResource.Max, Is.EqualTo(60f));
        }

        [Test]
        public void HasTraitChecksFlags()
        {
            var kit = Kit();
            Assert.That(kit.HasTrait(ExplorationTraits.Dash), Is.True);
            Assert.That(kit.HasTrait(ExplorationTraits.DoubleJump | ExplorationTraits.Dash), Is.True);
            Assert.That(kit.HasTrait(ExplorationTraits.Climb), Is.False);
            Assert.That(kit.HasTrait(ExplorationTraits.None), Is.False);
        }

        [Test]
        public void BaseStatsAreCopied()
        {
            var source = Stats();
            var kit = new ClassKit(ClassKitId.Vigia, "Vigía", ClassRole.Tank, ClassResourceType.Focus,
                ExplorationTraits.Climb, source);
            source[StatType.MaxHealth] = 1f;
            Assert.That(kit.BaseStats[StatType.MaxHealth], Is.EqualTo(120f));
        }

        [Test]
        public void InvalidArgumentsThrow()
        {
            Assert.Throws<ArgumentException>(() => new ClassKit(ClassKitId.Vigia, " ", ClassRole.Tank,
                ClassResourceType.Focus, ExplorationTraits.None, Stats()));
            Assert.Throws<ArgumentNullException>(() => new ClassKit(ClassKitId.Vigia, "V", ClassRole.Tank,
                ClassResourceType.Focus, ExplorationTraits.None, null));
            var negative = new Dictionary<StatType, float> { { StatType.Armor, -1f } };
            Assert.Throws<ArgumentOutOfRangeException>(() => new ClassKit(ClassKitId.Vigia, "V", ClassRole.Tank,
                ClassResourceType.Focus, ExplorationTraits.None, negative));
        }
    }
}
