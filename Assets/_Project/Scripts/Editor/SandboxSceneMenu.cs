using System.Linq;
using Specimen.Editor.Arte;
using Specimen.Runtime.Sandbox;
using Specimen.Runtime.Visuals;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Specimen.Editor
{
    /// <summary>
    /// Menús del Campo de pruebas: crear la escena (con CatalogoVisual y CatalogoArte enlazados) y
    /// «Probar»: genera el arte si falta, crea la escena y entra en Play.
    /// </summary>
    public static class SandboxSceneMenu
    {
        private const string ScenePath = "Assets/_Project/Scenes/CampoPruebas/" + SandboxBootstrap.SceneName + ".unity";

        [MenuItem("SPECIMEN/Crear escena Campo de pruebas")]
        public static void Create()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            var bootstrap = new GameObject(nameof(SandboxBootstrap)).AddComponent<SandboxBootstrap>();
            var serialized = new SerializedObject(bootstrap);
            serialized.FindProperty("catalogo").objectReferenceValue = VisualCatalogWriter.LoadOrCreate();
            serialized.FindProperty("arte").objectReferenceValue =
                AssetDatabase.LoadAssetAtPath<CatalogoArte>(ArtPaths.ArtCatalog);
            serialized.ApplyModifiedPropertiesWithoutUndo();
            ArtFolders.Ensure(ScenePath.Substring(0, ScenePath.LastIndexOf('/')));
            EditorSceneManager.SaveScene(scene, ScenePath);
            AddToBuildSettings();
            Debug.Log($"[SPECIMEN] Escena creada en {ScenePath}. Pulsa Play.");
        }

        [MenuItem("SPECIMEN/▶ Probar Campo de pruebas")]
        public static void CreateAndPlay()
        {
            if (AssetDatabase.LoadAssetAtPath<CatalogoArte>(ArtPaths.ArtCatalog) == null) ArtGenerator.GenerateAll();
            Create();
            EditorApplication.EnterPlaymode();
        }

        private static void AddToBuildSettings()
        {
            var scenes = EditorBuildSettings.scenes.ToList();
            if (scenes.Any(s => s.path == ScenePath)) return;
            scenes.Add(new EditorBuildSettingsScene(ScenePath, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
