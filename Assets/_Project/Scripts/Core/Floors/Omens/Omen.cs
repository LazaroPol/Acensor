using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Floors.Types;

namespace Specimen.Core.Floors.Omens
{
    /// <summary>Presagio de un piso: anuncia su tipo y los modificadores que lo afectan.</summary>
    public sealed class Omen
    {
        public Omen(IFloorType floorType, IReadOnlyList<FloorModifier> modifiers)
        {
            FloorType = Guard.NotNull(floorType, nameof(floorType));
            Modifiers = Guard.NotNull(modifiers, nameof(modifiers)).ToList();
        }

        public IFloorType FloorType { get; }
        public IReadOnlyList<FloorModifier> Modifiers { get; }

        public bool Has(FloorModifierKind kind) => Modifiers.Any(m => m.Kind == kind);

        /// <summary>Texto que leen los jugadores en la Antesala.</summary>
        public string Inscription
        {
            get
            {
                var head = $"Presagio: {FloorType.DisplayName}.";
                if (Modifiers.Count == 0) return head + " El piso guarda silencio.";
                var hints = string.Join("; ", Modifiers.Select(m => m.Hint));
                var names = string.Join(", ", Modifiers.Select(m => m.DisplayName));
                return $"{head} Se dice que {hints}. ({names})";
            }
        }

        public override string ToString() => Inscription;
    }
}
