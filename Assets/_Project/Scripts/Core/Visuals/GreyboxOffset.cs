using Specimen.Core.Common;

namespace Specimen.Core.Visuals
{
    /// <summary>
    /// Dónde colocar el centro de la primitiva greybox (pivote centrado) para que respete
    /// el pivote del asset real: así cubos y prefabs ocupan el mismo sitio.
    /// </summary>
    public static class GreyboxOffset
    {
        public static VisualOffset CenterOf(GreyboxStyle style, VisualAnchor anchor)
        {
            var s = Guard.NotNull(style, nameof(style)).Scale;
            return anchor == VisualAnchor.Esquina
                ? new VisualOffset(s.X / 2f, s.Y / 2f, s.Z / 2f)
                : new VisualOffset(0f, s.Y / 2f, 0f);
        }
    }
}
