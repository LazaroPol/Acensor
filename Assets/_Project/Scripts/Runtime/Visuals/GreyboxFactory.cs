using Specimen.Core.Visuals;
using UnityEngine;

namespace Specimen.Runtime.Visuals
{
    /// <summary>Crea la primitiva greybox de un estilo, respetando el pivote del asset real.</summary>
    public static class GreyboxFactory
    {
        public static GameObject Create(VisualId id, GreyboxStyle style, Transform parent)
        {
            var root = new GameObject($"{id} (greybox)");
            root.transform.SetParent(parent, false);
            var shape = GameObject.CreatePrimitive(ToPrimitive(style.Primitive));
            shape.name = "Forma";
            shape.transform.SetParent(root.transform, false);
            var center = GreyboxOffset.CenterOf(style, VisualAnchors.For(id));
            shape.transform.localPosition = new Vector3(center.X, center.Y, center.Z);
            shape.transform.localScale = ToShapeScale(style);
            shape.GetComponent<Renderer>().material.color =
                new Color(style.Color.R, style.Color.G, style.Color.B);
            return root;
        }

        // La cápsula y el cilindro de Unity miden 2 m de alto con escala 1.
        private static Vector3 ToShapeScale(GreyboxStyle style)
        {
            var s = style.Scale;
            var tall = style.Primitive == GreyboxPrimitive.Capsula || style.Primitive == GreyboxPrimitive.Cilindro;
            return new Vector3(s.X, tall ? s.Y / 2f : s.Y, s.Z);
        }

        private static PrimitiveType ToPrimitive(GreyboxPrimitive primitive)
        {
            switch (primitive)
            {
                case GreyboxPrimitive.Capsula: return PrimitiveType.Capsule;
                case GreyboxPrimitive.Cilindro: return PrimitiveType.Cylinder;
                case GreyboxPrimitive.Esfera: return PrimitiveType.Sphere;
                default: return PrimitiveType.Cube;
            }
        }
    }
}
