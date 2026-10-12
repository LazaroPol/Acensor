using System.Collections.Generic;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Libro de hechizos (P): los cinco hechizos del kit, con nivel de aprendizaje, tecla y descripción.</summary>
    public sealed class SpellbookWindow : UiWindow
    {
        private const float Row = 78f;
        private const int Spells = 5;

        private readonly List<(UiSlot slot, Text text)> _rows = new List<(UiSlot, Text)>();

        public SandboxContext Context { get; set; }
        public SandboxPlayer Player { get; set; }

        public override string Title => "Libro de hechizos";
        protected override Vector2 Size => new Vector2(500f, Spells * Row + TitleHeight + 50f);

        protected override void Build(UiRoot root)
        {
            for (int i = 0; i < Spells; i++)
            {
                int index = i;
                var slot = UiSlot.Create(Body, new Vector2(0f, i * Row + 4f), 60f, (i + 1).ToString());
                slot.Hover.Text = () => TooltipAt(index);
                slot.Hover.LeftClick = () => Player.GetComponent<SpellCaster>().Cast(index);
                var text = UiFactory.Label(Body, string.Empty, new Vector2(72f, i * Row), new Vector2(400f, Row - 4f), 14);
                _rows.Add((slot, text));
            }
            UiFactory.Label(Body, "Los hechizos se aprenden al subir de nivel. Teclas 1-5 o clic en el icono.",
                new Vector2(0f, Spells * Row + 2f), new Vector2(476f, 30f), 13, TextAnchor.UpperLeft, UiTheme.TextDim);
        }

        public override void Refresh()
        {
            if (Player.Kit == null) return;
            var book = Context.Hero.SpellbookFor(Player.Kit.Id).All;
            int level = Context.Hero.Level;
            for (int i = 0; i < _rows.Count; i++)
            {
                var (slot, text) = _rows[i];
                if (i >= book.Count)
                {
                    slot.Clear();
                    text.text = string.Empty;
                    continue;
                }
                var spell = book[i];
                bool known = level >= spell.Level;
                slot.Set(SpellView.Glyph(spell), SpellView.Tint(spell), known ? UiTheme.Gold : UiTheme.GoldDim, 0, !known);
                string state = known ? "<color=#5AE65A>Aprendido</color>" : $"<color=#F04A3A>Nivel {spell.Level}</color>";
                text.text = $"<b>{spell.Name}</b>  {state}\n<color=#B0A890>{SpellView.KindName(spell.Kind)} · " +
                            $"Coste {spell.Cost:0} · {spell.Cooldown:0.#} s</color>\n{spell.Description}";
            }
        }

        private string TooltipAt(int index)
        {
            if (Player.Kit == null) return null;
            var book = Context.Hero.SpellbookFor(Player.Kit.Id).All;
            return index < book.Count ? SpellView.Tooltip(book[index], Context.Hero.Level) : null;
        }
    }
}
