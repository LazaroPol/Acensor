using System;
using System.Collections.Generic;

namespace Specimen.Core.Professions
{
    /// <summary>Profesiones aprendidas por el personaje (como en WoW, como mucho dos a la vez).</summary>
    public sealed class LearnedProfessions
    {
        public const int Max = 2;

        private readonly List<ProfessionId> _known = new List<ProfessionId>();

        public event Action Changed;

        public IReadOnlyList<ProfessionId> All => _known;
        public bool IsFull => _known.Count >= Max;

        public bool Knows(ProfessionId id) => _known.Contains(id);

        public bool Learn(ProfessionId id)
        {
            if (Knows(id) || IsFull) return false;
            _known.Add(id);
            Changed?.Invoke();
            return true;
        }

        public bool Forget(ProfessionId id)
        {
            if (!_known.Remove(id)) return false;
            Changed?.Invoke();
            return true;
        }
    }
}
