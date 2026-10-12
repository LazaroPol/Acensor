using UnityEngine;

namespace Specimen.Runtime.Sandbox
{
    /// <summary>Instancia el prefab de arte o, si falta (arte sin generar), una cápsula greybox con el pivote en los pies.</summary>
    public static class VisualFactory
    {
        public static GameObject Character(GameObject prefab, Transform parent, Color fallback, float height)
        {
            if (prefab != null)
            {
                var visual = Object.Instantiate(prefab, parent, false);
                foreach (var collider in visual.GetComponentsInChildren<Collider>()) Object.Destroy(collider);
                return visual;
            }
            var root = new GameObject("Greybox");
            root.transform.SetParent(parent, false);
            var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            Object.Destroy(capsule.GetComponent<Collider>());
            capsule.transform.SetParent(root.transform, false);
            capsule.transform.localPosition = Vector3.up * (height / 2f);
            capsule.transform.localScale = new Vector3(0.7f, height / 2f, 0.7f);
            capsule.GetComponent<Renderer>().material.color = fallback;
            return root;
        }

        /// <summary>Prop con colisión (banco, decorado): prefab o caja greybox del tamaño indicado.</summary>
        public static GameObject Prop(GameObject prefab, Vector3 position, float yaw, Vector3 fallbackSize, Color fallback)
        {
            var rotation = Quaternion.Euler(0f, yaw, 0f);
            if (prefab != null) return Object.Instantiate(prefab, position, rotation);
            var box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            box.transform.SetPositionAndRotation(position + Vector3.up * (fallbackSize.y / 2f), rotation);
            box.transform.localScale = fallbackSize;
            box.GetComponent<Renderer>().material.color = fallback;
            return box;
        }
    }
}
