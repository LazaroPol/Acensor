using Specimen.Core.Prototype;
using Specimen.Core.Visuals;
using UnityEngine;

namespace Specimen.Runtime.Visuals
{
    /// <summary>Instancia visuales por ID lógico: prefab del catálogo o, si falta, greybox.</summary>
    public sealed class VisualSpawner : MonoBehaviour
    {
        private IVisualCatalog<GameObject> _catalog;

        public void Init(IVisualCatalog<GameObject> catalog) => _catalog = catalog;

        public GameObject Spawn(LevelPlacement placement)
        {
            var resolution = _catalog.Resolve(placement.Id);
            var instance = resolution.HasAsset
                ? Instantiate(resolution.Asset, transform)
                : GreyboxFactory.Create(placement.Id, resolution.Greybox, transform);
            instance.transform.localPosition = new Vector3(placement.X, placement.Y, placement.Z);
            instance.transform.localRotation = Quaternion.Euler(0f, placement.YawDegrees, 0f);
            return instance;
        }
    }
}
