using Specimen.Core.Gathering;
using UnityEngine;
using M = Specimen.Runtime.Sandbox.World.WorldMaterials;

namespace Specimen.Runtime.Sandbox.World
{
    /// <summary>
    /// Naturaleza procedural del poblado (el pack medieval no trae árboles ni rocas): árboles de copa
    /// redondeada, rocas, vetas de hierro con vetas anaranjadas y matas de hierba de ceniza.
    /// </summary>
    public static class NatureFactory
    {
        private static readonly Color Bark = new Color(0.33f, 0.22f, 0.14f);
        private static readonly Color Leaves = new Color(0.22f, 0.42f, 0.16f);
        private static readonly Color AshLeaves = new Color(0.36f, 0.4f, 0.3f);
        private static readonly Color Stone = new Color(0.46f, 0.45f, 0.43f);
        private static readonly Color DarkStone = new Color(0.3f, 0.28f, 0.27f);
        private static readonly Color Iron = new Color(0.8f, 0.42f, 0.18f);
        private static readonly Color Herb = new Color(0.55f, 0.35f, 0.62f);
        private static readonly Color HerbStem = new Color(0.3f, 0.5f, 0.25f);

        public static GameObject Build(GatherKind kind, Transform parent, float seed, bool ash)
        {
            var root = new GameObject("Modelo");
            root.transform.SetParent(parent, false);
            float size = 0.85f + Mathf.Repeat(seed * 0.618f, 0.4f);
            switch (kind)
            {
                case GatherKind.Arbol: Tree(root.transform, size, ash ? AshLeaves : Leaves); break;
                case GatherKind.Roca: Rock(root.transform, size, Stone); break;
                case GatherKind.VetaHierro:
                    Rock(root.transform, size, DarkStone);
                    for (int i = 0; i < 4; i++)
                        M.Shape(PrimitiveType.Sphere, root.transform, Around(i, 0.55f * size, 0.5f * size),
                            Vector3.one * 0.25f, Iron);
                    break;
                default: Bush(root.transform); break;
            }
            root.transform.localRotation = Quaternion.Euler(0f, seed * 47f % 360f, 0f);
            return root;
        }

        private static void Tree(Transform root, float size, Color leaves)
        {
            M.Shape(PrimitiveType.Cylinder, root, new Vector3(0f, 1.3f * size, 0f), new Vector3(0.45f, 1.3f, 0.45f) * size,
                Bark, true);
            M.Shape(PrimitiveType.Sphere, root, new Vector3(0f, 3.1f * size, 0f), new Vector3(2.6f, 2.2f, 2.6f) * size, leaves);
            M.Shape(PrimitiveType.Sphere, root, new Vector3(0.5f, 3.9f * size, 0.2f), new Vector3(1.8f, 1.6f, 1.8f) * size,
                leaves * 1.1f);
            M.Shape(PrimitiveType.Sphere, root, new Vector3(-0.6f, 3.5f * size, -0.3f), new Vector3(1.6f, 1.4f, 1.6f) * size,
                leaves * 0.9f);
        }

        private static void Rock(Transform root, float size, Color color)
        {
            M.Shape(PrimitiveType.Sphere, root, new Vector3(0f, 0.4f * size, 0f), new Vector3(1.6f, 1f, 1.3f) * size, color, true);
            M.Shape(PrimitiveType.Sphere, root, new Vector3(0.5f, 0.3f * size, 0.4f), new Vector3(0.9f, 0.7f, 0.8f) * size,
                color * 0.9f);
        }

        private static void Bush(Transform root)
        {
            for (int i = 0; i < 5; i++)
            {
                var stem = Around(i, 0.25f, 0.2f);
                M.Shape(PrimitiveType.Cylinder, root, stem, new Vector3(0.05f, 0.2f, 0.05f), HerbStem);
                M.Shape(PrimitiveType.Sphere, root, stem + Vector3.up * 0.25f, Vector3.one * 0.18f, Herb);
            }
        }

        private static Vector3 Around(int i, float radius, float height)
        {
            float angle = i * 1.9f;
            return new Vector3(Mathf.Cos(angle) * radius, height, Mathf.Sin(angle) * radius);
        }
    }
}
