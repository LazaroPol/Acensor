using System;
using Specimen.Core.Common;

namespace Specimen.Core.Items
{
    /// <summary>
    /// Plantilla inmutable de un objeto. <see cref="BaseDurability"/> = 0 indica que no se desgasta.
    /// Los objetos con durabilidad no se apilan (cada uno guarda su desgaste).
    /// </summary>
    public sealed class ItemDefinition
    {
        public string Id { get; }
        public string Name { get; }
        public ItemCategory Category { get; }
        public Rarity Rarity { get; }
        public int BaseDurability { get; }
        public int MaxStack { get; }
        /// <summary>Tier del material: 0 = sin tier (equipo, consumibles); 1..4 para materiales.</summary>
        public int Tier { get; }
        /// <summary>Peso por unidad en kg.</summary>
        public float Weight { get; }
        public MaterialTags Tags { get; }
        /// <summary>Clave del icono; la resuelve Runtime. Por defecto, el propio <see cref="Id"/>.</summary>
        public string IconKey { get; }

        public bool IsDegradable => BaseDurability > 0;
        public bool IsStackable => MaxStack > 1;
        public int MaxDurability => Rarity.ScaleDurability(BaseDurability);

        public const int MinTier = 0;
        public const int MaxTier = 4;

        /// <summary>True si tiene todas las etiquetas pedidas; <see cref="MaterialTags.None"/> nunca coincide.</summary>
        public bool HasTag(MaterialTags tag) => tag != MaterialTags.None && (Tags & tag) == tag;

        public ItemDefinition(string id, string name, ItemCategory category, Rarity rarity,
            int baseDurability = 0, int maxStack = 1, int tier = 0, float weight = 0f,
            MaterialTags tags = MaterialTags.None, string iconKey = null)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            Id = id;
            Name = Guard.NotNull(name, nameof(name));
            Category = category;
            Rarity = rarity;
            BaseDurability = Guard.NonNegative(baseDurability, nameof(baseDurability));
            MaxStack = Guard.Positive(maxStack, nameof(maxStack));
            if (tier < MinTier || tier > MaxTier)
                throw new ArgumentOutOfRangeException(nameof(tier), tier, $"El tier debe estar entre {MinTier} y {MaxTier}.");
            Tier = tier;
            Weight = Guard.NonNegative(weight, nameof(weight));
            Tags = tags;
            IconKey = string.IsNullOrWhiteSpace(iconKey) ? id : iconKey;
            if (IsDegradable && IsStackable)
                throw new ArgumentException("Un objeto con durabilidad no puede apilarse.", nameof(maxStack));
        }

        public override string ToString() => $"{Name} [{Rarity}]";
    }
}
