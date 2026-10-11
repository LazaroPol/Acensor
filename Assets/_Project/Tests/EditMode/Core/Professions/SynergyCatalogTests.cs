using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Professions
{
    [TestFixture]
    public class SynergyCatalogTests
    {
        private readonly SynergyCatalog _catalog = SynergyCatalog.CreateDefault();

        [Test]
        public void DefaultHasSevenSynergies() =>
            Assert.That(DefaultProfessionSynergies.Create(), Has.Count.EqualTo(7));

        [Test]
        public void PairUnlocksItsRecipe()
        {
            var found = _catalog.AvailableFor(new[] { ProfessionId.Quimico, ProfessionId.Cocinero });
            Assert.That(found.Select(s => s.Recipe), Is.EquivalentTo(new[] { "Festín alquímico" }));
        }

        [Test]
        public void SingleProfessionUnlocksNothing() =>
            Assert.That(_catalog.AvailableFor(new[] { ProfessionId.Ingeniero }), Is.Empty);

        [Test]
        public void IngenieroHubUnlocksSeveral()
        {
            var group = new[] { ProfessionId.Ingeniero, ProfessionId.Herrero, ProfessionId.Escultor, ProfessionId.Manitas };
            Assert.That(_catalog.AvailableFor(group), Has.Count.EqualTo(3));
        }

        [Test]
        public void SynergyRequiresDistinctProfessions() =>
            Assert.Throws<ArgumentException>(() => new ProfessionSynergy(ProfessionId.Sastre, ProfessionId.Sastre, "x"));

        [Test]
        public void SynergyRequiresRecipe() =>
            Assert.Throws<ArgumentException>(() => new ProfessionSynergy(ProfessionId.Sastre, ProfessionId.Herrero, " "));

        [Test]
        public void InvolvesBothMembers()
        {
            var s = new ProfessionSynergy(ProfessionId.Sastre, ProfessionId.Herrero, "x");
            Assert.That(s.Involves(ProfessionId.Herrero) && s.Involves(ProfessionId.Sastre), Is.True);
            Assert.That(s.Involves(ProfessionId.Quimico), Is.False);
        }
    }
}
