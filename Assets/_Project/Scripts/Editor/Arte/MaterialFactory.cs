using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Materiales URP Lit generados (Art/Generado/Materiales); se reutilizan si ya existen.</summary>
    public static class MaterialFactory
    {
        public static Material Textured(string name, Texture2D baseMap, Texture2D normal, Texture2D emissive)
        {
            var mat = LoadOrCreate(name);
            mat.SetTexture("_BaseMap", baseMap);
            mat.SetColor("_BaseColor", Color.white);
            mat.SetFloat("_Smoothness", 0.15f);
            if (normal != null)
            {
                mat.SetTexture("_BumpMap", normal);
                mat.EnableKeyword("_NORMALMAP");
            }
            if (emissive != null) SetEmission(mat, emissive, Color.white);
            EditorUtility.SetDirty(mat);
            return mat;
        }

        /// <summary>Color liso, opcionalmente emisivo (proyectiles de hechizo, marcadores).</summary>
        public static Material Solid(string name, Color color, bool glow)
        {
            var mat = LoadOrCreate(name);
            mat.SetColor("_BaseColor", color);
            if (glow) SetEmission(mat, null, color * 2.5f);
            EditorUtility.SetDirty(mat);
            return mat;
        }

        private static void SetEmission(Material mat, Texture2D map, Color color)
        {
            mat.SetTexture("_EmissionMap", map);
            mat.SetColor("_EmissionColor", color);
            mat.EnableKeyword("_EMISSION");
            mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        }

        private static Material LoadOrCreate(string name)
        {
            ArtFolders.Ensure(ArtPaths.Materials);
            string path = $"{ArtPaths.Materials}/{name.Replace(' ', '_')}.mat";
            var mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat != null) return mat;
            mat = new Material(LitShader());
            AssetDatabase.CreateAsset(mat, path);
            return mat;
        }

        private static Shader LitShader()
            => Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
    }
}
