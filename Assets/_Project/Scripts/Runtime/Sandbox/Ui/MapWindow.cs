using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Mapa del mundo (M): el poblado dibujado desde el layout, con la posición y orientación del jugador.</summary>
    public sealed class MapWindow : UiWindow
    {
        private static readonly Vector2 MapSize = new Vector2(800f, 640f);

        private MapDrawing _drawing;
        private RectTransform _marker;
        private Text _zone;

        public SandboxContext Context { get; set; }
        public SandboxPlayer Player { get; set; }

        public override string Title => "Mapa · Poblado de la Armería";
        protected override Vector2 Size => MapSize + new Vector2(24f, TitleHeight + 56f);

        protected override void Build(UiRoot root)
        {
            var area = UiFactory.Panel(Body, "Plano", Vector2.zero, MapSize, new Color(0.3f, 0.36f, 0.22f, 1f), false);
            _drawing = new MapDrawing(area, MapSize);
            _drawing.Draw(Context.Village);
            _marker = UiFactory.Panel(area, "Jugador", Vector2.zero, new Vector2(14f, 14f), UiTheme.Gold, false);
            _marker.pivot = new Vector2(0.5f, 0.5f);
            UiFactory.Panel(_marker, "Frente", new Vector2(5f, -6f), new Vector2(4f, 8f), Color.white, false);
            _zone = UiFactory.Label(Body, string.Empty, new Vector2(0f, MapSize.y + 6f), new Vector2(MapSize.x, 24f), 15,
                TextAnchor.MiddleLeft, UiTheme.Gold);
        }

        public override void Refresh()
        {
            if (_marker == null) return;
            var p = Player.transform.position;
            var at = _drawing.ToMap(p.x, p.z);
            _marker.anchoredPosition = new Vector2(at.x, -at.y);
            _marker.localRotation = Quaternion.Euler(0f, 0f, -Player.transform.eulerAngles.y);
            var zone = Context.Village.ZoneAt(p.x, p.z);
            _zone.text = $"Estás en: {(zone != null ? $"{zone.Name} — {zone.Hint}" : "Campo abierto")}";
        }

        private void LateUpdate()
        {
            if (IsOpen) Refresh();
        }
    }
}
