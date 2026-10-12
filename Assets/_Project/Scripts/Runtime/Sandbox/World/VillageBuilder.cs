using Specimen.Core.World;
using Specimen.Runtime.Visuals;
using UnityEngine;
using M = Specimen.Runtime.Sandbox.World.WorldMaterials;

namespace Specimen.Runtime.Sandbox.World
{
    /// <summary>
    /// Levanta el poblado alrededor de la Armería a partir de <see cref="VillageLayout"/>: terreno, caminos
    /// y plaza, casas modulares, decorado, naturaleza recolectable y límites del mapa.
    /// </summary>
    public sealed partial class VillageBuilder
    {
        private static readonly Color Grass = new Color(0.27f, 0.36f, 0.18f);
        private static readonly Color AshGround = new Color(0.3f, 0.3f, 0.26f);
        private static readonly Color Quarry = new Color(0.42f, 0.38f, 0.32f);
        private static readonly Color Dirt = new Color(0.45f, 0.34f, 0.22f);
        private static readonly Color Cobble = new Color(0.5f, 0.48f, 0.44f);

        private readonly SandboxContext _context;
        private readonly Transform _root;

        public VillageBuilder(SandboxContext context)
        {
            _context = context;
            _root = new GameObject("Poblado").transform;
        }

        private VillageLayout Layout => _context.Village;
        private CatalogoArte Art => _context.Art;

        public void Build()
        {
            BuildGround();
            BuildPaths();
            BuildHouses();
            BuildProps();
            BuildNature();
            BuildBounds();
        }

        private void BuildGround()
        {
            float width = VillageLayout.MaxX - VillageLayout.MinX, depth = VillageLayout.MaxZ - VillageLayout.MinZ;
            var center = new Vector3((VillageLayout.MinX + VillageLayout.MaxX) / 2f, -0.25f,
                (VillageLayout.MinZ + VillageLayout.MaxZ) / 2f);
            M.Shape(PrimitiveType.Cube, _root, center, new Vector3(width + 30f, 0.5f, depth + 30f), Grass, true).name = "Terreno";
            foreach (var zone in Layout.Zones)
            {
                if (zone.Name.StartsWith("Bosque")) Patch(zone, AshGround);
                if (zone.Name.StartsWith("Cantera")) Patch(zone, Quarry);
            }
        }

        private void Patch(MapZone zone, Color color)
        {
            var size = new Vector3(zone.MaxX - zone.MinX, 0.02f, zone.MaxZ - zone.MinZ);
            M.Shape(PrimitiveType.Cube, _root, new Vector3(zone.CenterX, 0.005f, zone.CenterZ), size, color).name = zone.Name;
        }

        private void BuildPaths()
        {
            var plaza = Layout.ZoneAt(VillageLayout.PlazaX, VillageLayout.PlazaZ);
            foreach (var tile in Layout.Paths)
            {
                bool cobbled = plaza != null && plaza.Contains(tile.X, tile.Z);
                var go = M.Shape(PrimitiveType.Cube, _root, new Vector3(tile.X, 0.02f, tile.Z), new Vector3(2.05f, 0.03f, 2.05f),
                    cobbled ? Cobble : Dirt);
                go.name = "Camino";
            }
        }

        private void BuildProps()
        {
            foreach (var spot in Layout.Props)
            {
                var prefab = Art != null ? Art.DecorPrefab(spot.Id) : null;
                var go = VisualFactory.Prop(prefab, new Vector3(spot.X, 0f, spot.Z), spot.Yaw, new Vector3(1f, 1f, 1f),
                    new Color(0.5f, 0.38f, 0.25f));
                go.transform.SetParent(_root, true);
            }
        }

        /// <summary>Muros invisibles en el borde del mapa para no caer al vacío.</summary>
        private void BuildBounds()
        {
            float minX = VillageLayout.MinX, maxX = VillageLayout.MaxX, minZ = VillageLayout.MinZ, maxZ = VillageLayout.MaxZ;
            Wall(new Vector3(minX, 2f, (minZ + maxZ) / 2f), new Vector3(1f, 4f, maxZ - minZ));
            Wall(new Vector3(maxX, 2f, (minZ + maxZ) / 2f), new Vector3(1f, 4f, maxZ - minZ));
            Wall(new Vector3((minX + maxX) / 2f, 2f, minZ), new Vector3(maxX - minX, 4f, 1f));
            Wall(new Vector3((minX + maxX) / 2f, 2f, maxZ), new Vector3(maxX - minX, 4f, 1f));
        }

        private void Wall(Vector3 center, Vector3 size)
        {
            var go = new GameObject("Límite");
            go.transform.SetParent(_root, false);
            go.transform.position = center;
            go.AddComponent<BoxCollider>().size = size;
        }
    }
}
