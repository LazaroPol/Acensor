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

        public bool IsDegradable => BaseDurability > 0;
        public bool IsStackable => MaxStack > 1;
        public int MaxDurability => Rarity.ScaleDurability(BaseDurability);

        public ItemDefinition(string id, string name, ItemCategory category, Rarity rarity,
            int baseDurability = 0, int maxStack = 1)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            Id = id;
            Name = Guard.NotNull(name, nameof(name));
            Category = category;
            Rarity = rarity;
            BaseDurability = Guard.NonNegative(baseDurability, nameof(baseDurability));
            MaxStack = Guard.Positive(maxStack, nameof(maxStack));
            if (IsDegradable && IsStackable)
                throw new ArgumentException("Un objeto con durabilidad no puede apilarse.", nameof(maxStack));
        }

        public override string ToString() => $"{Name} [{Rarity}]";
    }
}
