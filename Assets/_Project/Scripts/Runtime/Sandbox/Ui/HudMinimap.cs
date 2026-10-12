using Specimen.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Specimen.Runtime.Sandbox.Ui
{
    /// <summary>Minimapa (arriba a la derecha): cámara cenital ortográfica sobre el jugador, norte arriba, y nombre de la zona.</summary>
    public sealed class HudMinimap : MonoBehaviour
    {
        private const float Size = 230f;
        private const float Height = 80f;

        private SandboxContext _context;
        private Transform _player;
        private Camera _camera;
        private RectTransform _marker;
        private Text _zone;

        public void Init(UiRoot root, SandboxContext context, Transform player)
        {
            _context = context;
            _player = player;
            var frame = UiFactory.Panel(root.Rect, "Minimapa", Vector2.zero, new Vector2(Size + 12f, Size + 40f), UiTheme.Background);
            frame.anchorMin = frame.anchorMax = frame.pivot = new Vector2(1f, 1f);
            frame.anchoredPosition = new Vector2(-16f, -16f);
            _zone = UiFactory.Label(frame, string.Empty, new Vector2(6f, 6f), new Vector2(Size, 22f), 14, TextAnchor.MiddleCenter,
                UiTheme.Gold);
            var view = UiFactory.Node(frame, "Vista", new Vector2(6f, 32f), new Vector2(Size, Size));
            var image = view.gameObject.AddComponent<RawImage>();
            image.raycastTarget = false;
            image.texture = CreateCamera();
            _marker = UiFactory.Panel(view, "Jugador", new Vector2(Size / 2f, Size / 2f), new Vector2(12f, 12f), UiTheme.Gold, false);
            _marker.pivot = new Vector2(0.5f, 0.5f);
            UiFactory.Panel(_marker, "Frente", new Vector2(4f, -8f), new Vector2(4f, 8f), Color.white, false);
            UiFactory.Border(view, UiTheme.GoldDim);
        }

        private RenderTexture CreateCamera()
        {
            var texture = new RenderTexture(512, 512, 16) { name = "Minimapa" };
            _camera = new GameObject("CamaraMinimapa").AddComponent<Camera>();
            _camera.orthographic = true;
            _camera.orthographicSize = 22f;
            _camera.targetTexture = texture;
            _camera.clearFlags = CameraClearFlags.SolidColor;
            _camera.backgroundColor = new Color(0.15f, 0.18f, 0.12f);
            _camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            _camera.farClipPlane = Height + 20f;
            return texture;
        }

        private void LateUpdate()
        {
            if (_player == null) return;
            var p = _player.position;
            _camera.transform.position = new Vector3(p.x, p.y + Height, p.z);
            _marker.localRotation = Quaternion.Euler(0f, 0f, -_player.eulerAngles.y);
            var zone = _context.Village.ZoneAt(p.x, p.z);
            _zone.text = zone != null ? zone.Name : "Campo abierto";
        }

        private void OnDestroy()
        {
            if (_camera != null && _camera.targetTexture != null) _camera.targetTexture.Release();
        }
    }
}
