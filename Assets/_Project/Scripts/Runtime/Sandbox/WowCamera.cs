using Specimen.Runtime.Player;
using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>
    /// Cámara al estilo WoW sobre <see cref="GameCamera"/>: arrastrar con el botón izquierdo gira la cámara alrededor
    /// del personaje, con el derecho la gira y el personaje se orienta hacia donde mira; la rueda acerca/aleja y la
    /// cámara se acerca sola si una pared o un tejado tapa al personaje.
    /// </summary>
    public sealed class WowCamera : MonoBehaviour
    {
        private const float Sensitivity = 0.25f;

        private GameCamera _camera;
        private MouseGestures _mouse;
        private SandboxPlayer _player;

        public void Init(GameCamera camera, MouseGestures mouse, SandboxPlayer player)
        {
            _camera = camera;
            _mouse = mouse;
            _player = player;
            _camera.AvoidObstacles = true;
            _camera.Yaw = 0f;
            _camera.Pitch = 40f;
        }

        private void Update()
        {
            if (_camera == null || !_mouse.Dragging) return;
            var delta = _mouse.Delta * Sensitivity;
            _camera.Yaw += delta.x;
            _camera.Pitch -= delta.y;
            if (_mouse.RightDragging && _player != null && _player.IsAlive)
                _player.transform.rotation = Quaternion.Euler(0f, _camera.Yaw, 0f);
        }
    }
}
