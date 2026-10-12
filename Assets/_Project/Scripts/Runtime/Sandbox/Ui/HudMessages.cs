using System.Text;
using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Textos del HUD: registro de combate, aviso de interacción (E), anuncio central (subida de nivel) y ayuda.</summary>
    public sealed class HudMessages : MonoBehaviour
    {
        private const string HelpText =
            "Arrastrar con el ratón: cámara (botón dcho también gira al personaje) · Rueda: zoom · WASD mover\n" +
            "Tab / clic objetivo · F / clic dcho atacar con el arma · Q firma · 1-5 hechizos · E usar\n" +
            "B bolsa · C personaje · P hechizos · N talentos · M mapa · Esc cerrar · K kit · R reiniciar · F3 +PX · F4 materiales · H ayuda";

        private SandboxContext _context;
        private Text _log;
        private Text _prompt;
        private RectTransform _promptBox;
        private Text _banner;
        private GameObject _help;
        private float _bannerTime;
        private Text _error;
        private float _errorTime;

        public void Init(UiRoot root, SandboxContext context)
        {
            _context = context;
            var log = UiFactory.Node(root.Rect, "Registro", Vector2.zero, new Vector2(560f, 170f));
            log.anchorMin = log.anchorMax = log.pivot = new Vector2(0f, 0f);
            log.anchoredPosition = new Vector2(16f, 40f);
            _log = UiFactory.Label(log, string.Empty, Vector2.zero, new Vector2(560f, 170f), 14);
            _log.alignment = TextAnchor.LowerLeft;
            _promptBox = Centered(root, "Aviso", new Vector2(420f, 34f), 142f, true);
            _prompt = UiFactory.Label(_promptBox, string.Empty, Vector2.zero, new Vector2(420f, 34f), 16, TextAnchor.MiddleCenter);
            var banner = Centered(root, "Anuncio", new Vector2(900f, 90f), 0f, false);
            banner.anchorMin = banner.anchorMax = banner.pivot = new Vector2(0.5f, 1f);
            banner.anchoredPosition = new Vector2(0f, -150f);
            _banner = UiFactory.Label(banner, string.Empty, Vector2.zero, new Vector2(900f, 90f), 30, TextAnchor.MiddleCenter,
                UiTheme.Gold);
            var error = Centered(root, "Error", new Vector2(900f, 30f), 0f, false);
            error.anchorMin = error.anchorMax = error.pivot = new Vector2(0.5f, 1f);
            error.anchoredPosition = new Vector2(0f, -250f);
            _error = UiFactory.Label(error, string.Empty, Vector2.zero, new Vector2(900f, 30f), 20, TextAnchor.MiddleCenter,
                new Color(1f, 0.15f, 0.1f));
            var help = Centered(root, "Ayuda", new Vector2(900f, 64f), 0f, true);
            help.anchorMin = help.anchorMax = help.pivot = new Vector2(0.5f, 1f);
            help.anchoredPosition = new Vector2(0f, -12f);
            UiFactory.Label(help, HelpText, new Vector2(8f, 4f), new Vector2(884f, 58f), 13, TextAnchor.MiddleCenter, UiTheme.TextDim);
            _help = help.gameObject;
        }

        public void Announce(string text)
        {
            _banner.text = text;
            _bannerTime = 4f;
        }

        /// <summary>Aviso rojo en el centro (como «No tienes objetivo» en WoW).</summary>
        public void Error(string text)
        {
            _error.text = text;
            _errorTime = 2f;
        }

        public void ToggleHelp() => _help.SetActive(!_help.activeSelf);

        private static RectTransform Centered(UiRoot root, string name, Vector2 size, float bottom, bool panel)
        {
            var rect = panel ? UiFactory.Panel(root.Rect, name, Vector2.zero, size, UiTheme.Background)
                : UiFactory.Node(root.Rect, name, Vector2.zero, size);
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0f);
            rect.anchoredPosition = new Vector2(0f, bottom);
            return rect;
        }

        private void Update()
        {
            var text = new StringBuilder();
            foreach (string line in _context.Feed.Recent()) text.AppendLine(line);
            _log.text = text.ToString();
            var current = _context.Interactions != null ? _context.Interactions.Current : null;
            _promptBox.gameObject.SetActive(current != null);
            if (current != null) _prompt.text = $"<color=#F0C060><b>E</b></color> · {current.Prompt}";
            _bannerTime -= Time.unscaledDeltaTime;
            _banner.color = new Color(UiTheme.Gold.r, UiTheme.Gold.g, UiTheme.Gold.b, Mathf.Clamp01(_bannerTime));
            _errorTime -= Time.unscaledDeltaTime;
            _error.color = new Color(1f, 0.15f, 0.1f, Mathf.Clamp01(_errorTime));
        }
    }
}
