using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace Specimen.Runtime.UI
{
    /// <summary>Canvas de pantalla (1920 × 1080 escalado) y EventSystem con el Input System. Uno por escena.</summary>
    public sealed class UiRoot : MonoBehaviour
    {
        public RectTransform Rect { get; private set; }
        public RectTransform Windows { get; private set; }
        public RectTransform Overlay { get; private set; }

        public static UiRoot Create()
        {
            var go = new GameObject("UI", typeof(RectTransform));
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 10;
            var scaler = go.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;
            go.AddComponent<GraphicRaycaster>();
            EnsureEventSystem();
            var root = go.AddComponent<UiRoot>();
            root.Rect = (RectTransform)go.transform;
            root.Windows = Layer(root.Rect, "Ventanas");
            root.Overlay = Layer(root.Rect, "Superpuesto");
            return root;
        }

        /// <summary>¿Está el ratón sobre algún elemento de la interfaz? (para no atacar al hacer clic en un menú).</summary>
        public static bool PointerOverUi()
            => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

        private static RectTransform Layer(RectTransform parent, string name)
        {
            var rect = UiFactory.Node(parent, name, Vector2.zero, Vector2.zero);
            UiFactory.Stretch(rect);
            return rect;
        }

        private static void EnsureEventSystem()
        {
            if (FindAnyObjectByType<EventSystem>() != null) return;
            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<InputSystemUIInputModule>();
        }
    }
}
