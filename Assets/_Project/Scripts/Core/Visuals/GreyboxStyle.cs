namespace Specimen.Core.Visuals
{
    /// <summary>Cómo se dibuja un ID en greybox: primitiva, color y escala por defecto.</summary>
    public sealed class GreyboxStyle
    {
        public GreyboxPrimitive Primitive { get; }
        public GreyboxColor Color { get; }
        public VisualScale Scale { get; }

        public GreyboxStyle(GreyboxPrimitive primitive, GreyboxColor color, VisualScale scale)
        {
            Primitive = primitive;
            Color = color;
            Scale = scale;
        }
    }
}
