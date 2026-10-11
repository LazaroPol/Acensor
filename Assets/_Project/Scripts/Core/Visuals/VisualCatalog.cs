using System.Collections.Generic;
using Specimen.Core.Common;

namespace Specimen.Core.Visuals
{
    /// <summary>
    /// Catálogo ID → asset. Sin asset asignado, el ID cae en su estilo greybox por defecto,
    /// así sustituir cubos por assets reales es solo asignar, sin tocar código.
    /// </summary>
    public sealed class VisualCatalog<TAsset> : IVisualCatalog<TAsset> where TAsset : class
    {
        private readonly Dictionary<VisualId, TAsset> _assets = new Dictionary<VisualId, TAsset>();
        private readonly IGreyboxDefaults _defaults;

        public VisualCatalog(IGreyboxDefaults defaults)
        {
            _defaults = Guard.NotNull(defaults, nameof(defaults));
        }

        public int AssignedCount => _assets.Count;

        public void Assign(VisualId id, TAsset asset)
            => _assets[id] = Guard.NotNull(asset, nameof(asset));

        public bool Unassign(VisualId id) => _assets.Remove(id);

        public VisualResolution<TAsset> Resolve(VisualId id)
            => _assets.TryGetValue(id, out var asset)
                ? VisualResolution<TAsset>.FromAsset(id, asset)
                : VisualResolution<TAsset>.FromGreybox(id, _defaults.For(id));
    }
}
