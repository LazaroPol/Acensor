using System.Collections.Generic;

namespace Specimen.Core.Dna
{
    /// <summary>Efectos que concede cada nodo del árbol.</summary>
    public interface IDnaEffectCatalog
    {
        IReadOnlyList<DnaEffect> EffectsOf(DnaNode node);
    }
}
