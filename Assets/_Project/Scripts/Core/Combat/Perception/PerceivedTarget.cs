using System;
using Specimen.Core.Movement;

namespace Specimen.Core.Combat.Perception
{
    /// <summary>Lo que un enemigo puede percibir de un jugador: posición, ruido que hace (0-1) y si está Atrapado.</summary>
    public sealed class PerceivedTarget
    {
        public PerceivedTarget(string id, PlanarVector position, float noise, bool isTrapped)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("El id no puede estar vacío.", nameof(id));
            if (noise < 0f || noise > 1f) throw new ArgumentOutOfRangeException(nameof(noise), noise, "Debe estar en [0, 1].");
            Id = id;
            Position = position;
            Noise = noise;
            IsTrapped = isTrapped;
        }

        public string Id { get; }
        public PlanarVector Position { get; }
        public float Noise { get; }
        public bool IsTrapped { get; }
    }
}
