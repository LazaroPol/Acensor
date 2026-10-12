using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Mods;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Mod", fileName = "Mod")]
    public sealed class ModDefinitionAsset : CraftingAsset<ModData>
    {
        public ModDefinition ToDefinition() => ModAssetMapper.ToDefinition(Data);
    }
}
