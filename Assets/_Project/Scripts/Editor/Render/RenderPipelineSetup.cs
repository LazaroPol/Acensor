using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Specimen.Editor.Render
{
    /// <summary>
    /// Garantiza que el proyecto renderiza con URP: si Graphics Settings no tiene pipeline asignado
    /// (Unity usa entonces el Built-in y todo material URP sale magenta), crea el asset de URP con su
    /// renderer en Assets/_Project/Settings y lo asigna. Se ejecuta solo al abrir el editor.
    /// </summary>
    [InitializeOnLoad]
    public static class RenderPipelineSetup
    {
        public const string Folder = "Assets/_Project/Settings";
        public const string AssetPath = Folder + "/SPECIMEN_URP.asset";
        public const string RendererPath = Folder + "/SPECIMEN_URP_Renderer.asset";

        static RenderPipelineSetup() => EditorApplication.delayCall += EnsureOnLoad;

        private static void EnsureOnLoad()
        {
            if (GraphicsSettings.defaultRenderPipeline != null) return;
            Ensure();
        }

        [MenuItem("SPECIMEN/Arte/0 · Activar URP (render del proyecto)")]
        public static void Ensure()
        {
            var asset = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(AssetPath) ?? Create();
            GraphicsSettings.defaultRenderPipeline = asset;
            for (int i = 0; i < QualitySettings.names.Length; i++)
            {
                var level = QualitySettings.GetRenderPipelineAssetAt(i);
                if (level == null || !(level is UniversalRenderPipelineAsset))
                    SetQualityPipeline(i, asset);
            }
            AssetDatabase.SaveAssets();
            Debug.Log($"[SPECIMEN] URP activo ({AssetPath}). Reimporta el arte si venía del pipeline Built-in.");
        }

        private static UniversalRenderPipelineAsset Create()
        {
            if (!AssetDatabase.IsValidFolder(Folder)) AssetDatabase.CreateFolder("Assets/_Project", "Settings");
            var renderer = ScriptableObject.CreateInstance<UniversalRendererData>();
            AssetDatabase.CreateAsset(renderer, RendererPath);
            var asset = UniversalRenderPipelineAsset.Create(renderer);
            asset.supportsHDR = true;
            asset.msaaSampleCount = 4;
            asset.shadowDistance = 60f;
            asset.renderScale = 1f;
            AssetDatabase.CreateAsset(asset, AssetPath);
            return asset;
        }

        private static void SetQualityPipeline(int level, RenderPipelineAsset asset)
        {
            int current = QualitySettings.GetQualityLevel();
            QualitySettings.SetQualityLevel(level, false);
            QualitySettings.renderPipeline = asset;
            QualitySettings.SetQualityLevel(current, false);
        }
    }
}
