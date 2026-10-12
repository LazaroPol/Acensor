using Specimen.Core.Animation;
using UnityEditor;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Reglas de importación de Art/ThirdParty: personajes, atuendos, bestiario y librerías de animación
    /// como Humanoid (todos comparten el esqueleto de Quaternius), clips «_Loop» en bucle y sin
    /// desplazamiento de raíz, mapas normales y de rugosidad como datos lineales.
    /// </summary>
    public sealed class ThirdPartyImportRules : AssetPostprocessor
    {
        // Sube este número si cambias las reglas: Unity reimporta los modelos afectados.
        public override uint GetVersion() => 5;

        private void OnPreprocessModel()
        {
            if (!ArtPaths.IsThirdParty(assetPath)) return;
            var importer = (ModelImporter)assetImporter;
            importer.materialImportMode = ModelImporterMaterialImportMode.ImportViaMaterialDescription;
            importer.materialSearch = ModelImporterMaterialSearch.RecursiveUp;
            if (ArtPaths.IsHumanoidSource(assetPath))
            {
                importer.animationType = ModelImporterAnimationType.Human;
                importer.avatarSetup = ModelImporterAvatarSetup.CreateFromThisModel;
                importer.importAnimation = ArtPaths.IsAnimationLibrary(assetPath);
                return;
            }
            bool skinned = assetPath.Contains("/Pelo/") || assetPath.Contains("/Partes/") || assetPath.EndsWith("_Peasant.fbx");
            importer.animationType = skinned ? ModelImporterAnimationType.Generic : ModelImporterAnimationType.None;
            importer.importAnimation = false;
        }

        private void OnPreprocessAnimation()
        {
            if (!ArtPaths.IsAnimationLibrary(assetPath)) return;
            var importer = (ModelImporter)assetImporter;
            var clips = importer.defaultClipAnimations;
            foreach (var clip in clips)
            {
                clip.loopTime = ClipLoopRule.IsLooping(clip.name);
                clip.lockRootRotation = true;
                clip.lockRootHeightY = true;
                clip.lockRootPositionXZ = true;
                // Orientación «Body Orientation» (no «Original»): la raíz de los FBX de Blender viene girada
                // 180° y con «Original» el cuerpo se horneaba mirando hacia atrás (andaban de espaldas).
                clip.keepOriginalOrientation = false;
                clip.keepOriginalPositionY = true;
                clip.keepOriginalPositionXZ = false;
            }
            importer.clipAnimations = clips;
        }

        private void OnPreprocessTexture()
        {
            if (!ArtPaths.IsThirdParty(assetPath)) return;
            var importer = (TextureImporter)assetImporter;
            if (assetPath.Contains("_Normal")) importer.textureType = TextureImporterType.NormalMap;
            else if (assetPath.Contains("_ORM") || assetPath.Contains("_Roughness")) importer.sRGBTexture = false;
        }
    }
}
