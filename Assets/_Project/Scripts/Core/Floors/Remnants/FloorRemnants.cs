using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Floors.Remnants
{
    /// <summary>Restos de clones anteriores y Fragmentos de memoria sembrados en un piso.</summary>
    public sealed class FloorRemnants
    {
        public FloorRemnants(IReadOnlyList<Remnant> remnants, IReadOnlyList<FragmentPlacement> fragments)
        {
            Remnants = remnants;
            Fragments = fragments;
        }

        public IReadOnlyList<Remnant> Remnants { get; }
        public IReadOnlyList<FragmentPlacement> Fragments { get; }

        public IEnumerable<Remnant> RemnantsIn(int roomId) => Remnants.Where(r => r.RoomId == roomId);
        public IEnumerable<FragmentPlacement> FragmentsIn(int roomId) => Fragments.Where(f => f.RoomId == roomId);
    }
}
