using Specimen.Core.Visuals;
using Specimen.Runtime.Visuals;
using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Escribe entradas ID → prefab en el CatalogoVisual (lo crea si no existe) sin tocar las demás.</summary>
    public static class VisualCatalogWriter
    {
        public static CatalogoVisual LoadOrCreate()
        {
            var catalog = AssetDatabase.LoadAssetAtPath<CatalogoVisual>(ArtPaths.VisualCatalog);
            if (catalog != null) return catalog;
            ArtFolders.Ensure(ArtPaths.VisualCatalog.Substring(0, ArtPaths.VisualCatalog.LastIndexOf('/')));
            catalog = ScriptableObject.CreateInstance<CatalogoVisual>();
            AssetDatabase.CreateAsset(catalog, ArtPaths.VisualCatalog);
            return catalog;
        }

        public static void Assign(VisualId id, GameObject prefab)
        {
            if (prefab == null) return;
            var serialized = new SerializedObject(LoadOrCreate());
            var entries = serialized.FindProperty("entradas");
            var entry = Find(entries, id);
            if (entry == null)
            {
                entries.InsertArrayElementAtIndex(entries.arraySize);
                entry = entries.GetArrayElementAtIndex(entries.arraySize - 1);
                entry.FindPropertyRelative("id").intValue = (int)id;
            }
            entry.FindPropertyRelative("prefab").objectReferenceValue = prefab;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static SerializedProperty Find(SerializedProperty entries, VisualId id)
        {
            for (int i = 0; i < entries.arraySize; i++)
            {
                var entry = entries.GetArrayElementAtIndex(i);
                if (entry.FindPropertyRelative("id").intValue == (int)id) return entry;
            }
            return null;
        }
    }
}
