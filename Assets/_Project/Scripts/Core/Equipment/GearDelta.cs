using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Equipment
{
    /// <summary>
    /// Diferencia entre una pieza y la equipada en su hueco (lo que WoW muestra en verde/rojo al comparar):
    /// armadura, daño por segundo del arma y bonos de atributos.
    /// </summary>
    public sealed class GearDelta
    {
        private GearDelta(float armor, float dps, Dictionary<StatType, float> stats)
        {
            Armor = armor;
            Dps = dps;
            Stats = stats;
        }

        public float Armor { get; }
        public float Dps { get; }
        public IReadOnlyDictionary<StatType, float> Stats { get; }
        public bool IsUpgrade => Armor + Dps * 10f + Sum() > 0f;

        public static GearDelta Between(GearPiece candidate, GearPiece equipped)
        {
            Guard.NotNull(candidate, nameof(candidate));
            float armor = candidate.Armor - (equipped?.Armor ?? 0f);
            float dps = (candidate.Weapon?.Dps ?? 0f) - (equipped?.Weapon?.Dps ?? 0f);
            var stats = new Dictionary<StatType, float>();
            foreach (var pair in candidate.Bonuses) stats[pair.Key] = pair.Value;
            if (equipped != null)
                foreach (var pair in equipped.Bonuses)
                    stats[pair.Key] = (stats.TryGetValue(pair.Key, out float v) ? v : 0f) - pair.Value;
            return new GearDelta(armor, dps, stats);
        }

        private float Sum()
        {
            float total = 0f;
            foreach (var pair in Stats) total += pair.Key == StatType.CritChance ? pair.Value * 100f : pair.Value;
            return total;
        }
    }
}
