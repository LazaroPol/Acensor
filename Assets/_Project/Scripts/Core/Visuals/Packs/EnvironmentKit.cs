using System.Collections.Generic;

namespace Specimen.Core.Visuals.Packs
{
    /// <summary>
    /// Piezas del Medieval Village MegaKit para los IDs del greybox (se ajustan a su caja y pivote) y
    /// decorado libre para el Campo de pruebas.
    /// </summary>
    public static class EnvironmentKit
    {
        private static readonly Dictionary<VisualId, string> Pieces = new Dictionary<VisualId, string>
        {
            [VisualId.Suelo] = "Floor_UnevenBrick",
            [VisualId.Muro] = "Wall_UnevenBrick_Straight",
            [VisualId.Puerta] = "Wall_UnevenBrick_Door_Round",
            [VisualId.Loot] = "Prop_Crate"
        };

        public static readonly IReadOnlyList<string> Decor = new[]
        {
            "Prop_Wagon", "Prop_Crate", "Prop_WoodenFence_Single", "Prop_WoodenFence_Extension1",
            "Prop_Support", "Prop_MetalFence_Ornament", "Prop_Vine1", "Stairs_Exterior_Straight"
        };

        public static IEnumerable<VisualId> Fitted => Pieces.Keys;

        /// <summary>Modelo de la pieza; null si el ID no se sustituye con el pueblo (personajes, ascensor).</summary>
        public static string ModelFor(VisualId id) => Pieces.TryGetValue(id, out var model) ? model : null;

        /// <summary>Caja greybox a la que se ajusta la pieza: la puerta ocupa el hueco de un muro entero.</summary>
        public static VisualId FitBoxOf(VisualId id) => id == VisualId.Puerta ? VisualId.Muro : id;
    }
}
