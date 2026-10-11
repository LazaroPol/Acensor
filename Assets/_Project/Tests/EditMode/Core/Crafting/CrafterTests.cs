using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class CrafterTests
    {
        private readonly Crafter _crafter = new Crafter(Book());

        [Test]
        public void RejectsNullBook()
        {
            Assert.Throws<ArgumentNullException>(() => new Crafter(null));
        }

        [Test]
        public void CraftConsumesIngredientsAndAddsOutput()
        {
            var inv = Stocked(4, 5, 2);
            Assert.That(_crafter.TryCraft("forjar-espada", ProfessionId.Herrero, inv), Is.EqualTo(CraftResult.Crafted));
            Assert.That(inv.CountOf("mena"), Is.EqualTo(2));
            Assert.That(inv.CountOf("carbon"), Is.EqualTo(1));
            Assert.That(inv.CountOf("espada"), Is.EqualTo(1));
        }

        [Test]
        public void CraftsMultipleUnitsOfOutput()
        {
            var inv = Stocked(4, 0, 2);
            Assert.That(_crafter.TryCraft("vendas", ProfessionId.Sastre, inv), Is.EqualTo(CraftResult.Crafted));
            Assert.That(inv.CountOf("venda"), Is.EqualTo(2));
            Assert.That(inv.CountOf("carbon"), Is.Zero);
        }

        [Test]
        public void FailsWithoutChangingInventory()
        {
            var inv = Stocked(4, 2, 1);
            Assert.That(_crafter.TryCraft("forjar-espada", ProfessionId.Herrero, inv), Is.EqualTo(CraftResult.MissingIngredients));
            Assert.That(_crafter.TryCraft("forjar-espada", ProfessionId.Sastre, inv), Is.EqualTo(CraftResult.WrongProfession));
            Assert.That(_crafter.TryCraft("nada", ProfessionId.Herrero, inv), Is.EqualTo(CraftResult.UnknownRecipe));
            Assert.That(inv.CountOf("mena"), Is.EqualTo(2));
            Assert.That(inv.CountOf("carbon"), Is.EqualTo(1));
        }

        [Test]
        public void UsesSlotFreedByIngredients()
        {
            var inv = Stocked(2, 3, 1);
            Assert.That(_crafter.TryCraft("forjar-espada", ProfessionId.Herrero, inv), Is.EqualTo(CraftResult.Crafted));
            Assert.That(inv.CountOf("espada"), Is.EqualTo(1));
        }

        [Test]
        public void NoSpaceRollsBackEverything()
        {
            var inv = Stocked(3, 4, 2);
            inv.Add(ItemInstance.CreateNew(Sword));
            var changes = 0;
            Assert.That(_crafter.TryCraft("forjar-espada", ProfessionId.Herrero, inv), Is.EqualTo(CraftResult.NoSpace));
            Assert.That(inv.CountOf("mena"), Is.EqualTo(4));
            Assert.That(inv.CountOf("carbon"), Is.EqualTo(2));
            Assert.That(inv.CountOf("espada"), Is.EqualTo(1));
            inv.Changed += () => changes++;
            Assert.That(_crafter.Check("forjar-espada", ProfessionId.Herrero, inv), Is.EqualTo(CraftResult.Crafted));
            Assert.That(changes, Is.Zero, "Check no debe modificar el inventario");
        }

        [Test]
        public void PartialOutputIsUndoneWhenNotAllUnitsFit()
        {
            var inv = Stocked(2, 0, 3);
            inv.Add(ItemInstance.CreateNew(Bandage), 4);
            Assert.That(_crafter.TryCraft("vendas", ProfessionId.Sastre, inv), Is.EqualTo(CraftResult.NoSpace));
            Assert.That(inv.CountOf("venda"), Is.EqualTo(4));
            Assert.That(inv.CountOf("carbon"), Is.EqualTo(3));
        }
    }
}
