using NUnit.Framework;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.Knowledge.KnowledgeFixtures;

namespace Specimen.Tests.Core.Crafting.Knowledge
{
    [TestFixture]
    public class KnowledgeReaderTests
    {
        private KnownRecipes _known;

        [SetUp]
        public void SetUp() => _known = new KnownRecipes();

        [Test]
        public void LeerConNivelSuficienteAprendeYConsume()
        {
            var r = KnowledgeReader.Read(Scroll("p.contrapeso", "herrero.contrapeso"),
                Skill(ProfessionId.Herrero, 1), _known);
            Assert.AreEqual(ReadOutcome.Learned, r.Outcome);
            Assert.IsTrue(r.Consumed);
            Assert.IsTrue(_known.KnowsRecipe("herrero.contrapeso"));
            Assert.IsTrue(_known.Knows("p.contrapeso"));
        }

        [Test]
        public void LeerDosVecesDaAlreadyKnownSinConsumir()
        {
            var scroll = Scroll("p.contrapeso", "herrero.contrapeso");
            KnowledgeReader.Read(scroll, Skill(ProfessionId.Herrero, 1), _known);
            var r = KnowledgeReader.Read(scroll, Skill(ProfessionId.Herrero, 1), _known);
            Assert.AreEqual(ReadOutcome.AlreadyKnown, r.Outcome);
            Assert.IsFalse(r.Consumed);
        }

        [Test]
        public void OtroObjetoConRecetasYaSabidasDaAlreadyKnown()
        {
            _known.RestoreRecipe("herrero.contrapeso");
            var r = KnowledgeReader.Read(Scroll("p.otro", "herrero.contrapeso"),
                Skill(ProfessionId.Herrero, 1), _known);
            Assert.AreEqual(ReadOutcome.AlreadyKnown, r.Outcome);
        }

        [Test]
        public void NivelBajoNoAprendeNiConsume()
        {
            var r = KnowledgeReader.Read(Treatise("t.acero", "herrero.espada_acero"),
                Skill(ProfessionId.Herrero, 9), _known);
            Assert.AreEqual(ReadOutcome.TooLowLevel, r.Outcome);
            Assert.AreEqual(10, r.RequiredLevel);
            Assert.IsFalse(r.Consumed);
            Assert.IsFalse(_known.KnowsRecipe("herrero.espada_acero"));
        }

        [Test]
        public void OficioDistintoNoAprendeNiConsume()
        {
            var r = KnowledgeReader.Read(Scroll("p.contrapeso", "herrero.contrapeso"),
                Skill(ProfessionId.Cocinero, 20), _known);
            Assert.AreEqual(ReadOutcome.WrongProfession, r.Outcome);
            Assert.IsFalse(r.Consumed);
            Assert.IsFalse(_known.Knows("p.contrapeso"));
        }

        [Test]
        public void UnTratadoEnsenaTodoSuGrupo()
        {
            KnowledgeReader.Read(Treatise("t.cueros", "a", "b"), Skill(ProfessionId.Herrero, 10), _known);
            CollectionAssert.AreEqual(new[] { "a", "b" }, _known.LearnedRecipes);
        }
    }
}
