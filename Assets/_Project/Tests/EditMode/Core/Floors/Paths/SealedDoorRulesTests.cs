using NUnit.Framework;
using Specimen.Core.Floors.Paths;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Floors.Paths
{
    public class SealedDoorRulesTests
    {
        [TestCase(ProfessionId.Manitas, SealMethod.Forzar)]
        [TestCase(ProfessionId.Quimico, SealMethod.Disolver)]
        [TestCase(ProfessionId.Escultor, SealMethod.RestaurarEstatua)]
        public void Opener_HasItsMethod(ProfessionId profession, SealMethod expected)
        {
            Assert.That(SealedDoorRules.TryGetMethod(profession, out var method), Is.True);
            Assert.That(method, Is.EqualTo(expected));
        }

        [TestCase(ProfessionId.Herrero)]
        [TestCase(ProfessionId.Sastre)]
        [TestCase(ProfessionId.Cocinero)]
        [TestCase(ProfessionId.Ingeniero)]
        public void OtherProfessions_CannotOpen(ProfessionId profession)
        {
            Assert.That(SealedDoorRules.TryGetMethod(profession, out _), Is.False);
            Assert.That(SealedDoorRules.CanOpen(new[] { profession }), Is.False);
        }

        [Test]
        public void Party_CanOpen_IfAnyMemberIsAnOpener()
        {
            Assert.That(SealedDoorRules.CanOpen(new[] { ProfessionId.Herrero, ProfessionId.Escultor }), Is.True);
            Assert.That(SealedDoorRules.CanOpen(null), Is.False);
            Assert.That(SealedDoorRules.Openers, Has.Count.EqualTo(3));
        }
    }
}
