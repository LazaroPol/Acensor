using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Content
{
    /// <summary>
    /// Constructor compacto de la escalera de un oficio. Entradas en texto: «id*2», «#Madera*3@2»
    /// (etiqueta, cantidad, tier mínimo). Desbloqueo: id de conocimiento, o de libro si empieza por «libro.».
    /// </summary>
    internal sealed class RecipeLadder
    {
        public const string BookPrefix = "libro.";

        private readonly ProfessionId _profession;
        private readonly List<RecipeDefinition> _sink;

        public RecipeLadder(ProfessionId profession, List<RecipeDefinition> sink)
        {
            _profession = profession;
            _sink = sink;
        }

        public RecipeLadder T1(string slug, string output, string inputs) => Add(1, slug, output, inputs);

        public RecipeLadder Add(int tier, string slug, string output, string inputs,
            string part = null, string unlock = null)
        {
            var parts = part == null ? null : new[] { DefaultStationParts.Prefix + part };
            _sink.Add(new RecipeDefinition($"{_profession.ToString().ToLowerInvariant()}.{slug}", _profession,
                StationTypes.MainOf(_profession), inputs.Split(' ').Select(ParseInput), new[] { ParseOutput(output) },
                RecipeTiers.StationLevel(tier, part != null), parts, RecipeTiers.ProfessionLevel(tier),
                RecipeTiers.CraftSeconds(tier), RecipeTiers.Xp(tier), ParseUnlock(unlock)));
            return this;
        }

        internal static UnlockCondition ParseUnlock(string unlock) =>
            unlock == null ? UnlockCondition.AlwaysKnown
            : unlock.StartsWith(BookPrefix, StringComparison.Ordinal) ? UnlockCondition.CollectionComplete(unlock)
            : UnlockCondition.KnowledgeRequired(unlock);

        internal static IngredientSpec ParseInput(string token)
        {
            var (name, qty, tier) = Split(token);
            if (!name.StartsWith("#", StringComparison.Ordinal))
                return IngredientSpec.ForItem(ContentItems.Get(name), qty);
            var tag = (MaterialTags)Enum.Parse(typeof(MaterialTags), name.Substring(1));
            return IngredientSpec.ForTag(tag, qty, tier);
        }

        private static RecipeOutput ParseOutput(string token)
        {
            var (name, qty, _) = Split(token);
            return new RecipeOutput(ContentItems.Get(name), qty);
        }

        private static (string name, int qty, int tier) Split(string token)
        {
            var at = token.Split('@');
            var star = at[0].Split('*');
            return (star[0], star.Length > 1 ? int.Parse(star[1], CultureInfo.InvariantCulture) : 1, at.Length > 1 ? int.Parse(at[1], CultureInfo.InvariantCulture) : 0);
        }
    }
}
