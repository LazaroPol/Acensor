using UnityEngine;

namespace Specimen.Runtime.Crafting.Authoring
{
    /// <summary>
    /// Base de los assets de oficios: guarda los datos serializables del núcleo (<typeparamref name="TData"/>)
    /// y deja la conversión a los mappers de <c>Specimen.Core.Crafting.Authoring</c>.
    /// </summary>
    public abstract class CraftingAsset<TData> : ScriptableObject where TData : class, new()
    {
        [SerializeField] private TData data = new TData();

        public TData Data
        {
            get => data ?? (data = new TData());
            set => data = value ?? new TData();
        }
    }
}
