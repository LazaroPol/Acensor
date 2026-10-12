using System;

namespace Specimen.Core.Dna
{
    /// <summary>Nodo inmutable del árbol: rama, nivel (tier) y coste en puntos de ADN.</summary>
    public sealed class DnaNode
    {
        public string Id { get; }
        public DnaBranch Branch { get; }
        public int Tier { get; }
        public int Cost { get; }

        public DnaNode(string id, DnaBranch branch, int tier, int cost)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id obligatorio.", nameof(id));
            if (tier < 1) throw new ArgumentOutOfRangeException(nameof(tier), tier, "Tier >= 1.");
            if (cost < 1) throw new ArgumentOutOfRangeException(nameof(cost), cost, "Coste >= 1.");
            Id = id;
            Branch = branch;
            Tier = tier;
            Cost = cost;
        }
    }
}
