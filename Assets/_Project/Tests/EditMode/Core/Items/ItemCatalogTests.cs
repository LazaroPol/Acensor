using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;
using Specimen.Core.Professions;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class ItemCatalogTests
    {
        [Test]
        public void IdsAreUniqueAndResolvable()
        {
            Assert.That(ItemCatalog.All.Select(i => i.Id).Distinct().Count(), Is.EqualTo(ItemCatalog.All.Count));
            Assert.That(ItemCatalog.Get(ItemIds.Madera).Category, Is.EqualTo(ItemCategory.Material));
            Assert.Throws<ArgumentException>(() => ItemCatalog.Get("nada"));
        }

        [Test]
        public void EveryConsumableWithEffectIsAConsumable()
        {
            foreach (var item in ItemCatalog.All)
                if (ConsumableEffect.TryGet(item.Id, out var effect))
                {
                    Assert.That(item.Category, Is.EqualTo(ItemCategory.Consumable), item.Id);
                    Assert.That(effect.Health + effect.Resource, Is.GreaterThan(0f), item.Id);
                }
        }

        [Test]
        public void EveryProfessionHasAtLeastTwoRecipes()
        {
            var book = DefaultRecipes.CreateBook();
            foreach (ProfessionId profession in Enum.GetValues(typeof(ProfessionId)))
                Assert.That(book.For(profession).Count, Is.GreaterThanOrEqualTo(2), profession.ToString());
        }

        [Test]
        public void BlacksmithCanSmeltAndForge()
        {
            var crafter = new Crafter(DefaultRecipes.CreateBook());
            var bag = new Inventory(10);
            bag.Add(ItemInstance.CreateNew(ItemCatalog.Get(ItemIds.MineralHierro)), 6);
            bag.Add(ItemInstance.CreateNew(ItemCatalog.Get(ItemIds.Madera)), 1);
            for (int i = 0; i < 3; i++)
                Assert.That(crafter.TryCraft("herrero.lingote", ProfessionId.Herrero, bag), Is.EqualTo(CraftResult.Crafted));
            Assert.That(crafter.TryCraft("herrero.espada", ProfessionId.Herrero, bag), Is.EqualTo(CraftResult.Crafted));
            Assert.That(bag.CountOf(ItemIds.EspadaHierro), Is.EqualTo(1));
            Assert.That(crafter.TryCraft("herrero.espada", ProfessionId.Sastre, bag), Is.EqualTo(CraftResult.WrongProfession));
        }
    }
}
