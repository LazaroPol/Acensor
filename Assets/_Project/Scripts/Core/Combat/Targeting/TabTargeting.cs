using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Combat.Targeting
{
    /// <summary>
    /// Selección de objetivo con Tab al estilo WoW: recorre los enemigos vivos al alcance del más cercano al más
    /// lejano; si el objetivo actual ya no está en la lista, empieza por el más cercano.
    /// </summary>
    public static class TabTargeting
    {
        public const float DefaultRange = 30f;

        /// <returns>Id del siguiente objetivo, o null si no hay ninguno al alcance.</returns>
        public static int? Next(IEnumerable<(int id, float distance)> candidates, int? current, float range = DefaultRange)
        {
            var ordered = Guard.NotNull(candidates, nameof(candidates))
                .Where(c => c.distance <= range).OrderBy(c => c.distance).Select(c => c.id).ToList();
            if (ordered.Count == 0) return null;
            int index = current.HasValue ? ordered.IndexOf(current.Value) : -1;
            return ordered[(index + 1) % ordered.Count];
        }
    }
}
