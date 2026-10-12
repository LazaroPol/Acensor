using Specimen.Core.Common;

namespace Specimen.Core.Loot
{
    /// <summary>Loot que el equipo ha subido al Ascensor en esta iteración; solo cuenta si llega al Osprey.</summary>
    public sealed class RunHaul
    {
        public int Items { get; private set; }

        public void Add(int items) => Items += Guard.NonNegative(items, nameof(items));

        public void Clear() => Items = 0;
    }
}
