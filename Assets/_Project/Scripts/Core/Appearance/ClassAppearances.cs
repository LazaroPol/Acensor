using System.Collections.Generic;
using Specimen.Core.Classes;

namespace Specimen.Core.Appearance
{
    /// <summary>Aspecto de cada kit de clase (GDD §4): silueta y paleta distintas para leerlos de un vistazo.</summary>
    public static class ClassAppearances
    {
        private static readonly Dictionary<ClassKitId, CharacterAppearance> Looks =
            new Dictionary<ClassKitId, CharacterAppearance>
            {
                [ClassKitId.Bastion] = new CharacterAppearance(BodyType.Masculino, OutfitId.Explorador),
                [ClassKitId.Vigia] = new CharacterAppearance(BodyType.Femenino, OutfitId.Explorador, 3),
                [ClassKitId.Oraculo] = new CharacterAppearance(BodyType.Femenino, OutfitId.Campesino, 1, "Hair_Buns"),
                [ClassKitId.DruidaCeniza] = new CharacterAppearance(BodyType.Masculino, OutfitId.Campesino, 2,
                    "Hair_Buzzed", "Hair_Beard"),
                [ClassKitId.Duelista] = new CharacterAppearance(BodyType.Masculino, OutfitId.Ajustado, 1,
                    "Hair_SimpleParted"),
                [ClassKitId.Arcanista] = new CharacterAppearance(BodyType.Femenino, OutfitId.Ajustado, 1, "Hair_Long")
            };

        public static CharacterAppearance For(ClassKitId kit) => Looks[kit];
    }
}
