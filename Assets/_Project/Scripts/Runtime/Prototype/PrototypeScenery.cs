using UnityEngine;

namespace Specimen.Runtime.Prototype
{
    /// <summary>Luz mínima si la escena no la trae (la cámara la pone <see cref="PrototypePlayer"/>).</summary>
    public static class PrototypeScenery
    {
        public static void EnsureLight()
        {
            if (Object.FindAnyObjectByType<Light>() != null) return;
            var light = new GameObject("Directional Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        }
    }
}
