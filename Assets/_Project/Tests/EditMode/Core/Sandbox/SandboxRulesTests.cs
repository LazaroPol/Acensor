using System;
using NUnit.Framework;
using Specimen.Core.Bestiary;
using Specimen.Core.Classes;
using Specimen.Core.Combat.Bosses;
using Specimen.Core.Randomness;
using Specimen.Core.Sandbox;

namespace Specimen.Tests.Core.Sandbox
{
    [TestFixture]
    public class SandboxRulesTests
    {
        [Test]
        public void EveryKitHasASignatureEffect()
        {
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
                Assert.That(SignatureEffects.For(kit), Is.Not.Null, kit.ToString());
        }

        [Test]
        public void DamagingSignaturesHaveARadius()
        {
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
            {
                var effect = SignatureEffects.For(kit);
                if (effect.DealsDamage) Assert.That(effect.Radius, Is.GreaterThan(0f), kit.ToString());
            }
        }

        [Test]
        public void ShieldLastsAndHealIsInstant()
        {
            Assert.That(SignatureEffects.For(ClassKitId.Bastion).Duration, Is.GreaterThan(0f));
            Assert.That(SignatureEffects.For(ClassKitId.Oraculo).Kind, Is.EqualTo(SignatureKind.Curacion));
        }

        [Test]
        public void CommonCreaturesDropOnceAndTheBossDropsMore()
        {
            var random = new Pcg32Random(42UL);
            Assert.That(SandboxLoot.DropsFor(DefaultBestiary.Find("puglin"), random).Count, Is.EqualTo(1));
            Assert.That(SandboxLoot.DropsFor(DefaultBestiary.Find(ActOneBoss.Id), random).Count, Is.EqualTo(3));
            Assert.That(SandboxLoot.RingOf(DefaultBestiary.Find(ActOneBoss.Id)), Is.EqualTo(ActOneBoss.Ring));
        }

        [Test]
        public void DropsHaveValidQuantities()
        {
            var random = new Pcg32Random(7UL);
            for (int i = 0; i < 50; i++)
                foreach (var drop in SandboxLoot.DropsFor(DefaultBestiary.Find("diablillo"), random))
                    Assert.That(drop.Quantity, Is.InRange(1, 3));
        }
    }
}
