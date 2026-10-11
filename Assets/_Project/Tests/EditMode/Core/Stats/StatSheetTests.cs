using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Stats
{
    [TestFixture]
    public class StatSheetTests
    {
        [Test]
        public void Define_ThenGetAndValueOf()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.MaxHealth, 100f);
            sheet.AddModifier(StatType.MaxHealth, StatModifier.Percent(0.5f));
            Assert.That(sheet.Has(StatType.MaxHealth), Is.True);
            Assert.That(sheet.ValueOf(StatType.MaxHealth), Is.EqualTo(150f).Within(1e-3f));
        }

        [Test]
        public void Define_Twice_Throws()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.Armor, 1f);
            Assert.Throws<InvalidOperationException>(() => sheet.Define(StatType.Armor, 2f));
        }

        [Test]
        public void Get_Undefined_Throws()
            => Assert.Throws<KeyNotFoundException>(() => new StatSheet().Get(StatType.Damage));

        [Test]
        public void RemoveAllFromSource_ClearsAcrossStats()
        {
            var sheet = new StatSheet();
            sheet.Define(StatType.Damage, 10f);
            sheet.Define(StatType.MoveSpeed, 5f);
            var relic = new object();
            sheet.AddModifier(StatType.Damage, StatModifier.Multiply(2f, relic));
            sheet.AddModifier(StatType.MoveSpeed, StatModifier.Flat(1f, relic));
            Assert.That(sheet.RemoveAllFromSource(relic), Is.EqualTo(2));
            Assert.That(sheet.ValueOf(StatType.Damage), Is.EqualTo(10f));
            Assert.That(sheet.ValueOf(StatType.MoveSpeed), Is.EqualTo(5f));
        }
    }
}
