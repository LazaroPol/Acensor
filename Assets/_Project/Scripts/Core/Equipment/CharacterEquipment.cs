using System;
using System.Collections.Generic;
using Specimen.Core.Common;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;

namespace Specimen.Core.Equipment
{
    /// <summary>Piezas equipadas por hueco. Equipar devuelve lo que ocupaba el hueco (para devolverlo a la bolsa).</summary>
    public sealed class CharacterEquipment
    {
        private readonly Dictionary<EquipSlot, ItemDefinition> _items = new Dictionary<EquipSlot, ItemDefinition>();

        public event Action Changed;

        public IEnumerable<KeyValuePair<EquipSlot, ItemDefinition>> Items => _items;

        public ItemDefinition In(EquipSlot slot) => _items.TryGetValue(slot, out var item) ? item : null;

        public GearPiece PieceIn(EquipSlot slot) =>
            _items.TryGetValue(slot, out var item) && GearCatalog.TryGet(item.Id, out var piece) ? piece : null;

        public WeaponSpec Weapon => PieceIn(EquipSlot.Arma)?.Weapon ?? WeaponSpec.Unarmed;

        public float TotalArmor
        {
            get
            {
                float armor = 0f;
                foreach (var item in _items.Values)
                    if (GearCatalog.TryGet(item.Id, out var piece)) armor += piece.Armor;
                return armor;
            }
        }

        public bool CanEquip(ItemDefinition item) => item != null && GearCatalog.IsGear(item.Id);

        /// <summary>Equipa <paramref name="item"/> y devuelve la pieza que había en su hueco (o null).</summary>
        public ItemDefinition Equip(ItemDefinition item)
        {
            Guard.NotNull(item, nameof(item));
            if (!GearCatalog.TryGet(item.Id, out var piece))
                throw new ArgumentException($"{item.Name} no es equipable.", nameof(item));
            var previous = In(piece.Slot);
            _items[piece.Slot] = item;
            Changed?.Invoke();
            return previous;
        }

        public ItemDefinition Unequip(EquipSlot slot)
        {
            var previous = In(slot);
            if (previous == null) return null;
            _items.Remove(slot);
            Changed?.Invoke();
            return previous;
        }

        public static CharacterEquipment WithItems(IEnumerable<string> itemIds)
        {
            var equipment = new CharacterEquipment();
            foreach (string id in itemIds) equipment.Equip(ItemCatalog.Get(id));
            return equipment;
        }
    }
}
