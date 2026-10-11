using Specimen.Core.Items;

namespace Specimen.Core.Loot
{
    /// <summary>Estrategia que ajusta el peso de una rareza según el anillo de profundidad.</summary>
    public interface IRingWeighting
    {
        /// <summary>Multiplicador (≥ 0) del peso base de un objeto de esa rareza en ese anillo.</summary>
        float Multiplier(Rarity rarity, int ring);
    }
}
