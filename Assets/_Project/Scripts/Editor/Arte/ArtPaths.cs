using System;

namespace Specimen.Editor.Arte
{
    /// <summary>Rutas del arte de terceros (local, fuera de git) y de lo que se genera a partir de él.</summary>
    public static class ArtPaths
    {
        public const string ThirdParty = "Assets/_Project/Art/ThirdParty";
        public const string Generated = "Assets/_Project/Art/Generado";
        public const string Materials = Generated + "/Materiales";
        public const string Animation = Generated + "/Animacion";
        public const string Characters = Generated + "/Personajes";
        public const string Enemies = Generated + "/Enemigos";
        public const string Environment = Generated + "/Entorno";
        public const string Furniture = Generated + "/Mobiliario";
        public const string Weapons = Generated + "/Armas";
        public const string ArtCatalog = "Assets/_Project/Data/Arte/CatalogoArte.asset";
        public const string VisualCatalog = "Assets/_Project/Data/Visuals/CatalogoVisual.asset";
        public const string KevinIglesias = "Assets/Kevin Iglesias";
        public const string PackagesFolder = "ThirdPartyPackages";

        public const string Characters3P = ThirdParty + "/Q_Personajes";
        public const string Hair3P = ThirdParty + "/Q_Personajes/Pelo";
        public const string Outfits3P = ThirdParty + "/Q_Atuendos";
        public const string Animations3P = ThirdParty + "/Q_Animaciones";
        public const string Bestiary3P = ThirdParty + "/Q_Bestiario";
        public const string Village3P = ThirdParty + "/Q_Pueblo";
        public const string Tools3P = ThirdParty + "/KayKit_Herramientas";
        public const string Weapons3P = ThirdParty + "/RPGWeapons";

        public static bool IsThirdParty(string path) => Starts(path, ThirdParty + "/");

        /// <summary>Modelos con el esqueleto común de Quaternius: se importan como Humanoid.</summary>
        public static bool IsHumanoidSource(string path)
            => Starts(path, Characters3P + "/") || Starts(path, Outfits3P + "/")
               || Starts(path, Animations3P + "/") || Starts(path, Bestiary3P + "/");

        public static bool IsAnimationLibrary(string path) => Starts(path, Animations3P + "/");

        private static bool Starts(string path, string prefix)
            => path != null && path.Replace('\\', '/').StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
    }
}
