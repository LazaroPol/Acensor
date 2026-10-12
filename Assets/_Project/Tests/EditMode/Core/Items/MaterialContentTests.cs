using System;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Tests.Core.Items
{
    /// <summary>Integridad de la batería de materiales de §17.5 (OF-11).</summary>
    [TestFixture]
    public class MaterialContentTests
    {
        private static ItemDefinition[] Materials =>
            ItemCatalog.All.Where(i => i.Category == ItemCategory.Material).ToArray();

        [Test]
        public void HasAtLeast45MaterialsWithUniqueIds()
        {
            Assert.That(Materials.Length, Is.GreaterThanOrEqualTo(45));
            Assert.That(Materials.Select(m => m.Id).Distinct().Count(), Is.EqualTo(Materials.Length));
        }

        [Test]
        public void IdsAreSnakeCaseOrLegacyKebab()
        {
            var format = new Regex("^[a-z]+([_-][a-z]+)*$");
            foreach (var m in Materials) Assert.That(format.IsMatch(m.Id), Is.True, m.Id);
        }

        [TestCase("rama_seca"), TestCase("madera_corazon_negro"), TestCase("acero_crisol"), TestCase("obsidiana")]
        [TestCase("seda"), TestCase("mandragora"), TestCase("trufa_negra"), TestCase("hueso_bestia_mayor")]
        [TestCase("polvo_estrella"), TestCase("vidrio_cuarzo"), TestCase("cuerda"), TestCase("engranaje_madera")]
        [TestCase(FuelIds.Lena), TestCase(FuelIds.CarbonVegetal), TestCase(FuelIds.Hulla), TestCase(FuelIds.AceiteLampara)]
        public void StableIdsResolve(string id) => Assert.That(ItemCatalog.TryGet(id, out _), Is.True, id);

        [Test]
        public void EveryTagHasAtLeastOneMaterial()
        {
            foreach (MaterialTags tag in Enum.GetValues(typeof(MaterialTags)))
            {
                if (tag == MaterialTags.None) continue;
                Assert.That(Materials.Any(m => m.HasTag(tag)), Is.True, tag.ToString());
            }
        }

        [Test]
        public void EveryTierHasAtLeastOneMaterial()
        {
            for (var tier = 1; tier <= ItemDefinition.MaxTier; tier++)
                Assert.That(Materials.Count(m => m.Tier == tier), Is.GreaterThan(0), $"tier {tier}");
        }

        [Test]
        public void TableTagsCoverEveryTier()
        {
            var tags = new[] { MaterialTags.Madera, MaterialTags.Metal, MaterialTags.Piedra, MaterialTags.Cuero,
                MaterialTags.Tela, MaterialTags.Hierba, MaterialTags.Comida, MaterialTags.Hueso, MaterialTags.Mineral };
            foreach (var tag in tags)
                for (var tier = 1; tier <= ItemDefinition.MaxTier; tier++)
                    Assert.That(Materials.Any(m => m.HasTag(tag) && m.Tier == tier), Is.True, $"{tag} T{tier}");
        }

        [Test]
        public void RarityGrowsWithTier()
        {
            Assert.That(ItemCatalog.Get("rama_seca").Rarity, Is.EqualTo(Rarity.Common));
            Assert.That(ItemCatalog.Get("lingote_acero").Rarity, Is.EqualTo(Rarity.Rare));
            Assert.That(ItemCatalog.Get("acero_crisol").Rarity, Is.EqualTo(Rarity.Epic));
        }
    }
}
