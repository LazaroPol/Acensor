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

        /// <summary>Calidad del objeto; None para materiales y objetos sin grado.</summary>
        public Quality Quality { get; }

        public string Id => Definition.Id;
        public Rarity Rarity => Definition.Rarity;

        /// <summary>Un objeto roto existe (puede repararse) pero no se puede usar.</summary>
        public bool IsUsable => !Durability.IsBroken;

        public ItemInstance(ItemDefinition definition, IDurability durability, Quality quality = Quality.None)
        {
            Definition = Guard.NotNull(definition, nameof(definition));
            Durability = Guard.NotNull(durability, nameof(durability));
            Quality = quality;
        }

        /// <summary>Crea un objeto nuevo con la durabilidad máxima que da su rareza.</summary>
        public static ItemInstance CreateNew(ItemDefinition definition)
        {
            Guard.NotNull(definition, nameof(definition));
            return new ItemInstance(definition, new Durability(definition.MaxDurability));
        }

        /// <summary>Crea un objeto con calidad: la durabilidad máxima se multiplica según su grado.</summary>
        public static ItemInstance CreateNew(ItemDefinition definition, QualityGrade grade)
        {
            Guard.NotNull(definition, nameof(definition));
            Guard.NotNull(grade, nameof(grade));
            var max = grade.ScaleDurability(definition.MaxDurability);
            return new ItemInstance(definition, new Durability(max), grade.Quality);
        }

        public override string ToString() => $"{Definition} {Durability.Current}/{Durability.Max}";
    }
}
