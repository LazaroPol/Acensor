using System.Linq;
using Specimen.Runtime.Prototype;
using Specimen.Runtime.Visuals;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Specimen.Editor
{
    /// <summary>
    /// Menú «SPECIMEN › Crear escena Prototipo»: crea (o reutiliza) el catálogo visual vacío y la
    /// escena con su bootstrap, y la añade a Build Settings. Evita versionar YAML de escena a mano.
    /// </summary>
    public static class PrototypeSceneMenu
    {
        private const string ScenePath = "Assets/_Project/Scenes/Prototipo/Prototipo.unity";
        private const string CatalogPath = "Assets/_Project/Data/Visuals/CatalogoVisual.asset";

        [MenuItem("SPECIMEN/Crear escena Prototipo")]
        public static void Create()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            var catalog = LoadOrCreateCatalog();
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            var bootstrap = new GameObject(nameof(PrototypeBootstrap)).AddComponent<PrototypeBootstrap>();
            var serialized = new SerializedObject(bootstrap);
            serialized.FindProperty("catalogo").objectReferenceValue = catalog;
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EnsureFolder("Assets/_Project/Scenes", "Prototipo");
            EditorSceneManager.SaveScene(scene, ScenePath);
            AddToBuildSettings();
            Debug.Log($"[SPECIMEN] Escena creada en {ScenePath}. Pulsa Play.");
        }

        private static CatalogoVisual LoadOrCreateCatalog()
        {
            var catalog = AssetDatabase.LoadAssetAtPath<CatalogoVisual>(CatalogPath);
            if (catalog != null) return catalog;
            EnsureFolder("Assets/_Project/Data", "Visuals");
            catalog = ScriptableObject.CreateInstance<CatalogoVisual>();
            AssetDatabase.CreateAsset(catalog, CatalogPath);
            AssetDatabase.SaveAssets();
            return catalog;
        }

        private static void EnsureFolder(string parent, string name)
        {
            if (!AssetDatabase.IsValidFolder($"{parent}/{name}"))
                AssetDatabase.CreateFolder(parent, name);
        }

        private static void AddToBuildSettings()
        {
            var scenes = EditorBuildSettings.scenes.ToList();
            if (scenes.Any(s => s.path == ScenePath)) return;
            scenes.Insert(0, new EditorBuildSettingsScene(ScenePath, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
