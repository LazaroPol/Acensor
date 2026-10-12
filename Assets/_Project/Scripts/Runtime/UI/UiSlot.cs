using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.UI
{
    /// <summary>Hueco cuadrado (bolsa, barra de acción, talento): «icono» de iniciales, cantidad, marco de color y enfriamiento.</summary>
    public sealed class UiSlot : MonoBehaviour
    {
        private Image _icon;
        private Image _border;
        private Image _cooldown;
        private Text _glyph;
        private Text _count;
        private Text _key;

        public UiHoverTip Hover { get; private set; }

        public static UiSlot Create(Transform parent, Vector2 position, float size, string key = null)
        {
            var rect = UiFactory.Panel(parent, "Hueco", position, new Vector2(size, size), UiTheme.Slot, false);
            var slot = rect.gameObject.AddComponent<UiSlot>();
            slot._icon = UiFactory.Panel(rect, "Icono", new Vector2(3f, 3f), new Vector2(size - 6f, size - 6f),
                Color.clear, false).GetComponent<Image>();
            slot._icon.raycastTarget = false;
            var cooldown = UiFactory.Panel(rect, "Enfriamiento", Vector2.zero, new Vector2(size, size),
                new Color(0f, 0f, 0f, 0.65f), false).GetComponent<Image>();
            cooldown.type = Image.Type.Filled;
            cooldown.fillMethod = Image.FillMethod.Radial360;
            cooldown.fillOrigin = (int)Image.Origin360.Top;
            cooldown.raycastTarget = false;
            cooldown.fillAmount = 0f;
            slot._cooldown = cooldown;
            slot._glyph = UiFactory.Label(rect, string.Empty, Vector2.zero, new Vector2(size, size), (int)(size * 0.36f),
                TextAnchor.MiddleCenter);
            slot._count = UiFactory.Label(rect, string.Empty, new Vector2(2f, size - 18f), new Vector2(size - 5f, 16f), 12,
                TextAnchor.LowerRight);
            int keySize = Mathf.Max(13, (int)(size * 0.3f));
            slot._key = UiFactory.Label(rect, key ?? string.Empty, new Vector2(5f, 2f), new Vector2(size - 8f, keySize + 4f),
                keySize, TextAnchor.UpperLeft, new Color(1f, 0.92f, 0.7f));
            slot._key.fontStyle = FontStyle.Bold;
            slot._border = UiFactory.Border(rect, UiTheme.GoldDim);
            slot.Hover = rect.gameObject.AddComponent<UiHoverTip>();
            slot.Clear();
            return slot;
        }

        public void Set(string glyph, Color tint, Color border, int count = 0, bool dim = false)
        {
            _glyph.text = glyph;
            _glyph.color = dim ? UiTheme.TextDim : UiTheme.Text;
            _icon.color = dim ? tint * 0.35f : tint;
            _border.color = border;
            _count.text = count > 1 ? count.ToString() : string.Empty;
        }

        public void SetCooldown(float fraction) => _cooldown.fillAmount = Mathf.Clamp01(fraction);

        public void Clear() => Set(string.Empty, Color.clear, UiTheme.GoldDim);
    }
}
