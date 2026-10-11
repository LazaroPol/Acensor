using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Floors.Types;

namespace Specimen.Tests.Core.Floors.Types
{
    public class FloorTypeCatalogTests
    {
        [Test]
        public void Default_ContainsEveryKind()
        {
            var catalog = FloorTypeCatalog.Default;
            foreach (FloorTypeKind kind in Enum.GetValues(typeof(FloorTypeKind)))
                Assert.That(catalog.Get(kind).Kind, Is.EqualTo(kind));
            Assert.That(catalog.All.Count, Is.EqualTo(Enum.GetValues(typeof(FloorTypeKind)).Length));
        }

        [Test]
        public void Default_EveryTypeHasNameAndChallenge()
        {
            foreach (var t in FloorTypeCatalog.Default.All)
            {
                Assert.That(t.DisplayName, Is.Not.Empty);
                Assert.That(t.Challenge, Is.Not.Empty);
            }
        }

        [Test]
        public void Duplicate_Throws()
        {
            Assert.Throws<ArgumentException>(() => new FloorTypeCatalog(new IFloorType[] { new LootFloor(), new LootFloor() }));
        }

        [Test]
        public void Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new FloorTypeCatalog(null));
            Assert.Throws<ArgumentException>(() => new FloorTypeCatalog(new IFloorType[] { null }));
        }

        [Test]
        public void MissingKind_Throws()
        {
            var catalog = new FloorTypeCatalog(new IFloorType[] { new LootFloor() });
            Assert.That(catalog.Contains(FloorTypeKind.Boss), Is.False);
            Assert.Throws<KeyNotFoundException>(() => catalog.Get(FloorTypeKind.Boss));
        }

        [Test]
        public void SanctuaryAndPuzzle_HaveNoCombat()
        {
            var calm = FloorTypeCatalog.Default.WithCombat(false).Select(t => t.Kind).ToList();
            Assert.That(calm, Does.Contain(FloorTypeKind.Sanctuary).And.Contain(FloorTypeKind.Puzzle));
            Assert.That(calm, Does.Not.Contain(FloorTypeKind.Boss));
        }
    }
}
