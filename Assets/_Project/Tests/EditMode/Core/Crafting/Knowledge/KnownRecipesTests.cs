using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.Knowledge.KnowledgeFixtures;

namespace Specimen.Tests.Core.Crafting.Knowledge
{
    [TestFixture]
    public class KnownRecipesTests
    {
        private KnownRecipes _known;

        [SetUp]
        public void SetUp() => _known = new KnownRecipes();

        private static KnowledgeDefinition Vol(int n) =>
            KnowledgeDefinition.BookVolume("libro_forjador", n, ProfessionId.Herrero, 15);

        [Test]
        public void LosVolumenesSeRegistranYCincoCompletanLaColeccion()
        {
            var condition = UnlockCondition.CollectionComplete("libro_forjador");
            for (var n = 1; n <= 4; n++) _known.Register(Vol(n));
            Assert.AreEqual(4, _known.VolumesRead("libro_forjador"));
            Assert.IsFalse(_known.Satisfies(condition));
            _known.Register(Vol(5));
            Assert.IsTrue(_known.HasCompleted("libro_forjador"));
            Assert.IsTrue(_known.Satisfies(condition));
        }

        [Test]
        public void ElMismoVolumenNoCuentaDosVeces()
        {
            Assert.IsTrue(_known.Register(Vol(1)));
            Assert.IsFalse(_known.Register(Vol(1)));
            Assert.AreEqual(1, _known.VolumesRead("libro_forjador"));
        }

        [Test]
        public void ColeccionDesconocidaTieneCeroVolumenes()
        {
            Assert.AreEqual(0, _known.VolumesRead("nada"));
            Assert.AreEqual(0, _known.VolumesRead(null));
        }

        [Test]
        public void SatisfacePorConocimientoOPorReceta()
        {
            _known.Register(Scroll("p.bolsa", "sastre.bolsa"));
            Assert.IsTrue(_known.Satisfies(UnlockCondition.KnowledgeRequired("p.bolsa")));
            Assert.IsTrue(_known.Satisfies(UnlockCondition.KnowledgeRequired("sastre.bolsa")));
            Assert.IsFalse(_known.Satisfies(UnlockCondition.KnowledgeRequired("otro")));
        }

        [Test]
        public void ExponeLoAprendidoOrdenadoParaGuardar()
        {
            _known.Register(Scroll("p.b", "r.b"));
            _known.Register(Vol(2));
            _known.RestoreRecipe("r.a");
            CollectionAssert.AreEqual(new[] { "libro_forjador.vol2", "p.b" }, _known.ReadKnowledge);
            CollectionAssert.AreEqual(new[] { "r.a", "r.b" }, _known.LearnedRecipes);
        }

        [Test]
        public void RestaurarLoLeidoRecuperaLosVolumenes()
        {
            var restored = new KnownRecipes();
            for (var n = 1; n <= 5; n++) restored.RestoreRead(Vol(n));
            Assert.IsTrue(restored.HasCompleted("libro_forjador"));
        }

        [Test]
        public void RestaurarRecetaVaciaFalla() =>
            Assert.Throws<System.ArgumentException>(() => _known.RestoreRecipe(" "));
    }
}
