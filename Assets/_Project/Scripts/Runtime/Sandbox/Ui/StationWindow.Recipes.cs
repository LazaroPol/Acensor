using System.Collections.Generic;
using System.Linq;
using Specimen.Core.Crafting;
using Specimen.Core.Crafting.Session;
using Specimen.Core.Crafting.Stations;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Columna izquierda: recetas de la estación (verde = se puede, gris = motivo) y botón Fabricar.</summary>
    public sealed partial class StationWindow
    {
        private const int MaxRows = 11;
        private const float RowHeight = 27f;
        private const float ListWidth = 262f;

        private readonly List<(Button button, Text label)> _rows = new List<(Button, Text)>();
        private IReadOnlyList<RecipeOption> _options = new List<RecipeOption>();
        private Text _detail;
        private Button _craft;
        private string _selected;
        private StationSession _builtFor;

        private void BuildRecipes(Vector2 origin)
        {
            var list = UiFactory.Panel(Body, "Recetas", origin, new Vector2(ListWidth, MaxRows * RowHeight + 8f),
                new Color(0f, 0f, 0f, 0.35f), false);
            for (int i = 0; i < MaxRows; i++)
            {
                int index = i;
                var button = UiFactory.Button(list, string.Empty, new Vector2(4f, 4f + i * RowHeight),
                    new Vector2(ListWidth - 8f, RowHeight - 2f), () => Select(index), 13);
                var label = button.GetComponentInChildren<Text>();
                label.alignment = TextAnchor.MiddleLeft;
                ((RectTransform)label.transform).anchoredPosition = new Vector2(8f, 0f);
                _rows.Add((button, label));
            }
            _detail = UiFactory.Label(Body, string.Empty, origin + new Vector2(0f, MaxRows * RowHeight + 14f),
                new Vector2(ListWidth, 96f), 13);
            _craft = UiFactory.Button(Body, "Fabricar", origin + new Vector2(0f, MaxRows * RowHeight + 116f),
                new Vector2(ListWidth, 32f), Craft, 16);
        }

        private void RefreshRecipes()
        {
            _options = Session.Options();
            if (_builtFor != Session || _options.All(o => o.Recipe.Id != _selected))
                _selected = _options.Count > 0 ? _options[0].Recipe.Id : null;
            _builtFor = Session;
            for (int i = 0; i < _rows.Count; i++)
            {
                var (button, label) = _rows[i];
                button.gameObject.SetActive(i < _options.Count);
                if (i >= _options.Count) continue;
                var option = _options[i];
                label.color = option.Recipe.Id == _selected ? UiTheme.Gold : option.IsCraftable ? UiTheme.Good : UiTheme.TextDim;
                label.text = option.Recipe.MainOutput.Item.Name + (option.IsCraftable ? string.Empty
                    : $"  <size=11>({StationLabels.Block(option)})</size>");
            }
            var current = _options.FirstOrDefault(o => o.Recipe.Id == _selected);
            _craft.interactable = current != null && current.IsCraftable;
            _detail.text = current == null ? "Esta estación no ofrece recetas que conozcas todavía."
                : $"<b>{current.Recipe.MainOutput.Item.Name}</b> x{current.Recipe.MainOutput.Quantity} · " +
                  $"{current.Recipe.CraftSeconds:0}s · +{current.Recipe.Xp} XP\n<color=#B0A890>{Ingredients(current.Recipe.Inputs)}</color>" +
                  (current.IsCraftable ? string.Empty : $"\n<color=#F04A3A>{StationLabels.Block(current)}</color>");
        }

        private void Select(int index)
        {
            if (index < _options.Count) _selected = _options[index].Recipe.Id;
            Refresh();
        }

        private void Craft()
        {
            var option = _options.FirstOrDefault(o => o.Recipe.Id == _selected);
            if (option == null || Session == null) return;
            var result = Session.Craft(option.Recipe);
            Say(result == StationCraftResult.Queued
                ? $"Encolado: {option.Recipe.MainOutput.Item.Name}."
                : $"No se puede fabricar: {StationLabels.Result(result)}.");
        }

        /// <summary>«2× Lingote de cobre, 1× cualquier madera» (por etiqueta con su tier mínimo).</summary>
        private static string Ingredients(IEnumerable<IngredientSpec> inputs) => string.Join(", ", inputs.Select(s =>
            s.IsByTag
                ? $"{s.Quantity}× cualquier {s.Tag.ToString().ToLowerInvariant()}{(s.MinTier > 0 ? $" tier {s.MinTier}+" : string.Empty)}"
                : $"{s.Quantity}× {s.Item.Name}"));
    }
}
