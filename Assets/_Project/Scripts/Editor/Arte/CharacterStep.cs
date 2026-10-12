using System;
using Specimen.Core.Appearance;
using Specimen.Core.Classes;
using Specimen.Runtime.Visuals;

namespace Specimen.Editor.Arte
{
    /// <summary>Paso «Personajes por clase»: un prefab por kit en Art/Generado/Personajes.</summary>
    public sealed class CharacterStep : IArtStep
    {
        public string Name => "Personajes por clase";

        public void Run(ArtContext context)
        {
            var models = new ModelLocator(ArtPaths.Outfits3P, ArtPaths.Characters3P);
            var textures = new TextureLocator(ArtPaths.Outfits3P, ArtPaths.Characters3P);
            var assembler = new CharacterAssembler(models, textures);
            context.Catalog.clases.Clear();
            foreach (ClassKitId kit in Enum.GetValues(typeof(ClassKitId)))
            {
                var instance = assembler.Assemble($"Personaje_{kit}", ClassAppearances.For(kit), context.HeroFor(kit));
                if (instance != null) context.Decorate(kit, instance);
                var prefab = PrefabSaver.Save(instance, ArtPaths.Characters);
                if (prefab != null) context.Catalog.clases.Add(new CatalogoArte.Clase { kit = kit, prefab = prefab });
            }
        }
    }
}
