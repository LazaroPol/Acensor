using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Specimen.Runtime.UI
{
    /// <summary>Tooltip único que sigue al ratón (texto enriquecido: nombre coloreado, descripción, costes…).</summary>
    public sealed class UiTooltip : MonoBehaviour
    {
        private const float Width = 300f;

        private static UiTooltip _instance;
        private RectTransform _rect;
        private Text _text;

        public static void Create(UiRoot root)
        {
            var rect = UiFactory.Panel(root.Overlay, "Tooltip", Vector2.zero, new Vector2(Width, 60f),
                new Color(0.02f, 0.02f, 0.06f, 0.95f));
            rect.GetComponent<Image>().raycastTarget = false;
            _instance = rect.gameObject.AddComponent<UiTooltip>();
            _instance._rect = rect;
            _instance._text = UiFactory.Label(rect, string.Empty, new Vector2(10f, 8f), new Vector2(Width - 20f, 40f), 14);
            rect.gameObject.SetActive(false);
        }

        public static void Show(string text)
        {
            if (_instance == null) return;
            _instance._text.text = text;
            float height = _instance._text.preferredHeight + 18f;
            _instance._rect.sizeDelta = new Vector2(Width, height);
            _instance.gameObject.SetActive(true);
            _instance.transform.SetAsLastSibling();
            _instance.Follow();
        }

        public static void Hide()
        {
            if (_instance != null) _instance.gameObject.SetActive(false);
        }

        private void Update() => Follow();

        private void Follow()
        {
            if (Mouse.current == null) return;
            var canvas = GetComponentInParent<Canvas>();
            float scale = canvas != null ? canvas.scaleFactor : 1f;
            var mouse = Mouse.current.position.ReadValue() / scale;
            var screen = new Vector2(Screen.width, Screen.height) / scale;
            float x = Mathf.Min(mouse.x + 18f, screen.x - Width - 4f);
            float y = Mathf.Max(mouse.y - 12f, _rect.sizeDelta.y + 4f);
            _rect.anchoredPosition = new Vector2(x, -(screen.y - y));
        }
    }
}
