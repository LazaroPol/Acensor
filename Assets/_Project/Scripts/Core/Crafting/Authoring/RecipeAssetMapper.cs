using System;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Convierte <see cref="RecipeData"/> ⇄ <see cref="RecipeDefinition"/>; los ítems se resuelven por id.</summary>
    public static class RecipeAssetMapper
    {
        public static RecipeDefinition ToDefinition(RecipeData data, Func<string, ItemDefinition> items)
        {
            Guard.NotNull(data, nameof(data));
            Guard.NotNull(items, nameof(items));
            var inputs = (data.inputs ?? new IngredientData[0]).Select(i => ToSpec(i, items));
            var outputs = (data.outputs ?? new OutputData[0]).Select(o => new RecipeOutput(items(o.itemId), o.quantity));
            return new RecipeDefinition(data.id, data.profession, data.station, inputs.ToList(), outputs.ToList(),
                data.minStationLevel, data.requiredParts, data.minProfessionLevel, data.craftSeconds, data.xp,
                UnlockAssetMapper.ToCondition(data.unlockKnowledge, data.unlockCollections), data.qualityCap,
                string.IsNullOrWhiteSpace(data.improves) ? null : data.improves);
        }

        public static RecipeData FromDefinition(RecipeDefinition recipe)
        {
            Guard.NotNull(recipe, nameof(recipe));
            var (knowledge, collections) = UnlockAssetMapper.Flatten(recipe.Unlock);
            return new RecipeData
            {
                id = recipe.Id, profession = recipe.Profession, station = recipe.Station,
                inputs = recipe.Inputs.Select(FromSpec).ToArray(),
                outputs = recipe.Outputs.Select(o => new OutputData { itemId = o.Item.Id, quantity = o.Quantity }).ToArray(),
                minStationLevel = recipe.MinStationLevel, requiredParts = recipe.RequiredParts.ToArray(),
                minProfessionLevel = recipe.MinProfessionLevel, craftSeconds = recipe.CraftSeconds, xp = recipe.Xp,
                unlockKnowledge = knowledge, unlockCollections = collections,
                qualityCap = recipe.QualityCap, improves = recipe.Improves
            };
        }

        private static IngredientSpec ToSpec(IngredientData data, Func<string, ItemDefinition> items) =>
            string.IsNullOrWhiteSpace(data.itemId)
                ? IngredientSpec.ForTag(data.tag, data.quantity, data.minTier)
                : IngredientSpec.ForItem(items(data.itemId), data.quantity);

        private static IngredientData FromSpec(IngredientSpec spec) => spec.IsByTag
            ? new IngredientData { tag = spec.Tag, quantity = spec.Quantity, minTier = spec.MinTier }
            : new IngredientData { itemId = spec.Item.Id, quantity = spec.Quantity };
    }
}
