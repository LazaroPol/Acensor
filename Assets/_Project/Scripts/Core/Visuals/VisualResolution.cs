using Specimen.Core.Common;

namespace Specimen.Core.Visuals
{
    /// <summary>Resultado de resolver un ID: el asset asignado o el fallback greybox.</summary>
    public sealed class VisualResolution<TAsset> where TAsset : class
    {
        public VisualId Id { get; }
        public TAsset Asset { get; }
        public GreyboxStyle Greybox { get; }
        public bool HasAsset => Asset != null;

        private VisualResolution(VisualId id, TAsset asset, GreyboxStyle greybox)
        {
            Id = id;
            Asset = asset;
            Greybox = greybox;
        }

        public static VisualResolution<TAsset> FromAsset(VisualId id, TAsset asset)
            => new VisualResolution<TAsset>(id, Guard.NotNull(asset, nameof(asset)), null);

        public static VisualResolution<TAsset> FromGreybox(VisualId id, GreyboxStyle greybox)
            => new VisualResolution<TAsset>(id, null, Guard.NotNull(greybox, nameof(greybox)));
    }
}
