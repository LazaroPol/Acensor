using System.Collections.Generic;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Content
{
    /// <summary>
    /// Recetario por defecto de §17.6: escalera T1–T4 de los 7 oficios. Integra las recetas antiguas de
    /// <see cref="DefaultRecipes"/> con sus mismos ids y salidas. Cada parcial añade un oficio.
    /// </summary>
    public static partial class DefaultRecipeCatalog
    {
        public static RecipeCatalog Create() => new RecipeCatalog(CreateAll());

        public static IReadOnlyList<RecipeDefinition> CreateAll()
        {
            var list = new List<RecipeDefinition>();
            AddHerrero(new RecipeLadder(ProfessionId.Herrero, list));
            AddSastre(new RecipeLadder(ProfessionId.Sastre, list));
            AddCocinero(new RecipeLadder(ProfessionId.Cocinero, list));
            AddQuimico(new RecipeLadder(ProfessionId.Quimico, list));
            AddIngeniero(new RecipeLadder(ProfessionId.Ingeniero, list));
            AddManitas(new RecipeLadder(ProfessionId.Manitas, list));
            AddEscultor(new RecipeLadder(ProfessionId.Escultor, list));
            return list.AsReadOnly();
        }
    }
}
