using System.Collections.Generic;
using UnityEngine;

namespace Specimen.Runtime.Sandbox.World
{
    /// <summary>Materiales URP Lit de color liso (cacheados por color) para el suelo, caminos y naturaleza procedural.</summary>
    public static class WorldMaterials
    {
        private static readonly Dictionary<Color, Material> Cache = new Dictionary<Color, Material>();

        public static Material Lit(Color color, float smoothness = 0.1f)
        {
            if (Cache.TryGetValue(color, out var material) && material != null) return material;
            var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
            material = new Material(shader) { color = color };
            if (material.HasProperty("_BaseColor")) material.SetColor("_BaseColor", color);
            if (material.HasProperty("_Smoothness")) material.SetFloat("_Smoothness", smoothness);
            Cache[color] = material;
            return material;
        }

        /// <summary>Primitiva coloreada hija de <paramref name="parent"/>; sin colisión salvo que se pida.</summary>
        public static GameObject Shape(PrimitiveType type, Transform parent, Vector3 position, Vector3 scale, Color color,
            bool collider = false)
        {
            var go = GameObject.CreatePrimitive(type);
            if (!collider) Object.Destroy(go.GetComponent<Collider>());
            go.transform.SetParent(parent, false);
            go.transform.localPosition = position;
            go.transform.localScale = scale;
            go.GetComponent<Renderer>().sharedMaterial = Lit(color);
            return go;
        }
    }
}
