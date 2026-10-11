using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;
using static Specimen.Tests.Core.Crafting.SynergyFixtures;

namespace Specimen.Tests.Core.Crafting
{
    [TestFixture]
    public class SynergyCrafterTests
    {
        private readonly SynergyCrafter _crafter = new SynergyCrafter(SynergyBook());

        [Test]
        public void RejectsNullBook()
        {
            Assert.Throws<ArgumentNullException>(() => new SynergyCrafter(null));
        }

        [Test]
        public void UnknownRecipeIsReported()
        {
            var inv = Stocked(5, 2, 2);
            Assert.That(_crafter.TryCraft("nada", ProfessionId.Herrero, SmithAndTailor, inv),
                Is.EqualTo(CraftResult.UnknownRecipe));
        }

        [Test]
        public void LockedWhenGroupLacksAPartner()
        {
            var inv = Stocked(5, 2, 2);
            var group = new[] { ProfessionId.Herrero, ProfessionId.Cocinero };
            Assert.That(_crafter.TryCraft("armadura-mixta", ProfessionId.Herrero, group, inv),
                Is.EqualTo(CraftResult.SynergyLocked));
            Assert.That(inv.CountOf(Ore.Id), Is.EqualTo(2));
        }

        [Test]
        public void OutsiderCannotCraftEvenIfUnlocked()
        {
            var inv = Stocked(5, 2, 2);
            var group = new[] { ProfessionId.Herrero, ProfessionId.Sastre, ProfessionId.Cocinero };
            Assert.That(_crafter.TryCraft("armadura-mixta", ProfessionId.Cocinero, group, inv),
                Is.EqualTo(CraftResult.WrongProfession));
        }

        [Test]
        public void MissingIngredientsAreReported()
        {
            var inv = Stocked(5, 1, 2);
            Assert.That(_crafter.Check("armadura-mixta", ProfessionId.Sastre, SmithAndTailor, inv),
                Is.EqualTo(CraftResult.MissingIngredients));
        }

        [Test]
        public void EitherPartnerCraftsAndConsumesIngredients()
        {
            var inv = Stocked(5, 4, 4);
            Assert.That(_crafter.TryCraft("armadura-mixta", ProfessionId.Sastre, SmithAndTailor, inv),
                Is.EqualTo(CraftResult.Crafted));
            Assert.That(_crafter.TryCraft("armadura-mixta", ProfessionId.Herrero, SmithAndTailor, inv),
                Is.EqualTo(CraftResult.Crafted));
            Assert.That(inv.CountOf(Armor.Id), Is.EqualTo(2));
            Assert.That(inv.CountOf(Ore.Id) + inv.CountOf(Coal.Id), Is.EqualTo(0));
        }

        [Test]
        public void NoSpaceRestoresInventory()
        {
            var inv = Stocked(2, 3, 3);
            Assert.That(_crafter.TryCraft("armadura-mixta", ProfessionId.Herrero, SmithAndTailor, inv),
                Is.EqualTo(CraftResult.NoSpace));
            Assert.That(inv.CountOf(Ore.Id), Is.EqualTo(3));
            Assert.That(inv.CountOf(Coal.Id), Is.EqualTo(3));
            Assert.That(inv.CountOf(Armor.Id), Is.EqualTo(0));
        }
    }
}
