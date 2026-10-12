using System;
using System.Collections.Generic;
using Specimen.Core.Classes;
using Specimen.Core.Professions;
using UnityEngine;

namespace Specimen.Runtime.Visuals
{
    /// <summary>
    /// Prefabs generados desde los packs de arte (SPECIMEN › Arte › Generar): personaje por kit,
    /// enemigo por id de bestiario, banco por profesión, decorado y controladores de animación.
    /// Vive en Data/Arte (local, fuera de git); si falta, el juego usa el greybox.
    /// </summary>
    [CreateAssetMenu(menuName = "SPECIMEN/Catálogo de arte", fileName = "CatalogoArte")]
    public sealed partial class CatalogoArte : ScriptableObject
    {
        [Serializable]
        public struct Clase
        {
            public ClassKitId kit;
            public GameObject prefab;
        }

        [Serializable]
        public struct Enemigo
        {
            public string id;
            public GameObject prefab;
        }

        [Serializable]
        public struct Banco
        {
            public ProfessionId profesion;
            public GameObject prefab;
        }

        public RuntimeAnimatorController heroe;
        public RuntimeAnimatorController monstruo;
        public GameObject proyectil;
        public List<Clase> clases = new List<Clase>();
        public List<Enemigo> enemigos = new List<Enemigo>();
        public List<Banco> bancos = new List<Banco>();
        public List<GameObject> decorado = new List<GameObject>();

        /// <summary>Casas del poblado montadas con piezas modulares (nombre = plantilla «Casa_4x6_Yeso»).</summary>
        public List<GameObject> casas = new List<GameObject>();
    }
}
