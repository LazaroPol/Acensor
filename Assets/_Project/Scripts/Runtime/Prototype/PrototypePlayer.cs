using Specimen.Core.Prototype;
using Specimen.Runtime.Visuals;
using UnityEngine;

namespace Specimen.Runtime.Prototype
{
    /// <summary>Monta el jugador (visual del catálogo + CharacterController + motor) y su cámara.</summary>
    public static class PrototypePlayer
    {
        public static PlayerMotor Spawn(VisualSpawner spawner, LevelPlacement placement)
        {
            var root = new GameObject("Jugador");
            root.transform.position = new Vector3(placement.X, placement.Y, placement.Z);
            var visual = spawner.Spawn(placement);
            foreach (var collider in visual.GetComponentsInChildren<Collider>()) Object.Destroy(collider);
            visual.transform.SetParent(root.transform, false);
            visual.transform.localPosition = Vector3.zero;
            var controller = root.AddComponent<CharacterController>();
            controller.height = 1.8f;
            controller.radius = 0.4f;
            controller.center = new Vector3(0f, 0.9f, 0f);
            var motor = root.AddComponent<PlayerMotor>();
            motor.SetCamera(EnsureCamera(root.transform).transform);
            return motor;
        }

        private static ThirdPersonCamera EnsureCamera(Transform target)
        {
            var camera = Camera.main != null
                ? Camera.main
                : new GameObject("Main Camera") { tag = "MainCamera" }.AddComponent<Camera>();
            var rig = camera.GetComponent<ThirdPersonCamera>();
            if (rig == null) rig = camera.gameObject.AddComponent<ThirdPersonCamera>();
            rig.Follow(target);
            return rig;
        }
    }
}
