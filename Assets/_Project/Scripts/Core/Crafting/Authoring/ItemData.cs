using System;
using Specimen.Core.Items;

namespace Specimen.Core.Crafting.Authoring
{
    /// <summary>
    /// Datos serializables de un <see cref="ItemDefinition"/> (campos públicos: Unity los guarda en el
    /// ScriptableObject sin conocer el núcleo). El mapper los convierte a la definición del núcleo.
    /// </summary>
    [Serializable]
    public sealed class ItemData
    {
        public string id;
        public string name;
        public ItemCategory category;
        public Rarity rarity;
        public int baseDurability;
        public int maxStack = 1;
        public int tier;
        public float weight;
        public MaterialTags tags;
        public string iconKey;
    }
}
