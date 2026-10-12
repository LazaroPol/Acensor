using System.Collections.Generic;

namespace Specimen.Core.Animation
{
    /// <summary>Lista de clips candidatos que se convierte sola en el array que espera <see cref="AnimationSet.With"/>.</summary>
    public sealed class Candidates
    {
        private readonly List<string> _names = new List<string>();

        public Candidates(params string[] names) => _names.AddRange(names);

        public Candidates Add(string name)
        {
            _names.Add(name);
            return this;
        }

        public static implicit operator string[](Candidates candidates) => candidates._names.ToArray();
    }
}
