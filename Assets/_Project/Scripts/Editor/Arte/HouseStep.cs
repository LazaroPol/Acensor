using System.Collections.Generic;
using Specimen.Core.World;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Paso «Casas»: monta cada plantilla de casa del poblado («Casa_4x6_Yeso»…) con las piezas modulares del
    /// pack medieval según <see cref="HouseBlueprint"/>. Cada pieza se encaja por sus bounds (módulo de 2 m),
    /// así no dependemos del pivote ni de la escala de origen. Una sola BoxCollider por casa.
    /// </summary>
    public sealed class HouseStep : IArtStep
    {
        private const float FloorTop = 0.08f;

        private readonly ModelLocator _models = new ModelLocator(ArtPaths.Village3P);
        private readonly TextureLocator _textures = new TextureLocator(ArtPaths.Village3P);

        public string Name => "Casas";

        public void Run(ArtContext context)
        {
            context.Catalog.casas.Clear();
            var done = new HashSet<string>();
            foreach (var house in new VillageLayout().Houses)
            {
                if (!done.Add(house.Template)) continue;
                var root = Build(house);
                if (root != null) context.Catalog.casas.Add(PrefabSaver.Save(root, ArtPaths.Environment + "/Casas"));
            }
        }

        private GameObject Build(HouseSpot house)
        {
            var root = new GameObject(house.Template);
            float wallTop = 0f;
            foreach (var piece in HouseBlueprint.For(house))
            {
                if (piece.Kind == HousePieceKind.Tejado) continue;
                var holder = Place(root, piece);
                if (holder == null) continue;
                if (piece.Kind == HousePieceKind.Muro) wallTop = Mathf.Max(wallTop, ModelFitting.WorldBounds(holder).max.y);
            }
            if (wallTop <= 0f)
            {
                Object.DestroyImmediate(root);
                Debug.LogWarning($"[SPECIMEN] Casas: faltan piezas del pack medieval para {house.Template}.");
                return null;
            }
            PlaceRoof(root, house, wallTop);
            MaterialRemapper.Apply(root, _textures, "Pueblo");
            var collider = root.AddComponent<BoxCollider>();
            collider.center = new Vector3(0f, wallTop / 2f, 0f);
            collider.size = new Vector3(house.Width, wallTop, house.Depth);
            return root;
        }

        private GameObject Place(GameObject root, HousePiece piece)
        {
            var wrapper = Wrapped(root, piece.Model, out var holder);
            if (wrapper == null) return null;
            var size = ModelFitting.WorldBounds(wrapper).size;
            bool wall = piece.Kind == HousePieceKind.Muro;
            if (wall && size.z > size.x) wrapper.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            wrapper.transform.localScale = Vector3.one * Ratio(HouseSpot.Module, Mathf.Max(size.x, size.z));
            float height = ModelFitting.WorldBounds(wrapper).size.y;
            ModelFitting.AlignMinTo(wrapper, new Vector3(0f, wall ? 0f : FloorTop - height, 0f), true);
            holder.transform.localPosition = new Vector3(piece.X, 0f, piece.Z);
            holder.transform.localRotation = Quaternion.Euler(0f, piece.Yaw, 0f);
            return holder;
        }

        private void PlaceRoof(GameObject root, HouseSpot house, float wallTop)
        {
            string model = HouseBlueprint.RoofFor(house.Width, house.Depth);
            var wrapper = Wrapped(root, model, out _);
            if (wrapper == null) return;
            var size = ModelFitting.WorldBounds(wrapper).size;
            bool wantsLongX = house.Width > house.Depth;
            if (house.Width != house.Depth && size.x > size.z != wantsLongX)
                wrapper.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            float longest = Mathf.Max(house.Width, house.Depth) + 0.6f;
            wrapper.transform.localScale = Vector3.one * Ratio(longest, Mathf.Max(size.x, size.z));
            ModelFitting.AlignMinTo(wrapper, new Vector3(0f, wallTop - 0.15f, 0f), true);
        }

        private GameObject Wrapped(GameObject root, string modelName, out GameObject holder)
        {
            holder = null;
            var asset = _models.Load(modelName);
            if (asset == null)
            {
                Debug.LogWarning($"[SPECIMEN] Casas: no encuentro la pieza «{modelName}».");
                return null;
            }
            holder = new GameObject(modelName);
            holder.transform.SetParent(root.transform, false);
            var model = Object.Instantiate(asset, holder.transform, false);
            model.name = "Modelo";
            return ModelFitting.Wrap(model);
        }

        private static float Ratio(float wanted, float current) => current > 1e-4f ? wanted / current : 1f;
    }
}
