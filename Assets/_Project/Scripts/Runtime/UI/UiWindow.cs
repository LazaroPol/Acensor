using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.UI
{
    /// <summary>
    /// Ventana base: fondo, marco dorado, título arrastrable y botón de cerrar. Las ventanas concretas
    /// construyen su contenido en <see cref="Build"/> y lo refrescan en <see cref="Refresh"/> mientras están abiertas.
    /// </summary>
    public abstract class UiWindow : MonoBehaviour
    {
        public const float TitleHeight = 34f;

        private float _refreshTimer;

        public RectTransform Body { get; private set; }
        public bool IsOpen => gameObject.activeSelf;
        public abstract string Title { get; }
        protected abstract Vector2 Size { get; }

        /// <summary>Crea la ventana; <paramref name="setup"/> inyecta dependencias antes de construir el contenido.</summary>
        public static T Spawn<T>(UiRoot root, Vector2 position, System.Action<T> setup) where T : UiWindow
        {
            var window = new GameObject(typeof(T).Name, typeof(RectTransform)).AddComponent<T>();
            setup?.Invoke(window);
            window.Create(root, position);
            return window;
        }

        public void Create(UiRoot root, Vector2 position)
        {
            var rect = (RectTransform)transform;
            rect.SetParent(root.Windows, false);
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = new Vector2(position.x, -position.y);
            rect.sizeDelta = Size;
            var background = gameObject.AddComponent<Image>();
            background.sprite = UiSprites.Fill;
            background.color = UiTheme.Background;
            UiFactory.Border(rect, UiTheme.Gold);
            var title = UiFactory.Panel(rect, "Titulo", new Vector2(4f, 4f), new Vector2(Size.x - 8f, TitleHeight - 6f),
                new Color(0.3f, 0.07f, 0.05f, 1f), false);
            title.gameObject.AddComponent<UiDragHandle>().Target = rect;
            UiFactory.Label(title, Title, new Vector2(10f, 0f), new Vector2(Size.x - 60f, TitleHeight - 6f), 17,
                TextAnchor.MiddleLeft, UiTheme.Gold).fontStyle = FontStyle.Bold;
            UiFactory.Button(rect, "X", new Vector2(Size.x - 34f, 6f), new Vector2(26f, 24f), Close, 13);
            Body = UiFactory.Node(rect, "Cuerpo", new Vector2(12f, TitleHeight + 6f),
                new Vector2(Size.x - 24f, Size.y - TitleHeight - 18f));
            Build(root);
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            if (IsOpen) Close();
            else Open();
        }

        public void Open()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            Refresh();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            UiTooltip.Hide();
        }

        protected abstract void Build(UiRoot root);

        public abstract void Refresh();

        private void Update()
        {
            _refreshTimer -= Time.unscaledDeltaTime;
            if (_refreshTimer > 0f) return;
            _refreshTimer = 0.25f;
            Refresh();
        }
    }
}
