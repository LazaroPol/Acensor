using System;
using Specimen.Core.Loot;

namespace Specimen.Core.Combat.Enemies
{
    /// <summary>Dónde aparece un enemigo: número de piso (>= 1) y anillo de profundidad (1-3).</summary>
    public sealed class EnemyDepth
    {
        public EnemyDepth(int floor, int ring)
        {
            if (floor < 1) throw new ArgumentOutOfRangeException(nameof(floor), floor, "El piso empieza en 1.");
            Floor = floor;
            Ring = DepthRings.Validate(ring, nameof(ring));
        }

        public int Floor { get; }
        public int Ring { get; }

        /// <summary>Pisos por encima del primero (0 en el piso 1).</summary>
        public int FloorSteps => Floor - 1;

        /// <summary>Anillos por debajo del más cercano (0 en el anillo 1).</summary>
        public int RingSteps => Ring - DepthRings.Min;
    }
}
