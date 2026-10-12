using NUnit.Framework;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Knowledge;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.Definitions.DefinitionFixtures;
using static Specimen.Tests.Core.Crafting.Knowledge.KnowledgeFixtures;

namespace Specimen.Tests.Core.Crafting.Knowledge
{
    /// <summary>De extremo a extremo: una receta bloqueada queda disponible al leer su plano o tratado.</summary>
    [TestFixture]
    public class KnowledgeUnlockEndToEndTests
    {
        [Test]
        public void LaRecetaBloqueadaSeDesbloqueaAlLeerSuTratado()
        {
            var espada = Make("herrero.espada_acero", unlock: UnlockCondition.KnowledgeRequired("t.forja_acero"));
            var known = new KnownRecipes();
            var herrero = Skill(ProfessionId.Herrero, 10);
            Assert.IsFalse(espada.IsKnownBy(known));

            var r = KnowledgeReader.Read(Treatise("t.forja_acero", espada.Id), herrero, known);

            Assert.IsTrue(r.Consumed);
            Assert.IsTrue(espada.IsKnownBy(known));
        }

        [Test]
        public void ElTratadoHaceQueLaEstacionMuestreLaVarianteMejorada()
        {
            var basica = Make("herrero.espada");
            var mejor = Make("herrero.espada_fina", improves: basica.Id,
                unlock: UnlockCondition.KnowledgeRequired("t.espada_fina"));
            var catalog = new RecipeCatalog(new[] { basica, mejor });
            var known = new KnownRecipes();
            Assert.AreSame(basica, catalog.ResolveVariant(basica.Id, known));

            KnowledgeReader.Read(Treatise("t.espada_fina", mejor.Id), Skill(ProfessionId.Herrero, 10), known);

            Assert.AreSame(mejor, catalog.ResolveVariant(basica.Id, known));
        }

        [Test]
        public void LaRecetaMaestraPideLosCincoVolumenes()
        {
            var maestra = Make("herrero.maestra", unlock: UnlockCondition.CollectionComplete("libro_forjador"));
            var known = new KnownRecipes();
            var herrero = Skill(ProfessionId.Herrero, 15);
            for (var n = 1; n <= 5; n++)
            {
                Assert.IsFalse(maestra.IsKnownBy(known));
                KnowledgeReader.Read(KnowledgeDefinition.BookVolume("libro_forjador", n, ProfessionId.Herrero, 15),
                    herrero, known);
            }
            Assert.IsTrue(maestra.IsKnownBy(known));
        }
    }
}
