using System;
using NUnit.Framework;
using Specimen.Core.Floors.Omens;
using Specimen.Core.Floors.Types;

namespace Specimen.Tests.Core.Floors.Omens
{
    public class OmenTests
    {
        private static readonly FloorModifierCatalog Catalog = FloorModifierCatalog.Default;

        [Test]
        public void Inscription_AnnouncesTypeAndModifiers()
        {
            var omen = new Omen(new BossFloor(), new[]
            {
                Catalog.Get(FloorModifierKind.Darkness), Catalog.Get(FloorModifierKind.LowGravity)
            });
            var text = omen.Inscription;
            StringAssert.Contains(new BossFloor().DisplayName, text);
            StringAssert.Contains("Oscuridad", text);
            StringAssert.Contains("Gravedad baja", text);
            StringAssert.Contains("las antorchas no prenden aquí", text);
            Assert.That(omen.ToString(), Is.EqualTo(text));
        }

        [Test]
        public void NoModifiers_IsSilent()
        {
            var omen = new Omen(new LootFloor(), new FloorModifier[0]);
            StringAssert.Contains("silencio", omen.Inscription);
            Assert.That(omen.Has(FloorModifierKind.Fog), Is.False);
        }

        [Test]
        public void Has_DetectsModifier()
        {
            var omen = new Omen(new LootFloor(), new[] { Catalog.Get(FloorModifierKind.Fog) });
            Assert.That(omen.Has(FloorModifierKind.Fog), Is.True);
            Assert.That(omen.Has(FloorModifierKind.Frost), Is.False);
        }

        [Test]
        public void NullArguments_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => new Omen(null, new FloorModifier[0]));
            Assert.Throws<ArgumentNullException>(() => new Omen(new LootFloor(), null));
        }
    }
}
