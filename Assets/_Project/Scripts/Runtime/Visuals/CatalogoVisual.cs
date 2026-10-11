using System;
using System.Collections.Generic;
using Specimen.Core.Visuals;
using UnityEngine;

namespace Specimen.Runtime.Visuals
{
    /// <summary>
    /// Catálogo editable ID → prefab (GDD 16). Vacío por defecto: todo sale en greybox.
    /// Sustituir cubos por assets reales es arrastrar prefabs aquí, sin tocar código.
    /// </summary>
    [CreateAssetMenu(menuName = "SPECIMEN/Catálogo visual", fileName = "CatalogoVisual")]
    public sealed class CatalogoVisual : ScriptableObject
    {
        [Serializable]
        public struct Entrada
        {
            public VisualId id;
            public GameObject prefab;
        }

        [SerializeField] private List<Entrada> entradas = new List<Entrada>();

        public IVisualCatalog<GameObject> Build(IGreyboxDefaults defaults)
        {
            var catalog = new VisualCatalog<GameObject>(defaults);
            foreach (var entrada in entradas)
                if (entrada.prefab != null)
                    catalog.Assign(entrada.id, entrada.prefab);
            return catalog;
        }
    }
}
