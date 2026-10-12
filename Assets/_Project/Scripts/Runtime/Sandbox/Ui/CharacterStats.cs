using System.Text;
using Specimen.Core.Combat;
using Specimen.Core.Equipment;
using Specimen.Core.Stats;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Texto de estadísticas de la hoja de personaje: generales, cuerpo a cuerpo (arma) y defensa (armadura).</summary>
    public static class CharacterStats
    {
        public static string Describe(SandboxPlayer player, int level)
        {
            var stats = player.Stats;
            var weapon = player.Gear.Weapon;
            float power = Value(stats, StatType.Damage);
            float armor = Value(stats, StatType.Armor);
            float avg = (weapon.MinHit(power) + weapon.MaxHit(power)) / 2f;
            var text = new StringBuilder();
            text.Append(Head("General"));
            text.Append(Line("Vida máxima", $"{Value(stats, StatType.MaxHealth):0}"));
            text.Append(Line("Recurso de clase", $"{Value(stats, StatType.MaxClassResource):0}"));
            text.Append(Line("Aguante", $"{Value(stats, StatType.MaxStamina):0}"));
            text.Append(Line("Velocidad", $"{Value(stats, StatType.MoveSpeed):0.0}"));
            text.Append(Head("Combate"));
            text.Append(Line("Poder de ataque", $"{power:0}"));
            text.Append(Line($"Arma ({WeaponSpec.KindName(weapon.Kind)})", $"{weapon.MinHit(power):0} - {weapon.MaxHit(power):0}"));
            text.Append(Line("Velocidad del arma", $"{weapon.Speed:0.00}".Replace('.', ',')));
            text.Append(Line("Daño por segundo", $"{avg / weapon.Speed:0.0}".Replace('.', ',')));
            text.Append(Line("Crítico", $"{Value(stats, StatType.CritChance) * 100f:0.#} % (×2)"));
            text.Append(Head("Defensa"));
            text.Append(Line("Armadura", $"{armor:0}"));
            text.Append(Line($"Reducción física (atacante nv. {level})", Pct(ArmorRules.Reduction(armor, level))));
            text.Append(Line($"Reducción física (atacante nv. {level + 3})", Pct(ArmorRules.Reduction(armor, level + 3))));
            text.Append(Head("Hechizos"));
            text.Append(Line("Poder de hechizos", $"+{player.Bonuses.SpellPower * 100f:0} %"));
            text.Append(Line("Reducción de enfriamientos", $"−{player.Bonuses.CooldownReduction * 100f:0} %"));
            return text.ToString();
        }

        private static float Value(StatSheet stats, StatType type) => stats.Has(type) ? stats.ValueOf(type) : 0f;

        private static string Head(string title) => $"<color=#DDAD52><b>{title}</b></color>\n";

        private static string Line(string label, string value) => $"   {label}: <color=#FFFFFF><b>{value}</b></color>\n";

        private static string Pct(float fraction) => $"{fraction * 100f:0.0} %".Replace('.', ',');
    }
}
