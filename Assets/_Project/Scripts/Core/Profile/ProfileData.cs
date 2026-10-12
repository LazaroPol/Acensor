using System.Collections.Generic;

namespace Specimen.Core.Profile
{
    /// <summary>Estado persistente del perfil entre iteraciones: puntos de ADN, mutaciones y fragmentos de memoria.</summary>
    public sealed class ProfileData
    {
        public int FreeDnaPoints { get; }
        public IReadOnlyList<string> UnlockedNodes { get; }
        public IReadOnlyList<string> Fragments { get; }

        public ProfileData(int freeDnaPoints, IReadOnlyList<string> unlockedNodes, IReadOnlyList<string> fragments)
        {
            FreeDnaPoints = freeDnaPoints;
            UnlockedNodes = unlockedNodes ?? new string[0];
            Fragments = fragments ?? new string[0];
        }
    }
}
