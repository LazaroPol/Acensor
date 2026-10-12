using System;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using Specimen.Core.Professions;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Datos serializables de un <see cref="RecipeDefinition"/>; los ítems van por id.</summary>
    [Serializable]
    public sealed class RecipeData
    {
        public string id;
        public ProfessionId profession;
        public StationType station = StationType.Fragua;
        public IngredientData[] inputs = new IngredientData[0];
        public OutputData[] outputs = new OutputData[0];
        public int minStationLevel = RecipeDefinition.MinStationLevelValue;
        public string[] requiredParts = new string[0];
        public int minProfessionLevel = 1;
        public float craftSeconds;
        public int xp;
        /// <summary>Conocimientos exigidos (todos). Vacío junto a <see cref="unlockCollections"/> = siempre conocida.</summary>
        public string[] unlockKnowledge = new string[0];
        public string[] unlockCollections = new string[0];
        public Quality qualityCap = Quality.Legendary;
        public string improves;
    }

    /// <summary>Entrada de receta: por ítem (<see cref="itemId"/>) o, si está vacío, por etiqueta.</summary>
    [Serializable]
    public sealed class IngredientData
    {
        public string itemId;
        public MaterialTags tag;
        public int quantity = 1;
        public int minTier;
    }

    [Serializable]
    public sealed class OutputData
    {
        public string itemId;
        public int quantity = 1;
    }
}
