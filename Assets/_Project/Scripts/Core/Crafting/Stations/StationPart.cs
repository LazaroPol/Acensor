using Specimen.Core.Common;

namespace Specimen.Core.Crafting.Stations
{
    /// <summary>Pieza física colocable: se gasta 1 punto por uso; rota deja de contar hasta repararla.</summary>
    public sealed class StationPart
    {
        public StationPartDefinition Definition { get; }
        public int Durability { get; private set; }

        public StationPart(StationPartDefinition definition)
        {
            Definition = Guard.NotNull(definition, nameof(definition));
            Durability = definition.MaxDurability;
        }

        public string Id => Definition.Id;
        public bool IsBroken => Durability == 0;

        /// <summary>Gasta un punto de durabilidad (no baja de 0).</summary>
        public void Wear()
        {
            if (Durability > 0) Durability--;
        }

        public void Repair() => Durability = Definition.MaxDurability;

        public override string ToString() => $"{Id} {Durability}/{Definition.MaxDurability}";
    }
}
