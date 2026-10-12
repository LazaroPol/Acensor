using System;
using System.Linq;

namespace Specimen.Core.Crafting.Mods
{
    /// <summary>Valores de los <see cref="GearStat"/> de un objeto. Inmutable: <see cref="With"/> devuelve otra hoja.</summary>
    public sealed class GearStatSheet
    {
        private static readonly int Count = Enum.GetValues(typeof(GearStat)).Length;
        public static readonly GearStatSheet Empty = new GearStatSheet(new float[Count]);

        private readonly float[] _values;

        private GearStatSheet(float[] values)
        {
            _values = values;
        }

        public float this[GearStat stat] => _values[(int)stat];

        public GearStatSheet With(GearStat stat, float value)
        {
            var copy = (float[])_values.Clone();
            copy[(int)stat] = value;
            return new GearStatSheet(copy);
        }

        public override string ToString() =>
            string.Join(", ", Enum.GetValues(typeof(GearStat)).Cast<GearStat>().Select(s => $"{s}={this[s]:0.##}"));
    }
}
