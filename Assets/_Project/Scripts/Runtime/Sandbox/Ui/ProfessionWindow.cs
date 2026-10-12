using System.Collections.Generic;
using Specimen.Core.Crafting;
using Specimen.Core.Professions;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>
    /// Ventana de profesión (E en un banco), al estilo de WoW: lista de recetas a la izquierda (verde = puedes
    /// fabricarla), detalle con componentes «tienes/necesitas» a la derecha y botón Fabricar.
    /// </summary>
    public sealed partial class ProfessionWindow : UiWindow
    {
        private const float ListWidth = 230f;

        private readonly List<(Button button, Text label, Recipe recipe)> _list = new List<(Button, Text, Recipe)>();
        private RectTransform _listRoot;
        private Text _status;
        private Button _learn;
        private Text _learnLabel;
        private ProfessionId? _built;
        private Recipe _selected;

        public SandboxContext Context { get; set; }
        public SandboxPlayer Player { get; set; }
        public ProfessionId Profession { get; private set; }

        public override string Title => "Profesiones";
        protected override Vector2 Size => new Vector2(640f, 520f);

        private bool Learned => Context.Hero.Professions.Knows(Profession);

        public void Show(ProfessionId profession)
        {
            Profession = profession;
            Open();
        }

        protected override void Build(UiRoot root)
        {
            _status = UiFactory.Label(Body, string.Empty, Vector2.zero, new Vector2(440f, 44f), 15);
            _learn = UiFactory.Button(Body, "Aprender", new Vector2(456f, 4f), new Vector2(160f, 30f), ToggleLearn);
            _learnLabel = _learn.GetComponentInChildren<Text>();
            UiFactory.Panel(Body, "Separador", new Vector2(0f, 48f), new Vector2(616f, 2f), UiTheme.GoldDim, false);
            _listRoot = UiFactory.Panel(Body, "Recetas", new Vector2(0f, 58f), new Vector2(ListWidth, 410f),
                new Color(0f, 0f, 0f, 0.35f), false);
            BuildDetail(new Vector2(ListWidth + 14f, 58f));
        }

        public override void Refresh()
        {
            if (_built != Profession) RebuildList();
            var known = Context.Hero.Professions.All;
            _status.text = $"<size=20><b>{Profession}</b></size>   " + (Learned
                ? "<color=#5AE65A>Aprendida</color>"
                : $"<color=#F0C060>Sin aprender</color> <color=#B0A890>({known.Count}/{LearnedProfessions.Max} profesiones)</color>");
            _learnLabel.text = Learned ? "Abandonar" : "Aprender";
            _learn.interactable = Learned || !Context.Hero.Professions.IsFull;
            foreach (var (_, label, recipe) in _list)
            {
                bool can = Learned && recipe.HasIngredients(Context.Inventory);
                label.color = recipe == _selected ? UiTheme.Gold : can ? UiTheme.Good : UiTheme.TextDim;
                label.text = $"{recipe.Output.Name}" + (recipe.OutputQuantity > 1 ? $" x{recipe.OutputQuantity}" : string.Empty);
            }
            RefreshDetail();
        }

        private void RebuildList()
        {
            foreach (var entry in _list) Destroy(entry.button.gameObject);
            _list.Clear();
            var recipes = Context.Recipes.For(Profession);
            for (int i = 0; i < recipes.Count; i++)
            {
                var recipe = recipes[i];
                var button = UiFactory.Button(_listRoot, recipe.Id, new Vector2(6f, 6f + i * 36f), new Vector2(ListWidth - 12f, 32f),
                    () => Select(recipe));
                var label = button.GetComponentInChildren<Text>();
                label.alignment = TextAnchor.MiddleLeft;
                ((RectTransform)label.transform).anchoredPosition = new Vector2(10f, 0f);
                _list.Add((button, label, recipe));
            }
            _selected = recipes.Count > 0 ? recipes[0] : null;
            _built = Profession;
        }

        private void Select(Recipe recipe)
        {
            _selected = recipe;
            Refresh();
        }

        private void ToggleLearn()
        {
            var professions = Context.Hero.Professions;
            if (Learned) professions.Forget(Profession);
            else if (professions.Learn(Profession)) Context.Feed.Add($"Has aprendido {Profession}.");
            Refresh();
        }
    }
}
