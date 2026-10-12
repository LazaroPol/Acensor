using System.Collections.Generic;
using Specimen.Core.Dna;

namespace Specimen.Core.Lab
{
    public enum NodeState
    {
        Unlocked,
        Available,
        TooExpensive,
        TierLocked
    }

    /// <summary>Nodo del árbol tal como lo ve el jugador en el Laboratorio.</summary>
    public readonly struct LabNodeView
    {
        public LabNodeView(string id, int tier, int cost, NodeState state, bool selected)
        {
            Id = id;
            Tier = tier;
            Cost = cost;
            State = state;
            IsSelected = selected;
        }

        public string Id { get; }
        public int Tier { get; }
        public int Cost { get; }
        public NodeState State { get; }
        public bool IsSelected { get; }
    }

    /// <summary>Columna de una rama: puntos invertidos y sus nodos ordenados por tier.</summary>
    public readonly struct LabBranchView
    {
        public LabBranchView(DnaBranch branch, int invested, IReadOnlyList<LabNodeView> nodes)
        {
            Branch = branch;
            Invested = invested;
            Nodes = nodes;
        }

        public DnaBranch Branch { get; }
        public int Invested { get; }
        public IReadOnlyList<LabNodeView> Nodes { get; }
    }
}
