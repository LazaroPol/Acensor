using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting.Stations;
using Specimen.Core.Items;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Columna derecha: ranuras de entrada, salida y combustible, y piezas (colocar / quitar).</summary>
    public sealed partial class StationWindow
    {
        private const float SlotSize = 36f;
        private const int PartRows = PartSlots.DefaultCapacity;

        private readonly List<UiSlot> _input = new List<UiSlot>();
        private readonly List<UiSlot> _output = new List<UiSlot>();
        private readonly List<UiSlot> _fuel = new List<UiSlot>();
        private readonly List<(Button button, Text label)> _parts = new List<(Button, Text)>();
        private Button _addFuel;
        private Text _addFuelLabel;
        private Text _fuelTitle;

        private void BuildSlots(Vector2 origin)
        {
            Row(origin, "Entrada", Workstation.InputSlots, _input);
            Row(origin + new Vector2(0f, 64f), "Salida (clic: recoger)", Workstation.OutputSlots, _output);
            foreach (var slot in _output) slot.Hover.LeftClick = Collect;
            _fuelTitle = Row(origin + new Vector2(0f, 128f), "Combustible", Workstation.FuelSlots, _fuel);
            _addFuel = UiFactory.Button(Body, string.Empty, origin + new Vector2(84f, 152f), new Vector2(152f, 30f), AddFuel, 13);
            _addFuelLabel = _addFuel.GetComponentInChildren<Text>();
            UiFactory.Label(Body, "<b>Piezas</b>", origin + new Vector2(0f, 196f), new Vector2(236f, 20f), 14,
                TextAnchor.UpperLeft, UiTheme.Gold);
            for (int i = 0; i < PartRows * 2; i++)
            {
                int index = i;
                var button = UiFactory.Button(Body, string.Empty, origin + new Vector2(0f, 220f + i * 30f),
                    new Vector2(236f, 27f), () => TogglePart(index), 12);
                _parts.Add((button, button.GetComponentInChildren<Text>()));
            }
        }

        private Text Row(Vector2 origin, string title, int count, List<UiSlot> slots)
        {
            var label = UiFactory.Label(Body, $"<b>{title}</b>", origin, new Vector2(236f, 20f), 13, TextAnchor.UpperLeft, UiTheme.Gold);
            for (int i = 0; i < count; i++)
                slots.Add(UiSlot.Create(Body, origin + new Vector2(i * (SlotSize + 3f), 22f), SlotSize));
            return label;
        }

        private void RefreshSlots()
        {
            var station = Session.Station;
            Fill(_input, station.Input);
            Fill(_output, station.Output);
            bool fuel = station.HasFuelSlots;
            _fuelTitle.text = fuel ? "<b>Combustible</b>" : "<b>Sin combustible</b> <color=#B0A890>(no lo necesita)</color>";
            _fuel.ForEach(s => s.gameObject.SetActive(fuel));
            if (fuel) Fill(_fuel, station.Fuel);
            var next = Session.FuelsInBag().FirstOrDefault();
            _addFuel.gameObject.SetActive(fuel);
            _addFuel.interactable = next != null;
            _addFuelLabel.text = next != null ? $"Echar {next.Name}" : "Sin combustible en la bolsa";
            var placed = station.Parts.Parts;
            var inBag = Session.PartsInBag();
            for (int i = 0; i < _parts.Count; i++)
            {
                var (button, label) = _parts[i];
                bool isPlaced = i < PartRows;
                var entry = isPlaced ? (i < placed.Count ? Session.PartEntry(placed[i].Id) : null)
                    : (i - PartRows < inBag.Count ? inBag[i - PartRows] : null);
                button.gameObject.SetActive(entry != null);
                if (entry == null) continue;
                label.text = isPlaced
                    ? $"Quitar {entry.Name} ({placed[i].Durability}/{entry.Definition.MaxDurability})"
                    : $"Colocar {entry.Name} · {entry.Enables}";
                label.color = isPlaced && placed[i].IsBroken ? UiTheme.Bad : UiTheme.Text;
            }
        }

        private static void Fill(List<UiSlot> slots, Inventory inventory)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (i < inventory.Stacks.Count) ItemView.Show(slots[i], inventory.Stacks[i].Definition, inventory.Stacks[i].Quantity);
                else slots[i].Clear();
            }
        }

        private void Collect()
        {
            if (Session == null) return;
            int moved = Session.CollectOutput();
            Say(moved > 0 ? $"Recoges {moved} objeto(s) de la salida." : "Nada que recoger (o la bolsa está llena).");
        }

        private void AddFuel()
        {
            var next = Session?.FuelsInBag().FirstOrDefault();
            if (next == null) return;
            Say($"Echas {Session.AddFuel(next.Id, 5)}× {next.Name} al fuego.");
        }

        private void TogglePart(int index)
        {
            if (Session == null) return;
            var placed = Session.Station.Parts.Parts;
            var inBag = Session.PartsInBag();
            if (index < PartRows && index < placed.Count)
                Say(Session.RemovePart(placed[index].Id) ? "Pieza retirada a la bolsa." : "No cabe en la bolsa.");
            else if (index >= PartRows && index - PartRows < inBag.Count)
                Say(Session.PlacePart(inBag[index - PartRows].Id) == PartPlaceResult.Placed
                    ? "Pieza colocada: revisa las recetas nuevas." : "No hay hueco para esa pieza.");
        }
    }
}
