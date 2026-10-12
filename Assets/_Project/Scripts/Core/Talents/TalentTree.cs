using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Classes;
using Specimen.Core.Common;

namespace Specimen.Core.Talents
{
    /// <summary>Árbol de talentos de un kit: tres ramas independientes.</summary>
    public sealed class TalentTree
    {
        public TalentTree(ClassKitId kit, IEnumerable<TalentBranch> branches)
        {
            Kit = kit;
            Branches = Guard.NotNull(branches, nameof(branches)).ToList();
        }

        public ClassKitId Kit { get; }
        public IReadOnlyList<TalentBranch> Branches { get; }

        public bool TryFind(string talentId, out TalentDefinition talent, out int branch)
        {
            for (branch = 0; branch < Branches.Count; branch++)
            {
                talent = Branches[branch].Talents.FirstOrDefault(t => t.Id == talentId);
                if (talent != null) return true;
            }
            talent = null;
            branch = -1;
            return false;
        }
    }
}
