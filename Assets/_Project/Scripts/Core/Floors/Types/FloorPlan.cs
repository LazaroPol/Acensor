using Specimen.Core.Common;

namespace Specimen.Core.Floors.Types
{
    /// <summary>Piso planificado: su tipo y el trazado generado para él.</summary>
    public sealed class FloorPlan
    {
        public FloorPlan(IFloorType type, FloorLayout layout)
        {
            Type = Guard.NotNull(type, nameof(type));
            Layout = Guard.NotNull(layout, nameof(layout));
        }

        public IFloorType Type { get; }
        public FloorLayout Layout { get; }
        public ulong Seed => Layout.Seed;
    }
}
