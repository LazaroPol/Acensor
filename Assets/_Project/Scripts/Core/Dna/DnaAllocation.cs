using System;
using System.Collections.Generic;

namespace Specimen.Core.Dna
{
    /// <summary>Estado de un perfil: puntos libres, nodos desbloqueados y puntos invertidos por rama.</summary>
    public sealed class DnaAllocation
    {
        private readonly DnaTree _tree;
        private readonly ITierLock _tierLock;
        private readonly HashSet<string> _unlocked = new HashSet<string>();
        private readonly Dictionary<DnaBranch, int> _invested = new Dictionary<DnaBranch, int>();

        public DnaAllocation(DnaTree tree, ITierLock tierLock)
        {
            _tree = tree ?? throw new ArgumentNullException(nameof(tree));
            _tierLock = tierLock ?? throw new ArgumentNullException(nameof(tierLock));
        }

        public int FreePoints { get; private set; }

        public IReadOnlyCollection<string> Unlocked => _unlocked;

        public int Invested(DnaBranch branch) => _invested.TryGetValue(branch, out var v) ? v : 0;

        public bool IsUnlocked(string id) => id != null && _unlocked.Contains(id);

        public void AddPoints(int amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), amount, "Debe ser >= 0.");
            FreePoints += amount;
        }

        public UnlockResult TryUnlock(string id)
        {
            if (!_tree.TryGet(id, out var node)) return UnlockResult.UnknownNode;
            if (_unlocked.Contains(id)) return UnlockResult.AlreadyUnlocked;
            if (Invested(node.Branch) < _tierLock.RequiredPoints(node.Tier)) return UnlockResult.TierLocked;
            if (FreePoints < node.Cost) return UnlockResult.NotEnoughPoints;
            FreePoints -= node.Cost;
            _unlocked.Add(id);
            _invested[node.Branch] = Invested(node.Branch) + node.Cost;
            return UnlockResult.Ok;
        }
    }
}
