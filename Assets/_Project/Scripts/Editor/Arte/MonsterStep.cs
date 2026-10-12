using System.Collections.Generic;
using Specimen.Core.Bestiary;
using Specimen.Runtime.Characters;
using Specimen.Runtime.Visuals;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>Paso «Bestiario»: un prefab por criatura (modelo, variante de color, escala y Monstruo.controller).</summary>
    public sealed class MonsterStep : IArtStep
    {
        private const float BaseHeight = 1.4f;

        public string Name => "Bestiario";

        public void Run(ArtContext context)
        {
            var models = new ModelLocator(ArtPaths.Bestiary3P);
            var textures = new TextureLocator(ArtPaths.Bestiary3P);
            context.Catalog.enemigos.Clear();
            foreach (var entry in DefaultBestiary.All())
            {
                var asset = models.Load(entry.ModelName);
                if (asset == null) continue;
                var root = new GameObject($"Enemigo_{entry.Id}");
                var model = Object.Instantiate(asset, root.transform, false);
                model.name = "Modelo";
                FacingFixer.FaceForward(model);
                var preferred = new Dictionary<string, string> { [entry.MaterialKey] = entry.BaseColorTexture };
                MaterialRemapper.Apply(root, textures, $"{entry.ModelName}_{entry.Variant}", preferred);
                ModelFitting.NormalizeHeight(model, BaseHeight);
                CharacterAssembler.SetupAnimator(model, models.AvatarOf(entry.ModelName), context.Monster);
                model.transform.localScale *= entry.Scale;
                root.AddComponent<CharacterAnimator>();
                var prefab = PrefabSaver.Save(root, ArtPaths.Enemies);
                context.Catalog.enemigos.Add(new CatalogoArte.Enemigo { id = entry.Id, prefab = prefab });
            }
        }
    }
}
