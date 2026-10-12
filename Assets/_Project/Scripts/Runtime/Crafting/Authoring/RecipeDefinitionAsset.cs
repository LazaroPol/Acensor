using System;
using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Items;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    /// <summary>Receta editable; entradas y salidas van por id de ítem (las resuelve la base de datos).</summary>
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Receta", fileName = "Receta")]
    public sealed class RecipeDefinitionAsset : CraftingAsset<RecipeData>
    {
        public RecipeDefinition ToDefinition(Func<string, ItemDefinition> items) =>
            RecipeAssetMapper.ToDefinition(Data, items);
    }
}
