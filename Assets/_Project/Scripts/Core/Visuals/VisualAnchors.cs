namespace Specimen.Core.Visuals
{
    /// <summary>Pivote de cada ID lógico: el entorno modular va por esquina, el resto por la base.</summary>
    public static class VisualAnchors
    {
        public static VisualAnchor For(VisualId id)
        {
            switch (id)
            {
                case VisualId.Suelo:
                case VisualId.Muro:
                case VisualId.Puerta:
                case VisualId.Ascensor:
                    return VisualAnchor.Esquina;
                default:
                    return VisualAnchor.Base;
            }
        }
    }
}
