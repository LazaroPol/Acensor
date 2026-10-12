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

        private CameraZoom _zoom;
        private PlayerInput _input;
        private Transform _target;

        public void Follow(Transform target, PlayerInput input)
        {
            _target = target;
            _input = input;
            _zoom = new CameraZoom();
            Apply(Rig().Desired(ToPoint(target.position)));
        }

        private void LateUpdate()
        {
            if (_target == null) return;
            _zoom.Apply(_input.Zoom.ReadValue<float>() * ScrollToNotches);
            var current = ToPoint(transform.position - Vector3.up * lookHeight);
            Apply(Rig().Follow(current, ToPoint(_target.position), Time.deltaTime));
        }

        private ThirdPersonRig Rig() => new ThirdPersonRig(_zoom.Distance, pitchDegrees, yawDegrees, followSharpness);

        private void Apply(CameraPoint p)
        {
            transform.position = new Vector3(p.X, p.Y, p.Z) + Vector3.up * lookHeight;
            transform.rotation = Quaternion.Euler(pitchDegrees, yawDegrees, 0f);
        }

        private static CameraPoint ToPoint(Vector3 v) => new CameraPoint(v.x, v.y, v.z);
    }
}
