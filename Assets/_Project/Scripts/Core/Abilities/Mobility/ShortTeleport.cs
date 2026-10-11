using Specimen.Core.Common;
using Specimen.Core.Movement;

namespace Specimen.Core.Abilities.Mobility
{
    /// <summary>Calcula el destino de un teletransporte corto recortando el desplazamiento al alcance máximo.</summary>
    public static class ShortTeleport
    {
        public static PlanarVector Destination(PlanarVector origin, PlanarVector target, float range)
        {
            Guard.Positive(range, nameof(range));
            var offset = new PlanarVector(target.X - origin.X, target.Z - origin.Z).ClampMagnitude(range);
            return new PlanarVector(origin.X + offset.X, origin.Z + offset.Z);
        }
    }
}
