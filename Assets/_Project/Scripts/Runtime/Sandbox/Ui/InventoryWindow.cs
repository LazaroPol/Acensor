using System;
using Specimen.Core.Items;
using Specimen.Core.Items.Catalog;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Bolsa (B / I): 6 × 4 huecos con marco del color de rareza y tooltip; clic derecho consume, equipa o lee; clic izquierdo abre los mods.</summary>
    public sealed class InventoryWindow : UiWindow
    {
        private const int Columns = 6;
        private const float Cell = 56f;

        private UiSlot[] _slots;
        private Text _footer;

        public SandboxContext Context { get; set; }
        public SandboxPlayer Player { get; set; }

        /// <summary>Clic izquierdo sobre un arma o armadura: abre su ventana de mods (OF-16).</summary>
        public Action<ItemInstance> OpenMods { get; set; }

        public override string Title => "Bolsa";
        protected override Vector2 Size => new Vector2(Columns * Cell + 24f, Rows * Cell + TitleHeight + 54f);
        private int Rows => Mathf.CeilToInt(Context.Inventory.Capacity / (float)Columns);

        protected override void Build(UiRoot root)
        {
            _slots = new UiSlot[Context.Inventory.Capacity];
            for (int i = 0; i < _slots.Length; i++)
            {
                int index = i;
                var slot = UiSlot.Create(Body, new Vector2(i % Columns * Cell, i / Columns * Cell), Cell - 4f);
                slot.Hover.Text = () => TooltipAt(index);
                slot.Hover.RightClick = () => Use(index);
                slot.Hover.LeftClick = () => Inspect(index);
                _slots[i] = slot;
            }
            _footer = UiFactory.Label(Body, string.Empty, new Vector2(0f, Rows * Cell + 4f), new Vector2(Columns * Cell, 22f),
                13, TextAnchor.MiddleRight, UiTheme.TextDim);
            Context.Inventory.Changed += Refresh;
        }

        private void OnDestroy()
        {
            if (Context != null) Context.Inventory.Changed -= Refresh;
        }

        public override void Refresh()
        {
            if (_slots == null) return;
            var stacks = Context.Inventory.Stacks;
            for (int i = 0; i < _slots.Length; i++)
            {
                if (i < stacks.Count) ItemView.Show(_slots[i], stacks[i].Definition, stacks[i].Quantity);
                else _slots[i].Clear();
            }
            _footer.text = $"Huecos libres: {Context.Inventory.FreeSlots}/{Context.Inventory.Capacity}";
        }

        private string TooltipAt(int index)
        {
            var stacks = Context.Inventory.Stacks;
            if (index >= stacks.Count) return null;
            return ItemView.Tooltip(stacks[index].Definition, stacks[index].Item, stacks[index].Quantity,
                Player.Kit != null ? Player.Gear : null, Player.Stats);
        }

        private void Use(int index)
        {
            var stacks = Context.Inventory.Stacks;
            if (index >= stacks.Count || !Player.IsAlive) return;
            var item = stacks[index].Definition;
            if (Context.ReadKnowledge(item.Id)) { Refresh(); return; }
            if (Player.Gear.CanEquip(item)) Gear.EquipFromBag(Context, Player, stacks[index].Item);
            else if (ConsumableUse.TryUse(Context.Inventory, item.Id, Player.Vitals, out var effect))
                Context.Feed.Add($"Usas {item.Name}: {effect.Description}");
            else Context.Feed.Add($"{item.Name} no se puede usar (material para recetas).");
            Refresh();
        }

        private void Inspect(int index)
        {
            var stacks = Context.Inventory.Stacks;
            if (index >= stacks.Count) return;
            var category = stacks[index].Definition.Category;
            if (category == ItemCategory.Weapon || category == ItemCategory.Armor || category == ItemCategory.Tool)
                OpenMods?.Invoke(stacks[index].Item);
        }
    }
}
