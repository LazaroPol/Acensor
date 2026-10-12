using System.Collections.Generic;
using Specimen.Core.Equipment;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Huecos de equipo de la hoja de personaje: icono con color de rareza, nombre y tooltip con stats.</summary>
    public sealed partial class CharacterWindow
    {
        private const float Row = 62f;

        private static readonly EquipSlot[] Order =
        {
            EquipSlot.Cabeza, EquipSlot.Pecho, EquipSlot.Espalda, EquipSlot.Manos, EquipSlot.Piernas, EquipSlot.Pies,
            EquipSlot.Arma
        };

        private readonly List<(EquipSlot slot, UiSlot view, Text name)> _slots = new List<(EquipSlot, UiSlot, Text)>();

        private void BuildSlots(Vector2 origin)
        {
            for (int i = 0; i < Order.Length; i++)
            {
                var slot = Order[i];
                var position = origin + new Vector2(0f, i * Row);
                var view = UiSlot.Create(Body, position, Row - 8f);
                view.Hover.Text = () => SlotTooltip(slot);
                view.Hover.RightClick = () =>
                {
                    Gear.Unequip(Context, Player, slot);
                    Refresh();
                };
                var name = UiFactory.Label(Body, string.Empty, position + new Vector2(Row, 4f), new Vector2(256f, Row - 12f), 14);
                _slots.Add((slot, view, name));
            }
        }

        private void RefreshSlots()
        {
            foreach (var (slot, view, name) in _slots)
            {
                var item = Player.Gear.In(slot);
                if (item == null)
                {
                    view.Clear();
                    name.text = $"<color=#7A7468>{ItemView.SlotName(slot)} — vacío</color>";
                    continue;
                }
                ItemView.Show(view, item, 0);
                name.text = $"<b>{ItemView.Colored(item)}</b>\n<color=#B0A890>{ItemView.SlotName(slot)}{Summary(slot)}</color>";
            }
        }

        private string Summary(EquipSlot slot)
        {
            var piece = Player.Gear.PieceIn(slot);
            if (piece == null) return string.Empty;
            if (piece.Weapon != null)
                return $" · {piece.Weapon.Min:0}-{piece.Weapon.Max:0} · vel. {piece.Weapon.Speed:0.00}".Replace('.', ',');
            return piece.Armor > 0f ? $" · {piece.Armor:0} armadura" : string.Empty;
        }

        private string SlotTooltip(EquipSlot slot)
        {
            var item = Player.Gear.In(slot);
            if (item == null) return $"<b>{ItemView.SlotName(slot)}</b>\nVacío. Equipa piezas con clic derecho desde la bolsa.";
            return ItemView.Tooltip(item, null, 1, null, Player.Stats) + "\n<color=#B0A890>Clic derecho para quitártelo.</color>";
        }
    }
}
