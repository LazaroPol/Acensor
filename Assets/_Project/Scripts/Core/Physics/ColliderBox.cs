using System;
using Specimen.Core.Common;

namespace Specimen.Core.Physics
{
    /// <summary>Caja de colisión alineada a ejes: centro y tamaño en metros, con su capa.</summary>
    public sealed class ColliderBox
    {
        public CollisionLayer Layer { get; }
        public float CenterX { get; }
        public float CenterY { get; }
        public float CenterZ { get; }
        public float SizeX { get; }
        public float SizeY { get; }
        public float SizeZ { get; }

        public ColliderBox(CollisionLayer layer, float centerX, float centerY, float centerZ,
            float sizeX, float sizeY, float sizeZ)
        {
            Layer = layer;
            CenterX = centerX;
            CenterY = centerY;
            CenterZ = centerZ;
            SizeX = Guard.Positive(sizeX, nameof(sizeX));
            SizeY = Guard.Positive(sizeY, nameof(sizeY));
            SizeZ = Guard.Positive(sizeZ, nameof(sizeZ));
        }

        /// <summary>True si el punto del plano (x, z) cae dentro de la huella de la caja.</summary>
        public bool ContainsPlanar(float x, float z) =>
            Math.Abs(x - CenterX) <= SizeX / 2f && Math.Abs(z - CenterZ) <= SizeZ / 2f;
    }
}
