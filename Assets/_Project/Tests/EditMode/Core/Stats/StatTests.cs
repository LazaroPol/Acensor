using System;
using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Stats
{
    [TestFixture]
    public class StatTests
    {
        private sealed class CountingFormula : IStatFormula
        {
            public int Calls;
            public float Evaluate(float baseValue, IReadOnlyList<StatModifier> modifiers) { Calls++; return baseValue; }
        }

        [Test]
        public void Value_ReflectsAddedAndRemovedModifiers()
        {
            var stat = new Stat(10f);
            var m = StatModifier.Flat(5f);
            stat.AddModifier(m);
            Assert.That(stat.Value, Is.EqualTo(15f));
            Assert.That(stat.RemoveModifier(m), Is.True);
            Assert.That(stat.Value, Is.EqualTo(10f));
            Assert.That(stat.RemoveModifier(m), Is.False);
        }

        [Test]
        public void RemoveAllFromSource_RemovesOnlyThatSource()
        {
            var sword = new object();
            var stat = new Stat(10f);
            stat.AddModifier(StatModifier.Flat(5f, sword));
            stat.AddModifier(StatModifier.Percent(1f, sword));
            stat.AddModifier(StatModifier.Flat(1f, "buff"));
            Assert.That(stat.RemoveAllFromSource(sword), Is.EqualTo(2));
            Assert.That(stat.Value, Is.EqualTo(11f));
        }

        [Test]
        public void Value_IsCachedUntilSomethingChanges()
        {
            var formula = new CountingFormula();
            var stat = new Stat(10f, formula);
            _ = stat.Value; _ = stat.Value;
            Assert.That(formula.Calls, Is.EqualTo(1));
            stat.BaseValue = 20f;
            Assert.That(stat.Value, Is.EqualTo(20f));
            Assert.That(formula.Calls, Is.EqualTo(2));
        }

        [Test]
        public void Changed_IsRaisedOnEveryMutation()
        {
            var stat = new Stat(1f);
            int raised = 0;
            stat.Changed += _ => raised++;
            var m = StatModifier.Flat(1f, "x");
            stat.AddModifier(m);
            stat.BaseValue = 2f;
            stat.RemoveAllFromSource("x");
            Assert.That(raised, Is.EqualTo(3));
        }

        [Test]
        public void Guards_RejectInvalidInput()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Stat(-1f));
            Assert.Throws<ArgumentNullException>(() => new Stat(1f).AddModifier(null));
            Assert.Throws<ArgumentNullException>(() => new Stat(1f).RemoveAllFromSource(null));
        }
    }
}
