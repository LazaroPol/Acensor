using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Specimen.Runtime.UI
{
    /// <summary>Construcción de controles uGUI por código, todos anclados arriba-izquierda dentro de su padre.</summary>
    public static class UiFactory
    {
        public static RectTransform Node(Transform parent, string name, Vector2 position, Vector2 size)
        {
            var go = new GameObject(name, typeof(RectTransform));
            var rect = (RectTransform)go.transform;
            rect.SetParent(parent, false);
            rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = new Vector2(position.x, -position.y);
            rect.sizeDelta = size;
            return rect;
        }

        public static RectTransform Panel(Transform parent, string name, Vector2 position, Vector2 size,
            Color background, bool border = true)
        {
            var rect = Node(parent, name, position, size);
            var image = rect.gameObject.AddComponent<Image>();
            image.sprite = UiSprites.Fill;
            image.color = background;
            if (border) Border(rect, UiTheme.Gold);
            return rect;
        }

        public static Image Border(RectTransform target, Color color)
        {
            var rect = Node(target, "Marco", Vector2.zero, Vector2.zero);
            Stretch(rect);
            var image = rect.gameObject.AddComponent<Image>();
            image.sprite = UiSprites.Frame;
            image.type = Image.Type.Sliced;
            image.color = color;
            image.raycastTarget = false;
            return image;
        }

        public static Text Label(Transform parent, string text, Vector2 position, Vector2 size, int fontSize = 14,
            TextAnchor align = TextAnchor.UpperLeft, Color? color = null)
        {
            var rect = Node(parent, "Texto", position, size);
            var label = rect.gameObject.AddComponent<Text>();
            label.font = UiTheme.Font;
            label.text = text;
            label.fontSize = fontSize;
            label.alignment = align;
            label.color = color ?? UiTheme.Text;
            label.raycastTarget = false;
            label.horizontalOverflow = HorizontalWrapMode.Wrap;
            label.verticalOverflow = VerticalWrapMode.Overflow;
            label.supportRichText = true;
            var shadow = rect.gameObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.8f);
            return label;
        }

        public static Button Button(Transform parent, string text, Vector2 position, Vector2 size, UnityAction onClick,
            int fontSize = 14)
        {
            var rect = Panel(parent, $"Boton_{text}", position, size, new Color(0.32f, 0.08f, 0.06f, 1f));
            var button = rect.gameObject.AddComponent<Button>();
            var colors = button.colors;
            colors.highlightedColor = new Color(1.3f, 1.2f, 1f);
            colors.pressedColor = new Color(0.7f, 0.7f, 0.7f);
            colors.disabledColor = new Color(0.35f, 0.35f, 0.35f);
            button.colors = colors;
            if (onClick != null) button.onClick.AddListener(onClick);
            Label(rect, text, Vector2.zero, size, fontSize, TextAnchor.MiddleCenter, UiTheme.Gold);
            return button;
        }

        public static Image Bar(Transform parent, Vector2 position, Vector2 size, Color fill, out Text label)
        {
            var back = Panel(parent, "Barra", position, size, new Color(0f, 0f, 0f, 0.7f), false);
            Border(back, UiTheme.GoldDim);
            var fillRect = Node(back, "Relleno", new Vector2(2f, 2f), size - new Vector2(4f, 4f));
            var image = fillRect.gameObject.AddComponent<Image>();
            image.sprite = UiSprites.Fill;
            image.color = fill;
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Horizontal;
            label = Label(back, string.Empty, Vector2.zero, size, 12, TextAnchor.MiddleCenter);
            return image;
        }

        public static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
        }

        public static T Add<T>(RectTransform rect, Action<T> setup = null) where T : Component
        {
            var component = rect.gameObject.AddComponent<T>();
            setup?.Invoke(component);
            return component;
        }
    }
}
