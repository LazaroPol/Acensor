using System;
using System.Collections.Generic;

namespace Specimen.Core.Visuals
{
    /// <summary>
    /// Estilos greybox por defecto (GDD 16): personajes en cápsula, entorno en cubos de 1 m,
    /// colores planos distinguibles para leer la escena sin assets.
    /// </summary>
    public sealed class DefaultGreyboxStyles : IGreyboxDefaults
    {
        private static readonly Dictionary<VisualId, GreyboxStyle> Styles =
            new Dictionary<VisualId, GreyboxStyle>
            {
                [VisualId.Jugador] = Style(GreyboxPrimitive.Capsula, 0.2f, 0.5f, 0.9f, 1f, 1.8f, 1f),
                [VisualId.Suelo] = Style(GreyboxPrimitive.Cubo, 0.5f, 0.5f, 0.5f, 1f, 0.1f, 1f),
                [VisualId.Muro] = Style(GreyboxPrimitive.Cubo, 0.7f, 0.7f, 0.7f, 1f, 3f, 0.2f),
                [VisualId.Puerta] = Style(GreyboxPrimitive.Cubo, 0.6f, 0.4f, 0.2f, 1f, 2.2f, 0.1f),
                [VisualId.Ascensor] = Style(GreyboxPrimitive.Cubo, 0.9f, 0.8f, 0.2f, 2f, 3f, 2f),
                [VisualId.Loot] = Style(GreyboxPrimitive.Esfera, 0.2f, 0.8f, 0.3f, 0.4f, 0.4f, 0.4f),
                [VisualId.Enemigo] = Style(GreyboxPrimitive.Capsula, 0.9f, 0.2f, 0.2f, 1f, 1.8f, 1f)
            };

        public GreyboxStyle For(VisualId id)
            => Styles.TryGetValue(id, out var style)
                ? style
                : throw new ArgumentOutOfRangeException(nameof(id), id, "VisualId sin estilo greybox.");

        private static GreyboxStyle Style(GreyboxPrimitive primitive,
            float r, float g, float b, float x, float y, float z)
            => new GreyboxStyle(primitive, new GreyboxColor(r, g, b), new VisualScale(x, y, z));
    }
}
