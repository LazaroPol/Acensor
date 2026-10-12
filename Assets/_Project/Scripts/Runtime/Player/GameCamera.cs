using Specimen.Core.Movement;
using UnityEngine;

namespace Specimen.Runtime.Player
{
    /// <summary>Cámara cenital-oblicua que sigue al jugador (<see cref="ThirdPersonRig"/>) con zoom (<see cref="CameraZoom"/>).</summary>
    [RequireComponent(typeof(Camera))]
    public sealed class GameCamera : MonoBehaviour
    {
        [SerializeField] private float pitchDegrees = 55f;
        [SerializeField] private float yawDegrees;
        [SerializeField] private float followSharpness = 10f;
        [SerializeField] private float lookHeight = 1f;

        private const float ScrollToNotches = 1f / 120f;

        private const float MinPitch = 8f;
        private const float MaxPitch = 85f;
        private const float ObstacleMargin = 0.25f;

        private CameraZoom _zoom;
        private PlayerInput _input;
        private Transform _target;
        private CameraPoint _rigPoint;

        /// <summary>Giro horizontal de la cámara (grados); el movimiento WASD es relativo a él.</summary>
        public float Yaw
        {
            get => yawDegrees;
            set => yawDegrees = Mathf.Repeat(value, 360f);
        }

        /// <summary>Inclinación (grados sobre el horizonte), limitada entre casi rasante y cenital.</summary>
        public float Pitch
        {
            get => pitchDegrees;
            set => pitchDegrees = Mathf.Clamp(value, MinPitch, MaxPitch);
        }

        /// <summary>Si está activo, la cámara se acerca cuando una pared o un tejado se interpone (como en WoW).</summary>
        public bool AvoidObstacles { get; set; }

        public void Follow(Transform target, PlayerInput input, float initialDistance = 12f)
        {
            _target = target;
            _input = input;
            _zoom = new CameraZoom(initialDistance);
            Apply(Rig().Desired(ToPoint(target.position)));
        }

        private void LateUpdate()
        {
            if (_target == null) return;
            _zoom.Apply(_input.Zoom.ReadValue<float>() * ScrollToNotches);
            var current = _rigPoint;
            Apply(Rig().Follow(current, ToPoint(_target.position), Time.deltaTime));
        }

        private ThirdPersonRig Rig() => new ThirdPersonRig(_zoom.Distance, pitchDegrees, yawDegrees, followSharpness);

        private void Apply(CameraPoint p)
        {
            _rigPoint = p;
            var position = new Vector3(p.X, p.Y, p.Z) + Vector3.up * lookHeight;
            transform.rotation = Quaternion.Euler(pitchDegrees, yawDegrees, 0f);
            if (AvoidObstacles && _target != null) position = Unblocked(_target.position + Vector3.up * (lookHeight + 0.6f), position);
            transform.position = position;
        }

        private static Vector3 Unblocked(Vector3 pivot, Vector3 desired)
        {
            var offset = desired - pivot;
            float distance = offset.magnitude;
            if (distance < 0.01f) return desired;
            return Physics.SphereCast(pivot, 0.2f, offset / distance, out var hit, distance, ~0, QueryTriggerInteraction.Ignore)
                ? pivot + offset / distance * Mathf.Max(0.5f, hit.distance - ObstacleMargin)
                : desired;
        }

        private static CameraPoint ToPoint(Vector3 v) => new CameraPoint(v.x, v.y, v.z);
    }
}
