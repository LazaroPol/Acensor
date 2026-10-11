using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors.Omens;
using Specimen.Core.Floors.Types;

namespace Specimen.Tests.Core.Floors.Omens
{
    public class FloorModifierCatalogTests
    {
        private static readonly FloorModifierCatalog Catalog = FloorModifierCatalog.Default;

        [Test]
        public void Default_RegistersEveryKind()
        {
            foreach (FloorModifierKind kind in Enum.GetValues(typeof(FloorModifierKind)))
                Assert.That(Catalog.Get(kind).Kind, Is.EqualTo(kind));
        }

        [Test]
        public void Default_HasGddExamples()
        {
            Assert.That(Catalog.Get(FloorModifierKind.Darkness).DisplayName, Is.EqualTo("Oscuridad"));
            Assert.That(Catalog.Get(FloorModifierKind.IgneousEnemies).RequiresCombat, Is.True);
            Assert.That(Catalog.Get(FloorModifierKind.LowGravity).RequiresCombat, Is.False);
        }

        [Test]
        public void CompatibleWith_PeacefulFloor_ExcludesCombatModifiers()
        {
            var peaceful = Catalog.CompatibleWith(new SanctuaryFloor());
            Assert.That(peaceful.Any(m => m.RequiresCombat), Is.False);
            Assert.That(peaceful.Count, Is.EqualTo(Catalog.All.Count - 1));
        }

        [Test]
        public void CompatibleWith_CombatFloor_IncludesAll()
        {
            Assert.That(Catalog.CompatibleWith(new BossFloor()).Count, Is.EqualTo(Catalog.All.Count));
        }

        [Test]
        public void InvalidInput_Throws()
        {
            var fog = new FloorModifier(FloorModifierKind.Fog, "Niebla", "bruma");
            Assert.Throws<ArgumentException>(() => new FloorModifierCatalog(new[] { fog, fog }));
            Assert.Throws<ArgumentException>(() => new FloorModifierCatalog(new FloorModifier[] { null }));
            Assert.Throws<ArgumentNullException>(() => new FloorModifierCatalog(null));
            Assert.Throws<ArgumentException>(() => new FloorModifier(FloorModifierKind.Fog, " ", "x"));
            Assert.Throws<ArgumentException>(() => new FloorModifier(FloorModifierKind.Fog, "x", ""));
            Assert.Throws<ArgumentNullException>(() => Catalog.CompatibleWith(null));
        }

        [Test]
        public void Get_Unregistered_Throws()
        {
            var only = new FloorModifierCatalog(new[] { new FloorModifier(FloorModifierKind.Fog, "Niebla", "b") });
            Assert.That(only.Contains(FloorModifierKind.Frost), Is.False);
            Assert.Throws<KeyNotFoundException>(() => only.Get(FloorModifierKind.Frost));
        }
    }
}
