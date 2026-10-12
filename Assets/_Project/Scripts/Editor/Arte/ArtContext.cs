using Specimen.Core.Animation;
using Specimen.Core.Classes;
using Specimen.Runtime.Visuals;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Estado compartido por los pasos del generador: catálogo de arte, clips y controladores.</summary>
    public sealed class ArtContext
    {
        private ArtContext(CatalogoArte catalog, ClipIndex clips)
        {
            Catalog = catalog;
            Clips = clips;
        }

        public CatalogoArte Catalog { get; }
        public ClipIndex Clips { get; }
        public AnimatorController Hero { get; private set; }
        public AnimatorController Monster { get; private set; }

        public static ArtContext Create()
        {
            var clips = new ClipIndex(ArtPaths.Animations3P, ArtPaths.KevinIglesias);
            return new ArtContext(LoadOrCreateCatalog(), clips);
        }

        public void BuildControllers()
        {
            Hero = AnimatorBuilder.Build(DefaultAnimationSets.Heroe(), Clips);
            Monster = AnimatorBuilder.Build(DefaultAnimationSets.Monstruo(), Clips);
            Catalog.heroe = Hero;
            Catalog.monstruo = Monster;
        }

        /// <summary>Aplica los decoradores de personaje (armas, equipo) a un personaje recién montado.</summary>
        public void Decorate(ClassKitId kit, GameObject character)
        {
            foreach (var decorator in ArtSteps.CharacterDecorators) decorator.Decorate(kit, character);
        }

        private static CatalogoArte LoadOrCreateCatalog()
        {
            var catalog = AssetDatabase.LoadAssetAtPath<CatalogoArte>(ArtPaths.ArtCatalog);
            if (catalog != null) return catalog;
            ArtFolders.Ensure(ArtPaths.ArtCatalog.Substring(0, ArtPaths.ArtCatalog.LastIndexOf('/')));
            catalog = ScriptableObject.CreateInstance<CatalogoArte>();
            AssetDatabase.CreateAsset(catalog, ArtPaths.ArtCatalog);
            return catalog;
        }
    }
}
