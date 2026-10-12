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
    }
}
