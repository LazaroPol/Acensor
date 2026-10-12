using System.Globalization;
using Specimen.Core.Stats;

namespace Specimen.Core.Talents
{
    /// <summary>Texto del tooltip de un talento para un rango dado.</summary>
    public static class TalentText
    {
        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        public static string Describe(TalentDefinition talent, int rank)
        {
            float total = talent.PerRank * rank;
            switch (talent.Kind)
            {
                case TalentEffectKind.StatPorcentaje: return $"+{Pct(total)} de {StatName(talent.Stat)}.";
                case TalentEffectKind.StatPlano: return $"+{Num(total)} de {StatName(talent.Stat)}.";
                case TalentEffectKind.PoderHechizos: return $"Tus hechizos y técnicas hacen +{Pct(total)} de daño y curación.";
                default: return $"Enfriamiento de tus hechizos −{Pct(total)}.";
            }
        }

        public static string StatName(StatType stat)
        {
            switch (stat)
            {
                case StatType.MaxHealth: return "vida máxima";
                case StatType.MaxStamina: return "aguante";
                case StatType.MaxClassResource: return "recurso de clase";
                case StatType.MoveSpeed: return "velocidad";
                case StatType.Armor: return "armadura";
                case StatType.AttackSpeed: return "velocidad de ataque";
                case StatType.CritChance: return "probabilidad de crítico";
                default: return "daño";
            }
        }

        private static string Pct(float fraction) => (fraction * 100f).ToString("0.#", Inv).Replace('.', ',') + " %";

        private static string Num(float value) => value.ToString("0.#", Inv).Replace('.', ',');
    }
}
