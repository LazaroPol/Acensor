using Specimen.Core.Visuals;
using Specimen.Core.Visuals.Packs;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Paso «Entorno»: piezas del pueblo ajustadas a la caja y pivote de su estilo greybox (así encajan en
    /// la cuadrícula sin tocar los layouts), decorado libre, y asignación en CatalogoVisual.
    /// </summary>
    public sealed class EnvironmentStep : IArtStep
    {
        private readonly ModelLocator _models = new ModelLocator(ArtPaths.Village3P);
        private readonly TextureLocator _textures = new TextureLocator(ArtPaths.Village3P);
        private readonly DefaultGreyboxStyles _styles = new DefaultGreyboxStyles();

        public string Name => "Entorno";

        public void Run(ArtContext context)
        {
            foreach (var id in EnvironmentKit.Fitted)
                VisualCatalogWriter.Assign(id, PrefabSaver.Save(Fitted(id), ArtPaths.Environment));
            context.Catalog.decorado.Clear();
            foreach (string name in EnvironmentKit.Decor)
            {
                var piece = Piece(name, $"Decor_{name}");
                if (piece != null) AddBoundsCollider(piece);
                if (piece != null) context.Catalog.decorado.Add(PrefabSaver.Save(piece, ArtPaths.Environment));
            }
        }

        private GameObject Fitted(VisualId id)
        {
            var root = Piece(EnvironmentKit.ModelFor(id), $"Entorno_{id}");
            if (root == null) return null;
            var model = root.transform.GetChild(0).gameObject;
            var style = _styles.For(EnvironmentKit.FitBoxOf(id));
            var size = new Vector3(style.Scale.X, style.Scale.Y, style.Scale.Z);
            var b = ModelFitting.WorldBounds(model).size;
            bool turn = size.x > size.z && b.z > b.x;
            ModelFitting.FitToSize(model, turn ? new Vector3(size.z, size.y, size.x) : size);
            if (turn) model.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            var center = GreyboxOffset.CenterOf(style, VisualAnchors.For(id));
            var min = new Vector3(center.X, center.Y, center.Z) - size / 2f;
            ModelFitting.AlignMinTo(model, min, false);
            var collider = root.AddComponent<BoxCollider>();
            collider.center = min + size / 2f;
            collider.size = size;
            collider.isTrigger = id == VisualId.Loot;
            return root;
        }

        private static void AddBoundsCollider(GameObject root)
        {
            var bounds = ModelFitting.WorldBounds(root);
            var collider = root.AddComponent<BoxCollider>();
            collider.center = bounds.center - root.transform.position;
            collider.size = bounds.size;
        }

        private GameObject Piece(string modelName, string name)
        {
            var asset = _models.Load(modelName);
            if (asset == null) return null;
            var root = new GameObject(name);
            var model = Object.Instantiate(asset, root.transform, false);
            model.name = "Modelo";
            MaterialRemapper.Apply(root, _textures, "Pueblo");
            return root;
        }
    }
}
