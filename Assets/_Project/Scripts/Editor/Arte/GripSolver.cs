using System.Collections.Generic;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Coloca un arma en la mano derecha a partir de la pose de reposo: el eje largo del arma sale por el
    /// lado del índice del puño (meñique → índice) y la empuñadura queda en la palma. Ajustable después
    /// moviendo el objeto «Arma» del prefab.
    /// </summary>
    public static class GripSolver
    {
        public static bool Place(GameObject weapon, Dictionary<string, Transform> bones, float length)
        {
            if (!bones.TryGetValue("hand_r", out var hand)) return false;
            if (!bones.TryGetValue("middle_01_r", out var middle)) return false;
            var index = Bone(bones, "index_01_r");
            var side = Bone(bones, "pinky_01_r") ?? Bone(bones, "ring_01_r");
            if (index == null || side == null) return false;

            var local = LocalBounds(weapon);
            int axis = LongestAxis(local.size);
            float scale = length / Mathf.Max(local.size[axis], 1e-4f);
            weapon.transform.localScale = Vector3.one * scale;
            bool positive = Mathf.Abs(local.max[axis]) >= Mathf.Abs(local.min[axis]);
            var bladeLocal = Axis(axis) * (positive ? 1f : -1f);
            float near = positive ? local.min[axis] : local.max[axis];
            var gripLocal = Mathf.Abs(near) < 0.25f * local.size[axis]
                ? Vector3.zero
                : Axis(axis) * (near + (positive ? 0.1f : -0.1f) * local.size[axis]);

            var bladeWorld = (index.position - side.position).normalized;
            var grip = Vector3.Lerp(hand.position, middle.position, 0.85f);
            var rotation = Quaternion.FromToRotation(bladeLocal, bladeWorld);
            weapon.transform.SetPositionAndRotation(grip - rotation * (gripLocal * scale), rotation);
            weapon.transform.SetParent(hand, true);
            return true;
        }

        private static Transform Bone(Dictionary<string, Transform> bones, string name)
            => bones.TryGetValue(name, out var bone) ? bone : null;

        private static Bounds LocalBounds(GameObject weapon)
        {
            var bounds = new Bounds();
            bool first = true;
            foreach (var filter in weapon.GetComponentsInChildren<MeshFilter>(true))
            {
                if (filter.sharedMesh == null) continue;
                var b = filter.sharedMesh.bounds;
                var toRoot = weapon.transform.worldToLocalMatrix * filter.transform.localToWorldMatrix;
                foreach (var corner in Corners(b))
                {
                    var p = toRoot.MultiplyPoint3x4(corner);
                    if (first) { bounds = new Bounds(p, Vector3.zero); first = false; }
                    else bounds.Encapsulate(p);
                }
            }
            return bounds;
        }

        private static IEnumerable<Vector3> Corners(Bounds b)
        {
            for (int i = 0; i < 8; i++)
                yield return new Vector3((i & 1) == 0 ? b.min.x : b.max.x, (i & 2) == 0 ? b.min.y : b.max.y,
                    (i & 4) == 0 ? b.min.z : b.max.z);
        }

        private static int LongestAxis(Vector3 size)
            => size.x >= size.y && size.x >= size.z ? 0 : size.y >= size.z ? 1 : 2;

        private static Vector3 Axis(int axis) => axis == 0 ? Vector3.right : axis == 1 ? Vector3.up : Vector3.forward;
    }
}
