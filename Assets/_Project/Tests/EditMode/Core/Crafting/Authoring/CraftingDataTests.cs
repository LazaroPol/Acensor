using System.Linq;
using NUnit.Framework;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Content;
using Specimen.Core.Crafting.Mods;
using Specimen.Core.Crafting.Stations;

namespace Specimen.Tests.Core.Crafting.Authoring
{
    public class CraftingDataTests
    {
        [Test]
        public void Los_datos_por_defecto_no_tienen_problemas()
        {
            var issues = CraftingDataValidator.Validate(DefaultCraftingData.Create());
            Assert.That(issues.Select(i => i.ToString()), Is.Empty);
        }

        [Test]
        public void El_catalogo_compilado_tiene_todo_el_contenido_por_defecto()
        {
            var catalog = new CraftingCatalog(DefaultCraftingData.Create());
            Assert.That(catalog.Recipes.Count, Is.EqualTo(DefaultRecipeCatalog.CreateAll().Count));
            Assert.That(catalog.Knowledge.Count, Is.EqualTo(DefaultKnowledge.All.Count));
            Assert.That(catalog.Parts.Count, Is.EqualTo(DefaultStationParts.All.Count));
            Assert.That(catalog.Mods.Count, Is.EqualTo(DefaultMods.All.Count));
            Assert.That(catalog.TryGetItem(DefaultStationParts.All.First().Id, out _), Is.True);
        }

        [Test]
        public void Cada_receta_compilada_equivale_a_la_del_catalogo_CSharp()
        {
            var catalog = new CraftingCatalog(DefaultCraftingData.Create());
            foreach (var original in DefaultRecipeCatalog.CreateAll())
            {
                var compiled = catalog.Recipes.Get(original.Id);
                Assert.That(compiled.ToString(), Is.EqualTo(original.ToString()), original.Id);
                Assert.That(compiled.Unlock.ToString(), Is.EqualTo(original.Unlock.ToString()), original.Id);
                Assert.That(compiled.Inputs.Select(i => i.ToString()), Is.EqualTo(original.Inputs.Select(i => i.ToString())));
            }
        }

        [Test]
        public void La_curva_y_la_calidad_del_catalogo_son_las_por_defecto()
        {
            var catalog = new CraftingCatalog(DefaultCraftingData.Create());
            Assert.That(catalog.Curve.MaxLevel, Is.EqualTo(Specimen.Core.Professions.ProfessionCurve.Default.MaxLevel));
            Assert.That(catalog.Quality.Grades.Count, Is.EqualTo(6));
        }
    }
}
