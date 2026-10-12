using Specimen.Core.Crafting.Authoring;
using Specimen.Core.Crafting.Stations;
using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    [CreateAssetMenu(menuName = "SPECIMEN/Oficios/Pieza de estación", fileName = "Pieza")]
    public sealed class StationPartAsset : CraftingAsset<StationPartData>
    {
        public StationPartEntry ToEntry() => StationPartAssetMapper.ToEntry(Data);
    }
}
