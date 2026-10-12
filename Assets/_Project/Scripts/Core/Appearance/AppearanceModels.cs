using Specimen.Core.Common;

namespace Specimen.Core.Appearance
{
    /// <summary>
    /// Nombres de modelo y textura de los packs de Quaternius para una apariencia: el modelo base lleva el
    /// esqueleto y el Animator; el atuendo (si hay) y los peinados se acoplan a ese esqueleto.
    /// </summary>
    public static class AppearanceModels
    {
        public static string BaseModel(CharacterAppearance look)
        {
            Guard.NotNull(look, nameof(look));
            return look.Outfit == OutfitId.Explorador
                ? $"{Sex(look)}_Ranger"
                : $"Superhero_{Sex(look)}_FullBody";
        }

        public static string OutfitModel(CharacterAppearance look)
            => Guard.NotNull(look, nameof(look)).Outfit == OutfitId.Campesino ? $"{Sex(look)}_Peasant" : null;

        /// <summary>Clave del material del atuendo («Ranger», «Peasant»); null si va sin atuendo.</summary>
        public static string OutfitMaterialKey(CharacterAppearance look)
        {
            switch (Guard.NotNull(look, nameof(look)).Outfit)
            {
                case OutfitId.Explorador: return "Ranger";
                case OutfitId.Campesino: return "Peasant";
                default: return null;
            }
        }

        /// <summary>Textura de la variante («T_Ranger_3_BaseColor»); null para la variante base.</summary>
        public static string OutfitTexture(CharacterAppearance look)
        {
            string key = OutfitMaterialKey(look);
            return key == null || look.Variant == 1 ? null : $"T_{key}_{look.Variant}_BaseColor";
        }

        private static string Sex(CharacterAppearance look) => look.Body == BodyType.Masculino ? "Male" : "Female";
    }
}
