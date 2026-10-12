using System.Linq;
using Specimen.Core.Composition;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Specimen.Editor
{
    /// <summary>
    /// Menú «SPECIMEN › Crear escenas de zonas»: crea Ascensor, Armería y Laboratorio como escenas vacías
    /// (sus bootstraps de Runtime montan el contenido al hacer Play) y las añade a Build Settings.
    /// </summary>
    public static class ZoneScenesMenu
    {
        private const string Folder = "Assets/_Project/Scenes/Zonas";
        private static readonly SceneId[] Zones = { SceneId.Armeria, SceneId.Ascensor, SceneId.Laboratorio, SceneId.Incursion, SceneId.Atico };

        [MenuItem("SPECIMEN/Crear escenas de zonas")]
        public static void Create()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            if (!AssetDatabase.IsValidFolder(Folder)) AssetDatabase.CreateFolder("Assets/_Project/Scenes", "Zonas");
            foreach (var zone in Zones)
            {
                var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(scene, PathOf(zone));
            }
            AddToBuildSettings();
            EditorSceneManager.OpenScene(PathOf(SceneId.Ascensor));
        }

        private static string PathOf(SceneId zone) => $"{Folder}/{zone}.unity";

        private static void AddToBuildSettings()
        {
            var scenes = EditorBuildSettings.scenes.ToList();
            foreach (var zone in Zones.Where(z => scenes.All(s => s.path != PathOf(z))))
                scenes.Add(new EditorBuildSettingsScene(PathOf(zone), true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
