using System;
using NUnit.Framework;
using Specimen.Core.Visuals;

namespace Specimen.Tests.Core.Visuals
{
    [TestFixture]
    public class VisualCatalogTests
    {
        private VisualCatalog<string> _catalog;

        [SetUp]
        public void SetUp() => _catalog = new VisualCatalog<string>(new DefaultGreyboxStyles());

        [Test]
        public void EmptyCatalogResolvesEverythingToGreybox()
        {
            foreach (VisualId id in Enum.GetValues(typeof(VisualId)))
            {
                var resolution = _catalog.Resolve(id);
                Assert.That(resolution.HasAsset, Is.False);
                Assert.That(resolution.Asset, Is.Null);
                Assert.That(resolution.Greybox, Is.Not.Null);
                Assert.That(resolution.Id, Is.EqualTo(id));
            }
        }

        [Test]
        public void AssignedIdResolvesToAsset()
        {
            _catalog.Assign(VisualId.Muro, "Entorno_Muro_Liso01");

            var resolution = _catalog.Resolve(VisualId.Muro);

            Assert.That(resolution.HasAsset, Is.True);
            Assert.That(resolution.Asset, Is.EqualTo("Entorno_Muro_Liso01"));
            Assert.That(resolution.Greybox, Is.Null);
            Assert.That(_catalog.Resolve(VisualId.Suelo).HasAsset, Is.False);
        }

        [Test]
        public void ReassignReplacesAsset()
        {
            _catalog.Assign(VisualId.Jugador, "a");
            _catalog.Assign(VisualId.Jugador, "b");

            Assert.That(_catalog.Resolve(VisualId.Jugador).Asset, Is.EqualTo("b"));
            Assert.That(_catalog.AssignedCount, Is.EqualTo(1));
        }

        [Test]
        public void UnassignFallsBackToGreybox()
        {
            _catalog.Assign(VisualId.Loot, "Objetos_Caja01");

            Assert.That(_catalog.Unassign(VisualId.Loot), Is.True);
            Assert.That(_catalog.Unassign(VisualId.Loot), Is.False);
            Assert.That(_catalog.Resolve(VisualId.Loot).HasAsset, Is.False);
        }

        [Test]
        public void AssigningNullFailsFast()
        {
            Assert.Throws<ArgumentNullException>(() => _catalog.Assign(VisualId.Puerta, null));
            Assert.That(_catalog.AssignedCount, Is.EqualTo(0));
        }

        [Test]
        public void RequiresDefaults()
            => Assert.Throws<ArgumentNullException>(() => new VisualCatalog<string>(null));

        [Test]
        public void UsesInjectedDefaults()
        {
            var style = new GreyboxStyle(GreyboxPrimitive.Cilindro,
                new GreyboxColor(1f, 0f, 1f), new VisualScale(2f, 2f, 2f));
            var catalog = new VisualCatalog<string>(new FixedDefaults(style));

            Assert.That(catalog.Resolve(VisualId.Enemigo).Greybox, Is.SameAs(style));
        }

        private sealed class FixedDefaults : IGreyboxDefaults
        {
            private readonly GreyboxStyle _style;
            public FixedDefaults(GreyboxStyle style) => _style = style;
            public GreyboxStyle For(VisualId id) => _style;
        }
    }
}
