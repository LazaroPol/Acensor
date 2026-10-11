using System;
using NUnit.Framework;
using Specimen.Core.Items;

namespace Specimen.Tests.Core.Items
{
    [TestFixture]
    public class ItemDefinitionTests
    {
        [Test]
        public void WeaponScalesDurabilityWithRarity()
        {
            var sword = new ItemDefinition("espada", "Espada", ItemCategory.Weapon, Rarity.Epic, 50);
            Assert.That(sword.IsDegradable, Is.True);
            Assert.That(sword.IsStackable, Is.False);
            Assert.That(sword.MaxDurability, Is.EqualTo(100));
        }

        [Test]
        public void MaterialStacksAndDoesNotDegrade()
        {
            var scrap = new ItemDefinition("chatarra", "Chatarra", ItemCategory.Material, Rarity.Common, maxStack: 20);
            Assert.That(scrap.IsDegradable, Is.False);
            Assert.That(scrap.IsStackable, Is.True);
            Assert.That(scrap.MaxDurability, Is.EqualTo(0));
        }

        [Test]
        public void DegradableCannotStack()
        {
            Assert.Throws<ArgumentException>(() =>
                new ItemDefinition("casco", "Casco", ItemCategory.Armor, Rarity.Rare, 30, 5));
        }

        [Test]
        public void InvalidArgumentsThrow()
        {
            Assert.Throws<ArgumentException>(() => new ItemDefinition(" ", "X", ItemCategory.Tool, Rarity.Common));
            Assert.Throws<ArgumentNullException>(() => new ItemDefinition("x", null, ItemCategory.Tool, Rarity.Common));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ItemDefinition("x", "X", ItemCategory.Tool, Rarity.Common, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ItemDefinition("x", "X", ItemCategory.Tool, Rarity.Common, maxStack: 0));
        }
    }
}
