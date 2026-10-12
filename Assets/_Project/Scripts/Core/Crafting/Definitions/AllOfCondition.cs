using System.Collections.Generic;
using System.Linq;

namespace Specimen.Core.Crafting.Definitions
{
    /// <summary>Combinación «Y»: se cumple si se cumplen todas las condiciones.</summary>
    public sealed class AllOfCondition : UnlockCondition
    {
        public IReadOnlyList<UnlockCondition> Conditions { get; }

        internal AllOfCondition(IEnumerable<UnlockCondition> conditions) =>
            Conditions = RequireAll(conditions);

        public override bool IsMet(IRecipeKnowledge knowledge) => Conditions.All(c => c.IsMet(knowledge));

        public override string ToString() => string.Join(" y ", Conditions);
    }
}
