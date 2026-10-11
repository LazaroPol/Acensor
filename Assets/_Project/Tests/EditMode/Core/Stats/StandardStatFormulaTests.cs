using System.Collections.Generic;
using NUnit.Framework;
using Specimen.Core.Stats;

namespace Specimen.Tests.Core.Stats
{
    [TestFixture]
    public class StandardStatFormulaTests
    {
        private static float Eval(float b, params StatModifier[] mods)
            => StandardStatFormula.Instance.Evaluate(b, new List<StatModifier>(mods));

        [Test]
        public void NoModifiers_ReturnsBase() => Assert.That(Eval(10f), Is.EqualTo(10f));

        [Test]
        public void FlatModifiers_AreSummed()
            => Assert.That(Eval(10f, StatModifier.Flat(5f), StatModifier.Flat(-2f)), Is.EqualTo(13f));

        [Test]
        public void PercentModifiers_StackAdditively()
            => Assert.That(Eval(100f, StatModifier.Percent(0.1f), StatModifier.Percent(0.2f)),
                Is.EqualTo(130f).Within(1e-3f));

        [Test]
        public void Multipliers_StackMultiplicatively()
            => Assert.That(Eval(100f, StatModifier.Multiply(1.5f), StatModifier.Multiply(2f)),
                Is.EqualTo(300f).Within(1e-3f));

        [Test]
        public void OrderOfApplication_IsFlatThenPercentThenMultiplier()
        {
            float v = Eval(10f, StatModifier.Multiply(2f), StatModifier.Percent(0.5f), StatModifier.Flat(10f));
            Assert.That(v, Is.EqualTo(60f).Within(1e-3f));
        }

        [Test]
        public void Result_IsClampedToZero()
            => Assert.That(Eval(10f, StatModifier.Flat(-50f)), Is.EqualTo(0f));
    }
}
