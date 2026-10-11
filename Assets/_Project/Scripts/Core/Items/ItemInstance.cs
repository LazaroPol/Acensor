using Specimen.Core.Common;

namespace Specimen.Core.Items
{
    /// <summary>
    /// Objeto concreto en el mundo o en un inventario: su plantilla más su propio desgaste.
    /// </summary>
    public sealed class ItemInstance
    {
        public ItemDefinition Definition { get; }
        public IDurability Durability { get; }

        public string Id => Definition.Id;
        public Rarity Rarity => Definition.Rarity;

        /// <summary>Un objeto roto existe (puede repararse) pero no se puede usar.</summary>
        public bool IsUsable => !Durability.IsBroken;

        public ItemInstance(ItemDefinition definition, IDurability durability)
        {
            Definition = Guard.NotNull(definition, nameof(definition));
            Durability = Guard.NotNull(durability, nameof(durability));
        }

        /// <summary>Crea un objeto nuevo con la durabilidad máxima que da su rareza.</summary>
        public static ItemInstance CreateNew(ItemDefinition definition)
        {
            Guard.NotNull(definition, nameof(definition));
            return new ItemInstance(definition, new Durability(definition.MaxDurability));
        }

        public override string ToString() => $"{Definition} {Durability.Current}/{Durability.Max}";
    }
}
