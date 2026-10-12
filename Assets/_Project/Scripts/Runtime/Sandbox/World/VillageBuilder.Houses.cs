using Specimen.Core.Gathering;
using Specimen.Core.World;
using UnityEngine;
using M = Specimen.Runtime.Sandbox.World.WorldMaterials;

namespace Specimen.Runtime.Sandbox.World
{
    /// <summary>Casas (prefab modular o greybox con tejado) y nodos de recolección del poblado.</summary>
    public sealed partial class VillageBuilder
    {
        private static readonly Color Plaster = new Color(0.85f, 0.8f, 0.68f);
        private static readonly Color Brick = new Color(0.62f, 0.38f, 0.3f);
        private static readonly Color Roof = new Color(0.55f, 0.22f, 0.16f);

        private void BuildHouses()
        {
            foreach (var house in Layout.Houses)
            {
                var prefab = Art != null ? Art.HousePrefab(house.Template) : null;
                var rotation = Quaternion.Euler(0f, house.Yaw, 0f);
                var go = prefab != null
                    ? Object.Instantiate(prefab, new Vector3(house.X, 0f, house.Z), rotation, _root)
                    : GreyboxHouse(house, rotation);
                go.name = house.Id;
            }
        }

        private GameObject GreyboxHouse(HouseSpot house, Quaternion rotation)
        {
            var root = new GameObject();
            root.transform.SetParent(_root, false);
            root.transform.SetPositionAndRotation(new Vector3(house.X, 0f, house.Z), rotation);
            var walls = house.Style == HouseStyle.Yeso ? Plaster : Brick;
            M.Shape(PrimitiveType.Cube, root.transform, new Vector3(0f, 1.5f, 0f), new Vector3(house.Width, 3f, house.Depth),
                walls, true);
            var roof = M.Shape(PrimitiveType.Cube, root.transform, new Vector3(0f, 3.4f, 0f),
                new Vector3(house.Width * 0.75f, house.Width * 0.75f, house.Depth + 0.6f), Roof);
            roof.transform.localRotation = Quaternion.Euler(0f, 0f, 45f);
            M.Shape(PrimitiveType.Cube, root.transform, new Vector3(0f, 1f, house.Depth / 2f + 0.01f),
                new Vector3(1.1f, 2f, 0.05f), new Color(0.3f, 0.2f, 0.12f));
            return root;
        }

        private void BuildNature()
        {
            var nature = new GameObject("Naturaleza").transform;
            nature.SetParent(_root, false);
            foreach (var (kind, spot) in Layout.Nodes)
            {
                var go = new GameObject($"{GatheringRules.NameOf(kind)}");
                go.transform.SetParent(nature, false);
                go.transform.position = new Vector3(spot.X, 0f, spot.Z);
                var zone = Layout.ZoneAt(spot.X, spot.Z);
                bool ash = zone != null && zone.Name.StartsWith("Bosque");
                go.AddComponent<GatherNode>().Init(kind, _context, ash);
            }
        }
    }
}
