using System.Collections.Generic;
using Specimen.Core.Stats;

namespace Specimen.Core.Talents
{
    /// <summary>Atajos para escribir árboles: cada rama son 5 talentos, uno por fila (tier 0 a 4).</summary>
    public static class TalentBuilder
    {
        public static TalentBranch Branch(string prefix, string name, string motto, params TalentDefinition[] talents)
        {
            var list = new List<TalentDefinition>();
            for (int i = 0; i < talents.Length; i++)
            {
                var t = talents[i];
                list.Add(new TalentDefinition($"{prefix}.{i}", t.Name, i, t.MaxRank, t.Kind, t.PerRank, t.Stat));
            }
            return new TalentBranch(name, motto, list);
        }

        public static TalentDefinition Pct(string name, StatType stat, float perRank, int ranks = 5)
            => new TalentDefinition("_", name, 0, ranks, TalentEffectKind.StatPorcentaje, perRank, stat);

        public static TalentDefinition Flat(string name, StatType stat, float perRank, int ranks = 5)
            => new TalentDefinition("_", name, 0, ranks, TalentEffectKind.StatPlano, perRank, stat);

        public static TalentDefinition Power(string name, float perRank, int ranks = 5)
            => new TalentDefinition("_", name, 0, ranks, TalentEffectKind.PoderHechizos, perRank);

        public static TalentDefinition Haste(string name, float perRank, int ranks = 3)
            => new TalentDefinition("_", name, 0, ranks, TalentEffectKind.Enfriamiento, perRank);
    }
}
