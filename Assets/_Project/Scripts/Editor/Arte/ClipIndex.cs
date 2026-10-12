using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Índice de los clips de animación de unas carpetas (por nombre de clip, nombre sin «Armature|»
    /// y nombre de fichero). Ignora las variantes con desplazamiento de raíz («[RM]», «_RM»).
    /// </summary>
    public sealed class ClipIndex
    {
        private readonly Dictionary<string, AnimationClip> _clips =
            new Dictionary<string, AnimationClip>(StringComparer.OrdinalIgnoreCase);

        public ClipIndex(params string[] folders)
        {
            foreach (string folder in folders.Where(AssetDatabase.IsValidFolder))
                foreach (string guid in AssetDatabase.FindAssets("t:Model", new[] { folder }))
                    IndexModel(AssetDatabase.GUIDToAssetPath(guid));
        }

        public int Count => _clips.Count;

        public AnimationClip Find(IEnumerable<string> candidates)
        {
            foreach (string name in candidates)
                if (_clips.TryGetValue(name, out var clip)) return clip;
            return null;
        }

        private void IndexModel(string path)
        {
            string file = Path.GetFileNameWithoutExtension(path);
            if (file.Contains("[RM]") || file.EndsWith("_RM", StringComparison.OrdinalIgnoreCase)) return;
            foreach (var clip in AssetDatabase.LoadAllAssetsAtPath(path).OfType<AnimationClip>())
            {
                if (clip.name.StartsWith("__preview__", StringComparison.Ordinal)) continue;
                Add(clip.name, clip);
                Add(clip.name.Substring(clip.name.LastIndexOf('|') + 1), clip);
                Add(file, clip);
            }
        }

        private void Add(string key, AnimationClip clip)
        {
            if (!_clips.ContainsKey(key)) _clips[key] = clip;
        }
    }
}
