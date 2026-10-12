using Specimen.Core.Hud;
using UnityEngine;

namespace Specimen.Runtime.Hud
{
    /// <summary>Dibuja el HUD de partida (IMGUI) a partir de <see cref="GameHudPresenter"/>; no contiene lógica de juego.</summary>
    public sealed class GameHud : MonoBehaviour
    {
        private const float BarWidth = 220f;
        private const float BarHeight = 18f;

        private GameHudPresenter _presenter;
        private GUIStyle _label;
        private Texture2D _pixel;

        public void Init(GameHudPresenter presenter) => _presenter = presenter;

        private void Update() => _presenter?.Notices.Tick(Time.deltaTime);

        private void OnGUI()
        {
            if (_presenter == null) return;
            EnsureStyles();
            Bar(new Rect(10f, 10f, BarWidth, BarHeight), _presenter.Health, new Color(0.8f, 0.15f, 0.15f));
            Bar(new Rect(10f, 32f, BarWidth, BarHeight), _presenter.ClassResource, new Color(0.2f, 0.4f, 0.9f));
            Bar(new Rect(10f, 54f, BarWidth, BarHeight), _presenter.Stamina, new Color(0.9f, 0.75f, 0.2f));
            Timer();
            Notices();
        }

        private void Timer()
        {
            var color = _presenter.IsTimerCritical ? Color.red : Color.white;
            var text = $"{_presenter.FloorText}  {_presenter.PhaseText}  {_presenter.TimerText}";
            Label(new Rect(Screen.width * 0.5f - 150f, 10f, 300f, 30f), text, color, TextAnchor.UpperCenter);
        }

        private void Notices()
        {
            float y = 50f;
            foreach (var notice in _presenter.Notices.Active())
            {
                Label(new Rect(Screen.width * 0.5f - 250f, y, 500f, 28f), notice.Text, Tint(notice.Severity),
                    TextAnchor.UpperCenter);
                y += 28f;
            }
        }

        private void Bar(Rect rect, HudBar bar, Color fill)
        {
            Fill(rect, new Color(0f, 0f, 0f, 0.6f));
            Fill(new Rect(rect.x, rect.y, rect.width * bar.Fraction, rect.height), fill);
            Label(rect, $"{bar.Label} {Mathf.RoundToInt(bar.Current)}/{Mathf.RoundToInt(bar.Max)}", Color.white,
                TextAnchor.MiddleCenter);
        }

        private void Fill(Rect rect, Color color)
        {
            var previous = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(rect, _pixel);
            GUI.color = previous;
        }

        private void Label(Rect rect, string text, Color color, TextAnchor anchor)
        {
            _label.alignment = anchor;
            _label.normal.textColor = color;
            GUI.Label(rect, text, _label);
        }

        private static Color Tint(NoticeSeverity severity) =>
            severity == NoticeSeverity.Danger ? Color.red : severity == NoticeSeverity.Warning ? Color.yellow : Color.white;

        private void EnsureStyles()
        {
            if (_label != null) return;
            _label = new GUIStyle(GUI.skin.label) { fontSize = 16, fontStyle = FontStyle.Bold };
            _pixel = Texture2D.whiteTexture;
        }
    }
}
