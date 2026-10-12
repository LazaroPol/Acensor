using System;
using Specimen.Core.Talents;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Un talento del árbol: icono, rango «x/y» y tooltip con el efecto actual y el del siguiente rango.</summary>
    public sealed class TalentCell
    {
        private readonly UiSlot _slot;
        private readonly Text _rank;
        private TalentAllocation _allocation;

        public TalentCell(Transform parent, Vector2 position, float size, TalentDefinition talent, int branch,
            Action<TalentDefinition> spend)
        {
            Talent = talent;
            _slot = UiSlot.Create(parent, position, size);
            _slot.Hover.Text = Tooltip;
            _slot.Hover.LeftClick = () => spend(talent);
            var rankPosition = new Vector2(position.x + size - 22f, position.y + size - 10f);
            var badge = UiFactory.Panel(parent, "Rango", rankPosition, new Vector2(34f, 18f), new Color(0f, 0f, 0f, 0.85f), false);
            _rank = UiFactory.Label(badge, string.Empty, Vector2.zero, new Vector2(34f, 18f), 12, TextAnchor.MiddleCenter);
        }

        public TalentDefinition Talent { get; }

        public void Refresh(TalentAllocation allocation, bool canSpend)
        {
            _allocation = allocation;
            int rank = allocation.RankOf(Talent.Id);
            bool unlocked = rank > 0 || canSpend;
            bool maxed = rank >= Talent.MaxRank;
            var border = maxed ? UiTheme.Gold : rank > 0 || canSpend ? UiTheme.Good : UiTheme.GoldDim;
            _slot.Set(ItemView.Glyph(Talent.Name), Tint(Talent.Kind), border, 0, !unlocked);
            _rank.text = $"{rank}/{Talent.MaxRank}";
            _rank.color = maxed ? UiTheme.Gold : rank > 0 ? UiTheme.Good : UiTheme.TextDim;
        }

        private string Tooltip()
        {
            if (_allocation == null) return null;
            int rank = _allocation.RankOf(Talent.Id);
            string text = $"<size=16><b>{Talent.Name}</b></size>\n<color=#B0A890>Rango {rank}/{Talent.MaxRank}</color>";
            if (rank > 0) text += $"\n{Talent.Describe(rank)}";
            if (rank < Talent.MaxRank) text += $"\n<color=#B0A890>Siguiente rango:</color>\n{Talent.Describe(rank + 1)}";
            if (Talent.RequiredBranchPoints > 0)
                text += $"\n<color=#F0C060>Requiere {Talent.RequiredBranchPoints} puntos en la rama.</color>";
            return text;
        }

        private static Color Tint(TalentEffectKind kind) => kind switch
        {
            TalentEffectKind.PoderHechizos => new Color(0.5f, 0.25f, 0.6f),
            TalentEffectKind.Enfriamiento => new Color(0.25f, 0.45f, 0.6f),
            TalentEffectKind.StatPlano => new Color(0.5f, 0.4f, 0.2f),
            _ => new Color(0.55f, 0.25f, 0.2f)
        };
    }
}
