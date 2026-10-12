using System;
using Specimen.Core.Professions;
using Specimen.Runtime.Visuals;
using UnityEngine;

namespace Specimen.Editor.Arte
{
    /// <summary>
    /// Paso «Mobiliario de profesiones»: un banco por profesión (caja del pueblo como mesa) con sus
    /// herramientas KayKit repartidas encima. Prefabs en Art/Generado/Mobiliario.
    /// </summary>
    public sealed class FurnitureStep : IArtStep
    {
        private static readonly Vector3 BenchSize = new Vector3(1.4f, 0.8f, 0.7f);
        private const float ToolSize = 0.35f;

        private readonly ModelLocator _village = new ModelLocator(ArtPaths.Village3P);
        private readonly ModelLocator _tools = new ModelLocator(ArtPaths.Tools3P);
        private readonly TextureLocator _villageTextures = new TextureLocator(ArtPaths.Village3P);
        private readonly TextureLocator _toolTextures = new TextureLocator(ArtPaths.Tools3P);

        public string Name => "Mobiliario de profesiones";

        public void Run(ArtContext context)
        {
            context.Catalog.bancos.Clear();
            foreach (ProfessionId profession in Enum.GetValues(typeof(ProfessionId)))
            {
                var bench = Build(profession);
                var prefab = PrefabSaver.Save(bench, ArtPaths.Furniture);
                if (prefab != null)
                    context.Catalog.bancos.Add(new CatalogoArte.Banco { profesion = profession, prefab = prefab });
            }
        }

        private GameObject Build(ProfessionId profession)
        {
            var root = new GameObject($"Banco_{profession}");
            var table = Place(_village.Load(ProfessionFurniture.BenchBase), root, "Mesa");
            if (table != null)
            {
                MaterialRemapper.Apply(table, _villageTextures, "Pueblo");
                ModelFitting.FitToSize(table, BenchSize);
                ModelFitting.AlignMinTo(table, new Vector3(0f, 0f, 0f), true);
            }
            var tools = ProfessionFurniture.ToolsFor(profession);
            for (int i = 0; i < tools.Count; i++) PlaceTool(root, tools[i], i, tools.Count);
            var collider = root.AddComponent<BoxCollider>();
            collider.center = new Vector3(0f, BenchSize.y / 2f, 0f);
            collider.size = BenchSize;
            return root;
        }

        private void PlaceTool(GameObject root, string name, int index, int count)
        {
            var tool = Place(_tools.Load(name), root, name);
            if (tool == null) return;
            MaterialRemapper.Apply(tool, _toolTextures, "Herramientas");
            var size = ModelFitting.WorldBounds(tool).size;
            float biggest = Mathf.Max(size.x, Mathf.Max(size.y, size.z));
            if (biggest > 1e-4f) tool.transform.localScale *= ToolSize / biggest;
            float x = (index + 0.5f) / count * BenchSize.x - BenchSize.x / 2f;
            ModelFitting.AlignMinTo(tool, new Vector3(x, BenchSize.y, 0f), true);
        }

        private static GameObject Place(GameObject asset, GameObject parent, string name)
        {
            if (asset == null) return null;
            var instance = UnityEngine.Object.Instantiate(asset, parent.transform, false);
            instance.name = name;
            return instance;
        }
    }
}
