using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Content
{
    /// <summary>
    /// Una receta por cada sinergia de <see cref="DefaultProfessionSynergies"/> (GDD §7 y §17.6), con los
    /// materiales del recetario nuevo. Ids estables «sinergia.*».
    /// </summary>
    public static class DefaultSynergyRecipes
    {
        private static readonly (string synergy, string slug, string output, string inputs)[] Data =
        {
            ("Festín alquímico", "festin_alquimico", "festin_alquimico", "carne*3 especias_sur pocion-vida"),
            ("Armas mecánicas", "ballesta_repeticion", "ballesta_repeticion",
                "lingote_acero*2 engranaje_madera*3 mecanismo_apertura"),
            ("Telas ignífugas", "capa_ignifuga", "capa_ignifuga", "pano_encerado*3 resina_ignea*2"),
            ("Gólem", "golem_piedra", "golem_piedra", "bloque_granito*6 mecanismo_apertura*2 nucleo"),
            ("Hackear torretas", "llave_torretas", "llave_torretas", "chatarra*3 engranaje_madera*2"),
            ("Estatuas explosivas", "estatua_explosiva", "estatua_explosiva", "piedra_labrada*3 azufre*2 salitre"),
            ("Armadura mixta", "armadura_mixta", "armadura_mixta", "lingote-hierro*3 forro_acolchado*2 remaches*4")
        };

        public static SynergyRecipeBook CreateBook() => new SynergyRecipeBook(Create());

        public static IReadOnlyList<SynergyRecipe> Create()
        {
            var synergies = DefaultProfessionSynergies.Create().ToDictionary(s => s.Recipe);
            return Data.Select(d => new SynergyRecipe("sinergia." + d.slug, synergies[d.synergy],
                d.inputs.Split(' ').Select(Ingredient), ContentItems.Get(d.output))).ToList().AsReadOnly();
        }

        private static RecipeIngredient Ingredient(string token)
        {
            var spec = RecipeLadder.ParseInput(token);
            return new RecipeIngredient(spec.Item, spec.Quantity);
        }
    }
}
