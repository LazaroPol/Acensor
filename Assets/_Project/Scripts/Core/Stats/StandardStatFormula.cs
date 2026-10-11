using System.Collections.Generic;

namespace Specimen.Core.Stats
{
    /// <summary>
    /// (base + Σ Flat) × (1 + Σ PercentAdd) × Π Multiplier, sin bajar de 0.
    /// </summary>
    public sealed class StandardStatFormula : IStatFormula
    {
        public static readonly StandardStatFormula Instance = new StandardStatFormula();

        public float Evaluate(float baseValue, IReadOnlyList<StatModifier> modifiers)
        {
            float flat = 0f, percent = 0f, multiplier = 1f;
            for (int i = 0; i < modifiers.Count; i++)
            {
                var m = modifiers[i];
                switch (m.Kind)
                {
                    case ModifierKind.Flat: flat += m.Value; break;
                    case ModifierKind.PercentAdd: percent += m.Value; break;
                    case ModifierKind.Multiplier: multiplier *= m.Value; break;
                }
            }
            float result = (baseValue + flat) * (1f + percent) * multiplier;
            return result > 0f ? result : 0f;
        }
    }
}
