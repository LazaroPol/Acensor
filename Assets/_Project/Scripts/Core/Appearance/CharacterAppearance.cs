using System;
using System.Collections.Generic;

namespace Specimen.Core.Appearance
{
    /// <summary>Aspecto de un personaje: cuerpo, atuendo, variante de textura (1 = la base) y peinados.</summary>
    public sealed class CharacterAppearance
    {
        public CharacterAppearance(BodyType body, OutfitId outfit, int variant = 1, params string[] hair)
        {
            if (variant < 1) throw new ArgumentOutOfRangeException(nameof(variant), variant, "La variante empieza en 1.");
            Body = body;
            Outfit = outfit;
            Variant = variant;
            Hair = hair ?? Array.Empty<string>();
        }

        public BodyType Body { get; }
        public OutfitId Outfit { get; }
        public int Variant { get; }
        public IReadOnlyList<string> Hair { get; }
    }
}
