using System;

namespace Specimen.Core.Physics
{
    /// <summary>Matriz simétrica que decide qué pares de capas colisionan (o se detectan).</summary>
    public sealed class CollisionMatrix
    {
        private static readonly int Size = Enum.GetValues(typeof(CollisionLayer)).Length;
        private readonly bool[,] _pairs = new bool[Size, Size];

        public CollisionMatrix Enable(CollisionLayer a, CollisionLayer b) => Set(a, b, true);

        public CollisionMatrix Disable(CollisionLayer a, CollisionLayer b) => Set(a, b, false);

        public bool Collides(CollisionLayer a, CollisionLayer b) => _pairs[Index(a), Index(b)];

        private CollisionMatrix Set(CollisionLayer a, CollisionLayer b, bool value)
        {
            _pairs[Index(a), Index(b)] = value;
            _pairs[Index(b), Index(a)] = value;
            return this;
        }

        private static int Index(CollisionLayer layer)
        {
            var index = (int)layer;
            if (index < 0 || index >= Size)
                throw new ArgumentOutOfRangeException(nameof(layer), layer, "Capa desconocida.");
            return index;
        }
    }
}
