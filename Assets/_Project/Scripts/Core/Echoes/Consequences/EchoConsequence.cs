using System;
using Specimen.Core.Floors.Omens;

namespace Specimen.Core.Echoes.Consequences
{
    /// <summary>Si en un Eco se eligió cierta opción, un modificador afecta a los pisos desde <see cref="FromFloor"/>.</summary>
    public sealed class EchoConsequence
    {
        public EchoConsequence(string echoId, int option, int fromFloor, FloorModifierKind modifier)
        {
            if (string.IsNullOrWhiteSpace(echoId))
                throw new ArgumentException("El id del Eco no puede estar vacío.", nameof(echoId));
            if (option < 0) throw new ArgumentOutOfRangeException(nameof(option));
            if (fromFloor < 1) throw new ArgumentOutOfRangeException(nameof(fromFloor));
            EchoId = echoId;
            Option = option;
            FromFloor = fromFloor;
            Modifier = modifier;
        }

        public string EchoId { get; }
        public int Option { get; }
        public int FromFloor { get; }
        public FloorModifierKind Modifier { get; }

        public bool AppliesTo(EchoRegistry registry, int floor)
            => floor >= FromFloor && registry.Chose(EchoId, Option);
    }
}
