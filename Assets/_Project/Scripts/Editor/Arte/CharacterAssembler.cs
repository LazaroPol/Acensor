using System.Collections.Generic;
using Specimen.Core.Appearance;
using Specimen.Runtime.Characters;
using UnityEditor.Animations;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Monta un personaje desde una apariencia: modelo base (esqueleto + Animator Humanoid), atuendo y
    /// peinados acoplados, materiales URP con la variante de textura y el controlador de animación.
    /// </summary>
    public sealed class CharacterAssembler
    {
        public const float Height = 1.8f;

        private readonly ModelLocator _models;
        private readonly TextureLocator _textures;

        public CharacterAssembler(ModelLocator models, TextureLocator textures)
        {
            _models = models;
            _textures = textures;
        }

        public GameObject Assemble(string name, CharacterAppearance look, AnimatorController controller)
        {
            string baseName = AppearanceModels.BaseModel(look);
            var baseAsset = _models.Load(baseName);
            if (baseAsset == null) return null;
            var root = new GameObject(name);
            var model = Object.Instantiate(baseAsset, root.transform, false);
            model.name = "Modelo";
            string outfit = AppearanceModels.OutfitModel(look);
            if (outfit != null) SkeletonMerger.Merge(model, _models.Load(outfit));
            foreach (string hair in look.Hair) SkeletonMerger.Merge(model, _models.Load(hair));
            if (FacingFixer.FaceForward(model)) Debug.Log($"[SPECIMEN] {name}: modelo girado 180° para mirar a +Z.");
            MaterialRemapper.Apply(root, _textures, baseName, Preferred(look));
            SetupAnimator(model, _models.AvatarOf(baseName), controller);
            ModelFitting.NormalizeHeight(model, Height);
            root.AddComponent<CharacterAnimator>();
            return root;
        }

        public static void SetupAnimator(GameObject model, Avatar avatar, AnimatorController controller)
        {
            var animator = model.GetComponent<Animator>();
            if (animator == null) animator = model.AddComponent<Animator>();
            if (animator.avatar == null) animator.avatar = avatar;
            animator.runtimeAnimatorController = controller;
            animator.applyRootMotion = false;
            animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            foreach (var skinned in model.GetComponentsInChildren<SkinnedMeshRenderer>(true)) skinned.updateWhenOffscreen = true;
        }

        private static IReadOnlyDictionary<string, string> Preferred(CharacterAppearance look)
        {
            var result = new Dictionary<string, string>();
            string key = AppearanceModels.OutfitMaterialKey(look);
            string texture = AppearanceModels.OutfitTexture(look);
            if (key != null && texture != null) result[key] = texture;
            return result;
        }
    }
}
