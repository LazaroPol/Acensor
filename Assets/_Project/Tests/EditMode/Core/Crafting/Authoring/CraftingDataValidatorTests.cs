using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Authoring;

namespace Specimen.Tests.Core.Crafting.Authoring
{
    public class CraftingDataValidatorTests
    {
        private static string[] Issues(CraftingDataSet data) =>
            CraftingDataValidator.Validate(data).Select(i => i.ToString()).ToArray();

        [Test]
        public void Detecta_ids_duplicados_entre_items_piezas_y_mods()
        {
            var data = DefaultCraftingData.Create();
            data.Mods.Add(new ModData { id = data.Items[0].id });
            Assert.That(Issues(data), Has.Some.Contains("id duplicado"));
        }

        [Test]
        public void Detecta_ids_vacios()
        {
            var data = DefaultCraftingData.Create();
            data.Recipes.Add(new RecipeData { id = " ", outputs = new[] { new OutputData { itemId = data.Items[0].id } } });
            Assert.That(Issues(data), Has.Some.Contains("id vacío"));
        }

        [Test]
        public void Detecta_recetas_con_items_o_piezas_inexistentes()
        {
            var data = DefaultCraftingData.Create();
            data.Recipes[0].inputs = new[] { new IngredientData { itemId = "no_existe" } };
            data.Recipes[0].requiredParts = new[] { "pieza.fantasma" };
            var issues = Issues(data);
            Assert.That(issues, Has.Some.Contains("ítem inexistente: no_existe"));
            Assert.That(issues, Has.Some.Contains("pieza inexistente: pieza.fantasma"));
        }

        [Test]
        public void Detecta_conocimientos_que_ensenan_recetas_inexistentes()
        {
            var data = DefaultCraftingData.Create();
            data.Knowledge[0].teaches = new[] { "receta.fantasma" };
            Assert.That(Issues(data), Has.Some.Contains("receta inexistente: receta.fantasma"));
        }

        [Test]
        public void Detecta_curvas_no_crecientes()
        {
            var data = DefaultCraftingData.Create();
            data.Curve.xpToNext = new[] { 40, 100, 100 };
            data.Curve.milestones = new[] { new MilestoneData { level = 2 } };
            var issues = Issues(data);
            Assert.That(issues, Has.Some.Contains("no creciente en el nivel 3"));
            Assert.That(issues, Has.Some.Contains("empezar en el nivel 1"));
        }

        [Test]
        public void Detecta_umbrales_de_calidad_no_crecientes()
        {
            var data = DefaultCraftingData.Create();
            data.Quality.grades[3].minScore = data.Quality.grades[2].minScore;
            Assert.That(Issues(data), Has.Some.Contains("umbral no creciente"));
        }
    }
}
