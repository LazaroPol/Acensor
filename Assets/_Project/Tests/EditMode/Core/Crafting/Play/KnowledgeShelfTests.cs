using NUnit.Framework;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Crafting.Play;

namespace Specimen.Tests.Core.Crafting.Play
{
    /// <summary>OF-16: leer un pergamino desde la bolsa deja su receta disponible.</summary>
    [TestFixture]
    public class KnowledgeShelfTests
    {
        [Test]
        public void LeerUnPergaminoEnseñaSuRecetaYLoConsume()
        {
            var play = new PlayFixtures();
            var scroll = PlayFixtures.Scroll();
            play.ReadyFor(scroll);
            play.Give(KnowledgeItems.For(scroll));
            var result = play.Shelf.Read(scroll.Id);
            Assert.AreEqual(ReadOutcome.Learned, result.Value.Outcome);
            Assert.IsTrue(play.Profile.Knowledge.KnowsRecipe(scroll.Teaches[0]));
            Assert.AreEqual(0, play.Profile.Bag.CountOf(scroll.Id));
            StringAssert.StartsWith("Aprendido", KnowledgeShelf.Message(result.Value));
        }

        [Test]
        public void SinNivelNoSeConsumeYAvisaDelNivel()
        {
            var play = new PlayFixtures();
            var scroll = PlayFixtures.Scroll();
            Assert.Greater(scroll.MinLevel, 1);
            play.Give(KnowledgeItems.For(scroll));
            var result = play.Shelf.Read(scroll.Id).Value;
            Assert.AreEqual(ReadOutcome.TooLowLevel, result.Outcome);
            Assert.AreEqual(1, play.Profile.Bag.CountOf(scroll.Id));
            StringAssert.Contains(scroll.MinLevel.ToString(), KnowledgeShelf.Message(result));
        }

        [Test]
        public void LoQueNoEsConocimientoOFaltaEnLaBolsaNoSeLee()
        {
            var play = new PlayFixtures();
            Assert.IsNull(play.Shelf.Read("madera"));
            Assert.IsNull(play.Shelf.Read(PlayFixtures.Scroll().Id));
            Assert.IsTrue(play.Shelf.IsKnowledge(PlayFixtures.Scroll().Id));
        }

        [Test]
        public void ElObjetoDeConocimientoEsSiempreLaMismaDefinicion()
        {
            var scroll = PlayFixtures.Scroll();
            Assert.AreSame(KnowledgeItems.For(scroll), KnowledgeItems.For(scroll));
            StringAssert.StartsWith("Pergamino de", KnowledgeItems.For(scroll).Name);
        }
    }
}
