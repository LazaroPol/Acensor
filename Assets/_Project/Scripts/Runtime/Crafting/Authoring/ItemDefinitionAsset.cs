using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Items;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    /// <summary>Objeto o material editable; el icono es el sprite que usará la UI (su clave, <c>iconKey</c>).</summary>
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Ítem", fileName = "Item")]
    public sealed class ItemDefinitionAsset : CraftingAsset<ItemData>
    {
        [SerializeField] private Sprite icon = null;
        public Sprite Icon => icon;
        public ItemDefinition ToDefinition() => ItemAssetMapper.ToDefinition(Data);
    }
}
