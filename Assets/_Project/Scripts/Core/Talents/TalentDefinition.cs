using System;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Talents
{
    /// <summary>Talento de un árbol: fila (tier 0-4), rangos máximos y efecto por rango.</summary>
    public sealed class TalentDefinition
    {
        public const int Tiers = 5;

        public TalentDefinition(string id, string name, int tier, int maxRank, TalentEffectKind kind, float perRank,
            StatType stat = StatType.Damage)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Id vacío.", nameof(id));
            if (tier < 0 || tier >= Tiers) throw new ArgumentOutOfRangeException(nameof(tier));
            Id = id;
            Name = Guard.NotNull(name, nameof(name));
            Tier = tier;
            MaxRank = Guard.Positive(maxRank, nameof(maxRank));
            Kind = kind;
            PerRank = perRank;
            Stat = stat;
        }

        public string Id { get; }
        public string Name { get; }
        public int Tier { get; }
        public int MaxRank { get; }
        public TalentEffectKind Kind { get; }
        public float PerRank { get; }
        public StatType Stat { get; }

        /// <summary>Puntos que hay que llevar gastados en la rama para desbloquear su fila (5 por fila, como en WoW).</summary>
        public int RequiredBranchPoints => Tier * 5;

        public string Describe(int rank) => TalentText.Describe(this, Math.Max(1, rank));
    }
}
