using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Medidas de modelos instanciados (bounds de sus renderers) y ajustes de escala.</summary>
    public static class ModelFitting
    {
        public static Bounds WorldBounds(GameObject root)
        {
            var renderers = root.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0) return new Bounds(root.transform.position, Vector3.zero);
            var bounds = renderers[0].bounds;
            foreach (var r in renderers) bounds.Encapsulate(r.bounds);
            return bounds;
        }

        /// <summary>Si la altura está claramente fuera de escala (p. ej. centímetros), la lleva a <paramref name="height"/>.</summary>
        public static void NormalizeHeight(GameObject model, float height, float tolerance = 0.5f)
        {
            float current = WorldBounds(model).size.y;
            if (current <= 0f) return;
            float ratio = height / current;
            if (ratio > 1f - tolerance && ratio < 1f + tolerance) return;
            model.transform.localScale *= ratio;
        }

        /// <summary>Escala no uniforme para que el modelo ocupe exactamente <paramref name="size"/> (m).</summary>
        public static void FitToSize(GameObject model, Vector3 size)
        {
            var current = WorldBounds(model).size;
            var s = model.transform.localScale;
            model.transform.localScale = new Vector3(
                Ratio(size.x, current.x) * s.x, Ratio(size.y, current.y) * s.y, Ratio(size.z, current.z) * s.z);
        }

        /// <summary>Mueve el modelo para que el mínimo de sus bounds quede en <paramref name="corner"/> (local del padre).</summary>
        public static void AlignMinTo(GameObject model, Vector3 corner, bool centerXZ)
        {
            var bounds = WorldBounds(model);
            var parent = model.transform.parent;
            var anchor = centerXZ ? new Vector3(bounds.center.x, bounds.min.y, bounds.center.z) : bounds.min;
            var target = parent != null ? parent.TransformPoint(corner) : corner;
            model.transform.position += target - anchor;
        }

        private static float Ratio(float wanted, float current) => current > 1e-4f ? wanted / current : 1f;
    }
}
