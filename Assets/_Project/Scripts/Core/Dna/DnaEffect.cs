using Specimen.Core.Stats;

namespace Specimen.Core.Dna
{
    /// <summary>Efecto de una mutación: un modificador (porcentual) sobre un atributo.</summary>
    public readonly struct DnaEffect
    {
        public StatType Stat { get; }
        public float Percent { get; }

        public DnaEffect(StatType stat, float percent)
        {
            Stat = stat;
            Percent = percent;
        }
    }
}
