using Specimen.Core.Common;

namespace Specimen.Core.Floors.Types
{
    /// <summary>Base común de las estrategias de piso; por defecto genera por anillos.</summary>
    public abstract class FloorType : IFloorType
    {
        protected FloorType(FloorTypeKind kind, string displayName, string challenge,
            FloorObjective objective, bool hasCombat, RingLayoutConfig layout)
        {
            Kind = kind;
            DisplayName = Guard.NotNull(displayName, nameof(displayName));
            Challenge = Guard.NotNull(challenge, nameof(challenge));
            Objective = objective;
            HasCombat = hasCombat;
            Layout = Guard.NotNull(layout, nameof(layout));
        }

        public FloorTypeKind Kind { get; }
        public string DisplayName { get; }
        public string Challenge { get; }
        public FloorObjective Objective { get; }
        public bool HasCombat { get; }
        public RingLayoutConfig Layout { get; }

        public virtual IFloorGenerator CreateGenerator() => new RingCorridorGenerator(Layout);

        protected static RingLayoutConfig Rings(float lateral, float back, params int[] rooms) =>
            new RingLayoutConfig(rooms, lateral, back);
    }
}
