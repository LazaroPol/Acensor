using System.Collections.Generic;
using Specimen.Core.Crafting;
using Specimen.Core.Progression;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Panel derecho de la ventana de profesión: objeto resultante, componentes y botón Fabricar.</summary>
    public sealed partial class ProfessionWindow
    {
        private const int MaxReagents = 4;

        private readonly List<(UiSlot slot, Text text)> _reagents = new List<(UiSlot, Text)>();
        private UiSlot _output;
        private Text _outputText;
        private Text _hint;
        private Button _craft;

        private void BuildDetail(Vector2 origin)
        {
            _output = UiSlot.Create(Body, origin, 64f);
            _output.Hover.Text = () => _selected != null ? ItemView.Tooltip(_selected.Output, null, _selected.OutputQuantity) : null;
            _outputText = UiFactory.Label(Body, string.Empty, origin + new Vector2(76f, 4f), new Vector2(290f, 60f), 16);
            UiFactory.Label(Body, "<b>Componentes</b>", origin + new Vector2(0f, 80f), new Vector2(300f, 20f), 14,
                TextAnchor.UpperLeft, UiTheme.Gold);
            for (int i = 0; i < MaxReagents; i++)
            {
                var position = origin + new Vector2(0f, 106f + i * 54f);
                int index = i;
                var slot = UiSlot.Create(Body, position, 46f);
                slot.Hover.Text = () => ReagentTip(index);
                _reagents.Add((slot, UiFactory.Label(Body, string.Empty, position + new Vector2(56f, 12f), new Vector2(300f, 24f), 15)));
            }
            _hint = UiFactory.Label(Body, string.Empty, origin + new Vector2(0f, 330f), new Vector2(370f, 40f), 13,
                TextAnchor.UpperLeft, UiTheme.TextDim);
            _craft = UiFactory.Button(Body, "Fabricar", origin + new Vector2(210f, 372f), new Vector2(160f, 34f), Craft, 16);
        }

        private void RefreshDetail()
        {
            if (_selected == null) return;
            ItemView.Show(_output, _selected.Output, _selected.OutputQuantity);
            _outputText.text = $"<b>{ItemView.Colored(_selected.Output)}</b>\n<color=#B0A890>" +
                               $"{ItemView.CategoryName(_selected.Output.Category)} · {ItemView.RarityName(_selected.Output.Rarity)}</color>";
            var ingredients = _selected.Ingredients;
            for (int i = 0; i < _reagents.Count; i++)
            {
                var (slot, text) = _reagents[i];
                bool used = i < ingredients.Count;
                slot.gameObject.SetActive(used);
                text.gameObject.SetActive(used);
                if (!used) continue;
                var ingredient = ingredients[i];
                int have = Context.Inventory.CountOf(ingredient.ItemId);
                bool enough = have >= ingredient.Quantity;
                ItemView.Show(slot, ingredient.Item, 0, !enough);
                text.text = $"{ingredient.Item.Name}  <color=#{(enough ? "5AE65A" : "F04A3A")}>{have}/{ingredient.Quantity}</color>";
            }
            _hint.text = Learned ? "Consigue materiales talando, picando y recogiendo hierbas en el poblado, o del botín."
                : "Aprende esta profesión para poder fabricar sus recetas (máximo dos).";
            _craft.interactable = Learned && _selected.HasIngredients(Context.Inventory);
        }

        private string ReagentTip(int index)
        {
            if (_selected == null || index >= _selected.Ingredients.Count) return null;
            var ingredient = _selected.Ingredients[index];
            return ItemView.Tooltip(ingredient.Item) + $"\nTienes {Context.Inventory.CountOf(ingredient.ItemId)}";
        }

        private void Craft()
        {
            if (_selected == null) return;
            var result = Context.Crafter.TryCraft(_selected.Id, Profession, Context.Inventory);
            if (result == CraftResult.Crafted)
            {
                Context.Feed.Add($"Fabricas {_selected.Output.Name} x{_selected.OutputQuantity}.");
                Context.GrantXp(XpRewards.Craft, "fabricación");
                Player.Animator.Play(Core.Animation.AnimSlot.Interactuar);
            }
            else Context.Feed.Add($"No se puede fabricar: {result}");
            Refresh();
        }
    }
}
