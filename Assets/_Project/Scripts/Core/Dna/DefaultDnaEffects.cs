using System.Collections.Generic;
using Specimen.Core.Stats;

namespace Specimen.Core.Dna
{
    /// <summary>
    /// Efectos por defecto: cada rama tiene un atributo principal (nodo «a») y uno secundario (nodo «b»);
    /// el bonus crece con el tier (+2 % × tier). Cognición mejora el recurso de clase.
    /// </summary>
    public sealed class DefaultDnaEffects : IDnaEffectCatalog
    {
        private const float PercentPerTier = 0.02f;

        private static readonly Dictionary<DnaBranch, StatType[]> Stats = new Dictionary<DnaBranch, StatType[]>
        {
            { DnaBranch.Resistencia, new[] { StatType.Armor, StatType.MaxHealth } },
            { DnaBranch.Agilidad, new[] { StatType.MoveSpeed, StatType.AttackSpeed } },
            { DnaBranch.Vitalidad, new[] { StatType.MaxHealth, StatType.MaxStamina } },
            { DnaBranch.Potencia, new[] { StatType.Damage, StatType.CritChance } },
            { DnaBranch.Cognicion, new[] { StatType.MaxClassResource, StatType.MaxStamina } }
        };

        public IReadOnlyList<DnaEffect> EffectsOf(DnaNode node)
        {
            var secondary = node.Id.EndsWith("-b") ? 1 : 0;
            return new[] { new DnaEffect(Stats[node.Branch][secondary], PercentPerTier * node.Tier) };
        }
    }
}
