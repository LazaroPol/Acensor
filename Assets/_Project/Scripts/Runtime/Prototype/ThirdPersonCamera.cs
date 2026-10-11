using Specimen.Core.Movement;
using UnityEngine;

namespace Specimen.Runtime.Prototype
{
    /// <summary>Coloca la cámara según <see cref="ThirdPersonRig"/> del núcleo y mira al objetivo.</summary>
    [RequireComponent(typeof(Camera))]
    public sealed class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private float distance = 12f;
        [SerializeField] private float pitchDegrees = 55f;
        [SerializeField] private float yawDegrees;
        [SerializeField] private float followSharpness = 10f;
        [SerializeField] private float lookHeight = 1f;

        private ThirdPersonRig _rig;
        private Transform _target;

        public void Follow(Transform target)
        {
            _target = target;
            _rig = new ThirdPersonRig(distance, pitchDegrees, yawDegrees, followSharpness);
            Apply(_rig.Desired(ToPoint(target.position)));
        }

        private void LateUpdate()
        {
            if (_target == null) return;
            Apply(_rig.Follow(ToPoint(transform.position - Vector3.up * lookHeight), ToPoint(_target.position),
                Time.deltaTime));
        }

        private void Apply(CameraPoint point)
        {
            transform.position = new Vector3(point.X, point.Y, point.Z) + Vector3.up * lookHeight;
            transform.rotation = Quaternion.Euler(pitchDegrees, yawDegrees, 0f);
        }

        private static CameraPoint ToPoint(Vector3 v) => new CameraPoint(v.x, v.y, v.z);
    }
}
