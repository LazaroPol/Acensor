using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Stats;

namespace Specimen.Core.Equipment
{
    /// <summary>Datos de equipo de un objeto: hueco, armadura, arma (si lo es) y bonos planos de atributos.</summary>
    public sealed class GearPiece
    {
        public GearPiece(string itemId, EquipSlot slot, float armor = 0f, WeaponSpec weapon = null,
            IReadOnlyDictionary<StatType, float> bonuses = null)
        {
            ItemId = Guard.NotNull(itemId, nameof(itemId));
            Slot = slot;
            Armor = Guard.NonNegative(armor, nameof(armor));
            Weapon = weapon;
            Bonuses = bonuses ?? new Dictionary<StatType, float>();
        }

        public string ItemId { get; }
        public EquipSlot Slot { get; }
        public float Armor { get; }
        public WeaponSpec Weapon { get; }
        public IReadOnlyDictionary<StatType, float> Bonuses { get; }

        public float BonusOf(StatType stat) => Bonuses.TryGetValue(stat, out float value) ? value : 0f;
    }
}
