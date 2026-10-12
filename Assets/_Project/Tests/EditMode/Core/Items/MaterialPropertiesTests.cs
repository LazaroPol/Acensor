using System;
using NUnit.Framework;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class MaterialPropertiesTests
    {
        private static ItemDefinition Coal() => new ItemDefinition("carbon", "Carbón", ItemCategory.Material,
            Rarity.Common, 0, 20, 1, 0.4f, MaterialTags.Mineral | MaterialTags.Combustible, "icono.carbon");

        [Test]
        public void TagValuesAreExplicitAndStable()
        {
            Assert.That((int)MaterialTags.Madera, Is.EqualTo(1));
            Assert.That((int)MaterialTags.Metal, Is.EqualTo(2));
            Assert.That((int)MaterialTags.Hueso, Is.EqualTo(128));
            Assert.That((int)MaterialTags.Vidrio, Is.EqualTo(1024));
        }

        [Test]
        public void ExposesTierWeightTagsAndIcon()
        {
            var coal = Coal();
            Assert.That(coal.Tier, Is.EqualTo(1));
            Assert.That(coal.Weight, Is.EqualTo(0.4f));
            Assert.That(coal.IconKey, Is.EqualTo("icono.carbon"));
            Assert.That(coal.HasTag(MaterialTags.Combustible), Is.True);
            Assert.That(coal.HasTag(MaterialTags.Mineral | MaterialTags.Combustible), Is.True);
            Assert.That(coal.HasTag(MaterialTags.Madera), Is.False);
            Assert.That(coal.HasTag(MaterialTags.None), Is.False);
        }

        [Test]
        public void OldConstructorKeepsNeutralDefaults()
        {
            var sword = new ItemDefinition("espada", "Espada", ItemCategory.Weapon, Rarity.Common, 50);
            Assert.That(sword.Tier, Is.Zero);
            Assert.That(sword.Weight, Is.Zero);
            Assert.That(sword.Tags, Is.EqualTo(MaterialTags.None));
            Assert.That(sword.IconKey, Is.EqualTo("espada"));
        }

        [TestCase(-1)]
        [TestCase(5)]
        public void TierOutOfRangeFailsFast(int tier)
            => Assert.Throws<ArgumentOutOfRangeException>(() => new ItemDefinition("x", "X",
                ItemCategory.Material, Rarity.Common, 0, 20, tier));

        [Test]
        public void NegativeWeightFailsFast()
            => Assert.Throws<ArgumentOutOfRangeException>(() => new ItemDefinition("x", "X",
                ItemCategory.Material, Rarity.Common, 0, 20, 1, -0.1f));

        [Test]
        public void CatalogMaterialsHaveTierWeightAndTags()
        {
            foreach (var item in ItemCatalog.All)
            {
                if (item.Category != ItemCategory.Material) continue;
                Assert.That(item.Tier, Is.InRange(1, 4), item.Id);
                Assert.That(item.Weight, Is.GreaterThan(0f), item.Id);
                Assert.That(item.Tags, Is.Not.EqualTo(MaterialTags.None), item.Id);
            }
            Assert.That(ItemCatalog.Get(ItemIds.Lingote).HasTag(MaterialTags.Metal), Is.True);
            Assert.That(ItemCatalog.Get(ItemIds.Madera).HasTag(MaterialTags.Madera), Is.True);
        }

        [Test]
        public void InventoryReportsTotalWeight()
        {
            var inventory = new Inventory(4);
            Assert.That(inventory.TotalWeight, Is.Zero);
            inventory.Add(ItemInstance.CreateNew(Coal()), 5);
            inventory.Add(ItemInstance.CreateNew(ItemCatalog.Get(ItemIds.Piedra)), 3);
            Assert.That(inventory.TotalWeight, Is.EqualTo(5 * 0.4f + 3 * 2f).Within(1e-4f));
        }
    }
}
