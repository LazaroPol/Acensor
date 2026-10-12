using System.Collections.Generic;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Acopla las mallas de otro modelo (atuendo, peinado) al esqueleto de un personaje: reasigna los
    /// huesos por nombre y mueve los renderers al personaje. Todos los packs de Quaternius comparten nombres.
    /// </summary>
    public static class SkeletonMerger
    {
        public static void Merge(GameObject target, GameObject sourceAsset)
        {
            if (sourceAsset == null) return;
            var bones = BonesByName(target.transform);
            var source = Object.Instantiate(sourceAsset);
            foreach (var skinned in source.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                var mapped = new Transform[skinned.bones.Length];
                for (int i = 0; i < mapped.Length; i++) mapped[i] = Map(bones, skinned.bones[i]);
                skinned.bones = mapped;
                skinned.rootBone = Map(bones, skinned.rootBone);
                skinned.transform.SetParent(target.transform, false);
            }
            foreach (var mesh in source.GetComponentsInChildren<MeshFilter>(true))
            {
                var bone = Map(bones, mesh.transform.parent);
                if (bone != null) mesh.transform.SetParent(bone, false);
            }
            Object.DestroyImmediate(source);
        }

        public static Dictionary<string, Transform> BonesByName(Transform root)
        {
            var result = new Dictionary<string, Transform>();
            foreach (var t in root.GetComponentsInChildren<Transform>(true))
                if (!result.ContainsKey(t.name)) result[t.name] = t;
            return result;
        }

        private static Transform Map(Dictionary<string, Transform> bones, Transform bone)
            => bone != null && bones.TryGetValue(bone.name, out var mapped) ? mapped : null;
    }
}
