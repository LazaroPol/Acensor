using System.Globalization;
using System.Text;
using Specimen.Core.Equipment;
using Specimen.Core.Items;
using Specimen.Core.Stats;
using Specimen.Core.Talents;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Líneas de equipo de los tooltips, al estilo WoW: hueco, tipo y velocidad del arma, daño, DPS, armadura y
    /// bonos; y la comparación con lo que llevas puesto (verde mejora, rojo empeora).
    /// </summary>
    public static partial class ItemView
    {
        private const string Up = "5AE65A";
        private const string Down = "F04A3A";

        public static string SlotName(EquipSlot slot) => slot switch
        {
            EquipSlot.Arma => "Arma",
            EquipSlot.Cabeza => "Cabeza",
            EquipSlot.Pecho => "Pecho",
            EquipSlot.Espalda => "Espalda",
            EquipSlot.Manos => "Manos",
            EquipSlot.Piernas => "Piernas",
            _ => "Pies"
        };

        public static string WeaponLines(WeaponSpec weapon, StatSheet stats)
        {
            float power = stats != null && stats.Has(StatType.Damage) ? stats.ValueOf(StatType.Damage) : 0f;
            string hands = weapon.IsRanged ? "A distancia" : weapon.IsTwoHanded ? "Dos manos" : "Una mano";
            return $"{hands} · {WeaponSpec.KindName(weapon.Kind)}      Velocidad {N(weapon.Speed, "0.00")}\n" +
                   $"{weapon.Min:0} - {weapon.Max:0} de daño\n({N(weapon.Dps, "0.0")} daño por segundo)\n" +
                   $"<color=#B0A890>Con tu poder de ataque: {weapon.MinHit(power):0} - {weapon.MaxHit(power):0} por golpe</color>";
        }

        private static void AppendGear(StringBuilder text, ItemDefinition item, CharacterEquipment compare, StatSheet stats)
        {
            if (!GearCatalog.TryGet(item.Id, out var piece)) return;
            text.Append($"\n{SlotName(piece.Slot)}");
            if (piece.Weapon != null) text.Append('\n').Append(WeaponLines(piece.Weapon, stats));
            if (piece.Armor > 0f) text.Append($"\n{piece.Armor:0} de armadura");
            foreach (var bonus in piece.Bonuses) text.Append($"\n<color=#{Up}>{Bonus(bonus.Key, bonus.Value)}</color>");
            if (compare == null) return;
            var equipped = compare.PieceIn(piece.Slot);
            if (equipped != null && equipped.ItemId == piece.ItemId) return;
            var delta = GearDelta.Between(piece, equipped);
            text.Append(equipped != null ? $"\n\n<color=#B0A890>Si lo cambias por lo que llevas en {SlotName(piece.Slot)}:</color>"
                : "\n\n<color=#B0A890>Hueco vacío: si te lo pones:</color>");
            if (delta.Armor != 0f) text.Append(Delta(delta.Armor, " de armadura", "0"));
            if (delta.Dps != 0f) text.Append(Delta(delta.Dps, " daño por segundo", "0.0"));
            foreach (var stat in delta.Stats)
                if (stat.Value != 0f) text.Append($"\n<color=#{(stat.Value > 0f ? Up : Down)}>{Bonus(stat.Key, stat.Value)}</color>");
            text.Append("\n<color=#B0A890>Clic derecho para equipar.</color>");
        }

        private static string Delta(float value, string label, string format) =>
            $"\n<color=#{(value > 0f ? Up : Down)}>{(value > 0f ? "+" : "−")}{N(System.Math.Abs(value), format)}{label}</color>";

        public static string Bonus(StatType stat, float value)
        {
            string sign = value >= 0f ? "+" : "−";
            float abs = System.Math.Abs(value);
            string amount = stat == StatType.CritChance ? N(abs * 100f, "0.#") + " %" : N(abs, "0.#");
            return $"{sign}{amount} de {TalentText.StatName(stat)}";
        }

        private static string N(float value, string format) =>
            value.ToString(format, CultureInfo.InvariantCulture).Replace('.', ',');
    }
}
