using System;
using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.Definitions.DefinitionFixtures;

namespace Specimen.Tests.Core.Crafting.Definitions
{
    [TestFixture]
    public class RecipeCatalogTests
    {
        private static RecipeCatalog Catalog() => new RecipeCatalog(new[]
        {
            Make("espada"),
            Make("espada.mejor", unlock: UnlockCondition.KnowledgeRequired("tratado.1"), improves: "espada"),
            Make("espada.maestra", unlock: UnlockCondition.KnowledgeRequired("tratado.2"), improves: "espada.mejor"),
            Make("venda", ProfessionId.Sastre, StationType.TallerSastre),
            Make("mango", ProfessionId.Manitas, StationType.Fragua)
        });

        [Test]
        public void ConsultaPorIdEstacionYOficio()
        {
            var c = Catalog();
            Assert.AreEqual(5, c.Count);
            Assert.AreEqual("venda", c.Get("venda").Id);
            Assert.IsFalse(c.TryGet("nada", out _));
            Assert.AreEqual(4, c.ForStation(StationType.Fragua).Count);
            Assert.AreEqual(3, c.ForProfession(ProfessionId.Herrero).Count);
            Assert.AreEqual("mango", c.ForProfession(ProfessionId.Manitas).Single().Id);
        }

        [Test]
        public void ResuelveLaVarianteMejoradaConocidaMasProfunda()
        {
            var c = Catalog();
            var k = new FakeKnowledge();
            Assert.AreEqual("espada", c.ResolveVariant("espada", k).Id);
            k.Known.Add("tratado.1");
            Assert.AreEqual("espada.mejor", c.ResolveVariant("espada", k).Id);
            k.Known.Add("tratado.2");
            Assert.AreEqual("espada.maestra", c.ResolveVariant("espada", k).Id);
            Assert.AreEqual("espada.mejor", c.ImprovementsOf("espada").Single().Id);
        }

        [Test]
        public void IdsRepetidosOImprovesInexistenteFallanPronto()
        {
            Assert.Throws<ArgumentException>(() => new RecipeCatalog(new[] { Make("a"), Make("a") }));
            Assert.Throws<ArgumentException>(() => new RecipeCatalog(new[] { Make("a", improves: "fantasma") }));
            Assert.Throws<ArgumentException>(() =>
                new RecipeCatalog(new[] { Make("a", improves: "b"), Make("b", improves: "a") }));
        }

        [Test]
        public void ConvierteTodasLasRecetasActuales()
        {
            var legacy = DefaultRecipes.Create();
            var c = RecipeCatalog.FromLegacy(legacy);
            Assert.AreEqual(legacy.Count, c.Count);
            Assert.IsTrue(c.All.All(r => r.IsInstant && r.MinStationLevel == 1
                                          && r.Station == StationTypes.MainOf(r.Profession)));
        }
    }
}
