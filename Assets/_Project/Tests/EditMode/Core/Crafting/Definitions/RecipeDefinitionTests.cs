using System;
using NUnit.Framework;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using Specimen.Core.Professions;
using static Specimen.Tests.Core.Crafting.CraftingFixtures;

namespace Specimen.Tests.Core.Crafting.Definitions
{
    [TestFixture]
    public class RecipeDefinitionTests
    {
        private static readonly IngredientSpec[] In = { IngredientSpec.ForTag(MaterialTags.Metal, 2, 1) };
        private static readonly RecipeOutput[] Out = { new RecipeOutput(Sword), new RecipeOutput(Coal, 2) };

        [Test]
        public void GuardaTodosLosCampos()
        {
            var r = new RecipeDefinition("herrero.espada_acero", ProfessionId.Herrero, StationType.Fragua, In, Out,
                minStationLevel: 2, requiredParts: new[] { "pieza.crisol" }, minProfessionLevel: 10,
                craftSeconds: 45f, xp: 30, unlock: UnlockCondition.KnowledgeRequired("plano"),
                qualityCap: Quality.Good, improves: "herrero.espada");
            Assert.AreEqual(2, r.MinStationLevel);
            CollectionAssert.AreEqual(new[] { "pieza.crisol" }, r.RequiredParts);
            Assert.AreEqual(10, r.MinProfessionLevel);
            Assert.AreEqual(45f, r.CraftSeconds);
            Assert.AreEqual(30, r.Xp);
            Assert.AreEqual(Quality.Good, r.QualityCap);
            Assert.AreSame(Sword, r.MainOutput.Item);
            Assert.IsTrue(r.IsVariant);
            Assert.IsFalse(r.IsInstant);
            Assert.IsFalse(r.IsKnownBy(new FakeKnowledge()));
        }

        [Test]
        public void ValoresPorDefectoSonNivel1InstantaneaYSiempreConocida()
        {
            var r = new RecipeDefinition("x", ProfessionId.Sastre, StationType.TallerSastre, In, Out);
            Assert.AreEqual(1, r.MinStationLevel);
            Assert.AreEqual(1, r.MinProfessionLevel);
            Assert.IsTrue(r.IsInstant);
            Assert.IsEmpty(r.RequiredParts);
            Assert.AreEqual(Quality.Legendary, r.QualityCap);
            Assert.IsTrue(r.IsKnownBy(new FakeKnowledge()));
        }

        [Test]
        public void DatosInvalidosFallanPronto()
        {
            RecipeDefinition M(IngredientSpec[] i = null, RecipeOutput[] o = null, float s = 0f, int lvl = 1,
                string imp = null, string[] parts = null) =>
                new RecipeDefinition("x", ProfessionId.Herrero, StationType.Fragua, i ?? In, o ?? Out,
                    minStationLevel: lvl, craftSeconds: s, improves: imp, requiredParts: parts);
            Assert.Throws<ArgumentException>(() => M(i: new IngredientSpec[0]));
            Assert.Throws<ArgumentException>(() => M(o: new RecipeOutput[0]));
            Assert.Throws<ArgumentOutOfRangeException>(() => M(s: -1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => M(lvl: 4));
            Assert.Throws<ArgumentException>(() => M(imp: "x"));
            Assert.Throws<ArgumentException>(() => M(parts: new[] { "a", "a" }));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new RecipeDefinition("x", ProfessionId.Herrero, (StationType)0, In, Out));
        }

        [Test]
        public void FromLegacyConvierteEnEstacionPrincipalNivel1EInstantanea()
        {
            var r = RecipeDefinition.FromLegacy(SwordRecipe());
            Assert.AreEqual("forjar-espada", r.Id);
            Assert.AreEqual(StationType.Fragua, r.Station);
            Assert.AreEqual(1, r.MinStationLevel);
            Assert.IsTrue(r.IsInstant);
            Assert.AreSame(UnlockCondition.AlwaysKnown, r.Unlock);
            Assert.AreEqual(2, r.Inputs.Count);
            Assert.AreSame(Ore, r.Inputs[0].Item);
            Assert.AreEqual(3, r.Inputs[0].Quantity);
            Assert.AreSame(Sword, r.MainOutput.Item);
        }
    }
}
