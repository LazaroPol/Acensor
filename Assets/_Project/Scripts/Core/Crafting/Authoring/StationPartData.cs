using System;
using Specimen.Core.Crafting.Definitions;
using Specimen.Core.Crafting.Stations;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>Datos serializables de una pieza de estación (<see cref="StationPartEntry"/>).</summary>
    [Serializable]
    public sealed class StationPartData
    {
        public string id;
        public string name;
        public StationType station = StationType.Fragua;
        public int maxDurability = 100;
        public int qualityBonus;
        public float timeBonus;
        public float fuelSaving;
        public string enables;
        public float weight;
    }
}
