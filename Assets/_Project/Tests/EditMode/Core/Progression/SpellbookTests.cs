using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Classes;
using Specimen.Core.Progression;

namespace Specimen.Tests.Core.Progression
{
    [TestFixture]
    public class SpellbookTests
    {
        [Test]
        public void EveryKitHasFiveSpellsWithUniqueIds()
        {
            var ids = Enum.GetValues(typeof(ClassKitId)).Cast<ClassKitId>()
                .SelectMany(k => KitSpellbooks.For(k).All).Select(s => s.Id).ToList();
            Assert.That(ids.Count, Is.EqualTo(30));
            Assert.That(ids.Distinct().Count(), Is.EqualTo(ids.Count));
        }

        [Test]
        public void SpellsUnlockByLevel()
        {
            var book = KitSpellbooks.For(ClassKitId.Arcanista);
            Assert.That(book.KnownAt(1), Is.Empty);
            Assert.That(book.KnownAt(7).Count, Is.EqualTo(3));
            Assert.That(book.LearnedAt(14).Single().Name, Is.EqualTo("Meteoro"));
        }

        [Test]
        public void AreaSpellsHaveRadiusAndBuffsDuration()
        {
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
                foreach (var spell in KitSpellbooks.For(kit).All)
                {
                    if (spell.Kind == SpellKind.Area) Assert.That(spell.Radius, Is.GreaterThan(0f), spell.Id);
                    if (spell.Kind == SpellKind.Escudo || spell.Kind == SpellKind.Furia)
                        Assert.That(spell.Duration, Is.GreaterThan(0f), spell.Id);
                }
        }
    }
}
