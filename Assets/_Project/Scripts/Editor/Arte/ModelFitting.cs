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

        /// <summary>
        /// Envuelve el modelo en «Ajuste» (sin rotación) y escala ese envoltorio para que el conjunto ocupe
        /// <paramref name="size"/> (m) en ejes del mundo. Escalar el propio modelo no sirve: los FBX de
        /// Blender traen la raíz girada -90° en X y la escala se aplicaría en ejes cruzados.
        /// </summary>
        public static GameObject FitToSize(GameObject model, Vector3 size)
        {
            var wrapper = Wrap(model);
            var current = WorldBounds(wrapper).size;
            wrapper.transform.localScale = new Vector3(
                Ratio(size.x, current.x), Ratio(size.y, current.y), Ratio(size.z, current.z));
            return wrapper;
        }

        public static GameObject Wrap(GameObject model)
        {
            var wrapper = new GameObject("Ajuste");
            wrapper.transform.SetParent(model.transform.parent, false);
            model.transform.SetParent(wrapper.transform, true);
            return wrapper;
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
