using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Loot;

namespace Specimen.Core.Floors
{
    /// <summary>Parámetros del generador por anillos: salas por anillo y pasillos extra.</summary>
    public sealed class RingLayoutConfig
    {
        public RingLayoutConfig(IReadOnlyList<int> roomsPerRing, float lateralChance, float backLinkChance)
        {
            Guard.NotNull(roomsPerRing, nameof(roomsPerRing));
            DepthRings.Validate(roomsPerRing.Count, nameof(roomsPerRing));
            foreach (var n in roomsPerRing) Guard.Positive(n, nameof(roomsPerRing));
            LateralChance = Probability(lateralChance, nameof(lateralChance));
            BackLinkChance = Probability(backLinkChance, nameof(backLinkChance));
            RoomsPerRing = roomsPerRing;
        }

        /// <summary>Salas en cada anillo; índice 0 = anillo 1.</summary>
        public IReadOnlyList<int> RoomsPerRing { get; }
        /// <summary>Probabilidad de unir dos salas vecinas del mismo anillo.</summary>
        public float LateralChance { get; }
        /// <summary>Probabilidad de que una sala tenga un segundo pasillo al anillo anterior.</summary>
        public float BackLinkChance { get; }
        public int RingCount => RoomsPerRing.Count;

        public static RingLayoutConfig Default => new RingLayoutConfig(new[] { 3, 4, 5 }, 0.5f, 0.25f);

        private static float Probability(float p, string name)
        {
            if (p < 0f || p > 1f) throw new ArgumentOutOfRangeException(name, p, "Debe estar en [0, 1].");
            return p;
        }
    }
}
