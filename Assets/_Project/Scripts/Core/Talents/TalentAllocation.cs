using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Common;

namespace Specimen.Core.Talents
{
    /// <summary>
    /// Puntos gastados en un árbol. Reglas estilo WoW clásico: un punto por nivel, cada fila exige 5 puntos
    /// más en su rama, cada talento tiene rangos máximos; se puede reiniciar (respec) gratis.
    /// </summary>
    public sealed class TalentAllocation
    {
        private readonly Dictionary<string, int> _ranks = new Dictionary<string, int>();

        public TalentAllocation(TalentTree tree) => Tree = Guard.NotNull(tree, nameof(tree));

        public TalentTree Tree { get; }
        public int TotalSpent => _ranks.Values.Sum();

        public int RankOf(string talentId) => _ranks.TryGetValue(talentId ?? string.Empty, out var r) ? r : 0;

        public int PointsIn(int branch)
            => Tree.Branches[branch].Talents.Sum(t => RankOf(t.Id));

        public bool CanSpend(string talentId, int availablePoints)
        {
            if (TotalSpent >= availablePoints) return false;
            if (!Tree.TryFind(talentId, out var talent, out int branch)) return false;
            return RankOf(talentId) < talent.MaxRank && PointsIn(branch) >= talent.RequiredBranchPoints;
        }

        public bool TrySpend(string talentId, int availablePoints)
        {
            if (!CanSpend(talentId, availablePoints)) return false;
            _ranks[talentId] = RankOf(talentId) + 1;
            return true;
        }

        public void Reset() => _ranks.Clear();

        public IEnumerable<(TalentDefinition talent, int rank)> Ranked()
        {
            foreach (var branch in Tree.Branches)
                foreach (var talent in branch.Talents)
                    if (RankOf(talent.Id) > 0) yield return (talent, RankOf(talent.Id));
        }
    }
}
