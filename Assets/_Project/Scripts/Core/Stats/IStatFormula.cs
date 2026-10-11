using System.Collections.Generic;

namespace Specimen.Core.Stats
{
    /// <summary>Estrategia que combina el valor base con los modificadores activos.</summary>
    public interface IStatFormula
    {
        float Evaluate(float baseValue, IReadOnlyList<StatModifier> modifiers);
    }
}
