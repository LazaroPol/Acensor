using Specimen.Core.Gathering;
using Specimen.Core.World;
using Specimen.Runtime.UI;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Dibuja el plano del poblado con rectángulos uGUI: zonas, caminos, casas, recursos y campamentos.</summary>
    public sealed class MapDrawing
    {
        private readonly RectTransform _area;
        private readonly Vector2 _size;

        public MapDrawing(RectTransform area, Vector2 size)
        {
            _area = area;
            _size = size;
        }

        public float Scale => _size.x / MapProjection.Width;

        public Vector2 ToMap(float x, float z) =>
            new Vector2(MapProjection.U(x) * _size.x, (1f - MapProjection.V(z)) * _size.y);

        public void Draw(VillageLayout layout)
        {
            foreach (var zone in layout.Zones) Zone(zone);
            foreach (var tile in layout.Paths) Box(tile.X, tile.Z, 2f, 2f, new Color(0.62f, 0.5f, 0.32f, 0.9f));
            foreach (var house in layout.Houses)
            {
                bool turned = Mathf.Abs(house.Yaw) % 180f > 1f;
                Box(house.X, house.Z, turned ? house.Depth : house.Width, turned ? house.Width : house.Depth,
                    new Color(0.55f, 0.3f, 0.22f));
            }
            foreach (var (kind, spot) in layout.Nodes) Box(spot.X, spot.Z, 1.2f, 1.2f, NodeColor(kind));
            foreach (var camp in layout.Camps) Camp(camp);
        }

        private void Zone(MapZone zone)
        {
            var color = zone.Name.StartsWith("Bosque") ? new Color(0.25f, 0.33f, 0.22f, 0.8f)
                : zone.Name.StartsWith("Cantera") ? new Color(0.45f, 0.4f, 0.33f, 0.8f)
                : zone.Name.StartsWith("Campamento") ? new Color(0.45f, 0.22f, 0.18f, 0.6f)
                : zone.Name.StartsWith("Armería") ? new Color(0.35f, 0.32f, 0.3f, 0.95f)
                : new Color(0.5f, 0.45f, 0.35f, 0.5f);
            var rect = Box(zone.CenterX, zone.CenterZ, zone.MaxX - zone.MinX, zone.MaxZ - zone.MinZ, color);
            UiFactory.Label(rect, $"<b>{zone.Name}</b>", new Vector2(4f, 2f), rect.sizeDelta - new Vector2(8f, 4f), 13,
                TextAnchor.UpperCenter, UiTheme.Gold);
        }

        private void Camp(WorldSpot camp)
        {
            Box(camp.X, camp.Z, 2.4f, 2.4f, new Color(0.85f, 0.15f, 0.1f));
            var at = ToMap(camp.X, camp.Z);
            UiFactory.Label(_area, camp.Id, at + new Vector2(-50f, 8f), new Vector2(100f, 16f), 10, TextAnchor.UpperCenter,
                new Color(1f, 0.6f, 0.5f));
        }

        private static Color NodeColor(GatherKind kind) => kind switch
        {
            GatherKind.Arbol => new Color(0.15f, 0.4f, 0.12f),
            GatherKind.Roca => new Color(0.6f, 0.6f, 0.6f),
            GatherKind.VetaHierro => new Color(0.85f, 0.45f, 0.2f),
            _ => new Color(0.65f, 0.4f, 0.75f)
        };

        public RectTransform Box(float x, float z, float width, float depth, Color color)
        {
            var size = new Vector2(Mathf.Max(2f, width * Scale), Mathf.Max(2f, depth * Scale));
            var rect = UiFactory.Panel(_area, "Mapa", ToMap(x, z) - size / 2f, size, color, false);
            rect.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
            return rect;
        }
    }
}
