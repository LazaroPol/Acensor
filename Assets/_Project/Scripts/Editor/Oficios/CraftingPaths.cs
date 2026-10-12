namespace Specimen.Editor.Oficios
{
    /// <summary>Dónde viven los assets de oficios (GDD 17.7: <c>Assets/_Project/Data/Crafting</c>).</summary>
    public static class CraftingPaths
    {
        public const string Root = "Assets/_Project/Data/Crafting";
        public const string Database = Root + "/CraftingDatabase.asset";
        public const string Items = Root + "/Items";
        public const string Recipes = Root + "/Recetas";
        public const string Knowledge = Root + "/Conocimientos";
        public const string Parts = Root + "/Piezas";
        public const string Mods = Root + "/Mods";
        public const string Curve = Root + "/CurvaOficio.asset";
        public const string Quality = Root + "/ReglasCalidad.asset";

        public static string AssetIn(string folder, string id) => $"{folder}/{id}.asset";
    }
}
