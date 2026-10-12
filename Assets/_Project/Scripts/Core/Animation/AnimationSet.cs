using System;
using System.Collections.Generic;

namespace Specimen.Core.Animation
{
    /// <summary>
    /// Conjunto de animación: para cada ranura, nombres de clip candidatos en orden de preferencia
    /// (el generador usa el primero que exista en los packs importados).
    /// </summary>
    public sealed class AnimationSet
    {
        private readonly Dictionary<AnimSlot, string[]> _clips = new Dictionary<AnimSlot, string[]>();

        public AnimationSet(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nombre vacío.", nameof(name));
            Name = name;
        }

        public string Name { get; }

        public AnimationSet With(AnimSlot slot, params string[] candidates)
        {
            if (candidates == null || candidates.Length == 0)
                throw new ArgumentException("Hace falta al menos un clip candidato.", nameof(candidates));
            _clips[slot] = candidates;
            return this;
        }

        public bool Has(AnimSlot slot) => _clips.ContainsKey(slot);

        public IReadOnlyList<string> CandidatesFor(AnimSlot slot)
            => _clips.TryGetValue(slot, out var clips) ? clips : Array.Empty<string>();
    }
}
