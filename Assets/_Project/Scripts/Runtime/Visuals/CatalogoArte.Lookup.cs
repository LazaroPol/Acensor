using Specimen.Core.Classes;
using Specimen.Core.Professions;
using UnityEngine;

namespace Specimen.Runtime.Visuals
{
    /// <summary>Búsquedas del catálogo de arte; devuelven null si no hay prefab (→ greybox).</summary>
    public sealed partial class CatalogoArte
    {
        public GameObject ClassPrefab(ClassKitId kit)
        {
            foreach (var entry in clases) if (entry.kit == kit) return entry.prefab;
            return null;
        }

        public GameObject EnemyPrefab(string id)
        {
            foreach (var entry in enemigos) if (entry.id == id) return entry.prefab;
            return null;
        }

        public GameObject BenchPrefab(ProfessionId profession)
        {
            foreach (var entry in bancos) if (entry.profesion == profession) return entry.prefab;
            return null;
        }

        public GameObject HousePrefab(string template) => Named(casas, template);

        public GameObject DecorPrefab(string model) => Named(decorado, $"Decor_{model}");

        private static GameObject Named(System.Collections.Generic.List<GameObject> list, string name)
        {
            foreach (var prefab in list) if (prefab != null && prefab.name == name) return prefab;
            return null;
        }
    }
}
