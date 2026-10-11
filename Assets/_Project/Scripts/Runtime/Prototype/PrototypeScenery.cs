using UnityEngine;

namespace Specimen.Runtime.Prototype
{
    /// <summary>Cámara y luz mínimas si la escena no las trae (PG-03 pondrá la cámara del jugador).</summary>
    public static class PrototypeScenery
    {
        public static void EnsureCameraAndLight(int width, int depth)
        {
            if (Camera.main == null)
            {
                var camera = new GameObject("Main Camera") { tag = "MainCamera" }.AddComponent<Camera>();
                camera.transform.position = new Vector3(width / 2f, Mathf.Max(width, depth), -depth * 0.4f);
                camera.transform.LookAt(new Vector3(width / 2f, 0f, depth / 2f));
            }
            if (Object.FindFirstObjectByType<Light>() == null)
            {
                var light = new GameObject("Directional Light").AddComponent<Light>();
                light.type = LightType.Directional;
                light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
            }
        }
    }
}
