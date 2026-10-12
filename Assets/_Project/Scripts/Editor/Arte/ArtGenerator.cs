using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Menú «SPECIMEN › Arte › 2 · Generar prefabs y catálogo»: a partir de Art/ThirdParty crea
    /// controladores de animación, prefabs (personajes, armas, enemigos, entorno, mobiliario) y
    /// rellena CatalogoArte y CatalogoVisual. Se puede repetir: sobrescribe lo generado.
    /// </summary>
    public static class ArtGenerator
    {
        [MenuItem("SPECIMEN/Arte/2 · Generar prefabs y catálogo")]
        public static void GenerateAll()
        {
            if (!AssetDatabase.IsValidFolder(ArtPaths.ThirdParty))
            {
                Debug.LogError($"[SPECIMEN] Falta {ArtPaths.ThirdParty}: copia los packs (docs/arte/PACKS.md).");
                return;
            }
            var context = ArtContext.Create();
            Debug.Log($"[SPECIMEN] {context.Clips.Count} clips de animación indexados.");
            context.BuildControllers();
            foreach (var step in ArtSteps.All) Run(step, context);
            EditorUtility.SetDirty(context.Catalog);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[SPECIMEN] Arte generado. Abre o crea la escena Campo de pruebas y pulsa Play.");
        }

        private static void Run(IArtStep step, ArtContext context)
        {
            try
            {
                EditorUtility.DisplayProgressBar("SPECIMEN · Arte", step.Name, 0.5f);
                step.Run(context);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SPECIMEN] Falló el paso «{step.Name}»: {e}");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }
    }
}
