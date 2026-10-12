using Specimen.Core.Vitals;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Marco del jugador (arriba a la izquierda: kit, nivel, vida, recurso y aguante) y barra de experiencia.</summary>
    public sealed class HudFrames : MonoBehaviour
    {
        private SandboxContext _context;
        private SandboxPlayer _player;
        private Text _name;
        private Text _level;
        private UiSlot _portrait;
        private (Image fill, Text label) _health, _resource, _stamina, _xp;

        public void Init(UiRoot root, SandboxContext context, SandboxPlayer player)
        {
            _context = context;
            _player = player;
            var frame = UiFactory.Panel(root.Rect, "MarcoJugador", new Vector2(16f, 16f), new Vector2(380f, 116f), UiTheme.Background);
            _portrait = UiSlot.Create(frame, new Vector2(10f, 10f), 92f);
            var badge = UiFactory.Panel(frame, "Nivel", new Vector2(70f, 78f), new Vector2(36f, 30f), UiTheme.Background);
            _level = UiFactory.Label(badge, "1", Vector2.zero, new Vector2(36f, 30f), 16, TextAnchor.MiddleCenter, UiTheme.Gold);
            _name = UiFactory.Label(frame, string.Empty, new Vector2(114f, 8f), new Vector2(260f, 22f), 16, TextAnchor.UpperLeft,
                UiTheme.Gold);
            _health = Bar(frame, 32f, 22f, UiTheme.Health);
            _resource = Bar(frame, 58f, 18f, UiTheme.Resource);
            _stamina = Bar(frame, 80f, 14f, UiTheme.Stamina);
            var xp = UiFactory.Node(root.Rect, "Experiencia", Vector2.zero, new Vector2(1000f, 26f));
            xp.anchorMin = xp.anchorMax = xp.pivot = new Vector2(0.5f, 0f);
            xp.anchoredPosition = new Vector2(0f, 6f);
            _xp.fill = UiFactory.Bar(xp, Vector2.zero, new Vector2(1000f, 26f), UiTheme.Xp, out _xp.label);
            _xp.label.fontSize = 15;
        }

        private static (Image, Text) Bar(Transform parent, float y, float height, Color color)
        {
            var fill = UiFactory.Bar(parent, new Vector2(114f, y), new Vector2(256f, height), color, out var label);
            return (fill, label);
        }

        private void Update()
        {
            if (_player == null || _player.Kit == null) return;
            _name.text = $"{_player.Kit.DisplayName}{(_player.IsShielded ? "  <color=#7FB2FF>[Escudo]</color>" : string.Empty)}" +
                         (_context.Fury.IsActive ? "  <color=#FF8040>[Furia]</color>" : string.Empty);
            _portrait.Set(ItemView.Glyph(_player.Kit.DisplayName), new Color(0.3f, 0.2f, 0.12f), UiTheme.Gold);
            var progress = _context.Hero.Progress;
            _level.text = progress.Level.ToString();
            Show(_health, _player.Vitals.Health);
            Show(_resource, _player.Vitals.ClassResource);
            Show(_stamina, _player.Vitals.Stamina);
            _xp.fill.fillAmount = progress.Fraction;
            _xp.label.text = progress.IsMaxLevel ? "Nivel máximo"
                : $"Nivel {progress.Level} · {progress.Xp} / {progress.XpToNext} PX";
        }

        private static void Show((Image fill, Text label) bar, ResourcePool pool)
        {
            bar.fill.fillAmount = pool.Fraction;
            bar.label.text = $"{pool.Current:0} / {pool.Max:0}";
        }
    }
}
