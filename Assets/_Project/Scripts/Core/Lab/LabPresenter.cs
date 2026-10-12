using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;
using Specimen.Core.Dna;

namespace Specimen.Core.Lab
{
    /// <summary>
    /// Modelo de vista de la pantalla del Laboratorio Genético: las cinco ramas del árbol de ADN con el estado
    /// de cada nodo, la selección y el desbloqueo. C# puro; la capa Runtime solo lo dibuja y reenvía clics.
    /// </summary>
    public sealed class LabPresenter
    {
        private readonly DnaTree _tree;
        private readonly DnaAllocation _allocation;
        private readonly ITierLock _tierLock;

        public LabPresenter(DnaTree tree, DnaAllocation allocation, ITierLock tierLock)
        {
            _tree = Guard.NotNull(tree, nameof(tree));
            _allocation = Guard.NotNull(allocation, nameof(allocation));
            _tierLock = Guard.NotNull(tierLock, nameof(tierLock));
        }

        public string SelectedId { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public int FreePoints => _allocation.FreePoints;

        public void Select(string id) => SelectedId = _tree.TryGet(id, out _) ? id : null;

        /// <summary>Intenta desbloquear el nodo seleccionado y deja el resultado en <see cref="Message"/>.</summary>
        public UnlockResult UnlockSelected()
        {
            var result = _allocation.TryUnlock(SelectedId);
            Message = SelectedId == null ? "Elige una mutación." : LabMessages.Of(result);
            return result;
        }

        public IReadOnlyList<LabBranchView> Branches() =>
            ((DnaBranch[])System.Enum.GetValues(typeof(DnaBranch)))
                .Select(b => new LabBranchView(b, _allocation.Invested(b), NodesOf(b)))
                .ToList();

        /// <summary>Puntos que faltan invertir en la rama para abrir el tier del nodo seleccionado (0 si ya está abierto).</summary>
        public int PointsMissingForSelected()
        {
            if (!_tree.TryGet(SelectedId, out var node)) return 0;
            return System.Math.Max(0, _tierLock.RequiredPoints(node.Tier) - _allocation.Invested(node.Branch));
        }

        private IReadOnlyList<LabNodeView> NodesOf(DnaBranch branch) =>
            _tree.Nodes.Where(n => n.Branch == branch).OrderBy(n => n.Tier).ThenBy(n => n.Id, System.StringComparer.Ordinal)
                .Select(n => new LabNodeView(n.Id, n.Tier, n.Cost, StateOf(n), n.Id == SelectedId)).ToList();

        private NodeState StateOf(DnaNode node)
        {
            if (_allocation.IsUnlocked(node.Id)) return NodeState.Unlocked;
            if (_allocation.Invested(node.Branch) < _tierLock.RequiredPoints(node.Tier)) return NodeState.TierLocked;
            return _allocation.FreePoints >= node.Cost ? NodeState.Available : NodeState.TooExpensive;
        }
    }
}
