using System;
using NUnit.Framework;
using Specimen.Core.Echoes;
using Specimen.Core.Echoes.Consequences;
using Specimen.Core.Floors.Omens;
using Specimen.Core.Floors.Types;

namespace Specimen.Tests.Core.Echoes.Consequences
{
    public class EchoConsequenceTests
    {
        private static EchoRegistry Registry(string id = "prisionero", int option = 1)
        {
            var r = new EchoRegistry();
            r.Record(new EchoDecision(id, 2, option));
            return r;
        }

        private static EchoConsequenceTable Table() => new EchoConsequenceTable(new[]
        {
            new EchoConsequence("prisionero", 1, 4, FloorModifierKind.Darkness),
            new EchoConsequence("prisionero", 0, 3, FloorModifierKind.Fog),
            new EchoConsequence("sello", 0, 3, FloorModifierKind.Darkness),
            new EchoConsequence("prisionero", 1, 3, FloorModifierKind.IgneousEnemies),
        });

        [Test]
        public void Consequence_RejectsInvalidValues()
        {
            Assert.Throws<ArgumentException>(() => new EchoConsequence(" ", 0, 1, FloorModifierKind.Fog));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EchoConsequence("x", -1, 1, FloorModifierKind.Fog));
            Assert.Throws<ArgumentOutOfRangeException>(() => new EchoConsequence("x", 0, 0, FloorModifierKind.Fog));
        }

        [Test]
        public void Table_RejectsNullEntries()
        {
            Assert.Throws<ArgumentNullException>(() => new EchoConsequenceTable(null));
            Assert.Throws<ArgumentException>(() => new EchoConsequenceTable(new EchoConsequence[] { null }));
        }

        [Test]
        public void Modifiers_OnlyForChosenOption_AndFromItsFloor()
        {
            var r = Registry();
            Assert.That(Table().ModifiersFor(r, 2), Is.Empty);
            Assert.That(Table().ModifiersFor(r, 3), Is.EqualTo(new[] { FloorModifierKind.IgneousEnemies }));
            Assert.That(Table().ModifiersFor(r, 5),
                Is.EqualTo(new[] { FloorModifierKind.Darkness, FloorModifierKind.IgneousEnemies }));
        }

        [Test]
        public void Modifiers_AreDistinct_AndIgnoreUndecidedEchoes()
        {
            var r = Registry();
            r.Record(new EchoDecision("sello", 3, 0));
            Assert.That(Table().ModifiersFor(r, 5), Has.Count.EqualTo(2));
            Assert.That(Table().ModifiersFor(new EchoRegistry(), 5), Is.Empty);
        }

        [Test]
        public void Adjuster_AddsForcedModifiers_WithoutDuplicates()
        {
            var adjuster = new EchoOmenAdjuster(Table(), FloorModifierCatalog.Default);
            var catalog = FloorModifierCatalog.Default;
            var omen = new Omen(new LootFloor(), new[] { catalog.Get(FloorModifierKind.Darkness) });
            var result = adjuster.Apply(omen, Registry(), 5);
            Assert.That(result.Has(FloorModifierKind.Darkness), Is.True);
            Assert.That(result.Has(FloorModifierKind.IgneousEnemies), Is.True);
            Assert.That(result.Modifiers, Has.Count.EqualTo(2));
            Assert.That(omen.Modifiers, Has.Count.EqualTo(1));
        }

        [Test]
        public void Adjuster_DoesNotForceCombatModifiers_OnCalmFloors()
        {
            var adjuster = new EchoOmenAdjuster(Table(), FloorModifierCatalog.Default);
            var result = adjuster.Apply(new Omen(new PuzzleFloor(), new FloorModifier[0]), Registry(), 5);
            Assert.That(result.Has(FloorModifierKind.IgneousEnemies), Is.False);
            Assert.That(result.Has(FloorModifierKind.Darkness), Is.True);
        }
    }
}
