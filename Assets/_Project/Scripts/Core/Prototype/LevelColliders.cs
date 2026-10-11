using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Physics;
using Specimen.Core.Visuals;

namespace Specimen.Core.Prototype
{
    /// <summary>
    /// Genera los colliders de muro a partir del layout (PG-06), independientes del modelo
    /// visual: un bloque de 1 m de largo por muro, con grosor y altura configurables.
    /// Las puertas no generan collider.
    /// </summary>
    public sealed class LevelColliders
    {
        public const float SegmentLength = 1f;
        private const float WestYaw = GreyboxRoomLayout.WestYaw;

        public float Thickness { get; }
        public float Height { get; }

        public LevelColliders(float thickness = 0.2f, float height = 3f)
        {
            Thickness = Guard.Positive(thickness, nameof(thickness));
            Height = Guard.Positive(height, nameof(height));
        }

        public IReadOnlyList<ColliderBox> Build(IEnumerable<LevelPlacement> placements) =>
            Guard.NotNull(placements, nameof(placements))
                .Where(p => p.Id == VisualId.Muro)
                .Select(ToBox)
                .ToList();

        private ColliderBox ToBox(LevelPlacement wall)
        {
            var alongZ = System.Math.Abs(wall.YawDegrees - WestYaw) < 0.01f;
            var half = SegmentLength / 2f;
            var centerY = wall.Y + Height / 2f;
            return alongZ
                ? new ColliderBox(CollisionLayer.Muro, wall.X, centerY, wall.Z + half, Thickness, Height, SegmentLength)
                : new ColliderBox(CollisionLayer.Muro, wall.X + half, centerY, wall.Z, SegmentLength, Height, Thickness);
        }
    }
}
