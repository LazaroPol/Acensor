using System;

namespace Specimen.Core.Floors.Remnants
{
    /// <summary>Probabilidad por sala de un anillo de tener un resto y de esconder un Fragmento de memoria.</summary>
    public sealed class RingRemnantOdds
    {
        public RingRemnantOdds(float remnantChance, float fragmentChance)
        {
            RemnantChance = Probability(remnantChance, nameof(remnantChance));
            FragmentChance = Probability(fragmentChance, nameof(fragmentChance));
        }

        public float RemnantChance { get; }
        public float FragmentChance { get; }

        private static float Probability(float value, string name)
        {
            if (float.IsNaN(value) || value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException(name, value, "La probabilidad debe estar en [0, 1].");
            return value;
        }
    }
}
