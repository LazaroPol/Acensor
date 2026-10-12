using System.Collections.Generic;
using Specimen.Core.Visuals.Packs;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Sustituye los materiales importados de un modelo instanciado por materiales URP con las texturas
    /// del pack (convención de <see cref="MaterialTextureNames"/>). Si no encuentra textura, deja el original.
    /// </summary>
    public static class MaterialRemapper
    {
        public static void Apply(GameObject root, TextureLocator textures, string prefix,
            IReadOnlyDictionary<string, string> preferred = null)
        {
            foreach (var renderer in root.GetComponentsInChildren<Renderer>(true))
            {
                var materials = renderer.sharedMaterials;
                for (int i = 0; i < materials.Length; i++)
                    materials[i] = Resolve(materials[i], textures, prefix, preferred) ?? materials[i];
                renderer.sharedMaterials = materials;
            }
        }

        private static Material Resolve(Material source, TextureLocator textures, string prefix,
            IReadOnlyDictionary<string, string> preferred)
        {
            if (source == null) return null;
            string key = MaterialTextureNames.KeyOf(source.name);
            string wanted = null;
            if (preferred != null) preferred.TryGetValue(key, out wanted);
            var baseMap = textures.FindFirst(MaterialTextureNames.BaseColorCandidates(source.name, wanted));
            if (baseMap == null) return null;
            return MaterialFactory.Textured($"{prefix}_{baseMap.name}", baseMap,
                textures.Find(MaterialTextureNames.NormalOf(source.name)),
                textures.Find(MaterialTextureNames.EmissiveOf(source.name)));
        }
    }
}
