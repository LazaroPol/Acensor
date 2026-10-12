using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Guarda un objeto temporal de la escena como prefab (sobrescribe) y lo destruye.</summary>
    public static class PrefabSaver
    {
        public static GameObject Save(GameObject instance, string folder)
        {
            if (instance == null) return null;
            ArtFolders.Ensure(folder);
            string path = $"{folder}/{instance.name}.prefab";
            var prefab = PrefabUtility.SaveAsPrefabAsset(instance, path);
            Object.DestroyImmediate(instance);
            return prefab;
        }
    }
}
