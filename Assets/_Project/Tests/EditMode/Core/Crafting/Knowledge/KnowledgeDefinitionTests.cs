using System;
using NUnit.Framework;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.Knowledge.KnowledgeFixtures;

namespace Specimen.Tests.Core.Crafting.Knowledge
{
    [TestFixture]
    public class KnowledgeDefinitionTests
    {
        [Test]
        public void UnTratadoPuedeEnsenarVariasRecetas()
        {
            var t = Treatise("tratado.guisos", "cocinero.estofado", "cocinero.sopa", "cocinero.caldo");
            Assert.AreEqual(3, t.Teaches.Count);
            Assert.AreEqual(Rarity.Rare, t.Rarity);
        }

        [Test]
        public void UnTratadoNoPuedeEnsenarMasDeTres() =>
            Assert.Throws<ArgumentException>(() => Treatise("t", "a", "b", "c", "d"));

        [Test]
        public void UnPergaminoEnsenaExactamenteUnaReceta() =>
            Assert.Throws<ArgumentException>(() => new KnowledgeDefinition("p", KnowledgeType.Scroll,
                Rarity.Common, ProfessionId.Herrero, 1, new[] { "a", "b" }));

        [Test]
        public void RecetasVaciasORepetidasFallan()
        {
            Assert.Throws<ArgumentException>(() => Treatise("t", "a", "a"));
            Assert.Throws<ArgumentException>(() => Treatise("t", " "));
        }

        [Test]
        public void ElVolumenTieneIdDeColeccionYNumero()
        {
            var v = KnowledgeDefinition.BookVolume("libro_forjador", 2, ProfessionId.Herrero, 15);
            Assert.AreEqual("libro_forjador.vol2", v.Id);
            Assert.AreEqual("libro_forjador", v.CollectionId);
            Assert.IsTrue(v.IsVolume);
            Assert.AreEqual(0, v.Teaches.Count);
        }

        [Test]
        public void VolumenFueraDeRangoFalla()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                KnowledgeDefinition.BookVolume("libro", 6, ProfessionId.Herrero, 15));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                KnowledgeDefinition.BookVolume("libro", 0, ProfessionId.Herrero, 15));
        }

        [Test]
        public void SoloLosVolumenesTienenColeccion() =>
            Assert.Throws<ArgumentException>(() => new KnowledgeDefinition("p", KnowledgeType.Scroll,
                Rarity.Common, ProfessionId.Herrero, 1, new[] { "a" }, "libro", 1));

        [Test]
        public void IdVacioYNivelNoPositivoFallan()
        {
            Assert.Throws<ArgumentException>(() => Scroll(" ", "a"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Scroll("p", "a", 0));
        }
    }
}
